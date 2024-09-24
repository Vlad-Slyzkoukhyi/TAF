using Microsoft.Extensions.Configuration;
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

        public CareersPage(IWebDriver driver, IConfiguration configuration) : base(driver, configuration)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "new_form_job_search-keyword")]
        private readonly IWebElement? _inputCareersField;

        [FindsBy(How = How.ClassName, Using = "recruiting-search__location")]
        private readonly IWebElement? _listFieldLocation;

        [FindsBy(How = How.XPath, Using = "//li[@title='All Locations']")]
        private readonly IWebElement? _dropDownListLocation;

        [FindsBy(How = How.CssSelector, Using = ".recruiting-search__filter-label-23")]
        private readonly IWebElement? _remoteCheckBoxElement;

        [FindsBy(How = How.Id, Using = "id-93414a92-598f-316d-b965-9eb0dfefa42d-remote")]
        private readonly IWebElement? _remoteCheckBoxElementState;

        [FindsBy(How = How.CssSelector, Using = "button[type='submit'].job-search-button-transparent-23")]
        private readonly IWebElement? _findButton;

        [FindsBy(How = How.XPath, Using = "//li[@title='Date']")]
        private readonly IWebElement? _sortByDateButton;

        [FindsBy(How = How.XPath, Using = "//a[text()='View More']")]
        private readonly IWebElement? _viewMoreButton;

        [FindsBy(How = How.XPath, Using = "//a[text()='View and apply']")]
        private readonly IList<IWebElement>? _jobOpportunities;

        [FindsBy(How = How.Id, Using = "main")]
        private readonly IWebElement? _mainElementText;

        private readonly By _searchPage = By.ClassName("search-result__list");

        public void SendKeysInInputCareerKeywordField(string programingLanguage)
        {
            _inputCareersField?.Clear();
            _inputCareersField?.SendKeys(programingLanguage);
        }

        public void ChoiceLocation(string location)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_listFieldLocation));
            _listFieldLocation?.Click();
            Wait.Until(ExpectedConditions.ElementToBeClickable(Driver.FindElement(By.XPath($"//li[@title='{location}']"))));
            _dropDownListLocation?.Click();
        }

        public void ClickRemoteCheckBox()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_remoteCheckBoxElement));
            _remoteCheckBoxElement?.Click();
            Wait.Until(ExpectedConditions.ElementSelectionStateToBe(_remoteCheckBoxElementState, true));
            CheckRemoteCheckBoxIsSelected(_remoteCheckBoxElementState);
        }

        public void ClickFindButton()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(_findButton));
            _findButton?.Click();
        }

        public void ClickSortByDate()
        {
            ScroolJS(_sortByDateButton);
            Wait.Until(ExpectedConditions.ElementToBeClickable(_sortByDateButton));
            _sortByDateButton?.Click();
        }

        public void ClickViewMoreButton()
        {
            ScroolJSTillTheEnd();
            Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(_searchPage));
            ScroolJS(_viewMoreButton);
            Wait.Until(ExpectedConditions.ElementToBeClickable(_viewMoreButton));
            _viewMoreButton?.Click();
        }

        public void ClickViewAndApplyAtTheLastElement()
        {            
            CheckListNotEmpty(_jobOpportunities);
            IWebElement jobOpportunity = _jobOpportunities[_jobOpportunities.Count - 1];
            Wait.Until(ExpectedConditions.ElementToBeClickable(jobOpportunity));
            jobOpportunity?.Click();
        }

        public void CheckRemoteCheckBoxIsSelected(IWebElement? element)
        {
            Assert.That(element?.Selected, Is.True);
        }

        public void CheckListNotEmpty(IList<IWebElement>? elements)
        {
            Assert.That(elements, Is.Not.Empty);
        }

        public void CheckTheLastElementContainProgramingLanguage(string programingLanguage)
        {
            string mainElementText = _mainElementText.Text;
            Assert.That(mainElementText.Contains(programingLanguage), $"The page does not contain the keyword {programingLanguage}.");
        }
    }
}
