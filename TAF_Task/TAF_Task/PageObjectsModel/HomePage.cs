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
        
        [FindsBy(How = How.LinkText, Using = "Careers")]
        private readonly IWebElement _careersPage;

        [FindsBy(How = How.LinkText, Using = "About")]
        private readonly IWebElement _aboutPage;

        [FindsBy(How = How.LinkText, Using = "Insights")]
        private readonly IWebElement _insightsPage;

        [FindsBy(How = How.CssSelector, Using = ".search-icon.dark-icon.header-search__search-icon")]
        private readonly IWebElement _magnifierIcon;

        [FindsBy(How = How.Id, Using = "new_form_search")]
        private readonly IWebElement _searchField;

        [FindsBy(How = How.ClassName, Using = "bth-text-layer")]
        private readonly IWebElement _findButton;

        [FindsBy(How = How.ClassName, Using = "search-results__item")]
        private readonly IList<IWebElement> _searchRequestWordResult;
        
        public CareersPage NavigateToCareersPage()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_careersPage));
            Log.Info("Navigate to careers page");
            _careersPage.Click();
            return new CareersPage(Driver);
        }

        public AboutPage NavigateToAboutPage()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_aboutPage));
            Log.Info("Navigate to about page");
            _aboutPage.Click();
            return new AboutPage(Driver);
        }

        public InsightsPage NavigateToInsightsPage()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_insightsPage));
            Log.Info("Navigate to insight page");
            _insightsPage.Click();
            return new InsightsPage(Driver);
        }   
        public HomePage ClickOnMagnifierIcon()
        {
            Log.Info("Click magnifier icon");
            ClickElement(_magnifierIcon);
            return this;
        }

        public HomePage SendRequestWordAtSearchField(string requestWord)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_searchField));
            _searchField.Clear();
            Log.Info("Send neccesary keyword");
            _searchField.SendKeys(requestWord);
            return this;
        }

        public HomePage ClickFindButtonMainPage()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_findButton));
            Log.Info("Click find button");
            _findButton.Click();
            return this;
        }

        public IList<IWebElement> GetSearchResults()
        {
            return _searchRequestWordResult;
        }
    }
}
