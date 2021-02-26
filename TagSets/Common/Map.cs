using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Map : ControllerBase
{
	#region constructors
	public Map () {
		
	}
	
	public Map (string id) {
		Id = id;
	}
	#endregion
	
	#region public properties
	public string Type {
		get { return type; }
		set { type = value; }
	}
	#endregion
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		RenderBeginTag(output, depth);
		RenderChildren(output, depth+1);
		RenderEndTag(output, depth);
	}
	
	private new void RenderBeginTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
	 	// Create an appropriate "input" element based on our parameters
	 	StringBuilder sb = new StringBuilder();
	 	sb.Append("\n".PadRight(depth, '\t')).Append("<map ");
	 	if (Id != null) {
	 		sb.Append(" id=\"").Append(propertyFallout(Id)).Append("\"");
	 	}
	 	
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "name", this.Name);
	 	
		sb.Append(prepareEventHandlers(this.Name));
		sb.Append(prepareStyles(this.Name));
		
		sb.Append(">");
		output.Write(sb.ToString());
	}
	
	private new void RenderEndTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		output.Write("\n".PadRight(depth, '\t') + "</map>");
	}
	
	#region private declarations
	private string type;
	#endregion
}
}
