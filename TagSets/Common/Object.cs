using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Object : ControllerBase
{
	public Object () {
		
	}
	public string Classid {
		get { return classid; }
		set { classid = value; }
	}
			
	public string Codebase {
		get { return codebase; }
		set { codebase = value; }
	}
			
	public string Align {
		get { return align; }
		set { align = value; }
	}
			
	public string Height {
		get { return height; }
		set { height = value; }
	}
	
	public string Width {
		get { return width; }
		set { width = value; }
	}
	
	public string Data {
		get { return data; }
		set { data = value; }
	}
	
	public string Type {
		get { return type; }
		set { type = value; }
	}
	
	public string Onload {
		get { return onload; }
		set { onload = value; }
	}
	
	public string Border {
		get { return border; }
		set { border = value; }
	}
	
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
		sb.Append("\n".PadRight(depth, '\t')).Append("<object");
		sb.Append(" id=\"").Append(Id).Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "name", this.Name);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "classid", classid);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "codebase", codebase);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "align", align);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "height", height);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "width", width);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "data", data);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "type", type);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "style", this.Style);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "class", this.Class);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "border", border);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onload", onload);
		
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
		output.Write("\n".PadRight(depth, '\t') + "</object>");
	}
	
	#region private declarations
	private string classid;
	private string codebase;
	private string align;
	private string height;
	private string width;
	private string data;
	private string type;
	private string onload;
	private string border;
	#endregion
}
}