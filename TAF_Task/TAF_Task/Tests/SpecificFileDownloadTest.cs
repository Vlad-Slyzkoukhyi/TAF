using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_Task.PageObjectsModel;
using TAF_Task.Utils;

namespace TAF_Task.Tests
{
    public class SpecificFileDownloadTest : BaseTest
    {
        private HomePage _homePage;
        private AboutPage _aboutPage;

        [SetUp]
        public void LocalSetUp()
        {
            IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true, true).Build();
            

            _homePage = new HomePage(Driver);
            _aboutPage = new AboutPage(Driver, configuration);
            Driver.Navigate().GoToUrl(configuration["AppSettings:BaseUrl"]);
        }


        //Check is file with specific name are downloaded
        [Test]
        [TestCase("EPAM_Corporate_Overview_Q4_EOY.pdf")]
        public void TestCheckFileDownload(string fileName)
        {
            _homePage.AcceptCookieButton();
            _homePage.ClickMenuButton();
            _homePage.ClickAboutPage();
            _aboutPage.ClickDownloadEPAMAtGlance();
            _aboutPage.CheckIsSpecificFileExist(fileName);
        }       
    }
}
