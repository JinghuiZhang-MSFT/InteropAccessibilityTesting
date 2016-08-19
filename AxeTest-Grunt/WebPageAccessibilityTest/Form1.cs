using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

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
            if (tocItemlList.Items[0].ToString().StartsWith("[MS-"))
            {
                foreach (string protocol in tocItemlList.CheckedItems)
                {
                    webPageExportingStatusText.Text = "Recording web page URLs for " + protocol + "...";
                    webPageExportingStatusText.Refresh();
                    using (StreamWriter sw = new StreamWriter(outputPath.Text + "\\" + protocol + ".txt"))
                    {
                        IWebElement currentEle = Util.FindElement(By.XPath("//div/a[starts-with(@title,'" + protocol + "')]/preceding-sibling::a"));
                        int protocolNodeCount = 0;
                        while (currentEle != null)
                        {
                            if (currentEle.GetAttribute("class").Equals("toc_collapsed"))
                            {
                                IWebElement itemLink = currentEle.FindElement(By.XPath("./following::a"));
                                if (itemLink.GetAttribute("title").StartsWith("[MS-"))
                                {
                                    protocolNodeCount++;
                                    if (protocolNodeCount >= 2)
                                    {
                                        // All the current protocol's toc items are recorded.
                                        // Complete the loop
                                        break;
                                    }
                                }
                                Util.Click(currentEle);
                                //Wait until the sub menu expands
                                do
                                {
                                    System.Threading.Thread.Sleep(2000);
                                } while (!currentEle.GetAttribute("class").Equals("toc_expanded"));
                            }
                            //If the sub menu has been expanded, no action.
                            else if (currentEle.GetAttribute("class").Equals("toc_expanded"))
                            {
                                IWebElement itemLink = currentEle.FindElement(By.XPath("./following::a"));
                                if (itemLink.GetAttribute("title").StartsWith("[MS-"))
                                {
                                    protocolNodeCount++;

                                    if (protocolNodeCount >= 2)
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (!currentEle.GetAttribute("title").Equals(string.Empty) && currentEle.GetAttribute("title") != null)
                            {
                                sw.WriteLine(currentEle.GetAttribute("href"));
                            }
                            else
                            {
                                break;
                            }
                            try
                            {
                                currentEle = currentEle.FindElement(By.XPath("./following::a"));
                            }
                            catch (NoSuchElementException)
                            {
                                currentEle = null;
                            }
                        }
                    }
                }
            }
            else
            {
                string tdName = Util.webDriver.Title.Split(':')[0];
                webPageExportingStatusText.Text = "Recording URLs for " + tdName + "...";
                webPageExportingStatusText.Refresh();
                using (StreamWriter sw = new StreamWriter(outputPath.Text + "\\" + tdName + ".txt"))
                {
                    sw.WriteLine(Util.EntryUrl);
                    foreach (string webpageUrl in Util.TOCItems.Values)
                    {
                        sw.WriteLine(webpageUrl);
                    }
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
                Util.TOCItems.Clear();
                protocolListImportingStatusText.Text = "Importing TOC item list...";
                protocolListImportingStatusText.Refresh();
                Util.webDriver.Navigate().GoToUrl(Util.EntryUrl);
                IWebElement entryEle = Util.webDriver.FindElement(By.CssSelector("div.toclevel1>a:nth-child(2)"));
                string itemName = entryEle.Text.Split(':')[0];
                if (!itemName.StartsWith("[MS-"))
                {
                    //Entry item does not begin with [MS-, which means its child items are protocol items starting with [MS-
                    int protocolCount = 0;
                    var wait = new WebDriverWait(Util.webDriver, TimeSpan.FromSeconds(20));
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[@data-toclevel='2']")));
                    //Check whether all the protocols are included.
                    while (protocolCount != Util.webDriver.FindElements(By.XPath("//div[@data-toclevel='2']")).Count)
                    {
                        protocolCount = Util.webDriver.FindElements(By.XPath("//div[@data-toclevel='2']")).Count;
                        System.Threading.Thread.Sleep(3000);
                    }
                    for (int i = 0; i < protocolCount; i++)
                    {
                        IWebElement tdEntryEle = Util.webDriver.FindElement(By.XPath("//div[@data-toclevel='1']/following::div[@data-toclevel='2'][" + (int)(i + 1) + "]"));
                        string tdName = tdEntryEle.Text;

                        tocItemlList.Items.Add(tdName.Split(':')[0]);
                        Util.TOCItems.Add(tdName, tdEntryEle.GetAttribute("href"));
                    }
                }
                else
                {
                    string tdName = "Entry Page: " + Util.webDriver.Title.Split(':')[0];
                    tocItemlList.Items.Add(tdName, true);
                    Util.TOCItems.Add(tdName, Util.EntryUrl);

                    IWebElement currentEle = Util.FindElement(By.CssSelector("div.toclevel2>a"));
                    while (currentEle != null)
                    {
                        if (currentEle.GetAttribute("class").Equals("toc_collapsed"))
                        {
                            Util.Click(currentEle);
                            //Wait until the sub menu expands
                            do
                            {
                                System.Threading.Thread.Sleep(2000);
                            } while (!currentEle.GetAttribute("class").Equals("toc_expanded"));
                        }
                        else if (!currentEle.GetAttribute("title").Equals(string.Empty) && currentEle.GetAttribute("title") != null)
                        {
                            tocItemlList.Items.Add(currentEle.Text, true);
                            Util.TOCItems.Add(currentEle.Text, currentEle.GetAttribute("href"));
                        }
                        else
                        {
                            break;
                        }
                        try
                        {
                            currentEle = currentEle.FindElement(By.XPath("./following::a"));
                        }
                        catch (NoSuchElementException)
                        {
                            currentEle = null;
                        }
                    }
                    Util.MultipleProtocolURLsImported = false;
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
                Util.MultipleProtocolURLsImported = false;
                tocItemlList.Items.Clear();
                Util.TOCItems.Clear();
                while (sr.Peek() >= 0)
                {
                    string currURL = sr.ReadLine();
                    tocItemlList.Items.Add(currURL, true);
                    if (!Util.TOCItems.ContainsKey(currURL))
                    {
                        Util.TOCItems.Add(currURL, currURL);
                    }
                }
            }
            itemCountText.Text = "Total items: " + tocItemlList.Items.Count.ToString();
        }

        private void testPages_Click(object sender, EventArgs e)
        {
            DateTime oldT = DateTime.Now;
            Util.ClearConfigureFile();
            for (int i= tocItemlList.CheckedItems.Count-1; i>=0;i--)
            {
                Util.ConfigureTestUrls(Util.TOCItems[tocItemlList.CheckedItems[i].ToString()]);
            }
            //foreach (string section in tocItemlList.CheckedItems)
            //{
            //    Util.ConfigureTestUrls(Util.TOCItems[section]);
            //}
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
    }
}
