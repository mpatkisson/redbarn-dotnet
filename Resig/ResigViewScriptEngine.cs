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

        public String BindScriptPath { get; private set; }

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

        public ResigViewScriptEngine(string scriptPath)
            : base()
        {
            Configure(scriptPath);
        }

        public ResigViewScriptEngine(string scriptPath, Action<Options> options)
            : base(options)
        {
            Configure(scriptPath);
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

        public string Bind(string html, ViewContext context)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.Parse(html);
            SetValue("document", document);
            var query = new ResigQuery(document);
            SetValue("resigQuery", query);
            Execute(BindScript);
            Invoke("bind", context.ViewData.Model, context.ViewData, context);
            return document.DocumentElement.OuterHtml;
        }

        #endregion

        #region [Helper Methods]

        private void Configure(string scriptPath)
        {
            BindScriptPath = scriptPath;
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
