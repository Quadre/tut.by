using System;
using NUnit.Framework;
using Tests.Pages;


namespace Tests
{
    [TestFixture]  
    public class MainPageTestCases 
    {        
        private MainPage mainPage;                

        [OneTimeSetUp]
        [Description("Sets up  tests activity")]
        public void SetUp() 
        {
            mainPage = new MainPage(TestSetup.WebDriver, TestSetup.Config);
            mainPage.Navigate();
        }
        
        
        [Test]
        [Description("Check that main page is accessible")]
        public void TC1_MainPageAccesible()
        {
            mainPage.ActualizePage();
            Assert.IsTrue(mainPage.Verify(), "Main page could not be validated.");
        }        

        [Test]
        [Description("Check that link from main page [tut.by] -> tab Финансы : Achivable")]
        public void TC2_CheckNavigateToFinance()
        {                        
            mainPage.ActualizePage();
            mainPage.FinanceClick();
            FinancePage financePage = new FinancePage(TestSetup.WebDriver, TestSetup.Config);
            Assert.IsTrue(financePage.Verify(), "Finance page could not be validated.");
        }                
    } 
}
