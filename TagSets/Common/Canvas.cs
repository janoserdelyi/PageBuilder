using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Canvas : BasicTag
{
	public Canvas () {
		this.TagType = "canvas";
	}
	
	public Canvas (string id) {
		Id = id;
		this.TagType = "canvas";
	}
}
}