using System;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class ShareSkillPage 
    {
        private readonly IWebDriver driver;
        private SignInPage signIn;

        //Create a Constructor
        public ShareSkillPage(IWebDriver driver)
        {
            this.driver = driver;
            signIn = new SignInPage(driver);

        }


        //page factory design pattern
        IWebElement ShareSkill => driver.FindElement(By.XPath("//*[@id='account-profile-section']/div/section[1]/div/div[2]/a"));
        IWebElement Title => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input"));
        IWebElement Description => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[2]/div/div[2]/div[1]/textarea"));
        IWebElement GraphicsDesign => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[1]/select/option[2]"));
        IWebElement DigitalMarketing => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[1]/select/option[3]"));
        IWebElement WritingTranslation => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[1]/select/option[4]"));
        IWebElement VideoAnimation => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[1]/select/option[5]"));
        IWebElement MusicAudio => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[1]/select/option[6]"));
        IWebElement ProgrammingTech => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[1]/select/option[7]"));
        IWebElement Business => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[1]/select/option[8]"));
        IWebElement FunLifestyle => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[1]/select/option[9]"));
        IWebElement Subcategory => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[3]/div[2]/div/div[2]/div[1]/select/option[2]"));
        IWebElement Tags => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[4]/div[2]/div/div/div/div/input"));
        IWebElement HourlyBasisService => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[5]/div[2]/div[1]/div[1]/div/input"));
        IWebElement OneOffService => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[5]/div[2]/div[1]/div[2]/div/input"));
        IWebElement Onsite => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[6]/div[2]/div/div[1]/div/input"));
        IWebElement Online => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[6]/div[2]/div/div[2]/div/input"));
        IWebElement StartDate => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[7]/div[2]/div/div[1]/div[2]/input"));
        IWebElement EndDate => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[7]/div[2]/div/div[1]/div[4]/input"));
        IWebElement SkillExchange => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[2]/div/div[1]/div/input"));
        IWebElement Credit => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[2]/div/div[2]/div/input"));
        IWebElement CreditServiceCharge => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[4]/div/div/input"));
        IWebElement SkillExchangeTag => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[4]/div/div/div/div/div/input"));
        IWebElement Active => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[10]/div[2]/div/div[1]/div/input"));
        IWebElement Hidden => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[10]/div[2]/div/div[2]/div/input"));
        IWebElement Save => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[11]/div/input[1]"));
        IWebElement Cancel => driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[11]/div/input[2]"));
        IWebElement SavedTitle => driver.FindElement(By.XPath("//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]"));
        IWebElement SavedDescription => driver.FindElement(By.XPath("//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[4]"));
        IWebElement SavedCategory => driver.FindElement(By.XPath("//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[2]"));
        IWebElement FileInput => driver.FindElement(By.XPath("//*[@id='selectFile']"));
        
        //reading data from file
        private string title = ExcelLibHelper.ReadData(1, "Title");
        private string description = ExcelLibHelper.ReadData(1, "Description");
        private string category = ExcelLibHelper.ReadData(1, "Category");
        private string tags = ExcelLibHelper.ReadData(1, "Tags");
        private string serviceType = ExcelLibHelper.ReadData(1, "ServiceType");
        private string locationType = ExcelLibHelper.ReadData(1, "LocationType");
        private string skillTrade = ExcelLibHelper.ReadData(1, "SkillTrade");
        private string acive = ExcelLibHelper.ReadData(1, "Active");
        private string skillExchangeTag = ExcelLibHelper.ReadData(1, "SkillExchangeTag");
        private string creditServiceCharge = ExcelLibHelper.ReadData(1, "CreditServiceCharge");
        private int addDaysToStartDate = Convert.ToInt32(ExcelLibHelper.ReadData(1, "AddDaysInCurrentDateToStart"));
        private int addDaysToEndDate = Convert.ToInt32(ExcelLibHelper.ReadData(1, "AddDaysInCurrentDateToEnd"));

       

        //creating a service listing
        public void CreateServiceListing()
        {
            signIn.Login(ExcelLibHelper.ReadData(1, "EmailAddress"), ExcelLibHelper.ReadData(1, "Password"));
            ClickShareSkill();
            ValidateYouAreAtShareSkillPage();
            EnterTitle(title);
            EnterDescription(description);
            SelectCategory(category);
            SelectSubCategory();
            EnterTags(tags);
            SelectServiceType(serviceType);
            SelectLocationType(locationType);
            EnterStartDate(addDaysToStartDate);
            EnterEndDate(addDaysToStartDate,addDaysToEndDate);
            SelectSkillTrade(skillTrade, skillExchangeTag, creditServiceCharge);
            UploadWorkSamples();
            SelectActive(acive);
            ClickSave();
            bool isServiceSaved =ValidateServiceSavedSuccessfully();
            
        }

        public void ClickShareSkill()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='account-profile-section']/div/section[1]/div/div[2]/a", 500);
            //click Share Skill 
            ShareSkill.Click();

        }

        public void ValidateYouAreAtShareSkillPage()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='service-listing-section']/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input", 200);
            bool isShareSkillPage = Save.Displayed;
            Assert.IsTrue(isShareSkillPage);
        }


        public void EnterTitle(string title)
        {
            Title.Clear();
            Title.SendKeys(title);
        }

        public void EnterDescription(string description)
        {
            Description.Clear();
            Description.SendKeys(description);
        }

        public void SelectCategory(string category)
        {
           
            switch (category)
            {
                case "Graphics & Design":
                    
                    GraphicsDesign.Click();
                    break;

                case "Digital Marketing":
                    DigitalMarketing.Click();
                    break;

                case "Writing & Translation":
                    WritingTranslation.Click();
                    break;

                case "Video & Animation":
                    VideoAnimation.Click();
                    break;


                case "Music & Audio":
                    MusicAudio.Click();
                    break;

                case "Programming & Tech":
                    ProgrammingTech.Click();
                    break;

                case "Business":
                    Business.Click();
                    break;


                default:
                    FunLifestyle.Click();
                    break;

            }
        }

        public void SelectSubCategory()
        {
            Subcategory.Click();
        }

        private bool ExistsElement()
        {
            try
            {
                driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[4]/div[2]/div/div/div/span/a"));
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }

        public void EnterTags(string tags)
        {
            bool isRemoveTag = ExistsElement();
            if (isRemoveTag == true)
            {
                driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[4]/div[2]/div/div/div/span/a")).Click();
                Tags.SendKeys(tags);
                Tags.SendKeys(Keys.Enter);
            }
            else
            {
                Tags.SendKeys(tags);
                Tags.SendKeys(Keys.Enter);
            }
            
        }

        public void SelectServiceType(string serviceType)
        {
            
            if (serviceType == "Hourly basis service" )
            {
                HourlyBasisService.Click();
            }
            else
            {
                OneOffService.Click();
            }
           
        }

        public void SelectLocationType(string locationType)
        {
          
            if (locationType == "On-site")
            {
                Onsite.Click();
            }
            else
            {
                Online.Click();
            }
        }

        public void EnterStartDate(int DaysToStartDate)
        { 
            DateTime currentDate = DateTime.Now;
            StartDate.Clear();
            StartDate.SendKeys(currentDate.AddDays(DaysToStartDate).ToString("dd/MM/yyyy"));
  
        }

        public void EnterEndDate(int DaysToStartDate, int DaysToEndDate)
        {
            DateTime currentDate = DateTime.Now;
            EndDate.Clear();

            if (DaysToStartDate > DaysToEndDate)
            {
                EndDate.SendKeys(currentDate.AddDays(DaysToStartDate).ToString("dd/MM/yyyy"));
            }
            else
            {
                EndDate.SendKeys(currentDate.AddDays(DaysToEndDate).ToString("dd/MM/yyyy"));
            }
            
          
        }


        public void SelectSkillTrade(string skillTrade, string skillExchangeTag, string creditServiceCharge)
        {
            
            if (skillTrade == "Skill-exchange")
            {
                SkillExchange.Click();
                SkillExchangeTag.SendKeys(skillExchangeTag);
                SkillExchangeTag.SendKeys(Keys.Enter);
            }
            else
            {
                Credit.Click();
                CreditServiceCharge.Clear();
                CreditServiceCharge.SendKeys(creditServiceCharge);
            }
        }

        public void UploadWorkSamples()
        {
            FileInput.SendKeys(ConstantHelpers.WorkSamplePath);
        }
 

        public void SelectActive(string active)
        {
            if (active == "Active")
            {
                Active.Click();
            }
            else
            {
                Hidden.Click();
            }
        }

        public void ClickSave()
        {
            Save.Click();
            
        }

        public void ClickCancel()
        {
            Cancel.Click();
        }
       

        public bool ValidateServiceSavedSuccessfully()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]", 10);
            if (SavedTitle.Text == title && SavedDescription.Text ==  description && SavedCategory.Text == category)
            {
                //Assert.Pass("user is able to create Service successfully, test passed");
                return true;
            }
            else
            {
                //Assert.Fail("user is not able to create Service, test failed");
                return false;
            }

        }
 
    }
}
