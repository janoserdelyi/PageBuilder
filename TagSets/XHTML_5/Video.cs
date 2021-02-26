using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class Video : ControllerBase
{
	public Video () {
		this.TagType = "video";
	}
	
	public Video (string id) {
		Id = id;
		this.TagType = "video";
	}
	
	public string Autoplay {get;set;}
	public string Buffered {get;set;}
	public string Controls {get;set;}
	public string CrossOrigin {get;set;}
	public string Height {get;set;}
	public string Loop {get;set;}
	public string Muted {get;set;}
	public string Played {get;set;}
	public string Preload {get;set;}
	public string Poster {get;set;}
	public string Src {get;set;}
	public string Width {get;set;}
	
	#region public and internal methods
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		RenderBeginTag(output, depth);
		RenderChildren(output, depth+1);
		RenderEndTag(output, depth);
	}
	
	internal new void RenderBeginTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		sb.Append("\n".PadRight(depth, '\t')).Append("<").Append(this.TagType);
		
		if (!string.IsNullOrEmpty(this.Id) && !(this.Id.StartsWith("element_") || this.Id.StartsWith("ctl"))) {
			sb.Append(" id=\"").Append(this.Id).Append("\"");
		}
		if (!string.IsNullOrEmpty(this.Name)) {
			sb.Append(" name=\"").Append(this.Name).Append("\"");
		}
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "title", this.Title);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "class", this.Class);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "style", this.Style);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onclick", this.Onclick);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "ondblclick", this.Ondblclick);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onmouseover", this.Onmouseover);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onmouseout", this.Onmouseout);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onmousemove", this.Onmousemove);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onmousedown", this.Onmousedown);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onmouseup", this.Onmouseup);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onkeydown", this.Onkeydown);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onkeyup", this.Onkeyup);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onkeypress", this.Onkeypress);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onselect", this.Onselect);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onchange", this.Onchange);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onblur", this.Onblur);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onfocus", this.Onfocus);
		//if (disabled) { ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "disabled", "true"); }
		//ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "disabled", this.Disabled); 
		//if (readOnly) { ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "readonly", "true"); }
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onerror", this.Onerror);
		
		//ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "draggable", this.Draggable);
		//ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "dropzone", this.Dropzone);
		
		//ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "itemscope", this.Itemscope);
		//ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "itemprop", this.Itemprop);
		//ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "itemtype", this.Itemtype);
		
		//ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "oninvalid", this.Oninvalid);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "autoplay", this.Autoplay);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "buffered", this.Buffered);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "controls", this.Controls);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "crossorigin", this.CrossOrigin);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "height", this.Height);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "loop", this.Loop);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "muted", this.Muted);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "played", this.Played);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "preload", this.Preload);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "poster", this.Poster);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "src", this.Src);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "width", this.Width);
		
		sb.Append(">");
		
		// this is where i used to inject a context value directly
		// i may or may not keep this. i may change up methods
		if (this.Id != null) {
			if (ContextValues.ContainsKey(this.Id)) {
				object val = ContextValues[this.Id];
				if (val == null) {
					this.Value = null;
				} else {
					this.Value = val.ToString();
				}
			} else {
				this.Value = null;
			}
			//this.Value = ContextValues.ContainsKey(this.Id) ? this.Value = (ContextValues[this.Id] == null ? null : ContextValues[this.Id].ToString()) : null;
			if (this.Value != null) {
				if (this.DoNotFilter == null) {
					sb.Append(ControlUtils.Filter(this.Value));
				} else {
					sb.Append(this.Value);
				}
			}
		}
			
		
		
		output.Write(sb.ToString());
	}
	
	internal new void RenderEndTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		output.Write("\n".PadRight(depth, '\t') + "</" + this.TagType + ">");
	}
	
	internal new void RenderChildren (
		System.IO.TextWriter output,
		int depth = 0
	) {
		foreach (IController child in this.GetChildren()) {
			child.Render(output, depth);
		}
	}
	#endregion
	
}
}
