using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HYL.MountBlue.Properties;

[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
[CompilerGenerated]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[UserScopedSetting]
	[DefaultSettingValue("")]
	[DebuggerNonUserCode]
	public string UzivJmeno
	{
		get
		{
			return (string)this["UzivJmeno"];
		}
		set
		{
			this["UzivJmeno"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool SkrytDokoncene
	{
		get
		{
			return (bool)this["SkrytDokoncene"];
		}
		set
		{
			this["SkrytDokoncene"] = value;
		}
	}

	[DefaultSettingValue("")]
	[DebuggerNonUserCode]
	[UserScopedSetting]
	public string SerioveCislo
	{
		get
		{
			return (string)this["SerioveCislo"];
		}
		set
		{
			this["SerioveCislo"] = value;
		}
	}

	[UserScopedSetting]
	[DefaultSettingValue("True")]
	[DebuggerNonUserCode]
	public bool AutomatickaAktivace
	{
		get
		{
			return (bool)this["AutomatickaAktivace"];
		}
		set
		{
			this["AutomatickaAktivace"] = value;
		}
	}
}
