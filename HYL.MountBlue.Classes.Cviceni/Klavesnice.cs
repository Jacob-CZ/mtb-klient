using System.Xml;
using HYL.MountBlue.Classes.Klavesnice;

namespace HYL.MountBlue.Classes.Cviceni;

internal class Klavesnice : _Cviceni
{
	private KlavesaKlavesnice.eKlavesy mbytCisloKlavesy;

	private KlavesaKlavesnice.eModifikatory mbytModifikator;

	private bool mbolShift;

	private string mstrZnameKlavesy;

	internal KlavesaKlavesnice.eKlavesy CisloKlavesy => mbytCisloKlavesy;

	internal bool Shift => mbolShift;

	internal KlavesaKlavesnice.eModifikatory Modifikator => mbytModifikator;

	internal string ZnameKlavesy => mstrZnameKlavesy;

	internal string ZnameKlavesyVcetne
	{
		get
		{
			string text = ZnameKlavesy;
			if (text == null)
			{
				text = string.Empty;
			}
			if (text.Length > 0)
			{
				text += ';';
			}
			string text2 = text;
			byte b = (byte)mbytCisloKlavesy;
			return text2 + b;
		}
	}

	public Klavesnice(OznaceniCviceni oznCv, ref XmlNode n)
		: base(oznCv, ref n)
	{
		if (n != null)
		{
			XmlNode xmlNode = n.SelectSingleNode("klavesnice");
			mbytCisloKlavesy = (KlavesaKlavesnice.eKlavesy)byte.Parse(xmlNode.Attributes["cislo_klavesy"].Value);
			mbytModifikator = (KlavesaKlavesnice.eModifikatory)byte.Parse(xmlNode.Attributes["modifikator"].Value);
			mbolShift = xmlNode.Attributes["shift"].Value != "0";
			mstrZnameKlavesy = xmlNode.Attributes["zname_klavesy"].Value;
		}
	}
}
