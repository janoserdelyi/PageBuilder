using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Meta : ControllerBase
{
	#region constructors
	public Meta() {
		
	}
	
	public Meta (
		string name, 
		string httpEquiv, 
		string content
	) {
		this.Name = name;
		this.httpEquiv = httpEquiv;
		this.content = content;
		//this.Id = name;
	}
	
	public Meta (
		string id,
		string name, 
		string httpEquiv, 
		string content
	) {
		this.Id = id;
		this.Name = name;
		this.httpEquiv = httpEquiv;
		this.content = content;
		
	}
	#endregion
	
	#region public properties
	public string Charset {
		get { return charset; }
		set { charset = value; }
	}
	
	public string HttpEquiv {
		get { return httpEquiv; }
		set { httpEquiv = value; }
	}
	
	public string Content {
		get { return content; }
		set { content = value; }
	}
	
	//public new string Property {
	//	get { return _property; }
	//	set { _property = value; }
	//}
	#endregion
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder("\t<meta");
		
		// 2013-10-11 i haven't looked at this in eons. changing this to name (versus id) hook like form elements
		// meta tags typically have a name, rarely an id
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "id", Id);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "http-equiv", httpEquiv);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "charset", charset);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "name", this.Name);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "property", this.Property);
		
		string val = ControlUtils.GetContextValue(ContextValues, this.Name);
		if (string.IsNullOrEmpty(val)) {
			ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "content", content);
		} else {
			ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "content", val);
		}
		
		
		sb.Append(" />\n");
		output.Write(sb.ToString());
	}
	
	#region private declarations
	private string content;
	private string httpEquiv;
	private string charset;
	//private string _property;
	#endregion
}
}