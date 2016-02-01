using System;
using OpenQA.Selenium;
using Tests.Configuration;
using Tests.Pages.Elements;

namespace Tests.Pages
{
    public enum RateProviderEnum { NBRB };

    class FinancePage : Page
    {                
        public CurrencyConverterWidget CurrencyConverter;
        public CurrencyRateWidget CurrencyRate;

        public FinancePage(IWebDriver webDriver) : base(webDriver) { }
        
        public override void NavigateToThisPage()
        {
            webDriver.Navigate().GoToUrl("http://www.finance." + GeneralConfig.Instance.MainDomian);
        }

        public override bool isValidPage()
        {
            return webDriver.Url.Contains("finance." + GeneralConfig.Instance.MainDomian) && webDriver.Title.ToLower().Contains("finance.tut.by");
        }    

        public double GetRate(RateProviderEnum rateProvider, string currencyFrom, string currencyTo)
        {            
            if (currencyFrom == currencyTo)
            {
                return 1;
            }
            else if (currencyFrom != "BYR" && currencyTo != "BYR")
            {
                throw new NotImplementedException(String.Format("Cross-currency checks not implemented. CurrencyFrom, or currencyTo should be equal 'BYR'. CurrencyFrom={0}, currencyTo{1}", currencyFrom, currencyTo));
            }
            else if (currencyFrom == "BYR")
            {
                return 1 / GetBYRRate(rateProvider, currencyTo);                    
            }
            else if (currencyTo == "BYR")
            {
                return GetBYRRate(rateProvider, currencyFrom);
            }

            return 0;
        }

        public double GetBYRRate(RateProviderEnum rateProvider, string foreginCurrency)
        {           
            string strRate = "";            

            switch (rateProvider)
            {
                case RateProviderEnum.NBRB:
                    strRate = CurrencyRate.RateByNBRB.RateTable[foreginCurrency];                    
                    break;   
            }

            double decRate = 0.0;
            if (double.TryParse(strRate, out decRate) == false)
            {
                throw new FormatException(string.Format("Rate provided by {0} couldn't be parsed as Decimal value, provided values is {1}", rateProvider.ToString(), strRate));
            }
                        
            return decRate;
        }
    }
}
