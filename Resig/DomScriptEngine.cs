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
        private IHtmlDocument _document;
        private Selector _domSelector;

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

        public DomScriptEngine(string html)
        {
            Html = html;
            Configure();
        }

        private void Configure()
        {
            var engine = new Engine(ops => ops.DebugMode());
            engine.Step += ScriptEngine_Step;
            engine.SetValue("log", new Action<object>(Log));
            engine.SetValue("document", Document);
            engine.SetValue("_selector", DomSelector);
            string source = string.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Resig.Scripts.selector.js"))
            using (var reader = new StreamReader(stream))
            {
                source = reader.ReadToEnd();
            }
            engine.Execute(source);

            using (var stream = assembly.GetManifestResourceStream("Resig.Scripts.lodash.js"))
            using (var reader = new StreamReader(stream))
            {
                source = reader.ReadToEnd();
            }
            engine.Execute(source);
        }

        private Jint.Runtime.Debugger.StepMode ScriptEngine_Step(object sender, Jint.Runtime.Debugger.DebugInformation e)
        {
            if (e.CurrentStatement.Location != null)
            {
                Debug.WriteLine("{0}: Line {1}, Char {2}", e.CurrentStatement.ToString(), e.CurrentStatement.Location.Start.Line, e.CurrentStatement.Location.Start.Column);
            }
            return StepMode.Over;
        }

        private void Log(object value)
        {
            Debug.WriteLine(value);
        }
    }
}
