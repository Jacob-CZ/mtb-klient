using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace HYL.MountBlue.Resources;

[CompilerGenerated]
[DebuggerNonUserCode]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
internal class Avatary
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(resourceMan, null))
			{
				ResourceManager resourceManager = new ResourceManager("HYL.MountBlue.Resources.Avatary", typeof(Avatary).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Bitmap pngAvatarAdmin
	{
		get
		{
			object @object = ResourceManager.GetObject("pngAvatarAdmin", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap pngAvatarMuz1
	{
		get
		{
			object @object = ResourceManager.GetObject("pngAvatarMuz1", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap pngAvatarMuz2
	{
		get
		{
			object @object = ResourceManager.GetObject("pngAvatarMuz2", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap pngAvatarUcitel
	{
		get
		{
			object @object = ResourceManager.GetObject("pngAvatarUcitel", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap pngAvatarZena1
	{
		get
		{
			object @object = ResourceManager.GetObject("pngAvatarZena1", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap pngAvatarZena2
	{
		get
		{
			object @object = ResourceManager.GetObject("pngAvatarZena2", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal Avatary()
	{
	}
}
