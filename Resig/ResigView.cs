﻿using AngleSharp.Dom;
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
    public class ResigView : IView
    {
        private string _html;
        private IHtmlDocument _document;
        private Selector _domSelector;

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

        public Engine ScriptEngine { get; private set; }

        public ResigView(string htmlPath)
        {
            HtmlPath = htmlPath;
            SetupScriptEngine();
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            ScriptEngine.Execute(Script);
            ScriptEngine.Invoke("bind", viewContext.ViewData.Model, viewContext.ViewData);
            writer.Write(Document.DocumentElement.OuterHtml);
            writer.Flush();
        }

        private void SetupScriptEngine()
        {
            ScriptEngine = new Engine(ops => ops.DebugMode());
            ScriptEngine.Step += ScriptEngine_Step;
            ScriptEngine.SetValue("log", new Action<object>(Log));
            ScriptEngine.SetValue("_document", Document);
            ScriptEngine.SetValue("_selector", DomSelector);
            string source = String.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Resig.Scripts.selector.js"))
            using (var reader = new StreamReader(stream))
            {
                source = reader.ReadToEnd();
            }
            ScriptEngine.Execute(source);

            using (var stream = assembly.GetManifestResourceStream("Resig.Scripts.lodash.js"))
            using (var reader = new StreamReader(stream))
            {
                source = reader.ReadToEnd();
            }
            ScriptEngine.Execute(source);
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
