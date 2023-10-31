using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Uzivatele.Historie;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Lekce;

internal class Klasifikace : IEnumerable<Psani>, IEnumerable
{
	private Dictionary<uint, Psani> klasifikace;

	public Psani this[uint index] => klasifikace[index];

	public int Pocet => klasifikace.Count;

	public Klasifikace(XmlDocument lekceKlasifikace)
	{
		klasifikace = new Dictionary<uint, Psani>();
		try
		{
			XmlElement documentElement = lekceKlasifikace.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode($"vetev[@nazev=\"{'*'}\"]");
			if (xmlNode == null)
			{
				throw new Exception("V klasifikaci neexistuje výchozí větev!");
			}
			int num = 0;
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("cviceni");
			foreach (XmlNode item in xmlNodeList)
			{
				OznaceniCviceni oznCvic = new OznaceniCviceni(0, '*', ++num);
				_Cviceni cviceni = _Cviceni.NactiCviceni(oznCvic, item);
				if (cviceni != null && cviceni is Psani)
				{
					klasifikace.Add(cviceni.ID, (Psani)cviceni);
				}
			}
			if (klasifikace.Count == 0)
			{
				throw new Exception("Nebylo načteno žádné klasifikační cvičení.");
			}
		}
		catch (Exception ex)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Lekce_msgboxChybaKlasifikace, ex.Message), HYL.MountBlue.Resources.Texty.Lekce_msgboxChybaKlasifikace_Title, eMsgBoxTlacitka.OK);
		}
	}

	public KlasifPolozka[] SeznamKlasifikaci(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student stu)
	{
		List<KlasifPolozka> list = new List<KlasifPolozka>();
		int lekce = stu.Cviceni.Lekce;
		bool klasifikaceOpakovani = stu.KlasifikaceOpakovani;
		uint klasifikaceOpakovaniID = stu.KlasifikaceOpakovaniID;
		using (IEnumerator<Psani> enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Psani current = enumerator.Current;
				KlasifPolozka klasifPolozka = new KlasifPolozka(current);
				if (lekce < current.KlasifikaceOd)
				{
					klasifPolozka.Stav = KlasifPolozka.eStav.neaktivni;
				}
				else
				{
					if (stu.Klasifikace.JeKlasifikaceSplnena(current.ID))
					{
						if (klasifikaceOpakovani && klasifikaceOpakovaniID == current.ID)
						{
							klasifPolozka.Stav = KlasifPolozka.eStav.nuceneOpakovani;
						}
						else if (lekce <= current.KlasifikaceDo && stu.Klasifikace.MuzeOpakovatKlasifikaci(current.ID))
						{
							klasifPolozka.Stav = KlasifPolozka.eStav.moznostVyuzitOdmeny;
						}
						else
						{
							klasifPolozka.Stav = KlasifPolozka.eStav.splneno;
						}
					}

					ZaznKlasifCvic zaznKlasifCvic = stu.Klasifikace.PlatneKlasifikacniCviceni(current.ID);
					if (zaznKlasifCvic != null)
					{
						klasifPolozka.Datum = zaznKlasifCvic.DatumAcas;
						klasifPolozka.Znamka = zaznKlasifCvic.Znamka;
					}
				}
				list.Add(klasifPolozka);
			}
		}
		return list.ToArray();
	}

	public bool KlasifikaceSplneny(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student stu, bool bPouzeUrgentni)
	{
		KlasifPolozka[] array = SeznamKlasifikaci(stu);
		KlasifPolozka[] array2 = array;
		foreach (KlasifPolozka klasifPolozka in array2)
		{
			if ((klasifPolozka.Stav == KlasifPolozka.eStav.nesplneno && !bPouzeUrgentni) || klasifPolozka.Stav == KlasifPolozka.eStav.nesplnenoUrgentni || klasifPolozka.Stav == KlasifPolozka.eStav.nuceneOpakovani)
			{
				return false;
			}
		}
		return true;
	}

	public IEnumerator<Psani> GetEnumerator()
	{
		return klasifikace.Values.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return klasifikace.Values.GetEnumerator();
	}
}
