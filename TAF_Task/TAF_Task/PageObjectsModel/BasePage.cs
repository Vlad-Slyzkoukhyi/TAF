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
using log4net.Config;
using log4net;

namespace TAF_Task.PageObjectsModel
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        protected IConfiguration? Configuration;
        protected ILog Log => LogManager.GetLogger(this.GetType());        

        private readonly By _acceptCookie = By.Id("onetrust-accept-btn-handler");

        protected BasePage(IWebDriver driver) 
        {
            Driver = driver;
            PageFactory.InitElements(Driver, this);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }
        protected BasePage(IWebDriver driver, IConfiguration? configuration)
        {
            Driver = driver;
            Configuration = configuration;
            PageFactory.InitElements(Driver, this);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void AcceptCookieButton()
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(_acceptCookie));
                Wait.Until(ExpectedConditions.ElementToBeClickable(_acceptCookie));

                IWebElement acceptCookieButton = Driver.FindElement(_acceptCookie);
                if (acceptCookieButton != null)
                {
                    acceptCookieButton.Click();
                    Log.Info("Cookie Accepted.");
                }
                else
                {
                    Log.Info("Cookie Accept button is not found."); 
                }
            }
            catch (WebDriverTimeoutException ex)
            {
                Log.Info($"Cookie dialog did not appear within the expected time: {ex.Message}");
            }
        }

        public void ClickJS(IWebElement? element)
        {
            Driver.ExecuteJavaScript("arguments[0].click();", element);
        }
        public void ScroolJS(IWebElement? element)
        {
            Driver.ExecuteJavaScript("arguments[0].scrollIntoView(true);", element);
        }

        public void ScroolJSTillTheEnd()
        {
            Driver.ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight);");
        }
    }
}
