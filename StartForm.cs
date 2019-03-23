using MessageBoxDesigner.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace MessageBoxDesigner
{
	public class StartForm : Form
	{
		private bool mInnoSetup;

		private bool mNoPreview;

		private bool mNoCopy;

		private bool mNoLink;

		private IContainer components;

		private GroupBox groupBoxStart;

		private RadioButton radioButtonInnoSetup;

		private RadioButton radioButtonNSIS;

		private PictureBox pictureBoxInnoSetup;

		private PictureBox pictureBoxNSIS;

		private Button buttonStart;

		public StartForm(bool innoSetup, bool noPreview, bool noCopy, bool noLink)
		{
			this.InitializeComponent();
			this.Font = SystemFonts.DefaultFont;
			this.mInnoSetup = innoSetup;
			this.mNoPreview = noPreview;
			this.mNoCopy = noCopy;
			this.mNoLink = noLink;
			this.radioButtonNSIS.Checked = !this.mInnoSetup;
			this.radioButtonInnoSetup.Checked = this.mInnoSetup;
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			if (!this.radioButtonInnoSetup.Checked)
			{
				this.mInnoSetup = false;
			}
			else
			{
				this.mInnoSetup = true;
			}
			MsgBoxForm msgBoxForm = new MsgBoxForm(this.mInnoSetup, this.mNoPreview, this.mNoCopy, this.mNoLink);
			msgBoxForm.ShowDialog();
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
            this.groupBoxStart = new System.Windows.Forms.GroupBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.radioButtonInnoSetup = new System.Windows.Forms.RadioButton();
            this.radioButtonNSIS = new System.Windows.Forms.RadioButton();
            this.pictureBoxInnoSetup = new System.Windows.Forms.PictureBox();
            this.pictureBoxNSIS = new System.Windows.Forms.PictureBox();
            this.groupBoxStart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInnoSetup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNSIS)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxStart
            // 
            this.groupBoxStart.Controls.Add(this.buttonStart);
            this.groupBoxStart.Controls.Add(this.radioButtonInnoSetup);
            this.groupBoxStart.Controls.Add(this.radioButtonNSIS);
            this.groupBoxStart.Controls.Add(this.pictureBoxInnoSetup);
            this.groupBoxStart.Controls.Add(this.pictureBoxNSIS);
            this.groupBoxStart.Location = new System.Drawing.Point(9, 10);
            this.groupBoxStart.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxStart.Name = "groupBoxStart";
            this.groupBoxStart.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxStart.Size = new System.Drawing.Size(298, 146);
            this.groupBoxStart.TabIndex = 2;
            this.groupBoxStart.TabStop = false;
            this.groupBoxStart.Text = "Select installation system:";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(210, 111);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(80, 26);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "&Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // radioButtonInnoSetup
            // 
            this.radioButtonInnoSetup.AutoSize = true;
            this.radioButtonInnoSetup.Checked = true;
            this.radioButtonInnoSetup.Location = new System.Drawing.Point(66, 80);
            this.radioButtonInnoSetup.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonInnoSetup.Name = "radioButtonInnoSetup";
            this.radioButtonInnoSetup.Size = new System.Drawing.Size(77, 17);
            this.radioButtonInnoSetup.TabIndex = 5;
            this.radioButtonInnoSetup.TabStop = true;
            this.radioButtonInnoSetup.Text = "&Inno Setup";
            this.radioButtonInnoSetup.UseVisualStyleBackColor = true;
            // 
            // radioButtonNSIS
            // 
            this.radioButtonNSIS.AutoSize = true;
            this.radioButtonNSIS.Location = new System.Drawing.Point(66, 36);
            this.radioButtonNSIS.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonNSIS.Name = "radioButtonNSIS";
            this.radioButtonNSIS.Size = new System.Drawing.Size(211, 17);
            this.radioButtonNSIS.TabIndex = 4;
            this.radioButtonNSIS.Text = "&NSIS (Nullsoft Scriptable Install System)";
            this.radioButtonNSIS.UseVisualStyleBackColor = true;
            // 
            // pictureBoxInnoSetup
            // 
            this.pictureBoxInnoSetup.Image = global::MessageBoxDesigner.Properties.Resources.inno_setup;
            this.pictureBoxInnoSetup.Location = new System.Drawing.Point(5, 73);
            this.pictureBoxInnoSetup.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxInnoSetup.Name = "pictureBoxInnoSetup";
            this.pictureBoxInnoSetup.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxInnoSetup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxInnoSetup.TabIndex = 3;
            this.pictureBoxInnoSetup.TabStop = false;
            this.pictureBoxInnoSetup.Click += new System.EventHandler(this.pictureBoxInnoSetup_Click);
            // 
            // pictureBoxNSIS
            // 
            this.pictureBoxNSIS.Image = global::MessageBoxDesigner.Properties.Resources.nsis;
            this.pictureBoxNSIS.Location = new System.Drawing.Point(5, 24);
            this.pictureBoxNSIS.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxNSIS.Name = "pictureBoxNSIS";
            this.pictureBoxNSIS.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxNSIS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxNSIS.TabIndex = 2;
            this.pictureBoxNSIS.TabStop = false;
            this.pictureBoxNSIS.Click += new System.EventHandler(this.pictureBoxNSIS_Click);
            // 
            // StartForm
            // 
            this.AcceptButton = this.buttonStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 166);
            this.Controls.Add(this.groupBoxStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.Opacity = 0.92D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "unSigned\'s NSIS and Inno Setup MessageBox Designer";
            this.groupBoxStart.ResumeLayout(false);
            this.groupBoxStart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInnoSetup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNSIS)).EndInit();
            this.ResumeLayout(false);

		}

		private void pictureBoxInnoSetup_Click(object sender, EventArgs e)
		{
			this.radioButtonInnoSetup.Checked = true;
			this.radioButtonNSIS.Checked = false;
			this.mInnoSetup = true;
		}

		private void pictureBoxNSIS_Click(object sender, EventArgs e)
		{
			this.radioButtonNSIS.Checked = true;
			this.radioButtonInnoSetup.Checked = false;
			this.mInnoSetup = false;
		}
	}
}