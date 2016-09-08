using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resig
{
    public class Match
    {
        private List<IElement> _elements;

        public string Selector { get; set; }

        public List<IElement> ListOfElements
        {
            get
            {
                if (_elements == null)
                {
                    _elements = new List<IElement>();
                }
                return _elements;
            }
        } 

        public IElement[] Elements
        {
            get
            {
                return ListOfElements.ToArray();
            }
        }

        public int Length
        {
            get
            {
                return Elements.Length;
            }
        }

        public string Val()
        {
            var value = String.Empty;
            if (Length > 0)
            {
                value = Elements[0].GetAttribute("value");
            }
            return value;
        }

        public Match Val(string value)
        {
            foreach (var element in Elements)
            {
                element.SetAttribute("value", value);
            }
            return this;
        }

        public string Text()
        {
            var text = String.Empty;
            foreach (var element in Elements)
            {
                text += element.TextContent + " ";
            }
            text = text.Trim();
            return text;
        }

        public Match Text(string text)
        {
            foreach (var element in Elements)
            {
                element.TextContent = text;
            }
            return this;
        }

        public string Html()
        {
            var value = String.Empty;
            if (Length > 0)
            {
                value = Elements[0].InnerHtml;
            }
            return value;
        }

        public Match Html(string html)
        {
            foreach (var element in Elements)
            {
                element.InnerHtml = html;
            }
            return this;
        }

        public Match Find(string selector)
        {
            Match match = new Match
            {
                Selector = selector
            };
            foreach (var element in Elements)
            {
                IHtmlCollection<IElement> elements = element.QuerySelectorAll(selector);
                foreach (var ele in elements)
                {
                    match.ListOfElements.Add(ele);
                }
            }
            return match;
        }

        public Match Repeat(IEnumerable items, Action<object, object> cb)
        {
            if (items != null && Length > 0)
            {
                IElement ele = Elements[0];
                foreach (var item in items)
                {
                    IElement clone = ele.Clone() as IElement;
                    var match = new Match();
                    match.ListOfElements.Add(clone);
                    cb(item, match);
                    ele.Parent.AppendChild(clone);
                }
                ele.Remove();
            }
            return this;
        }
    }
}
