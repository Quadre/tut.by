using System;
using OpenQA.Selenium;
using Yandex.HtmlElements.Loaders;
using OpenQA.Selenium.Support.PageObjects;

namespace Tests.Pages
{
    public abstract class Page
    {
        protected IWebDriver webDriver;


        public Page(IWebDriver webDriver)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }
            this.webDriver = webDriver;            
            HtmlElementLoader.PopulatePageObject(this, webDriver);
            PageFactory.InitElements(webDriver, this);
        }
        /// <summary>
        /// Loads into POM page, by executing  webDriver.Navigate().GoToUrl() or similar;
        /// </summary>
        public abstract void NavigateToThisPage();
        
        /// <summary>
        /// Check that currently loaded into webDriver page is one descibed by this POM
        /// </summary>
        /// <returns>True, if loaded into webDriver page is one descibed by this POM else - false</returns>
        public abstract bool isValidPage();

        /// <summary>
        /// This method executes isExpectedPage(), and if webDriver is not on expected page, execute NavigateToThisPage();
        /// </summary>
        public virtual void ActualizePage()
        {
            if (!isValidPage())
            {
                NavigateToThisPage();
            }
        }
    }
}
