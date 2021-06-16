﻿using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Text;
using static Mars.Utilities.CommonMethods;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

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

            //Defining the browser
            driver = new ChromeDriver();
           
            //Maximise the window
            driver.Manage().Window.Maximize();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
          
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "Credentials");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ProfileTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ShareSkillTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ManageListingsTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "SearchTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ChatTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ServiceDetailTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "NotificationTestData");
            ExcelLibHelper.PopulateInCollection(ConstantHelpers.DataFilePath, "ManageRequestsTestData");
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

        
        [OneTimeTearDown]
        public void FinalSteps()
        {
            // close the driver
            driver.Close();
            driver.Quit();
            extent.Flush();
        }


    }
}

