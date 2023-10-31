using System.Xml;
using HYL.MountBlue.Classes.Lekce;

namespace HYL.MountBlue.Classes.Cviceni;

internal abstract class _Cviceni
{
	private OznaceniCviceni oznCvic;

	private uint uintID;

	internal OznaceniCviceni OznaceniCviceni => oznCvic;

	internal uint ID => uintID;

	internal _Cviceni(OznaceniCviceni oznCv, ref XmlNode n)
	{
		oznCvic = oznCv;
		uintID = uint.Parse(n.Attributes["ID"].Value);
	}

	internal string IDsLekci()
	{
		return IDsLekci(OznaceniCviceni.Lekce, ID);
	}

	internal static string IDsLekci(int iLekce, uint uiID)
	{
		uint cislo = HYL.MountBlue.Classes.Cviceni.ID.Checksum(iLekce, uiID);
		string arg = HYL.MountBlue.Classes.Cviceni.ID.CisloNaText(uiID, 2);
		string arg2 = HYL.MountBlue.Classes.Cviceni.ID.CisloNaText(cislo);
		return $"{iLekce:000}-{arg}-{arg2}";
	}

	public static _Cviceni NactiCviceni(OznaceniCviceni oznCvic)
	{
		XmlNode node = _Lekce.Lekce().Cviceni(oznCvic);
		return NactiCviceni(oznCvic, node);
	}

	public static _Cviceni NactiCviceni(OznaceniCviceni oznCvic, XmlNode node)
	{
		if (node == null)
		{
			return null;
		}
		_Cviceni result = null;
		if (node.SelectSingleNode("klavesnice") != null)
		{
			result = new Klavesnice(oznCvic, ref node);
		}
		else if (node.SelectSingleNode("psani") != null)
		{
			result = new Psani(oznCvic, ref node);
		}
		else if (node.SelectSingleNode("spravne_sezeni") != null)
		{
			result = new Sezeni(oznCvic, ref node);
		}
		return result;
	}
}
