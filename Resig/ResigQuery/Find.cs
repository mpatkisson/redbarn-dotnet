using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resig
{
    public partial class ResigQuery
    {

        public ResigQuery Find(string selector)
        {
            ResigQuery match = new ResigQuery(Context)
            {
                Selector = selector
            };
            foreach (var element in Elements)
            {
                IHtmlCollection<IElement> elements = element.QuerySelectorAll(selector);
                foreach (var ele in elements)
                {
                    match.Elements.Add(ele);
                }
            }
            return match;
        }

    }
}
