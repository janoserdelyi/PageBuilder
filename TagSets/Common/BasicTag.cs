using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class BasicTag : ControllerBase
{
	#region constructors
	public BasicTag () {
		
	}
	
	public BasicTag (
		string type
	) {
		this.TagType = type;
	}
	
	public BasicTag (
		string type,
		string id
	) {
		this.TagType = type;
		this.Id = id;
	}
	#endregion
	
	#region public properties
	public string Fill {
		get { return fill; }
		set { fill = value; }
	}
	
	public string Height {get;set;}
	public string Width {get;set;}
	#endregion
	
	#region render
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
		int depth = 0
	) {
		if (Id == null && this.Property == null) {
			Id = "";
		}
		
		//set the name with the id
		if (Id != null && Name == null) {
			//Name = Id;
			this.Name = Id;
		}
		
		// Create an appropriate "input" element based on our parameters
		StringBuilder sb = new StringBuilder();
		sb.Append("\n".PadRight(depth, '\t')).Append("<");
		sb.Append(this.TagType);
		/*
		if (!string.IsNullOrEmpty(propertyFallout(Id))) {
			results.Append(" id=\"").Append(propertyFallout(Id)).Append("\"");
		}
		*/
		if (!string.IsNullOrEmpty(Id) && !(Id.StartsWith("element_") || Id.StartsWith("ctl"))) {
			sb.Append(" id=\"").Append(Id).Append("\"");
		}
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accesskey", this.Accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", this.Tabindex);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "xmlns", this.Xmlns);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "fill", fill);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "width", this.Width);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "height", this.Height);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "itemscope", this.Itemscope);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "itemprop", this.Itemprop);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "itemtype", this.Itemtype);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "role", this.Role);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "hidden", this.Hidden);
		
		sb.Append(prepareEventHandlers(this.Name));
		sb.Append(prepareStyles(this.Name));
		
		if (base.DataAttributes.Count > 0) {
			foreach (System.Collections.Generic.KeyValuePair<string, object> da in base.DataAttributes) {
				ControlUtils.RenderAttribute(ContextValues, sb, this.Name, da.Key, da.Value.ToString());
			}
		}
		if (base.AriaAttributes.Count > 0) {
			foreach (System.Collections.Generic.KeyValuePair<string, object> da in base.AriaAttributes) {
				ControlUtils.RenderAttribute(ContextValues, sb, this.Id, da.Key, da.Value.ToString());
			}
		}
		
		sb.Append(">");
		
		//contents of the tag
		if (_value != null) {
			if (this.DoNotFilter == null) {
				sb.Append(ControlUtils.Filter(_value));
			} else {
				sb.Append(_value);
			}
		} 
		else if (!"password".Equals(this.TagType)) {
			object value = null;
			try {
				value = ControlUtils.GetContextValue(ContextValues, this.Name);
			} catch {}
			if (value == null) {
				value = "";
			}
			if (this.DoNotFilter == null) {
				sb.Append(ControlUtils.Filter(value.ToString()));
			} else {
				sb.Append(value.ToString());
			}
		}
		
		// Print this field to our output writer
		output.Write(sb.ToString());
	}
	
	private new void RenderEndTag (
		System.IO.TextWriter output,
		int depth
	) {
		output.Write("\n".PadRight(depth, '\t') + "</" + this.TagType + ">");
	}
	#endregion
	
	/*
	protected string propertyFallout (
		string inputTest
	) {
	 	if (inputTest == "" || inputTest == null) { 
	 		return property;
	 	}
		return inputTest;
	}
	*/
	
	
	#region private declarations
	//protected string property = null;
	protected string _value = null;
	protected string fill = null;
	#endregion
}
}