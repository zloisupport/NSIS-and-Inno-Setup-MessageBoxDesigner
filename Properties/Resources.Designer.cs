using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MessageBoxDesigner.Properties
{
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		private static System.Resources.ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap error
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("error", Resources.resourceCulture);
			}
		}

		internal static Bitmap information
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("information", Resources.resourceCulture);
			}
		}

		internal static Bitmap inno_setup
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("inno-setup", Resources.resourceCulture);
			}
		}

		internal static Bitmap nsis
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("nsis", Resources.resourceCulture);
			}
		}

		internal static Bitmap question
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("question", Resources.resourceCulture);
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					Resources.resourceMan = new System.Resources.ResourceManager("MessageBoxDesigner.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		internal static Bitmap warning
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("warning", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}