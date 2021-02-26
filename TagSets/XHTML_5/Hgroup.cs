using System;
using cb = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.TagSets.XHTML_5
{
public class Hgroup : cb.BasicTag
{
	public Hgroup () {
		this.TagType = "hgroup";
	}
	
	[Obsolete("2017-10-05 - i wish to remove all parameterless constructors")]
	public Hgroup (string id) {
		Id = id;
		this.TagType = "hgroup";
	}
}
}