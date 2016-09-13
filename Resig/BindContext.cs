using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Resig
{
    public class BindContext
    {
        public ViewContext View { get; private set; }
        public object Model { get; private set; }
        public ViewDataDictionary ViewData { get; private set; }
        public HttpRequestBase Request { get; private set; }
        public IPrincipal User { get; private set; }

        public BindContext(ViewContext viewContext)
        {
            View = viewContext;
            Model = viewContext.ViewData.Model;
            ViewData = viewContext.ViewData;
            Request = viewContext.HttpContext.Request;
            User = viewContext.HttpContext.User;
        }

    }
}
