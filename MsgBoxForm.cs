using MessageBoxDesigner.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MessageBoxDesigner
{
	public class MsgBoxForm : Form
	{
		private const int WM_SYSCOMMAND = 274;

		private const int MF_STRING = 0;

		private const int MF_SEPARATOR = 2048;

		private bool mInnoSetup;

		private bool mNoPreview;

		private bool mNoCopy;

		private bool mNoLink;

		private string[] mTextIcons = new string[] { "Information", "Question", "Warning", "Error" };

		private string[] mTextNsisIcons = new string[] { "MB_ICONINFORMATION", "MB_ICONQUESTION", "MB_ICONEXCLAMATION", "MB_ICONSTOP" };

		private string[] mTextInnoSetupIcons = new string[] { "mbInformation", "mbConfirmation", "mbError", "mbCriticalError" };

		private string[] mTextDefaultButton = new string[] { "First", "Second", "Third" };

		private string[] mTextNsisDefaultButton = new string[] { "MB_DEFBUTTON1", "MB_DEFBUTTON2", "MB_DEFBUTTON3" };

		private string[] mTextButtons = new string[] { "OK", "OK/Cancel", "Yes/No", "Yes/No/Cancel", "Retry/Cancel", "Abort/Retry/Ignore" };

		private string[] mTextNsisButtons = new string[] { "MB_OK", "MB_OKCANCEL", "MB_YESNO", "MB_YESNOCANCEL", "MB_RETRYCANCEL", "MB_ABORTRETRYIGNORE" };

		private string[] mTextInnoSetupButtons = new string[] { "MB_OK", "MB_OKCANCEL", "MB_YESNO", "MB_YESNOCANCEL", "MB_RETRYCANCEL", "MB_ABORTRETRYIGNORE" };

		private string[] mTextReturn = new string[] { "OK", "Cancel", "Yes", "No", "Abort", "Retry", "Ignore" };

		private string[] mTextNsisReturn = new string[] { "IDOK", "IDCANCEL", "IDYES", "IDNO", "IDABORT", "IDRETRY", "IDIGNORE" };

		private string[] mTextInnoSetupReturn = new string[] { "IDOK", "IDCANCEL", "IDYES", "IDNO", "IDABORT", "IDRETRY", "IDIGNORE" };

		private string[] mResultCode;

		private int SYSMENU_ABOUT_ID = 1;

	    private IContainer components;

		private GroupBox groupBoxIcon;

		private RadioButton radioButtonNone;

		private RadioButton radioButtonError;

		private RadioButton radioButtonWarning;

		private RadioButton radioButtonQuestion;

		private RadioButton radioButtonInformation;

		private GroupBox groupBoxDefaultButton;

		private RadioButton radioButtonThird;

		private RadioButton radioButtonSecond;

		private RadioButton radioButtonFirst;

		private GroupBox groupBoxText;

		private TextBox textBoxText;

		private GroupBox groupBoxButtons;

		private RadioButton radioButtonAbortRetryIgnore;

		private RadioButton radioButtonRetryCancel;

		private RadioButton radioButtonYesNoCancel;

		private RadioButton radioButtonYesNo;

		private RadioButton radioButtonOKCancel;

		private RadioButton radioButtonOK;

		private GroupBox groupBoxReturnValue;

		private CheckBox checkBoxIgnore;

		private CheckBox checkBoxRetry;

		private CheckBox checkBoxAbort;

		private CheckBox checkBoxNo;

		private CheckBox checkBoxYes;

		private CheckBox checkBoxCancel;

		private CheckBox checkBoxOK;

		private Button buttonCancel;

		private Button buttonOK;

		private Button buttonPreview;

		private GroupBox groupBoxDetails;

		private TextBox textBoxIgnore;

		private TextBox textBoxRetry;

		private TextBox textBoxAbort;

		private TextBox textBoxNo;

		private TextBox textBoxYes;

		private TextBox textBoxCancel;

		private TextBox textBoxOK;

		private RadioButton radioButtonSdOK;

		private RadioButton radioButtonSdIgnore;

		private RadioButton radioButtonSdRetry;

		private RadioButton radioButtonSdAbort;

		private RadioButton radioButtonSdNo;

		private RadioButton radioButtonSdYes;

		private RadioButton radioButtonSdCancel;

        private Button buttonAbout;

		private PictureBox pictureBoxInformation;

		private PictureBox pictureBoxError;

		private PictureBox pictureBoxWarning;
        private Button btn_back_menu;
        private PictureBox pictureBoxQuestion;

		public MsgBoxForm(bool innoSetup, bool noPreview, bool noCopy, bool noLink)
		{
			this.InitializeComponent();
			this.Font = SystemFonts.DefaultFont;
			this.mInnoSetup = innoSetup;
			this.mNoPreview = noPreview;
			this.mNoCopy = noCopy;
			this.mNoLink = noLink;
			if (!innoSetup)
			{
				this.InitializeAsNSIS();
			}
			else
			{
				this.InitializeAsInnoSetup();
			}
			if (noPreview)
			{
				this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=false, SetLastError=true)]
		private static extern bool AppendMenu(IntPtr hMenu, int uFlags, int uIDNewItem, string lpNewItem);

		private string[] BuildInnoSetupCode()
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			string str1 = string.Empty;
			string empty2 = string.Empty;
			string str2 = string.Empty;
			if (this.radioButtonInformation.Checked)
			{
				str = "mbInformation";
			}
			if (this.radioButtonQuestion.Checked)
			{
				str = "mbConfirmation";
			}
			if (this.radioButtonWarning.Checked)
			{
				str = "mbError";
			}
			if (this.radioButtonError.Checked)
			{
				str = "mbCriticalError";
			}
			if (this.radioButtonOK.Checked)
			{
				empty = "MB_OK";
			}
			if (this.radioButtonOKCancel.Checked)
			{
				empty = "MB_OKCANCEL";
				if (this.checkBoxOK.Checked)
				{
					str1 = "IDOK";
				}
				if (this.checkBoxCancel.Checked)
				{
					if (!string.IsNullOrEmpty(str1))
					{
						empty2 = "IDCANCEL";
					}
					else
					{
						str1 = "IDCANCEL";
					}
				}
			}
			if (this.radioButtonYesNo.Checked)
			{
				empty = "MB_YESNO";
				if (this.checkBoxYes.Checked)
				{
					str1 = "IDYES";
				}
				if (this.checkBoxNo.Checked)
				{
					if (!string.IsNullOrEmpty(str1))
					{
						empty2 = "IDNO";
					}
					else
					{
						str1 = "IDNO";
					}
				}
			}
			if (this.radioButtonYesNoCancel.Checked)
			{
				empty = "MB_YESNOCANCEL";
				if (this.checkBoxYes.Checked)
				{
					str1 = "IDYES";
				}
				if (this.checkBoxNo.Checked)
				{
					if (!string.IsNullOrEmpty(str1))
					{
						empty2 = "IDNO";
					}
					else
					{
						str1 = "IDNO";
					}
				}
				if (this.checkBoxCancel.Checked)
				{
					if (string.IsNullOrEmpty(str1))
					{
						str1 = "IDCANCEL";
					}
					else if (!string.IsNullOrEmpty(empty2))
					{
						str2 = "IDCANCEL";
					}
					else
					{
						empty2 = "IDCANCEL";
					}
				}
			}
			if (this.radioButtonRetryCancel.Checked)
			{
				empty = "MB_RETRYCANCEL";
				if (this.checkBoxRetry.Checked)
				{
					str1 = "IDRETRY";
				}
				if (this.checkBoxCancel.Checked)
				{
					if (!string.IsNullOrEmpty(str1))
					{
						empty2 = "IDCANCEL";
					}
					else
					{
						str1 = "IDCANCEL";
					}
				}
			}
			if (this.radioButtonAbortRetryIgnore.Checked)
			{
				empty = "MB_ABORTRETRYIGNORE";
				if (this.checkBoxAbort.Checked)
				{
					str1 = "IDABORT";
				}
				if (this.checkBoxRetry.Checked)
				{
					if (!string.IsNullOrEmpty(str1))
					{
						empty2 = "IDRETRY";
					}
					else
					{
						str1 = "IDRETRY";
					}
				}
				if (this.checkBoxIgnore.Checked)
				{
					if (string.IsNullOrEmpty(str1))
					{
						str1 = "IDIGNORE";
					}
					else if (!string.IsNullOrEmpty(empty2))
					{
						str2 = "IDIGNORE";
					}
					else
					{
						empty2 = "IDIGNORE";
					}
				}
			}
			if (this.radioButtonFirst.Checked)
			{
				empty1 = " or MB_DEFBUTTON1";
			}
			if (this.radioButtonSecond.Checked)
			{
				empty1 = " or MB_DEFBUTTON2";
			}
			if (this.radioButtonThird.Checked)
			{
				empty1 = " or MB_DEFBUTTON3";
			}
			string[] strArrays = null;
			if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(empty2) && string.IsNullOrEmpty(str2))
			{
				strArrays = new string[1];
				object[] text = new object[] { this.textBoxText.Text, str, empty, empty1 };
				strArrays[0] = string.Format("MsgBox('{0}', {1}, {2}{3});", text);
			}
			else if ((string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(empty2)) && (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)) && (string.IsNullOrEmpty(empty2) || string.IsNullOrEmpty(str2)))
			{
				string empty3 = string.Empty;
				if (!string.IsNullOrEmpty(str1))
				{
					empty3 = str1;
				}
				else if (!string.IsNullOrEmpty(empty2))
				{
					empty3 = empty2;
				}
				else if (!string.IsNullOrEmpty(str2))
				{
					empty3 = str2;
				}
				strArrays = new string[4];
				object[] objArray = new object[] { this.textBoxText.Text, str, empty, empty1, empty3 };
				strArrays[0] = string.Format("if (MsgBox('{0}', {1}, {2}{3}) = {4}) then", objArray);
				strArrays[1] = "begin";
				strArrays[2] = " ";
				strArrays[3] = "end;";
			}
			else
			{
				string str3 = string.Empty;
				int num = 2;
				if (!string.IsNullOrEmpty(str1))
				{
					num += 4;
				}
				if (!string.IsNullOrEmpty(empty2))
				{
					num += 4;
				}
				if (!string.IsNullOrEmpty(str2))
				{
					num += 4;
				}
				strArrays = new string[num];
				num = 0;
				object[] text1 = new object[] { this.textBoxText.Text, str, empty, empty1 };
				strArrays[num] = string.Format("case MsgBox('{0}', {1}, {2}{3}) of", text1);
				if (!string.IsNullOrEmpty(str1))
				{
					num++;
					strArrays[num] = string.Format("{0}:", str1);
					strArrays[num + 1] = "begin";
					strArrays[num + 2] = " ";
					strArrays[num + 3] = "end;";
					num += 3;
				}
				if (!string.IsNullOrEmpty(empty2))
				{
					num++;
					strArrays[num] = string.Format("{0}:", empty2);
					strArrays[num + 1] = "begin";
					strArrays[num + 2] = " ";
					strArrays[num + 3] = "end;";
					num += 3;
				}
				if (!string.IsNullOrEmpty(str2))
				{
					num++;
					strArrays[num] = string.Format("{0}:", str2);
					strArrays[num + 1] = "begin";
					strArrays[num + 2] = " ";
					strArrays[num + 3] = "end;";
					num += 3;
				}
				int num1 = num + 1;
				num = num1;
				strArrays[num1] = "end;";
			}
			return strArrays;
		}



		private string[] BuildNsisCode()
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			string str1 = string.Empty;
			string empty2 = string.Empty;
			string text = string.Empty;
			string text1 = string.Empty;
			string str2 = string.Empty;
			string empty3 = string.Empty;
			if (this.radioButtonOK.Checked)
			{
				empty = "MB_OK";
			}
			if (this.radioButtonOKCancel.Checked)
			{
				empty = "MB_OKCANCEL";
				if (this.checkBoxOK.Checked)
				{
					if (this.radioButtonSdOK.Checked)
					{
						str1 = " /SD IDOK";
					}
					empty2 = string.Concat(empty2, " IDOK ", this.textBoxOK.Text);
					text = this.textBoxOK.Text;
					str2 = "IDOK";
				}
				if (this.checkBoxCancel.Checked)
				{
					if (this.radioButtonSdCancel.Checked)
					{
						str1 = " /SD IDCANCEL";
					}
					empty2 = string.Concat(empty2, " IDCANCEL ", this.textBoxCancel.Text);
					if (!string.IsNullOrEmpty(text))
					{
						text1 = this.textBoxCancel.Text;
						empty3 = "IDCANCEL";
					}
					else
					{
						text = this.textBoxCancel.Text;
						str2 = "IDCANCEL";
					}
				}
			}
			if (this.radioButtonYesNo.Checked)
			{
				empty = "MB_YESNO";
				if (this.checkBoxYes.Checked)
				{
					if (this.radioButtonSdYes.Checked)
					{
						str1 = " /SD IDYES";
					}
					empty2 = string.Concat(empty2, " IDYES ", this.textBoxYes.Text);
					text = this.textBoxYes.Text;
					str2 = "IDYES";
				}
				if (this.checkBoxNo.Checked)
				{
					if (this.radioButtonSdNo.Checked)
					{
						str1 = " /SD IDNO";
					}
					empty2 = string.Concat(empty2, " IDNO ", this.textBoxNo.Text);
					if (!string.IsNullOrEmpty(text))
					{
						text1 = this.textBoxNo.Text;
						empty3 = "IDNO";
					}
					else
					{
						text = this.textBoxNo.Text;
						str2 = "IDNO";
					}
				}
			}
			if (this.radioButtonYesNoCancel.Checked)
			{
				empty = "MB_YESNOCANCEL";
				if (this.checkBoxYes.Checked)
				{
					if (this.radioButtonSdYes.Checked)
					{
						str1 = " /SD IDYES";
					}
					empty2 = string.Concat(empty2, " IDYES ", this.textBoxYes.Text);
					text = this.textBoxYes.Text;
					str2 = "IDYES";
				}
				if (this.checkBoxNo.Checked)
				{
					if (this.radioButtonSdNo.Checked)
					{
						str1 = " /SD IDNO";
					}
					empty2 = string.Concat(empty2, " IDNO ", this.textBoxNo.Text);
					if (!string.IsNullOrEmpty(text))
					{
						text1 = this.textBoxNo.Text;
						empty3 = "IDNO";
					}
					else
					{
						text = this.textBoxNo.Text;
						str2 = "IDNO";
					}
				}
				if (this.checkBoxCancel.Checked)
				{
					if (this.radioButtonSdCancel.Checked)
					{
						str1 = " /SD IDCANCEL";
					}
					empty2 = string.Concat(empty2, " IDCANCEL ", this.textBoxCancel.Text);
					if (!string.IsNullOrEmpty(text))
					{
						text1 = this.textBoxCancel.Text;
						empty3 = "IDCANCEL";
					}
					else
					{
						text = this.textBoxCancel.Text;
						str2 = "IDCANCEL";
					}
				}
			}
			if (this.radioButtonRetryCancel.Checked)
			{
				empty = "MB_RETRYCANCEL";
				if (this.checkBoxRetry.Checked)
				{
					if (this.radioButtonSdRetry.Checked)
					{
						str1 = " /SD IDRETRY";
					}
					empty2 = string.Concat(empty2, " IDRETRY ", this.textBoxRetry.Text);
					text = this.textBoxRetry.Text;
					str2 = "IDRETRY";
				}
				if (this.checkBoxCancel.Checked)
				{
					if (this.radioButtonSdCancel.Checked)
					{
						str1 = " /SD IDCANCEL";
					}
					empty2 = string.Concat(empty2, " IDCANCEL ", this.textBoxCancel.Text);
					if (!string.IsNullOrEmpty(text))
					{
						text1 = this.textBoxCancel.Text;
						empty3 = "IDCANCEL";
					}
					else
					{
						text = this.textBoxCancel.Text;
						str2 = "IDCANCEL";
					}
				}
			}
			if (this.radioButtonAbortRetryIgnore.Checked)
			{
				empty = "MB_ABORTRETRYIGNORE";
				if (this.checkBoxAbort.Checked)
				{
					if (this.radioButtonSdAbort.Checked)
					{
						str1 = " /SD IDABORT";
					}
					empty2 = string.Concat(empty2, " IDABORT ", this.textBoxAbort.Text);
					text = this.textBoxAbort.Text;
					str2 = "IDABORT";
				}
				if (this.checkBoxRetry.Checked)
				{
					if (this.radioButtonSdRetry.Checked)
					{
						str1 = " /SD IDRETRY";
					}
					empty2 = string.Concat(empty2, " IDRETRY ", this.textBoxRetry.Text);
					if (!string.IsNullOrEmpty(text))
					{
						text1 = this.textBoxRetry.Text;
						empty3 = "IDRETRY";
					}
					else
					{
						text = this.textBoxRetry.Text;
						str2 = "IDRETRY";
					}
				}
				if (this.checkBoxIgnore.Checked)
				{
					if (this.radioButtonSdIgnore.Checked)
					{
						str1 = " /SD IDIGNORE";
					}
					empty2 = string.Concat(empty2, " IDIGNORE ", this.textBoxIgnore.Text);
					if (!string.IsNullOrEmpty(text))
					{
						text1 = this.textBoxIgnore.Text;
						empty3 = "IDIGNORE";
					}
					else
					{
						text = this.textBoxIgnore.Text;
						str2 = "IDIGNORE";
					}
				}
			}
			if (this.radioButtonInformation.Checked)
			{
				str = "|MB_ICONINFORMATION";
			}
			if (this.radioButtonQuestion.Checked)
			{
				str = "|MB_ICONQUESTION";
			}
			if (this.radioButtonWarning.Checked)
			{
				str = "|MB_ICONEXCLAMATION";
			}
			if (this.radioButtonError.Checked)
			{
				str = "|MB_ICONSTOP";
			}
			if (this.radioButtonFirst.Checked)
			{
				empty1 = "|MB_DEFBUTTON1";
			}
			if (this.radioButtonSecond.Checked)
			{
				empty1 = "|MB_DEFBUTTON2";
			}
			if (this.radioButtonThird.Checked)
			{
				empty1 = "|MB_DEFBUTTON3";
			}
			string[] strArrays = null;
			int num = 1;
			if (!string.IsNullOrEmpty(text))
			{
				num += 3;
			}
			if (!string.IsNullOrEmpty(text1))
			{
				num += 2;
			}
			strArrays = new string[num];
			num = 0;
			object[] objArray = new object[] { empty, str, empty1, this.textBoxText.Text, str1, empty2 };
			strArrays[num] = string.Format("MessageBox {0}{1}{2} \"{3}\"{4}{5}", objArray);
			num++;
			if (!string.IsNullOrEmpty(text))
			{
				strArrays[num] = string.Format("{0}:", text);
				num++;
				strArrays[num] = string.Format("; {0} was clicked", str2);
				num++;
				strArrays[num] = string.Format(" ", new object[0]);
				num++;
			}
			if (!string.IsNullOrEmpty(text1))
			{
				strArrays[num] = string.Format("{0}:", text1);
				num++;
				strArrays[num] = string.Format("; {0} was clicked", empty3);
				num++;
			}
			return strArrays;
		}

		private void buttonAbout_Click(object sender, EventArgs e)
		{
			MessageBox.Show("unSigned's NSIS and Inno Setup MessageBox Designer\n\nVersion 1.1.01\n\nCompatible with:\n\nVisual & Installer (www.visual-installer.com)\nRAD & Installer (www.rad-installer.com)\nGraphical Installer (www.graphical-installer.com)\n\nCopyright (c) 2016 - 2020 unSigned, s. r. o.\n\nAll Rights Reserved.\n\nwww.unsignedsw.com", "About...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
		
			Application.Exit();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (!this.mInnoSetup)
			{
				this.mResultCode = this.BuildNsisCode();
			}
			else
			{
				this.mResultCode = this.BuildInnoSetupCode();
			}
			if (!this.mNoPreview)
			{
				PreviewForm previewForm = new PreviewForm(this.mInnoSetup, this.mNoLink, this.mResultCode);
				previewForm.ShowDialog();
				return;
			}
			if (!this.mNoCopy)
			{
				Clipboard.SetText(this.GetResultCode());
			}

			Application.Exit();
		}

		private void buttonPreview_Click(object sender, EventArgs e)
		{
			MessageBoxIcon messageBoxIcon = MessageBoxIcon.None;
			if (this.radioButtonInformation.Checked)
			{
				messageBoxIcon = MessageBoxIcon.Asterisk;
			}
			else if (this.radioButtonQuestion.Checked)
			{
				messageBoxIcon = MessageBoxIcon.Question;
			}
			else if (this.radioButtonWarning.Checked)
			{
				messageBoxIcon = MessageBoxIcon.Exclamation;
			}
			else if (this.radioButtonError.Checked)
			{
				messageBoxIcon = MessageBoxIcon.Hand;
			}
			MessageBoxButtons messageBoxButton = MessageBoxButtons.OK;
			if (this.radioButtonOKCancel.Checked)
			{
				messageBoxButton = MessageBoxButtons.OKCancel;
			}
			else if (this.radioButtonYesNo.Checked)
			{
				messageBoxButton = MessageBoxButtons.YesNo;
			}
			else if (this.radioButtonYesNoCancel.Checked)
			{
				messageBoxButton = MessageBoxButtons.YesNoCancel;
			}
			else if (this.radioButtonRetryCancel.Checked)
			{
				messageBoxButton = MessageBoxButtons.RetryCancel;
			}
			else if (this.radioButtonAbortRetryIgnore.Checked)
			{
				messageBoxButton = MessageBoxButtons.AbortRetryIgnore;
			}
			MessageBoxDefaultButton messageBoxDefaultButton = MessageBoxDefaultButton.Button1;
			if (this.groupBoxDefaultButton.Enabled)
			{
				if (this.radioButtonSecond.Checked)
				{
					messageBoxDefaultButton = MessageBoxDefaultButton.Button2;
				}
				if (this.radioButtonThird.Checked)
				{
					messageBoxDefaultButton = MessageBoxDefaultButton.Button3;
				}
			}
			string str = this.textBoxText.Text.Replace("$\\r$\\n", Environment.NewLine);
			str = str.Replace("#13#10", Environment.NewLine);
			str = str.Replace("$\\r", Environment.NewLine);
			str = str.Replace("$\\n", Environment.NewLine);
			str = str.Replace("%n", Environment.NewLine);
			MessageBox.Show(str, "unSigned's MessageBox Designer", messageBoxButton, messageBoxIcon, messageBoxDefaultButton);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public string GetResultCode()
		{
			string empty = string.Empty;
			for (int i = 0; i < (int)this.mResultCode.Length; i++)
			{
				empty = string.Concat(empty, Environment.NewLine, this.mResultCode[i]);
			}
			return empty;
		}

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=false, SetLastError=true)]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		private void InitializeAsInnoSetup()
		{
			this.PrepareDialog();
		}

		private void InitializeAsNSIS()
		{
			this.PrepareDialog();
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBoxForm));
            this.groupBoxIcon = new System.Windows.Forms.GroupBox();
            this.pictureBoxError = new System.Windows.Forms.PictureBox();
            this.pictureBoxWarning = new System.Windows.Forms.PictureBox();
            this.pictureBoxQuestion = new System.Windows.Forms.PictureBox();
            this.pictureBoxInformation = new System.Windows.Forms.PictureBox();
            this.radioButtonNone = new System.Windows.Forms.RadioButton();
            this.radioButtonError = new System.Windows.Forms.RadioButton();
            this.radioButtonWarning = new System.Windows.Forms.RadioButton();
            this.radioButtonQuestion = new System.Windows.Forms.RadioButton();
            this.radioButtonInformation = new System.Windows.Forms.RadioButton();
            this.groupBoxDefaultButton = new System.Windows.Forms.GroupBox();
            this.radioButtonThird = new System.Windows.Forms.RadioButton();
            this.radioButtonSecond = new System.Windows.Forms.RadioButton();
            this.radioButtonFirst = new System.Windows.Forms.RadioButton();
            this.groupBoxText = new System.Windows.Forms.GroupBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.groupBoxButtons = new System.Windows.Forms.GroupBox();
            this.radioButtonAbortRetryIgnore = new System.Windows.Forms.RadioButton();
            this.radioButtonRetryCancel = new System.Windows.Forms.RadioButton();
            this.radioButtonYesNoCancel = new System.Windows.Forms.RadioButton();
            this.radioButtonYesNo = new System.Windows.Forms.RadioButton();
            this.radioButtonOKCancel = new System.Windows.Forms.RadioButton();
            this.radioButtonOK = new System.Windows.Forms.RadioButton();
            this.groupBoxReturnValue = new System.Windows.Forms.GroupBox();
            this.radioButtonSdIgnore = new System.Windows.Forms.RadioButton();
            this.radioButtonSdRetry = new System.Windows.Forms.RadioButton();
            this.radioButtonSdAbort = new System.Windows.Forms.RadioButton();
            this.radioButtonSdNo = new System.Windows.Forms.RadioButton();
            this.radioButtonSdYes = new System.Windows.Forms.RadioButton();
            this.radioButtonSdCancel = new System.Windows.Forms.RadioButton();
            this.radioButtonSdOK = new System.Windows.Forms.RadioButton();
            this.textBoxIgnore = new System.Windows.Forms.TextBox();
            this.textBoxRetry = new System.Windows.Forms.TextBox();
            this.textBoxAbort = new System.Windows.Forms.TextBox();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.textBoxYes = new System.Windows.Forms.TextBox();
            this.textBoxCancel = new System.Windows.Forms.TextBox();
            this.textBoxOK = new System.Windows.Forms.TextBox();
            this.checkBoxIgnore = new System.Windows.Forms.CheckBox();
            this.checkBoxRetry = new System.Windows.Forms.CheckBox();
            this.checkBoxAbort = new System.Windows.Forms.CheckBox();
            this.checkBoxNo = new System.Windows.Forms.CheckBox();
            this.checkBoxYes = new System.Windows.Forms.CheckBox();
            this.checkBoxCancel = new System.Windows.Forms.CheckBox();
            this.checkBoxOK = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonPreview = new System.Windows.Forms.Button();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.btn_back_menu = new System.Windows.Forms.Button();
            this.groupBoxIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuestion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInformation)).BeginInit();
            this.groupBoxDefaultButton.SuspendLayout();
            this.groupBoxText.SuspendLayout();
            this.groupBoxButtons.SuspendLayout();
            this.groupBoxReturnValue.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxIcon
            // 
            this.groupBoxIcon.Controls.Add(this.pictureBoxError);
            this.groupBoxIcon.Controls.Add(this.pictureBoxWarning);
            this.groupBoxIcon.Controls.Add(this.pictureBoxQuestion);
            this.groupBoxIcon.Controls.Add(this.pictureBoxInformation);
            this.groupBoxIcon.Controls.Add(this.radioButtonNone);
            this.groupBoxIcon.Controls.Add(this.radioButtonError);
            this.groupBoxIcon.Controls.Add(this.radioButtonWarning);
            this.groupBoxIcon.Controls.Add(this.radioButtonQuestion);
            this.groupBoxIcon.Controls.Add(this.radioButtonInformation);
            this.groupBoxIcon.Location = new System.Drawing.Point(12, 12);
            this.groupBoxIcon.Name = "groupBoxIcon";
            this.groupBoxIcon.Size = new System.Drawing.Size(214, 222);
            this.groupBoxIcon.TabIndex = 0;
            this.groupBoxIcon.TabStop = false;
            this.groupBoxIcon.Text = "Icon: ";
            // 
            // pictureBoxError
            // 
            this.pictureBoxError.Image = global::MessageBoxDesigner.Properties.Resources.error;
            this.pictureBoxError.Location = new System.Drawing.Point(5, 165);
            this.pictureBoxError.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxError.Name = "pictureBoxError";
            this.pictureBoxError.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxError.TabIndex = 13;
            this.pictureBoxError.TabStop = false;
            // 
            // pictureBoxWarning
            // 
            this.pictureBoxWarning.Image = global::MessageBoxDesigner.Properties.Resources.warning;
            this.pictureBoxWarning.Location = new System.Drawing.Point(5, 120);
            this.pictureBoxWarning.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxWarning.Name = "pictureBoxWarning";
            this.pictureBoxWarning.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxWarning.TabIndex = 12;
            this.pictureBoxWarning.TabStop = false;
            // 
            // pictureBoxQuestion
            // 
            this.pictureBoxQuestion.Image = global::MessageBoxDesigner.Properties.Resources.question;
            this.pictureBoxQuestion.Location = new System.Drawing.Point(5, 76);
            this.pictureBoxQuestion.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxQuestion.Name = "pictureBoxQuestion";
            this.pictureBoxQuestion.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxQuestion.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxQuestion.TabIndex = 11;
            this.pictureBoxQuestion.TabStop = false;
            // 
            // pictureBoxInformation
            // 
            this.pictureBoxInformation.Image = global::MessageBoxDesigner.Properties.Resources.information;
            this.pictureBoxInformation.Location = new System.Drawing.Point(5, 29);
            this.pictureBoxInformation.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxInformation.Name = "pictureBoxInformation";
            this.pictureBoxInformation.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxInformation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxInformation.TabIndex = 10;
            this.pictureBoxInformation.TabStop = false;
            // 
            // radioButtonNone
            // 
            this.radioButtonNone.AutoSize = true;
            this.radioButtonNone.Location = new System.Drawing.Point(54, 17);
            this.radioButtonNone.Name = "radioButtonNone";
            this.radioButtonNone.Size = new System.Drawing.Size(51, 17);
            this.radioButtonNone.TabIndex = 9;
            this.radioButtonNone.Text = "None";
            this.radioButtonNone.UseVisualStyleBackColor = true;
            // 
            // radioButtonError
            // 
            this.radioButtonError.AutoSize = true;
            this.radioButtonError.Location = new System.Drawing.Point(54, 177);
            this.radioButtonError.Name = "radioButtonError";
            this.radioButtonError.Size = new System.Drawing.Size(32, 17);
            this.radioButtonError.TabIndex = 8;
            this.radioButtonError.Text = "E";
            this.radioButtonError.UseVisualStyleBackColor = true;
            // 
            // radioButtonWarning
            // 
            this.radioButtonWarning.AutoSize = true;
            this.radioButtonWarning.Location = new System.Drawing.Point(54, 133);
            this.radioButtonWarning.Name = "radioButtonWarning";
            this.radioButtonWarning.Size = new System.Drawing.Size(36, 17);
            this.radioButtonWarning.TabIndex = 7;
            this.radioButtonWarning.Text = "W";
            this.radioButtonWarning.UseVisualStyleBackColor = true;
            // 
            // radioButtonQuestion
            // 
            this.radioButtonQuestion.AutoSize = true;
            this.radioButtonQuestion.Location = new System.Drawing.Point(54, 90);
            this.radioButtonQuestion.Name = "radioButtonQuestion";
            this.radioButtonQuestion.Size = new System.Drawing.Size(33, 17);
            this.radioButtonQuestion.TabIndex = 6;
            this.radioButtonQuestion.Text = "Q";
            this.radioButtonQuestion.UseVisualStyleBackColor = true;
            // 
            // radioButtonInformation
            // 
            this.radioButtonInformation.AutoSize = true;
            this.radioButtonInformation.Checked = true;
            this.radioButtonInformation.Location = new System.Drawing.Point(54, 45);
            this.radioButtonInformation.Name = "radioButtonInformation";
            this.radioButtonInformation.Size = new System.Drawing.Size(28, 17);
            this.radioButtonInformation.TabIndex = 5;
            this.radioButtonInformation.TabStop = true;
            this.radioButtonInformation.Text = "I";
            this.radioButtonInformation.UseVisualStyleBackColor = true;
            // 
            // groupBoxDefaultButton
            // 
            this.groupBoxDefaultButton.Controls.Add(this.radioButtonThird);
            this.groupBoxDefaultButton.Controls.Add(this.radioButtonSecond);
            this.groupBoxDefaultButton.Controls.Add(this.radioButtonFirst);
            this.groupBoxDefaultButton.Location = new System.Drawing.Point(12, 240);
            this.groupBoxDefaultButton.Name = "groupBoxDefaultButton";
            this.groupBoxDefaultButton.Size = new System.Drawing.Size(214, 122);
            this.groupBoxDefaultButton.TabIndex = 1;
            this.groupBoxDefaultButton.TabStop = false;
            this.groupBoxDefaultButton.Text = "Default button: ";
            // 
            // radioButtonThird
            // 
            this.radioButtonThird.Location = new System.Drawing.Point(9, 90);
            this.radioButtonThird.Name = "radioButtonThird";
            this.radioButtonThird.Size = new System.Drawing.Size(198, 17);
            this.radioButtonThird.TabIndex = 2;
            this.radioButtonThird.Text = "3";
            this.radioButtonThird.UseVisualStyleBackColor = true;
            // 
            // radioButtonSecond
            // 
            this.radioButtonSecond.Location = new System.Drawing.Point(9, 59);
            this.radioButtonSecond.Name = "radioButtonSecond";
            this.radioButtonSecond.Size = new System.Drawing.Size(198, 17);
            this.radioButtonSecond.TabIndex = 1;
            this.radioButtonSecond.Text = "2";
            this.radioButtonSecond.UseVisualStyleBackColor = true;
            // 
            // radioButtonFirst
            // 
            this.radioButtonFirst.Checked = true;
            this.radioButtonFirst.Location = new System.Drawing.Point(9, 29);
            this.radioButtonFirst.Name = "radioButtonFirst";
            this.radioButtonFirst.Size = new System.Drawing.Size(198, 17);
            this.radioButtonFirst.TabIndex = 0;
            this.radioButtonFirst.TabStop = true;
            this.radioButtonFirst.Text = "1";
            this.radioButtonFirst.UseVisualStyleBackColor = true;
            // 
            // groupBoxText
            // 
            this.groupBoxText.Controls.Add(this.textBoxText);
            this.groupBoxText.Location = new System.Drawing.Point(232, 12);
            this.groupBoxText.Name = "groupBoxText";
            this.groupBoxText.Size = new System.Drawing.Size(333, 83);
            this.groupBoxText.TabIndex = 2;
            this.groupBoxText.TabStop = false;
            this.groupBoxText.Text = "Text: ";
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(6, 19);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(322, 58);
            this.textBoxText.TabIndex = 0;
            this.textBoxText.Text = "<Enter your text here...>";
            // 
            // groupBoxButtons
            // 
            this.groupBoxButtons.Controls.Add(this.radioButtonAbortRetryIgnore);
            this.groupBoxButtons.Controls.Add(this.radioButtonRetryCancel);
            this.groupBoxButtons.Controls.Add(this.radioButtonYesNoCancel);
            this.groupBoxButtons.Controls.Add(this.radioButtonYesNo);
            this.groupBoxButtons.Controls.Add(this.radioButtonOKCancel);
            this.groupBoxButtons.Controls.Add(this.radioButtonOK);
            this.groupBoxButtons.Location = new System.Drawing.Point(232, 100);
            this.groupBoxButtons.Name = "groupBoxButtons";
            this.groupBoxButtons.Size = new System.Drawing.Size(333, 158);
            this.groupBoxButtons.TabIndex = 3;
            this.groupBoxButtons.TabStop = false;
            this.groupBoxButtons.Text = "Buttons: ";
            // 
            // radioButtonAbortRetryIgnore
            // 
            this.radioButtonAbortRetryIgnore.AutoSize = true;
            this.radioButtonAbortRetryIgnore.Location = new System.Drawing.Point(6, 133);
            this.radioButtonAbortRetryIgnore.Name = "radioButtonAbortRetryIgnore";
            this.radioButtonAbortRetryIgnore.Size = new System.Drawing.Size(115, 17);
            this.radioButtonAbortRetryIgnore.TabIndex = 5;
            this.radioButtonAbortRetryIgnore.Text = "Abort/Retry/Ignore";
            this.radioButtonAbortRetryIgnore.UseVisualStyleBackColor = true;
            this.radioButtonAbortRetryIgnore.CheckedChanged += new System.EventHandler(this.SomeRadioButton_CheckedChanged);
            // 
            // radioButtonRetryCancel
            // 
            this.radioButtonRetryCancel.AutoSize = true;
            this.radioButtonRetryCancel.Location = new System.Drawing.Point(6, 111);
            this.radioButtonRetryCancel.Name = "radioButtonRetryCancel";
            this.radioButtonRetryCancel.Size = new System.Drawing.Size(88, 17);
            this.radioButtonRetryCancel.TabIndex = 4;
            this.radioButtonRetryCancel.Text = "Retry/Cancel";
            this.radioButtonRetryCancel.UseVisualStyleBackColor = true;
            this.radioButtonRetryCancel.CheckedChanged += new System.EventHandler(this.SomeRadioButton_CheckedChanged);
            // 
            // radioButtonYesNoCancel
            // 
            this.radioButtonYesNoCancel.AutoSize = true;
            this.radioButtonYesNoCancel.Location = new System.Drawing.Point(6, 88);
            this.radioButtonYesNoCancel.Name = "radioButtonYesNoCancel";
            this.radioButtonYesNoCancel.Size = new System.Drawing.Size(100, 17);
            this.radioButtonYesNoCancel.TabIndex = 3;
            this.radioButtonYesNoCancel.Text = "Yes/No/Cancel";
            this.radioButtonYesNoCancel.UseVisualStyleBackColor = true;
            this.radioButtonYesNoCancel.CheckedChanged += new System.EventHandler(this.SomeRadioButton_CheckedChanged);
            // 
            // radioButtonYesNo
            // 
            this.radioButtonYesNo.AutoSize = true;
            this.radioButtonYesNo.Location = new System.Drawing.Point(6, 65);
            this.radioButtonYesNo.Name = "radioButtonYesNo";
            this.radioButtonYesNo.Size = new System.Drawing.Size(62, 17);
            this.radioButtonYesNo.TabIndex = 2;
            this.radioButtonYesNo.Text = "Yes/No";
            this.radioButtonYesNo.UseVisualStyleBackColor = true;
            this.radioButtonYesNo.CheckedChanged += new System.EventHandler(this.SomeRadioButton_CheckedChanged);
            // 
            // radioButtonOKCancel
            // 
            this.radioButtonOKCancel.AutoSize = true;
            this.radioButtonOKCancel.Location = new System.Drawing.Point(6, 42);
            this.radioButtonOKCancel.Name = "radioButtonOKCancel";
            this.radioButtonOKCancel.Size = new System.Drawing.Size(78, 17);
            this.radioButtonOKCancel.TabIndex = 1;
            this.radioButtonOKCancel.Text = "OK/Cancel";
            this.radioButtonOKCancel.UseVisualStyleBackColor = true;
            this.radioButtonOKCancel.CheckedChanged += new System.EventHandler(this.SomeRadioButton_CheckedChanged);
            // 
            // radioButtonOK
            // 
            this.radioButtonOK.AutoSize = true;
            this.radioButtonOK.Location = new System.Drawing.Point(6, 19);
            this.radioButtonOK.Name = "radioButtonOK";
            this.radioButtonOK.Size = new System.Drawing.Size(40, 17);
            this.radioButtonOK.TabIndex = 0;
            this.radioButtonOK.Text = "OK";
            this.radioButtonOK.UseVisualStyleBackColor = true;
            this.radioButtonOK.CheckedChanged += new System.EventHandler(this.SomeRadioButton_CheckedChanged);
            // 
            // groupBoxReturnValue
            // 
            this.groupBoxReturnValue.Controls.Add(this.radioButtonSdIgnore);
            this.groupBoxReturnValue.Controls.Add(this.radioButtonSdRetry);
            this.groupBoxReturnValue.Controls.Add(this.radioButtonSdAbort);
            this.groupBoxReturnValue.Controls.Add(this.radioButtonSdNo);
            this.groupBoxReturnValue.Controls.Add(this.radioButtonSdYes);
            this.groupBoxReturnValue.Controls.Add(this.radioButtonSdCancel);
            this.groupBoxReturnValue.Controls.Add(this.radioButtonSdOK);
            this.groupBoxReturnValue.Controls.Add(this.textBoxIgnore);
            this.groupBoxReturnValue.Controls.Add(this.textBoxRetry);
            this.groupBoxReturnValue.Controls.Add(this.textBoxAbort);
            this.groupBoxReturnValue.Controls.Add(this.textBoxNo);
            this.groupBoxReturnValue.Controls.Add(this.textBoxYes);
            this.groupBoxReturnValue.Controls.Add(this.textBoxCancel);
            this.groupBoxReturnValue.Controls.Add(this.textBoxOK);
            this.groupBoxReturnValue.Controls.Add(this.checkBoxIgnore);
            this.groupBoxReturnValue.Controls.Add(this.checkBoxRetry);
            this.groupBoxReturnValue.Controls.Add(this.checkBoxAbort);
            this.groupBoxReturnValue.Controls.Add(this.checkBoxNo);
            this.groupBoxReturnValue.Controls.Add(this.checkBoxYes);
            this.groupBoxReturnValue.Controls.Add(this.checkBoxCancel);
            this.groupBoxReturnValue.Controls.Add(this.checkBoxOK);
            this.groupBoxReturnValue.Location = new System.Drawing.Point(232, 265);
            this.groupBoxReturnValue.Name = "groupBoxReturnValue";
            this.groupBoxReturnValue.Size = new System.Drawing.Size(333, 184);
            this.groupBoxReturnValue.TabIndex = 4;
            this.groupBoxReturnValue.TabStop = false;
            this.groupBoxReturnValue.Text = "Return value: ";
            // 
            // radioButtonSdIgnore
            // 
            this.radioButtonSdIgnore.AutoSize = true;
            this.radioButtonSdIgnore.Enabled = false;
            this.radioButtonSdIgnore.Location = new System.Drawing.Point(276, 156);
            this.radioButtonSdIgnore.Name = "radioButtonSdIgnore";
            this.radioButtonSdIgnore.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSdIgnore.TabIndex = 20;
            this.radioButtonSdIgnore.TabStop = true;
            this.radioButtonSdIgnore.Text = "/SD";
            this.radioButtonSdIgnore.UseVisualStyleBackColor = true;
            this.radioButtonSdIgnore.Visible = false;
            // 
            // radioButtonSdRetry
            // 
            this.radioButtonSdRetry.AutoSize = true;
            this.radioButtonSdRetry.Enabled = false;
            this.radioButtonSdRetry.Location = new System.Drawing.Point(276, 133);
            this.radioButtonSdRetry.Name = "radioButtonSdRetry";
            this.radioButtonSdRetry.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSdRetry.TabIndex = 19;
            this.radioButtonSdRetry.TabStop = true;
            this.radioButtonSdRetry.Text = "/SD";
            this.radioButtonSdRetry.UseVisualStyleBackColor = true;
            this.radioButtonSdRetry.Visible = false;
            // 
            // radioButtonSdAbort
            // 
            this.radioButtonSdAbort.AutoSize = true;
            this.radioButtonSdAbort.Enabled = false;
            this.radioButtonSdAbort.Location = new System.Drawing.Point(276, 110);
            this.radioButtonSdAbort.Name = "radioButtonSdAbort";
            this.radioButtonSdAbort.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSdAbort.TabIndex = 18;
            this.radioButtonSdAbort.TabStop = true;
            this.radioButtonSdAbort.Text = "/SD";
            this.radioButtonSdAbort.UseVisualStyleBackColor = true;
            this.radioButtonSdAbort.Visible = false;
            // 
            // radioButtonSdNo
            // 
            this.radioButtonSdNo.AutoSize = true;
            this.radioButtonSdNo.Enabled = false;
            this.radioButtonSdNo.Location = new System.Drawing.Point(276, 87);
            this.radioButtonSdNo.Name = "radioButtonSdNo";
            this.radioButtonSdNo.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSdNo.TabIndex = 17;
            this.radioButtonSdNo.TabStop = true;
            this.radioButtonSdNo.Text = "/SD";
            this.radioButtonSdNo.UseVisualStyleBackColor = true;
            this.radioButtonSdNo.Visible = false;
            // 
            // radioButtonSdYes
            // 
            this.radioButtonSdYes.AutoSize = true;
            this.radioButtonSdYes.Enabled = false;
            this.radioButtonSdYes.Location = new System.Drawing.Point(276, 64);
            this.radioButtonSdYes.Name = "radioButtonSdYes";
            this.radioButtonSdYes.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSdYes.TabIndex = 16;
            this.radioButtonSdYes.TabStop = true;
            this.radioButtonSdYes.Text = "/SD";
            this.radioButtonSdYes.UseVisualStyleBackColor = true;
            this.radioButtonSdYes.Visible = false;
            // 
            // radioButtonSdCancel
            // 
            this.radioButtonSdCancel.AutoSize = true;
            this.radioButtonSdCancel.Enabled = false;
            this.radioButtonSdCancel.Location = new System.Drawing.Point(276, 40);
            this.radioButtonSdCancel.Name = "radioButtonSdCancel";
            this.radioButtonSdCancel.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSdCancel.TabIndex = 15;
            this.radioButtonSdCancel.TabStop = true;
            this.radioButtonSdCancel.Text = "/SD";
            this.radioButtonSdCancel.UseVisualStyleBackColor = true;
            this.radioButtonSdCancel.Visible = false;
            // 
            // radioButtonSdOK
            // 
            this.radioButtonSdOK.AutoSize = true;
            this.radioButtonSdOK.Enabled = false;
            this.radioButtonSdOK.Location = new System.Drawing.Point(276, 18);
            this.radioButtonSdOK.Name = "radioButtonSdOK";
            this.radioButtonSdOK.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSdOK.TabIndex = 14;
            this.radioButtonSdOK.TabStop = true;
            this.radioButtonSdOK.Text = "/SD";
            this.radioButtonSdOK.UseVisualStyleBackColor = true;
            this.radioButtonSdOK.Visible = false;
            // 
            // textBoxIgnore
            // 
            this.textBoxIgnore.Enabled = false;
            this.textBoxIgnore.Location = new System.Drawing.Point(126, 156);
            this.textBoxIgnore.Name = "textBoxIgnore";
            this.textBoxIgnore.Size = new System.Drawing.Size(145, 20);
            this.textBoxIgnore.TabIndex = 13;
            this.textBoxIgnore.Visible = false;
            // 
            // textBoxRetry
            // 
            this.textBoxRetry.Enabled = false;
            this.textBoxRetry.Location = new System.Drawing.Point(126, 132);
            this.textBoxRetry.Name = "textBoxRetry";
            this.textBoxRetry.Size = new System.Drawing.Size(145, 20);
            this.textBoxRetry.TabIndex = 12;
            this.textBoxRetry.Visible = false;
            // 
            // textBoxAbort
            // 
            this.textBoxAbort.Enabled = false;
            this.textBoxAbort.Location = new System.Drawing.Point(126, 109);
            this.textBoxAbort.Name = "textBoxAbort";
            this.textBoxAbort.Size = new System.Drawing.Size(145, 20);
            this.textBoxAbort.TabIndex = 11;
            this.textBoxAbort.Visible = false;
            // 
            // textBoxNo
            // 
            this.textBoxNo.Enabled = false;
            this.textBoxNo.Location = new System.Drawing.Point(126, 86);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.Size = new System.Drawing.Size(145, 20);
            this.textBoxNo.TabIndex = 10;
            this.textBoxNo.Visible = false;
            // 
            // textBoxYes
            // 
            this.textBoxYes.Enabled = false;
            this.textBoxYes.Location = new System.Drawing.Point(126, 64);
            this.textBoxYes.Name = "textBoxYes";
            this.textBoxYes.Size = new System.Drawing.Size(145, 20);
            this.textBoxYes.TabIndex = 9;
            this.textBoxYes.Visible = false;
            // 
            // textBoxCancel
            // 
            this.textBoxCancel.Enabled = false;
            this.textBoxCancel.Location = new System.Drawing.Point(126, 40);
            this.textBoxCancel.Name = "textBoxCancel";
            this.textBoxCancel.Size = new System.Drawing.Size(145, 20);
            this.textBoxCancel.TabIndex = 8;
            this.textBoxCancel.Visible = false;
            // 
            // textBoxOK
            // 
            this.textBoxOK.Enabled = false;
            this.textBoxOK.Location = new System.Drawing.Point(126, 17);
            this.textBoxOK.Name = "textBoxOK";
            this.textBoxOK.Size = new System.Drawing.Size(145, 20);
            this.textBoxOK.TabIndex = 7;
            this.textBoxOK.Visible = false;
            // 
            // checkBoxIgnore
            // 
            this.checkBoxIgnore.AutoSize = true;
            this.checkBoxIgnore.Enabled = false;
            this.checkBoxIgnore.Location = new System.Drawing.Point(6, 156);
            this.checkBoxIgnore.Name = "checkBoxIgnore";
            this.checkBoxIgnore.Size = new System.Drawing.Size(56, 17);
            this.checkBoxIgnore.TabIndex = 6;
            this.checkBoxIgnore.Text = "Ignore";
            this.checkBoxIgnore.UseVisualStyleBackColor = true;
            this.checkBoxIgnore.CheckedChanged += new System.EventHandler(this.SomeCheckBox_CheckedChanged);
            // 
            // checkBoxRetry
            // 
            this.checkBoxRetry.AutoSize = true;
            this.checkBoxRetry.Enabled = false;
            this.checkBoxRetry.Location = new System.Drawing.Point(6, 134);
            this.checkBoxRetry.Name = "checkBoxRetry";
            this.checkBoxRetry.Size = new System.Drawing.Size(51, 17);
            this.checkBoxRetry.TabIndex = 5;
            this.checkBoxRetry.Text = "Retry";
            this.checkBoxRetry.UseVisualStyleBackColor = true;
            this.checkBoxRetry.CheckedChanged += new System.EventHandler(this.SomeCheckBox_CheckedChanged);
            // 
            // checkBoxAbort
            // 
            this.checkBoxAbort.AutoSize = true;
            this.checkBoxAbort.Enabled = false;
            this.checkBoxAbort.Location = new System.Drawing.Point(6, 111);
            this.checkBoxAbort.Name = "checkBoxAbort";
            this.checkBoxAbort.Size = new System.Drawing.Size(51, 17);
            this.checkBoxAbort.TabIndex = 4;
            this.checkBoxAbort.Text = "Abort";
            this.checkBoxAbort.UseVisualStyleBackColor = true;
            this.checkBoxAbort.CheckedChanged += new System.EventHandler(this.SomeCheckBox_CheckedChanged);
            // 
            // checkBoxNo
            // 
            this.checkBoxNo.AutoSize = true;
            this.checkBoxNo.Enabled = false;
            this.checkBoxNo.Location = new System.Drawing.Point(6, 88);
            this.checkBoxNo.Name = "checkBoxNo";
            this.checkBoxNo.Size = new System.Drawing.Size(40, 17);
            this.checkBoxNo.TabIndex = 3;
            this.checkBoxNo.Text = "No";
            this.checkBoxNo.UseVisualStyleBackColor = true;
            this.checkBoxNo.CheckedChanged += new System.EventHandler(this.SomeCheckBox_CheckedChanged);
            // 
            // checkBoxYes
            // 
            this.checkBoxYes.AutoSize = true;
            this.checkBoxYes.Enabled = false;
            this.checkBoxYes.Location = new System.Drawing.Point(6, 65);
            this.checkBoxYes.Name = "checkBoxYes";
            this.checkBoxYes.Size = new System.Drawing.Size(44, 17);
            this.checkBoxYes.TabIndex = 2;
            this.checkBoxYes.Text = "Yes";
            this.checkBoxYes.UseVisualStyleBackColor = true;
            this.checkBoxYes.CheckedChanged += new System.EventHandler(this.SomeCheckBox_CheckedChanged);
            // 
            // checkBoxCancel
            // 
            this.checkBoxCancel.AutoSize = true;
            this.checkBoxCancel.Enabled = false;
            this.checkBoxCancel.Location = new System.Drawing.Point(6, 42);
            this.checkBoxCancel.Name = "checkBoxCancel";
            this.checkBoxCancel.Size = new System.Drawing.Size(59, 17);
            this.checkBoxCancel.TabIndex = 1;
            this.checkBoxCancel.Text = "Cancel";
            this.checkBoxCancel.UseVisualStyleBackColor = true;
            this.checkBoxCancel.CheckedChanged += new System.EventHandler(this.SomeCheckBox_CheckedChanged);
            // 
            // checkBoxOK
            // 
            this.checkBoxOK.AutoSize = true;
            this.checkBoxOK.Location = new System.Drawing.Point(6, 19);
            this.checkBoxOK.Name = "checkBoxOK";
            this.checkBoxOK.Size = new System.Drawing.Size(41, 17);
            this.checkBoxOK.TabIndex = 0;
            this.checkBoxOK.Text = "OK";
            this.checkBoxOK.UseVisualStyleBackColor = true;
            this.checkBoxOK.CheckedChanged += new System.EventHandler(this.SomeCheckBox_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(472, 464);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 22);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(390, 464);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 22);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonPreview
            // 
            this.buttonPreview.Location = new System.Drawing.Point(178, 465);
            this.buttonPreview.Name = "buttonPreview";
            this.buttonPreview.Size = new System.Drawing.Size(75, 22);
            this.buttonPreview.TabIndex = 7;
            this.buttonPreview.Text = "&Preview...";
            this.buttonPreview.UseVisualStyleBackColor = true;
            this.buttonPreview.Click += new System.EventHandler(this.buttonPreview_Click);
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Location = new System.Drawing.Point(12, 368);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(214, 81);
            this.groupBoxDetails.TabIndex = 8;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Advanced: ";
            this.groupBoxDetails.Visible = false;
            // 
            // buttonAbout
            // 
            this.buttonAbout.Location = new System.Drawing.Point(290, 464);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(75, 22);
            this.buttonAbout.TabIndex = 9;
            this.buttonAbout.Text = "&About...";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // btn_back_menu
            // 
            this.btn_back_menu.Location = new System.Drawing.Point(19, 464);
            this.btn_back_menu.Name = "btn_back_menu";
            this.btn_back_menu.Size = new System.Drawing.Size(98, 22);
            this.btn_back_menu.TabIndex = 10;
            this.btn_back_menu.Text = "&Back to Menu";
            this.btn_back_menu.UseVisualStyleBackColor = true;
            this.btn_back_menu.Click += new System.EventHandler(this.btn_back_menu_Click);
            // 
            // MsgBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(576, 499);
            this.Controls.Add(this.btn_back_menu);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(this.buttonPreview);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxReturnValue);
            this.Controls.Add(this.groupBoxButtons);
            this.Controls.Add(this.groupBoxText);
            this.Controls.Add(this.groupBoxDefaultButton);
            this.Controls.Add(this.groupBoxIcon);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MsgBoxForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "unSigned\'s NSIS and Inno Setup MessageBox Designer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MsgBoxForm_FormClosed);
            this.groupBoxIcon.ResumeLayout(false);
            this.groupBoxIcon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQuestion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInformation)).EndInit();
            this.groupBoxDefaultButton.ResumeLayout(false);
            this.groupBoxText.ResumeLayout(false);
            this.groupBoxText.PerformLayout();
            this.groupBoxButtons.ResumeLayout(false);
            this.groupBoxButtons.PerformLayout();
            this.groupBoxReturnValue.ResumeLayout(false);
            this.groupBoxReturnValue.PerformLayout();
            this.ResumeLayout(false);

		}

		[DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=false, SetLastError=true)]
		private static extern bool InsertMenu(IntPtr hMenu, int uPosition, int uFlags, int uIDNewItem, string lpNewItem);

		private void linkLabelWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		
		}

		private void MsgBoxForm_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			IntPtr systemMenu = MsgBoxForm.GetSystemMenu(base.Handle, false);
			MsgBoxForm.AppendMenu(systemMenu, 2048, 0, string.Empty);
			MsgBoxForm.AppendMenu(systemMenu, 0, this.SYSMENU_ABOUT_ID, "&About...");
		}

		private void PrepareDialog()
		{
			if (!this.mInnoSetup)
			{
				MsgBoxForm msgBoxForm = this;
				msgBoxForm.Text = string.Concat(msgBoxForm.Text, " (NSIS)");
			}
			else
			{
				MsgBoxForm msgBoxForm1 = this;
				msgBoxForm1.Text = string.Concat(msgBoxForm1.Text, " (Inno Setup)");
			}
			if (this.mInnoSetup)
			{
				this.radioButtonNone.Visible = false;
			}
			if (!this.mInnoSetup)
			{
				this.radioButtonInformation.Text = string.Concat(this.mTextIcons[0], "\n(", this.mTextNsisIcons[0], ")");
				this.radioButtonQuestion.Text = string.Concat(this.mTextIcons[1], "\n(", this.mTextNsisIcons[1], ")");
				this.radioButtonWarning.Text = string.Concat(this.mTextIcons[2], "\n(", this.mTextNsisIcons[2], ")");
				this.radioButtonError.Text = string.Concat(this.mTextIcons[3], "\n(", this.mTextNsisIcons[3], ")");
			}
			else
			{
				this.radioButtonInformation.Text = string.Concat(this.mTextIcons[0], "\n(", this.mTextInnoSetupIcons[0], ")");
				this.radioButtonQuestion.Text = string.Concat(this.mTextIcons[1], "\n(", this.mTextInnoSetupIcons[1], ")");
				this.radioButtonWarning.Text = string.Concat(this.mTextIcons[2], "\n(", this.mTextInnoSetupIcons[2], ")");
				this.radioButtonError.Text = string.Concat(this.mTextIcons[3], "\n(", this.mTextInnoSetupIcons[3], ")");
			}
			if (!this.mInnoSetup)
			{
				this.radioButtonFirst.Text = string.Concat(this.mTextDefaultButton[0], " (", this.mTextNsisDefaultButton[0], ")");
				this.radioButtonSecond.Text = string.Concat(this.mTextDefaultButton[1], " (", this.mTextNsisDefaultButton[1], ")");
				this.radioButtonThird.Text = string.Concat(this.mTextDefaultButton[2], " (", this.mTextNsisDefaultButton[2], ")");
			}
			else
			{
				this.radioButtonFirst.Text = this.mTextDefaultButton[0];
				this.radioButtonSecond.Text = this.mTextDefaultButton[1];
				this.radioButtonThird.Text = this.mTextDefaultButton[2];
			}
			this.groupBoxDefaultButton.Enabled = false;
			if (!this.mInnoSetup)
			{
				this.radioButtonOK.Text = string.Concat(this.mTextButtons[0], " (", this.mTextNsisButtons[0], ")");
				this.radioButtonOKCancel.Text = string.Concat(this.mTextButtons[1], " (", this.mTextNsisButtons[1], ")");
				this.radioButtonYesNo.Text = string.Concat(this.mTextButtons[2], " (", this.mTextNsisButtons[2], ")");
				this.radioButtonYesNoCancel.Text = string.Concat(this.mTextButtons[3], " (", this.mTextNsisButtons[3], ")");
				this.radioButtonRetryCancel.Text = string.Concat(this.mTextButtons[4], " (", this.mTextNsisButtons[4], ")");
				this.radioButtonAbortRetryIgnore.Text = string.Concat(this.mTextButtons[5], " (", this.mTextNsisButtons[5], ")");
			}
			else
			{
				this.radioButtonOK.Text = string.Concat(this.mTextButtons[0], " (", this.mTextInnoSetupButtons[0], ")");
				this.radioButtonOKCancel.Text = string.Concat(this.mTextButtons[1], " (", this.mTextInnoSetupButtons[1], ")");
				this.radioButtonYesNo.Text = string.Concat(this.mTextButtons[2], " (", this.mTextInnoSetupButtons[2], ")");
				this.radioButtonYesNoCancel.Text = string.Concat(this.mTextButtons[3], " (", this.mTextInnoSetupButtons[3], ")");
				this.radioButtonRetryCancel.Text = string.Concat(this.mTextButtons[4], " (", this.mTextInnoSetupButtons[4], ")");
				this.radioButtonAbortRetryIgnore.Text = string.Concat(this.mTextButtons[5], " (", this.mTextInnoSetupButtons[5], ")");
			}
			if (!this.mInnoSetup)
			{
				this.checkBoxOK.Text = string.Concat(this.mTextReturn[0], " (", this.mTextNsisReturn[0], ")");
				this.checkBoxCancel.Text = string.Concat(this.mTextReturn[1], " (", this.mTextNsisReturn[1], ")");
				this.checkBoxYes.Text = string.Concat(this.mTextReturn[2], " (", this.mTextNsisReturn[2], ")");
				this.checkBoxNo.Text = string.Concat(this.mTextReturn[3], " (", this.mTextNsisReturn[3], ")");
				this.checkBoxAbort.Text = string.Concat(this.mTextReturn[4], " (", this.mTextNsisReturn[4], ")");
				this.checkBoxRetry.Text = string.Concat(this.mTextReturn[5], " (", this.mTextNsisReturn[5], ")");
				this.checkBoxIgnore.Text = string.Concat(this.mTextReturn[6], " (", this.mTextNsisReturn[6], ")");
			}
			else
			{
				this.checkBoxOK.Text = string.Concat(this.mTextReturn[0], " (", this.mTextInnoSetupReturn[0], ")");
				this.checkBoxCancel.Text = string.Concat(this.mTextReturn[1], " (", this.mTextInnoSetupReturn[1], ")");
				this.checkBoxYes.Text = string.Concat(this.mTextReturn[2], " (", this.mTextInnoSetupReturn[2], ")");
				this.checkBoxNo.Text = string.Concat(this.mTextReturn[3], " (", this.mTextInnoSetupReturn[3], ")");
				this.checkBoxAbort.Text = string.Concat(this.mTextReturn[4], " (", this.mTextInnoSetupReturn[4], ")");
				this.checkBoxRetry.Text = string.Concat(this.mTextReturn[5], " (", this.mTextInnoSetupReturn[5], ")");
				this.checkBoxIgnore.Text = string.Concat(this.mTextReturn[6], " (", this.mTextInnoSetupReturn[6], ")");
			}
			this.radioButtonOK.Checked = true;
		}

		private void SomeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.radioButtonSdOK.Enabled = false;
			this.radioButtonSdCancel.Enabled = false;
			this.radioButtonSdYes.Enabled = false;
			this.radioButtonSdNo.Enabled = false;
			this.radioButtonSdAbort.Enabled = false;
			this.radioButtonSdRetry.Enabled = false;
			this.radioButtonSdIgnore.Enabled = false;
			if (this.radioButtonOKCancel.Checked && this.checkBoxOK.Checked && this.checkBoxCancel.Checked)
			{
				this.radioButtonSdOK.Enabled = true;
				this.radioButtonSdCancel.Enabled = true;
			}
			if (this.radioButtonYesNo.Checked && this.checkBoxYes.Checked && this.checkBoxNo.Checked)
			{
				this.radioButtonSdYes.Enabled = true;
				this.radioButtonSdNo.Enabled = true;
			}
			if (this.radioButtonYesNoCancel.Checked && (this.checkBoxYes.Checked && this.checkBoxNo.Checked || this.checkBoxYes.Checked && this.checkBoxCancel.Checked || this.checkBoxCancel.Checked && this.checkBoxNo.Checked))
			{
				if (this.checkBoxYes.Checked)
				{
					this.radioButtonSdYes.Enabled = true;
				}
				if (this.checkBoxNo.Checked)
				{
					this.radioButtonSdNo.Enabled = true;
				}
				if (this.checkBoxCancel.Checked)
				{
					this.radioButtonSdCancel.Enabled = true;
				}
			}
			if (this.radioButtonRetryCancel.Checked && this.checkBoxRetry.Checked && this.checkBoxCancel.Checked)
			{
				this.radioButtonSdRetry.Enabled = true;
				this.radioButtonSdCancel.Enabled = true;
			}
			if (this.radioButtonAbortRetryIgnore.Checked && (this.checkBoxAbort.Checked && this.checkBoxRetry.Checked || this.checkBoxAbort.Checked && this.checkBoxIgnore.Checked || this.checkBoxIgnore.Checked && this.checkBoxRetry.Checked))
			{
				if (this.checkBoxAbort.Checked)
				{
					this.radioButtonSdAbort.Enabled = true;
				}
				if (this.checkBoxRetry.Checked)
				{
					this.radioButtonSdRetry.Enabled = true;
				}
				if (this.checkBoxIgnore.Checked)
				{
					this.radioButtonSdIgnore.Enabled = true;
				}
			}
			if (!this.mInnoSetup)
			{
				if (this.radioButtonYesNoCancel.Checked)
				{
					bool flag = false;
					if (this.checkBoxYes.Checked && this.checkBoxNo.Checked)
					{
						this.checkBoxCancel.Enabled = false;
						flag = true;
					}
					if (this.checkBoxYes.Checked && this.checkBoxCancel.Checked)
					{
						this.checkBoxNo.Enabled = false;
						flag = true;
					}
					if (this.checkBoxCancel.Checked && this.checkBoxNo.Checked)
					{
						this.checkBoxYes.Enabled = false;
						flag = true;
					}
					if (!flag)
					{
						this.checkBoxYes.Enabled = true;
						this.checkBoxNo.Enabled = true;
						this.checkBoxCancel.Enabled = true;
					}
				}
				if (this.radioButtonAbortRetryIgnore.Checked)
				{
					bool flag1 = false;
					if (this.checkBoxAbort.Checked && this.checkBoxRetry.Checked)
					{
						this.checkBoxIgnore.Enabled = false;
						flag1 = true;
					}
					if (this.checkBoxAbort.Checked && this.checkBoxIgnore.Checked)
					{
						this.checkBoxRetry.Enabled = false;
						flag1 = true;
					}
					if (this.checkBoxIgnore.Checked && this.checkBoxRetry.Checked)
					{
						this.checkBoxAbort.Enabled = false;
						flag1 = true;
					}
					if (!flag1)
					{
						this.checkBoxAbort.Enabled = true;
						this.checkBoxRetry.Enabled = true;
						this.checkBoxIgnore.Enabled = true;
					}
				}
			}
			if (sender is CheckBox)
			{
				CheckBox checkBox = sender as CheckBox;
				if (checkBox == this.checkBoxOK)
				{
					this.textBoxOK.Enabled = checkBox.Checked;
				}
				if (checkBox == this.checkBoxCancel)
				{
					this.textBoxCancel.Enabled = checkBox.Checked;
				}
				if (checkBox == this.checkBoxYes)
				{
					this.textBoxYes.Enabled = checkBox.Checked;
				}
				if (checkBox == this.checkBoxNo)
				{
					this.textBoxNo.Enabled = checkBox.Checked;
				}
				if (checkBox == this.checkBoxAbort)
				{
					this.textBoxAbort.Enabled = checkBox.Checked;
				}
				if (checkBox == this.checkBoxRetry)
				{
					this.textBoxRetry.Enabled = checkBox.Checked;
				}
				if (checkBox == this.checkBoxIgnore)
				{
					this.textBoxIgnore.Enabled = checkBox.Checked;
				}
			}
		}

		private void SomeRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (sender is RadioButton)
			{
				this.radioButtonFirst.Enabled = true;
				this.radioButtonSecond.Enabled = true;
				this.radioButtonThird.Enabled = true;
				this.radioButtonFirst.Checked = true;
				this.radioButtonSecond.Checked = false;
				this.radioButtonThird.Checked = false;
				this.groupBoxDefaultButton.Enabled = true;
				this.checkBoxOK.Enabled = false;
				this.checkBoxCancel.Enabled = false;
				this.checkBoxYes.Enabled = false;
				this.checkBoxNo.Enabled = false;
				this.checkBoxAbort.Enabled = false;
				this.checkBoxRetry.Enabled = false;
				this.checkBoxIgnore.Enabled = false;
				this.checkBoxOK.Checked = false;
				this.checkBoxCancel.Checked = false;
				this.checkBoxYes.Checked = false;
				this.checkBoxNo.Checked = false;
				this.checkBoxAbort.Checked = false;
				this.checkBoxRetry.Checked = false;
				this.checkBoxIgnore.Checked = false;
				this.textBoxOK.Visible = false;
				this.textBoxCancel.Visible = false;
				this.textBoxYes.Visible = false;
				this.textBoxNo.Visible = false;
				this.textBoxAbort.Visible = false;
				this.textBoxRetry.Visible = false;
				this.textBoxIgnore.Visible = false;
				this.radioButtonSdOK.Visible = false;
				this.radioButtonSdCancel.Visible = false;
				this.radioButtonSdYes.Visible = false;
				this.radioButtonSdNo.Visible = false;
				this.radioButtonSdAbort.Visible = false;
				this.radioButtonSdRetry.Visible = false;
				this.radioButtonSdIgnore.Visible = false;
				this.radioButtonSdOK.Enabled = false;
				this.radioButtonSdCancel.Enabled = false;
				this.radioButtonSdYes.Enabled = false;
				this.radioButtonSdNo.Enabled = false;
				this.radioButtonSdAbort.Enabled = false;
				this.radioButtonSdRetry.Enabled = false;
				this.radioButtonSdIgnore.Enabled = false;
				RadioButton radioButton = sender as RadioButton;
				if (radioButton.Checked)
				{
					if (radioButton == this.radioButtonOK)
					{
						this.groupBoxDefaultButton.Enabled = false;
					}
					if (radioButton == this.radioButtonOKCancel)
					{
						this.checkBoxOK.Enabled = true;
						this.checkBoxCancel.Enabled = true;
						this.radioButtonThird.Enabled = false;
						if (!this.mInnoSetup)
						{
							this.textBoxOK.Visible = true;
							this.textBoxOK.Text = "MessageBox_OK";
							this.textBoxCancel.Visible = true;
							this.textBoxCancel.Text = "MessageBox_Cancel";
							this.radioButtonSdOK.Visible = true;
							this.radioButtonSdCancel.Visible = true;
						}
					}
					if (radioButton == this.radioButtonYesNo)
					{
						this.checkBoxYes.Enabled = true;
						this.checkBoxNo.Enabled = true;
						this.radioButtonThird.Enabled = false;
						if (!this.mInnoSetup)
						{
							this.textBoxYes.Visible = true;
							this.textBoxYes.Text = "MessageBox_Yes";
							this.textBoxNo.Visible = true;
							this.textBoxNo.Text = "MessageBox_No";
							this.radioButtonSdYes.Visible = true;
							this.radioButtonSdNo.Visible = true;
						}
					}
					if (radioButton == this.radioButtonYesNoCancel)
					{
						this.checkBoxYes.Enabled = true;
						this.checkBoxNo.Enabled = true;
						this.checkBoxCancel.Enabled = true;
						if (!this.mInnoSetup)
						{
							this.textBoxYes.Visible = true;
							this.textBoxYes.Text = "MessageBox_Yes";
							this.textBoxNo.Visible = true;
							this.textBoxNo.Text = "MessageBox_No";
							this.textBoxCancel.Visible = true;
							this.textBoxCancel.Text = "MessageBox_Cancel";
							this.radioButtonSdYes.Visible = true;
							this.radioButtonSdNo.Visible = true;
							this.radioButtonSdCancel.Visible = true;
						}
					}
					if (radioButton == this.radioButtonRetryCancel)
					{
						this.checkBoxRetry.Enabled = true;
						this.checkBoxCancel.Enabled = true;
						this.radioButtonThird.Enabled = false;
						if (!this.mInnoSetup)
						{
							this.textBoxRetry.Visible = true;
							this.textBoxRetry.Text = "MessageBox_Retry";
							this.textBoxCancel.Visible = true;
							this.textBoxCancel.Text = "MessageBox_Cancel";
							this.radioButtonSdRetry.Visible = true;
							this.radioButtonSdCancel.Visible = true;
						}
					}
					if (radioButton == this.radioButtonAbortRetryIgnore)
					{
						this.checkBoxAbort.Enabled = true;
						this.checkBoxRetry.Enabled = true;
						this.checkBoxIgnore.Enabled = true;
						if (!this.mInnoSetup)
						{
							this.textBoxAbort.Visible = true;
							this.textBoxAbort.Text = "MessageBox_Abort";
							this.textBoxRetry.Visible = true;
							this.textBoxRetry.Text = "MessageBox_Retry";
							this.textBoxIgnore.Visible = true;
							this.textBoxIgnore.Text = "MessageBox_Ignore";
							this.radioButtonSdAbort.Visible = true;
							this.radioButtonSdRetry.Visible = true;
							this.radioButtonSdIgnore.Visible = true;
						}
					}
				}
			}
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == 274 && (int)m.WParam == this.SYSMENU_ABOUT_ID)
			{
				this.buttonAbout_Click(null, null);
			}
		}

        private void btn_back_menu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}