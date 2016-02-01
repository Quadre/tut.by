using NUnit.Framework;
using System;
using Tests.Pages;
using Tests.Utils;

namespace Tests
{
    [TestFixture]  
    public class FinancePageTestCases 
    {                
        private FinancePage financePage;

        [OneTimeSetUp]
        [Description("Sets up  tests activity")]
        public void SetUp()
        {
            //base.SetUp();
            financePage = new FinancePage(TestConfiguration.WebDriver);
            financePage.NavigateToThisPage();
        }
               
        private void PrepareCurrencyConvertor (string ccyFrom, string ccyTo)
        {
            financePage.ActualizePage();            

            if (financePage.CurrencyConverter.CaluculatorBottom.InputCurrency!= ccyFrom)
            {
                financePage.CurrencyConverter.CaluculatorBottom.InputCurrency = ccyFrom;
            }

            if (financePage.CurrencyConverter.CaluculatorTop.InputCurrency != ccyTo)
            {
                financePage.CurrencyConverter.CaluculatorTop.InputCurrency = ccyTo;
            }

            financePage.CurrencyConverter.ClearInputs();
        }




        [TestCase(1, "USD", "BYR")]
        [TestCase(10, "USD", "BYR")]
        [TestCase(100, "USD", "BYR")]
        [TestCase(1000, "USD", "BYR")]
        [TestCase(1000000, "USD", "BYR")]
        // Swap currency
        [TestCase(1, "BYR", "USD")]
        [TestCase(10, "BYR", "USD")]
        [TestCase(100, "BYR", "USD")]
        [TestCase(1000000, "BYR", "USD")]

        [TestCase(1.12, "USD", "BYR", Description = "floating point")]                        
        
        //Check Round
        [TestCase(1.129, "USD", "USD", Description = "Check Round")]
        [TestCase(1.12123232, "USD", "USD", Description = "Check precision")]

        [Description("Check that Currency Converter works correct for current day rate by NBRB")]
        public void TC3_CheckCurrencyConvertor_RateByNBRB(double amount, string currencyFrom, string currencyTo)
        {
            // Configure calculator
            PrepareCurrencyConvertor(currencyFrom, currencyTo);

            // Set value in Bottom input
            financePage.CurrencyConverter.CaluculatorBottom.InputValue = amount.ToString();
            // Get value, calculated by CurrencyConvertor
            string strOutValActual = financePage.CurrencyConverter.CaluculatorTop.InputValue;
            double decOutValActual = 0;
            double.TryParse(CurrencyUtils.NormalizeCurrencyRate(strOutValActual), out decOutValActual);

            // Calculate expected value
            double decRate = financePage.GetRate(RateProviderEnum.NBRB, currencyFrom, currencyTo);
            double decOutValCalculated = Math.Round(decRate * amount, 2);

            Assert.AreEqual(decOutValActual, decOutValCalculated, 0.001,
                        string.Format("Checked combination: {0} {1} convert to {2}.", amount, currencyFrom, currencyTo));
    
        }

        // Add tc with incorrect input data
        // !@#$%^&&*()__
        // asdas
        // -1  -- max (?)        
    }
}
