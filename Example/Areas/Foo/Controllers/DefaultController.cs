using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example.Areas.Foo.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Foo/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}