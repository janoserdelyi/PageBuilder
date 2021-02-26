using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class A : ControllerBase
{
	#region constructors
	public A () {
		
	}
	
	public A (string id) {
		this.Id = id;
	}
	#endregion
	
	#region public properties
	public string Href {
		get { return href; }
		set { href = value; }
	}
	
	public string Target  {
		get { return target; }
		set { target = value; }
	}
	
	public string AnchorText {
		get { return anchorText; }
		set { anchorText = value; }
	}
	
	public string Rel {
		get { return rel; }
		set { rel = value; }
	}
	
	public string Coords {
		get { return coords; }
		set { coords = value; }
	}
	#endregion
	
	#region render
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		// TODO: does this REALLY need to output and terminate? can named anchors exist as 
		// regular anchors?
		// Special case for name anchors
		
		//commented out 2009-03-13 janos while working on sqibl for merryfools. we'll see how this goes...
		//ideally i'd like some way to switch the behavior.
		/* 
		if (name != null) {
			StringBuilder results = new StringBuilder("<a name=\"");
			results.Append(name);
			results.Append("\">");
			output.Write(results.ToString());
			return;
		}
		*/
		
		StringBuilder sb = new StringBuilder();
		
		sb.Append("\n".PadRight(depth, '\t')).Append("<a");
		
		if (!string.IsNullOrEmpty(Id) && !Id.StartsWith("element_")) {
			sb.Append(" id=\"").Append(Id).Append("\"");
		}
		if (!string.IsNullOrEmpty(this.Name)) {
			sb.Append(" name=\"").Append(this.Name).Append("\"");
		}
		
		//normally i'd pass in name instead of id, but here we are
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "href", href);
		
		//this is a patch. i don't feel like disturbing other things for an edge case right now
		if (ContextValues.ContainsKey(Id + ":href")) {
			href = ContextValues[Id + ":href"].ToString();
		}
		
		/* wtf? why did i have this in here
		if (target != null) {
			sb.Append(" onclick=\"window.open('").Append(href).Append("', '").Append(target).Append("'); return false;\"");
		} 
		*/
		
		//normally i'd pass in name instead of id, but here we are
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "tabindex", this.Tabindex);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "rel", rel);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "coords", coords);
		
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "itemscope", this.Itemscope);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "itemprop", this.Itemprop);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Name, "itemtype", this.Itemtype);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "target", this.Target);
		ControlUtils.RenderAttribute(ContextValues, sb, this.Id, "hidden", this.Hidden);
		
		sb.Append(prepareStyles(Id));
		sb.Append(prepareEventHandlers(Id));
		
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
		
		if (ContextValues.ContainsKey(this.Id + ":AnchorText")) {
			anchorText = ContextValues[this.Id + ":AnchorText"].ToString();
		}
		
		//TODO: technically it could have both... but i'm not sure what i want to do with it
		if (anchorText == null) {
			RenderChildren(output, depth+1); 
			output.Write("\n".PadRight(depth, '\t'));
		} else { 
			output.Write(anchorText); 
		}
		output.Write("</a>");
	}
	#endregion
	
	#region private
	protected string href = null;
	protected string target = null;
	protected string anchorText = null;
	protected string rel = null;
	protected string coords = null;
	#endregion
}
}