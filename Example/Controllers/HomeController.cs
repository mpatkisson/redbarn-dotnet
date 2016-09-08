using Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            FooModel model = new FooModel
            {
                Foo = "foo",
                Bar = "bar",
                Baz = "baz",
                Now = DateTime.Now
            };
            ViewBag.Bacon = "Bacon ipsum dolor amet pig meatball turducken";
            return View(model);
        }
    }
}