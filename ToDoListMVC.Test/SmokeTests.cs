using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace ToDoListMVC.Test
{
    public class SmokeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestToDoListRedirectIfNotAuthorized()
        {
            using (IWebDriver driver = new EdgeDriver(@"C:\Users\PC\Desktop"))
            {
                driver.Navigate().GoToUrl("https://localhost:44337/ToDoLists");

                Assert.AreEqual("Log in - ToDoListMVC", driver.Title);
            }
        }
    }
}