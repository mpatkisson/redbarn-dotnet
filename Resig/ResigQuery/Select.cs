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

        #region [Public Methods]

        public ResigQuery Select(object value)
        {
            ResigQuery match = null;
            if (value is string)
            {
                match = Select(value as string);
            }
            else
            {
                match = Select(value as IElement);
            }
            return match;
        }
        
        #endregion

        #region [Helper Methods]

        private ResigQuery Select(string selector)
        {
            ResigQuery match = new ResigQuery(Context);
            if (selector.StartsWith("#"))
            {
                selector = selector.Remove(0, 1);
                IElement element = Context.GetElementById(selector);
                match.Elements.Add(element);
            }
            else if (selector.StartsWith("<"))
            {
                IElement element = Context.CreateElement("template");
                element.InnerHtml = selector;
                foreach (var child in element.Children)
                {
                    match.Elements.Add(child);
                }
                match.Elements.Add(element);
                selector = null;
            }
            else
            {
                IHtmlCollection<IElement> elements = Context.QuerySelectorAll(selector);
                match.Elements.AddRange(elements);
            }
            match.Selector = selector;
            return match;
        }

        private ResigQuery Select(IElement element)
        {
            ResigQuery match = new ResigQuery(Context);
            match.Elements.Add(element);
            return match;
        }

        #endregion

    }
}
