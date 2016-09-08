using System;
using System.IO;
using System.Web.Mvc;

namespace Resig
{
    public class ResigView : IView
    {
        public ResigViewScriptEngine ScriptEngine { get; private set; }

        public ResigView(string htmlPath)
        {
            ScriptEngine = new ResigViewScriptEngine(htmlPath);
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            string html = ScriptEngine.Bind(viewContext);
            writer.Write(html);
            writer.Flush();
        }
    }
}
