using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_Task.PageObjectsModel
{
    internal class InsightsPage : BasePage
    {
        private string? _activeArticleName;
        private string? _articleName;
        public InsightsPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public InsightsPage(IWebDriver driver, IConfiguration configuration) : base(driver, configuration)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "slider__right-arrow")]
        private IWebElement? _swipeRightButton;

        [FindsBy(How = How.CssSelector, Using = ".owl-item.active .text-ui-23")]
        private IWebElement? _activeArticleElement;

        [FindsBy(How = How.CssSelector, Using = ".owl-item.active .custom-link")]
        private IWebElement? _readMoreActiveArticle;

        [FindsBy(How = How.ClassName, Using = "font-size-80-33")]
        private IWebElement? _articleNameElement;

        public void ClickSwipeRightButtonWithWait1000()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_swipeRightButton));
            _swipeRightButton?.Click();
            Thread.Sleep(1000);
        }

        public void GetActiveArticleName()
        {
            _activeArticleName = _activeArticleElement?.Text.Trim();
        }

        public void GetArticleName()
        {
            _articleName = _articleNameElement?.Text.Trim();
        }

        public void ClickReadMoreActiveArticle()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_readMoreActiveArticle));
            _readMoreActiveArticle?.Click();
        }  

        public void CheckArcticleName()
        {
            if (_activeArticleName != null && _articleName != null)
            {
                Assert.That(_activeArticleName.Equals(_articleName), "Articles are different");
            }
        }
    }
}