using System;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
public class Li : BasicTag
{
	public Li () {
		this.TagType = "li";
	}
	
	public Li (string id) {
		Id = id;
		this.TagType = "li";
	}
}
}