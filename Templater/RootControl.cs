using System;
using System.Collections.Generic;

namespace com.janoserdelyi.PageBuilder.Templater
{
	// just a container for the node tree
	public class RootControl
	{
		public RootControl () {
			ContextValues = new Dictionary<string, object> ();
			children = new List<IController> ();
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

		public IDictionary<string, object> ContextValues { get; set; }

		public void MergeNewContextValues (IDictionary<string, object> ctx) {
			// loop the new context values and upsert basically
			foreach (KeyValuePair<string, object> kvp in ctx) {
				if (ContextValues.ContainsKey (kvp.Key)) {
					ContextValues[kvp.Key] = kvp.Value;
				} else {
					ContextValues.Add (kvp);
				}
			}
		}

		private ICollection<IController> children;
	}
}