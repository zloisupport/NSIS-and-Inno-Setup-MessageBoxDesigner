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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PreviewForm));
			this.groupBoxCode = new GroupBox();
			this.textBoxCode = new TextBox();
			this.buttonBack = new Button();
			this.buttonQuit = new Button();
			this.buttonCopyAndQuit = new Button();
			this.groupBoxCode.SuspendLayout();
			base.SuspendLayout();
			this.groupBoxCode.Controls.Add(this.textBoxCode);
			this.groupBoxCode.Location = new Point(9, 10);
			this.groupBoxCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBoxCode.Name = "groupBoxCode";
			this.groupBoxCode.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBoxCode.Size = new System.Drawing.Size(676, 188);
			this.groupBoxCode.TabIndex = 0;
			this.groupBoxCode.TabStop = false;
			this.groupBoxCode.Text = "Generated code:";
			this.textBoxCode.Location = new Point(5, 17);
			this.textBoxCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.textBoxCode.Multiline = true;
			this.textBoxCode.Name = "textBoxCode";
			this.textBoxCode.Size = new System.Drawing.Size(668, 166);
			this.textBoxCode.TabIndex = 0;
			this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonBack.Location = new Point(10, 203);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new System.Drawing.Size(116, 22);
			this.buttonBack.TabIndex = 8;
			this.buttonBack.Text = "&Go Back (Edit data)";
			this.buttonBack.UseVisualStyleBackColor = true;
			this.buttonBack.Click += new EventHandler(this.buttonBack_Click);
			this.buttonQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonQuit.Location = new Point(610, 203);
			this.buttonQuit.Name = "buttonQuit";
			this.buttonQuit.Size = new System.Drawing.Size(75, 22);
			this.buttonQuit.TabIndex = 7;
			this.buttonQuit.Text = "&Quit";
			this.buttonQuit.UseVisualStyleBackColor = true;
			this.buttonQuit.Click += new EventHandler(this.buttonCancel_Click);
			this.buttonCopyAndQuit.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonCopyAndQuit.Location = new Point(259, 203);
			this.buttonCopyAndQuit.Name = "buttonCopyAndQuit";
			this.buttonCopyAndQuit.Size = new System.Drawing.Size(174, 22);
			this.buttonCopyAndQuit.TabIndex = 9;
			this.buttonCopyAndQuit.Text = "&Copy to Clipboard and Quit";
			this.buttonCopyAndQuit.UseVisualStyleBackColor = true;
			this.buttonCopyAndQuit.Click += new EventHandler(this.buttonCopyAndQuit_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.buttonBack;
			base.ClientSize = new System.Drawing.Size(694, 234);
			base.Controls.Add(this.buttonCopyAndQuit);
			base.Controls.Add(this.buttonBack);
			base.Controls.Add(this.buttonQuit);
			base.Controls.Add(this.groupBoxCode);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, FontStyle.Regular, GraphicsUnit.Pixel, 238);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;

			base.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "PreviewForm";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "unSigned's NSIS and Inno Setup MessageBox Designer";
			this.groupBoxCode.ResumeLayout(false);
			this.groupBoxCode.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}