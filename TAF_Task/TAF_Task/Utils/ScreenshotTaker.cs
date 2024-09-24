using System.Drawing;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using System.IO;
using System;

namespace TAF_Task.ScreenShots
{
    public static class ScreenshotTaker
    {
        internal static void TakeScreenshot(IWebDriver driver, string testName, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string screenFileName =
                $"{testName} {DateTime.Now:dd,MM}.{ImageFormat.Jpeg.ToString().ToLowerInvariant()}";

            string screenPath = Path.Combine(TestContext.Parameters.Get("ScreenShotPath").ToString(), screenFileName);

            using (Image screenshot = Image.FromStream(new MemoryStream(((ITakesScreenshot)driver).GetScreenshot().AsByteArray)))
            {
                screenshot.Save(screenPath);
            }
        }
    }
}
