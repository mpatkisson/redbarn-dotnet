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

        public ResigQuery Append(string htmlString)
        {
            foreach(var element in Elements)
            {
                IElement created = Context.CreateElement("template");
                created.InnerHtml = htmlString;
                if (created.HasChildNodes)
                {
                    element.Append(created.ChildNodes.ToArray());
                }
            }
            return this;
        }

        public ResigQuery Append(ResigQuery query)
        {
            foreach (var element in query)
            {
                Append(element.OuterHtml);
            }
            return this;
        }

        // Covers Element and Text
        public ResigQuery Append(INode node)
        {
            foreach (var element in Elements)
            {
                element.Append(node);
            }
            return this;
        }

        public ResigQuery Append(Func<string> func)
        {
            string html = func();
            return Append(html);
        }

    }
}
