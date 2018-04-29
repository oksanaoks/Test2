using System;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public IWebDriver driver;

        [TestMethod]
        public void TestMethod1()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://relex.ru/");

            IWebElement element_search = driver.FindElement(By.Name("q"));
            element_search.SendKeys("QA школа");

            IWebElement search = driver.FindElement(By.ClassName("input-group-btn"));
            search.Click();

            ReadOnlyCollection<IWebElement> element_item;
            element_item = driver.FindElements(By.ClassName("search-page"));

            ConditionOne(element_item); //Проверяем, что результат поиска не пустой.
            ConditionTwo();//Проверяем, что перешли на страицу с заголовом "QA-школа".
        }

        public void ConditionOne(ReadOnlyCollection<IWebElement> element_item)
        {
            IWebElement element_find;
            if (element_item.Count != 0)
            {
                element_find = driver.FindElement(By.CssSelector("#relex-panel-main > div > div > div.col-md-9 > div > div > a:nth-child(5)"));
                element_find.Click();
            }
            else
            {
                Assert.Inconclusive("По запросу ничего не найдено");
                driver.Quit();
            }
        }

        public void ConditionTwo()
        {
            try
            {
                IWebElement element_result = driver.FindElement(By.CssSelector("#relex-panel-main > div > div > div.col-md-9 > div > h2"));
                string proverka = element_result.Text;

                if (proverka == "QA - школа")
                {
                    driver.Quit();
                }
            }
            catch
            {
                Assert.Inconclusive("Перешли не на ту страницу");
                driver.Quit();
            }
        }
    }
}