using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Label : ControllerBase
{
	#region constructors
	public Label() {
		
	}
	
	public Label (string id) {
		this.Id = id;
	}
	#endregion
	
	#region public properties
	public string For {
		get { return forId; }
		set { forId = value; }
	}
	
	//this may spread and be used more than here
	public string OverrideContent {
		get { return overrideContent; }
		set { overrideContent = value; }
	}
	#endregion
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		
		sb.Append("\n".PadRight(depth, '\t')).Append("<label");
		
		if (!string.IsNullOrEmpty(Id) && !Id.StartsWith("element_")) {
	 		sb.Append(" id=\"").Append(Id).Append("\"");
	 	}
	 	
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "for", forId);
		
		sb.Append(prepareStyles(Id));
		sb.Append(prepareEventHandlers(Id));
		
		sb.Append(">");
		
		if (this.Value == null && !string.IsNullOrEmpty(Id)) {
			this.Value = ControlUtils.GetContextValue(ContextValues, Id);
		}
		
		if (this.Value != null) {
			sb.Append(this.Value);
		}
		
		output.Write(sb.ToString());
		
		if (overrideContent == null || (overrideContent != null && overrideContent.ToLower() != "true")) {
			RenderChildren(output, depth+1);
		}
		
		output.Write("\n".PadRight(depth, '\t') + "</label>");
	}
	
	#region private properties
	protected string forId = null;
	protected string overrideContent = null;
	#endregion
}
}