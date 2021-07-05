using System;
using Mars.Pages;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars
{
    class ProfileLanguage
    {
        private readonly IWebDriver driver;
        private SignInPage signIn;
        private ProfileDescription profileDescription;

        //page factory design pattern

        IWebElement AddNew => driver.FindElement(By.XPath("//th[3]//div[@class='ui teal button ' and contains(text(),'Add New')]"));
        IWebElement Language => driver.FindElement(By.XPath("//input[@name='name' and @placeholder='Add Language']"));
        IWebElement LanguageLevel => driver.FindElement(By.XPath("//option[@value='Basic']"));
        IWebElement Add => driver.FindElement(By.XPath("//input[@class='ui teal button' and @value='Add']"));
        IWebElement AddedLanguage => driver.FindElement(By.XPath("//td[contains(text(),'English')]"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'English has been added to your languages')]"));
        

        //Create a Constructor
        public ProfileLanguage(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);
            profileDescription = new ProfileDescription(driver);
        }

        //adding language
        public void Languages()
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            profileDescription.ValidateProfilePage();
            ClickAddNew();
            EnterLanguage();
            ChooseLanguageLevel();
            ClickAdd();
            bool isMessage = ValidateLanguageSavedMessage();
            Assert.IsTrue(isMessage);
            bool isLanguage = ValidateAddedLanguage();
            Assert.IsTrue(isLanguage);
        }

        /*
        public void DeleteLanguage()
        {
            //click delete icon
            RemoveLanguage.Click();

        }
        */

        public void ClickAddNew()
        {
            //click add new for language
            AddNew.Click();
        }

        public void EnterLanguage()
        {
            // enter language
            Language.SendKeys(ExcelLibHelper.ReadData(1, "Language"));
        }

        public void ChooseLanguageLevel()
        {
           //choose language lavel
            LanguageLevel.Click();
        }

        public void ClickAdd()
        {
            //click add for language
            Add.Click();
        }

        public bool ValidateLanguageSavedMessage()
        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'English has been added to your languages')]", 100);

            if (Message.Text == ExcelLibHelper.ReadData(1, "LanguageMessage"))
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

        public bool ValidateAddedLanguage()
        {
            Wait.ElementExists(driver, "XPath", "//td[contains(text(),'English')]", 30);

            //validate language is added
            if (AddedLanguage.Text == ExcelLibHelper.ReadData(1, "Language"))
            {   
                //Console.WriteLine("Language is added, test passed");
                return true;
            }
            else
            {
                //Console.WriteLine("Language is not added, test failed");
                return false;
            }
        }

    }
}
