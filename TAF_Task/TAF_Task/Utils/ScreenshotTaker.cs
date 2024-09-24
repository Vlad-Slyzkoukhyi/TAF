using System.Drawing;
using System.Drawing.Imaging;
using OpenQA.Selenium;
using System.IO;
using System;

namespace TAF_Task.ScreenShots
{
    public static class ScreenshotTaker
    {
        public static void TakeScreenshot(IWebDriver driver, string testName, string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string sanitizedTestName = SanitizeFilename(testName);
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                string screenFileName = $"{sanitizedTestName}_{timestamp}.jpeg";
                string screenPath = Path.Combine(folderPath, screenFileName);

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenPath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to capture screenshot.", ex);
            }
        }

        private static string SanitizeFilename(string filename)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var cleanFilename = new string(filename.Select(ch => invalidChars.Contains(ch) ? '_' : ch).ToArray());
            return cleanFilename;
        }
    }
}
