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

        public ResigQuery Repeat(IEnumerable items, Action<object, object> cb)
        {
            if (items != null && Length > 0)
            {
                IElement ele = Elements[0];
                foreach (var item in items)
                {
                    IElement clone = ele.Clone() as IElement;
                    var match = new ResigQuery(Context);
                    match.Elements.Add(clone);
                    cb(item, match);
                    ele.Parent.AppendChild(clone);
                }
                ele.Remove();
            }
            return this;
        }

    }
}
