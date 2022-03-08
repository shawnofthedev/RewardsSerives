using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace BingRewards
{
    internal class BingBrowserRun
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new EdgeDriver("C:\\WebDriver\\edgedriver_win64\\");
            driver.Manage().Window.Size = new System.Drawing.Size(800, 800); 
        }

        [Test]
        public void Test()
        {
            string homeUrl = "https://www.bing.com/";

            driver.Url = homeUrl; 
        }

        [Test]
        public string GetCurrPoints()
        {
            var points = "";
            try
            {
                //points = driver.FindElement(By.ClassName("pointsDetail c-subheading-3 ng-binding")).GetAttribute("<b>");
                points = driver.FindElement(By.CssSelector(".pointsBreakdownCard:nth-child(1) b:nth-child(1)")).GetAttribute("<b>");                    
            }
            catch (Exception)
            { 
                throw;
            } 
            return points;
        }

        [Test]
        public string RandomSearches()
        {
            string eleId = "sb_form_q"; 
            driver.FindElement(By.Id(eleId)).Click();
            driver.FindElement(By.Id(eleId)).Clear();
            string inputText = RandomString();
            driver.FindElement(By.Id(eleId)).SendKeys(inputText);
            driver.FindElement(By.Id(eleId)).SendKeys(Keys.Enter);
            
            return inputText;
        }

        private string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijclmnopqrstuvwxyz!@#$%^&*()_+";
            int length = 10;

            Random random = new Random();
            string randomString = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            return randomString;
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
