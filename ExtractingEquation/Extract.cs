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
//ironpython Lib
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

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


		/// <summary>
		/// Remove unuseful delimeters in Equation
		/// </summary>
		/// <param name="rawEqation"></param>
		/// <returns></returns>
		public static string removingDelimeterOfEq(string rawEqation)
		{
			string resultEquation = "";

			string getAppPath = System.IO.Directory.GetCurrentDirectory();
			int index1;
			index1 = getAppPath.IndexOf("ExtractEquationFrHwp");
			int pathLength = getAppPath.Length;
			string Lpath;
			if (index1 > 0)
			{
				Lpath = getAppPath.Remove(index1, pathLength - index1);
			}
			else
			{
				Lpath = getAppPath;
			}
			string filePath = Lpath + "ExtractEquationFrHwp\\Delimeters4Equation.py";

			ScriptEngine engine = Python.CreateEngine();
			ScriptSource source = engine.CreateScriptSourceFromFile(filePath);
			ScriptScope scope = engine.CreateScope();

			ObjectOperations op = engine.Operations;

			source.Execute(scope); // class object created
			object classObject = scope.GetVariable("Delimiters4Equation"); // get the class object
			object instance = op.Invoke(classObject); // create the instance
			object method = op.GetMember(instance, "getDelimeters"); // get a method

			List<string> result = ((IList<object>)op.Invoke(method)).Cast<string>().ToList();

			string tempText = rawEqation;

			for (int i = 0; i < result.Count; i++)
			{

				tempText = tempText.Replace(result[i], "");
				//MessageBox.Show(tempText);
			}

			resultEquation = tempText;

			return resultEquation;
		}
	}
}
