using System;
using System.IO;
using System.Xml;
using System.Collections;

namespace com.janoserdelyi.PageBuilder.Templater
{
public class CustomXmlResolver : System.Xml.XmlUrlResolver
{
	/*	this is really a very single-minded purpose here
		i want to look up the dtd in a document locally.
		
		so i'll make up a protocol designation for filtering out
		
		we'll try local://
		
		this is really being done because of a potential move to a host and i don't wish
		to look up every request externally, but i won't necessarily know the full local 
		path, plus it could always change
		
	*/
	public CustomXmlResolver() {
		//log("CustomXmlResolver constructor called");
	}
	
	override public object GetEntity (
		Uri absoluteUri, 
		string role, 
		Type ofObjectToReturn
	) {
		Stream stream = null;
		
		//log("CustomXmlResolver.GetEntity absoluteUri.Scheme : " + absoluteUri.Scheme);
		
		switch (absoluteUri.Scheme) {
			case "local" :
				
				//try to map the path to a local file
				/*
				log("CustomXmlResolver absoluteUri.AbsolutePath : " + absoluteUri.AbsolutePath);
				log("CustomXmlResolver absoluteUri.AbsoluteUri : " + absoluteUri.AbsoluteUri);
				log("CustomXmlResolver absoluteUri.LocalPath : " + absoluteUri.LocalPath);
				*/
				//string fullpath = System.IO.Path.GetDirectoryName(absoluteUri.LocalPath);
				
				string localpath = "";
				//if (System.Web.HttpContext.Current != null) {
				//	localpath = System.Web.HttpContext.Current.Request.MapPath(absoluteUri.LocalPath);
				//}
				string appDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
				Console.WriteLine("appDir : " + appDir);
				Console.WriteLine("absoluteUri.LocalPath : " + absoluteUri.LocalPath);
				localpath = System.IO.Path.Combine(appDir, absoluteUri.LocalPath);
				Console.WriteLine("localpath : " + localpath);
				
				//log("CustomXmlResolver System.IO.Path.GetDirectoryName(absoluteUri.LocalPath) : " + fullpath);
				//log("CustomXmlResolver localpath : " + localpath);
				
				if (System.IO.File.Exists(localpath)) {
					return (Stream)System.IO.File.OpenRead(localpath);
				}
				
				return stream;
			default:
				/*
				log("CustomXmlResolver absoluteUri.AbsolutePath : " + absoluteUri.AbsolutePath);
				log("CustomXmlResolver absoluteUri.AbsoluteUri : " + absoluteUri.AbsoluteUri);
				log("CustomXmlResolver absoluteUri.LocalPath : " + absoluteUri.LocalPath);
				*/
				try {
					stream = (Stream)base.GetEntity(absoluteUri, role, ofObjectToReturn);
				} catch (Exception oops) {
					throw new Exception("Error loading '" + absoluteUri + "'. " + oops.ToString());
				}
				return stream;
		}
	}
	
	private void log(string message) {
		/*
		com.janoserdelyi.Logging.SystemLog.WriteEntry(
			"PageTemplater.CustomXmlResolver", 
			message,
			new Exception(),
			Logging.LogEntryType.Information
		);
		*/
	}
}
}
