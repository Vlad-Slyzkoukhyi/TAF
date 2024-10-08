using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_Task.Utils;

namespace TAF_Task.Tests
{
    public class Assertions
    {
        public void AssertRemoteCheckBoxIsSelected(IWebElement element)
        {
            Assert.That(element.Selected, Is.True);
        }
        public void CheckListNotEmpty(IList<IWebElement> elements)
        {
            Assert.That(elements, Is.Not.Empty);
        }

        public void AssertTheLastElementContainProgramingLanguage(string expectedProgramingLanguage, string actualContentText)
        {
            Assert.That(actualContentText, Does.Contain(expectedProgramingLanguage), $"The page does not contain the keyword {expectedProgramingLanguage}.");
        }

        public void AssertSearchResultsContainKeyword(IList<IWebElement> webElementsList, string requestWord)
        {
            bool allLinksIsValid = webElementsList.All(link => requestWord.Any(keyword => link.Text.Contains(keyword)));
            Assert.That(allLinksIsValid, Is.True);
        }

        public void AssertFileDownloaded(string pathToFile)
        {
            Assert.That(File.Exists(pathToFile), Is.True, "Expected file was not found.");
        }

        public void AssertArticleNamesAreEqual(string activeArticleName, string openArticleName)
        {
            Assert.That(activeArticleName, Is.EqualTo(openArticleName), "Articles are different");
        }
    }
}
