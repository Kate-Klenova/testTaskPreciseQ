using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;

namespace TestTask_PreciseQ.Tests
{
    public class TC1_TestPressureRange
    {
        private IWebDriver _driver;
        LandingPageService landingPageService; 

        [SetUp]
        public void Init()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            landingPageService = new LandingPageService(_driver);
        }

        [TestCase]
        public void TestPressureRange()
        {
            var testDay = "Воскресенье";
            var testPlace = "Драгобрат";

            _driver.Navigate().GoToUrl("https://sinoptik.ua/");
            landingPageService.SearchForCity(testPlace);
            landingPageService.ClickTab(testDay);
            landingPageService.CheckTabIsOpened(testDay);
            Assert.IsTrue(landingPageService.VerifyPressureInRange(700,800));
        }

        [TearDown]
        public void CleanUp()
        {
            _driver.Quit();
        }
    }
}
