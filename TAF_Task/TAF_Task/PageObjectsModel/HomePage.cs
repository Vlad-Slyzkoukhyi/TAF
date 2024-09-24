using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace TAF_Task.PageObjectsModel
{
    internal class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public HomePage(IWebDriver driver, IConfiguration configuration) : base(driver, configuration)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "onetrust-accept-btn-handler")]
        private readonly IWebElement? _acceptCookieButton;

        [FindsBy(How = How.XPath, Using = "//a[@class='top-navigation__item-link js-op'][normalize-space()='Careers']")]
        private readonly IWebElement? _careersPage;

        [FindsBy(How = How.CssSelector, Using = ".search-icon.dark-icon.header-search__search-icon")]
        private readonly IWebElement? _magnifierIcon;

        [FindsBy(How = How.Id, Using = "new_form_search")]
        private readonly IWebElement? _searchField;

        [FindsBy(How = How.ClassName, Using = "bth-text-layer")]
        private readonly IWebElement? _findButton;

        [FindsBy(How = How.ClassName, Using = "search-results__item")]
        private readonly IWebElement? _searchResult;

        [FindsBy(How = How.XPath, Using = "//a[@class='top-navigation__item-link js-op'][normalize-space()='About']")]
        private readonly IWebElement? _aboutPage;

        [FindsBy(How = How.XPath, Using = "//a[@class='top-navigation__item-link js-op'][normalize-space()='Insights']")]
        private readonly IWebElement? _insightsPage;

        [FindsBy(How = How.ClassName, Using = "search-results__item")]
        private readonly IList<IWebElement>? _searchRequestWordResult;

        private readonly By? _searchResults = By.ClassName("search-results__items");

        public void ClickCareersPage()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_careersPage));
            _careersPage?.Click();
            Log.Info("Careers page is open");
        }

        public void ClickMagnifierIcon()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_magnifierIcon));
            _magnifierIcon?.Click();
        }

        public void ClickAboutPage()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_aboutPage));
            _aboutPage?.Click();
            Log.Info("About page is open");
        }

        public void ClickInsightsPage()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_insightsPage));
            _insightsPage?.Click();
            Log.Info("Insights page is open");
        }   

        public void SendRequestWordAtSearchField(string requestWord)
        {
            _searchField?.Clear();
            _searchField?.SendKeys(requestWord);
        }

        public void ClickFindButtonMainPage()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_findButton));
            _findButton?.Click();
        }

        public void CheckIsResultSearchContainRequestWord(string requestWord)
        {
            Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(_searchResults));
            bool allLinksIsValid = _searchRequestWordResult.All(link => requestWord.Any(keyword => link.Text.Contains(keyword)));
            Assert.That(allLinksIsValid, Is.True);
        }
    }
}
