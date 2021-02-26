using System;
using System.Collections.Generic;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Tr : ControllerBase
{
	/*
	<!ATTLIST th
		%attrs;
		abbr        %Text;         #IMPLIED
		axis        CDATA          #IMPLIED
		headers     IdREFS         #IMPLIED
		scope       %Scope;        #IMPLIED
		rowspan     %Number;       "1"			//added this element to add this
		colspan     %Number;       "1"			//added this element to add this
		%cellhalign;
		%cellvalign;
		nowrap      (nowrap)       #IMPLIED
		bgcolor     %Color;        #IMPLIED
		width       %Length;       #IMPLIED
		height      %Length;       #IMPLIED
	>
	*/
	#region constructors
	public Tr() {
		
	}
	
	public Tr(string id) {
		this.Id = id;
	}
	#endregion
	
	#region public properties
	public string Rowspan {
		get { return rowspan; }
		set { rowspan = value; }
	}
	 
	public string Colspan {
		get { return colspan; }
		set { colspan = value; }
	}
	#endregion
	

	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		
		sb.Append("<tr");
		
		if (Id != null) {
	 		sb.Append(" id=\"").Append(propertyFallout(Id)).Append("\"");
	 	}
	 	
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "rowspan", rowspan);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "colspan", colspan);
		
		sb.Append(prepareStyles(Id));
		sb.Append(prepareEventHandlers(Id));

		if (this.DataAttributes.Count > 0) {
			foreach (KeyValuePair<string, object> da in this.DataAttributes) {
				ControlUtils.RenderAttribute(ContextValues, sb, this.Id, da.Key, da.Value.ToString());
			}
		}
		if (this.AriaAttributes.Count > 0) {
			foreach (KeyValuePair<string, object> da in this.AriaAttributes) {
				ControlUtils.RenderAttribute(ContextValues, sb, this.Id, da.Key, da.Value.ToString());
			}
		}

		sb.Append(">");

		output.Write(sb.ToString());
		
		RenderChildren(output); 
		
		output.Write("</tr>");
	}
	
	#region private declarations
	protected string rowspan = null;
	protected string colspan = null;
	#endregion
}
}