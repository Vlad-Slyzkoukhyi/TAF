using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TAF_Task.Tests;
using TAF_Task.Utils;

namespace TAF_Task.PageObjectsModel
{
    internal class AboutPage : BasePage
    {
        private DownloadHelper? _downloadHelper;

        private readonly IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true, true).Build();

        public AboutPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public AboutPage(IWebDriver driver, IConfiguration configuration) : base(driver, configuration)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='button__wrapper button--left']" +
            "//a[contains(@class, 'button-ui-23') and contains(@href, 'epam.com') and @download]")]
        private IWebElement? _downloadEPAMAtAGlance;

        public void ClickDownloadEPAMAtGlance()
        {
            ScroolJS(_downloadEPAMAtAGlance);
            Wait.Until(ExpectedConditions.ElementToBeClickable(_downloadEPAMAtAGlance));
            ClickJS(_downloadEPAMAtAGlance);
        }

        public void CheckIsSpecificFileExist(string filePath)
        {
            _downloadHelper = new DownloadHelper(configuration);
            string? downloadDirectory = Configuration["AppSettings:DownloadDirectory"];
            if (downloadDirectory != null)
            {
                _downloadHelper.WaitForFileDownload(Path.Combine(downloadDirectory, filePath));
                Assert.That(File.Exists(Path.Combine(downloadDirectory, filePath)));
            }
        }
    }
}
