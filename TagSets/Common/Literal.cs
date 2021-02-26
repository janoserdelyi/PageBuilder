using System;
using System.Collections.Generic;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class Literal : IController
	{
		public Literal () {
			this.TagType = "literal";
		}

		public Literal (string content) {
			this.TagType = "literal";
			this.content = content;
		}

		public virtual void Render (
			System.IO.TextWriter output,
			int depth = 0
		) {
			if (this.Id != null && ContextValues.ContainsKey (this.Id) && ContextValues[this.Id] != null) {
				this.content = ContextValues[this.Id].ToString ();
			}
			output.Write ("\n".PadRight (depth, '\t'));
			output.Write (this.content);
		}

		public void AddClass (
			string className
		) {
			// does nothing. just fulfilling interface contract
		}

		public void RemoveClass (
			string className
		) {
			// does nothing. just fulfilling interface contract
		}

		public void AddChild (IController child) {

		}
		public ICollection<IController> GetChildren () {
			return null;
		}

		private string content;

		#region IController contractual obligations
		public string TagType { get; set; }

		public string Id { get; set; }
		public string Name { get; set; }

		public IDictionary<string, object> ContextValues { get; set; }

		public string Class { get; set; }
		public string Style { get; set; }

		public string Accesskey { get; set; }
		public string Tabindex { get; set; }
		public string Xmlns { get; set; }
		public string Title { get; set; }

		//  Mouse Events
		public string Onclick { get; set; }
		public string Ondblclick { get; set; }
		public string Onmouseover { get; set; }
		public string Onmouseout { get; set; }
		public string Onmousemove { get; set; }
		public string Onmousedown { get; set; }
		public string Onmouseup { get; set; }

		//  Keyboard Events
		public string Onkeydown { get; set; }
		public string Onkeyup { get; set; }
		public string Onkeypress { get; set; }

		// Text Events
		public string Onselect { get; set; }
		public string Onchange { get; set; }

		// Focus Events and States
		public string Onblur { get; set; }
		public string Onfocus { get; set; }

		public string Disabled { get; set; }

		//other events
		public string Onerror { get; set; }

		//html5 inclusions
		public string Draggable { get; set; }
		public string Dropzone { get; set; }

		public string Itemscope { get; set; }
		public string Itemprop { get; set; }
		public string Itemtype { get; set; }

		public string DoNotFilter { get; set; }
		public string Property { get; set; }
		public string Value { get; set; }

		public string Role { get; set; }
		#endregion
	}
}