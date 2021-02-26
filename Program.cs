using System;
using System.Dynamic;
using System.IO;
using System.Text;
using com.janoserdelyi.PageBuilder.Templater;
using tags = com.janoserdelyi.PageBuilder.TagSets;

namespace com.janoserdelyi.PageBuilder
{
public class Program 
{
	public static void Main (
		string[] args
	) {
		Console.WriteLine("Small test harness time!");
		Console.WriteLine("-".PadRight(50, '-'));
		Console.WriteLine();
		/*
		// simulate a context collection
		System.Collections.Generic.IDictionary<string, object> context = new System.Collections.Generic.Dictionary<string, object>();
		context.Add("CoolId:title", "WHAT");
		context.Add("CoolId:style", "display:none;");
		context.Add("TheList:style", "display:block;");
		
		//ControllerBase div = new ControllerBase("div");
		//div.Id = "Coolness";
		IController div = new tags.Common.Div {
			TagType = "div",
			Id = "CoolId",
			ContextValues = context			
		};
		IController ul = new ControllerBase("ul");
		ul.Id = "TheList";
		div.AddChild(ul);
		
		StringBuilder sb = new StringBuilder();
		using (TextWriter tw = new StringWriter(sb)) {
			div.Render(tw);
		}
		*/
		/**/
		// straight test
		/*
		dynamic person = new DynamicDictionary();

        // Adding new dynamic properties. 
        // The TrySetMember method is called.
        person.FirstName = "Ellen";
        person.LastName = "Adams";

        // Getting values of the dynamic properties.
        // The TryGetMember method is called.
        // Note that property names are case-insensitive.
        Console.WriteLine(person.firstname + " " + person.lastname);

        // Getting the value of the Count property.
        // The TryGetMember is not called, 
        // because the property is defined in the class.
        Console.WriteLine("Number of dynamic properties:" + person.Count);
		*/
		
		string templatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates");
		const string templateName = "TestQsSignup.html";
		
		Template pageTemplate = Template.Load (
			templatePath,
			templateName
		);
		
		// this is where you'd inject the context
		// simulate a context collection
		System.Collections.Generic.IDictionary<string, object> context = new System.Collections.Generic.Dictionary<string, object>();
		context.Add("CoolId:title", "WHAT");
		context.Add("CoolId:style", "display:none;");
		context.Add("TheList:style", "display:block;");
		context.Add("Title", "Overridden Context Title!");
		context.Add("LiteralTest", "<a href=\"/\">Literal Link</a>");
		
		
		pageTemplate.AddContextValues(context);
		
		StringBuilder sb = new StringBuilder();
		using (TextWriter tw = new StringWriter(sb)) {
			pageTemplate.RenderControls(tw);
		}
		
		Console.WriteLine(sb.ToString());
		
		/*
		// test the caching template manager
		string templatePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates");
		string templateName = "index.html";
		
		Template pageTemplate = Templater.Manager.GetTemplate (
			templatePath,
			templateName
		);
		
		for (int i = 0; i < 10; i++) {
			// this is where you'd inject the context
			// simulate a context collection
			System.Collections.Generic.IDictionary<string, object> context = new System.Collections.Generic.Dictionary<string, object>();
			context.Add("CoolId:title", "WHAT");
			context.Add("CoolId:style", "display:none;");
			context.Add("TheList:style", "display:block;");
			context.Add("Title", "Overridden Context Title! " +  i.ToString());
			
			
			pageTemplate.AddContextValues(context);
			
			StringBuilder sb = new StringBuilder();
			using (TextWriter tw = new StringWriter(sb)) {
				pageTemplate.RenderControls(tw);
			}
			
			Console.WriteLine(sb.ToString());
			Console.WriteLine("-".PadRight(80, '-'));
			Console.WriteLine("\n\n");
		}
		*/	
		
		
		Console.WriteLine();
		Console.WriteLine("Press [enter] to exit");
		Console.ReadLine();
	}
}
}