using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class Option : ControllerBase
	{
		public Option (
			IController parentController = null
		) {
			this.parentController = parentController;
		}
		// added because i wanted to programatically create these and add to a Select
		// rather than only doing it through markup
		public Option (
			string optionValue,
			string displayValue,
			IController parentController = null
		) {
			this.Value = optionValue;
			DisplayValue = displayValue;
			this.parentController = parentController;
		}

		// this might be a mistake, but let's try
		//public string ParentName { get; set; }
		/**
		 * The key used to look up the text displayed to the user for this
		 * option, if any.
		 */
		public string Key { get; set; }
		public string DisplayValue { get; set; }
		public string Selected { get; set; }
		public string Label { get; set; }

		public override void Render (
			System.IO.TextWriter output,
			int depth = 0
		) {
			/*	2006 20 25 janos erdelyi
				i don't care for this method of parental discovery.
				i really think it's the obligation, since the select should exist, for the 
				select to 'tell' the option it is its parent.
				i'm all for loose coupling and such, but there is nothing wrong with this being 
				tight. they are by their nature tightly bound and related to each other
			*/
			// Acquire the select tag we are associated with
			// these can be within datalists too
			/*
			Select selectTag = null;

			if (ParentName == null) {
				selectTag = (Select)ContextValues["com.janoserdelyi.PageBuilder.TagSets.Common.SELECT"];
			} else {
				selectTag = (Select)ContextValues[ParentName];
			}

			if (selectTag == null) {
				throw new NullReferenceException ("com.janoserdelyi.PageBuilder.TagSets.Common.Option. unable to find '" + (ParentName == null ? "com.janoserdelyi.PageBuilder.TagSets.Common.SELECT" : ParentName) + "' in the current context.");
			}
			*/

			// this whole thing needs a rewrite. it's forcing option to have a particular type of parent - which is legit enough but a pain
			// especially since i cannot tell (yet) what the parent node is
			/*
			IOptionParent selectTag = null;

			string parentContextKey = "com.janoserdelyi.PageBuilder.TagSets.Common.SELECT";

			if (ParentName == null) {

				if (selectTag is Datalist) {
					parentContextKey = "com.janoserdelyi.PageBuilder.TagSets.Common.DATALIST";
				}
				selectTag = (IOptionParent)ContextValues[parentContextKey];
			} else {
				selectTag = (IOptionParent)ContextValues[ParentName];
			}

			if (selectTag == null) {
				throw new NullReferenceException ("com.janoserdelyi.PageBuilder.TagSets.Common.Option. unable to find '" + (ParentName == null ? parentContextKey : ParentName) + "' in the current context.");
			}
			*/

			IOptionParent matchingParentTag = null;

			if (parentController != null) {
				if (parentController is IOptionParent) {
					matchingParentTag = (IOptionParent)parentController;
				}
			}

			// Generate an HTML <option> element
			StringBuilder sb = new StringBuilder ();
			sb.Append ("\n".PadRight (depth, '\t')).Append ("<option value=\"");
			sb.Append (this.Value);
			sb.Append ("\"");

			// 2017-12-27 holy crap correcting a long-standing error in implementation
			if (!string.IsNullOrEmpty (Disabled) && Disabled == "disabled") {
				sb.Append (" disabled=\"disabled\"");
			}

			if (matchingParentTag != null && matchingParentTag.isMatched (this.Value)) {
				sb.Append (" selected=\"selected\"");
			} else {
				if (!string.IsNullOrEmpty (Selected) && Selected == "selected") {
					sb.Append (" selected=\"selected\"");
				}
			}

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "class", this.Class);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "style", this.Style);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "label", this.Label);

			sb.Append (">");

			if (DisplayValue != null) {
				sb.Append (DisplayValue);
			}

			output.Write (sb.ToString ());

			RenderChildren (output, depth + 1);

			output.Write ("\n".PadRight (depth, '\t') + "</option>");
		}

		protected IController parentController { get; set; } = null;
	}

	// both Select and Datalist can contain options
	public interface IOptionParent
	{
		bool isMatched (string matchValue);
	}
}