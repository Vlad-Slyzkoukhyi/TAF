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
        private Assertions _assertions;

        [SetUp]
        public void LocalSetUp()
        {            
            _homePage = new HomePage(Driver);
            _assertions = new Assertions();
        }

        //Check are search result contain specific words
        [Test]
        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void TestSearchFunctionality(string requestWord)
        {
            _homePage.AcceptCookieIfPresent();
            _homePage.ClickOnMagnifierIcon()
                .SendRequestWordAtSearchField(requestWord)
                .ClickFindButtonMainPage();
            _assertions.AssertSearchResultsContainKeyword(_homePage.GetSearchResults(), requestWord);
        }
    }
}
