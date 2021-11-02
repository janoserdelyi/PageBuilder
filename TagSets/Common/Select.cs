using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class Select : ControllerBase, IOptionParent
	{

		public Select (
			IController parentController = null
		) {
			this.Id = null;
			this.Name = null;
			this.parentController = parentController;
		}

		public Select (
			string id,
			IController parentController = null
		) {
			this.Id = id;
			this.Name = id;
			this.parentController = parentController;
		}

		public Select (
			string id,
			string name,
			IController parentController = null
		) {
			this.Id = id;
			this.Name = name;
			this.parentController = parentController;
		}

		public string Multiple { get; set; }
		// How many available options should be displayed when this element is rendered?
		public string Size { get; set; }

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

			if (this.Name == null) {
				//throw new System.ArgumentNullException("attribute 'name' missing on Select. Id '" + Id + "'");
				this.Name = Id;
			}

			StringBuilder sb = new StringBuilder ();

			sb.Append ("\n".PadRight (depth, '\t')).Append ($"<select name=\"{this.Name}\" id=\"{Id}\"");

			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "accesskey", this.Accesskey);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "tabindex", this.Tabindex);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "multiple", this.Multiple);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "size", this.Size);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "disabled", this.Disabled);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "required", this.Required);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "readonly", this.Readonly);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "hidden", this.Hidden);

			sb.Append (prepareEventHandlers (this.Name));
			sb.Append (prepareStyles (this.Name));

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

			//results.Append("<option>" + name + " {" + _value.ToString() + "}</option>");

			// Print this field to our output writer
			output.Write (sb.ToString ());

			// Store this tag itself
			// 2017-10-06 dear lord what am i looking at. this can be rewritten
			ContextValues["com.janoserdelyi.PageBuilder.TagSets.Common.SELECT"] = this;

			//this.Value = ControlUtils.GetContextValue (ContextValues, this.Name);

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
			output.Write ("\n".PadRight (depth, '\t') + "</select>");
		}

		protected string matchList = "";
		protected string[] match = null;
		//protected string saveBody = null;
		protected IController parentController { get; set; } = null;
	}
}