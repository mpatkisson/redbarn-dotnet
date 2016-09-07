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
        private Dictionary<string, ResigView> _viewCache = new Dictionary<string, ResigView>();

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

        }

        protected ResigView GetOrCreateView(ControllerContext context, string path)
        {
            ResigView view = null;
            if (_viewCache.ContainsKey(path))
            {
                view = _viewCache[path];
            }
            else
            {
                string htmlPath = context.HttpContext.Server.MapPath(path);
                view = new ResigView(htmlPath);
                _viewCache[path] = view;
            }
            return view;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return GetOrCreateView(controllerContext, partialPath);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return GetOrCreateView(controllerContext, viewPath);
        }

    }
}
