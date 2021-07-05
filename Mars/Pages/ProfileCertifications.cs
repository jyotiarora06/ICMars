using System;
using Mars.Pages;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars
{
    class ProfileCertifications
    {
        private readonly IWebDriver driver;
        private SignInPage signIn;
        private ProfileDescription profileDescription;

        //page factory design pattern

        IWebElement AddNew => driver.FindElement(By.XPath("//th[4]//div[@class='ui teal button ' and contains(text(),'Add New')]"));                                                                                    
        IWebElement Certificate => driver.FindElement(By.XPath("//input[@name='certificationName']"));
        IWebElement CertifiedFrom => driver.FindElement(By.XPath("//input[@name='certificationFrom']"));
        IWebElement Year => driver.FindElement(By.XPath("//select[@name='certificationYear']//option[@value='2016']"));
        IWebElement Add => driver.FindElement(By.XPath("//div[@class='five wide field']//input[@class='ui teal button ' and @value='Add']"));
        IWebElement AddedCertificate => driver.FindElement(By.XPath("//td[text()='abc']"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'abc has been added to your certification')]"));
        IWebElement CertificationsTab => driver.FindElement(By.XPath("//a[text()='Certifications']"));


        //Create a Constructor
        public ProfileCertifications(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);
            profileDescription = new ProfileDescription(driver);
        }

        //adding a certification
        public void Certification()
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            profileDescription.ValidateProfilePage();
            ClickCertificationsTab();
            ClickAddNew();
            EnterCertificate();
            EnterCertifiedFrom();
            SelectYear();
            ClickAdd();
            bool isMessage = ValidateCertificateSavedMessage();
            Assert.IsTrue(isMessage);
            bool isCertificate = ValidateAddedCertificate();
            Assert.IsTrue(isCertificate);
        }

        public void ClickCertificationsTab()
        {
            //click Certifications Tab
            CertificationsTab.Click();
        }

        public void ClickAddNew()
        {
            //click add new for Certifications
            AddNew.Click();
        }

        public void EnterCertificate()
        {
            // enter Certificate
            Certificate.SendKeys(ExcelLibHelper.ReadData(1, "Certificate"));
        }

        public void EnterCertifiedFrom()
        {
            // enter Certified From
            CertifiedFrom.SendKeys(ExcelLibHelper.ReadData(1, "CertifiedFrom"));
        }


        public void SelectYear()
        {
            //select year
            Year.Click();
        }

        public void ClickAdd()
        {
            //click add for Certifications
            Add.Click();
        }

        public bool ValidateCertificateSavedMessage()
        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'abc has been added to your certification')]", 50);

            if (Message.Text == ExcelLibHelper.ReadData(1, "CertificateMessage"))
            {
                //Console.WriteLine("Success message is displayed, test passed");
                return true;
            }
            else
            {
                //Console.WriteLine("Success message is not displayed, test failed");
                return false;
            }
        }

        public bool ValidateAddedCertificate()
        {
            Wait.ElementExists(driver, "XPath", "//td[text()='abc']", 30);

            //validate Certificate is added
            if (AddedCertificate.Text == ExcelLibHelper.ReadData(1, "Certificate"))
            {
                //Console.WriteLine("Certificate is added, test passed");
                return true;
            }
            else
            {
                //Console.WriteLine("Certificate is not added, test failed");
                return false;
            }
        }

    }
}
