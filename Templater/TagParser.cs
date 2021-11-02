using System;
//using System.Reflection;
//
using System.Xml;

using x1 = com.janoserdelyi.PageBuilder.TagSets.XHTML_1;
using x5 = com.janoserdelyi.PageBuilder.TagSets.XHTML_5;
using commonBase = com.janoserdelyi.PageBuilder.TagSets.Common;

namespace com.janoserdelyi.PageBuilder.Templater
{
	public class TagParser
	{
		#region constructors
		public TagParser () {
			currentAssemblyName = System.Reflection.Assembly.GetExecutingAssembly ().FullName;
			IdList = new System.Collections.Generic.Dictionary<string, string> ();
		}
		#endregion

		#region public methods

		public void Parse (

		) {
			Parse (true);
		}

		public void Parse (
			bool prependDoctype
		) {
			parseStart = DateTime.Now;

			//this is going to be ghetto, but here goes.
			//if there is no doctype, then use html5
			//default to xhtml 1
			TagFamily tagFamily = TagFamily.XHTML_1;

			if (template.DocumentType == null || (template.DocumentType.Name == "html" && string.IsNullOrEmpty (template.DocumentType.PublicId)) || template.DocumentType.OuterXml == "<!DOCTYPE html>") { //it is optional in XHTML5 to use the <!DOCTYPE html>. it *must* be in this casing
				tagFamily = TagFamily.XHTML_5;
				if (prependDoctype) {
					rootControl.AddChild (new com.janoserdelyi.PageBuilder.TagSets.XHTML_5.DocType ()); //there is really only one doctype for XHTML5
				}
			} else if (template.DocumentType.OuterXml.ToLower ().Contains ("xhtml 1.0") || template.DocumentType.OuterXml.ToLower ().Contains ("xhtml 1.1")) {
				tagFamily = TagFamily.XHTML_1;
				//go ahead and add the doctype now
				com.janoserdelyi.PageBuilder.TagSets.XHTML_1.DocType docType = new com.janoserdelyi.PageBuilder.TagSets.XHTML_1.DocType ();
				docType.Type = "xhtml 1.0 strict"; //this really is a shortcut. i'm not happy with the doctype tag implementation.
				if (prependDoctype) {
					rootControl.AddChild (docType);
				}
			}

			//now really start drilling
			//get the root element and begin translating
			XmlNode documentElement = template.DocumentElement;
			IController documentElementControl = null;
			//parse the root, then continue down the chain
			try {
				documentElementControl = translateNode (documentElement, tagFamily);
				rootControl.AddChild (documentElementControl);
			} catch (Exception) {
				throw;
			}

			if (documentElementControl == null) {
				throw new System.NullReferenceException ("Error, unable to establish root control for template.");
			}

			translateChildNodes (documentElement, documentElementControl, tagFamily);

			parseDuration = DateTime.Now - parseStart;
			// add a comment at the bottom

			if (Verbose) {
				documentElementControl.AddChild (new commonBase.Literal ($"\n\n<!-- {parseStart.ToString ()} : {parseDuration.TotalMilliseconds.ToString ()}ms -->"));
			}
		}
		#endregion

		#region private methods
		private void translateChildNodes (
			XmlNode node,
			IController control,
			TagFamily tagFamily
		) {
			if (node.ChildNodes == null || node.ChildNodes.Count == 0) {
				return;
			}

			//2011-07-26 janos. this may slow things down but i need some locking on the control collection

			foreach (XmlNode childNode in node.ChildNodes) {
				if (childNode.NodeType == System.Xml.XmlNodeType.Text) {
					//this is taking things like &amp; and killing them down to &. i need to preserve &amp;
					//what this means is that all <style> and <script> blocks should be <![CDATA[ ... ]]>
					string input = System.Net.WebUtility.HtmlEncode (childNode.InnerText);
					if (!string.IsNullOrEmpty (input)) {
						// TODO: take a look at these locks again
						//lock (control.Controls.SyncRoot) {
						control.AddChild (new commonBase.Literal (input));
						//}
					}
				}
				if (childNode.NodeType == System.Xml.XmlNodeType.EntityReference && !string.IsNullOrEmpty (childNode.InnerText)) {
					// TODO: take a look at these locks again
					//lock (control.Controls.SyncRoot) {
					control.AddChild (new commonBase.Literal (childNode.InnerText));
					//}
					/*	i'm not entirely satisfied with this
						i initially made this because nodes with just a &nbsp; in them were not 
						passing anything along and ending up as empty nodes
						in this state, it's converting the entity to the display type.
						in this case, '&nbsp;' becomes ' ' on the page
					*/
				}
				//ahh yes. added this because of some pesky content within some <script> nodes. 
				//i had to CDATA inside the node, but then it wasn't being rendered. jolly.
				if (childNode.NodeType == System.Xml.XmlNodeType.CDATA && !string.IsNullOrEmpty (childNode.Value)) {
					// TODO: take a look at these locks again
					//lock (control.Controls.SyncRoot) {
					string content = childNode.Value;
					// 2017-03-20 janos. long overdue. i want to strip block comments from items in CDATA (css and js)
					content = System.Text.RegularExpressions.Regex.Replace (content, @"/\*(.*?)\*/", "", System.Text.RegularExpressions.RegexOptions.Singleline);
					control.AddChild (new commonBase.Literal (content));
					//}
				}

				if (childNode.NodeType != System.Xml.XmlNodeType.Element) {
					//continue;
				}

				if (childNode.Name == "option") {
					Console.WriteLine ("foo");
				}

				if (childNode.NodeType == System.Xml.XmlNodeType.Element) {
					IController childControl = translateNode (childNode, tagFamily, parentController: control);
					// TODO: take a look at these locks again
					//lock (control.Controls.SyncRoot) {
					control.AddChild (childControl);
					//}
					translateChildNodes (childNode, childControl, tagFamily);
				}
			}
		}

		private IController translateNode (
			XmlNode node,
			TagFamily tagFamily,
			IController parentController = null
		) {

			//not all elements have an id, but i need to set one to keep everyone happy
			System.Threading.Interlocked.Increment (ref this.elementCounter);

			IController obj = null;

			//yeah, this is ugly, but not all types have the appropriate name
			string typeName = null;

			#region XHTML 1.0 tag translation
			if (tagFamily == TagFamily.XHTML_1) {
				switch (node.LocalName.ToLower ()) {
					case "html":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_1.Html";
						obj = new x1.Html ();
						break;
					case "head":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Head";
						obj = new commonBase.Head ();
						break;
					case "title":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Title";
						obj = new commonBase.Title ();
						break;
					case "body":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Body";
						obj = new commonBase.Body ();
						break;
					case "meta":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Meta";
						obj = new commonBase.Meta ();
						break;
					case "link":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Link";
						obj = new commonBase.Link ();
						break;
					case "style":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Style";
						obj = new commonBase.Style ();
						break;
					case "script":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Javascript";
						obj = new commonBase.Javascript ();
						break;
					case "div":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Div";
						obj = new commonBase.Div ();
						break;
					case "a":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.A";
						obj = new commonBase.A ();
						break;

					case "img":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Img";
						obj = new commonBase.Img ();
						break;

					case "hr":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Hr";
						obj = new commonBase.Hr ();
						break;

					case "iframe":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.IFrame";
						obj = new commonBase.IFrame ();
						break;

					case "table":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Table";
						obj = new commonBase.Table ();
						break;
					case "tr":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Tr";
						obj = new commonBase.Tr ();
						break;
					case "th":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Th";
						obj = new commonBase.Th ();
						break;
					case "td":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Td";
						obj = new commonBase.Td ();
						break;

					case "li":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Li";
						obj = new commonBase.Li ();
						break;

					case "form":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_1.Form";
						obj = new commonBase.Form ();
						break;
					case "select":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Select";
						obj = new commonBase.Select ();
						break;
					case "datalist":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Datalist";
						obj = new commonBase.Datalist ();
						break;
					case "option":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Option";
						obj = new commonBase.Option (parentController: parentController);
						break;
					case "optgroup":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Optgroup";
						obj = new commonBase.OptGroup ();
						break;
					case "label":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Label";
						obj = new commonBase.Label ();
						break;
					case "textarea":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Textarea";
						obj = new commonBase.Textarea ();
						break;
					case "button":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_1.Button";
						obj = new x1.Button ();
						break;
					case "email":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Email";
						obj = new commonBase.Email ();
						break;

					case "embed":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Embed";
						obj = new commonBase.Embed ();
						break;

					case "object":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Object";
						obj = new commonBase.Object ();
						break;
					case "param":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Param";
						obj = new commonBase.Param ();
						break;

					case "map":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Map";
						obj = new commonBase.Map ();
						break;
					case "area":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Area";
						obj = new commonBase.Area ();
						break;

					case "canvas":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Canvas";
						obj = new commonBase.Canvas ();
						break;

					case "literal":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Literal";
						obj = new commonBase.Literal ();
						break;




					/*	this could get ugly
						because there area a few types of input.
						text
						radio
						password
						checkbox
					*/
					case "input":
						typeName = "FormInput";
						break;
				}


				//i need to do a seperate attribute scan for certain form elements just to determine what they are
				//scan for 'type'
				if (typeName == "FormInput") {
					typeName = null; //allow for the null fall-through to try to set this as BaseBasicTag
					if (node.Attributes != null && node.Attributes.Count > 0) {
						foreach (System.Xml.XmlAttribute attribute in node.Attributes) {
							//weird attributes showing up through xml parsing, but not something written in the source document
							if (attribute.LocalName.ToLower () == "type" && attribute.Value != null) {
								//there's both a <button> tag and a <input type="button"/> bah!
								//we want the <input type="button"/> here
								string mod = "";
								if (attribute.Value == "button" || attribute.Value == "file" || attribute.Value == "submit") {
									mod = "Input";
								}
								typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_1." + uppercaseFirst (cleanPropertyName (attribute.Value)) + mod;
								break;
							} else {
								continue;
							}
						}
					}
				}

				if (typeName == null) {
					typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.BasicTag";
					obj = new commonBase.BasicTag (node.LocalName);
				}

				if (obj == null) {
					try {
						//obj = (IController)Activator.CreateInstance(currentAssemblyName, typeName).Unwrap();
						obj = (IController)(Activator.CreateInstance (Type.GetType (typeName))); // 2019-01-07. converting to netstandard 2.0. hrmmm. what about Unwrap()
					} catch (Exception) {
						throw new Exception ("error, unable to create instance of '" + typeName + "'");
					}
				}

				//now try to set attributes
				if (node.Attributes != null && node.Attributes.Count > 0) {
					bool hasAnId = false;
					foreach (System.Xml.XmlAttribute attribute in node.Attributes) {
						string localNameLower = attribute.LocalName.ToLower ();

						//weird attributes showing up through xml parsing, but not something written in the source doucment
						if (
							localNameLower == "space" ||
							(localNameLower == "shape" && node.LocalName.ToLower () != "area")
						) {
							continue;
						}

						//ok, this drives me nuts.
						//since you cannot be case-sensitive when going from the code front to the code behind, it can cause problems
						string attributeLocalName = checkAttributeForEvent (attribute.LocalName);

						try {
							//having issue on <param>'s
							if (attributeLocalName.ToLower () == "valuetype") { //i have no idea how this is coming out
								attributeLocalName = "value";
							}

							//not the most ideal dependency point
							com.janoserdelyi.PageBuilder.TagSets.ControlUtils.SetSimpleProperty (
								obj,
								uppercaseFirst (cleanPropertyName (attributeLocalName)),
								attribute.Value
							);

							if (attributeLocalName.ToLower () == "id") {// && attribute.Value != "") {
								hasAnId = true;
								// 2017-10-13 it's time. finally time i remembered to deal with this
								if (IdList.ContainsKey (attribute.Value) && !(obj is commonBase.Literal)) {
									// this eventually needs to be an exploding death error
									Console.ForegroundColor = ConsoleColor.Red;
									Console.WriteLine ("THERE IS ALREADY AN OBJECT WITH ID '" + attribute.Value + "' - DON'T DO THIS. this will eventually cause a fatal error.");
									Console.ResetColor ();
								} else {
									if (obj is commonBase.Literal && IdList.ContainsKey (attribute.Value)) {
										continue;
									}
									IdList.Add (attribute.Value, null); // not putting anything like obj in here. don't need it for anything yet and will keep mem footprint down
								}
							}
						} catch (Exception wtf) {
							throw new Exception ("error setting property '" + cleanPropertyName (attributeLocalName) + "' with value '" + attribute.Value + "' on object '" + typeName.ToString () + "'. " + wtf.ToString ());
						}
					}

					//if (hasAnId == false || obj.Id == null) {
					if (hasAnId == false) {
						//blank it out? on many of the controls, i have a blank id not render the id=""
						obj.Id = "element_" + this.elementCounter.ToString () + "_" + DateTime.Now.Ticks.ToString ();

						// another attempt to prevetn id collision
						//obj.Id = "element_" + DateTime.Now.Ticks.ToString();
					}
				}
			}
			#endregion

			#region (x)HTML5 tag translation
			if (tagFamily == TagFamily.XHTML_5) {
				switch (node.LocalName.ToLower ()) {
					case "html":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Html";
						obj = new x5.Html ();
						break;
					case "head":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Head";
						obj = new commonBase.Head ();
						break;
					case "title":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Title";
						obj = new commonBase.Title ();
						obj.Value = node.InnerText;
						break;
					case "body":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Body";
						obj = new commonBase.Body ();
						break;
					case "meta":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Meta";
						obj = new x5.Meta ();
						break;
					case "link":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Link";
						obj = new commonBase.Link ();
						break;
					case "style":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Style";
						obj = new commonBase.Style ();
						break;
					case "script":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Javascript";
						obj = new commonBase.Javascript ();
						break;
					case "div":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Div";
						obj = new commonBase.Div ();
						break;
					case "br":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Br";
						obj = new x5.Br ();
						break;
					case "a":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.A";
						obj = new commonBase.A ();
						break;

					case "img":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Img";
						obj = new commonBase.Img ();
						break;

					case "hr":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Hr";
						obj = new commonBase.Hr ();
						break;

					case "iframe":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.IFrame";
						obj = new commonBase.IFrame ();
						break;

					case "table":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Table";
						obj = new commonBase.Table ();
						break;
					case "tr":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Tr";
						obj = new commonBase.Tr ();
						break;
					case "th":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Th";
						obj = new commonBase.Th ();
						break;
					case "td":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Td";
						obj = new commonBase.Td ();
						break;

					case "li":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Li";
						obj = new commonBase.Li ();
						break;

					case "form":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Form";
						obj = new commonBase.Form ();
						break;
					case "select":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Select";
						obj = new commonBase.Select ();
						break;
					case "datalist":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Datalist";
						obj = new commonBase.Datalist ();
						break;
					case "option":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Option";
						obj = new commonBase.Option (parentController: parentController);
						break;
					case "optgroup":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Optgroup";
						obj = new commonBase.OptGroup ();
						break;
					case "label":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Label";
						obj = new commonBase.Label ();
						break;
					case "textarea":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Textarea";
						obj = new commonBase.Textarea ();
						break;
					case "button":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Button";
						obj = new x5.Button ();
						break;
					case "email":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Email";
						obj = new commonBase.Email ();
						break;

					case "embed":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Embed";
						obj = new commonBase.Embed ();
						break;

					case "object":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Object";
						obj = new commonBase.Object ();
						break;
					case "param":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Param";
						obj = new commonBase.Param ();
						break;

					case "map":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Map";
						obj = new commonBase.Map ();
						break;
					case "area":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Area";
						obj = new commonBase.Area ();
						break;

					case "canvas":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Canvas";
						obj = new commonBase.Canvas ();
						break;

					case "literal":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Literal";
						obj = new commonBase.Literal ();
						break;

					case "section":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Section";
						obj = new x5.Section ();
						break;
					case "article":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Article";
						obj = new x5.Article ();
						break;
					case "hgroup":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Hgroup";
						obj = new x5.Hgroup ();
						break;

					case "video":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Video";
						obj = new x5.Video ();
						break;
					case "source":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5.Source";
						obj = new x5.Source ();
						break;

					case "pre":
						typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.Pre";
						obj = new commonBase.Pre ();
						break;

					/*	this could get ugly
						because there area a few types of input.
						text
						radio
						password
						checkbox
					*/
					case "input":
						typeName = "FormInput";
						break;
				}

				//i need to do a seperate attribute scan for certain form elements just to determine what they are
				//scan for 'type'
				if (typeName == "FormInput") {
					typeName = null; //allow for the null fall-through to try to set this as BaseBasicTag
					if (node.Attributes != null && node.Attributes.Count > 0) {
						foreach (System.Xml.XmlAttribute attribute in node.Attributes) {
							//weird attributes showing up through xml parsing, but not something written in the source document
							if (attribute.LocalName.ToLower () == "type" && attribute.Value != null) {
								//there's both a <button> tag and a <input type="button"/> bah!
								//we want the <input type="button"/> here
								string mod = "Input";
								//taking a different approach than the xhtml_1 branch. everything will start with "Input" unless otherwise specified
								if (attribute.Value == "radio") {
									mod = "";
								}
								typeName = "com.janoserdelyi.PageBuilder.TagSets.XHTML_5." + mod + uppercaseFirst (cleanPropertyName (attribute.Value));
								break;
							} else {
								continue;
							}
						}
					}
				}

				if (typeName == null) {
					typeName = "com.janoserdelyi.PageBuilder.TagSets.Common.BasicTag";
					obj = new commonBase.BasicTag (node.LocalName);
				}

				if (obj == null) {
					try {
						//obj = (IController)Activator.CreateInstance(currentAssemblyName, typeName).Unwrap();
						obj = (IController)(Activator.CreateInstance (Type.GetType (typeName))); // 2019-01-07. converting to netstandard 2.0. hrmmm. what about Unwrap()
					} catch (Exception) {
						throw new Exception ("error, unable to create instance of '" + typeName + "'");
					}
					/*
					if (typeName == "com.janoserdelyi.PageBuilder.TagSets.Common.BasicTag") {
						//set the tag type
						com.janoserdelyi.PageBuilder.TagSets.Utils.ControlUtils.SetSimpleProperty (
							obj,
							"Type",
							node.LocalName
						);
					}
					*/
				}

				bool isControllerBase = (obj is ControllerBase);

				//now try to set attributes. this is a copy of the xhtml 1.0 section. so it may get changes
				if (node.Attributes != null && node.Attributes.Count > 0) {
					bool hasAnId = false;
					foreach (System.Xml.XmlAttribute attribute in node.Attributes) {
						string attributeLocalName = attribute.LocalName;
						string localNameLower = attributeLocalName.ToLower ();

						//weird attributes showing up through xml parsing, but not something written in the source doucment
						if (
							localNameLower == "space" ||
							(localNameLower == "shape" && node.LocalName.ToLower () != "area")
						) {
							continue;
						}

						// 2017-03-24 janos. time to attempt data-* attributes
						if (localNameLower.StartsWith ("data-")) {
							//Console.WriteLine("skipping attribute '" + localNameLower + "' with value '" + attribute.Value + "'");
							if (isControllerBase) {
								((ControllerBase)obj).DataAttributes[localNameLower] = attribute.Value;
							}
							continue;
						}
						// 2017-04-10 aria-* attributes
						if (localNameLower.StartsWith ("aria-")) {
							//Console.WriteLine("skipping attribute '" + localNameLower + "' with value '" + attribute.Value + "'");
							if (isControllerBase) {
								((ControllerBase)obj).AriaAttributes[localNameLower] = attribute.Value;
							}
							continue;
						}

						//ok, this drives me nuts.
						//since you cannot be case-sensitive when going from the code front to the code behind, it can cause problems
						//string attributeLocalName = checkAttributeForEvent(attribute.LocalName);

						try {
							//having issue on <param>'s
							if (localNameLower == "valuetype") { //i have no idea how this is coming out
								attributeLocalName = "value";
							}

							//not the most ideal dependency point
							com.janoserdelyi.PageBuilder.TagSets.ControlUtils.SetSimpleProperty (
								obj,
								uppercaseFirst (cleanPropertyName (attributeLocalName)),
								attribute.Value
							);

							if (localNameLower == "id") {// && attribute.Value != "") {
								hasAnId = true;
								// 2017-10-13 it's time. finally time i remembered to deal with this
								if (IdList.ContainsKey (attribute.Value) && !(obj is commonBase.Literal)) {
									// this eventually needs to be an exploding death error
									Console.ForegroundColor = ConsoleColor.Red;
									Console.WriteLine ("THERE IS ALREADY AN OBJECT WITH ID '" + attribute.Value + "' - DON'T DO THIS. this will eventually cause a fatal error.");
									Console.ResetColor ();
								} else {
									if (obj is commonBase.Literal && IdList.ContainsKey (attribute.Value)) {
										continue;
									}
									IdList.Add (attribute.Value, null); // not putting anything like obj in here. dno't need it for anything yet and will keep mem footprint down
								}
							}
						} catch (Exception wtf) {
							throw new Exception ("error setting property '" + cleanPropertyName (attributeLocalName) + "' with value '" + attribute.Value + "' on object '" + typeName.ToString () + "'. " + wtf.ToString ());
						}
					}

					//if (hasAnId == false || obj.Id == null) {
					if (hasAnId == false) {
						//blank it out? on many of the controls, i have a blank id not render the id=""
						obj.Id = "element_" + this.elementCounter.ToString () + "_" + DateTime.Now.Ticks.ToString ();

						// another attempt to prevetn id collision
						//obj.Id = "element_" + DateTime.Now.Ticks.ToString();
					}
				}
			}
			#endregion

			return (IController)obj;
		}

		private string uppercaseFirst (
			string input
		) {
			if (input == null) {
				return null;
			}
			if (input == String.Empty) {
				return "";
			}
			if (input.Length == 1) {
				return input.ToUpper ();
			}

			string firstChar = input.Substring (0, 1);
			return firstChar.ToUpper () + input.Substring (1);
		}

		private string cleanPropertyName (
			string input
		) {
			//this is a rare case, but a real one nonetheless
			if (input == null) {
				return null;
			}
			if (input == String.Empty) {
				return "";
			}

			/*	i tend to be pretty consistent about removing odd characters
				from attributes which will end up being class properties, and pascal/camel-casing 
				off of them.
				examples:
					xml:lang --> XmlLang
					http-equiv -- > HttpEquiv
				so i'm going to go ahead and do that here
			*/

			string illegalCharacters = ":-";

			//input = input.Replace(":", "");
			input = camelIllegalCharacters (illegalCharacters, input);

			/*	this drives me nuts to have to do this, but because some programmers at MS
				were not consistent with Pascal casing on some properties, this is necessary.
				namely - 'Id' should be 'Id'.
			*/
			if (input.ToLower () == "id") {
				return "Id";
			}

			return input;
		}

		/*	this is checking for javascript events and casing them properly 
			for the object. stuff like 'onclick' needs to be 'OnClick' for reflection

			// i changed this to just First character uppercase
		*/
		private string checkAttributeForEvent (string input) {
			if (input == null) {
				return null;
			}
			if (input == String.Empty) {
				return "";
			}
			switch (input.ToLower ()) {
				case "onclick":
					input = "OnClick";
					break;
				case "ondblclick":
					input = "OnDblClick";
					break;
				case "onmousedown":
					input = "OnMouseDown";
					break;
				case "onmouseup":
					input = "OnMouseUp";
					break;
				case "onmousemove":
					input = "OnMouseMove";
					break;
				case "onmouseover":
					input = "OnMouseOver";
					break;
				case "onmouseout":
					input = "OnMouseOut";
					break;
				case "onkeydown":
					input = "OnKeyDown";
					break;
				case "onkeyup":
					input = "OnKeyUp";
					break;
				case "onkeypress":
					input = "OnKeyPress";
					break;
				case "onchange":
					input = "OnChange";
					break;
				case "onselect":
					input = "OnSelect";
					break;
				case "onblur":
					input = "OnBlur";
					break;
				case "onfocus":
					input = "OnFocus";
					break;
			}
			return input;
		}

		private string camelIllegalCharacters (
			string illegalCharacters,
			string input
		) {

			for (int i = 0; i < illegalCharacters.Length; i++) {
				if (input.IndexOf (illegalCharacters[i]) == -1) {
					continue;
				}
				//run through the input in case there are multiple? nah, not now
				int charPosition = input.IndexOf (illegalCharacters[i]);
				//remove it
				input = input.Replace (illegalCharacters[i].ToString (), "");
				if (input.Length > charPosition) {
					input = input.Substring (0, charPosition) + input.Substring (charPosition, 1).ToUpper () + input.Substring (charPosition + 1);
				}
			}

			return input;
		}
		#endregion

		#region public properties
		public System.Xml.XmlDocument Template {
			get { return template; }
			set { template = value; }
		}

		public RootControl RootControl {
			get { return rootControl; }
			set { rootControl = value; }
		}

		public TimeSpan ParseDuration {
			get { return parseDuration; }
		}

		public System.Collections.Generic.IDictionary<string, string> IdList { get; set; }
		public bool Verbose { get; set; } = true;
		#endregion

		#region private declarations
		private string currentAssemblyName;
		private int elementCounter;
		private XmlDocument template;
		private RootControl rootControl;
		private DateTime parseStart;
		private TimeSpan parseDuration;
		#endregion
	}
}