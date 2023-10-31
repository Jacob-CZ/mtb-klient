using System;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Lekce;

internal class Lekce : _Lekce
{
	public enum eLekce
	{
		Lekce001 = 1,
		Lekce002,
		Lekce003,
		Lekce004,
		Lekce005,
		Lekce006,
		Lekce007,
		Lekce008,
		Lekce009,
		Lekce010,
		Lekce011,
		Lekce012,
		Lekce013,
		Lekce014,
		Lekce015,
		Lekce016,
		Lekce017,
		Lekce018,
		Lekce019,
		Lekce020,
		Lekce021,
		Lekce022,
		Lekce023,
		Lekce024,
		Lekce025,
		Lekce026,
		Lekce027,
		Lekce028,
		Lekce029,
		Lekce030,
		Lekce031,
		Lekce032,
		Lekce033,
		Lekce034,
		Lekce035,
		Lekce036,
		Lekce037,
		Lekce038,
		Lekce039,
		Lekce040,
		Lekce041,
		Lekce042,
		Lekce043,
		Lekce044,
		Lekce045,
		Lekce046
	}

	public enum eTrenink
	{
		Trenink001 = 800,
		Trenink002,
		Trenink003,
		Trenink004,
		Trenink005,
		Trenink006,
		Trenink007,
		Trenink008,
		Trenink009
	}

	internal override string DataLekce(int iLekce)
	{
		if (iLekce < 800)
		{
			return (eLekce)iLekce switch
			{
				eLekce.Lekce001 => HYL.MountBlue.Resources.Lekce.Lekce001, 
				eLekce.Lekce002 => HYL.MountBlue.Resources.Lekce.Lekce002, 
				eLekce.Lekce003 => HYL.MountBlue.Resources.Lekce.Lekce003, 
				eLekce.Lekce004 => HYL.MountBlue.Resources.Lekce.Lekce004, 
				eLekce.Lekce005 => HYL.MountBlue.Resources.Lekce.Lekce005, 
				eLekce.Lekce006 => HYL.MountBlue.Resources.Lekce.Lekce006, 
				eLekce.Lekce007 => HYL.MountBlue.Resources.Lekce.Lekce007, 
				eLekce.Lekce008 => HYL.MountBlue.Resources.Lekce.Lekce008, 
				eLekce.Lekce009 => HYL.MountBlue.Resources.Lekce.Lekce009, 
				eLekce.Lekce010 => HYL.MountBlue.Resources.Lekce.Lekce010, 
				eLekce.Lekce011 => HYL.MountBlue.Resources.Lekce.Lekce011, 
				eLekce.Lekce012 => HYL.MountBlue.Resources.Lekce.Lekce012, 
				eLekce.Lekce013 => HYL.MountBlue.Resources.Lekce.Lekce013, 
				eLekce.Lekce014 => HYL.MountBlue.Resources.Lekce.Lekce014, 
				eLekce.Lekce015 => HYL.MountBlue.Resources.Lekce.Lekce015, 
				eLekce.Lekce016 => HYL.MountBlue.Resources.Lekce.Lekce016, 
				eLekce.Lekce017 => HYL.MountBlue.Resources.Lekce.Lekce017, 
				eLekce.Lekce018 => HYL.MountBlue.Resources.Lekce.Lekce018, 
				eLekce.Lekce019 => HYL.MountBlue.Resources.Lekce.Lekce019, 
				eLekce.Lekce020 => HYL.MountBlue.Resources.Lekce.Lekce020, 
				eLekce.Lekce021 => HYL.MountBlue.Resources.Lekce.Lekce021, 
				eLekce.Lekce022 => HYL.MountBlue.Resources.Lekce.Lekce022, 
				eLekce.Lekce023 => HYL.MountBlue.Resources.Lekce.Lekce023, 
				eLekce.Lekce024 => HYL.MountBlue.Resources.Lekce.Lekce024, 
				eLekce.Lekce025 => HYL.MountBlue.Resources.Lekce.Lekce025, 
				eLekce.Lekce026 => HYL.MountBlue.Resources.Lekce.Lekce026, 
				eLekce.Lekce027 => HYL.MountBlue.Resources.Lekce.Lekce027, 
				eLekce.Lekce028 => HYL.MountBlue.Resources.Lekce.Lekce028, 
				eLekce.Lekce029 => HYL.MountBlue.Resources.Lekce.Lekce029, 
				eLekce.Lekce030 => HYL.MountBlue.Resources.Lekce.Lekce030, 
				eLekce.Lekce031 => HYL.MountBlue.Resources.Lekce.Lekce031, 
				eLekce.Lekce032 => HYL.MountBlue.Resources.Lekce.Lekce032, 
				eLekce.Lekce033 => HYL.MountBlue.Resources.Lekce.Lekce033, 
				eLekce.Lekce034 => HYL.MountBlue.Resources.Lekce.Lekce034, 
				eLekce.Lekce035 => HYL.MountBlue.Resources.Lekce.Lekce035, 
				eLekce.Lekce036 => HYL.MountBlue.Resources.Lekce.Lekce036, 
				eLekce.Lekce037 => HYL.MountBlue.Resources.Lekce.Lekce037, 
				eLekce.Lekce038 => HYL.MountBlue.Resources.Lekce.Lekce038, 
				eLekce.Lekce039 => HYL.MountBlue.Resources.Lekce.Lekce039, 
				eLekce.Lekce040 => HYL.MountBlue.Resources.Lekce.Lekce040, 
				eLekce.Lekce041 => HYL.MountBlue.Resources.Lekce.Lekce041, 
				eLekce.Lekce042 => HYL.MountBlue.Resources.Lekce.Lekce042, 
				eLekce.Lekce043 => HYL.MountBlue.Resources.Lekce.Lekce043, 
				eLekce.Lekce044 => HYL.MountBlue.Resources.Lekce.Lekce044, 
				eLekce.Lekce045 => HYL.MountBlue.Resources.Lekce.Lekce045, 
				eLekce.Lekce046 => HYL.MountBlue.Resources.Lekce.Lekce046, 
				_ => null, 
			};
		}
		return (eTrenink)iLekce switch
		{
			eTrenink.Trenink001 => HYL.MountBlue.Resources.Lekce.Trenink001, 
			eTrenink.Trenink002 => HYL.MountBlue.Resources.Lekce.Trenink002, 
			eTrenink.Trenink003 => HYL.MountBlue.Resources.Lekce.Trenink003, 
			eTrenink.Trenink004 => HYL.MountBlue.Resources.Lekce.Trenink004, 
			eTrenink.Trenink005 => HYL.MountBlue.Resources.Lekce.Trenink005, 
			eTrenink.Trenink006 => HYL.MountBlue.Resources.Lekce.Trenink006, 
			eTrenink.Trenink007 => HYL.MountBlue.Resources.Lekce.Trenink007, 
			eTrenink.Trenink008 => HYL.MountBlue.Resources.Lekce.Trenink008, 
			eTrenink.Trenink009 => HYL.MountBlue.Resources.Lekce.Trenink009, 
			_ => null, 
		};
	}

	internal override string DataKlasifikace()
	{
		return HYL.MountBlue.Resources.Lekce.klasifikace;
	}

	internal override string DataKlavesy()
	{
		return HYL.MountBlue.Resources.Lekce.klavesy;
	}

	protected override bool CisloDalsiLekce(int iSoucasnaLekce, out int iNovaLekce)
	{
		bool flag = false;
		Array values = Enum.GetValues(typeof(eLekce));
		foreach (int item in values)
		{
			if (flag)
			{
				iNovaLekce = item;
				return true;
			}
			flag = item == iSoucasnaLekce;
		}
		Array values2 = Enum.GetValues(typeof(eTrenink));
		foreach (int item2 in values2)
		{
			if (flag)
			{
				iNovaLekce = item2;
				return true;
			}
			flag = item2 == iSoucasnaLekce;
		}
		if (flag)
		{
			iNovaLekce = 800;
			return true;
		}
		iNovaLekce = 0;
		return false;
	}
}
