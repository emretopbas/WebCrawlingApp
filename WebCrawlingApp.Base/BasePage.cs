using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using WebCrawlingApp.Base.ComponentHelper;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using Newtonsoft.Json;
using OpenQA.Selenium.Firefox;

namespace WebCrawlingApp.Base
{
    public class BasePage
    {
        public Helpers Helper = new Helpers();
        private static IWebElement _webElement;

        public BasePage(IWebDriver webDriver)
        {
            Driver.Browser = webDriver;
        }


        public static IWebElement WaitTillElementExist(string locator, ElementLocator elementLocatorType = ElementLocator.Xpath, int TimeOutForFindingElement = 10)
        {
            var wait = new WebDriverWait(Driver.Browser, TimeSpan.FromSeconds(TimeOutForFindingElement));

            if (elementLocatorType == ElementLocator.Xpath)
            {
                _webElement = wait.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
            }
            else if (elementLocatorType == ElementLocator.PartialLinkText)
            {
                _webElement = wait.Until(ExpectedConditions.ElementExists(By.PartialLinkText(locator)));
            }
            else if (elementLocatorType == ElementLocator.Name)
            {
                _webElement = wait.Until(ExpectedConditions.ElementExists(By.Name(locator)));
            }
            else if (elementLocatorType == ElementLocator.LinkText)
            {
                _webElement = wait.Until(ExpectedConditions.ElementExists(By.LinkText(locator)));
            }
            else if (elementLocatorType == ElementLocator.ID)
            {
                _webElement = wait.Until(ExpectedConditions.ElementExists(By.Id(locator)));
            }
            else if (elementLocatorType == ElementLocator.CssSelector)
            {
                _webElement = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(locator)));
            }
            else if (elementLocatorType == ElementLocator.TagName)
            {
                _webElement = wait.Until(ExpectedConditions.ElementExists(By.TagName(locator)));
            }
            else if (elementLocatorType == ElementLocator.ClassName)
            {
                _webElement = wait.Until(ExpectedConditions.ElementExists(By.ClassName(locator)));
            }

            return _webElement;
        }

        public static IWebElement WaitTillElementDisplayed(string locator, ElementLocator elementLocatorType = ElementLocator.Xpath, int TimeOutForFindingElement = 10)
        {
            var wait = new WebDriverWait(Driver.Browser, TimeSpan.FromSeconds(TimeOutForFindingElement));

            if (elementLocatorType == ElementLocator.Xpath)
            {
                _webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
            }
            else if (elementLocatorType == ElementLocator.PartialLinkText)
            {
                _webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.PartialLinkText(locator)));
            }
            else if (elementLocatorType == ElementLocator.Name)
            {
                _webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Name(locator)));
            }
            else if (elementLocatorType == ElementLocator.LinkText)
            {
                _webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(locator)));
            }
            else if (elementLocatorType == ElementLocator.ID)
            {
                _webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id(locator)));
            }
            else if (elementLocatorType == ElementLocator.CssSelector)
            {
                _webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(locator)));
            }
            else if (elementLocatorType == ElementLocator.TagName)
            {
                _webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName(locator)));
            }
            else if (elementLocatorType == ElementLocator.ClassName)
            {
                _webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(locator)));
            }

            return _webElement;
        }

        public class Helpers
        {
            public Browser BrowserHelper = new Browser();
        }

        public static void setUpBrowser()
        {
            var config = new ConfigurationBuilder().AddJsonFile("Appconfig.json").Build();

            string browserName = config["Browser"];
            string proxyOption = config["RotatingProxy"];

            if(proxyOption != null) {
                List<ProxyList> proxylist = JsonConvert.DeserializeObject<List<ProxyList>>(config["ProxyList"]);

                foreach (var proxy in proxylist)
                {
                    if (browserName.Equals("Chrome"))
                        Driver.StartBrowser(BrowserTypes.Chrome, 30, GetChromeProxyOptions(proxy));
                    else if (browserName.Equals("ChromeHeadless"))
                        Driver.StartBrowser(BrowserTypes.ChromeHeadless, 30, GetChromeHeadlessProxyOptions(proxy));
                    else if (browserName.Equals("Firefox"))
                        Driver.StartBrowser(BrowserTypes.Firefox, 30, GetFireFoxProxyOptions(proxy));
                    else if (browserName.Equals("FirefoxHeadless"))
                        Driver.StartBrowser(BrowserTypes.FirefoxHeadless, 30, GetFireFoxHeadlessProxyOptions(proxy));
                }
            }
            if (browserName.Equals("Chrome"))
                Driver.StartBrowser(BrowserTypes.Chrome, 30);
            else if (browserName.Equals("ChromeHeadless"))
                Driver.StartBrowser(BrowserTypes.ChromeHeadless, 30);
            else if (browserName.Equals("Firefox"))
                Driver.StartBrowser(BrowserTypes.Firefox, 30);
            else if (browserName.Equals("FirefoxHeadless"))
                Driver.StartBrowser(BrowserTypes.FirefoxHeadless, 30);

        }

        private static FirefoxOptions GetFireFoxHeadlessProxyOptions(ProxyList? proxy)
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();

            if (proxy != null)
            {
                firefoxOptions.AddArgument("--proxy-server=" + "https://" + proxy.address + ":" + proxy.port);
            }
            firefoxOptions.AddArgument("--headless");
            return firefoxOptions;
        }

        private static FirefoxOptions GetFireFoxProxyOptions(ProxyList? proxy)
        {
            FirefoxOptions firefoxOptions = new FirefoxOptions();

            if (proxy != null)
            {
                firefoxOptions.AddArgument("--proxy-server=" + "https://" + proxy.address + ":" + proxy.port);
            }
            firefoxOptions.AddArgument("window-size=1920,1080");
            return firefoxOptions;
        }

        private static ChromeOptions GetChromeHeadlessProxyOptions(ProxyList? proxy)
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            if (proxy != null)
            {
                chromeOptions.AddArgument("--proxy-server=" + "https://" + proxy.address + ":" + proxy.port);
            }
            chromeOptions.AddArgument("--headless");
            return chromeOptions;
        }

        private static ChromeOptions GetChromeProxyOptions(ProxyList? proxy)
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            if (proxy != null)
            {
                chromeOptions.AddArgument("--proxy-server=" + "https://" + proxy.address + ":" + proxy.port);
            }
            chromeOptions.AddArgument("window-size=1920,1080");
            return chromeOptions;
        }
    }

    internal class ProxyList
    {
        public string address { get; set; }
        public int port { get; set; }
    }
}
