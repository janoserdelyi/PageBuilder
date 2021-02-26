using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
	public class Img : ControllerBase
	{
		#region constructors
		public Img () {
			//name = Constants.BEAN_KEY;
		}

		public Img (string id) {
			Id = id;
			/*
			if (Name == null) {
				name = id;
			}
			*/
		}
		#endregion

		#region public properties
		public string Src {
			get { return src; }
			set { src = value; }
		}

		public string Alt {
			get { return alt; }
			set { alt = value; }
		}

		public string Type {
			get { return type; }
			set { type = value; }
		}

		public string Usemap {
			get { return usemap; }
			set { usemap = value; }
		}

		public string Width { get; set; }
		public string Height { get; set; }
		public string Srcset { get; set; }
		public string Sizes { get; set; }
		public string Loading { get; set; }
		#endregion

		public override void Render (
			System.IO.TextWriter output,
			int depth = 0
		) {
			// Create an appropriate "input" element based on our parameters
			StringBuilder sb = new StringBuilder ();
			sb.Append ("\n".PadRight (depth, '\t')).Append ("<img");
			if (!string.IsNullOrEmpty (Id) && !Id.StartsWith ("element_")) {
				sb.Append (" id=\"").Append (Id).Append ("\"");
			}

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "src", src);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "alt", alt);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "usemap", usemap);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "width", this.Width);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "height", this.Height);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "hidden", this.Hidden);

			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "srcset", this.Srcset);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "sizes", this.Sizes);
			ControlUtils.RenderAttribute (ContextValues, sb, this.Id, "loading", this.Loading);

			sb.Append (prepareEventHandlers (this.Id));
			sb.Append (prepareStyles (this.Id));
			sb.Append (prepareOther (this.Id));

			if (base.DataAttributes.Count > 0) {
				foreach (System.Collections.Generic.KeyValuePair<string, object> da in base.DataAttributes) {
					ControlUtils.RenderAttribute (ContextValues, sb, this.Id, da.Key, da.Value.ToString ());
				}
			}
			if (base.AriaAttributes.Count > 0) {
				foreach (System.Collections.Generic.KeyValuePair<string, object> da in base.AriaAttributes) {
					ControlUtils.RenderAttribute (ContextValues, sb, this.Id, da.Key, da.Value.ToString ());
				}
			}

			sb.Append (" />");
			output.Write (sb.ToString ());
		}

		#region private declarations
		protected string src = null;
		protected string alt = null;
		private string type;
		private string usemap;
		#endregion
	}
}
