using System;
using System.Collections.Generic;

namespace com.janoserdelyi.PageBuilder.TagSets
{
public class ControlUtils
{
	public static void RenderAttribute (
		IDictionary<string, object> valueCollection,
		System.Text.StringBuilder output,
		string nameOrId,
		string attributeName,
		string val
	) {
		RenderAttribute(valueCollection, output, nameOrId, attributeName, val, null);
	}
	
	public static void RenderAttribute (
		IDictionary<string, object> valueCollection,
		System.Text.StringBuilder output,
		string nameOrId,
		string attributeName,
		string val,
		string defaultIfNull
	) {
		
		string result = null;
		if (valueCollection == null) {
			Console.WriteLine("null context!");
		} else {
			//lookup by value collection passed in
			if (valueCollection.ContainsKey(nameOrId + ":" + attributeName)) {
				result = valueCollection[nameOrId + ":" + attributeName].ToString();
			}
		}
		
		if (val != null && result == null) {
			result = val;
		}
		
		if (result == null && defaultIfNull != null) {
			result = defaultIfNull;
		}
		
		if (!string.IsNullOrEmpty(result)) {
		//if (result != null) {
			//ok, real special case here. i don't want to show generated id's
			if (!(attributeName == "id" && val.StartsWith("element_"))) {
				output.Append(" ").Append(attributeName).Append("=\"").Append(result).Append("\"");
			}
		}
	}
	
	
	public static void SetSimpleProperty (
		object obj,
		string propertyName, 
		object proprtyValue
	) {
		if (obj == null) {
			throw new System.ArgumentNullException("No object specified for property assignment");
		}
		if (string.IsNullOrEmpty(propertyName)) {
			throw new ArgumentNullException("No property name specified.");
		}
				
		System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(Pascal(propertyName));
		if (pi == null) {
			throw new MissingMethodException("Unknown object property '" + propertyName + "'");
		}
		pi.SetValue(obj, proprtyValue, null);
	}
	
	public static string GetSimpleProperty (
		object obj,
		string propertyName
	) {
		System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(Pascal(propertyName));
		if (pi == null) {
			throw new MissingMethodException("Unknown property '" + propertyName + "'");
		}
		return pi.GetValue(obj, null).ToString();
		
	}
	
	private static string Pascal(string name) {
		return name.Substring(0, 1).ToUpper() + name.Substring(1);
	}
	
	#region filter
	/**
	* Filter the specified string for characters that are senstive to
	* HTML interpreters, returning the string with these characters replaced
	* by the corresponding character entities.
	*
	* @param value The string to be filtered and returned
	*/
	
	//this just seems ugly
	
	public static string Filter(string value) {
		
		if (value == null) {
			return (null);
		}
		
		char[] content = value.ToCharArray();
		System.Text.StringBuilder result = new System.Text.StringBuilder(); //content.Length + 50
		for (int i = 0; i < content.Length; i++) {
			switch (content[i]) {
				case '<':
					result.Append("&lt;");
					break;
				case '>':
					result.Append("&gt;");
					break;
				case '&':
					result.Append("&amp;");
					break;
				case '"':
					result.Append("&quot;");
					break;
				default:
					result.Append(content[i]);
					break;
			}
		}
		return (result.ToString());
	}
	#endregion
	
	public static string GetContextValue (
		System.Collections.Generic.IDictionary<string, object> context, 
		string nameOrId
	) {
		if (string.IsNullOrEmpty(nameOrId)) {
			return null;
		}
		
		if (context.ContainsKey(nameOrId) && context[nameOrId] != null) {
			return context[nameOrId].ToString();
		} 
		/*
		//otherwise try to return a property value
		try {
			return GetSimpleProperty(obj, property);
		} catch (ArgumentException) {
			throw new ArgumentException("Error retrieving property '" + property + "'");
		} catch (MissingMethodException) {
			throw new MissingMethodException("Unknown method on object '" + property + "'");
		} catch (MissingFieldException) {
			throw new MissingFieldException("Unknown field on object '" + property + "'");
		} catch (Exception oops) {
			throw new Exception("Unknown error looking up propert value on object for property '" + property + "'. " + oops.ToString());
		}
		*/
		
		return null;
	}
}
}