using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Body : ControllerBase
{
	public Body() {
		
	}
	
	public string Onload {
		get { return onload; }
		set { onload = value; }
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		if (this.Name == null) { 
			this.Name = Id; 
		}
		
		depth = 1; // enforcing. i have some stylistic prefs here. i don't indent head and body inside html
		RenderBeginTag(output, depth);
		RenderChildren(output, depth+1);
		RenderEndTag(output, depth);
	}

	private new void RenderBeginTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder("\n<body");
		if (!string.IsNullOrEmpty(Id)) {
			sb.Append(" id=\"").Append(Id).Append("\"");
		}
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onload", onload);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "role", this.Role);
		
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
	}

	private new void RenderEndTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		output.Write("\n".PadRight(depth, '\t') + "</body>");
	}
	
	private string onload;
}
}