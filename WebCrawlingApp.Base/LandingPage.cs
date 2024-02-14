using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawlingApp.Base;

namespace WebCrawlingApp
{
    public class LandingPage : BasePage
    {
        public LandingPage(IWebDriver driver) : base(driver)
        {
            BasePage.setUpBrowser();
        }

        public string LandingPageUrl = "https://www.arabam.com";
        
        //Burası Click islemlerinde buton veya sayfanın yüklenmesinin tamamlanması için
        public IWebElement MainBody_WebElement => WaitTillElementDisplayed("/html/body", ElementLocator.Xpath, 10);

        public IWebElement ShowCase_WebElement(string xpath) => WaitTillElementDisplayed(xpath, ElementLocator.Xpath, 10);

        public IWebElement ShowcaseDetail_WebElement(string xpath) => WaitTillElementDisplayed(xpath, ElementLocator.Xpath, 10);

    }
}
