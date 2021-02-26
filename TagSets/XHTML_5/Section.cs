using System;
using System.Text;


using com.janoserdelyi.PageBuilder.TagSets;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class Section : cb.BasicTag
{
	public Section () {
		this.TagType = "section";
	}
	
	public Section (string id) {
		Id = id;
		this.TagType = "section";
	}
}
}