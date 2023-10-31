using System.Xml;

namespace HYL.MountBlue.Classes.Cviceni;

internal class Sezeni : _Cviceni
{
	private string sPopis;

	internal string Popis => sPopis;

	public Sezeni(OznaceniCviceni oznCv, ref XmlNode n)
		: base(oznCv, ref n)
	{
		if (n != null)
		{
			XmlNode xmlNode = n.SelectSingleNode("spravne_sezeni");
			sPopis = XmlConvert.DecodeName(xmlNode.Attributes["popis"].Value);
		}
	}
}
