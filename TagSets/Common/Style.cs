using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class Style : ControllerBase
	{
		public Style () {
			this.Type = "text/css";
		}

		[Obsolete ("2017-10-05 - i wish to remove all parameterless constructors")]
		public Style (
			string media
		) {
			this.Media = media;
			this.Type = "text/css";
		}

		[Obsolete ("2017-10-05 - i wish to remove all parameterless constructors")]
		public Style (
			string media,
			string content
		) {
			this.Media = media;
			this.Type = "text/css";
		}

		public string Type { get; set; }
		public string Media { get; set; }
		public string Nonce { get; set; }

		public override void Render (
			System.IO.TextWriter output,
			int depth = 0
		) {
			RenderBeginTag (output, depth);
			RenderChildren (output, depth + 1);
			RenderEndTag (output, depth);
		}

		private new void RenderBeginTag (
			System.IO.TextWriter output,
			int depth = 0
		) {
			if (Id != null && Name == null) {
				Name = Id;
			}

			StringBuilder sb = new StringBuilder ();
			sb.Append ("\n".PadRight (depth, '\t')).Append ("<style");
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "id", Id);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "type", Type);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "media", Media);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "nonce", Nonce);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "title", base.Title);

			sb.Append (">");

			output.Write (sb.ToString ());

			/*
			if (string.IsNullOrEmpty(content)) {
				content = ControlUtils.GetContextValue(ContextValues, this.name);
			}
			if (!string.IsNullOrEmpty(content)) {
				sb.Append(content);
			}
			*/
			string val = ControlUtils.GetContextValue (ContextValues, this.Id);
			if (!string.IsNullOrEmpty (val)) {
				output.Write (val);
			}
		}

		private new void RenderEndTag (
			System.IO.TextWriter output,
			int depth = 0
		) {
			output.Write ("\n".PadRight (depth, '\t') + "</style>\n");
		}
	}
}