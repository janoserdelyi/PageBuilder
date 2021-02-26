using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Param : ControllerBase
{
	
	#region constructors
	public Param () {
		
	}
	#endregion
	
	#region render
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder("<param");
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "name", this.Name);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "value", this.Value);
		sb.Append(" />");
		output.Write(sb.ToString());
	}
	#endregion
}
}