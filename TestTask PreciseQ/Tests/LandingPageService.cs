using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace TestTask_PreciseQ.Tests
{
    public class LandingPageService
    {
        private readonly IWebDriver _driver;
        private readonly By _searchInput = By.Id("search_city");
        private readonly By _infoDay = By.XPath("//p[@class = 'infoDayweek']");
        private readonly By _dayPressures = By.XPath("//*[@id='bd6c']/div[1]/div[2]/table/tbody/tr[5]/td");

        public LandingPageService(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SearchForCity(string city)
        {
            var searchInput = _driver.FindElement(_searchInput);
            searchInput.SendKeys(city);
        }

        public void ClickTab(string tabTiltle)
        {
            var sundayTab = _driver.FindElement(By.XPath("//a[text() = '" + tabTiltle + "' ]"));
            sundayTab.Click();
        }

        public bool CheckTabIsOpened(string expectedText) {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            var sundayTabInfo = wait.Until(d => d.FindElement(_infoDay));
            Assert.IsTrue(expectedText == sundayTabInfo.Text);
            return true; 
        }

        public bool VerifyPressureInRange(int minValue, int maxValue)
        {
            IList <IWebElement> dayPressureRanges = _driver.FindElements(_dayPressures);
            int count = dayPressureRanges.Count;
            for (int i = 0; i < count; i++)
            {
                if (Convert.ToInt32(dayPressureRanges[i].Text) < minValue ||
                    Convert.ToInt32(dayPressureRanges[i].Text) > maxValue)
                    return false;
            }
         return true;
        }
    }
}

