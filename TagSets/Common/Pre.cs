using System;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Pre : cb.BasicTag
{
	public Pre () {
		this.TagType = "pre";
	}
	
	public Pre (string id) {
		Id = id;
		this.TagType = "pre";
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		RenderBeginTag(output, depth);
		RenderChildren(output, 0);
		RenderEndTag(output, depth);
	}
}
}