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

namespace TAF_Task.PageObjectsModel
{
    public abstract class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        protected IConfiguration? Configuration;

        [FindsBy(How = How.Id, Using = "onetrust-accept-btn-handler")]
        private readonly IWebElement? _acceptCookie;

        protected BasePage(IWebDriver driver) 
        {
            Driver = driver;
            PageFactory.InitElements(Driver, this);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        protected BasePage(IWebDriver driver, IConfiguration? configuration)
        {
            Driver = driver;
            Configuration = configuration;
            PageFactory.InitElements(Driver, this);
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void AcceptCookieButton()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_acceptCookie));            
            _acceptCookie?.Click();
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
