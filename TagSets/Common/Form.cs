using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Form : ControllerBase
{
	
	#region public properties
	public string Action {
		get { return action; }
		set { action = value; }
	}
			
	public string Enctype {
		get { return enctype; }
		set { enctype = value; }
	}
			
	public string Method {
		get { return method; }
		set { method = value; }
	}
	
	public string Target {
		get { return target; }
		set { target = value; }
	}
	
	public string OnReset {
		get { return onreset; }
		set { onreset = value; }
	}
			
	public string OnSubmit {
		get { return onsubmit; }
		set { onsubmit = value; }
	}
	public string Onsubmit {
		get { return onsubmit; }
		set { onsubmit = value; }
	}
			
	public string RespectQuerystring {
		get { return respectQuerystring; }
		set { respectQuerystring = value; }
	}
	#endregion

	/// <summary>
	/// Render this control to the output parameter specified.
	/// </summary>
	/// <param name="output"> The HTML writer to write out to </param>
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
		int depth
	) {
		StringBuilder sb = new StringBuilder();
		sb.Append("\n".PadRight(depth, '\t')).Append("<form");
		sb.Append(" id=\"").Append(Id).Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "method", method, "post");
		
		sb.Append(" action=\"");
		if (action != null) {
			sb.Append(action);
		} else {
			//i want this changable by context as well
			if (ContextValues.ContainsKey(Id + ":action")) {
				sb.Append(ContextValues[Id + ":action"].ToString());
			} else {
				//i wonder if this should be HttpContext.Current.Request.RawUrl.ToString() instead
				// TODO: damnit, losing some helpers on Forms. investigate
				//sb.Append(Context.Request.Url.LocalPath);			
			}
		}
		
		//add the qstring info
		// TODO: damnit, losing some helpers on Forms. investigate
		/*
		if (respectQuerystring != null && respectQuerystring.ToLower() == "true") {
			sb.Append("?");
			sb.Append(Context.Request.QueryString);
		}
		*/
		sb.Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "enctype", enctype);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onreset", onreset);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "onsubmit", onsubmit);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "style", this.Style);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "class", this.Class);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "target", target);
		
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
		output.Write("\n".PadRight(depth, '\t') + "</form>");
	}
	
	#region private declarations
	private string action;
	private string enctype;
	private string method; // = "post";
	private string onreset;
	private string onsubmit;
	private string target;
	private string respectQuerystring;
	#endregion
}
}