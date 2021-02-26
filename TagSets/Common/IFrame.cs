using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class IFrame : ControllerBase
	{
		public IFrame () {

		}

		public IFrame (string id) {
			this.Id = id;
		}

		public string Frameborder { get; set; }
		public string Src { get; set; }
		public string Allowfullscreen { get; set; }
		public string Allow { get; set; }
		public string Csp { get; set; }
		public string Importance { get; set; }
		public string Loading { get; set; }
		public string Referrerpolicy { get; set; }
		public string Sandbox { get; set; }
		public string Srcdoc { get; set; }

		public override void Render (
			System.IO.TextWriter output,
			int depth = 0
		) {
			StringBuilder sb = new StringBuilder ();

			sb.Append ("<iframe");

			if (Id != null) {
				sb.Append (" id=\"").Append (propertyFallout (Id)).Append ("\"");
			}

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "frameborder", this.Frameborder);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "src", this.Src);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "allowfullscreen", this.Allowfullscreen);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "allow", this.Allow);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "csp", this.Csp);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "importance", this.Importance);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "loading", this.Loading);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "referrerpolicy", this.Referrerpolicy);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "sandbox", this.Sandbox);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "srcdoc", this.Srcdoc);

			sb.Append (prepareStyles (this.Name));
			sb.Append (prepareEventHandlers (this.Name));

			sb.Append ("></iframe>");

			output.Write (sb.ToString ());
		}
	}
}