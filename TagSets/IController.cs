using System;

namespace com.janoserdelyi.PageBuilder
{
	public interface IController
	{
		void Render (System.IO.TextWriter tw, int depth = 0);
		void AddChild (IController child);
		System.Collections.Generic.ICollection<IController> GetChildren ();

		System.Collections.Generic.IDictionary<string, object> ContextValues { get; set; }

		// sometimes additive and substractive methods would be nice on class and style
		void AddClass (string className);
		void RemoveClass (string className);

		string TagType { get; set; }
		string Id { get; set; }
		string Name { get; set; }

		string Class { get; set; }
		string Style { get; set; }

		string Accesskey { get; set; }
		string Tabindex { get; set; }
		string Xmlns { get; set; }
		string Title { get; set; }

		//  Mouse Events
		string Onclick { get; set; }
		string Ondblclick { get; set; }
		string Onmouseover { get; set; }
		string Onmouseout { get; set; }
		string Onmousemove { get; set; }
		string Onmousedown { get; set; }
		string Onmouseup { get; set; }

		//  Keyboard Events
		string Onkeydown { get; set; }
		string Onkeyup { get; set; }
		string Onkeypress { get; set; }

		// Text Events
		string Onselect { get; set; }
		string Onchange { get; set; }

		// Focus Events and States
		string Onblur { get; set; }
		string Onfocus { get; set; }

		string Disabled { get; set; }

		//other events
		string Onerror { get; set; }

		//html5 inclusions
		string Draggable { get; set; }
		string Dropzone { get; set; }

		string Itemscope { get; set; }
		string Itemprop { get; set; }
		string Itemtype { get; set; }

		string Property { get; set; } // used on meta tags i think?
		string Value { get; set; }

		string DoNotFilter { get; set; }

		string Role { get; set; }
	}
}