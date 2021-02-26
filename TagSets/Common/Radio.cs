using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Radio : ControllerBase
{
	#region constructors
	public Radio () {
		this.Name = "com.janoserdelyi.PageBuilder.TagSets.Common.RADIO";
		this.type = "radio";
	}
	
	public Radio (string id) {
		Id = id;
		if (this.Name == null) {
			this.Name = id;
		}
		this.type = "radio";
	}
	#endregion
	
	#region public properties
	public string Type {
		get { return type; }
		set { type = value; }
	}
	
	public string Checked {
		get { return _checked; }
		set { _checked = value; }
	}
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
	 	StringBuilder sb = new StringBuilder();
	 	sb.Append("\n".PadRight(depth, '\t')).Append("<input type=\"");
	 	sb.Append(type); //yes, this will always be "checkbox". i need Type property exposed for some consistent interfacing
	 	sb.Append("\"");
		
		sb.Append(" name=\"").Append(propertyFallout(this.Name)).Append("\" id=\"").Append(propertyFallout(Id)).Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accesskey", this.Accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", this.Tabindex);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "disabled", this.Disabled);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "required", this.Required);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "readonly", this.Readonly);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "hidden", this.Hidden);
		
		sb.Append(" value=\"");
		
		if (this.Value == null) {
			sb.Append("on");
		} else {
			sb.Append(this.Value);
		}
		sb.Append("\"");
		
		if ((this.Value != null && this.Value.Equals(currentValue)) || (!string.IsNullOrEmpty(_checked)) ) {
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
		
		sb.Append(" />");
		output.Write(sb.ToString());
	}
	#endregion
	
	#region private declarations
	private string type;
	private string _checked;
	#endregion
}
}