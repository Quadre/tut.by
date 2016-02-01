using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using OpenQA.Selenium.Support.PageObjects;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Attributes;
using OpenQA.Selenium;
using Tests.Utils;

namespace Tests.Pages.Elements
{
    [Name("General table for CurrencyRate widget")]
    [Block]
    class CurrencyRateWidgetTable : HtmlElement
    {
        [FindsBy(How = How.XPath, Using = ".//tr/td[1]/a")]
        private IList<IWebElement> CurrencyName;

        [FindsBy(How = How.XPath, Using = ".//tr/td[2]/span")]
        private IList<IWebElement> CurrencyRate;


        /// <summary>
        /// Represent data in human-readeble format: Dictionary[Currency, Rate]
        /// </summary>
        /// <returns>Dictionary[Currency, Rate] pair</returns>
        public Dictionary<string, string> ToDictionary  ()
        {
            if (CurrencyName.Count != CurrencyRate.Count)
            {
                throw new IndexOutOfRangeException("CurrencyName.Count != CurrencyCourse.Count");
            }
            Dictionary<string, string> table = CurrencyName.ToDictionary(x => GetCurrencyFromUrlParameter(x.GetAttribute("href")), 
                                                                            x => GetRateById(CurrencyName.IndexOf(x)));                
            return table;
        }
        

        private string GetCurrencyFromUrlParameter (string href)
        {
            return HttpUtility.ParseQueryString(new Uri(href).Query).Get("currency").ToUpper();
        }

        private string GetRateById(int id)
        {            
            string rate = CurrencyRate[id].GetAttribute("innerHTML");            
            return CurrencyUtils.NormalizeCurrencyRate(rate);
        }
    }
}
