using System;
using Mars.Pages;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars
{
    class ProfileDescription
    {
        private readonly IWebDriver driver;
        private SignInPage signIn;

        //page factory design pattern

        IWebElement DescriptionText => driver.FindElement(By.XPath("//h3[(text()='Description')]"));
        IWebElement DescriptionIcon => driver.FindElement(By.XPath("//h3[text()='Description']//i[@class='outline write icon']"));
        IWebElement DescriptionTextBox => driver.FindElement(By.XPath("//textarea[@name='value']"));
        IWebElement Save => driver.FindElement(By.XPath("//button[text()='Save' and @type='button']"));
        IWebElement SavedDescription => driver.FindElement(By.XPath("//span[contains(text(),'I like playing drums')]"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'Description has been saved successfully')]"));
        
        //Create a Constructor
        public ProfileDescription(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);
        }

        //adding profile description
        public void Description()
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            ValidateProfilePage();
            ClickDescriptionIcon();
            EnterDescription();
            ClickSave();
            bool isMessage = ValidateDescriptionSavedMessage();
            Assert.IsTrue(isMessage);
            bool isDescription = ValidateSavedDescription();
            Assert.IsTrue(isDescription);
        }

        public void ValidateProfilePage()
        {
            Wait.ElementExists(driver, "XPath", "//h3[(text()='Description')]", 20);
            bool isProfilePage = DescriptionText.Displayed;
            Assert.IsTrue(isProfilePage);
        }

        public void ClickDescriptionIcon()
        {
            //click description icon
            DescriptionIcon.Click();
        }

        public void EnterDescription()
        {
            //enter description
            DescriptionTextBox.Clear();
            DescriptionTextBox.SendKeys(ExcelLibHelper.ReadData(1, "ProfileDescription"));
        }

        public void ClickSave()
        {
            //click save
            Save.Click();

        }

        public bool ValidateDescriptionSavedMessage()
        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'Description has been saved successfully')]", 50);

            if (Message.Text == ExcelLibHelper.ReadData(1, "DescriptionMessage"))
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

        public bool ValidateSavedDescription()
        {
            Wait.ElementExists(driver, "XPath", "//span[contains(text(),'I like playing drums')]", 50);

            //validate description is saved
            if (SavedDescription.Text == ExcelLibHelper.ReadData(1, "ProfileDescription"))
            {
                //Console.WriteLine("Description is saved, test passed");
                return true;
            }
            else
            {
                //Console.WriteLine("Description is not saved, test failed");
                return false;
            }
        }
    }
}
