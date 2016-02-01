using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Yandex.HtmlElements.Loaders;
using Tests.Configuration;

namespace Tests.Pages
{
    public abstract class Page
    {
        protected IWebDriver webDriver;
        protected GeneralConfig cfg;


        public Page(IWebDriver webDriver, GeneralConfig cfg)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            } else if (cfg == null)
            {
                throw new ArgumentNullException("cfg");
            }
            this.webDriver = webDriver;
            this.cfg = cfg;
                       
            HtmlElementLoader.PopulatePageObject(this, webDriver);
            PageFactory.InitElements(webDriver, this);
        }
        /// <summary>
        /// Loads into POM page, by executing  webDriver.Navigate().GoToUrl() or similar;
        /// </summary>
        public abstract void Navigate();
        
        /// <summary>
        /// Check that currently loaded into webDriver page is one descibed by derivate POM class
        /// </summary>
        /// <returns>True, if loaded into webDriver page is one descibed by this POM else - false</returns>
        public abstract bool Verify();

        /// <summary>
        /// This method executes Verify(), and if webDriver is not on expected page, execute Navigate();
        /// </summary>
        public virtual void ActualizePage()
        {
            if (!Verify())
            {
                Navigate();
            }
        }
    }
}
