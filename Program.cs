using System;
using System.Windows.Forms;

namespace MessageBoxDesigner
{
	internal static class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			bool flag = false;
			bool flag1 = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
            bool flag5 = false;

            for (int i = 0; i < (int)args.Length; i++)
			{
				if (args[i].IndexOf("NSIS", StringComparison.OrdinalIgnoreCase) > 0)
				{
					flag = true;
					flag1 = false;
				}
				if (args[i].IndexOf("INNO", StringComparison.OrdinalIgnoreCase) > 0)
				{
					flag = true;
					flag1 = true;
				}
				if (args[i].IndexOf("NOPREVIEW", StringComparison.OrdinalIgnoreCase) > 0)
				{
					flag2 = true;
				}
				if (args[i].IndexOf("NOCOPY", StringComparison.OrdinalIgnoreCase) > 0)
				{
					flag3 = true;
				}
				if (args[i].IndexOf("NOLINK", StringComparison.OrdinalIgnoreCase) > 0)
				{
					flag4 = true;
				}
                if (args[i].IndexOf("NOLINK", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    flag5 = true;
                }
            }
			if (!flag3)
			{
				Clipboard.Clear();
			}
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (flag)
			{
				Application.Run(new MsgBoxForm(flag1, flag2, flag3, flag4, flag5));
				return;
			}
			Application.Run(new StartForm(flag1, flag2, flag3, flag4, flag5));
		}
	}
}