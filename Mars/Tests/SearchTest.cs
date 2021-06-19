using System;
using AventStack.ExtentReports;
using Mars.Pages;
using Mars.Utilities;
using NUnit.Framework;
using static Mars.Utilities.CommonMethods;

namespace Mars.Tests
{
    [TestFixture]
    class SearchTest : Driver
    {
        private readonly CommonMethods commonMethods;
       

        public SearchTest()
        {
            commonMethods = new CommonMethods();
            
        }

        [Test]
        [TestCaseSource(typeof(Driver), "BrowserToRunWith")]
        public void SearchSkillsByAllCategoriesTest(string browserName)
        {

            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name).Info("Test Started");
                test.Log(Status.Info, "SearchSkillsByAllCategories method is called");

                Setup(browserName);
                //Search Page Objects
                SearchPage searchPageObj = new SearchPage(driver);
                searchPageObj.SearchSkillsByAllCategories(ExcelLibHelper.ReadData(1, "SearchSkillToAccept"));

                test.Log(Status.Pass, "Search skills by all categories is tested");
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
        public void SearchSkillsByFiltersTest(string browserName)
        {

            try
            {
                test = extent.CreateTest(TestContext.CurrentContext.Test.Name).Info("Test Started");
                test.Log(Status.Info, "SearchSkillsByFilters method is called");

                Setup(browserName);
                //Search Page Objects
                SearchPage searchPageObj = new SearchPage(driver);
                searchPageObj.SearchSkillsByFilters();

                test.Log(Status.Pass, "Search skills by filters is tested");
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


