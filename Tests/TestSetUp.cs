using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Tests.Configuration;
using Tests.Data;


namespace Tests
{ 
    [SetUpFixture]
    public class TestSetup
    {
        public static IWebDriver WebDriver { get; private set; }        
        public static GeneralConfig Config { get; private set; }        

        [OneTimeSetUp]
        [Description("Configure tests activity")]
        public void SetUp()
        {           
            DataPersister<GeneralConfig> cfgHelper = new DataPersister<GeneralConfig>();
                        
            if (cfgHelper.FileExist(GeneralConfig.DEFAULT_FILE_NAME))
            {
                Config =  cfgHelper.Load(GeneralConfig.DEFAULT_FILE_NAME);
            }
            else
            {
                Config = GeneralConfig.CreateDefault();
                cfgHelper.Save(Config, GeneralConfig.DEFAULT_FILE_NAME);
            }
            
            // think about muti-browser support
            WebDriver = new ChromeDriver();
            
            // configure default timeouts
            WebDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(Config.DefaultTimeoutSec));
        }

        [OneTimeTearDown]
        [Description("Tear down test activity")]
        public void TearDown()
        {            
            WebDriver.Quit();
        }
    }      
}
