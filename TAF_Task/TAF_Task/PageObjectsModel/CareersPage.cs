using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_Task.PageObjectsModel
{
    internal class CareersPage : BasePage
    {
        public CareersPage(IWebDriver driver) : base (driver)
        {
            PageFactory.InitElements(driver, this);
        }
        
        [FindsBy(How = How.ClassName, Using = "recruiting-search__location")]
        private readonly IWebElement _listFieldLocation;

        [FindsBy(How = How.XPath, Using = "//li[@title='All Locations']")]
        private readonly IWebElement _dropDownListLocation;

        [FindsBy(How = How.CssSelector, Using = ".recruiting-search__filter-label-23")]
        private readonly IWebElement _remoteCheckBox;

        [FindsBy(How = How.CssSelector, Using = "button[type='submit'].job-search-button-transparent-23")]
        private readonly IWebElement _findButton;

        private readonly By _jobField = By.Id("new_form_job_search-keyword");
        private readonly By _sortByDateButton = By.XPath("//li[@title='Date']");
        private readonly By _searchPage = By.ClassName("search-result__list");
        private readonly By _viewMoreButton = By.XPath("//a[text()='View More']");
        private readonly By _isRemoteCheckBoxTrue = By.Id("id-93414a92-598f-316d-b965-9eb0dfefa42d-remote");
        private readonly By _jobOpportunitiesList = By.XPath("//a[text()='View and apply']");
        private readonly By _currentJobElement = By.Id("main");

        public CareersPage SearchJobByKeyword(string keyword)
        {
            Log.Info("Send keyword");
            SendKeys(_jobField, keyword);
            return this;
        }

        public CareersPage SearchJobByLocation(string location)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_listFieldLocation));
            Log.Info("Click location list");
            _listFieldLocation?.Click();
            Wait.Until(ExpectedConditions.ElementToBeClickable(Driver.FindElement(By.XPath($"//li[@title='{location}']"))));
            Log.Info("Click necessary element");
            _dropDownListLocation?.Click();
            return this;
        }

        public CareersPage ClickRemoteCheckBox()
        {
            Log.Info("Click remote check box");
            ClickElement(_remoteCheckBox);
            return this;
        }

        public CareersPage ClickFindButton()
        {
            Log.Info("Click find button");
            ClickElement(_findButton);
            return this;
        }

        public CareersPage ClickSortByDate()
        {
            ScroolJS(_sortByDateButton);
            Wait.Until(ExpectedConditions.ElementToBeClickable(_sortByDateButton));
            IWebElement sortByDateElement = Driver.FindElement(_sortByDateButton);
            Log.Info("Click sort by date");
            ClickElementJS(sortByDateElement);
            return this;
        }

        public CareersPage ClickViewMoreButton()
        {
            ScrollJSTillTheEnd();
            Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(_searchPage));
            ScroolJS(_viewMoreButton);
            Wait.Until(ExpectedConditions.ElementToBeClickable(_viewMoreButton));
            IWebElement viewMoreButtonElement = Driver.FindElement(_viewMoreButton);
            Log.Info("Click view more button");
            ClickElementJS(viewMoreButtonElement);
            return this;
        }

        public void ClickViewAndApplyAtTheLastElement()
        {
            IList<IWebElement> jobOpportunities = Driver.FindElements(_jobOpportunitiesList);
            IWebElement jobOpportunity = jobOpportunities[jobOpportunities.Count - 1];
            Log.Info("Click last searched job element");
            ClickElementJS(jobOpportunity);
        }

        public IWebElement GetCheckBoxElement()
        {
            return GetElement(_isRemoteCheckBoxTrue);
        }

        public IList<IWebElement> GetJobOpportunitiesList()
        {
            return GetElements(_jobOpportunitiesList);
        }

        public string GetMainElementText()
        {
            return GetElement(_currentJobElement).Text;
        }
    }
}
