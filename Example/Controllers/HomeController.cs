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
                Now = DateTime.Now,
                Foos = new List<FooModel>
                {
                    new FooModel { Foo = "foo1", Bar = "bar1", Baz = "baz1" },
                    new FooModel { Foo = "foo2", Bar = "bar2", Baz = "baz2" },
                    new FooModel { Foo = "foo3", Bar = "bar3", Baz = "baz3" },
                    new FooModel { Foo = "foo4", Bar = "bar4", Baz = "baz4" }
                }
            };
            ViewBag.Bacon = "Bacon ipsum dolor amet pig meatball turducken";
            return View(model);
        }
    }
}