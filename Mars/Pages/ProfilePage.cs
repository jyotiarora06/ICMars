using System;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class ProfilePage
    {
        private readonly IWebDriver driver;
        private SignInPage signInPageObj;
        private ProfileDescription profileDescription;

        //page factory design pattern

        IWebElement AvailabilityIcon => driver.FindElement(By.XPath("//strong[text()='Availability']//parent::span//following-sibling::div//i[@class='right floated outline small write icon']"));                                                                                          
        IWebElement PartTime => driver.FindElement(By.XPath("//select[@name='availabiltyType']//option[@value='0']"));
        IWebElement FullTime => driver.FindElement(By.XPath("//select[@name='availabiltyType']//option[@value='1']"));
        IWebElement HoursIcon => driver.FindElement(By.XPath("//strong[text()='Hours']//parent::span//following-sibling::div//i[@class='right floated outline small write icon']"));
        IWebElement LessThan30Hours => driver.FindElement(By.XPath("//select[@name='availabiltyHour']//option[@value='0']"));
        IWebElement MoreThan30Hours => driver.FindElement(By.XPath("//select[@name='availabiltyHour']//option[@value='1']"));
        IWebElement AsNeeded => driver.FindElement(By.XPath("//select[@name='availabiltyHour']//option[@value='2']"));
        IWebElement EarnTargetIcon => driver.FindElement(By.XPath("//strong[text()='Earn Target']//parent::span//following-sibling::div//i[@class='right floated outline small write icon']"));
        IWebElement LessThan500 => driver.FindElement(By.XPath("//select[@name='availabiltyTarget']//option[@value='0']"));
        IWebElement Between500And1000 => driver.FindElement(By.XPath("//select[@name='availabiltyTarget']//option[@value='1']"));
        IWebElement MoreThan1000 => driver.FindElement(By.XPath("//select[@name='availabiltyTarget']//option[@value='2']"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'Availability updated')]"));

        //Create a Constructor
        public ProfilePage(IWebDriver driver)
        {
            this.driver = driver;
            signInPageObj = new SignInPage(driver);
            profileDescription = new ProfileDescription(driver);
        }

        //selecting availability
        public void Availability()
        {
            signInPageObj.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            profileDescription.ValidateProfilePage();
            ClickAvailability();
            SelectAvailability();
            bool isAvailability = ValidateSuccessMessage();
            Assert.IsTrue(isAvailability);
        }

        //selecting hours
        public void Hours()
        {
            signInPageObj.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            profileDescription.ValidateProfilePage();
            ClickHours();
            SelectHours(ExcelLibHelper.ReadData(1, "Hours"));
            bool isHours = ValidateSuccessMessage();
            Assert.IsTrue(isHours);
        }

        //selecting earn target
        public void EarnTarget()
        {
            signInPageObj.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            profileDescription.ValidateProfilePage();
            ClickEarnTarget();
            SelectEarnTarget(ExcelLibHelper.ReadData(1, "EarnTarget"));
            bool isEarnTarget = ValidateSuccessMessage();
            Assert.IsTrue(isEarnTarget);
        }

        public void ClickAvailability()
        {
            //click availability icon
            AvailabilityIcon.Click();
        }

        public void SelectAvailability()
        {
            if (ExcelLibHelper.ReadData(1, "Availability") == "Part Time")
            {
                PartTime.Click();
            }
            else
            {
                FullTime.Click();
            }
        }

        public void ClickHours()
        {
            // click hours icon
            HoursIcon.Click();
        }

        public void SelectHours(string hours)
        {
            switch (hours)
            {
                case "Less than 30hours a week":

                    LessThan30Hours.Click();
                    break;

                case "More than 30hours a week":

                    MoreThan30Hours.Click();
                    break;

                default:

                    AsNeeded.Click();
                    break;
            }

        }

        public void ClickEarnTarget()
        {
            //click earn target
            EarnTargetIcon.Click();
        }

        public void SelectEarnTarget(string earnTarget)
        {
            switch (earnTarget)
            {
                case "Less than $500 per month":

                    LessThan500.Click();
                    break;

                case "Between $500 and $1000 per month":

                    Between500And1000.Click();
                    break;

                default:

                    MoreThan1000.Click();
                    break;
            }
        }

        public bool ValidateSuccessMessage()

        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'Availability updated')]", 10);
            //validate updation message
            if (Message.Text == ExcelLibHelper.ReadData(1, "AvailabilityMessage"))
            {
                
                return true;
            }
            else
            {
                
                return false;
            }

        }

    }
}

