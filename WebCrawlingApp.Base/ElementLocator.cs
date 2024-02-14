using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlingApp.Base
{
    public enum ElementLocator
    {
        ID,
        Name,
        CssSelector,
        LinkText,
        PartialLinkText,
        Xpath,
        ClassName,
        TagName
    }
}
