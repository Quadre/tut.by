using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Attributes;


namespace Tests.Pages.Elements
{
    [Block]
    public class CurrencyConverterWidgetElement : HtmlElement
    {
        [Name("Currency input field")]
        [FindsBy(How = How.XPath, Using = ".//input")]
        private IWebElement input;

        [Name("Currency select button")]
        [FindsBy(How = How.XPath, Using = ".//button")]
        private IWebElement cyrrencyButton;
        
        [Name("Calculator currency rate")]
        [FindsBy(How = How.XPath, Using = ".//option")]
        private IList<IWebElement> currencyRateList;

        /// <summary>
        /// Get\Set value of Amount field in currency convertor
        /// </summary>
        public string Value
        {
            get
            {
                return input.GetAttribute("value");
            }
            set
            {
                input.SendKeys(value);
            }
        }

        /// <summary>
        /// Clear value of Amount field in currency convertor
        /// </summary>
        public void ClearInput()
        {
            input.Clear();
        }

        /// <summary>
        /// Activate Amount field in currency convertor, by emulating single click on it
        /// </summary>
        public void SetActive()
        {
            input.Click();
        }

        /// <summary>
        /// Get\Set value of currency field in currency convertor
        /// </summary>
        public string Currency
        {
            get
            {
                return cyrrencyButton.GetAttribute("data-title").ToUpper();
            }
            set
            {              
                // v2
                try
                {
                    IWebElement currencyRef = FindElement(By.XPath(".//ul[@role='menu']//a[./span/text()='" + value + "']"));
                    cyrrencyButton.Click();
                    currencyRef.Click();
                }
                catch (Exception ex)
                {

                    throw new ArgumentException(string.Format("'{0}' currency not found in the dropdown list", value), ex);
                }
            }
        }

        /// <summary>
        /// Get rate value for the selected currency from data currency convertor
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>Rate</returns>
        public string GetRate (string currency)
        {
            try
            {                
                IWebElement currencyWebElement = FindElement(By.XPath(".//option[@data-currency-name='" + currency.ToUpper() + "']"));
                return currencyWebElement.GetAttribute("value");
            }
            catch (Exception ex)
            {

                throw new ArgumentException(string.Format("'{0}' rate not found in the dropdown list", currency.ToUpper()), ex); ;
            }
        }
    }
}
