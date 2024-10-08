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
        private Assertions _assertions;

        [SetUp]
        public void LocalSetUp()
        {            
            _homePage = new HomePage(Driver);
            _assertions = new Assertions();
        }

        //Check are latest job contain specific language and location
        [Test]
        [TestCase("C#", "All Locations")]
        [TestCase("Java", "All Locations")]
        [TestCase("JavaScript", "All Locations")]
        [TestCase("Python", "All Locations")]
        public void TestCheckLatestJob(string programingLanguage, string location)
        {
            _homePage.AcceptCookieIfPresent();
            CareersPage _careersPage = _homePage.NavigateToCareersPage()
                .SearchJobByKeyword(programingLanguage)
                .SearchJobByLocation(location)
                .ClickRemoteCheckBox();
            _assertions.AssertRemoteCheckBoxIsSelected(_careersPage.GetCheckBoxElement());
            _careersPage.ClickFindButton()
                .ClickSortByDate()
                .ClickViewMoreButton();
            _assertions.CheckListNotEmpty(_careersPage.GetJobOpportunitiesList());
            _careersPage.ClickViewAndApplyAtTheLastElement();
            _assertions.AssertTheLastElementContainProgramingLanguage(programingLanguage, _careersPage.GetMainElementText());
        }
    }
}
