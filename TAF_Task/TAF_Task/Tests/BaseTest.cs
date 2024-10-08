using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_Task.Driver;
using TAF_Task.ScreenShots;
using log4net.Config;
using log4net;
using Microsoft.Extensions.Configuration;
using TAF_Task.Utils;

namespace TAF_Task.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver? Driver;
        protected IConfiguration _configuration;
        protected ILog Log => LogManager.GetLogger(this.GetType());

        [OneTimeSetUp]
        public void InitializeOnce()
        {
            XmlConfigurator.Configure(new FileInfo("Log4net.config"));
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true);
            _configuration = builder.Build();
        }

        [SetUp]
        public void SetUp()
        {
            Driver = WebDriverFactory.CreateDriver();
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Manage().Window.Maximize();
            Log.Info("Browser start");
            Driver.Navigate().GoToUrl(_configuration["AppSettings:BaseUrl"]);
            Log.Info("Url open");
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
                {
                    SaveScreenshot(TestContext.CurrentContext.Test.MethodName,
                                   Path.Combine(TestContext.CurrentContext.TestDirectory,
                                                ScreenshotTaker.ScreenShotPath));
                }
            }
            finally
            {
                Log.Info("Browser closed");
                Driver?.Dispose();
                Driver?.Quit();
            }
        }

        public void SaveScreenshot(string? screenshotName, string? folderPath)
        {
            try
            {
                Log.Info("Generating of screenshot started.");
                ScreenshotTaker.TakeScreenshot(Driver, screenshotName, folderPath);
                Log.Info("Generating of screenshot finished.");
            }
            catch (Exception ex)
            {
                Log.Info($"Failed to capture screenshot. Exception message: {ex.Message}");
                throw;
            }
        }       
    }
}
