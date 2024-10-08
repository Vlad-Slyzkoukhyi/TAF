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
                    string basePath = Environment.GetEnvironmentVariable("BUILD_ARTIFACTSTAGINGDIRECTORY") ?? Directory.GetCurrentDirectory();
                    string downloadPath = Path.Combine(basePath, "Downloads");
                    Directory.CreateDirectory(downloadPath);
                    options.AddUserProfilePreference("download.default_directory", downloadPath);
                    options.AddUserProfilePreference("download.default_directory", configuration["AppSettings:DownloadDirectory"]);
                    options.AddUserProfilePreference("download.prompt_for_download", false);
                    options.AddUserProfilePreference("disable-popup-blocking", "true");
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
