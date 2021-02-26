/*
using System;

using System.ComponentModel;

namespace com.janoserdelyi.PageBuilder.TagSets.Common
{
/// <summary>
/// I made BaseTag to be a sister-page to BaseInput
/// some tags do not use BaseInput, but would benefit from having a simliar
/// layer
/// </summary>
// this was BaseAlternate in the old library
public class BaseTag : ControllerBase
{
	
	public string Property {
		get { return property; }
		set { property = value; }
	}
	
	public string Value {
		get { return _value; }
		set { _value = value; }
	}
	
	public string DoNotFilter {
		get { return doNotFilter; }
		set { doNotFilter = value; }
	}
	
	protected string propertyFallout (
		string inputTest
	) {
		if (string.IsNullOrEmpty(inputTest)) {
			return property; 
		}
		return inputTest;
	}
	
	protected string property = null;
	protected string _value = null;
	protected string name = null;
	protected string doNotFilter = null;
}
}
*/