using System;
using Mars.Pages;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars
{
    class ProfileSkill
    {
        private readonly IWebDriver driver;
        private SignInPage signIn;
        private ProfileDescription profileDescription;

        //page factory design pattern
        IWebElement AddNew => driver.FindElement(By.XPath("//th[3]//div[@class='ui teal button' and contains(text(),'Add New')]"));
        IWebElement Add => driver.FindElement(By.XPath("//span[@class='buttons-wrapper']//input[@class='ui teal button ' and @value='Add']"));
        IWebElement SkillsTab => driver.FindElement(By.XPath("//a[text()='Skills']"));
        IWebElement Skill => driver.FindElement(By.XPath("//input[@name='name' and @placeholder='Add Skill']"));
        IWebElement SkillLevel => driver.FindElement(By.XPath("//option[@value='Beginner']"));
        IWebElement AddedSkill => driver.FindElement(By.XPath("//td[contains(text(),'Drums')]"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'Drums has been added to your skills')]"));
        
        //Create a Constructor
        public ProfileSkill(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);
            profileDescription = new ProfileDescription(driver);
        }

        // adding a skill
        public void Skills()
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            profileDescription.ValidateProfilePage();
            ClickSkillsTab();
            ClickAddNew();
            EnterSkill();
            ChooseSkillLevel();
            ClickAdd();
            bool isMessage = ValidateSkillSavedMessage();
            Assert.IsTrue(isMessage);
            bool isSkill = ValidateAddedSkill();
            Assert.IsTrue(isSkill);
        }

        /*
        public void DeleteSkill()
        {
            //click delete icon
            RemoveSkill.Click();

        }
        */

        public void ClickAddNew()
        {
            //click add new for skill
            AddNew.Click();
        }

        public void ClickAdd()
        {
            //click add for skill
            Add.Click();
        }

        public void ClickSkillsTab()
        {
            //click skills tab
            SkillsTab.Click();
            //Wait.ElementExists(driver, "XPath", "//*[@id='account-profile-section']/div/section[2]/div/div/div/div[3]/form/div[3]/div/div[2]/div/table/thead/tr/th[3]/div", 10);
        }

        public void EnterSkill()
        {
            //enter skill
            Skill.SendKeys(ExcelLibHelper.ReadData(1, "Skill"));
        }

        public void ChooseSkillLevel()
        {
            //choose skill level
            SkillLevel.Click();

        }

        public bool ValidateSkillSavedMessage()
        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'Drums has been added to your skills')]", 40);

            if (Message.Text == ExcelLibHelper.ReadData(1, "SkillMessage"))
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

        public bool ValidateAddedSkill()
        {
            Wait.ElementExists(driver, "XPath", "//td[contains(text(),'Drums')]", 30);

            //validate skill is added
            if (AddedSkill.Text == ExcelLibHelper.ReadData(1, "Skill"))
            {
                //Console.WriteLine("Skill is added, test passed");
                return true;
            }
            else
            {
                //Console.WriteLine("Skill is not added, test failed");
                return false;
            }
        }
    }
}
