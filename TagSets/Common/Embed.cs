using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Embed : ControllerBase
{
	
	#region constructors
	public Embed () {
		
	}
	#endregion
	
	#region public properties
	public string Src {get;set;}
	public string Quality {get;set;}
	public string Bgcolor {get;set;}
	public string Allowscriptaccess {get;set;}
	public string Pluginspage {get;set;}
	public string Type {get;set;}
	public string Align {get;set;}
	public string Height {get;set;}
	public string Width {get;set;}
	public string Allowfullscreen {get;set;}
	#endregion

	#region render
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
		StringBuilder sb = new StringBuilder();
		sb.Append("\n".PadRight(depth, '\t')).Append("<embed");
		sb.Append(" id=\"").Append(Id).Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "name", this.Name);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "src", Src);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "quality", Quality);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "bgcolor", Bgcolor);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "allowscriptaccess", Allowscriptaccess);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "allowfullscreen", Allowscriptaccess);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "pluginspage", Pluginspage);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "type", Type);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "align", Align);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "height", Height);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "width", Width);
		
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
		output.Write("\n".PadRight(depth, '\t') + "</embed>");
	}
	#endregion
}
}