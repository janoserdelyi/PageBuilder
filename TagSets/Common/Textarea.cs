using System;
using System.Text;

using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Textarea : BaseInput
{
	#region constructors
	public Textarea () {
		
	}
	#endregion
	
	#region public properties
	public string OverrideContent {
		get { return overrideContent; }
		set { overrideContent = value; }
	}
	
	// 2018-06-06
	public string Spellcheck {get;set;}
	#endregion
	
	#region render
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		/*
		RenderBeginTag(output);
		RenderChildren(output);
		RenderEndTag(output, depth);
		*/
		
		StringBuilder sb = new StringBuilder();
		sb.Append("\n".PadRight(depth, '\t')).Append("<textarea");
		sb.Append(" name=\"").Append(this.Name).Append("\" id=\"").Append(Id).Append("\"");
		
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accesskey", this.Accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", this.Tabindex);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "maxlength", maxlength);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "cols", cols);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "rows", rows);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "placeholder", this.Placeholder);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "required", base.Required);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "readonly", base.Readonly);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "spellcheck", this.Spellcheck);
		
		sb.Append(prepareEventHandlers(this.Name));
		sb.Append(prepareStyles(this.Name));
		
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
		
		
		string val = null;
		if (string.IsNullOrEmpty(Id)) {
			val = ControlUtils.GetContextValue(ContextValues, this.Name);
		} else {
			val = ControlUtils.GetContextValue(ContextValues, this.Id);
		}
		if (val == null) {
			val = this.Value;
		}
		output.Write(sb.ToString());
		
		
		/*
		if (!string.IsNullOrEmpty(val)) {
			sb.Append(val);
		}
		*/
		
		//the bahavior i want is:
		//if i specify context via context, then override and only show that content if overrideContent = true
		//if we are not overriding content, then redern children
		bool oc = (overrideContent != null && overrideContent.ToLower() == "true");
		if (oc) {
			if (string.IsNullOrEmpty(val)) {
				RenderChildren(output);
			} else {
				output.Write(val);
			}
		} else {
			if (!string.IsNullOrEmpty(val)) {
				output.Write(val);
			}
			RenderChildren(output);
		}
		
		output.Write("</textarea>");
	}
	/*
	private void RenderBeginTag (System.IO.TextWriter output) {
		
		StringBuilder sb = new StringBuilder("<textarea");
		sb.Append(" name=\"").Append(name).Append("\" id=\"").Append(Id).Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accesskey", accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", tabindex);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "maxlength", maxlength);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "cols", cols);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "rows", rows);
		
		sb.Append(prepareEventHandlers(name));
		sb.Append(prepareStyles(name));
		sb.Append(">");
		
		
		string val = null;
		if (string.IsNullOrEmpty(Id)) {
			val = ControlUtils.GetContextValue(ContextValues, this.name);
		} else {
			val = ControlUtils.GetContextValue(ContextValues, this.Id);
		}
		if (val == null) {
			val = this.Value;
		}
		if (!string.IsNullOrEmpty(val)) {
			sb.Append(val);
		}
		
		output.Write(sb.ToString());
		
	}
	
	private void RenderEndTag(System.IO.TextWriter output) {
		output.Write("</textarea>");
	}
	*/
	#endregion
	
	#region private declarations
	protected string overrideContent = null;
	#endregion
}
}