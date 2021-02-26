using System;
using System.Text;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Hr : ControllerBase
{
	public Hr () {
		
	}
	
	[Obsolete("2017-10-05 - i wish to remove all parameterless constructors")]
	public Hr (string id) {
		Id = id;
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		// Create an appropriate "input" element based on our parameters
	 	StringBuilder sb = new StringBuilder();
		
	 	sb.Append("\n".PadRight(depth, '\t')).Append("<hr");
	 	if (!string.IsNullOrEmpty(Id)) {
			sb.Append(" id=\"").Append(Id).Append("\"");
		}
	 	
		sb.Append(prepareStyles(Id));
		
		sb.Append(" />");
		output.Write(sb.ToString());
	}
}
}