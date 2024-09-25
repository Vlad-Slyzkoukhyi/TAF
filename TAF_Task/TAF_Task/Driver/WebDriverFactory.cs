using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("download.default_directory", @"$(Build.ArtifactStagingDirectory)\Downloads");
                    chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
                    chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
                    if (settings.Headless)
                        chromeOptions.AddArguments("--headless");
                    if (settings.MaximizeWindow)
                        chromeOptions.AddArgument("--start-maximized");

                    return new ChromeDriver(chromeOptions);

                case BrowserType.Firefox:
                    var firefoxOptions = new FirefoxOptions();
                    if (settings.Headless)
                        firefoxOptions.AddArgument("-headless");
                    if (settings.MaximizeWindow)
                        firefoxOptions.AddArgument("--window-size=1920,1080");

                    return new FirefoxDriver(firefoxOptions);

                case BrowserType.Edge:
                    var edgeOptions = new EdgeOptions();
                    if (settings.Headless)
                        edgeOptions.AddArguments("--headless");
                    if (settings.MaximizeWindow)
                        edgeOptions.AddArgument("--start-maximized");

                    return new EdgeDriver(edgeOptions);

                default:
                    throw new ArgumentException("Unsupported browser type or configuration.");
            }
        }
    }

    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }
}
