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

namespace Resig
{
    public class DomScriptEngine : Engine
    {

        #region [Fields]

        private IHtmlDocument _document;
        private Selector _domSelector;
        private Action<string> _consoleLogAction = new Action<string>(Log);

        #endregion

        #region [Properties]

        public string Html { get; private set; }

        public IHtmlDocument Document
        {
            get
            {
                if (_document == null)
                {
                    var parser = new HtmlParser();
                    _document = parser.Parse(Html);
                }
                return _document;
            }
        }

        public Selector DomSelector
        {
            get
            {
                if (_domSelector == null)
                {
                    _domSelector = new Selector(Document);
                }
                return _domSelector;
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

        public DomScriptEngine(string html)
            : base()
        {
            Configure(html);
        }

        public DomScriptEngine(string html, Action<Options> options)
            : base(options)
        {
            Configure(html);
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

        #endregion

        #region [Helper Methods]

        private void Configure(string html)
        {
            Html = html;
            SetValue("document", Document);
            SetValue("_selector", DomSelector);
            LoadConsole();
            LoadScriptResource("selector");
            LoadScriptResource("lodash");
        }

        private static void Log(object value)
        {
            Debug.WriteLine(value);
        }

        #endregion

    }
}
