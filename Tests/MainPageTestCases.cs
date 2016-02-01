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
            mainPage = new MainPage(TestConfiguration.WebDriver);
            mainPage.NavigateToThisPage();
        }
        
        
        [Test]
        [Description("Check that main page is accessible")]
        public void TC1_MainPageAccesible()
        {
            mainPage.ActualizePage();
            Assert.IsTrue(mainPage.isValidPage(), "Main page could not be validated.");
        }        

        [Test]
        [Description("Check that link from main page [tut.by] -> tab Финансы : Achivable")]
        public void TC2_CheckNavigateToFinance()
        {                        
            mainPage.ActualizePage();
            mainPage.FinanceClick();
            FinancePage financePage = new FinancePage(TestConfiguration.WebDriver);
            Assert.IsTrue(financePage.isValidPage(), "Finance page could not be validated.");
        }                
    } 
}
