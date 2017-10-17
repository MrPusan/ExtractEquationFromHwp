using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
//HWP Equation Extraction lib
using ExtractingEquation;


namespace ExtractEquationFrHwp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		

		/// <summary>
		/// Initailize HWP Control
		/// </summary>
		/// <param name="Ax"></param>
		/// <param name="sUse"></param>
		private void AxHwpPageSetup(AxHWPCONTROLLib.AxHwpCtrl Ax, int sUse)
		{
			HWPCONTROLLib.HwpAction act = (HWPCONTROLLib.HwpAction)Ax.CreateAction("PageSetup");
			HWPCONTROLLib.HwpParameterSet set = (HWPCONTROLLib.HwpParameterSet)act.CreateSet();
			//HWPCONTROLLib.DHwpParameterArray pset= (HWPCONTROLLib.DHwpParameterArray)set.CreateItemSet("PageDef", "PageDef");

			act.GetDefault(set);
			set.SetItem("ApplyClass", 24);
			set.SetItem("ApplyTo", 3);

			HWPCONTROLLib.HwpParameterSet pset = (HWPCONTROLLib.HwpParameterSet)set.CreateItemSet("PageDef", "PageDef");

			if (sUse == 1)
			{
				pset.SetItem("PaperWidth", 32882); // 1mm=283.465 HWPUNITs ;;; 106mm+좌/우 여백 각 5mm=116mm

				pset.SetItem("PaperHeight", 103181);

				pset.SetItem("LeftMargin", 1400);
				pset.SetItem("RightMargin", 1400);
				pset.SetItem("TopMargin", 1400);
				pset.SetItem("HeaderLen", 0);
				pset.SetItem("BottomMargin", 1400);
				pset.SetItem("FooterLen", 0);
				pset.SetItem("GutterLen", 0);

				act.Execute(set);

			}
			else if (sUse == 2)  //B4 사이즈 기준
			{
				//pset.SetItem("PaperWidth", 72850);
				pset.SetItem("PaperWidth", 72850);
				//pset.SetItem("PaperWidth", 32882); // 1mm=283.465 HWPUNITs ;;; 106mm+좌/우 여백 각 5mm=116mm

				pset.SetItem("PaperHeight", 103181);

				//pset.SetItem("LeftMargin", 4252); //15mm To HWPUNITs
				pset.SetItem("LeftMargin", 5102);
				//pset.SetItem("RightMargin", 4252);
				pset.SetItem("RightMargin", 5102);
				pset.SetItem("TopMargin", 3401);
				pset.SetItem("HeaderLen", 4252);
				pset.SetItem("BottomMargin", 4252);
				pset.SetItem("FooterLen", 4252);
				pset.SetItem("GutterLen", 0);

				act.Execute(set);

			}
			//100% 보기 설정
			HWPCONTROLLib.HwpAction act1 = (HWPCONTROLLib.HwpAction)Ax.CreateAction("ViewZoom");
			HWPCONTROLLib.HwpParameterSet set1 = (HWPCONTROLLib.HwpParameterSet)act1.CreateSet();
			//HWPCONTROLLib.DHwpParameterArray pset= (HWPCONTROLLib.DHwpParameterArray)set.CreateItemSet("PageDef", "PageDef");

			act1.GetDefault(set1);

			set1.SetItem("ZoomType", 0);
			set1.SetItem("ZoomRatio", 100);

			act1.Execute(set1);

		}


		/// <summary>
		/// Convert HWP string to HML string
		/// </summary>
		/// <returns></returns>
		private string ConvertHwp2Hml()
		{
			
			axHwpText.Run("MoveDocBegin");
			axHwpText.Run("MoveSelDocEnd");
			axHwpText.Run("Select");
			var quest = axHwpText.GetTextFile("HWPML2X", "saveblock");
			axHwpText.Run("MoveDocBegin");

			var eucKrEncoding = Encoding.GetEncoding("euc-kr");
			//var utf8Encoding = Encoding.UTF8;

			string eucKrString = eucKrEncoding.GetString(eucKrEncoding.GetBytes(quest.ToString()));
			//var resultOfUtf8Bytes = utf8Encoding.GetBytes(eucKrString);
			string treatedText1 = eucKrString.Replace("?<?xml version=", "<?xml version=");
			string myText = treatedText1;
			return myText;
		}


		/// <summary>
		/// Clear Textbox
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void clearButton_Click(object sender, EventArgs e)
		{
			originalText.Clear();
			extractedText.Clear();
		}


		/// <summary>
		/// Extracting Equation only from HML
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void extractEquation_Click(object sender, EventArgs e)
		{
			string myText = ConvertHwp2Hml();

			//originalText.Text = myText;

			List<string> eqList = ExtractEq.ExtractEquation(myText);

			string outText = "";
			string inText = "";

			if (eqList == null || eqList.Count<=0)
			{
				outText = "추출된 수식이 없습니다..";
			}
			else
			{
				for (int i = 0; i < eqList.Count; i++)
				{
					inText = inText + eqList[i] + "\n";

					//Remove unuseful delimeters
					outText = outText + ExtractEq.removingDelimeterOfEq(eqList[i]) + "\n";
				}
			}
			originalText.Text = inText;
			extractedText.Text = outText;

		}


		/// <summary>
		/// Extracting Equation in first and foloowed TEXT from HML
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExtTextAndEq_Click(object sender, EventArgs e)
		{
			string myText = ConvertHwp2Hml();

			
			// Extracting EQUATION
			List<string> myListEq = ExtractEq.ExtractEquation(myText);
			
			string outText = "";
			string inText = "";
			if (myListEq == null || myListEq.Count <= 0)
			{
				outText = "추출된 수식이 없습니다..";
			}
			else
			{
				for (int i = 0; i < myListEq.Count; i++)
				{
					inText = inText + myListEq[i];

					//Remove unuseful delimeters
					outText = outText + ExtractEq.removingDelimeterOfEq(myListEq[i]) + "\n";

				}
			}

			// Extracting TEXT
			List<string> myListText = ExtractEq.ExtractText(myText);
			
			if (myListText == null || myListText.Count <= 0)
			{
				outText = outText + "====추출된 TEXT가 없습니다..=====";
			}
			else
			{
				for (int i = 0; i < myListText.Count; i++)
				{
					inText = inText + myListText[i];
					outText = outText + myListText[i] + "\n";

				}
			}

			originalText.Text = inText;
			extractedText.Text = outText;
		}


		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender, EventArgs e)
		{
			AxHwpPageSetup(axHwpText, 1);
		}
	}
	
}
