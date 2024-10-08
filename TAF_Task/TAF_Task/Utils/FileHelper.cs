using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_Task.Utils
{
    public static class FileHelper
    {
        public static async Task WaitForFileDownload(string fullPathToFile)
        {
            int timeoutInSeconds = 60;
            bool fileExists = false;
            int totalWaitTime = 0;

            while (!fileExists && totalWaitTime < timeoutInSeconds)
            {
                await Task.Delay(1000);  
                totalWaitTime += 1;

                try
                {
                    if (File.Exists(fullPathToFile))
                    {
                        FileInfo file = new FileInfo(fullPathToFile);
                        while (file.Length == 0)
                        {
                            await Task.Delay(1000);
                            totalWaitTime += 1;
                            if (totalWaitTime > timeoutInSeconds)
                            {
                                throw new TimeoutException("File download did not complete in the allotted time.");
                            }

                            file.Refresh(); 
                        }
                        fileExists = file.Length > 0;
                    }
                }
                catch (IOException e)
                {
                    int maxRetryCount = 3;
                    int retryCounter = 0;
                    while (retryCounter < maxRetryCount && !fileExists)
                    {
                        retryCounter++;
                        await Task.Delay(1000);
                        if (File.Exists(fullPathToFile) && new FileInfo(fullPathToFile).Length > 0)
                        {
                            fileExists = true;
                        }
                    }

                    if (!fileExists)
                    {
                        throw new IOException($"Unable to access the file after {retryCounter} retries.", e);
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    throw new UnauthorizedAccessException($"Access to the file {fullPathToFile} is denied: {e.Message}", e);
                }
            }

            if (!fileExists)
            {
                throw new FileNotFoundException("The file was not found on the disk after the waiting period.");
            }
        }        
    }
}
