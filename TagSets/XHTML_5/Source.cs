using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class Source : ControllerBase
{
	public Source () {
		this.TagType = "source";
	}
	
	public Source (string id) {
		Id = id;
		this.TagType = "source";
	}
	
	public string Sizes {get;set;}
	public string Src {get;set;}
	public string Srcset {get;set;}
	public string Type {get;set;}
	public string Media {get;set;}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		sb.Append("<").Append(this.TagType);
		
		if (!string.IsNullOrEmpty(this.Id) && !(this.Id.StartsWith("element_") || this.Id.StartsWith("ctl"))) {
			sb.Append(" id=\"").Append(this.Id).Append("\"");
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
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "sizes", this.Sizes);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "src", this.Src);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "srcset", this.Srcset);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "type", this.Type);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "media", this.Media);
		
		sb.Append(" />");
		
		output.Write(sb.ToString());
	}
	
}
}
