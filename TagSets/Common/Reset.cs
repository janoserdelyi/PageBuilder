using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Reset : ControllerBase
{
	#region constructors
	public Reset () {
		this.type = "reset";
	}
	
	public Reset (string id) {
		Id = id;
		if (this.Name == null) {
			this.Name = id;
		}
		this.type = "reset";
	}
	#endregion
	
	#region public properties
	public string Type {
		get { return type; }
		set { type = value; }
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
	 	StringBuilder sb = new StringBuilder("<input type=\"");
	 	sb.Append(type); //yes, this will always be "checkbox". i need Type property exposed for some consistent interfacing
	 	sb.Append("\"");
		
		sb.Append("\" name=\"").Append(propertyFallout(this.Name)).Append("\" id=\"").Append(propertyFallout(Id)).Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "accesskey", this.Accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "tabindex", this.Tabindex);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "value", this.Value);
		
		sb.Append(prepareEventHandlers(this.Name));
		sb.Append(prepareStyles(this.Name));
		
		sb.Append(" />");
		output.Write(sb.ToString());
	}
	#endregion
	
	#region private declarations
	private string type;
	#endregion
}
}