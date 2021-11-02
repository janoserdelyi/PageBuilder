using System;
using System.Text;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class BaseInput : ControllerBase
	{
		#region public properties
		public string Cols { get; set; }

		public string Minlength { get; set; }
		public string Maxlength { get; set; }
		public string Min { get; set; }
		public string Max { get; set; }
		public string List { get; set; }

		public string Rows { get; set; }

		public string Size { get; set; }

		public string Accept { get; set; }

		public string Type {
			get { return type; }
			set { type = value; }
		}
		// i don't recall what this was for
		public bool Redisplay { get; set; }

		public string Lang { get; set; }
		public string ContentEditable { get; set; }
		public string ContextMenu { get; set; }
		public string Dir { get; set; }

		public string Placeholder { get; set; }
		public string Pattern { get; set; }

		/*public string Disabled {get;set;}*/

		// added mostly to keep Apple from being obnoxious. i think that's an impossible task personally...
		public string Autocorrect { get; set; }
		public string Autocapitalize { get; set; }
		//
		public string Autocomplete { get; set; }

		public string Step { get; set; }
		// public string Multiple {get;set;} // for file input types
		#endregion

		#region private methods
		public override void Render (
			System.IO.TextWriter output,
			int depth = 0
		) {
			StringBuilder sb = new StringBuilder ();
			sb.Append ("\n".PadRight (depth, '\t'));
			sb.Append ($"<input type=\"{type}\"");

			//name and id. i need to come up with a cleaner standard process for this
			if (!string.IsNullOrEmpty (this.Name)) {
				sb.Append ($" name=\"{this.Name}\"");
			}

			if (!string.IsNullOrEmpty (this.Id) && !this.Id.StartsWith ("element_")) {
				sb.Append ($" id=\"{this.Id}\"");
			}

			// 2016-04-04. possibly dangerous. not sure yet
			if (string.IsNullOrEmpty (this.Name)) {
				this.Name = this.Id;
			}

			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "accesskey", this.Accesskey);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "accept", this.Accept);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "minlength", this.Minlength);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "maxlength", this.Maxlength);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "min", this.Min);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "max", this.Max);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "cols", this.Cols);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "rows", this.Rows);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "tabindex", this.Tabindex);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "lang", this.Lang);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "contenteditable", this.ContentEditable);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "contextmenu", this.ContextMenu);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "dir", this.Dir);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "step", this.Step);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "list", this.List);

			// some html5 attributes added on here
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "placeholder", this.Placeholder);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "pattern", this.Pattern);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "autocorrect", this.Autocorrect);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "autocapitalize", this.Autocapitalize);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "autocomplete", this.Autocomplete);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "disabled", this.Disabled);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "required", this.Required);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "readonly", this.Readonly);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "hidden", this.Hidden);

			//time to get value. again, i need a cleaner way to get this
			//2011-09-22. experimenting getting value by id instead of name
			string val = null;
			if (string.IsNullOrEmpty (this.Id)) {
				if (ContextValues.ContainsKey (this.Name) && ContextValues[this.Name] != null) {
					val = ContextValues[this.Name].ToString ();
				} else {
					val = null;
				}
			} else {
				if (ContextValues.ContainsKey (this.Id) && ContextValues[this.Id] != null) {
					val = ContextValues[this.Id].ToString ();
				} else {
					val = null;
				}
			}
			if (val == null) {
				val = this.Value;
			}

			if (!string.IsNullOrEmpty (val)) {
				val = val.Replace ("\"", "\\\"");
				sb.Append (" value=\"").Append (val).Append ("\"");
			}

			sb.Append (prepareEventHandlers (this.Name));
			sb.Append (prepareStyles (this.Name));
			sb.Append (prepareOther (this.Name));

			if (base.DataAttributes.Count > 0) {
				foreach (System.Collections.Generic.KeyValuePair<string, object> da in base.DataAttributes) {
					ControlUtils.RenderAttribute (ContextValues, sb, this.Id, da.Key, da.Value.ToString ());
				}
			}
			if (base.AriaAttributes.Count > 0) {
				foreach (System.Collections.Generic.KeyValuePair<string, object> da in base.AriaAttributes) {
					ControlUtils.RenderAttribute (ContextValues, sb, this.Id, da.Key, da.Value.ToString ());
				}
			}

			sb.Append (" />");

			output.Write (sb.ToString ());
		}
		#endregion


		protected string type;
	}
}