using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Irony.Parsing;

namespace IronyGrammarSample
{
    public static class ParseTreeExtensions
    {
        public static string ToXml(this ParseTree parseTree)
        {
            if (parseTree == null || parseTree.Root == null)
                return string.Empty;
            XmlDocument xmlDocument = parseTree.ToXmlDocument();
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter1 = new XmlTextWriter((TextWriter)stringWriter);
            xmlTextWriter1.Formatting = Formatting.Indented;
            XmlTextWriter xmlTextWriter2 = xmlTextWriter1;
            xmlDocument.WriteTo((XmlWriter)xmlTextWriter2);
            xmlTextWriter1.Flush();
            return stringWriter.ToString();
        }

        public static XmlDocument ToXmlDocument(this ParseTree parseTree)
        {
            XmlDocument ownerDocument = new XmlDocument();
            if (parseTree == null || parseTree.Root == null)
                return ownerDocument;
            XmlElement element = ownerDocument.CreateElement("ParseTree");
            ownerDocument.AppendChild((XmlNode)element);
            XmlElement xmlElement = parseTree.Root.ToXmlElement(ownerDocument);
            element.AppendChild((XmlNode)xmlElement);
            return ownerDocument;
        }

        public static XmlElement ToXmlElement(this ParseTreeNode node, XmlDocument ownerDocument)
        {
            XmlElement element = ownerDocument.CreateElement("Node");
            element.SetAttribute("Term", node.Term.Name);
            BnfTerm term = node.Term;
            if (term.HasAstConfig() && term.AstConfig.NodeType != (Type)null)
                element.SetAttribute("AstNodeType", term.AstConfig.NodeType.Name);
            if (node.Token != null)
            {
                element.SetAttribute("Terminal", node.Term.GetType().Name);
                if (node.Token.Value != null)
                    element.SetAttribute("Value", node.Token.Value.ToString());
            }
            else
            {
                foreach (ParseTreeNode childNode in (List<ParseTreeNode>)node.ChildNodes)
                {
                    XmlElement xmlElement = childNode.ToXmlElement(ownerDocument);
                    element.AppendChild((XmlNode)xmlElement);
                }
            }
            return element;
        }
    }
}