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

        public MainPage(IWebDriver webDriver) : base(webDriver) { }

        public override void NavigateToThisPage()
        {
            webDriver.Navigate().GoToUrl("http://www." + GeneralConfig.Instance.MainDomian);
        }

        public void FinanceClick()
        {            
            LinkToFinanceOnTopMenu.Click();
        }

        public override bool isValidPage()
        {
            return webDriver.Url.Contains(GeneralConfig.Instance.MainDomian) &&  webDriver.Title.ToLower().Contains("tut.by");
        }
    }
}
