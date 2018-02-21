using OpenQA.Selenium;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.UI;

namespace TestTask_PreciseQ.Tests
{
    public class LandingPageService
    {
        IWebDriver _driver;

        public LandingPageService(IWebDriver driver)
        {
            _driver = driver;
        }

        public void SearchForCity(string city)
        {
            IWebElement searchInput = _driver.FindElement(By.Id("search_city"));
            searchInput.SendKeys(city);
        }

        public void ClickTab(string tabTiltle)
        {
            IWebElement sundayTab = _driver.FindElement(By.XPath("//a[text() = 'Воскресенье']"));
            sundayTab.Click();
        }

        public bool CheckTabIsOpened(string expectedText) {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            IWebElement sundayTabInfo = wait.Until(d => d.FindElement(By.XPath("//p[@class = 'infoDayweek']")));
            Assert.IsTrue(expectedText == sundayTabInfo.Text);
            return true; 
        }

        public bool VerifyPressureInRange(int minValue, int maxValue)
        {   
            string nightPressure = _driver.FindElement(By.XPath("//tr[@class = 'gray']/td[@class = 'p1 bR '] [1]")).Text;
            int nightPressureInt = Convert.ToInt32(nightPressure);
            string morningPressure = _driver.FindElement(By.XPath("//tr[@class = 'gray']/td[@class = 'p2 bR ' and @colspan = '2'] [1]")).Text;
            int morningPressureInt = Convert.ToInt32(morningPressure);
            string dayPressure = _driver.FindElement(By.XPath("//tr[@class = 'gray']/td[@class = 'p3 bR '] [1]")).Text;
            int dayPressureInt = Convert.ToInt32(dayPressure);
            string eveningPressure = _driver.FindElement(By.XPath("//tr[@class = 'gray']/td[@class = 'p4  '] [1]")).Text;
            int eveningPressureInt = Convert.ToInt32(eveningPressure);
            try
            {
                Assert.IsTrue(nightPressureInt >= minValue && nightPressureInt <= maxValue);
                Assert.IsTrue(morningPressureInt >= minValue && morningPressureInt <= maxValue);
                Assert.IsTrue(dayPressureInt >= minValue && dayPressureInt <= maxValue);
                Assert.IsTrue(eveningPressureInt >= minValue && eveningPressureInt <= maxValue);
            }
            catch (AssertionException e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
    }
}

