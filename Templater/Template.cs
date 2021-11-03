using System;
using System.Xml;

namespace com.janoserdelyi.PageBuilder.Templater
{
	public class Template
	{
		internal Template (
			string templatePath,
			string templateName,
			bool verbose = true
		) {
			this.TemplatePath = templatePath;
			this.TemplateName = templateName;
			this.Verbose = verbose;
		}

		public static Template Load (
			string templatePath,
			string templateName,
			bool verbose = true,
			bool prependDoctype = true
		) {
			lock (new object ()) {
				Template pageTemplate = new Template (
					templatePath,
					templateName,
					verbose
				);

				//load 'er up!
				pageTemplate.loadXmlDocument ();
				pageTemplate.parseXmlDocument (prependDoctype);

				pageTemplate.LoadDate = DateTime.Now;

				return pageTemplate;
			}
		}

		public static Template LoadSnippet (
			string templatePath,
			string templateName,
			bool verbose = true
		) {
			return Load (templatePath, templateName, verbose: verbose, prependDoctype: false);
		}

		public void AddContextValues (
			System.Collections.Generic.IDictionary<string, object> ctx
		) {
			parser.RootControl.MergeNewContextValues (ctx);
		}

		public void RenderControls (
			System.IO.TextWriter output
		) {
			if (parser != null) {
				foreach (IController child in parser.RootControl.GetChildren ()) {
					child.Render (output, 0);
				}
			}
		}

		/*
		public RootControl NodeTree {
			get { 
				if (parser == null) {
					return null;
				}
				return parser.RootControl;
			}
		}
		*/


		#region private methods
		private void loadXmlDocument () {
			if (TemplatePath == null) {
				throw new Exception ("Error, no template path specified.");
			}

			this.template = new XmlDocument ();

			//injecting a new resolver which can resolve local files much better
			template.XmlResolver = new com.janoserdelyi.PageBuilder.Templater.CustomXmlResolver ();

			try {
				template.Load (System.IO.Path.Combine (TemplatePath, TemplateName));
			} catch (System.ObjectDisposedException oops) {
				//i got this once because a file was in a closed state and it puked out
				throw new System.ObjectDisposedException ("Conflict opening '" + TemplatePath + TemplateName + "'" + oops.ToString ());
			} catch (System.IO.FileNotFoundException) {
				throw new System.IO.FileNotFoundException ("Error, unable to locate template at '" + TemplatePath + TemplateName + "'");
			} catch (Exception oops) {
				throw new Exception ("Error, '" + oops.ToString () + "'");
			}
		}

		private void parseXmlDocument (

		) {
			parseXmlDocument (true);
		}

		private void parseXmlDocument (
			bool prependDoctype
		) {
			if (template == null) {
				throw new System.NullReferenceException ("no template found to parse");
			}

			parser = new TagParser () {
				Template = template,
				RootControl = new RootControl (),
				Verbose = this.Verbose
			};

			parser.Parse (prependDoctype);
		}
		#endregion


		public string TemplatePath { get; private set; }
		public string TemplateName { get; private set; }
		public bool Verbose { get; set; } = true;
		public DateTime LoadDate { get; private set; }

		private System.Xml.XmlDocument template;
		private TagParser parser;

	}
}