using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_Task.Utils
{
    public class WebDriverSettings
    {
        public string? BrowserType { get; set; }
        public bool Headless { get; set; }
        public bool MaximizeWindow { get; set; }
    }
}
