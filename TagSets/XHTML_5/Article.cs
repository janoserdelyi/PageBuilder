using System;
using System.Text;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class Article : com.janoserdelyi.PageBuilder.ControllerBase
{
	public Article () {
		this.TagType = "article";
	}
	
	public Article (string id) {
		this.Id = id;
		this.TagType = "article";
	}
}
}