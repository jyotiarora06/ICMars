using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using static Mars.Utilities.CommonMethods;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Collections.Generic;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Firefox;
using Mars.Config;
using System.IO;

namespace Mars.Utilities
{
    public class Driver
    {
        //Initialize the browser
        public static IWebDriver driver;
        public static ExtentReports extent;
        public static ExtentHtmlReporter hTMLReporter;
        public static ExtentTest test;

        [OneTimeSetUp]
        public void Initialize()
        {
            hTMLReporter = new ExtentHtmlReporter(ConstantHelpers.ReportsPath);
            hTMLReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(hTMLReporter);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "Credentials");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ChangePasswordTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ProfileTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ShareSkillTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ManageListingsTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "SearchTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ChatTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ServiceDetailTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "NotificationTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ManageRequestsTestData");
        }

        public void Setup(string browserName)
        {

            //Defining the browser
            if (browserName.Equals("chrome"))
                driver = new ChromeDriver();
            else if (browserName.Equals("ie"))
                driver = new InternetExplorerDriver();
            else if (browserName.Equals("safari"))
                driver = new SafariDriver();
            else
                driver = new FirefoxDriver();

            //Maximise the window
            driver.Manage().Window.Maximize();

            NavigateUrl();
        }

        public static string BaseUrl
        {
            get { return ConstantHelpers.Url; }
        }


        public static void NavigateUrl()
        {
            driver.Navigate().GoToUrl(BaseUrl);
        }

        public static IEnumerable <string> BrowserToRunWith()
        {
            string[] browsers = AutomationSettings.browsersToRunWith.Split(',');
            foreach (String b in browsers)
            {
                yield return b;
            }

        }

        [TearDown]
        public void CloseBrowser()
        {
            // close the driver
            driver.Close();
            driver.Quit();
        }

        [OneTimeTearDown]
        public void FinalSteps()
        {
            extent.Flush();
            File.Move("/Users/jyotimadan/Documents/Projects/CSharpProject/Mars.Selenium/ExtentReport/index.html", "/Users/jyotimadan/Documents/Projects/CSharpProject/Mars.Selenium/ExtentReport/ExtentReport_" + DateTime.Now.ToString("MMM-dd-yyyy hh-mm-ss") + ".html");
        }


    }
}

