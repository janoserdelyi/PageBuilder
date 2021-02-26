using System;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Head : ControllerBase
{
	public Head () {
		
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		depth = 1; // enforcing. i have some stylistic prefs here. i don't indent head and body inside html
		RenderBeginTag(output, depth);
		RenderChildren(output, depth+1);
		RenderEndTag(output);
	}

	private new void RenderBeginTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		output.Write("\n<head>");
	}

	private void RenderEndTag (
		System.IO.TextWriter output
	) {
		output.Write("\n</head>");
	}
}
}