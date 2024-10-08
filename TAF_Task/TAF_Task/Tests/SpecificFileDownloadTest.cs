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
        private Assertions _assertions;
        
        [SetUp]
        public void LocalSetUp()
        {
            _homePage = new HomePage(Driver);
            _assertions = new Assertions();
        }


        //Check is file with specific name are downloaded
        [Test]
        [TestCase("EPAM_Corporate_Overview_Q4_EOY.pdf")]
        public async Task TestCheckFileDownload(string fileName)
        {
            _homePage.AcceptCookieIfPresent();
            _homePage.NavigateToAboutPage()
                .ScrollIntoViewDownloadEPAMAtGlance()
                .ClickDownloadEPAMAtGlance();

            string downloadDirectory = _configuration["AppSettings:DownloadDirectory"];
            string fullPathToFile = Path.Combine(downloadDirectory, fileName);

            await FileHelper.WaitForFileDownload(fullPathToFile);
            _assertions.AssertFileDownloaded(fullPathToFile);
        }       
    }
}
