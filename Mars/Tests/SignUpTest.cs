using System;
using AventStack.ExtentReports;
using Mars.Pages;
using Mars.Utilities;
using NUnit.Framework;
using static Mars.Utilities.CommonMethods;

namespace Mars.Tests
{
    [TestFixture]
    public class SignUpTest : Driver
    {
        private CommonMethods commonMethods;

        public SignUpTest()
        {
            commonMethods = new CommonMethods();
        }
        /*
        [Test]
        [TestCaseSource(typeof(Driver), "BrowserToRunWith")]
        public void RegistrationTest(string browserName)
        {

            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name).Info("Test Started");
                test.Log(Status.Info, "Registration method is called");

                Setup(browserName);
                //Registration Page objects
                RegistrationPage registrationPageObj = new RegistrationPage(driver);
                registrationPageObj.Registration();

                test.Log(Status.Pass, "User registered successfully");
                test.Pass("Test Passed");
            }
            catch (Exception e)
            {

                var mediaEntity = commonMethods.CaptureScreenshotAndReturnModel(TestContext.CurrentContext.Test.Name.Trim());
                test.Log(Status.Fail, e.StackTrace.ToString());
                test.Fail("Test Failed", mediaEntity);
            }

        }
        */
        [Test]
        [TestCaseSource(typeof(Driver), "BrowserToRunWith")]
        public void LoginTest(string browserName)
        {

            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name).Info("Test Started");
                test.Log(Status.Info, "login method is called");

                Setup(browserName);
                //SignInPage objects
                SignInPage signInPageObj = new SignInPage(driver);
                signInPageObj.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));

                test.Log(Status.Pass, "User login successful");
                test.Pass("Test Passed");
            }
            catch (Exception e)
            {

                var mediaEntity = commonMethods.CaptureScreenshotAndReturnModel(TestContext.CurrentContext.Test.Name.Trim());
                test.Log(Status.Fail, e.StackTrace.ToString());
                test.Fail("Test Failed", mediaEntity);
            }

        }

        [Test]
        [TestCaseSource(typeof(Driver), "BrowserToRunWith")]
        public void ChangePasswordTest(string browserName)
        {

            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name).Info("Test Started");
                test.Log(Status.Info, "ChangePassword method is called");

                Setup(browserName);
                // ChangePasswordPage objects
                ChangePasswordPage changePasswordPageObj = new ChangePasswordPage(driver);
                changePasswordPageObj.ChangePassword();
                test.Log(Status.Pass, "Password changed successfully");
                test.Pass("Test Passed");
            }
            catch (Exception e)
            {

                var mediaEntity = commonMethods.CaptureScreenshotAndReturnModel(TestContext.CurrentContext.Test.Name.Trim());
                test.Log(Status.Fail, e.StackTrace.ToString());
                test.Fail("Test Failed", mediaEntity);
            }

        }
    }
}
