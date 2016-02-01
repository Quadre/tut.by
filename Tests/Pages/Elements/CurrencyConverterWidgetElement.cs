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

        // option data-subtext="Доллар США" value="20823" data-currency-name="USD"        
        [Name("Calculator currency rate")]
        [FindsBy(How = How.XPath, Using = ".//option")]
        private IList<IWebElement> currencyRateList;
        

        public string InputValue
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

        public void ClearInput()
        {
            input.Clear();
        }

        public void SetActive()
        {
            input.Click();
        }

        public string InputCurrency
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
                catch (Exception)
                {

                    throw new ArgumentException(string.Format("'{0}' currency not found in the dropdown list", value));
                }
            }
        }

        public string GetRate (string currency)
        {
            try
            {
                //option data-subtext="Доллар США" value="20823" data-currency-name="USD"     
                IWebElement currencyWebElement = FindElement(By.XPath(".//option[@data-currency-name='" + currency.ToUpper() + "']"));
                return currencyWebElement.GetAttribute("value");
            }
            catch (Exception)
            {

                throw new ArgumentException(string.Format("'{0}' rate not found in the dropdown list", currency.ToUpper())); ;
            }
        }
    }
}
