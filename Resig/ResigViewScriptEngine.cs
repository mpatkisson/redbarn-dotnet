using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Jint;
using Jint.Runtime.Debugger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Resig
{
    public class ResigViewScriptEngine : Engine
    {

        #region [Fields]

        private Action<string> _consoleLogAction = new Action<string>(Log);

        #endregion

        #region [Properties]

        public String HtmlPath { get; private set; }

        public String Html
        {
            get
            {
                string html = string.Empty;
                using (var reader = new StreamReader(HtmlPath))
                {
                    html = reader.ReadToEnd();
                }
                return html;
            }
        }

        public String BindScriptPath
        {
            get
            {
                return Path.ChangeExtension(HtmlPath, "js");
            }
        }

        public String BindScript
        {
            get
            {
                var script = string.Empty;
                using (var reader = new StreamReader(BindScriptPath))
                {
                    script = reader.ReadToEnd();
                }
                return script;
            }
        }

        public Action<string> ConsoleLogAction
        {
            get
            {
                return _consoleLogAction;
            }
            set
            {
                _consoleLogAction = value;
            }
        }

        #endregion

        #region [.ctors]

        public ResigViewScriptEngine(string htmlPath)
            : base()
        {
            Configure(htmlPath);
        }

        public ResigViewScriptEngine(string htmlPath, Action<Options> options)
            : base(options)
        {
            Configure(htmlPath);
        }

        #endregion

        #region [Public Methods]

        public void Load(string name)
        {
            name = name.ToLower();
            if (name == "console")
            {
                LoadConsole();
            } else
            {
                LoadScriptResource(name);
            }
        }

        public void LoadConsole()
        {
            SetValue("__env_log", ConsoleLogAction);
            Execute(@"var console = { log: __env_log };");
        }

        public void LoadScriptResource(string name)
        {
            string source = string.Empty;
            name = "Resig.Scripts." + name + ".js";
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(name))
            using (var reader = new StreamReader(stream))
            {
                source = reader.ReadToEnd();
            }
            Execute(source);
        }

        public string Bind(ViewContext context)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.Parse(Html);
            SetValue("document", document);
            var query = new ResigQuery(document);
            SetValue("resigQuery", query);
            Execute(BindScript);
            Invoke("bind", context.ViewData.Model, context.ViewData);
            return document.DocumentElement.OuterHtml;
        }

        #endregion

        #region [Helper Methods]

        private void Configure(string htmlPath)
        {
            HtmlPath = htmlPath;
            LoadConsole();
            LoadScriptResource("selector");
            LoadScriptResource("lodash");
            LoadScriptResource("moment");
        }

        private static void Log(object value)
        {
            Debug.WriteLine(value);
        }

        #endregion

    }
}
