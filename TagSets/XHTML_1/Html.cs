using System;
using System.Text;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_1
{
public class Html : ControllerBase
{	
	public Html() {
		
	}
	
	public string Lang {
		get { return lang; }
		set { lang = value; }
	}
	
	public string XmlLang {
		get { return xmlLang; }
		set { xmlLang = value; }
	}
	public string Xmllang {
		get { return xmlLang; }
		set { xmlLang = value; }
	}
	
	public string Dir {
		get { return dir; }
		set { dir = value; }
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		RenderBeginTag(output, depth);
		RenderChildren(output, depth+1);
		//RenderEndTag(output);
		output.Write("\n</html>");
	}

	private new void RenderBeginTag (
		System.IO.TextWriter output,
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder("\n<html");
		sb.Append(" id=\"");
		sb.Append(this.Id);
		sb.Append("\"");
		
		ControlUtils.RenderAttribute (ContextValues, sb, Id, "xmlns", this.Xmlns);
		ControlUtils.RenderAttribute (ContextValues, sb, Id, "lang", lang);
		ControlUtils.RenderAttribute (ContextValues, sb, Id, "xml:lang", xmlLang);
		ControlUtils.RenderAttribute (ContextValues, sb, Id, "dir", dir);
		
		sb.Append(prepareEventHandlers(this.Id));
		sb.Append(prepareStyles(this.Id));
		
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
		
		sb.Append(">\n");
		output.Write(sb.ToString());
	}
	/*
	private new void RenderEndTag (
		System.IO.TextWriter output
	) {
		output.Write("\n</html>");
	}
	*/
	private string lang;
	private string xmlLang;
	private string dir;
}
}