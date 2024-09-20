using System.Drawing;
using System.Drawing.Imaging;
using OpenQA.Selenium;

namespace TAF_Task.ScreenShots
{
    internal static class ScreenshotTaker
    {
        public static string ScreenShotPath => @"E:\Study\EPAM\TAF\TAF_Task\TAF_Task\Screenshots\TestFailScreenshots\";
        internal static void TakeScreenshot(IWebDriver? driver, string? testName, string? folderPath)
        {
            if (!Directory.Exists(folderPath) && folderPath != null)
            {
                Directory.CreateDirectory(folderPath);
            }

            string screenFileName =
                $"{testName} {DateTime.Now:dd,MM,yyyy_H,mm,ss}.{ImageFormat.Jpeg.ToString().ToLowerInvariant()}";

            string screenPath = Path.Combine(ScreenShotPath, screenFileName);

            using (Image screenshot = Image.FromStream(new MemoryStream(((ITakesScreenshot)driver).GetScreenshot().AsByteArray)))
            {
                screenshot?.Save(screenPath);
            }
        }
    }
}
