using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class Meta : ControllerBase
{
	public Meta () {
		
	}
	
	[Obsolete("2017-10-05 - i wish to remove all parameterless constructors")]
	public Meta (
		string name, 
		string httpEquiv, 
		string content
	) {
		this.Name = name;
		this.HttpEquiv = httpEquiv;
		this.Content = content;
		//this.Id = name;
	}
	
	[Obsolete("2017-10-05 - i wish to remove all parameterless constructors")]
	public Meta (
		string id,
		string name, 
		string httpEquiv, 
		string content
	) {
		this.Id = id;
		this.Name = name;
		this.HttpEquiv = httpEquiv;
		this.Content = content;
	}
	
	public string Charset {get;set;}
	public string HttpEquiv {get;set;}
	public string Content {get;set;}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		sb.Append("\n".PadRight(depth, '\t')).Append("<meta");
		
		// 2013-10-11 i haven't looked at this in eons. changing this to name (versus id) hook like form elements
		// meta tags typically have a name, rarely an id
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "id", Id);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "http-equiv", HttpEquiv);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "charset", Charset);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "name", this.Name);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "property", this.Property);
		
		if (string.IsNullOrEmpty(Content)) {
			//ok, i need to allow empty, just not null
			Content = ControlUtils.GetContextValue(ContextValues, this.Name);
		}
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "content", Content);
		
		sb.Append(" />");
		output.Write(sb.ToString());
	}
}
}