using System;
using Mars.Pages;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars
{
    class ProfileEducation
    {
        private readonly IWebDriver driver;
        private SignInPage signIn;
        private ProfileDescription profileDescription;

        //page factory design pattern

        IWebElement AddNew => driver.FindElement(By.XPath("//th[6]//div[@class='ui teal button ' and contains(text(),'Add New')]"));
        IWebElement College => driver.FindElement(By.XPath("//input[@name='instituteName']"));
        IWebElement Country => driver.FindElement(By.XPath("//option[@value='Australia']"));
        IWebElement Title => driver.FindElement(By.XPath("//option[@value='M.Tech']"));
        IWebElement Degree => driver.FindElement(By.XPath("//input[@name='degree']"));
        IWebElement Year => driver.FindElement(By.XPath("//select[@name='yearOfGraduation']//option[@value='2015']"));
        IWebElement Add => driver.FindElement(By.XPath("//div[@class='sixteen wide field']//input[@class='ui teal button ' and @value='Add']"));
        IWebElement AddedDegree => driver.FindElement(By.XPath("//td[contains(text(),'Computers')]"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'Education has been added')]"));
        IWebElement EducationTab => driver.FindElement(By.XPath("//a[text()='Education']"));


        //Create a Constructor
        public ProfileEducation(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);
            profileDescription = new ProfileDescription(driver);
        }

        //adding education details
        public void Education()
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            profileDescription.ValidateProfilePage();
            ClickEducationTab();
            ClickAddNew();
            EnterCollege();
            SelectCountry();
            SelectTitle();
            EnterDegree();
            SelectYear();
            ClickAdd();
            bool isMessage = ValidateEducationSavedMessage();
            Assert.IsTrue(isMessage);
            bool isEducation = ValidateAddedEducation();
            Assert.IsTrue(isEducation);
        }

        public void ClickEducationTab()
        {
            //click Education Tab
            EducationTab.Click();
        }

        public void ClickAddNew()
        {
            //click add new 
            AddNew.Click();
        }

        public void EnterCollege()
        {
            // enter College
            College.SendKeys(ExcelLibHelper.ReadData(1, "College"));
        }

        public void EnterDegree()
        {
            // enter Degree
            Degree.SendKeys(ExcelLibHelper.ReadData(1, "Degree"));
        }

        public void SelectCountry()
        {
            //select country
            Country.Click();
        }

        public void SelectTitle()
        {
            //select title
            Title.Click();
        }

        public void SelectYear()
        {
            //select year
            Year.Click();
        }

        public void ClickAdd()
        {
            //click add 
            Add.Click();
        }

        public bool ValidateEducationSavedMessage()
        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'Education has been added')]", 30);

            if (Message.Text == ExcelLibHelper.ReadData(1, "EducationMessage"))
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

        public bool ValidateAddedEducation()
        {
            Wait.ElementExists(driver, "XPath", "//td[contains(text(),'Computers')]", 30);

            //validate Education is added
            if (AddedDegree.Text == ExcelLibHelper.ReadData(1, "Degree"))
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
