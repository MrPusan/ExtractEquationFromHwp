using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;

namespace ExtractingEquation
{
    public class ExtractEq
    {
		public static List<string> ExtractEquation(string RawText)
		{
			XmlNodeList myXmlNode = null;
			List<string> resultList = new List<string>();

			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(RawText);
				
				myXmlNode = doc.SelectNodes("//EQUATION/SCRIPT");

				foreach (XmlNode node in myXmlNode)
				{
					resultList.Add(node.InnerText);
				}
				//return resultList;
			}
			catch (XmlException xe)
			{
				MessageBox.Show(String.Format("{0} - {1}", xe.LineNumber, xe.Message), "ExEquation");
				return null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Format("{0} - {1}", "197", ex.Message), "ExEquation");
				return null;
			}

			
			return resultList;

		}

		public static List<string> ExtractText(string RawText)
		{
			XmlNodeList myXmlNode = null;
			List<string> resultList = new List<string>();

			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(RawText);

				myXmlNode = doc.SelectNodes("//TEXT/CHAR");

				foreach (XmlNode node in myXmlNode)
				{
					resultList.Add(node.InnerText);
				}
				//return resultList;
			}
			catch (XmlException xe)
			{
				MessageBox.Show(String.Format("{0} - {1}", xe.LineNumber, xe.Message), "Text And ExEquation");
				return null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Format("{0} - {1}", "197", ex.Message), "Text And ExEquation");
				return null;
			}

			return resultList;
		}

		public static List<string> ExtractTxtAndEq(string RawText)
		{
			XmlNodeList myXmlNode = null;
			List<string> resultList = new List<string>();

			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(RawText);

				myXmlNode = doc.SelectNodes("//TEXT/CHAR | //EQUATION/SCRIPT");

				foreach (XmlNode node in myXmlNode)
				{
					resultList.Add(node.InnerText);
				}
				//return resultList;
			}
			catch (XmlException xe)
			{
				MessageBox.Show(String.Format("{0} - {1}", xe.LineNumber, xe.Message), "Text And ExEquation");
				return null;
			}
			catch (Exception ex)
			{
				MessageBox.Show(String.Format("{0} - {1}", "197", ex.Message), "Text And ExEquation");
				return null;
			}

			return resultList;
		}

	}
}
