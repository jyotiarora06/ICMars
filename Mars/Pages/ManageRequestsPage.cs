using System;
using System.Threading;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class ManageRequestsPage
    {
        private IWebDriver driver;
        private SignInPage signIn;
        private ServiceDetailPage serviceDetailPageObj;

        //page factory design pattern
        IWebElement ReceivedRequests => driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[1]/div/div[1]/div/a[1]"));
        IWebElement SentRequestsHeading => driver.FindElement(By.XPath("//*[@id='sent-request-section']/div[2]/h2"));
        IWebElement ReceivedRequestsHeading => driver.FindElement(By.XPath("//*[@id='received-request-section']/div[2]/h2"));
        IWebElement ActionButton => driver.FindElement(By.XPath("//*[@id='sent-request-section']/div[2]/div[1]/table/tbody/tr/td[8]/button"));
        IWebElement Accept => driver.FindElement(By.XPath("//*[@id='received-request-section']/div[2]/div[1]/table/tbody/tr[1]/td[8]/button[1]"));
        IWebElement Decline => driver.FindElement(By.XPath("//*[@id='received-request-section']/div[2]/div[1]/table/tbody/tr[1]/td[8]/button[2]"));
        IWebElement SentRequestStatus => driver.FindElement(By.XPath("//*[@id='sent-request-section']/div[2]/div[1]/table/tbody/tr[1]/td[5]"));
        IWebElement ReceivedRequestStatus => driver.FindElement(By.XPath("//*[@id='received-request-section']/div[2]/div[1]/table/tbody/tr[1]/td[5]"));



        //Create a Constructor
        public ManageRequestsPage(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);
            serviceDetailPageObj = new ServiceDetailPage(driver);
        }

        //accepting received service request
        public void AcceptReceivedRequest(string skill)
        {
            serviceDetailPageObj.SendServiceRequest(skill);
            ClickSignOutServiceDetail();
            signIn.Login(ExcelLibHelper.ReadData(1, "Email"), ExcelLibHelper.ReadData(1, "Pwd"));
            ClickManageRequestsProfile();
            ClickReceivedRequests();
            ValidateYouAreAtReceivedRequestsPage();
            ClickAccept();
            bool isStatusAccepted = ValidateReceivedRequestStatus(ExcelLibHelper.ReadData(1, "AcceptReceivedRequest"));
            Assert.IsTrue(isStatusAccepted);
        }

        //declining received service request
        public void DeclineReceivedRequest()
        {
            serviceDetailPageObj.SendServiceRequest(ExcelLibHelper.ReadData(1, "SearchSkillToDecline"));
            ClickSignOutServiceDetail();
            signIn.Login(ExcelLibHelper.ReadData(1, "Email"), ExcelLibHelper.ReadData(1, "Pwd"));
            ClickManageRequestsProfile();
            ClickReceivedRequests();
            ValidateYouAreAtReceivedRequestsPage();
            ClickDecline();
            bool isStatusDeclined = ValidateReceivedRequestStatus(ExcelLibHelper.ReadData(1, "DeclineReceivedRequest"));
            Assert.IsTrue(isStatusDeclined);

        }

        //withdrawing sent service request
        public void WithdrawSentRequest()
        {
            serviceDetailPageObj.SendServiceRequest(ExcelLibHelper.ReadData(1, "SearchSkillToWithdraw"));
            ClickManageRequestsServiceDetail();
            ClickSentRequestsServiceDetail();
            ValidateYouAreAtSentRequestsPage();
            //Click Withdraw
            ClickActionButton();
            bool isStatusWithdrawn = ValidateSentRequestStatus(ExcelLibHelper.ReadData(1, "WithdrawSentRequest"));
            Assert.IsTrue(isStatusWithdrawn);
        }

        //completing sent service request
        public void CompleteSentRequest()
        {

            AcceptReceivedRequest(ExcelLibHelper.ReadData(1, "SearchSkillToComplete"));
            ClickSignOutReceivedRequest();
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            ClickManageRequestsProfile();
            ClickSentRequestsProfile();
            ValidateYouAreAtSentRequestsPage();
            //Click Completed
            ClickActionButton();
            bool isStatusCompleted = ValidateSentRequestStatus(ExcelLibHelper.ReadData(1, "CompleteSentRequest"));
            Assert.IsTrue(isStatusCompleted);
        }

        public void ClickManageRequestsProfile()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='account-profile-section']/div/section[1]/div/div[1]", 100);
            //click manage requests from profile section
            driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[1]/div/div[1]")).Click();
            Thread.Sleep(500);
        }
        public void ClickManageRequestsServiceDetail()
        { 
            Wait.ElementExists(driver, "XPath", "//*[@id='service-detail-section']/section[1]/div/div[1]", 100);
            //click manage requests from service detail section
            driver.FindElement(By.XPath("//*[@id='service-detail-section']/section[1]/div/div[1]")).Click();
            Thread.Sleep(500);
        }

        public void ClickReceivedRequests()
        {
            //click received requests
            ReceivedRequests.Click();
        }

        public void ClickSentRequestsProfile()
        {
            //click sent requestes from profile section  
            driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[1]/div/div[1]/div/a[2]")).Click();

        }
        public void ClickSentRequestsServiceDetail()
        {
            //click sent requestes from service detail section  
            driver.FindElement(By.XPath("//*[@id='service-detail-section']/section[1]/div/div[1]/div/a[2]")).Click();

        }

        public void ClickAccept()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='received-request-section']/div[2]/div[1]/table/tbody/tr[1]/td[8]/button[1]", 100);
            //click accept
            Accept.Click();
        }

        public void ClickDecline()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='received-request-section']/div[2]/div[1]/table/tbody/tr[1]/td[8]/button[2]", 100);
            //click decline
            Decline.Click();
        }

        public void ClickActionButton()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='sent-request-section']/div[2]/div[1]/table/tbody/tr/td[8]/button", 100);
            //Click withdraw or complete actions
            ActionButton.Click();
        }

        public void ClickSignOutReceivedRequest()
        {
            //click sign out from received requests section  
            Wait.ElementExists(driver, "XPath", "//*[@id='received-request-section']/div[1]/div[2]/div/a[2]/button", 100);
            driver.FindElement(By.XPath("//*[@id='received-request-section']/div[1]/div[2]/div/a[2]/button")).Click();
        }

        public void ClickSignOutServiceDetail()
        {
            //click sign out from service detail section  
            Wait.ElementExists(driver, "XPath", "//*[@id='service-detail-section']/div[1]/div[2]/div/a[2]/button", 100);
            driver.FindElement(By.XPath("//*[@id='service-detail-section']/div[1]/div[2]/div/a[2]/button")).Click();


        }

        public void ValidateYouAreAtSentRequestsPage()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='sent-request-section']/div[2]/h2", 500);
            bool isSentRequestsPage = SentRequestsHeading.Displayed;
            Assert.IsTrue(isSentRequestsPage);
        }


        public void ValidateYouAreAtReceivedRequestsPage()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='received-request-section']/div[2]/h2", 500);
            bool isReceivedRequestsPage = ReceivedRequestsHeading.Displayed;
            Assert.IsTrue(isReceivedRequestsPage);
        }

        public bool ValidateReceivedRequestStatus(string status)
        {
            Thread.Sleep(1000);
            Wait.ElementExists(driver, "XPath", "//*[@id='received-request-section']/div[2]/div[1]/table/tbody/tr[1]/td[5]", 100);
           
            if (ReceivedRequestStatus.Text == status)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ValidateSentRequestStatus(string status)
        {
            Thread.Sleep(100);
            Wait.ElementExists(driver, "XPath", "//*[@id='sent-request-section']/div[2]/div[1]/table/tbody/tr[1]/td[5]", 10);

            if (SentRequestStatus.Text == status)
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
