namespace ExtractEquationFrHwp
{
	partial class Form1
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다. 
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.originalText = new System.Windows.Forms.RichTextBox();
			this.extractedText = new System.Windows.Forms.RichTextBox();
			this.clearButton = new System.Windows.Forms.Button();
			this.axHwpText = new AxHWPCONTROLLib.AxHwpCtrl();
			this.extractEquation = new System.Windows.Forms.Button();
			this.ExtTextAndEq = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.axHwpText)).BeginInit();
			this.SuspendLayout();
			// 
			// originalText
			// 
			this.originalText.Location = new System.Drawing.Point(12, 246);
			this.originalText.Name = "originalText";
			this.originalText.Size = new System.Drawing.Size(529, 243);
			this.originalText.TabIndex = 0;
			this.originalText.Text = "";
			// 
			// extractedText
			// 
			this.extractedText.Location = new System.Drawing.Point(547, 12);
			this.extractedText.Name = "extractedText";
			this.extractedText.Size = new System.Drawing.Size(428, 391);
			this.extractedText.TabIndex = 1;
			this.extractedText.Text = "";
			// 
			// clearButton
			// 
			this.clearButton.Location = new System.Drawing.Point(853, 409);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(121, 80);
			this.clearButton.TabIndex = 3;
			this.clearButton.Text = "Clear";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// axHwpText
			// 
			this.axHwpText.Enabled = true;
			this.axHwpText.Location = new System.Drawing.Point(11, 12);
			this.axHwpText.Name = "axHwpText";
			this.axHwpText.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axHwpText.OcxState")));
			this.axHwpText.Size = new System.Drawing.Size(530, 228);
			this.axHwpText.TabIndex = 4;
			// 
			// extractEquation
			// 
			this.extractEquation.Location = new System.Drawing.Point(547, 409);
			this.extractEquation.Name = "extractEquation";
			this.extractEquation.Size = new System.Drawing.Size(300, 38);
			this.extractEquation.TabIndex = 5;
			this.extractEquation.Text = "Extract Equation only From HWP";
			this.extractEquation.UseVisualStyleBackColor = true;
			this.extractEquation.Click += new System.EventHandler(this.extractEquation_Click);
			// 
			// ExtTextAndEq
			// 
			this.ExtTextAndEq.Location = new System.Drawing.Point(547, 451);
			this.ExtTextAndEq.Name = "ExtTextAndEq";
			this.ExtTextAndEq.Size = new System.Drawing.Size(300, 38);
			this.ExtTextAndEq.TabIndex = 6;
			this.ExtTextAndEq.Text = "Extract Text And Equation From HWP";
			this.ExtTextAndEq.UseVisualStyleBackColor = true;
			this.ExtTextAndEq.Click += new System.EventHandler(this.ExtTextAndEq_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(987, 509);
			this.Controls.Add(this.ExtTextAndEq);
			this.Controls.Add(this.extractEquation);
			this.Controls.Add(this.axHwpText);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.extractedText);
			this.Controls.Add(this.originalText);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			((System.ComponentModel.ISupportInitialize)(this.axHwpText)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox originalText;
		private System.Windows.Forms.RichTextBox extractedText;
		private System.Windows.Forms.Button clearButton;
		private AxHWPCONTROLLib.AxHwpCtrl axHwpText;
		private System.Windows.Forms.Button extractEquation;
		private System.Windows.Forms.Button ExtTextAndEq;
	}
}

