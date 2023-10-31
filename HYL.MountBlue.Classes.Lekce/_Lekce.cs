using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Lekce;

internal abstract class _Lekce
{
	internal const int MaximalniCisloLekce = 46;

	public const string xEncoding = "UTF-8";

	public const string xStandalone = "yes";

	public const string xVersion = "1.0";

	public const string xPrefix = "smb";

	public const string xNamespace = "hyl";

	public const string xVerze = "verze";

	public const string xLekce = "lekce";

	public const string xProjekt = "projekt";

	public const string xPoznamka = "poznamka";

	public const string xVetev = "vetev";

	public const string xNazev = "nazev";

	public const string xCviceni = "cviceni";

	public const string xKlavesnice = "klavesnice";

	public const string xPsani = "psani";

	public const string xCislo = "cislo";

	public const string xID = "ID";

	public const string xCisloKlavesy = "cislo_klavesy";

	public const string xShift = "shift";

	public const string xModifikator = "modifikator";

	public const string xZnameKlavesy = "zname_klavesy";

	public const string xPrst = "prst";

	public const string xText = "text";

	public const string xKodovano = "kodovano";

	public const string xVlastnosti = "vlastnosti";

	public const string xPocet = "pocet";

	public const string xZpusob = "zpusob";

	public const string xEnter = "enter";

	public const string xBackspace = "backspace";

	public const string xCas = "cas";

	public const string xPenalizace = "penalizace";

	public const string xRychlost = "rychlost";

	public const string xZnaky = "znaky";

	public const string xUkazka = "ukazka";

	public const string xVyhodnoceni = "vyhodnoceni";

	public const string xTypVyhodnoceni = "typvyhodnoceni";

	public const string xKriterium = "kriterium";

	public const string xVychoziVetev = "vychozivetev";

	public const string xPodminka = "podminka";

	public const string xHodnota = "hodnota";

	public const string xVetevAtr = "vetev";

	public const string xZnamka = "znamka";

	public const string xKlasifikaceOd = "klasifikace_od";

	public const string xKlasifikaceDo = "klasifikace_do";

	public const string xOdmena = "odmena";

	public const string xTypOdmeny = "typodmeny";

	public const string xSezeni = "spravne_sezeni";

	public const string xPopis = "popis";

	public const string xKlavesy = "klavesy";

	public const string xKlavesa = "klavesa";

	public const string xJeZnama = "je_znama";

	public const string xVetevXPath = "vetev[@nazev=\"{0}\"]";

	public const string xCviceniXPath = "cviceni[@cislo=\"{0}\"]";

	public const string xCviceniKlasifikaceXPath = "cviceni";

	public const string xLekceXPath = "lekce";

	public const string xKlavesaXPath = "klavesa";

	public const string xCviceniIDXPath = "vetev/cviceni[@ID=\"{0}\"]";

	private XmlDocument lekcePosledni;

	private int cisloPosledniLekce;

	private static _Lekce lekce;

	private Klasifikace klasifikace;

	private Klavesy klavesy;

	internal Klasifikace Klasifikace => klasifikace;

	internal Klavesy Klavesy => klavesy;

	internal static void InicializovatLekce()
	{
		lekce = new Lekce();
	}

	internal static _Lekce Lekce()
	{
		return lekce;
	}

	public _Lekce()
	{
		lekcePosledni = null;
		cisloPosledniLekce = -1;
		klasifikace = new Klasifikace(DataKlasifikaceXml());
		klavesy = new Klavesy(DataKlavesyXml());
	}

	internal static string BoolToString(bool value)
	{
		if (value)
		{
			return "1";
		}
		return "0";
	}

	internal abstract string DataLekce(int iLekce);

	internal abstract string DataKlasifikace();

	internal abstract string DataKlavesy();

	internal XmlDocument DataLekceXml(int iLekce)
	{
		return StringToValidXmlDoc(DataLekce(iLekce), XSD.Lekce);
	}

	internal XmlDocument DataKlasifikaceXml()
	{
		return StringToValidXmlDoc(DataKlasifikace(), XSD.Lekce);
	}

	internal XmlDocument DataKlavesyXml()
	{
		return StringToValidXmlDoc(DataKlavesy(), XSD.Klavesy);
	}

	protected static XmlDocument StringToValidXmlDoc(string soubor, string XSD)
	{
		if (soubor == null)
		{
			return null;
		}
		try
		{
			TextReader xmlSoubor = new StringReader(soubor);
			ValidateXML(xmlSoubor, XSD, out var DocumentXML);
			return DocumentXML;
		}
		catch
		{
			return null;
		}
	}

	protected static bool ValidateXML(TextReader xmlSoubor, string XSD, out XmlDocument DocumentXML)
	{
		try
		{
			TextReader input = new StringReader(XSD);
			XmlReader schemaDocument = XmlReader.Create(input);
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.Schemas.Add(null, schemaDocument);
			xmlReaderSettings.ValidationEventHandler += ErrorHandlerXSD;
			xmlReaderSettings.ValidationFlags &= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			XmlReader reader = XmlReader.Create(xmlSoubor, xmlReaderSettings);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			DocumentXML = xmlDocument;
			return true;
		}
		catch (XmlException ex)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Lekce_msgboxChybaXML, ex.Message), HYL.MountBlue.Resources.Texty.Lekce_msgboxChyba_Title, eMsgBoxTlacitka.OK);
			DocumentXML = null;
			return false;
		}
		catch (XmlSchemaValidationException ex2)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Lekce_msgboxChybaValidaceXML, ex2.Message), HYL.MountBlue.Resources.Texty.Lekce_msgboxChyba_Title, eMsgBoxTlacitka.OK);
			DocumentXML = null;
			return false;
		}
		catch (Exception ex3)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Lekce_msgboxChyba, ex3.Message), HYL.MountBlue.Resources.Texty.Lekce_msgboxChyba_Title, eMsgBoxTlacitka.OK);
			DocumentXML = null;
			return false;
		}
	}

	private static void ErrorHandlerXSD(object sender, ValidationEventArgs e)
	{
		if (e.Severity == XmlSeverityType.Error)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Lekce_msgboxChybaValidaceXML, e.Message), HYL.MountBlue.Resources.Texty.Lekce_msgboxChyba_Title, eMsgBoxTlacitka.OK);
		}
	}

	internal static void KodovatText(ref string text)
	{
		long num = 23L;
		long num2 = 317L;
		long num3 = 115L;
		StringBuilder stringBuilder = new StringBuilder();
		char[] array = text.ToCharArray();
		foreach (char value in array)
		{
			int num4 = Convert.ToInt32(value);
			int num5 = ((num4 >= 48 && num4 <= 57) ? (num4 - 48) : ((num4 >= 63 && num4 <= 90) ? (num4 - 53) : ((num4 < 97 || num4 > 122) ? (-1) : (num4 - 59))));
			if (num5 >= 0)
			{
				num = (num * num2 + num3) % 32768;
				num5 = (int)((num & 0x3F) ^ num5);
				if (num5 >= 0 && num5 <= 9)
				{
					num4 = num5 + 48;
				}
				else if (num5 >= 10 && num5 <= 37)
				{
					num4 = num5 + 53;
				}
				else if (num5 >= 38 && num5 <= 63)
				{
					num4 = num5 + 59;
				}
			}
			stringBuilder.Append(Convert.ToChar(num4));
		}
		text = stringBuilder.ToString();
	}

	private XmlDocument NactiLekci(int iCisloLekce)
	{
		if (cisloPosledniLekce == iCisloLekce)
		{
			return lekcePosledni;
		}
		return NacistLekciPosledni(iCisloLekce);
	}

	private XmlDocument NacistLekciPosledni(int iCisloLekce)
	{
		XmlDocument xmlDocument = DataLekceXml(iCisloLekce);
		if (xmlDocument != null)
		{
			lekcePosledni = xmlDocument;
			cisloPosledniLekce = iCisloLekce;
			return xmlDocument;
		}
		return null;
	}

	internal XmlNode Cviceni(OznaceniCviceni ozn)
	{
		try
		{
			XmlDocument xmlDocument = NactiLekci(ozn.Lekce);
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode($"vetev[@nazev=\"{ozn.Vetev}\"]");
			if (xmlNode == null)
			{
				return null;
			}
			XmlNode xmlNode2 = xmlNode.SelectSingleNode($"cviceni[@cislo=\"{ozn.Cviceni}\"]");
			if (xmlNode2 == null)
			{
				return null;
			}
			return xmlNode2;
		}
		catch
		{
			return null;
		}
	}

	internal OznaceniCviceni OveritCisloCviceni(OznaceniCviceni navrhCviceni)
	{
		XmlNode xmlNode = Cviceni(navrhCviceni);
		if (xmlNode != null)
		{
			return navrhCviceni;
		}
		if (!CisloDalsiLekce(navrhCviceni.Lekce, out var iNovaLekce))
		{
			return null;
		}
		OznaceniCviceni oznaceniCviceni = new OznaceniCviceni(iNovaLekce);
		if (oznaceniCviceni.JeTrenink)
		{
			int num = PocetCviceniVeVetviLekce(oznaceniCviceni);
			if (num > 0)
			{
				Random random = new Random();
				int iNoveCviceni = random.Next(num) + 1;
				oznaceniCviceni = new OznaceniCviceni(oznaceniCviceni, iNoveCviceni);
			}
		}
		xmlNode = Cviceni(oznaceniCviceni);
		if (xmlNode != null)
		{
			return oznaceniCviceni;
		}
		return null;
	}

	private int PocetCviceniVeVetviLekce(OznaceniCviceni ozn)
	{
		int num = 0;
		XmlDocument xmlDocument = NactiLekci(ozn.Lekce);
		XmlElement documentElement = xmlDocument.DocumentElement;
		XmlNode xmlNode = documentElement.SelectSingleNode($"vetev[@nazev=\"{ozn.Vetev}\"]");
		if (xmlNode == null)
		{
			return num;
		}
		XmlNode xmlNode2;
		do
		{
			num++;
			xmlNode2 = xmlNode.SelectSingleNode($"cviceni[@cislo=\"{num}\"]");
		}
		while (xmlNode2 != null);
		return num - 1;
	}

	internal bool NajitCisloCviceni(int lekce, uint cviceniID, out char vetev, out int cviceni)
	{
		vetev = '\0';
		cviceni = 0;
		try
		{
			XmlDocument xmlDocument = NactiLekci(lekce);
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode($"vetev/cviceni[@ID=\"{cviceniID}\"]");
			if (xmlNode == null)
			{
				return false;
			}
			cviceni = int.Parse(xmlNode.Attributes["cislo"].Value);
			vetev = char.Parse(xmlNode.ParentNode.Attributes["nazev"].Value);
			return true;
		}
		catch
		{
			return false;
		}
	}

	protected abstract bool CisloDalsiLekce(int iSoucasnaLekce, out int iNovaLekce);

	internal string[] TextyLekce(int iLekce)
	{
		List<string> list = new List<string>();
		try
		{
			XmlDocument xmlDocument = NactiLekci(iLekce);
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode($"vetev[@nazev=\"{'*'}\"]");
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("cviceni");
			foreach (XmlNode item in xmlNodeList)
			{
				_Cviceni cviceni = _Cviceni.NactiCviceni(new OznaceniCviceni(), item);
				if (cviceni is Psani)
				{
					Psani psani = (Psani)cviceni;
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(psani.CelyText);
					stringBuilder.Replace("\r\n", "\n");
					stringBuilder.Replace("\r", "\n");
					stringBuilder.Replace("\n\n", "\n");
					stringBuilder.Replace("\n", "\r\n");
					list.Add(stringBuilder.ToString());
				}
			}
			return list.ToArray();
		}
		catch
		{
			return new string[0];
		}
	}
}
