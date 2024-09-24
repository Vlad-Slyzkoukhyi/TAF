using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_Task.PageObjectsModel;

namespace TAF_Task.Tests
{
    [TestFixture]
    public class LatestJobCorrecspondLanguageTest : BaseTest
    {
        private HomePage _homePage;
        private CareersPage _careersPage;
        private string _baseUrl;

        [SetUp]
        public void LocalSetUp()
        {
            _homePage = new HomePage(Driver);
            _careersPage = new CareersPage(Driver);
            var appSettings = GetAppSettings();
            _baseUrl = appSettings.BaseUrl;

            Driver.Navigate().GoToUrl(_baseUrl);
        }

        //Check are latest job contain specific language and location
        [Test]
        [TestCase("C#", "All Locations")]
        [TestCase("Java", "All Locations")]
        [TestCase("JavaScript", "All Locations")]
        [TestCase("Python", "All Locations")]
        public void CheckLatestJobTest(string programingLanguage, string location)
        {
            _homePage.AcceptCookieButton();
            _homePage.ClickMenuButton();
            _homePage.ClickCareersPage();
            _careersPage.SendKeysInInputCareerKeywordField(programingLanguage);
            _careersPage.ChoiceLocation(location);
            _careersPage.ClickRemoteCheckBox();
            _careersPage.ClickFindButton();
            _careersPage.ClickSortByDate();
            _careersPage.ClickViewMoreButton();
            _careersPage.ClickViewAndApplyAtTheLastElement();
            _careersPage.CheckTheLastElementContainProgramingLanguage(programingLanguage);
        }
    }
}
