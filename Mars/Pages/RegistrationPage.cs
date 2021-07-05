using System;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class RegistrationPage
    {
        private readonly IWebDriver driver;
        

        //page factory design pattern
        IWebElement Join => driver.FindElement(By.XPath("//button[contains(text(),'Join')]"));
        IWebElement FirstName => driver.FindElement(By.XPath("//input[@name='firstName']"));
        IWebElement LastName => driver.FindElement(By.XPath("//input[@name='lastName']"));
        IWebElement EmailAddress => driver.FindElement(By.XPath("//input[@name='email']"));
        IWebElement Password => driver.FindElement(By.XPath("//input[@name='password']"));
        IWebElement ConfirmPassword => driver.FindElement(By.XPath("//input[@name='confirmPassword']"));
        IWebElement AgreeTerms => driver.FindElement(By.XPath("//input[@name='terms']"));
        IWebElement JoinButton => driver.FindElement(By.XPath("//div[@id='submit-btn']"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'Registration successful')]"));



        //Create a Constructor
        public RegistrationPage(IWebDriver driver)
        {
            this.driver = driver;
          
        }


        public void ClickJoin()
        {
            Wait.ElementExists(driver, "XPath", "//button[contains(text(),'Join')]", 10);
            //click join
            Join.Click();

        }

        public void ValidateYouAreAtRegistrationPage()
        {
            bool isJoinPage = JoinButton.Displayed;
            Assert.IsTrue(isJoinPage);
        }

        public void EnterData()
        {
            try
            {
                //Enter first name 
                FirstName.SendKeys(ExcelLibHelper.ReadData(1, "FirstName"));

                //Enter last name
                LastName.SendKeys(ExcelLibHelper.ReadData(1, "LastName"));

                //Enter email address
                EmailAddress.SendKeys(ExcelLibHelper.ReadData(1, "EmailAddress"));

                //Enter password
                Password.SendKeys(ExcelLibHelper.ReadData(1, "Password"));

                //Enter confirm password
                ConfirmPassword.SendKeys(ExcelLibHelper.ReadData(1, "ConfirmPassword"));

            }
            catch (Exception msg)
            {
                Assert.Fail("Test failed at Join page", msg.Message);
            }

        }
        public void ClickAgreeTermsAndConditions()
        {
            //Click on AgreeTermsAndConditions
            AgreeTerms.Click();

        }

        public void ClickJoinButton()
        {
            //Click Join button
            JoinButton.Click();

        }
        public bool ValidateSuccessMessage()
        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'Registration successful')]", 100);
            // validate registration message
            if (Message.Text == ExcelLibHelper.ReadData(1, "RegistrationMessage"))
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

        // registering a user
        public void Registration()
        {
            ClickJoin();
            ValidateYouAreAtRegistrationPage();
            EnterData();
            ClickAgreeTermsAndConditions();
            ClickJoinButton();
            bool isRegistered = ValidateSuccessMessage();
            Assert.IsTrue(isRegistered);
        }
    }
}
