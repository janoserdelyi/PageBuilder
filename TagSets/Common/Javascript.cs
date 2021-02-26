using System;
using System.Text;
using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class Javascript : ControllerBase
	{
		public Javascript () {

		}

		[Obsolete ("2017-10-05 - i wish to remove all parameterless constructors")]
		public Javascript (string id) {
			Id = id;
			Name = id;
			Type = "text/javascript";
		}

		[Obsolete ("2017-10-05 - i wish to remove all parameterless constructors")]
		public Javascript (string id, string src) {
			Id = id;
			Name = id;
			Src = src;
			Type = "text/javascript";
		}

		public string Src { get; set; }
		public string Type { get; set; }
		public string Dataappkey { get; set; }
		public string Async { get; set; }
		public string Defer { get; set; }
		public string Crossorigin { get; set; }
		public string Integrity { get; set; }
		public string Nonce { get; set; }
		public string Nomodule { get; set; }
		public string Referrerpolicy { get; set; }

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
			StringBuilder sb = new StringBuilder ();
			sb.Append ("\n".PadRight (depth, '\t')).Append ("<script type=\"");
			sb.Append (this.Type);
			sb.Append ("\"");

			ControlUtils.RenderAttribute (ContextValues, sb, this.Name, "src", ControlUtils.Filter (Src));
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "data-app-key", Dataappkey);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "async", Async);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "defer", Defer);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "crossorigin", Crossorigin);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "integrity", Integrity);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "nonce", Nonce);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "nomodule", Nomodule);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "referrerpolicy", Referrerpolicy);

			sb.Append (">");

			string content = null;
			//hrmmmmm. name or id for this? i'm really inclined to think id. it WAS name originally
			if (!string.IsNullOrEmpty (this.Id)) {
				ControlUtils.GetContextValue (ContextValues, this.Id);
			}
			if (!string.IsNullOrEmpty (content)) {
				sb.Append (content).Append ("\n".PadRight (depth - 1, '\t'));
			}

			output.Write (sb.ToString ());
		}

		private new void RenderEndTag (
			System.IO.TextWriter output,
			int depth
		) {
			if (!(base.GetChildren () == null || base.GetChildren ().Count == 0)) {
				output.Write ("\n".PadRight (depth, '\t'));
			}
			output.Write ("</script>");
		}
	}
}