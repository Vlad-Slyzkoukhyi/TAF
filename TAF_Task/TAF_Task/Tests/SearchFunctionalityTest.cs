using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_Task.PageObjectsModel;

namespace TAF_Task.Tests
{
    [TestFixture]
    public class SearchFunctionalityTest : BaseTest
    {
        private HomePage _homePage;
        private string _baseUrl;

        [SetUp]
        public void LocalSetUp()
        {
            _homePage = new HomePage(Driver);
            var appSettings = GetAppSettings();
            _baseUrl = appSettings.BaseUrl;

            Driver.Navigate().GoToUrl(_baseUrl);
        }

        //Check are search result contain specific words
        [Test]
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void TestSearchFunctionality(string requestWord)
        {
            _homePage.AcceptCookieButton();
            _homePage.ClickMagnifierIcon();
            _homePage.SendRequestWordAtSearchField(requestWord);
            _homePage.ClickFindButtonMainPage();
            _homePage.CheckIsResultSearchContainRequestWord(requestWord);
        }
    }
}
