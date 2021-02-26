using System;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class InputFile : cb.BaseInput
{
	public InputFile () {
		this.type = "file";
	}
}
}

/*
using System;
using System.Text;

using com.janoserdelyi.PageBuilder.TagSets;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class InputFile : cb.FileInput
{
	#region render
	public override void Render (
		System.IO.TextWriter output
	) {
		StringBuilder sb = new StringBuilder();
		
		sb.Append("<input type=\"file\"");
		
		//name and id. i need to come up with a cleaner standard process for this
		sb.Append(" name=\"").Append(name).Append("\"");
		if (!string.IsNullOrEmpty(Id) && !Id.StartsWith("element_")) {
			sb.Append(" id=\"").Append(Id).Append("\"");
		}
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accept", base.Accept);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", tabindex);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "multiple", multiple);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "style", style);
		
		//time to get value. again, i need a cleaner way to get this
		//2011-09-22. experimenting getting value by id instead of name
		string val = null;
		if (string.IsNullOrEmpty(Id)) {
			val = ControlUtils.GetContextValue(ContextValues, name);
		} else {
			val = ControlUtils.GetContextValue(ContextValues, this.Id);
		}
		if (val == null) {
			val = this.Value;
		}
		
		if (!string.IsNullOrEmpty(val)) {
			sb.Append(" value=\"").Append(val).Append("\"");
		}
		
		sb.Append(" />");
		
		output.Write(sb.ToString());
	}
	#endregion
	
	#region public properties
	public string Multiple {
		get { return multiple; }
		set { multiple = value; }
	}
	
	public new string Name {
		get { return name; }
		set { 
			name = value; 
			base.Name = value;
		}
	}
	#endregion
	
	#region private methods
	protected string prepareEventHandlers(string name) {
		StringBuilder handlers = new StringBuilder();
		prepareMouseEvents(handlers, name);
		prepareKeyEvents(handlers, name);
		prepareTextEvents(handlers, name);
		prepareFocusEvents(handlers, name);
		prepareAlternateEvents(handlers, name);
		return handlers.ToString();
	}
	
	private void prepareMouseEvents (
		StringBuilder sb,
		string name
	) {
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onclick", onclick);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "ondblclick", ondblclick);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onmouseover", onmouseover);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onmouseout", onmouseout);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onmousemove", onmousemove);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onmousedown", onmousedown);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onmouseup", onmouseup);
	}
	
	private void prepareKeyEvents (
		StringBuilder sb,
		string name
	) {
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onkeydown", onkeydown);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onkeyup", onkeyup);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onkeypress", onkeypress);
	}
	
	private void prepareTextEvents (
		StringBuilder sb,
		string name
	) {
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onselect", onselect);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onchange", onchange);
	}
	
	private void prepareFocusEvents (
		StringBuilder sb,
		string name
	) {
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onblur", onblur);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onfocus", onfocus);
		//if (disabled) { ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "disabled", "true"); }
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "disabled", disabled); 
		if (readOnly) { ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "readonly", "true"); }
	}
	
	private void prepareAlternateEvents (
		StringBuilder sb,
		string name
	) {
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "onerror", onerror);
	}
	
	protected string prepareOther (string name) {
		
		StringBuilder sb = new StringBuilder();
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "draggable", draggable);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "dropzone", dropzone);
		
		return sb.ToString();
		
	}
	
	protected string prepareStyles (string name) {
		StringBuilder styles = new StringBuilder();
		
		ControlUtils.RenderAttribute(Context, styles, name, "style", style);
		ControlUtils.RenderAttribute(Context, styles, name, "class", cssClass);
		ControlUtils.RenderAttribute(Context, styles, name, "title", title);
		
		return styles.ToString();
	}
	#endregion
	
	#region private declarations
	private string name;
	
	private string multiple;
	//private string accept;
	
	// accessibility and keyboarding
	private string title = null;
	protected string accesskey = null;
	protected string tabindex = null;

	//  Mouse Events
	private string onclick = null;
	private string ondblclick = null;
	private string onmouseover = null;
	private string onmouseout = null;
	private string onmousemove = null;
	private string onmousedown = null;
	private string onmouseup = null;

	//  Keyboard Events
	private string onkeydown = null;
	private string onkeyup = null;
	private string onkeypress = null;

	// Text Events
	private string onselect = null;
	private string onchange = null;

	// Focus Events and States
	private string onblur = null;
	private string onfocus = null;
	private bool readOnly = false;
	//private bool disabled = false;
	private string disabled = null;
	
	//other events
	private string onerror = null;	
	
	// CSS Style Support
	private string style = null;
	private string cssClass = null;	
	
	//urg. basetag has these
	protected string width = null;
	protected string height = null;
	
	//html5 inclusions
	protected string draggable = null;
	protected string dropzone = null;
	#endregion
}
}
*/