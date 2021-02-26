using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Area : ControllerBase
{
	#region constructors
	public Area () {
		
	}
	
	public Area (string id) {
		Id = id;
	}
	#endregion
	
	#region public properties
	public string Href {
		get { return href; }
		set { href = value; }
	}
	
	public string Alt {
		get { return alt; }
		set { alt = value; }
	}
	
	public string Type {
		get { return type; }
		set { type = value; }
	}
	
	public string Coords {
		get { return coords; }
		set { coords = value; }
	}
	
	public string Shape {
		get { return shape; }
		set { shape = value; }
	}
	#endregion
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
	 	// Create an appropriate "input" element based on our parameters
	 	StringBuilder sb = new StringBuilder("<area ");
	 	
	 	if (Id != null) {
	 		sb.Append(" id=\"").Append(propertyFallout(Id)).Append("\"");
	 	}
	 	
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "href", href);
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "alt", alt);
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "coords", coords);
	 	ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "shape", shape);
	 	
		sb.Append(prepareEventHandlers(this.Name));
		sb.Append(prepareStyles(this.Name));
		
		sb.Append(" />");
		output.Write(sb.ToString());
	}
	
	#region private declarations
	protected string href = null;
	protected string alt = null;
	private string type;
	private string coords;
	private string shape;
	#endregion
}
}
