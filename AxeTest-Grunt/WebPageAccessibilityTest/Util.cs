using Microsoft.Win32;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
//using OpenQA.Selenium.Edge;
using System.Net.Http;

namespace TDWebPageListGenerator
{
    public class Util
    {
        public static IWebDriver webDriver;
        public static string EntryUrl = string.Empty;
        const string gruntFilePath = "./AxeScripts/Gruntfile.js";

        public static IWebElement FindElement(By by)
        {
            try
            {
                return webDriver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public static IWebElement FindElement(IWebElement ele, By by)
        {
            try
            {
                return ele.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        public static void Initialize()
        {
        }

        internal static void Click(IWebElement element)
        {
            (webDriver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", element);
        }

        internal static void ConfigureTestUrls(string urlToTest)
        {
            string fileContent = File.ReadAllText(gruntFilePath);
            int index = fileContent.IndexOf("urls:[");
            string urlToInsert = "'" + urlToTest + "'";
            if (fileContent.Substring(index + 6).StartsWith("'"))
            {
                urlToInsert += ",";
            }
            fileContent = fileContent.Insert(index + 6, urlToInsert);
            File.WriteAllText(gruntFilePath, fileContent);
        }

        internal static void ClearConfigureFile()
        {
            string fileContent = File.ReadAllText(gruntFilePath);
            int index = fileContent.IndexOf("urls:[");

            if (fileContent.Substring(index + 6).StartsWith("'http"))
            {
                string tempStr = fileContent.Substring(index + 6);
                int endIndex = tempStr.IndexOf("]");
                string strToRemove = tempStr.Substring(0, endIndex);
                fileContent = fileContent.Replace(strToRemove, "");
                File.WriteAllText(gruntFilePath, fileContent);
            }
        }

        internal static bool IsNodeJSInstalled()
        {
            RegistryKey localMachineKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
            RegistryKey nodeJSKey = localMachineKey.OpenSubKey(@"SOFTWARE\Node.js");
            bool installed = nodeJSKey != null;
            nodeJSKey.Close();
            localMachineKey.Close();
            return installed;
        }

        internal static void SetTestRules(List<string> ruleList)
        {
            string fileContent = File.ReadAllText("./AxeScripts/build/tasks/axe-selenium.js");
            int insertIndex = fileContent.IndexOf("withRules([") + 11;
            //Clear old rules
            int oldRulesLength = fileContent.Substring(insertIndex).IndexOf("])");
            string rulesString = fileContent.Substring(insertIndex, oldRulesLength);
            if (!rulesString.Equals(string.Empty))
            {
                fileContent = fileContent.Replace(rulesString, "");
            }
            int i = 0;
            foreach (string rule in ruleList)
            {
                fileContent = fileContent.Insert(insertIndex, "'" + rule + "'");
                if (++i < ruleList.Count)
                {
                    fileContent = fileContent.Insert(insertIndex, ", ");
                }
            }
            File.WriteAllText("./AxeScripts/build/tasks/axe-selenium.js", fileContent);
        }
    }
}
