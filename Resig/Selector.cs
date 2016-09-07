using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resig
{
    public class Selector
    {
        private IHtmlDocument _document;

        public Selector(IHtmlDocument document)
        {
            _document = document;
        }

        public Match Query(object value)
        {
            Match match = new Match();
            if (value is string)
            {
                match = Query(value as string);
            }
            else
            {
                match = Query(value as IElement);
            }
            return match;
        }

        private Match Query(string selector)
        {
            Match match = new Match();
            if (selector.StartsWith("#"))
            {
                selector = selector.Remove(0, 1);
                IElement element = _document.GetElementById(selector);
                match.ListOfElements.Add(element);
            }
            else
            {
                IHtmlCollection<IElement> elements = _document.QuerySelectorAll(selector);
                match.ListOfElements.AddRange(elements);
            }
            match.Selector = selector;
            return match;
        }

        private Match Query(IElement element)
        {
            Match match = new Match();
            match.ListOfElements.Add(element);
            return match;
        }
    }
}
