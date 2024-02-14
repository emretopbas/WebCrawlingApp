using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlingApp.Base
{
    public static class Driver
    {
        private static WebDriverWait _browserWait;
        private static IWebDriver _browser;

        public static IWebDriver Browser
        {
            get
            {
                if (_browser == null) { throw new NullReferenceException(); }
                return _browser;
            }
            set { _browser = value; }
        }

        public static WebDriverWait BrowserWait
        {
            get
            {
                if (_browserWait == null || _browser == null) { throw new NullReferenceException(); }
                return _browserWait;
            }
            set { _browserWait = value; }
        }

        public static void StartBrowser(BrowserTypes browserType, int defaultTimeOut = 30, object? browserOptions = null)
        {
            switch (browserType)
            {
                case BrowserTypes.Firefox:
                    if (browserOptions != null)
                        Browser = new FirefoxDriver((FirefoxOptions)browserOptions);
                    else
                        Browser = new FirefoxDriver();
                    break;

                case BrowserTypes.FirefoxHeadless:
                    ((FirefoxOptions)browserOptions).AddArguments("--headless");
                    Browser = new FirefoxDriver((FirefoxOptions)browserOptions);
                    break;

                case BrowserTypes.InternetExplorer:
                    if (browserOptions != null)
                        Browser = new InternetExplorerDriver((InternetExplorerOptions)browserOptions);
                    else
                        Browser = new InternetExplorerDriver();
                    break;

                case BrowserTypes.Chrome:
                    if (browserOptions != null)
                        Browser = new ChromeDriver((ChromeOptions)browserOptions);
                    else
                        Browser = new ChromeDriver();
                    break;

                case BrowserTypes.ChromeHeadless:
                    if (browserOptions != null)
                    {
                        ((ChromeOptions)browserOptions).AddArgument("--headless");
                        ((ChromeOptions)browserOptions).AddArgument("disable-gpu");
                    }
                    else
                    {
                        browserOptions = new ChromeOptions();
                        ((ChromeOptions)browserOptions).AddArgument("--headless");
                        ((ChromeOptions)browserOptions).AddArgument("disable-gpu");
                    }

                    Browser = new ChromeDriver((ChromeOptions)browserOptions);
                    break;

            }
            BrowserWait = new WebDriverWait(Browser, TimeSpan.FromSeconds(defaultTimeOut));
        }

        public static void StopBrowser()
        {
            Browser.Quit();
            Browser.Dispose();
            BrowserWait = null;
        }
    }
}
