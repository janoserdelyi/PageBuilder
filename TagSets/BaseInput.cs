using System;
using System.Text;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class BaseInput : ControllerBase
{
	#region public properties
	public string Cols {
		get { return cols; }
		set { cols = value; }
	}
	
	public string MaxLength {
		get { return maxlength; }
		set { maxlength = value; }
	}
	
	public string Rows {
		get { return rows; }
		set { rows = value; }
	}
	
	public string Size {
		get { return cols; }
		set { cols = value; }
	}
	
	public string Accept {
		get { return accept; }
		set { accept = value; }
	}
	
	public string Type {
		get { return type; }
		set { type = value; }
	}
	
	public bool Redisplay {
		get { return redisplay; }
		set { redisplay = value; }
	}
	
	public string Lang {
		get { return lang; }
		set { lang = value; }
	}
	
	public string ContentEditable {
		get { return contentEditable; }
		set { contentEditable = value; }
	}
	
	public string ContextMenu {
		get { return contextMenu; }
		set { contextMenu = value; }
	}
	
	public string Dir {
		get { return dir; }
		set { dir = value; }
	}
	
	public string Placeholder {get;set;}
	public string Pattern {get;set;}
	
	/*public string Disabled {get;set;}*/
	
	// added mostly to keep Apple from being obnoxious. i think that's an impossible task personally...
	public string Autocorrect {get;set;}
	public string Autocapitalize {get;set;}
	
	//
	public string Autocomplete {get;set;}
	
	public string Step { get; set; }
	// public string Multiple {get;set;} // for file input types
	#endregion
	
	#region private methods
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		sb.Append("\n".PadRight(depth, '\t'));
		sb.Append("<input type=\"").Append(type).Append("\"");
		
		//name and id. i need to come up with a cleaner standard process for this
		if (!string.IsNullOrEmpty(this.Name)) {
			sb.Append(" name=\"").Append(this.Name).Append("\"");
		}
		
		if (!string.IsNullOrEmpty(this.Id) && !this.Id.StartsWith("element_")) {
			sb.Append(" id=\"").Append(this.Id).Append("\"");
		}
		
		// 2016-04-04. possibly dangerous. not sure yet
		if (string.IsNullOrEmpty(this.Name)) {
			this.Name = this.Id;
		}
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accesskey", this.Accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accept", accept);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "maxlength", maxlength);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "cols", cols);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", this.Tabindex);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "lang", lang);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "contenteditable", contentEditable);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "contextmenu", contextMenu);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "dir", dir);

		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "step", this.Step);

		// some html5 attributes added on here
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "placeholder", this.Placeholder);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "pattern", this.Pattern);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "autocorrect", this.Autocorrect);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "autocapitalize", this.Autocapitalize);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "autocomplete", this.Autocomplete);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "disabled", this.Disabled);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "required", this.Required);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "readonly", this.Readonly);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "hidden", this.Hidden);
		
		//time to get value. again, i need a cleaner way to get this
		//2011-09-22. experimenting getting value by id instead of name
		string val = null;
		if (string.IsNullOrEmpty(this.Id)) {
			if (ContextValues.ContainsKey(this.Name) && ContextValues[this.Name] != null) {
				val = ContextValues[this.Name].ToString();
			} else {
				val = null;
			}
		} else {
			if (ContextValues.ContainsKey(this.Id) && ContextValues[this.Id] != null) {
				val = ContextValues[this.Id].ToString();
			} else {
				val = null;
			}
		}
		if (val == null) {
			val = this.Value;
		}
		
		if (!string.IsNullOrEmpty(val)) {
			val = val.Replace("\"", "\\\"");
			sb.Append(" value=\"").Append(val).Append("\"");
		}
		
		sb.Append(prepareEventHandlers(this.Name));
		sb.Append(prepareStyles(this.Name));
		sb.Append(prepareOther(this.Name));
		
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
		
		sb.Append(" />");
		
		output.Write(sb.ToString());
	}
	#endregion
	
	#region private declarations
	protected string maxlength = null;
	protected string rows = null;
	protected string cols = null;
	protected bool redisplay = true;
	protected string type;
	protected string accept = null;
	
	protected string lang = null;
	protected string contentEditable = null;
	protected string contextMenu = null;
	protected string dir = null;
	
	#endregion
}
}