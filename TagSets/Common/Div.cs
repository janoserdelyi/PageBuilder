using System;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Div : ControllerBase
{
	public Div () {
		this.TagType = "div";
	}
	
	public Div (string id) {
		this.Id = id;
		this.TagType = "div";
	}
}
}