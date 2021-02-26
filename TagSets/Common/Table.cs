using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Table : ControllerBase
{
	/*
	<!ATTLIST table
		%attrs;
		summary     %Text;         #IMPLIED //this is one i'm adding here
		width       %Length;       #IMPLIED
		border      %Pixels;       #IMPLIED
		frame       %TFrame;       #IMPLIED
		rules       %TRules;       #IMPLIED
		cellspacing %Length;       #IMPLIED
		cellpadding %Length;       #IMPLIED
		align       %TAlign;       #IMPLIED
		bgcolor     %Color;        #IMPLIED
	>
	*/
	#region constructors
	public Table() {
		
	}
	
	public Table (string id) {
		this.Id = id;
	}
	#endregion
	
	#region public properties
	
	//The anchor to be added to the end of the generated hyperlink? i forget
	public string Summary {
		get { return summary; }
		set { summary = value; }
	}
	
	
	#endregion
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		StringBuilder sb = new StringBuilder();
		
		sb.Append("<table");
		
		if (Id != null) {
	 		sb.Append(" id=\"").Append(propertyFallout(Id)).Append("\"");
	 	}
	 	
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "summary", summary);
		
		sb.Append(prepareStyles(this.Name));
		sb.Append(prepareEventHandlers(this.Name));
		
		sb.Append(">");

		output.Write(sb.ToString());
		
		RenderChildren(output); 
		
		output.Write("</table>");
	}
	
	#region private properties
	protected string summary = null;
	
	#endregion
}
}