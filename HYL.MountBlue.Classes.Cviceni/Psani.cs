using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Texty;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Cviceni;

internal class Psani : _Cviceni, IEnumerable<Podminka>, IEnumerable
{
	public enum eZpusob
	{
		normalne,
		bezMezer,
		zpravaDoleva,
		slovaObsahujici,
		slovaZacinajici,
		slepaObrazovka
	}

	public enum eKriterium
	{
		pocetChybAbs,
		pocetChybRel,
		cistychUhozuZaMin
	}

	public enum eOdmenaTyp : byte
	{
		zadna,
		MalaHvezdicka
	}

	public enum eVyhodnoceniTyp
	{
		zadne,
		vetveni,
		klasifikace
	}

	private string mstrText;

	private string mstrCelyText;

	private int mintPocet;

	private int mintSkutecnyPocet;

	private eZpusob mintZpusob;

	private bool mbolEnter;

	private bool mbolBackspace;

	private float msngCas;

	private int mintPenalizace;

	private int mintRychlost;

	private string mstrZnaky;

	private bool mbolUkazka;

	private int mintKlasifikaceOd;

	private int mintKlasifikaceDo;

	private eVyhodnoceniTyp mintVyhodnoceni;

	private eKriterium mintVyhodnoceniKriterium;

	private char mchrVychoziVetev;

	private bool mbolOdmena;

	private eKriterium mintOdmenaKriterium;

	private float msngOdmenaHodnota;

	private eOdmenaTyp mintOdmenaTyp;

	private List<Podminka> mlstPodminky;

	public string Text => mstrText;

	public string TextZadani
	{
		get
		{
			if (Enter)
			{
				return Text + '\n';
			}
			return Text;
		}
	}

	public string TextZadaniSopakovanim
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i <= Pocet; i++)
			{
				if (Enter)
				{
					stringBuilder.Append(Text + '\n');
				}
				else
				{
					stringBuilder.Append(Text + ' ');
				}
			}
			stringBuilder.Replace("  ", " ");
			return stringBuilder.ToString();
		}
	}

	public string CelyText => mstrCelyText;

	public int Pocet => mintPocet;

	public int SkutecnyPocet
	{
		get
		{
			return mintSkutecnyPocet;
		}
		set
		{
			mintSkutecnyPocet = value;
		}
	}

	public eZpusob Zpusob => mintZpusob;

	public bool ZobrazitUkazku => mbolUkazka;

	public string Znaky => mstrZnaky;

	public bool Enter => mbolEnter;

	public bool Backspace => mbolBackspace;

	public float Cas => msngCas;

	public int Penalizace => mintPenalizace;

	public int Rychlost => mintRychlost;

	public eVyhodnoceniTyp Vyhodnoceni => mintVyhodnoceni;

	public bool Klasifikace => Vyhodnoceni == eVyhodnoceniTyp.klasifikace;

	public bool Vetveni => Vyhodnoceni == eVyhodnoceniTyp.vetveni;

	public int KlasifikaceOd => mintKlasifikaceOd;

	public int KlasifikaceDo => mintKlasifikaceDo;

	public eKriterium VyhodnoceniKriterium => mintVyhodnoceniKriterium;

	public char VychoziVetev => mchrVychoziVetev;

	public bool Odmena => mbolOdmena;

	public eKriterium OdmenaKriterium => mintOdmenaKriterium;

	public float OdmenaHodnota => msngOdmenaHodnota;

	public eOdmenaTyp OdmenaTyp => mintOdmenaTyp;

	public int PocetPodminek => mlstPodminky.Count;

	public Psani(OznaceniCviceni oznCv, ref XmlNode root)
		: base(oznCv, ref root)
	{
		if (root == null)
		{
			return;
		}
		XmlNode xmlNode = root.SelectSingleNode("psani");
		XmlNode xmlNode2 = xmlNode.SelectSingleNode("text");
		string text = xmlNode2.InnerText;
		CultureInfo provider = new CultureInfo("en-US");
		if (xmlNode2.Attributes["kodovano"].Value != "0")
		{
			_Lekce.KodovatText(ref text);
		}
		mstrText = XmlConvert.DecodeName(text);
		xmlNode2 = xmlNode.SelectSingleNode("vlastnosti");
		mintPocet = int.Parse(xmlNode2.Attributes["pocet"].Value);
		mintSkutecnyPocet = mintPocet;
		mintZpusob = (eZpusob)int.Parse(xmlNode2.Attributes["zpusob"].Value);
		mbolEnter = xmlNode2.Attributes["enter"].Value != "0";
		mbolBackspace = xmlNode2.Attributes["backspace"].Value != "0";
		msngCas = Convert.ToSingle(xmlNode2.Attributes["cas"].Value, provider);
		mintPenalizace = int.Parse(xmlNode2.Attributes["penalizace"].Value);
		mintRychlost = int.Parse(xmlNode2.Attributes["rychlost"].Value);
		mstrZnaky = xmlNode2.Attributes["znaky"].Value;
		mbolUkazka = xmlNode2.Attributes["ukazka"].Value != "0";
		mlstPodminky = new List<Podminka>();
		xmlNode2 = xmlNode.SelectSingleNode("vyhodnoceni");
		if (xmlNode2 == null)
		{
			mintVyhodnoceni = eVyhodnoceniTyp.zadne;
		}
		else
		{
			mintVyhodnoceni = (eVyhodnoceniTyp)int.Parse(xmlNode2.Attributes["typvyhodnoceni"].Value);
			mintVyhodnoceniKriterium = (eKriterium)int.Parse(xmlNode2.Attributes["kriterium"].Value);
			mchrVychoziVetev = char.Parse(xmlNode2.Attributes["vychozivetev"].Value);
			if (mintVyhodnoceni == eVyhodnoceniTyp.klasifikace)
			{
				mintKlasifikaceOd = int.Parse(xmlNode2.Attributes["klasifikace_od"].Value);
				mintKlasifikaceDo = int.Parse(xmlNode2.Attributes["klasifikace_do"].Value);
			}
			XmlNodeList xmlNodeList = xmlNode2.SelectNodes("podminka");
			foreach (XmlNode item2 in xmlNodeList)
			{
				float hodnota = Convert.ToSingle(item2.Attributes["hodnota"].Value, provider);
				char vetev = '\0';
				if (Vyhodnoceni == eVyhodnoceniTyp.vetveni)
				{
					vetev = char.Parse(item2.Attributes["vetev"].Value);
				}
				Podminka item = new Podminka(hodnota, vetev);
				mlstPodminky.Add(item);
			}
		}
		xmlNode2 = xmlNode.SelectSingleNode("odmena");
		mbolOdmena = xmlNode2 != null;
		if (mbolOdmena)
		{
			mintOdmenaKriterium = (eKriterium)int.Parse(xmlNode2.Attributes["kriterium"].Value);
			msngOdmenaHodnota = Convert.ToSingle(xmlNode2.Attributes["hodnota"].Value, provider);
			mintOdmenaTyp = (eOdmenaTyp)int.Parse(xmlNode2.Attributes["typodmeny"].Value);
		}
		NactiCelyText();
	}

	private void NactiCelyText()
	{
		string text = (Zpusob switch
		{
			eZpusob.bezMezer => BezMezer(), 
			eZpusob.zpravaDoleva => ZpravaDoleva(), 
			eZpusob.slovaObsahujici => SlovaObsahujici(), 
			eZpusob.slovaZacinajici => SlovaZacinajici(), 
			_ => Text, 
		}).TrimEnd(' ');
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 1; i <= Pocet; i++)
		{
			if (Enter)
			{
				stringBuilder.Append(text + '\n');
				continue;
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append(text);
		}
		stringBuilder.Replace("  ", " ");
		mstrCelyText = stringBuilder.ToString();
	}

	private string BezMezer()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(Text);
		stringBuilder.Replace(" ", null);
		return stringBuilder.ToString();
	}

	private string ZpravaDoleva()
	{
		StringBuilder stringBuilder = new StringBuilder();
		char[] array = Text.ToCharArray();
		char[] array2 = array;
		foreach (char value in array2)
		{
			stringBuilder.Insert(0, value);
		}
		return stringBuilder.ToString();
	}

	private string SlovaObsahujici()
	{
		Veta veta = new Veta(Text);
		char[] array = Znaky.ToCharArray();
		StringBuilder stringBuilder = new StringBuilder();
		foreach (Slovo item in (IEnumerable)veta)
		{
			bool flag = false;
			char[] array2 = array;
			foreach (char value in array2)
			{
				if (item.Text.ToUpper().IndexOf(value) > -1)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				stringBuilder.Append(item.Text + ' ');
			}
		}
		return stringBuilder.ToString();
	}

	private string SlovaZacinajici()
	{
		Veta veta = new Veta(Text);
		StringBuilder stringBuilder = new StringBuilder();
		char value = Znaky.ToCharArray()[0];
		foreach (Slovo item in (IEnumerable)veta)
		{
			if (item.Text.ToUpper().IndexOf(value) == 0)
			{
				stringBuilder.Append(item.Text + ' ');
			}
		}
		return stringBuilder.ToString();
	}

	public string ZpusobToString()
	{
		return Zpusob switch
		{
			eZpusob.bezMezer => HYL.MountBlue.Resources.Texty.Psani_BezMezer, 
			eZpusob.zpravaDoleva => HYL.MountBlue.Resources.Texty.Psani_ZpravaDoleva, 
			eZpusob.slovaObsahujici => string.Format(HYL.MountBlue.Resources.Texty.Psani_SlovaObsahujici, ZnakyToString()), 
			eZpusob.slovaZacinajici => string.Format(HYL.MountBlue.Resources.Texty.Psani_SlovaZacinajici, ZnakyToString()), 
			_ => HYL.MountBlue.Resources.Texty.Psani_Normalne, 
		};
	}

	public string ZpusobToInfoUkazka()
	{
		string text = Zpusob switch
		{
			eZpusob.bezMezer => HYL.MountBlue.Resources.Texty.Psani_BezMezer_Info, 
			eZpusob.zpravaDoleva => HYL.MountBlue.Resources.Texty.Psani_ZpravaDoleva_Info, 
			eZpusob.slovaObsahujici => string.Format(HYL.MountBlue.Resources.Texty.Psani_SlovaObsahujici_Info, ZnakyToString()), 
			eZpusob.slovaZacinajici => string.Format(HYL.MountBlue.Resources.Texty.Psani_SlovaZacinajici_Info, ZnakyToString()), 
			eZpusob.slepaObrazovka => HYL.MountBlue.Resources.Texty.Psani_SlepaObrazovka_Info, 
			_ => string.Empty, 
		};
		if (SkutecnyPocet > 1)
		{
			if (text.Length > 0)
			{
				text += "\n\n";
			}
			text += string.Format(HYL.MountBlue.Resources.Texty.Psani_opisNkrat, SkutecnyPocet);
		}
		return text;
	}

	public string ZpusobToTitleUkazka()
	{
		return Zpusob switch
		{
			eZpusob.bezMezer => HYL.MountBlue.Resources.Texty.Psani_BezMezer_Title, 
			eZpusob.zpravaDoleva => HYL.MountBlue.Resources.Texty.Psani_ZpravaDoleva_Title, 
			eZpusob.slovaObsahujici => HYL.MountBlue.Resources.Texty.Psani_SlovaObsahujici_Title, 
			eZpusob.slovaZacinajici => HYL.MountBlue.Resources.Texty.Psani_SlovaZacinajici_Title, 
			eZpusob.slepaObrazovka => HYL.MountBlue.Resources.Texty.Psani_SlepaObrazovka_Title, 
			_ => string.Empty, 
		};
	}

	public string ZnakyToString()
	{
		if (Znaky == null || Znaky.Length == 0)
		{
			return string.Empty;
		}
		StringBuilder stringBuilder = new StringBuilder();
		char[] array = Znaky.ToLower().ToCharArray();
		char[] array2 = Znaky.ToUpper().ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (TextZadani.Contains(array[i].ToString()))
			{
				stringBuilder.Append(array[i]);
			}
			if (TextZadani.Contains(array2[i].ToString()))
			{
				stringBuilder.Append(array2[i]);
			}
		}
		if (Znaky.ToLower().Contains("c") && TextZadani.ToLower().Contains("ch"))
		{
			stringBuilder.Append("#");
		}
		char[] array3 = stringBuilder.ToString().ToCharArray();
		if (array3.Length == 1)
		{
			return array3[0].ToString();
		}
		StringBuilder stringBuilder2 = new StringBuilder();
		for (int j = 0; j <= array3.Length - 2; j++)
		{
			stringBuilder2.Append(array3[j]);
			if (j < array3.Length - 2)
			{
				stringBuilder2.Append(HYL.MountBlue.Resources.Texty.Psani_neboCarka);
			}
		}
		stringBuilder2.Append(HYL.MountBlue.Resources.Texty.Psani_nebo);
		stringBuilder2.Append(array3[array3.Length - 1]);
		stringBuilder2.Replace("#", "ch");
		return stringBuilder2.ToString();
	}

	public static string KriteriumToString(eKriterium kriterium)
	{
		return kriterium switch
		{
			eKriterium.pocetChybAbs => HYL.MountBlue.Resources.Texty.Psani_kriterium_pocetChybAbs, 
			eKriterium.pocetChybRel => HYL.MountBlue.Resources.Texty.Psani_kriterium_pocetChybRel, 
			eKriterium.cistychUhozuZaMin => HYL.MountBlue.Resources.Texty.Psani_kriterium_cistychUhozuZaMin, 
			_ => string.Empty, 
		};
	}

	public Podminka Podminka(int index)
	{
		return mlstPodminky[index];
	}

	public static string BoolToString(bool val)
	{
		if (val)
		{
			return HYL.MountBlue.Resources.Texty.Psani_Ano;
		}
		return HYL.MountBlue.Resources.Texty.Psani_Ne;
	}

	public static string CasToString(float val)
	{
		if (val == 0f)
		{
			return HYL.MountBlue.Resources.Texty.Psani_minuty_0;
		}
		return string.Format(arg1: (val == 1f) ? HYL.MountBlue.Resources.Texty.Psani_minuty_1 : ((val < 5f) ? HYL.MountBlue.Resources.Texty.Psani_minuty_234 : ((val % 1f != 0.5f) ? HYL.MountBlue.Resources.Texty.Psani_minuty_5 : HYL.MountBlue.Resources.Texty.Psani_minuty_pul)), format: "{0:0.0} {1}", arg0: val);
	}

	public IEnumerator<Podminka> GetEnumerator()
	{
		return mlstPodminky.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return mlstPodminky.GetEnumerator();
	}
}
