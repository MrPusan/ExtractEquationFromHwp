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
using ExtractingEquation;


namespace ExtractEquationFrHwp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

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

		private string ConvertHwp2Hml()
		{
			
			axHwpText.Run("MoveDocBegin");
			axHwpText.Run("MoveSelDocEnd");
			axHwpText.Run("Select");
			var quest = axHwpText.GetTextFile("HWPML2X", "saveblock");
			axHwpText.Run("MoveDocBegin");

			var eucKrEncoding = Encoding.GetEncoding("euc-kr");
			var utf8Encoding = Encoding.UTF8;

			string eucKrString = eucKrEncoding.GetString(eucKrEncoding.GetBytes(quest.ToString()));
			//var resultOfUtf8Bytes = utf8Encoding.GetBytes(eucKrString);
			string treatedText1 = eucKrString.Replace("?<?xml version=", "<?xml version=");
			string myText = treatedText1;
			return myText;
		}

		private void extractButton_Click(object sender, EventArgs e)
		{
			string inText = orginalText.Text;
			List<string> eqList = ExtractEq.ExEquation(inText);
			string outText = "";
			
			if (eqList != null)
			{
				for (int i = 0; i < eqList.Count; i++)
				{
					outText = outText + eqList[i] + "\n";
				}
			}
			else
			{
				outText = "추출된 데이터가 없습니다..";
			}

			extractedText.Text = outText;
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			orginalText.Clear();
			extractedText.Clear();
		}

		private void fromHML_Click(object sender, EventArgs e)
		{
			string myText = ConvertHwp2Hml();

			orginalText.Text = myText;

			List<string> eqList = ExtractEq.ExEquation(myText);
			string outText = "";

			if (eqList == null || eqList.Count<=0)
			{
				outText = "추출된 수식이 없습니다..";
			}
			else
			{
				for (int i = 0; i < eqList.Count; i++)
				{
					outText = outText + eqList[i] + "\n";
				}
			}

			extractedText.Text = outText;

		}

		private void ExtTextAndEq_Click(object sender, EventArgs e)
		{
			string myText = ConvertHwp2Hml();

			orginalText.Text = myText;

			List<string> eqList = ExtractEq.GetTextAndEquation(myText);
			string outText = "";

			if (eqList == null || eqList.Count <= 0)
			{
				outText = "추출된 수식이 없습니다..";
			}
			else
			{
				for (int i = 0; i < eqList.Count; i++)
				{
					string tempText = eqList[i];
					//MessageBox.Show(tempText);
					string tempText1 = tempText.Replace("`", "");	//<<<python과 연계 "it","~"
					tempText1 = tempText1.Replace("rm", "");
					outText = outText + tempText1 + "\n";
				}
			}

			extractedText.Text = outText;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			AxHwpPageSetup(axHwpText, 1);
		}
	}
	
}
