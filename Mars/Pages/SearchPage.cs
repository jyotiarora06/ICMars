using System;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class SearchPage
    {
        private IWebDriver driver;
        private SignInPage signIn;


        //page factory design pattern
        IWebElement SearchIcon => driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/div[1]/div[1]/i"));
        IWebElement SearchSkillsBox => driver.FindElement(By.XPath("//*[@id='service-search-section']/div[2]/div/section/div/div[1]/div[2]/input"));
        IWebElement SearchedSkill => driver.FindElement(By.XPath("//*[@id='service-search-section']/div[2]/div/section/div/div[2]/div/div[2]/div/div/div/div[1]/a[2]/p"));
        IWebElement Online => driver.FindElement(By.XPath("//*[@id='service-search-section']/div[2]/div/section/div/div[1]/div[5]/button[1]"));

        //Create a Constructor
        public SearchPage(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);
        }

        // searching a skill from all categories
        public void SearchSkillsByAllCategories(string searchSkill)
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            ClickSearchIcon();
            EnterSearchSkill(searchSkill);
            ClickEnter();
            bool isSearchResult = ValidateSearchResult(searchSkill);
            Assert.IsTrue(isSearchResult);
        }

        //searching a skill using filter
        public void SearchSkillsByFilters()
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            ClickSearchIcon();
            ClickOnline();
            EnterSearchSkill(ExcelLibHelper.ReadData(1, "SearchSkillToAccept"));
            ClickEnter();
            bool isSearchResult = ValidateSearchResult(ExcelLibHelper.ReadData(1, "SearchSkillToAccept"));
            Assert.IsTrue(isSearchResult);
        }
        public void ClickSearchIcon()
        {
            //click search icon
            SearchIcon.Click();
        }

        public void EnterSearchSkill(string skill)
        {
            //enter skill to search
            SearchSkillsBox.SendKeys(skill);
        }

        public void ClickEnter()
        {
            //click enter button
            SearchSkillsBox.SendKeys(Keys.Enter);
        }

        public void ClickOnline()
        {
            //click online filter
            Online.Click();
        }

        public void ClickSearchedSkill()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='service-search-section']/div[2]/div/section/div/div[2]/div/div[2]/div/div/div/div[1]/a[2]/p", 50);

            //Click search result
            SearchedSkill.Click();
        }


        public bool ValidateSearchResult(string searchSkill)
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='service-search-section']/div[2]/div/section/div/div[2]/div/div[2]/div/div/div/div[1]/a[2]/p", 100);

            //validate search result
            if (SearchedSkill.Text == searchSkill)
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
