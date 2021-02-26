using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class OptGroup : ControllerBase
{
	#region constructors
	public OptGroup () {
		//this.type = "optgroup";
	}
	
	public OptGroup (string id) {
		Id = id;
		//this.type = "optgroup";
	}
	#endregion
	
	#region public properties
	public string Label {
		get { return label; }
		set { label = value; }
	}
	#endregion
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		
		sb.Append("\n".PadRight(depth, '\t')).Append("<optgroup");
		
	 	if (Id != null && Name == null) {
			this.Name = Id;
		}
	 	
	 	if (Id != null) {
	 		sb.Append(" id=\"").Append(propertyFallout(Id)).Append("\"");
	 	}
	 	
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "label", label);
		
		sb.Append(prepareEventHandlers(this.Name));
		sb.Append(prepareStyles(this.Name));
		
		sb.Append(" />");
		
		output.Write(sb.ToString());
		RenderChildren(output, depth+1);
		output.Write("\n".PadRight(depth, '\t') + "</optgroup>");
	}
	
	#region private declarations
	private string label;
	#endregion
}
}