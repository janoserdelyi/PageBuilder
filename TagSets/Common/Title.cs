using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Title : ControllerBase
{
	public Title () {
		
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		RenderBeginTag(output, depth);
		
		string titleValue = ControlUtils.GetContextValue(ContextValues, this.Id);
		
		if (string.IsNullOrEmpty(titleValue)) {
			titleValue = this.Value;
		}
		
		output.Write(titleValue);
		
		//RenderEndTag(output);
		output.Write("</title>");
	}
	
	private new void RenderBeginTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		//if (string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(Id)) {
		//	this.Name = Id;
		//}
		
		StringBuilder sb = new StringBuilder();
		
		sb.Append("\n".PadRight(depth, '\t')).Append("<title");
		//bother with name? does the spec even show a name on title as being allowed?
		
		if (!string.IsNullOrEmpty(Id) && !Id.StartsWith("element_")) {
			sb.Append(" id=\"").Append(Id).Append("\"");
		}
		if (!string.IsNullOrEmpty(this.Name)) {
			sb.Append(" name=\"").Append(this.Name).Append("\"");
		}
		
		sb.Append(">");
		
		output.Write(sb.ToString());
	}
}
}