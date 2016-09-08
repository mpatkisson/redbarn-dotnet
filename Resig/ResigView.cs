using System;
using System.IO;
using System.Web.Mvc;

namespace Resig
{
    public class ResigView : IView
    {
        private string _html;

        public String HtmlPath { get; private set; }

        public String Html
        {
            get
            {
                if (String.IsNullOrEmpty(_html))
                {
                    using (var reader = new StreamReader(HtmlPath))
                    {
                        _html = reader.ReadToEnd();
                    }
                }
                return _html;
            }
        }
        
        public String ScriptPath
        {
            get
            {
                return Path.ChangeExtension(HtmlPath, "js");
            }
        }

        public String Script
        {
            get
            {
                var script = String.Empty;
                using (var reader = new StreamReader(ScriptPath))
                {
                    script = reader.ReadToEnd();
                }
                return script;
            }
        }

        public DomScriptEngine ScriptEngine { get; private set; }

        public ResigView(string htmlPath)
        {
            HtmlPath = htmlPath;
            ScriptEngine = new DomScriptEngine(Html);
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            ScriptEngine.Execute(Script);
            ScriptEngine.Invoke("bind", viewContext.ViewData.Model, viewContext.ViewData);
            writer.Write(ScriptEngine.Document.DocumentElement.OuterHtml);
            writer.Flush();
        }
    }
}
