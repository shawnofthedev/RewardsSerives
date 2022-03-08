using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace BingRewards
{
    public class BatMine
    {
        private IWebDriver driver;

        [SetUp]
        public void StartBrave()
        {
            string driverDir = "C:\\Program Files\\BraveSoftware\\Brave-Browser\\Application";
            var options = new ChromeOptions();
            options.BinaryLocation = "C:\\Program Files\\BraveSoftware\\Brave-Browser\\Application\\brave.exe";
            driver = new ChromeDriver(driverDir, options); 
        }

        public void OpenHome()
        {
            IWebElement body = driver.FindElement(By.TagName("body"));
            body.SendKeys(Keys.Control + "t");

        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

    }
}
