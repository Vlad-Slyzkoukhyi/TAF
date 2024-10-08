using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.DevTools.V126.Network;
using SeleniumExtras.WaitHelpers;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using log4net;

namespace TAF_Task.PageObjectsModel
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        protected IConfiguration? Configuration;
        protected ILog Log => LogManager.GetLogger(this.GetType());

        protected BasePage(IWebDriver driver) 
        {
            Driver = driver;
            PageFactory.InitElements(Driver, this);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }        

        private readonly By _acceptCookie = By.Id("onetrust-accept-btn-handler");

        public void AcceptCookieIfPresent()
        {
            try
            {
                WaitUntilElementIsVisibleByLocator(_acceptCookie);
                if (Driver.FindElement(_acceptCookie).Displayed)
                {
                    Log.Info("Press Accept cookie");
                    ClickElementJS(_acceptCookie);
                    Log.Info("Cookie accepted");
                }
            }
            catch (NoSuchElementException)
            {
                Log.Info("Cookie is not exist");
                // Element not found, no action needed
            }
        }

        public void ClickElement(By locator)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            IWebElement element = Driver.FindElement(locator);
            element.Click();
        }

        public void ClickElement(IWebElement element)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(element));
            element?.Click();
        }

        public async Task ClickElementWithDelayAfterClicking(By locator)
        {
            IWebElement element = Wait.Until(ExpectedConditions.ElementToBeClickable(Driver.FindElement(locator)));
            element.Click();
            await Task.Delay(1000);
        }

        public void ClickElementJS(By locator)
        {
            IWebElement element = Driver.FindElement(locator);
            Wait.Until(ExpectedConditions.ElementToBeClickable(element));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
        }

        public void ClickElementJS(IWebElement element)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(element));
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
        }

        public void SendKeys(By locator, string keyword)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            IWebElement element = Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(keyword);
        }

        public string GetText(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            IWebElement element = Driver.FindElement(locator);
            return element.Text.Trim();
        }

        public void ScroolJS(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            IWebElement element = Driver.FindElement(locator);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public void ScrollJSTillTheEnd()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        public void WaitUntilAllElementsIsPresentByLocator(By locator)
        {
            Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }

        public void WaitUntilElementIsVisibleByLocator(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement GetElement(By locator)
        {
            Wait.Until(ExpectedConditions.ElementExists(locator));
            IWebElement element = Driver.FindElement(locator);
            return element;
        }

        public IList<IWebElement> GetElements(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            IList<IWebElement> elements = Driver.FindElements(locator);
            return elements;
        }
    }
}
