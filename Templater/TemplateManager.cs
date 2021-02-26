using System;
using System.Runtime.Caching;

namespace com.janoserdelyi.PageBuilder.Templater
{
public class Manager
{
	//don't use the constructor
	internal Manager() {
		
	}
	
	public static Template GetTemplate (
		string templatePath,
		string templateName
	) {
		//lock(locker) {
		if (cache[templatePath + templateName] == null) {
			Template pageTemplate = null;
			
			try {
				pageTemplate = Template.Load (
					templatePath,
					templateName
				);
			} catch (Exception) {
				throw;
			}
			//System.Web.HttpContext.Current.Response.Write("<div>inserting '" + templatePath + templateName + "' into cache");
			//cache.Insert(templatePath + templateName, pageTemplate, new CacheDependency((templatePath + templateName).Replace('/', '\\')));
			CacheItemPolicy policy = new CacheItemPolicy();
			policy.AbsoluteExpiration = DateTime.Now.AddHours(1);
			System.Collections.Generic.List<string> pathsToMonitor = new System.Collections.Generic.List<string>();
			pathsToMonitor.Add(System.IO.Path.Combine(templatePath, templateName));
			policy.ChangeMonitors.Add(new HostFileChangeMonitor(pathsToMonitor));
			cache.Add(templatePath + templateName, pageTemplate, policy);
		}
		//}
			
		return (Template)cache[templatePath + templateName];
	}
	
	private static MemoryCache cache = new MemoryCache("cachemoney");
	//private static readonly object locker = new object();
}
}