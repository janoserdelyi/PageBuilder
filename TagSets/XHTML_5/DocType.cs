using System;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
//this is much simpler than the XHTML 1.x doctype implementation. there is only one xhtml5 doctype!
public class DocType : ControllerBase
{
	public DocType () {
		
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		output.Write("<!DOCTYPE html>");
	}
}
}
