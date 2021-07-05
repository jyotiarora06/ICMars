using System;
using System.Threading;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class ChangePasswordPage
    {
        private readonly IWebDriver driver;
        private SignInPage signIn;

        //page factory design pattern
        IWebElement Username => driver.FindElement(By.XPath("//span[contains(text(),'Hi')]"));
        IWebElement ChangePasswordItem => driver.FindElement(By.XPath("//a[text()='Change Password']"));
        IWebElement CurrentPassword => driver.FindElement(By.XPath("//input[@name='oldPassword']"));
        IWebElement NewPassword => driver.FindElement(By.XPath("//input[@name='newPassword']"));
        IWebElement ConfirmPassword => driver.FindElement(By.XPath("//input[@name='confirmPassword']"));
        IWebElement Save => driver.FindElement(By.XPath("//button[@class='ui button ui teal button' and text()='Save']"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'Password Changed Successfully')]"));

        //Create a Constructor
        public ChangePasswordPage(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);
        }

        //changing password
        public void ChangePassword()
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "Email"), ExcelLibHelper.ReadData(1, "Pwd"));
            ClickChangePassword();
            ValidateYouAreAtChangePasswordPage();
            EnterData();
            ClickSave();
            bool isPasswordChanged = ValidateSuccessMessage();
            Assert.IsTrue(isPasswordChanged);

        }

       
        public void ClickChangePassword()
        {
            Wait.ElementExists(driver, "XPath", "//span[contains(text(),'Hi')]", 50);

            //click Hi username
            Username.Click();

            Thread.Sleep(200);

            Wait.ElementExists(driver, "XPath", "//a[text()='Change Password']", 50);
           
            //click Change Password menu item
            ChangePasswordItem.Click();

        }

        public void ValidateYouAreAtChangePasswordPage()
        {
            Wait.ElementExists(driver, "XPath", "//button[@class='ui button ui teal button' and text()='Save']", 100);
            bool isChangePasswordPage = Save.Displayed;
            Assert.IsTrue(isChangePasswordPage);
           
        }

        public void EnterData()
        {
            try
            {
                Wait.ElementExists(driver, "XPath", "//input[@name='oldPassword']", 50);
                //Enter current password
                CurrentPassword.SendKeys(ExcelLibHelper.ReadData(1, "CurrentPassword"));

                //Enter new password
                NewPassword.SendKeys(ExcelLibHelper.ReadData(1, "NewPassword"));

                //Enter confirm password
                ConfirmPassword.SendKeys(ExcelLibHelper.ReadData(1, "NewConfirmPassword"));

            }
            catch (Exception msg)
            {
                Assert.Fail("Test failed at SignIn page", msg.Message);
            }

        }

        public void ClickSave()
        {
            //Click save button
            Wait.ElementExists(driver, "XPath", "//button[contains(text(),'Save')]", 50);

            Save.Click();

        }

        public bool ValidateSuccessMessage()
        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'Password Changed Successfully')]", 100);
            
            //validate password changed message is displayed
            if (Message.Text == ExcelLibHelper.ReadData(1, "PasswordChangeMessage"))
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

        
    }
}
