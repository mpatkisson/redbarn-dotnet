using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Resig
{
    public class ResigViewEngine : VirtualPathProviderViewEngine
    {

        public Engine ScriptEngine { get; private set; }

        public ResigViewEngine()
        {
            AreaViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/Resig/{1}/{0}.html",
                "~/Areas/{2}/Views/Shared/Resig/{0}.html"
            };

            AreaMasterLocationFormats = new[] {
                "~/Areas/{2}/Views/Resig/{1}/{0}.html",
                "~/Areas/{2}/Views/Shared/Resig/{0}.html"
            };

            AreaPartialViewLocationFormats = new[] {
                "~/Areas/{2}/Views/Resig/{1}/{0}.html",
                "~/Areas/{2}/Views/Shared/Resig/{0}.html"
            };


            ViewLocationFormats = new[] {
                "~/Views/Resig/{1}/{0}.html",
                "~/Views/Shared/Resig/{0}.html"
            };

            MasterLocationFormats = new[] {
                "~/Views/Resig/{1}/{0}.html",
                "~/Views/Shared/Resig/{0}.html"
            };

            PartialViewLocationFormats = new[] {
                "~/Views/Resig/{1}/{0}.html",
                "~/Views/Shared/Resig/{0}.html"
            };

            ScriptEngine = new Engine();
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            string path = controllerContext.HttpContext.Server.MapPath(partialPath);
            return new ResigView(path, ScriptEngine);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            string path = controllerContext.HttpContext.Server.MapPath(viewPath);
            return new ResigView(path, ScriptEngine);
        }
    }
}
