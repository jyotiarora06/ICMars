using System;
using System.Threading;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class ServiceDetailPage
    {
        private readonly IWebDriver driver;
        private SearchPage searchPageObj;

        //page factory design pattern
        IWebElement ChatButton => driver.FindElement(By.XPath("//a[@class='ui teal button' and contains(text(),'Chat')]"));
        IWebElement Request => driver.FindElement(By.XPath("//div[@class='ui teal  button' and contains(text(),'Request')]"));
        IWebElement MessageTextBox => driver.FindElement(By.XPath("//textarea[@ placeholder ='I am interested in trading my cooking skills with your coding skills..']"));
        IWebElement Yes => driver.FindElement(By.XPath("//button[contains(text(),'Yes')]"));
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'Request sent')]"));

        //Create a Constructor
        public ServiceDetailPage(IWebDriver driver)
        {
            this.driver = driver;
            searchPageObj = new SearchPage(driver);
        }

        public void ValidateYouAreAtServiceDetailPage()
        {
            Wait.ElementExists(driver, "XPath", "//a[@class='ui teal button' and contains(text(),'Chat')]", 100);
            bool isServicePage = ChatButton.Displayed;
            Assert.IsTrue(isServicePage);
        }

        public void ClickChat()
        {
            //click chat button
            ChatButton.Click();
        }

        public void ClickRequest()
        {
            //click request button
            Request.Click();
        }

        public void EnterMessageToSeller()
        {
            //enter message in message text box
            MessageTextBox.SendKeys(ExcelLibHelper.ReadData(1, "MessageToSeller"));

        }

        public void ClickYes()
        {
            //click yes on confirm popup
            Yes.Click();
        }

        public bool ValidateRequestSent()
        {
            Thread.Sleep(100);
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'Request sent')]", 10);
            //validate request is sent
            if (Message.Text == ExcelLibHelper.ReadData(1, "SentRequestMessage"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void SendServiceRequest(string skill)
        {
            searchPageObj.SearchSkillsByAllCategories(skill);
            searchPageObj.ClickSearchedSkill();
            ValidateYouAreAtServiceDetailPage();
            EnterMessageToSeller();
            ClickRequest();
            ClickYes();
            bool isRequestSent = ValidateRequestSent();
            Assert.IsTrue(isRequestSent);

        }
    }
}
