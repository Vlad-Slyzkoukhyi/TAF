using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TAF_Task.Utils;

namespace TAF_Task.Driver
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

            var settings = configuration.GetSection("WebDriverSettings").Get<WebDriverSettings>();
            BrowserType browserType = Enum.Parse<BrowserType>(settings.BrowserType, true);

            switch (browserType)
            {
                case BrowserType.Chrome:
                    var options = new ChromeOptions();                    
                    if (settings.Headless)
                        options.AddArguments("--headless");
                    if (settings.MaximizeWindow)
                        options.AddArgument("--start-maximized");

                    return new ChromeDriver(options);
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                default:
                    throw new ArgumentException("Unsupported browser type or configuration.");
            }
        }
    }

    public enum BrowserType
    {
        Chrome,
        Firefox
    }
}
