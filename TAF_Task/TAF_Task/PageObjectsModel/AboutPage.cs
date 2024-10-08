using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TAF_Task.PageObjectsModel
{
    internal class AboutPage : BasePage
    {        
        public AboutPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        private readonly By _downloadEPAMAtAGlance = By.XPath("//div[@class='button__wrapper button--left']" +
            "//a[contains(@class, 'button-ui-23') and contains(@href, 'epam.com') and @download]");

        public AboutPage ScrollIntoViewDownloadEPAMAtGlance()
        {
            ScroolJS(_downloadEPAMAtAGlance);
            return this;
        }

        public AboutPage ClickDownloadEPAMAtGlance()
        {
            Log.Info("Click button download");
            ClickElementJS(_downloadEPAMAtAGlance);
            return this;
        }
    }
}
