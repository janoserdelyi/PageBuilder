using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Link : ControllerBase
{
	//i really doubt i've covered all the atributes.
	//i need to look it up
	public Link () {
		
	}
	
	[Obsolete("2017-10-05 - i wish to remove all parameterless constructors")]
	public Link (
		string rel, 
		string type, 
		string title,
		string href
	) {
		
		// TODO: evaluate the id/name thing here with Link
		//this.name = name;
		//this.Id = name;
		this.Rel = rel;
		this.Type = type;
		this.Title = title;
		this.Href = href;
		
	}
	
	public string Rel {get;set;}
	public string Type {get;set;}
	public string Href {get;set;}
	public string Hreflang {get;set;}
	public string Sizes {get;set;}
	public string Media {get;set;}
	public string Crossorigin {get;set;}
	public string Integrity {get;set;}
	public string Importance {get;set;}
	public string Referrerpolicy  {get;set;}
	public string As {get;set;}
	public string Prefetch {get;set;}
	public string Target {get;set;}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		sb.Append("\n".PadRight(depth, '\t')).Append("<link");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "rel", this.Rel);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "as", this.As);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "type", this.Type);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "sizes", this.Sizes);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "title", this.Title);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "href", this.Href);
		if (!string.IsNullOrEmpty(this.Href)) {
			ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "hreflang", this.Hreflang);
		}
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "media", this.Media);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "crossorigin", this.Crossorigin);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "integrity", Integrity);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "importance", Importance);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "referrerpolicy ", Referrerpolicy );
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "prefetch ", Prefetch );
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "target ", Target );
		
		sb.Append(" />");
		output.Write(sb.ToString());
	}
}
}