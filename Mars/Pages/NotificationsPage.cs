using System;
using System.Threading;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class NotificationsPage
    {
        private IWebDriver driver;
        private SignInPage SignIn;


        //page factory design pattern
        IWebElement Message => driver.FindElement(By.XPath("//div[contains(text(),'Notification updated')]"));
        IWebElement Dashboard => driver.FindElement(By.XPath("//a[contains(text(),'Dashboard')]"));
        IWebElement NotificationsText => driver.FindElement(By.XPath("//h1[contains(text(),'Notifications')]"));
        IWebElement LoadMore => driver.FindElement(By.XPath("//a[contains(text(),'Load More...')]"));
        IWebElement ShowLess => driver.FindElement(By.XPath("//a[contains(text(),'...Show Less')]"));
        IWebElement NotificationCheckBox => driver.FindElement(By.XPath("//input[@type='checkbox' and @value='0']"));
        IWebElement SelectAll => driver.FindElement(By.XPath("//div[@data-tooltip='Select all']"));
        IWebElement UnselectAll => driver.FindElement(By.XPath("//div[@data-tooltip='Unselect all']"));
        IWebElement DeleteSelection => driver.FindElement(By.XPath("//div[@data-tooltip='Delete selection']"));
        IWebElement MarkSelectionAsRead => driver.FindElement(By.XPath("//div[@data-tooltip='Mark selection as read']"));

        
            

        //Create a Constructor
        public NotificationsPage(IWebDriver driver)
        {
            this.driver = driver;
            SignIn = new SignInPage(driver);
        }

        // performing actions on notifications
        public void Notifications()
        {
            SignIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            ClickDashboard();
            ValidateYouAreAtNotificationPage();
            ClickLoadMore();
            ClickShowLess();
            SelectNotification();
            UnselectNotification();
            ClickSelectAll();
            ClickUnselectAll();
            ClickMarkSelectionAsRead();
            bool isNotificationMarked = ValidateNotificationMarked();
            Assert.IsTrue(isNotificationMarked);
            ClickDeleteSelection();
            bool isNotificationDeleted = ValidateMessage(ExcelLibHelper.ReadData(1, "NotificationMessage"));
            Assert.IsTrue(isNotificationDeleted);

        }

        public void ClickDashboard()
        {
            Wait.ElementExists(driver, "XPath", "//a[contains(text(),'Dashboard')]", 10);
            //click dashboard
            Dashboard.Click();
        }

        public void ClickLoadMore()
        {
            Wait.ElementExists(driver, "XPath", "//a[contains(text(),'Load More...')]", 30);

            //load more notifications
            LoadMore.Click();
        }

        public void ClickShowLess()
        {
            Wait.ElementExists(driver, "XPath", "//a[contains(text(),'...Show Less')]", 20);
            //show less notifications
            ShowLess.Click();
        }

        //selecting a notification
        public void SelectNotification()
        {
            if (!NotificationCheckBox.Selected)
            {
                NotificationCheckBox.Click();
            }
           
        }

        //unselecting a notification
        public void UnselectNotification()
        {
            if (NotificationCheckBox.Selected)
            {
                NotificationCheckBox.Click();
            }
        }

        //selecting all notifications
        public void ClickSelectAll()
        {
            SelectAll.Click();
        }

        //unselecting all notifications
        public void ClickUnselectAll()
        {
            UnselectAll.Click();
        }

        //mark selected notification as read
        public void ClickMarkSelectionAsRead()
        {
            Wait.ElementExists(driver, "XPath", "//input[@type='checkbox' and @value='0']//parent::div//preceding-sibling::div[@class='fourteen wide column']", 20);
                                              
            string fontWeight = driver.FindElement(By.XPath("//input[@type='checkbox' and @value='0']//parent::div//preceding-sibling::div[@class='fourteen wide column']")).GetCssValue("font-weight");
            Assert.AreEqual("700", fontWeight);

            SelectNotification();
            MarkSelectionAsRead.Click();
            Thread.Sleep(200);

        }

        //deleting selected notification
        public void ClickDeleteSelection()
        {
            SelectNotification();
            DeleteSelection.Click();
        }

        public void ValidateYouAreAtNotificationPage()
        {
            Wait.ElementExists(driver, "XPath", "//h1[contains(text(),'Notifications')]", 20);
            bool isNotificationPage = NotificationsText.Displayed;
            Assert.IsTrue(isNotificationPage);
        }

        public bool ValidateNotificationMarked()
        {
            Wait.ElementExists(driver, "XPath", "//a[contains(text(),'Load More...')]", 50);

            string fontWeight = driver.FindElement(By.XPath("//input[@type='checkbox' and @value='0']//parent::div//preceding-sibling::div[@class='fourteen wide column']")).GetCssValue("font-weight");

            //validate notification is marked
            if (fontWeight == "400")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool ValidateMessage(string message)
        {
            Wait.ElementExists(driver, "XPath", "//div[contains(text(),'Notification updated')]", 200);
            //validate notification is updated
            if (Message.Text == message)
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
