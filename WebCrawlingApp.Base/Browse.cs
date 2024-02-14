
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawlingApp.Base;

namespace WebCrawlingApp
{
    public class Browse 
    {
        public IWebElement _showcase;
        public IWebElement _headline;
        public IWebElement _price;
        public IWebDriver _driver;
        public LandingPage _landingPage;

        public void Run()
        {
            _landingPage = new LandingPage(_driver);
            DirectToLandingPage();
            FindandPrintShowcaseDiv("/html/body/div[2]/div[2]/div[3]/div/div[2]/div/div[1]");
            ReturnElementsFromShowCaseDiv("/html/body/div[2]/div[2]/div[3]/div/div[2]/div/div[1]");
        }

        public void DirectToLandingPage()
        {
            _landingPage.Helper.BrowserHelper.Navigate(_landingPage.LandingPageUrl);
        }

        public void FindandPrintShowcaseDiv(string xpathDiv)
        {
            _landingPage.ShowCase_WebElement(xpathDiv);
            _showcase = _landingPage.Helper.BrowserHelper.FindXpath(xpathDiv);
            Console.WriteLine(_showcase.Text);
        }

        public void ReturnElementsFromShowCaseDiv(string xpath)
        {
            List<string> strArr = new List<string>();
            List<decimal> intArr = new List<decimal>();
            //string[] strArr = null;

            IList<IWebElement> node = _showcase.FindElements(By.ClassName("content-container"));

            decimal averagePrice = 0;
            int counter = 1;

            foreach (IWebElement item in node)
            {

                var linkNode = item.FindElement(By.XPath("a"));
                var link = linkNode.GetAttribute("href");

                _landingPage.Helper.BrowserHelper.NewTab();
                _landingPage.Helper.BrowserHelper.SwitchToWindow(1);
                _landingPage.Helper.BrowserHelper.Navigate(link);

                _headline = _landingPage.Helper.BrowserHelper.FindXpath("//*[@class='product-name-container']");

                _price = _landingPage.Helper.BrowserHelper.FindXpath("//*[@class='product-price']");
                var priceNode = _price.GetAttribute("innerText");

                var x = ParsePriceString(priceNode);

                decimal price = decimal.Parse(x,NumberStyles.Currency);

                averagePrice += price;

                strArr.Add(_headline.Text);
                
                intArr.Add(price);

                Console.WriteLine(x);
                _landingPage.Helper.BrowserHelper.CloseTab();
                _landingPage.Helper.BrowserHelper.SwitchToParent();
            }

            string docPath =Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter sw = new StreamWriter(Path.Combine(docPath, "WebCrawlingData.txt")))
            {
                for (int i = 0;i<strArr.Count;i++)
                {
                    sw.WriteLine(i+".İlan Açıklama : "+ strArr[i]);
                    sw.WriteLine(i+".İlan Fiyat : " + intArr[i]+"\n");
                }
                sw.WriteLine("Ortalama Fiyat : " + averagePrice / node.Count);
                sw.Close();
            }


        }

        public string ParsePriceString(string value)
        {
            var charsToRemove = new string[] { "@", "T", "L", ";", " " };
            if (value.Contains("\r\n"))
            {
                //burada boyle bir kontrol olmasının sebebi indirimli fiyatla normal fiyatın aynı anda gelmesi ve güncel fiyatı almak istememiz
                string[] prices = value.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                value = prices[1];
            }
            foreach (var c in charsToRemove)
            {
                value = value.Replace(c, string.Empty);
            }
            
            return value;
        }
    }
}
