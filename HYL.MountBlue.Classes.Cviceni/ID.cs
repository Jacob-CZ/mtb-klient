using System;
using System.Text;

namespace HYL.MountBlue.Classes.Cviceni;

internal static class ID
{
	private static char[] znaky = new char[34]
	{
		'T', 'Q', '8', '5', 'B', 'R', 'C', '1', 'N', 'L',
		'U', '9', '7', 'J', '3', 'Z', '0', '6', 'Y', '4',
		'E', 'X', 'V', 'F', 'W', 'M', 'D', 'H', 'S', '2',
		'K', 'P', 'A', 'G'
	};

	internal static string CisloNaText(uint cislo)
	{
		if (cislo == 0)
		{
			return znaky[0].ToString();
		}
		return IntToStr(cislo);
	}

	internal static string CisloNaText(uint cislo, byte pocetMist)
	{
		if (cislo >= MaxValue(pocetMist))
		{
			throw new ArgumentOutOfRangeException($"Zadaná hodnota ({cislo}) je mimo rozsah pro zadaný počet míst ({pocetMist})!");
		}
		string text = CisloNaText(cislo);
		while (text.Length < pocetMist)
		{
			text = CisloNaText(0u) + text;
		}
		return text;
	}

	internal static uint TextNaCislo(string cislo)
	{
		AutoNahrazeni(ref cislo);
		if (!ParseText(cislo))
		{
			return 0u;
		}
		return StrToInt(cislo);
	}

	private static string IntToStr(uint cislo)
	{
		if (cislo == 0)
		{
			return string.Empty;
		}
		uint num = (uint)(cislo % znaky.Length);
		uint cislo2 = (uint)((cislo - num) / znaky.Length);
		return IntToStr(cislo2) + znaky[num];
	}

	internal static uint MaxValue(byte pocetMist)
	{
		return (uint)Math.Pow(znaky.Length, (int)pocetMist);
	}

	internal static uint MinValue()
	{
		return 0u;
	}

	private static byte IndexZnaku(char znak)
	{
		for (int i = 0; i < znaky.Length; i++)
		{
			if (znaky[i] == znak)
			{
				return (byte)i;
			}
		}
		return byte.MaxValue;
	}

	public static bool ParseText(string cislo)
	{
		AutoNahrazeni(ref cislo);
		char[] array = cislo.ToCharArray();
		foreach (char znak in array)
		{
			if (IndexZnaku(znak) == byte.MaxValue)
			{
				return false;
			}
		}
		return true;
	}

	private static uint StrToInt(string cislo)
	{
		if (cislo.Length == 0)
		{
			return 0u;
		}
		char znak = cislo.Substring(cislo.Length - 1, 1).ToUpper().ToCharArray()[0];
		string cislo2 = cislo.Substring(0, cislo.Length - 1);
		int num = IndexZnaku(znak);
		return (uint)(StrToInt(cislo2) * znaky.Length + num);
	}

	private static void AutoNahrazeni(ref string cislo)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(cislo);
		stringBuilder.Replace('O', '0');
		stringBuilder.Replace('o', '0');
		stringBuilder.Replace('I', '1');
		stringBuilder.Replace('i', '1');
		cislo = stringBuilder.ToString();
	}

	internal static uint Checksum(int iLekce, uint uiID)
	{
		return (uint)((iLekce + 3 * uiID) % znaky.Length);
	}
}
