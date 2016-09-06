using Jint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Resig
{
    public class ResigView : IView
    {
        public String HtmlPath { get; private set; }
        
        public String ScriptPath
        {
            get
            {
                return HtmlPath + ".js";
            }
        }

        public Engine ScriptEngine { get; private set; }

        public ResigView(string htmlPath, Engine scriptEngine)
        {
            HtmlPath = htmlPath;
            ScriptEngine = scriptEngine;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            using (var reader = new StreamReader(HtmlPath))
            {
                writer.Write(reader.ReadToEnd());
                writer.Flush();
            }
        }
    }
}
