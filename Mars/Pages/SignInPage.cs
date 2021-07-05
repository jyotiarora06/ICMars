using System;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class SignInPage
    {
        private readonly IWebDriver driver;

        //page factory design pattern
        IWebElement SignIn => driver.FindElement(By.XPath("//a[contains(text(),'Sign In')]"));
        IWebElement EmailAddress => driver.FindElement(By.XPath("//input[@name='email']"));
        IWebElement Password => driver.FindElement(By.XPath("//input[@name='password']"));
        IWebElement LoginButton => driver.FindElement(By.XPath("//button[contains(text(),'Login')]"));
        IWebElement SignOut => driver.FindElement(By.XPath("//button[contains(text(),'Sign Out')]"));

        //Create a Constructor
        public SignInPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        public void ClickSignIn()
        {
            Wait.ElementExists(driver, "XPath", "//a[contains(text(),'Sign In')]", 50);
            //click sign in 
            SignIn.Click();

        }

        public void ValidateYouAreAtLoginPage()
        {
            Wait.ElementExists(driver, "XPath", "//button[contains(text(),'Login')]", 50);
            bool isLoginPage= LoginButton.Displayed;
            Assert.IsTrue(isLoginPage);
        }

        public void EnterEmailAddressAndPassword(string emailAddressValue, string PasswordValue)
        {
            try
            {
                Wait.ElementExists(driver, "XPath", "//input[@name='email']", 100);

                //Enter email address
                EmailAddress.SendKeys(emailAddressValue);

                //Enter password
                Password.SendKeys(PasswordValue);

            }
            catch (Exception msg)
            {
                Assert.Fail("Test failed at SignIn page", msg.Message);
            }

        }

        public void ClickLoginButton()
        {
            //Click login button

            LoginButton.Click();

        }

        public bool ValidateYouAreLoggedInSuccessfully()
        {
            Wait.ElementExists(driver, "XPath", "//button[contains(text(),'Sign Out')]", 100);
            return SignOut.Displayed;
        }

        //login into the application
        public void Login(string emailAddress,string password)
        {
            ClickSignIn();
            ValidateYouAreAtLoginPage();
            EnterEmailAddressAndPassword(emailAddress,password);
            ClickLoginButton();
            bool isLoggedIn = ValidateYouAreLoggedInSuccessfully();
            Assert.IsTrue(isLoggedIn);
        }

    }
}








