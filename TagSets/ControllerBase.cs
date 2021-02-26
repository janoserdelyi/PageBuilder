using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder
{
	public class ControllerBase : IController
	{

		public ControllerBase () {
			children = new List<IController> ();
			DataAttributes = new Dictionary<string, object> ();
			AriaAttributes = new Dictionary<string, object> ();
		}

		public ControllerBase (
			string tagType
		) {
			this.TagType = tagType;
			this.children = new List<IController> ();
			DataAttributes = new Dictionary<string, object> ();
			AriaAttributes = new Dictionary<string, object> ();
		}

		// unforunately i think AddClass and RemoveClass are practically usesless since they happen at too late a stage when used for real
		// may have to make a syntax for injecting into localcontext
		public void AddClass (
			string className
		) {
			if (string.IsNullOrEmpty (this.Class)) {
				this.Class = className;
				return;
			}

			string[] classes = this.Class.Split (new char[0], StringSplitOptions.RemoveEmptyEntries);

			if (!classes.Contains<string> (className)) {
				this.Class = " " + className;
				return;
			}
		}

		public void RemoveClass (
			string className
		) {
			if (string.IsNullOrEmpty (this.Class)) {
				return;
			}

			if (this.Class.IndexOf (className) == -1) {
				return;
			}

			string[] classes = this.Class.Split (new char[0], StringSplitOptions.RemoveEmptyEntries).Where (c => !(c == className)).ToArray<string> ();

			this.Class = string.Join (" ", classes);
		}

		public virtual void Render (
			System.IO.TextWriter output,
			int depth = 0
		) {
			RenderBeginTag (output, depth);
			RenderChildren (output, depth + 1);
			RenderEndTag (output, depth);
		}

		internal void RenderBeginTag (
			System.IO.TextWriter output,
			int depth = 0
		) {
			StringBuilder sb = new StringBuilder ();
			sb.Append ("\n".PadRight (depth, '\t')).Append ("<").Append (this.TagType);

			if (!string.IsNullOrEmpty (this.Id) && !(this.Id.StartsWith ("element_") || this.Id.StartsWith ("ctl"))) {
				sb.Append (" id=\"").Append (this.Id).Append ("\"");
			}
			if (!string.IsNullOrEmpty (this.Name)) {
				sb.Append (" name=\"").Append (this.Name).Append ("\"");
			}

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "title", this.Title);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "class", this.Class);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "style", this.Style);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onclick", this.Onclick);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "ondblclick", this.Ondblclick);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onmouseover", this.Onmouseover);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onmouseout", this.Onmouseout);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onmousemove", this.Onmousemove);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onmousedown", this.Onmousedown);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onmouseup", this.Onmouseup);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onkeydown", this.Onkeydown);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onkeyup", this.Onkeyup);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onkeypress", this.Onkeypress);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onselect", this.Onselect);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onchange", this.Onchange);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onblur", this.Onblur);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onfocus", this.Onfocus);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "disabled", this.Disabled);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "hidden", this.Hidden);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "onerror", this.Onerror);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "draggable", this.Draggable);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "dropzone", this.Dropzone);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "itemscope", this.Itemscope);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "itemprop", this.Itemprop);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "itemtype", this.Itemtype);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "oninvalid", this.Oninvalid);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "role", this.Role);

			if (this.DataAttributes.Count > 0) {
				foreach (KeyValuePair<string, object> da in this.DataAttributes) {
					ControlUtils.RenderAttribute (ContextValues, sb, this.Id, da.Key, da.Value.ToString ());
				}
			}
			if (this.AriaAttributes.Count > 0) {
				foreach (KeyValuePair<string, object> da in this.AriaAttributes) {
					ControlUtils.RenderAttribute (ContextValues, sb, this.Id, da.Key, da.Value.ToString ());
				}
			}

			sb.Append (">");

			// this is where i used to inject a context value directly
			// i may or may not keep this. i may change up methods
			if (this.Id != null) {
				if (ContextValues.ContainsKey (this.Id)) {
					object val = ContextValues[this.Id];
					if (val == null) {
						this.Value = null;
					} else {
						this.Value = val.ToString ();
					}
				} else {
					this.Value = null;
				}
				//this.Value = ContextValues.ContainsKey(this.Id) ? this.Value = (ContextValues[this.Id] == null ? null : ContextValues[this.Id].ToString()) : null;
				if (this.Value != null) {
					sb.Append ("\n".PadRight (depth + 1, '\t'));
					if (this.DoNotFilter == null) {
						sb.Append (ControlUtils.Filter (this.Value));
					} else {
						sb.Append (this.Value);
					}
				}
			}

			output.Write (sb.ToString ());
		}

		internal void RenderEndTag (
			System.IO.TextWriter output,
			int depth // intentionally removed default value
		) {
			if (!(children == null || children.Count == 0)) {
				output.Write ("\n".PadRight (depth, '\t'));
			}
			output.Write ("</" + this.TagType + ">");
		}

		internal void RenderChildren (
			System.IO.TextWriter output,
			int depth = 0
		) {
			if (children != null && children.Count > 0) {
				//output.Write("\n");
				foreach (IController child in children) {
					child.Render (output, depth);
				}
			}
		}

		public void AddChild (IController child) {
			if (child == null) {
				return;
			}
			// give the child the context
			child.ContextValues = this.ContextValues;
			children.Add (child);
		}

		public ICollection<IController> GetChildren () {
			return children;
		}



		protected string prepareEventHandlers (string nameOrId) {
			StringBuilder handlers = new StringBuilder ();
			prepareMouseEvents (handlers, nameOrId);
			prepareKeyEvents (handlers, nameOrId);
			prepareTextEvents (handlers, nameOrId);
			prepareFocusEvents (handlers, nameOrId);
			prepareAlternateEvents (handlers, nameOrId);
			return handlers.ToString ();
		}

		private void prepareMouseEvents (
			StringBuilder sb,
			string nameOrId
		) {
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onclick", this.Onclick);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "ondblclick", this.Ondblclick);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onmouseover", this.Onmouseover);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onmouseout", this.Onmouseout);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onmousemove", this.Onmousemove);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onmousedown", this.Onmousedown);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onmouseup", this.Onmouseup);
		}

		private void prepareKeyEvents (
			StringBuilder sb,
			string nameOrId
		) {
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onkeydown", this.Onkeydown);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onkeyup", this.Onkeyup);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onkeypress", this.Onkeypress);
		}

		private void prepareTextEvents (
			StringBuilder sb,
			string nameOrId
		) {
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onselect", this.Onselect);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onchange", this.Onchange);
		}

		private void prepareFocusEvents (
			StringBuilder sb,
			string nameOrId
		) {
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onblur", this.Onblur);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onfocus", this.Onfocus);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "disabled", this.Disabled);
		}

		private void prepareAlternateEvents (
			StringBuilder sb,
			string nameOrId
		) {
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "onerror", this.Onerror);
		}

		protected string prepareOther (string nameOrId) {
			StringBuilder sb = new StringBuilder ();

			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "draggable", this.Draggable);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "dropzone", this.Dropzone);

			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "itemscope", this.Itemscope);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "itemprop", this.Itemprop);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "itemtype", this.Itemtype);

			return sb.ToString ();
		}

		protected string prepareStyles (string nameOrId) {
			StringBuilder sb = new StringBuilder ();

			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "title", this.Title);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "class", this.Class);
			ControlUtils.RenderAttribute (ContextValues, sb, nameOrId, "style", this.Style);

			return sb.ToString ();
		}



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
		public string Required { get; set; }
		public string Readonly { get; set; }
		public string Hidden { get; set; }

		//other events
		public string Onerror { get; set; }

		//html5 inclusions
		public string Draggable { get; set; }
		public string Dropzone { get; set; }

		public string Itemscope { get; set; }
		public string Itemprop { get; set; }
		public string Itemtype { get; set; }

		public string Oninvalid { get; set; }

		// a semi-secret directive to guide some parsing
		public string DoNotFilter { get; set; }

		public string Value { get; set; }
		public string Property { get; set; }


		// 2017-03-20
		public string Role { get; set; }

		// 2017-03-24. time to take a stab at data-* properties. i want to vomit
		//public dynamic DataAttributes {get;set;}
		public IDictionary<string, object> DataAttributes { get; set; }
		// 2017-04-10. aria-* attributes. these are good. accesibility
		public IDictionary<string, object> AriaAttributes { get; set; }

		#region hrm. i don't care for this here
		protected string propertyFallout (
			string inputTest
		) {
			/*
			// this is the original contents. this needs a complete re-evaluation
			if (string.IsNullOrEmpty(inputTest)) {
				return property; 
			}
			return inputTest;
			*/
			return inputTest;
		}
		#endregion


		private ICollection<IController> children;
	}
}