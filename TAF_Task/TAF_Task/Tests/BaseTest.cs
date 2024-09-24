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
        protected ILog Log => LogManager.GetLogger(this.GetType());

        [OneTimeSetUp]
        public void InitializeOnce()
        {
            XmlConfigurator.Configure(new FileInfo("Log4net.config"));
        }

        [SetUp]
        public void SetUp()
        {
            Driver = WebDriverFactory.CreateDriver();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Driver.Manage().Window.Maximize();
            Log.Info("Browser start");
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                try
                {
                    var outcome = TestContext.CurrentContext.Result.Outcome; 
                    if (!outcome.Equals(ResultState.Success)) 
                    {
                        string screenshotsDirectory = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");
                        ScreenshotTaker.TakeScreenshot(Driver, TestContext.CurrentContext.Test.Name, screenshotsDirectory);
                    }
                }
                catch (Exception ex)
                {                   
                    Log.Error("Error taking screenshot or during TearDown", ex);
                }
                finally
                {
                    Log.Info("Closing Browser");
                    Driver.Dispose();
                    Driver.Quit();
                }
            }
        }

        public void SaveScreenshot(string? screenshotName, string? folderPath)
        {
            try
            {
                Log.Info("Generating of screenshot started.");
                ScreenshotTaker.TakeScreenshot(Driver, screenshotName, folderPath ?? Path.Combine(Environment.CurrentDirectory, "Screenshots"));
                Log.Info("Generating of screenshot finished.");
            }
            catch (Exception ex)
            {
                Log.Info($"Failed to capture screenshot. Exception message: {ex.Message}");
                throw;
            }
        }

        public AppSettings? GetAppSettings()
        {
            IConfigurationRoot? configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration?.GetSection("AppSettings").Get<AppSettings>();
        }
    }
}
