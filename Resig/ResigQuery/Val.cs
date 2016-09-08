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

        public string Val()
        {
            var value = String.Empty;
            if (Length > 0)
            {
                value = Elements[0].GetAttribute("value");
            }
            return value;
        }

        public ResigQuery Val(string value)
        {
            foreach (var element in Elements)
            {
                element.SetAttribute("value", value);
            }
            return this;
        }

    }
}
