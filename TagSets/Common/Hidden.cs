using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Hidden : BaseInput
{
	#region constructors
	public Hidden () {
		this.type = "hidden";
	}
	#endregion
}
}