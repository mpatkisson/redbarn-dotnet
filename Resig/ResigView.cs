using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Resig
{
    public class ResigView : IView
    {
        public string HtmlPath { get; private set; }

        public string ScriptPath { get; private set; }

        public ResigViewScriptEngine ScriptEngine { get; private set; }

        public ResigView(string htmlPath)
        {
            HtmlPath = htmlPath;
            ScriptPath = Path.ChangeExtension(htmlPath, "js");
            ScriptEngine = new ResigViewScriptEngine(ScriptPath);
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            string html = GetHtmlFromPath();
            html = IntegrateHtmlWithLayout(html);
            html = ReplaceAttributes(html);
            html = ScriptEngine.Bind(html, viewContext.ViewData.Model, viewContext.ViewData);
            writer.Write(html);
            writer.Flush();
        }

        private string GetHtmlFromPath(string path)
        {
            string html = string.Empty;
            using (var reader = new StreamReader(path))
            {
                html = reader.ReadToEnd();
            }
            return html;
        }

        private string GetHtmlFromPath()
        {
            return GetHtmlFromPath(HtmlPath);
        }

        private string IntegrateHtmlWithLayout(string html)
        {
            string integrated = html;

            // Get the original
            var parser = new HtmlParser();
            IHtmlDocument original = parser.Parse(html);

            // Get the layout
            string virtualLayoutPath = original.FirstElementChild.GetAttribute("data-layout");
            if (!String.IsNullOrEmpty(virtualLayoutPath))
            {
                string layoutPath = HttpContext.Current.Server.MapPath(virtualLayoutPath);
                string layoutHtml = GetHtmlFromPath(layoutPath);
                IHtmlDocument layout = parser.Parse(layoutHtml);
                foreach (var template in layout.QuerySelectorAll("[id]"))
                {
                    string id = template.Id;
                    IElement content = original.GetElementById(id);
                    if (content != null)
                    {
                        template.InnerHtml = content.InnerHtml;
                    }
                }
                integrated = layout.DocumentElement.OuterHtml;
            }

            return integrated;
        }

        private string ReplaceAttributes(string html)
        {
            // Get the original
            var parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(html);
            foreach (var element in doc.QuerySelectorAll("[data-resig-href]"))
            {
                string href = VirtualPathUtility.ToAbsolute(element.GetAttribute("data-resig-href"));
                element.SetAttribute("href", href);
                element.RemoveAttribute("data-resig-href");
            }
            foreach (var element in doc.QuerySelectorAll("[data-resig-src]"))
            {
                string href = VirtualPathUtility.ToAbsolute(element.GetAttribute("data-resig-src"));
                element.SetAttribute("src", href);
                element.RemoveAttribute("data-resig-src");
            }
            return doc.DocumentElement.OuterHtml;
        }
    }
}
