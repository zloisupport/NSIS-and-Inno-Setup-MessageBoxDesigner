using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace MessageBoxDesigner
{
	public class PreviewForm : Form
	{
		private IContainer components;

		private GroupBox groupBoxCode;

		private TextBox textBoxCode;

		private Button buttonBack;

		private Button buttonQuit;

		private Button buttonCopyAndQuit;

		private string[] mCode;

		private bool mNoLink;
        private Button button1;
        private bool mInnoSetup;

		public PreviewForm(bool innoSetup, bool noLink, string[] code)
		{
			this.InitializeComponent();
			this.Font = SystemFonts.DefaultFont;
			this.mCode = code;
			this.mNoLink = noLink;
			this.mInnoSetup = innoSetup;
			this.textBoxCode.Lines = this.mCode;
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			this.mNoLink = true;
			this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.None;
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void buttonCopyAndQuit_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			for (int i = 0; i < (int)this.mCode.Length; i++)
			{
				empty = string.Concat(empty, Environment.NewLine, this.mCode[i]);
			}
			Clipboard.SetText(empty);

			Application.Exit();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.groupBoxCode = new System.Windows.Forms.GroupBox();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonCopyAndQuit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCode
            // 
            this.groupBoxCode.Controls.Add(this.textBoxCode);
            this.groupBoxCode.Location = new System.Drawing.Point(9, 10);
            this.groupBoxCode.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxCode.Name = "groupBoxCode";
            this.groupBoxCode.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxCode.Size = new System.Drawing.Size(676, 188);
            this.groupBoxCode.TabIndex = 0;
            this.groupBoxCode.TabStop = false;
            this.groupBoxCode.Text = "Generated code:";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(5, 17);
            this.textBoxCode.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCode.Multiline = true;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(668, 166);
            this.textBoxCode.TabIndex = 0;
            // 
            // buttonBack
            // 
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonBack.Location = new System.Drawing.Point(10, 203);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(116, 22);
            this.buttonBack.TabIndex = 8;
            this.buttonBack.Text = "&Go Back (Edit data)";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonQuit
            // 
            this.buttonQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonQuit.Location = new System.Drawing.Point(610, 203);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(75, 22);
            this.buttonQuit.TabIndex = 7;
            this.buttonQuit.Text = "&Quit";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonCopyAndQuit
            // 
            this.buttonCopyAndQuit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonCopyAndQuit.Location = new System.Drawing.Point(391, 203);
            this.buttonCopyAndQuit.Name = "buttonCopyAndQuit";
            this.buttonCopyAndQuit.Size = new System.Drawing.Size(174, 22);
            this.buttonCopyAndQuit.TabIndex = 9;
            this.buttonCopyAndQuit.Text = "&Copy to Clipboard and Quit";
            this.buttonCopyAndQuit.UseVisualStyleBackColor = true;
            this.buttonCopyAndQuit.Click += new System.EventHandler(this.buttonCopyAndQuit_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(171, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 22);
            this.button1.TabIndex = 10;
            this.button1.Text = "&Copy to Clipboard";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonBack;
            this.ClientSize = new System.Drawing.Size(694, 234);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCopyAndQuit);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonQuit);
            this.Controls.Add(this.groupBoxCode);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "unSigned\'s NSIS and Inno Setup MessageBox Designer";
            this.groupBoxCode.ResumeLayout(false);
            this.groupBoxCode.PerformLayout();
            this.ResumeLayout(false);

		}

        private void button1_Click(object sender, EventArgs e)
        {
            string empty = string.Empty;
            for (int i = 0; i < (int)this.mCode.Length; i++)
            {
                empty = string.Concat(empty, Environment.NewLine, this.mCode[i]);
            }
            Clipboard.SetText(empty);
        }
    }
}