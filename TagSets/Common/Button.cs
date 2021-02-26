using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Button : BaseInput
{
	public Button () {
		
	}
	
	//this may spread and be used more than here. it is from Label originally
	public string OverrideContent {
		get { return overrideContent; }
		set { overrideContent = value; }
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
	 	// Create an appropriate "input" element based on our parameters
	 	StringBuilder sb = new StringBuilder();
	 	sb.Append("\n".PadRight(depth, '\t')).Append("<button ");
	 	if (Id != null && Name == null) {
			this.Name = Id;
		}
	 	
	 	if (Id != null) {
	 		sb.Append("id=\"").Append(Id).Append("\"");
	 	}
	 	
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "name", this.Name);
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "type", type);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accesskey", this.Accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", this.Tabindex);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "disabled", this.Disabled);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "required", this.Required);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "readonly", this.Readonly);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "hidden", this.Hidden);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "value", this.Value);
		
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
		
		sb.Append(">");
		
		output.Write(sb.ToString());
		
		if (overrideContent == null || (overrideContent != null && overrideContent.ToLower() != "true")) {
			RenderChildren(output, depth+1);
		}
		
		output.Write("\n".PadRight(depth, '\t') + "</button>");
	}
	
	protected string overrideContent = null;
}
}