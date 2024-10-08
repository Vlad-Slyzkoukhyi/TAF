using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_Task.PageObjectsModel;
using TAF_Task.Utils;

namespace TAF_Task.Tests
{
    [TestFixture]
    public class ArticleNameOnInsightPageTest : BaseTest
    {
        private HomePage _homePage;
        private Assertions _assertions;

        [SetUp]
        public void LocalSetUp()
        {            
            _homePage = new HomePage(Driver);
            _assertions = new Assertions();
        }

        //Check is article name on insight page equal article name when read article page
        [Test]
        public async Task TestCheckArticleName()
        {
            _homePage.AcceptCookieIfPresent();
            InsightsPage _insightsPage = _homePage.NavigateToInsightsPage();
            _insightsPage = await _insightsPage.SwipeCaruselRight();
            _insightsPage = await _insightsPage.SwipeCaruselRight();
            string activeArticle = _insightsPage.GetActiveArticleName();
            _insightsPage.ClickActiveArticle();
            _assertions.AssertArticleNamesAreEqual(activeArticle, _insightsPage.GetOpenArticleName());
        }
    }
}
