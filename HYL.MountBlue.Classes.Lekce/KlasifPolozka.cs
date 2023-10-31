using System;
using HYL.MountBlue.Classes.Cviceni;

namespace HYL.MountBlue.Classes.Lekce;

internal class KlasifPolozka
{
	internal enum eStav
	{
		neaktivni,
		nesplneno,
		nesplnenoUrgentni,
		splneno,
		moznostVyuzitOdmeny,
		nuceneOpakovani
	}

	private Psani cviceni;

	private eStav stav;

	private byte byZnamka;

	private DateTime dDatum;

	public Psani CviceniPsani => cviceni;

	public eStav Stav
	{
		get
		{
			return stav;
		}
		set
		{
			stav = value;
		}
	}

	public DateTime Datum
	{
		get
		{
			return dDatum;
		}
		set
		{
			dDatum = value;
		}
	}

	public byte Znamka
	{
		get
		{
			return byZnamka;
		}
		set
		{
			byZnamka = value;
		}
	}

	public KlasifPolozka(Psani cvic)
	{
		cviceni = cvic;
	}
}
