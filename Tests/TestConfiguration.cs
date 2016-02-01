using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Tests.Configuration;


namespace Tests
{ 
    [SetUpFixture]
    public class TestConfiguration
    {
        public static IWebDriver WebDriver { get; private set; }        

        [OneTimeSetUp]
        [Description("Configure tests activity")]
        public void SetUp()
        {           
            ConfigHelperBase<GeneralConfig> cfgHelper = new ConfigHelperBase<GeneralConfig>();
                        
            if (File.Exists(cfgHelper.DefaultConfigFullPath))
            {                
                GeneralConfig.Instance = cfgHelper.Load();
            }
            else
            {
                GeneralConfig.Instance = GeneralConfig.CreateDefault();
                cfgHelper.Save(GeneralConfig.Instance);
            }
            
            // think about multiple instances
            WebDriver = new ChromeDriver();
            // configure default timeouts
            WebDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(GeneralConfig.Instance.DefaultTimeoutSec));
        }

        [OneTimeTearDown]
        [Description("Tear down test activity")]
        public void TearDown()
        {
            GeneralConfig.Instance = null;
            WebDriver.Quit();
        }
    }      
}
