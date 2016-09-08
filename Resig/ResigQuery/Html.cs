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

        public string Html()
        {
            var value = String.Empty;
            if (Length > 0)
            {
                value = Elements[0].InnerHtml;
            }
            return value;
        }

        public ResigQuery Html(string html)
        {
            foreach (var element in Elements)
            {
                element.InnerHtml = html;
            }
            return this;
        }

    }
}
