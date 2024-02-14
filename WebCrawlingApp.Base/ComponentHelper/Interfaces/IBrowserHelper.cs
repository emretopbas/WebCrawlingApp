using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlingApp.Base.ComponentHelper.Interfaces
{
    internal interface IBrowserHelper
    {
        void MoveForward();

        void MoveBackward();

        void BrowserMaximise();

        void BrowserMinimise();

        void BrowserRefresh();

        void Navigate(string url);

        string GetBrowserTitle();

        string GetBrowserUrl();

        void SwitchToWindow(int index = 0);

        void SwitchToParent();

        void SwitchToFrame(IWebElement frameElement);

        IWebElement FindXpath(string xpath);

        void NewTab();

        void CloseTab();
    }
}
