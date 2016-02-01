using OpenQA.Selenium.Support.PageObjects;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Attributes;

namespace Tests.Pages.Elements
{
    [Name("Currency rate widget")]
    [Block(How = How.XPath, Using = "//div[.//div/@id='tab-kurs' and @class='b-widget']")]
    class CurrencyRateWidget : HtmlElement
    {

        [FindsBy(How = How.XPath, Using = ".//div[@id='k-nbrb']//table[@class='k-table']")]
        public CurrencyRateWidgetTable RateByNBRB;
            
    }
}
