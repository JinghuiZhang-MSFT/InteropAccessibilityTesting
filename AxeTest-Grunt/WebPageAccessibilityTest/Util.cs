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
        public static Dictionary<string, string> TOCItems;
        public static bool MultipleProtocolURLsImported = true;
        const string gruntFilePath= "./AxeScripts/Gruntfile.js";

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

        public static void Initialize()
        {
            TOCItems = new Dictionary<string, string>();
        }

        internal static void Click(IWebElement element)
        {
            (webDriver as IJavaScriptExecutor).ExecuteScript("arguments[0].click();", element);
        }

       internal static void ConfigureTestUrls(string urlToTest)
        {
           string fileContent = File.ReadAllText(gruntFilePath);
            int index = fileContent.IndexOf("urls:[");
            string urlToInsert = "'"+urlToTest+"'";
            if (fileContent.Substring(index+6).StartsWith("'"))
            {
                urlToInsert += ",";
            }
            fileContent = fileContent.Insert(index+6,urlToInsert);
            File.WriteAllText(gruntFilePath, fileContent);
        }

        internal static void ClearConfigureFile()
        {
            string fileContent = File.ReadAllText(gruntFilePath);
            int index = fileContent.IndexOf("urls:[");
            
            if (fileContent.Substring(index + 6).StartsWith("'http"))
            {
                int endIndex = fileContent.IndexOf("[");
                string strToRemove = fileContent.Substring(index + 6,endIndex-index);
                fileContent = fileContent.Replace(strToRemove,"");
                File.WriteAllText(gruntFilePath, fileContent);
            }
        }

        internal static bool IsNodeJSInstalled()
        {
            RegistryKey localMachineKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
            RegistryKey nodeJSKey = localMachineKey.OpenSubKey(@"SOFTWARE\Node.js");
            bool installed= nodeJSKey != null;
            nodeJSKey.Close();
            localMachineKey.Close();
            return installed;
        }
    }
}
