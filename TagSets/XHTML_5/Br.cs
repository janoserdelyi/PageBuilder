using System;
using System.ComponentModel;
using System.Text;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class Br : ControllerBase
{
	public Br () {
		
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		output.Write("<br/>");
	}
}
}