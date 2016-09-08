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
    public partial class ResigQuery : IEnumerable<IElement>
    {

        #region [Fields]

        private List<IElement> _elements;

        #endregion

        #region [Properties]

        public IHtmlDocument Context { get; set; }

        public object PrevObject { get; set; }

        public int Length
        {
            get
            {
                return Elements.Count;
            }
        }

        public string Selector { get; set; }

        public IElement this[int index]
        {
            get
            {
                return Elements[index];
            }
            set
            {
                Elements.Add(value);
            }
        }

        private List<IElement> Elements
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

        #endregion

        #region [.ctors]

        public ResigQuery(IHtmlDocument context)
        {
            Context = context;
        }

        public ResigQuery(string html)
            : this(new HtmlParser().Parse(html))
        {

        }

        #endregion

        #region [Public Methods]

        public IEnumerator<IElement> GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        #endregion

    }
}
