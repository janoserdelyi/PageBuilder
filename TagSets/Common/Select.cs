using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Select : ControllerBase
{
	#region constructors
	public Select () {
		this.Id = null;
		this.Name = null;
	}
	
	public Select (
		string id
	) {
		this.Id = id;
		this.Name = id;
	}
	
	public Select (
		string id,
		string name
	) {
		this.Id = id;
		this.Name = name;
	}
	#endregion
	
	#region public properties
	public string Multiple { 
		get { return multiple; }
		set { multiple = value; }
	}
	
	public string matchList = "";
	
	/*	How many available options should be displayed when this element
		is rendered?
	*/
	public string Size {
		get { return size; }
		set { size = value; }
	}
	#endregion
	
	#region public methods
	public bool isMatched (
		string matchValue
	) {
		if (match == null || matchValue == null) {
			return false;
		}
		
		for (int i=0; i<match.Length; i++) {
			matchList += match[i] + ",";
			if (matchValue.ToString() == match[i].ToString()) {
				return true;
			}
		}
		return false;
	}
	#endregion
	
	#region render
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		//both id and name are required, so blow up if either is missing
		if (Id == null) {
			throw new System.ArgumentNullException("attribute 'id' missing on Select");
		}
		
		if (this.Name == null) {
			throw new System.ArgumentNullException("attribute 'name' missing on Select. Id '" + Id + "'");
		}
		
		StringBuilder sb = new StringBuilder();
		
		sb.Append("\n".PadRight(depth, '\t')).Append("<select name=\"").Append(this.Name).Append("\" id=\"").Append(Id).Append("\"");
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "accesskey", this.Accesskey);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "tabindex", this.Tabindex);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "multiple", multiple);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "size", size);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "disabled", disabled);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "required", this.Required);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "readonly", this.Readonly);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "hidden", this.Hidden);
		
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
		
		//results.Append("<option>" + name + " {" + _value.ToString() + "}</option>");
		
		// Print this field to our output writer
		output.Write(sb.ToString());

		// Store this tag itself
		// 2017-10-06 dear lord what am i looking at. this can be rewritten
		ContextValues["com.janoserdelyi.PageBuilder.TagSets.Common.SELECT"] = this;
		
		// Calculate the match values we will actually be using
		/*
		string message = null;
		try {
			object value = null;
			try {
				value = ControlUtils.GetContextValue(ContextValues, name, property, null);
			} catch (Exception) {
				message = "Select rendered. Name '" + name + "', Id '" + Id + "', property '" + property + "', _value during check '" + (_value == null ? "null" : _value) + "'";
			}
			if (value == null) {
				_value = "";
			} else {
				_value = value.ToString();
			}
		} catch (Exception) {
			message = "Error while getting Select value. Name '" + name + "', Id '" + Id + "', property '" + property + "', _value after check '" + _value + "'";
			
		}
		*/
		this.Value = ControlUtils.GetContextValue(ContextValues, this.Name);
		if (this.Value == null) {
			this.Value = "";
		}
		
		if (this.Value.IndexOf(',') > -1) {
			string[] matches = this.Value.Split(',');
			match = new String[matches.Length];
			
			for(int i=0; i<match.Length; i++) {
				match[i] = matches[i];
			}
		} else {
			match = new String[1];
			match[0] = this.Value;
		}
		
		RenderChildren(output, depth+1);
		output.Write("\n".PadRight(depth, '\t') + "</select>");
	}
	#endregion
	
	#region private declarations
	protected string[] match = null;
	protected string multiple = null;
	protected string saveBody = null;
	protected string size = null;
	public string disabled = null;
	#endregion
}
}