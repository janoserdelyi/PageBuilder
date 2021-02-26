using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Checkbox : ControllerBase
{
	#region constructors
	public Checkbox () {
		this.Name = "com.janoserdelyi.PageBuilder.TagSets.Common.CHECKBOX";
		this.type = "checkbox";
	}
	
	public Checkbox (string id) {
		Id = id;
		if (Name == null) {
			this.Name = id;
		}
		this.type = "checkbox";
	}
	#endregion
	
	#region public properties
	public string Text {
		get { return text; }
		set { text = value; }
	}
	
	public string Type {
		get { return type; }
		set { type = value; }
	}
	
	public string Checked {get;set;}
	#endregion
	
	#region render
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		string currentValue = ControlUtils.GetContextValue(ContextValues, this.Name);
		if (currentValue == null) {
			currentValue = "";
		}
		
	 	// Create an appropriate "input" element based on our parameters
	 	StringBuilder sb = new StringBuilder("<input type=\"");
	 	sb.Append(type); //yes, this will always be "checkbox". i need Type property exposed for some consistent interfacing
	 	sb.Append("\"");
		
		sb.Append(" name=\"").Append(propertyFallout(this.Name)).Append("\" id=\"").Append(propertyFallout(Id)).Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accesskey", this.Accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", this.Tabindex);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "disabled", this.Disabled);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "required", this.Required);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "readonly", this.Readonly);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "hidden", this.Hidden);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "checked", this.Checked);
		
		sb.Append(" value=\"");
		
		if (this.Value == null) {
			sb.Append("on");
		} else {
			sb.Append(this.Value);
		}
		sb.Append("\"");
		
		bool isChecked = false;
		if (this.Value != null && this.Value.Equals(currentValue)) {
			sb.Append(" checked=\"checked\"");
			isChecked = true;
		}
		
		//the value may be a comma-delimited multi-value
		//this is because the name for a group of checkboxes may be the same (much like a radio button)
		if (this.Value != null && currentValue != null && currentValue.ToString().IndexOf(',') > -1) {
			string[] matches = currentValue.ToString().Split(',');
			for(int i=0; i<matches.Length; i++) {
				if (matches[i].Equals(this.Value)) {
					sb.Append(" checked=\"checked\"");
					isChecked = true;
					break;
				}
			}
		}
		
		if (!isChecked && !string.IsNullOrEmpty(this.Checked)) {
			sb.Append(" checked=\"checked\"");
		}
		
		sb.Append(prepareEventHandlers(this.Name));
		sb.Append(prepareStyles(this.Name));
		
		if (base.DataAttributes.Count > 0) {
			foreach (System.Collections.Generic.KeyValuePair<string, object> da in base.DataAttributes) {
				ControlUtils.RenderAttribute(ContextValues, sb, this.Id, da.Key, da.Value.ToString());
			}
		}
		if (base.AriaAttributes.Count > 0) {
			foreach (System.Collections.Generic.KeyValuePair<string, object> da in base.AriaAttributes) {
				ControlUtils.RenderAttribute(ContextValues, sb, this.Id, da.Key, da.Value.ToString());
			}
		}
		
		if (this.GetChildren() == null || this.GetChildren().Count == 0) {
			sb.Append(" />");
			output.Write(sb.ToString());
		} else {
			sb.Append(">");
			output.Write(sb.ToString());
			RenderChildren(output);
			output.Write("</input>");
		}
	}
	#endregion
	
	#region private declarations
	protected string text = null;
	private string type;
	#endregion
}
}