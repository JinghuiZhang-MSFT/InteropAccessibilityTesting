using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;

namespace TDWebPageListGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Util.Initialize();
            outputPath.Text = Environment.CurrentDirectory;
        }

        /// <summary>
        /// Record all web page urls of a protocol, including the protocol entry url
        /// </summary>
        private void generatePageList_Click(object sender, EventArgs e)
        {
            string tdName = Util.webDriver.Title.Split(':')[0];
            webPageExportingStatusText.Text = "Recording URLs for " + tdName + "...";
            webPageExportingStatusText.Refresh();
            using (StreamWriter sw = new StreamWriter(outputPath.Text + "\\" + tdName + ".txt"))
            {
                sw.WriteLine(Util.EntryUrl);
                foreach (object webpageUrl in tocItemlList.Items)
                {
                    sw.WriteLine(webpageUrl.ToString());
                }
            }

            webPageExportingStatusText.Text = "All web page Urls are recorded!";
            webPageExportingStatusText.Refresh();
        }

        private void pathBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Choose file path";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                outputPath.Text = dialog.SelectedPath;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            string pathValue = System.Environment.GetEnvironmentVariable("PATH");
            int index = pathValue.LastIndexOf(";");
            pathValue = pathValue.Substring(0, index);
            System.Environment.SetEnvironmentVariable("PATH", pathValue);
        }

        private void getTOCItems_Click(object sender, EventArgs e)
        {
            if (Util.webDriver == null)
            {
                Util.webDriver = new ChromeDriver(System.IO.Directory.GetCurrentDirectory() + "\\AxeScripts\\");
            }
            if (protocolEntryUrl.Text.Equals(string.Empty))
            {
                MessageBox.Show("Entry URL must be input at first!!");
            }
            else
            {
                tocItemlList.Items.Clear();
                protocolListImportingStatusText.Text = "Importing TOC item list...";
                protocolListImportingStatusText.Refresh();
                Util.webDriver.Navigate().GoToUrl(Util.EntryUrl);
                IWebElement entryEle = Util.webDriver.FindElement(By.CssSelector("div.toclevel1>a:nth-child(2)"));
                string itemName = entryEle.Text.Split(':')[0];

                IWebElement currentEle = Util.FindElement(By.CssSelector("div.current>a"));
                while (currentEle != null)
                {
                    if (currentEle.GetAttribute("class").Equals("toc_collapsed"))
                    {
                        int followingCount = currentEle.FindElements(By.XPath("./following::a")).Count;
                        Util.Click(currentEle);
                        //Wait until the sub menu expands
                        System.Threading.Thread.Sleep(1000);
                        while (!currentEle.GetAttribute("class").Equals("toc_expanded")
                           || currentEle.FindElements(By.XPath("./following::a")).Count == followingCount)
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    else if (!currentEle.GetAttribute("title").Equals(string.Empty) && currentEle.GetAttribute("title") != null)
                    {
                        if (tocItemlList.Items.Count == 1)
                        {
                            if (!tocItemlList.Items.Contains(currentEle.GetAttribute("href")))
                            {
                                tocItemlList.Items.Add(currentEle.GetAttribute("href"), true);
                            }
                        }
                        else
                        {
                            tocItemlList.Items.Add(currentEle.GetAttribute("href"), true);
                        }
                        //The responding time after clicking collapse arrows of the following elements are a bit long,will loop
                        if (currentEle.GetAttribute("title").Equals("Overview Documents") || currentEle.GetAttribute("title").Equals("Technical Documents"))
                        {
                            while (Util.FindElement(currentEle, By.XPath("./following::a")) == null)
                            {
                                System.Threading.Thread.Sleep(1000);
                            }
                        }
                    }

                    currentEle = Util.FindElement(currentEle, By.XPath("./following::a"));
                    if (currentEle != null && Util.FindElement(currentEle, By.XPath("./parent::div[@data-toclevel]")) == null)
                    {
                        break;
                    }
                }

                protocolNameText.Text = Util.webDriver.Title;
                itemCountText.Text = "Total items: " + tocItemlList.Items.Count.ToString();
                protocolListImportingStatusText.Text = "TOC item list is generated!";
                protocolListImportingStatusText.Refresh();
            }
        }

        private void selectAll_CheckStateChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < tocItemlList.Items.Count; i++)
            {
                tocItemlList.SetItemChecked(i, selectAll.Checked);
            }
        }

        private void protocolEntryUrl_TextChanged(object sender, EventArgs e)
        {
            Util.EntryUrl = protocolEntryUrl.Text;
        }

        private void tocItemList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.Equals(CheckState.Checked))
            {
                generatePageList.Enabled = true;
                testPages.Enabled = true;
            }
            else
            {
                if (tocItemlList.CheckedItems.Count == 1)
                {
                    generatePageList.Enabled = false;
                    testPages.Enabled = false;
                }
            }
        }

        private void importURLList_Click(object sender, EventArgs e)
        {
            string selectedFile = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = outputPath.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedFile = dialog.FileName;
                StreamReader sr = File.OpenText(selectedFile);
                tocItemlList.Items.Clear();
                while (sr.Peek() >= 0)
                {
                    string currURL = sr.ReadLine();
                    tocItemlList.Items.Add(currURL, true);
                }
            }
            itemCountText.Text = "Total items: " + tocItemlList.Items.Count.ToString();
        }

        private void testPages_Click(object sender, EventArgs e)
        {
            DateTime oldT = DateTime.Now;
            testStatus.Text = "Writing URLs into Grunt file...";
            testStatus.Refresh();
            Util.ClearConfigureFile();
            for (int i = tocItemlList.CheckedItems.Count - 1; i >= 0; i--)
            {
                Util.ConfigureTestUrls(tocItemlList.CheckedItems[i].ToString());
            }
            List<string> ruleList = new List<string>();
            if (testRuleGridView.Visible)
            {
                for (int j = 0; j < testRuleGridView.RowCount; j++)
                {
                    if (testRuleGridView.Rows[j].Cells[0].Value.ToString().Equals(Boolean.TrueString))
                    {
                        ruleList.Add(testRuleGridView.Rows[j].Cells[1].Value.ToString());
                    }
                }
                if (ruleList.Count > 0)
                {
                    Util.SetTestRules(ruleList);
                }
                else
                {
                    MessageBox.Show("No rule is selected", "Warning");
                    return;
                }
            }
            else
            {
                //Set the default test rules
                ruleList.Add("color-contrast");
                ruleList.Add("image-alt");
                ruleList.Add("link-name");
                Util.SetTestRules(ruleList);
            }
            testStatus.Text = "Start to test...";
            testStatus.Refresh();
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c grunt test --force";
            p.StartInfo.WorkingDirectory = System.Environment.CurrentDirectory + "\\AxeScripts\\";
            p.Start();

            p.WaitForExit();
            DateTime newT = DateTime.Now;
            testStatus.Text = "Testing completed!";
            testStatus.Refresh();
            MessageBox.Show(string.Format("Test completed. Result path: {0}. Time cost: {1}", System.Environment.CurrentDirectory + "\\Results\\", (newT - oldT).ToString()));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Util.IsNodeJSInstalled())
            {
                DialogResult boxResult = MessageBox.Show("Node.JS must be installed at first. \n 1. Click Yes to the installation page. After installing NodeJS,input `npm install -g grunt-cli` in NodeJS command prompt to install Grunt globally.\n 2. Click Cancel to quit the application.", "Warning", MessageBoxButtons.YesNoCancel);
                if (boxResult.Equals(DialogResult.Yes))
                {
                    Util.webDriver.Navigate().GoToUrl("https://nodejs.org/en/");
                }
                else if (boxResult.Equals(DialogResult.Cancel))
                {
                    System.Environment.Exit(0);
                }
            }
            string pathValue = System.Environment.GetEnvironmentVariable("PATH");
            pathValue += ";" + System.Environment.CurrentDirectory + "\\AxeScripts";
            System.Environment.SetEnvironmentVariable("PATH", pathValue);
        }

        private void configureRules_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("ruleTable");

            dt.Columns.Add(new DataColumn("Check", typeof(Boolean)));

            dt.Columns.Add(new DataColumn("Id", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));

            XmlDocument doc = new XmlDocument();
            doc.Load("AxeRules.xml");

            XmlNodeList nodes = doc.GetElementsByTagName("Rule");
            foreach (XmlNode xnode in nodes)
            {
                DataRow row = dt.NewRow();
                row["Id"] = xnode.SelectSingleNode("child::Id").InnerText;
                //Set the default check rules
                if (row["Id"].Equals("color-contrast") || row["Id"].Equals("image-alt") || row["Id"].Equals("link-name"))
                {
                    row["Check"] = true;
                }
                row["Description"] = xnode.SelectSingleNode("child::Description").InnerText;

                dt.Rows.Add(row);
            }

            DataSet ds = new DataSet("newDS");
            ds.Tables.Add(dt);

            testRuleGridView.DataSource = ds.Tables[0];
            testRuleGridView.Visible = true;
        }
    }
}
