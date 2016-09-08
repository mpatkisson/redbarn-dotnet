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

        public ResigQuery Text(string text)
        {
            foreach (var element in Elements)
            {
                element.TextContent = text;
            }
            return this;
        }

    }
}
