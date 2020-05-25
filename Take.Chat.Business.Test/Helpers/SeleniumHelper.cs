using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Take.Chat.Business.Test.Helpers
{
    public static class SeleniumHelper
    {
        public static IWebDriver CreateWebDriver(string browserName, Type type)
        {
            string driverDirectory = Path.GetDirectoryName(type.Assembly.Location) ?? ".";
            bool isDebuggerAttached = System.Diagnostics.Debugger.IsAttached;

            return browserName.ToLowerInvariant() switch
            {
                "chrome" => CreateChromeDriver(driverDirectory, isDebuggerAttached),
                _ => throw new NotSupportedException($"The browser '{browserName}' is not supported."),
            };
        }

        private static IWebDriver CreateChromeDriver(string driverDirectory,bool isDebuggerAttached)
        {
            var options = new ChromeOptions();

            if (!isDebuggerAttached)
            {
                options.AddArgument("--headless");
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                options.AddArgument("--no-sandbox");
            }

            return new ChromeDriver(driverDirectory, options);
        }
    }
}
