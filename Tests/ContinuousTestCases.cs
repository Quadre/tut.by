﻿using System;
using System.IO;
using NUnit.Framework;
using Tests.Pages;
using Tests.Utils;
using Tests.Data;

namespace Tests
{
    [TestFixture]  
    public class ContinuousTestCases
    {        
        private FinancePage financePage;        
        private WeekendRateCheckDataHelper dataHelper;

        [OneTimeSetUp]
        [Description("Sets up tests activity")]
        public void SetUp() 
        {            
            dataHelper = new WeekendRateCheckDataHelper();
            financePage = new FinancePage(TestConfiguration.WebDriver);
            financePage.NavigateToThisPage();
        }       
    
        /*
        [Test]
        [Description("Check that rate didn't change on weekend (Friday - Saturday - Sunday), for BYR\\USD pair")]
        public void TC4_WeekendRateCheck()
        {            
            AnalizeRateByDate(DateTime.Now);
        }*/

        [TestCase(2016, 01, 29)]
        [TestCase(2016, 01, 30)]
        [TestCase(2016, 01, 31)]
        [Description("Mock input data for testing TC4_WeekendRateCheck")]
        public void MockTC4_WeekendRateCheck(int year, int month, int date)
        {                                  
            AnalizeRateByDate(new DateTime(year, month, date));
        }       
        
        private void AnalizeRateByDate (DateTime todayDateStamp, string currency = "USD")
        {
            WeekendRateCheckData storedData;
            string usdRateByCalc;

            switch (todayDateStamp.DayOfWeek)
            {
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                    Assert.IsTrue(false, string.Format("Execution applicable only throught Friday - Sunday"));
                    break;
                case DayOfWeek.Friday:
                    financePage.ActualizePage();
                    string usdRateByNBRB = CurrencyUtils.NormalizeCurrencyRate(financePage.CurrencyRate.RateByNBRB.RateTable[currency]);
                    usdRateByCalc = financePage.CurrencyConverter.CaluculatorBottom.GetRate(currency);

                    storedData = new WeekendRateCheckData();
                    storedData.DateStamp = todayDateStamp;
                    storedData.Rate = usdRateByCalc;
                    dataHelper.Save(storedData);

                    StringAssert.IsMatch(usdRateByNBRB, usdRateByCalc, currency + " rate by NBRB and CurrencyCalculator rate didn't match.");
                    break;
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    financePage.ActualizePage();

                    Assert.IsTrue(File.Exists(dataHelper.DefaultConfigFullPath), "Precondition - failed. No previousle stored data.");

                    storedData = dataHelper.Load();
                    usdRateByCalc = financePage.CurrencyConverter.CaluculatorBottom.GetRate(currency);

                    Assert.IsTrue(todayDateStamp.Subtract(storedData.DateStamp).TotalDays < 3,
                            string.Format("Checked data must be within same week. Stored Data is from {0}, today is {1}", storedData.DateStamp, todayDateStamp));

                    StringAssert.IsMatch(storedData.Rate, usdRateByCalc,
                            string.Format("Stored USD rate and CurrencyCalculator rate didn't match.\nToday {0} [{1}] != Stored {2} [{3}]", usdRateByCalc, todayDateStamp, storedData.Rate, storedData.DateStamp));

                    break;
            }
        }      
    } 
}
