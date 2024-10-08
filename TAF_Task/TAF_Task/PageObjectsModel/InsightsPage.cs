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
        public InsightsPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".owl-item.active .custom-link")]
        private readonly IWebElement _readMoreActiveArticle;

        private readonly By _swipeRightButton = By.ClassName("slider__right-arrow");
        private readonly By _activeArticleElement = By.CssSelector(".owl-item.active .text-ui-23");
        private readonly By _articleName = By.ClassName("font-size-80-33");

        public async Task<InsightsPage> SwipeCaruselRight()
        {
            Log.Info("Click swipe carusel right");
            await ClickElementWithDelayAfterClicking(_swipeRightButton);
            return this;
        }

        public void ClickActiveArticle()
        {
            Log.Info("Click read more");
            ClickElement(_readMoreActiveArticle);
        }

        public string GetActiveArticleName()
        {
            return GetText(_activeArticleElement);
        }

        public string GetOpenArticleName()
        {
            return GetText(_articleName);
        }
    }
}
