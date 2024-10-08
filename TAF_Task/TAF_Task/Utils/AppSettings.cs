using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_Task.Utils
{
    public class AppSettings
    {
        public string? BaseUrl { get; set; }
        public string? DownloadDirectory { get; set; }
        public bool PromptForDownload { get; set; }
        public bool DisablePopupBlocking { get; set; }
    }
}
