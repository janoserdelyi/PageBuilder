using System;
using System.Collections.Generic;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Td : ControllerBase
{
	
	#region constructors
	public Td() {
		
	}
	
	public Td(string id) {
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
		
		sb.Append("<td");
		
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

		// Print this element to our output writer
		output.Write(sb.ToString());
		
		RenderChildren(output); 
		
		output.Write("</td>");
	}
	
	#region private declarations
	protected string rowspan = null;
	protected string colspan = null;
	#endregion
}
}