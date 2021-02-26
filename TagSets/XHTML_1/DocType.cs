using System;
using System.Text;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_1
{
public class DocType : ControllerBase
{
	public DocType () {
		
	}
	
	public DocType (string type) {
		this.type = type;
	}
	
	public string Type {
		get { return type; }
		set { type = value; }
	}
	
	private string translatedType {
		get {
			if (type == null) {
				throw new ArgumentNullException("a DocType.Type must be defined. currently supported (xhtml 1.0|xhtml 1.0 strict|xhtml 1.0 frameset). default is xhtml 1.0 strict");
			}
			
			switch (type.ToLower()) {
				case "xhtml 1.0" :
				case "xhtml 1.0 strict" :
					return "\"-//W3C//DTD XHTML 1.0 Strict//EN\"\n\t\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"";
				case "xhtml 1.0 frameset" :
					return "\"-//W3C//DTD XHTML 1.0 Frameset//EN\"\n\t\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd\"";
				
				default :
					return "\"-//W3C//DTD XHTML 1.0 Strict//EN\"\n\t\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"";
			}
		}
	}
	
	public override void Render (
		System.IO.TextWriter output, 
		int depth = 0
	) {
		output.Write("<!DOCTYPE html PUBLIC " + translatedType + ">");
	}
	
	private string type;
}
}
