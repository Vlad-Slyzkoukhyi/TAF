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
        private HomePage? _homePage;
        private InsightsPage? _insightPage;
        private string? _baseUrl;

        [SetUp]
        public void LocalSetUp()
        {
            _homePage = new HomePage(Driver);
            _insightPage = new InsightsPage(Driver);
            AppSettings? appSettings = GetAppSettings();
            _baseUrl = appSettings?.BaseUrl;

            Driver.Navigate().GoToUrl(_baseUrl);
        }

        //Check is article name on insight page equal article name when read article page
        [Test]        
        public void CheckArticleName()
        {
            _homePage?.AcceptCookieButton();
            _homePage?.ClickInsightsPage();
            _insightPage?.ClickSwipeRightButtonWithWait1000();
            _insightPage?.ClickSwipeRightButtonWithWait1000();
            _insightPage?.GetActiveArticleName();
            _insightPage?.ClickReadMoreActiveArticle();
            _insightPage?.GetArticleName();
            _insightPage?.CheckArcticleName();
        }
    }
}
