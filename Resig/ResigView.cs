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
            html = ScriptEngine.Bind(html, new BindContext(viewContext));
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
            var parser = new HtmlParser();
            IHtmlDocument doc = parser.Parse(html);
            ReplaceAttributes(doc, "href");
            ReplaceAttributes(doc, "src");
            return doc.DocumentElement.OuterHtml;
        }

        private void ReplaceAttributes(IHtmlDocument doc, string suffix)
        {
            string attribute = String.Format("data-view-{0}", suffix);
            string selector = String.Format("[{0}]", attribute);
            foreach (var element in doc.QuerySelectorAll(selector))
            {
                string value = VirtualPathUtility.ToAbsolute(element.GetAttribute(attribute));
                element.SetAttribute(suffix, value);
                element.RemoveAttribute(attribute);
            }
        }
    }
}
