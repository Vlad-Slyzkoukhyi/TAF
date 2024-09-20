using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_Task.Utils
{
    public class DownloadHelper
    {
        private readonly string _downloadPath;
        private readonly int _timeoutInSeconds  = 60;

        public DownloadHelper(IConfiguration configuration)
        {
            _downloadPath = configuration["AppSettings:DownloadDirectory"];
            if (string.IsNullOrWhiteSpace(_downloadPath))
            {
                throw new InvalidOperationException("Download path is not configured.");
            }
        }

        public void WaitForFileDownload(string fileName)
        {
            string filePath = Path.Combine(_downloadPath, fileName);
            bool fileExists = SpinWaitFileExistence(filePath);

            if (!fileExists)
            {
                throw new Exception("File download failed or did not complete in time.");
            }
        }

        private bool SpinWaitFileExistence(string filePath)
        {
            int totalWaitTime = 0;
            long lastSize = -1;
            long unchangedSizeDuration = 0;

            while (totalWaitTime <= _timeoutInSeconds)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists && fileInfo.Length > 0)
                {
                    if (lastSize == fileInfo.Length)
                    {
                        unchangedSizeDuration += 1;
                    }
                    else
                    {
                        unchangedSizeDuration = 0;
                    }

                    lastSize = fileInfo.Length;

                    if (unchangedSizeDuration >= 3)
                    {
                        return true;
                    }
                }
                Thread.Sleep(1000);
                totalWaitTime += 1;
            }
            return false;
        }
    }
}
