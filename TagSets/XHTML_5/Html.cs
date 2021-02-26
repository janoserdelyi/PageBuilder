using System;
using System.Text;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class Html : ControllerBase
{
	
	public Html() {
		
	}
	
	public string Lang {get;set;}
	public string XmlLang {get;set;}
	public string Xmllang {get;set;}
	public string Dir {get;set;}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		RenderBeginTag(output, depth);
		RenderChildren(output);
		RenderEndTag(output);
	}

	private new void RenderBeginTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder("\n<html");
		if (!string.IsNullOrEmpty(Id)) {
			sb.Append(" id=\"").Append(Id).Append("\"");
		}
		
		//to server the document as xhtml5, it needs xmlns="http://www.w3.org/1999/xhtml"
		//so i will be defaulting
		if (string.IsNullOrEmpty(this.Xmlns)) {
			this.Xmlns = "http://www.w3.org/1999/xhtml";
		}
		if (string.IsNullOrEmpty(this.Lang)) {
			this.Lang = "en";
		}
		if (string.IsNullOrEmpty(this.XmlLang)) {
			this.XmlLang = "en";
		}
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "xmlns", this.Xmlns);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "lang", this.Lang);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "xml:lang", this.XmlLang);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "dir", this.Dir);
		
		sb.Append(prepareEventHandlers(Id));
		sb.Append(prepareStyles(Id));
		sb.Append(">");
		output.Write(sb.ToString());
	}

	private void RenderEndTag (
		System.IO.TextWriter output
	) {
		output.Write("\n</html>");
	}
}
}