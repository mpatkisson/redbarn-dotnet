using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resig
{
    public class DomHelper
    {
        public IElement[] ToArray(IHtmlCollection<IElement> elements)
        {
            return elements.ToArray();
        }
    }
}
