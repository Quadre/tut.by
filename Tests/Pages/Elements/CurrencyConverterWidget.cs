using System;
using OpenQA.Selenium.Support.PageObjects;
using Yandex.HtmlElements.Elements;
using Yandex.HtmlElements.Attributes;

namespace Tests.Pages.Elements
{
    
    [Name("Currency converter")]
    [Block(How = How.Id, Using = "currency_converter")]
    public class CurrencyConverterWidget : HtmlElement
    {
        [Name("Top field")]
        [FindsBy(How = How.XPath, Using = ".//div[@class='converterRows']/table[1]")]
        public CurrencyConverterWidgetElement CaluculatorTop;

        [Name("Bottom field")]
        [FindsBy(How = How.XPath, Using = ".//div[@class='converterRows']/table[2]")]
        public CurrencyConverterWidgetElement CaluculatorBottom;

        public void ClearInputs()
        {
            CaluculatorTop.ClearInput();
            CaluculatorBottom.ClearInput();
        }
    }
}
