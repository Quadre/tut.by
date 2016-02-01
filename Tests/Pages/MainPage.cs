using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Tests.Configuration;

namespace Tests.Pages
{    
    class MainPage : Page
    {        
        [FindsBy(How = How.XPath, Using = "//a[@class='topbar__link' and @title='Финансы']")]             
        public IWebElement LinkToFinanceOnTopMenu { get; set; }

        public MainPage(IWebDriver webDriver, GeneralConfig cfg) : base(webDriver, cfg) { }

        public override void Navigate()
        {
            webDriver.Navigate().GoToUrl("http://www." + cfg.MainDomian);
        }

        public void FinanceClick()
        {            
            LinkToFinanceOnTopMenu.Click();
        }

        public override bool Verify()
        {
            return webDriver.Url.Contains(cfg.MainDomian) &&  webDriver.Title.ToLower().Contains("tut.by");
        }
    }
}
