using System;
using System.Text;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class Datalist : ControllerBase, IOptionParent
	{
		public Datalist (
			IController parentController = null
		) {
			this.TagType = "datalist";
			this.parentController = parentController;
		}

		public Datalist (
			string id,
			IController parentController = null
		) {
			this.Id = id;
			this.TagType = "datalist";
			this.parentController = parentController;
		}

		public bool isMatched (
			string matchValue
		) {
			if (match == null || matchValue == null) {
				return false;
			}

			for (int i = 0; i < match.Length; i++) {
				matchList += match[i] + ",";
				if (matchValue.ToString () == match[i].ToString ()) {
					return true;
				}
			}
			return false;
		}

		public override void Render (
			System.IO.TextWriter output,
			int depth = 0
		) {

			if (Id == null) {
				throw new System.ArgumentNullException ("attribute 'id' missing on Select");
			}

			StringBuilder sb = new StringBuilder ();

			sb.Append ("\n".PadRight (depth, '\t')).Append ($"<datalist id=\"{Id}\"");

			ControlUtils.RenderAttribute (ContextValues, sb, Id, "name", this.Name);
			ControlUtils.RenderAttribute (ContextValues, sb, Id, "accesskey", this.Accesskey);
			ControlUtils.RenderAttribute (ContextValues, sb, Id, "tabindex", this.Tabindex);
			ControlUtils.RenderAttribute (ContextValues, sb, Id, "disabled", this.Disabled);
			ControlUtils.RenderAttribute (ContextValues, sb, Id, "required", this.Required);
			ControlUtils.RenderAttribute (ContextValues, sb, Id, "readonly", this.Readonly);
			ControlUtils.RenderAttribute (ContextValues, sb, Id, "hidden", this.Hidden);

			sb.Append (prepareEventHandlers (Id));
			sb.Append (prepareStyles (Id));

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

			sb.Append (">");

			output.Write (sb.ToString ());

			// Store this tag itself
			// 2017-10-06 dear lord what am i looking at. this can be rewritten
			ContextValues["com.janoserdelyi.PageBuilder.TagSets.Common.DATALIST"] = this;

			//this.Value = ControlUtils.GetContextValue (ContextValues, Id);

			if (string.IsNullOrEmpty (this.Value)) {
				this.Value = "";
			}

			if (this.Value.IndexOf (',') > -1) {
				string[] matches = this.Value.Split (',');
				match = new String[matches.Length];

				for (int i = 0; i < match.Length; i++) {
					match[i] = matches[i];
				}
			} else {
				match = new String[1];
				match[0] = this.Value;
			}

			RenderChildren (output, depth + 1);
			output.Write ("\n".PadRight (depth, '\t') + "</datalist>");
		}

		protected string matchList = "";
		protected string[] match = null;
		protected IController parentController { get; set; } = null;
	}
}