using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Klavesnice;

internal class Klavesnice : IEnumerable
{
	private List<KlavesaKlavesnice> mlstKlavesy;

	private List<KlavesniceAnimace> mlstAnimace;

	private KlavesaKlavesnice mobjBlikKlavesa;

	private bool mbolShift;

	private KlavesaKlavesnice.eModifikatory mbytModifikator;

	private Bitmap mbmpPodkladovyObrazek;

	private bool mbolKlavesaJeZnama;

	private string mstrNadpis;

	private string mstrPopis;

	internal int Pocet => mlstKlavesy.Count;

	internal bool KlavesaJeZnama => mbolKlavesaJeZnama;

	internal string Nadpis => mstrNadpis;

	internal string Popis => mstrPopis;

	internal char Znak
	{
		get
		{
			if (mobjBlikKlavesa == null || mobjBlikKlavesa.JeBezobsahovaKlavesa())
			{
				return '\0';
			}
			KlavesaObsah klavesaObsah = Modifikator switch
			{
				KlavesaKlavesnice.eModifikatory.delka => BlikajiciKlavesa.ObsahDelka(), 
				KlavesaKlavesnice.eModifikatory.hacek => BlikajiciKlavesa.ObsahHacek(), 
				KlavesaKlavesnice.eModifikatory.krouzek => BlikajiciKlavesa.ObsahKrouzek(), 
				_ => BlikajiciKlavesa.ObsahZakladni(), 
			};
			if (Shift)
			{
				return klavesaObsah.ZnakShift;
			}
			return klavesaObsah.Znak;
		}
	}

	internal KlavesaKlavesnice BlikajiciKlavesa => mobjBlikKlavesa;

	internal bool Shift => mbolShift;

	internal KlavesaKlavesnice.eModifikatory Modifikator => mbytModifikator;

	internal int DelkaAnimace => mlstAnimace.Count;

	internal Klavesnice()
	{
		mlstKlavesy = new List<KlavesaKlavesnice>();
		mlstAnimace = new List<KlavesniceAnimace>();
		mbolKlavesaJeZnama = false;
		mstrNadpis = string.Empty;
		mstrPopis = string.Empty;
	}

	internal void Pridat(KlavesaKlavesnice.eKlavesy byKlavesa)
	{
		Pridat(new KlavesaKlavesnice(byKlavesa));
	}

	internal void Pridat(KlavesaKlavesnice objKlavesa)
	{
		mlstKlavesy.Add(objKlavesa);
	}

	internal void PridatRozsah(string seznamKlaves)
	{
		if (seznamKlaves == null)
		{
			return;
		}
		string[] array = seznamKlaves.Split(';');
		string[] array2 = array;
		foreach (string s in array2)
		{
			if (int.TryParse(s, out var result))
			{
				Pridat((KlavesaKlavesnice.eKlavesy)result);
			}
		}
	}

	internal void Vymazat()
	{
		mlstKlavesy.Clear();
	}

	internal KlavesaKlavesnice Klavesa(int index)
	{
		return mlstKlavesy[index];
	}

	internal KlavesaKlavesnice Klavesa(KlavesaKlavesnice.eKlavesy byKlavesa)
	{
		foreach (KlavesaKlavesnice item in mlstKlavesy)
		{
			if (item.Klavesa == byKlavesa)
			{
				return item;
			}
		}
		return null;
	}

	internal void NastavitBlikajiciKlavesu(KlavesaKlavesnice.eKlavesy byKlavesa, bool bShift, KlavesaKlavesnice.eModifikatory byModifikator)
	{
		mbolKlavesaJeZnama = Klavesa(byKlavesa) != null;
		mobjBlikKlavesa = new KlavesaKlavesnice(byKlavesa);
		OveritSpravnostZadani(mobjBlikKlavesa, ref bShift, ref byModifikator);
		KlavesaKlavesnice mobjBlikShift = null;
		KlavesaKlavesnice mobjBlikModifikator = null;
		KlavesaKlavesnice mobjBlikModifikatorShift = null;
		ZjistitDalsiKlavesy(mobjBlikKlavesa, bShift, byModifikator, ref mobjBlikShift, ref mobjBlikModifikator, ref mobjBlikModifikatorShift);
		mbolShift = bShift;
		mbytModifikator = byModifikator;
		mlstAnimace.Clear();
		mlstAnimace.Add(new KlavesniceAnimace(null, null, bKrouzky: false));
		if ((int)byModifikator > 0)
		{
			if (mobjBlikModifikatorShift != null)
			{
				mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikModifikatorShift, bKrouzky: true));
				mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikModifikatorShift, bKrouzky: false));
			}
			mlstAnimace.Add(new KlavesniceAnimace(mobjBlikModifikator, mobjBlikModifikatorShift, bKrouzky: true));
			mlstAnimace.Add(new KlavesniceAnimace(mobjBlikModifikator, mobjBlikModifikatorShift, bKrouzky: false));
			if (mobjBlikModifikatorShift != null)
			{
				mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikModifikatorShift, bKrouzky: false));
			}
			if (mobjBlikShift != null)
			{
				if (mobjBlikModifikatorShift != null)
				{
					if (mobjBlikShift.Klavesa != mobjBlikModifikatorShift.Klavesa)
					{
						mlstAnimace.Add(new KlavesniceAnimace(null, null, bKrouzky: false));
						mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikShift, bKrouzky: true));
						mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikShift, bKrouzky: false));
					}
				}
				else
				{
					mlstAnimace.Add(new KlavesniceAnimace(null, null, bKrouzky: false));
					mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikShift, bKrouzky: true));
					mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikShift, bKrouzky: false));
				}
			}
			else
			{
				mlstAnimace.Add(new KlavesniceAnimace(null, null, bKrouzky: false));
			}
		}
		else if (mobjBlikShift != null)
		{
			mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikShift, bKrouzky: true));
			mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikShift, bKrouzky: false));
		}
		mlstAnimace.Add(new KlavesniceAnimace(mobjBlikKlavesa, mobjBlikShift, bKrouzky: true));
		mlstAnimace.Add(new KlavesniceAnimace(mobjBlikKlavesa, mobjBlikShift, bKrouzky: false));
		if (mobjBlikShift != null)
		{
			mlstAnimace.Add(new KlavesniceAnimace(null, mobjBlikShift, bKrouzky: false));
		}
		VygenerovatNadpisApopis(mobjBlikKlavesa, bShift, byModifikator, mobjBlikModifikator, mobjBlikModifikatorShift, mbolKlavesaJeZnama, bVyzkousejteSiTo: true, out mstrNadpis, out mstrPopis);
	}

	internal static void ZjistitNadpisApopis(KlavesaKlavesnice.eKlavesy byKlavesa, bool bShift, KlavesaKlavesnice.eModifikatory byModifikator, bool bJeZnama, out string sNadpis, out string sPopis)
	{
		KlavesaKlavesnice klavesa = new KlavesaKlavesnice(byKlavesa);
		OveritSpravnostZadani(klavesa, ref bShift, ref byModifikator);
		KlavesaKlavesnice mobjBlikShift = null;
		KlavesaKlavesnice mobjBlikModifikator = null;
		KlavesaKlavesnice mobjBlikModifikatorShift = null;
		ZjistitDalsiKlavesy(klavesa, bShift, byModifikator, ref mobjBlikShift, ref mobjBlikModifikator, ref mobjBlikModifikatorShift);
		VygenerovatNadpisApopis(klavesa, bShift, byModifikator, mobjBlikModifikator, mobjBlikModifikatorShift, bJeZnama, bVyzkousejteSiTo: false, out sNadpis, out sPopis);
	}

	private static void ZjistitDalsiKlavesy(KlavesaKlavesnice klavesa, bool bShift, KlavesaKlavesnice.eModifikatory byModifikator, ref KlavesaKlavesnice mobjBlikShift, ref KlavesaKlavesnice mobjBlikModifikator, ref KlavesaKlavesnice mobjBlikModifikatorShift)
	{
		if (bShift)
		{
			switch (klavesa.Shift)
			{
			case KlavesaKlavesnice.eShift.levy:
				mobjBlikShift = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.levyShift);
				if (mobjBlikShift == null)
				{
					mobjBlikShift = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.levyShift);
				}
				break;
			case KlavesaKlavesnice.eShift.pravy:
				mobjBlikShift = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.pravyShift);
				if (mobjBlikShift == null)
				{
					mobjBlikShift = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.pravyShift);
				}
				break;
			}
		}
		if ((int)byModifikator <= 0)
		{
			return;
		}
		switch (byModifikator)
		{
		case KlavesaKlavesnice.eModifikatory.delka:
			mobjBlikModifikator = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.delka_hacek);
			if (mobjBlikModifikator == null)
			{
				mobjBlikModifikator = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.delka_hacek);
			}
			break;
		case KlavesaKlavesnice.eModifikatory.hacek:
			mobjBlikModifikator = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.delka_hacek);
			if (mobjBlikModifikator == null)
			{
				mobjBlikModifikator = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.delka_hacek);
			}
			break;
		case KlavesaKlavesnice.eModifikatory.krouzek:
			mobjBlikModifikator = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.strednik_krouzek);
			if (mobjBlikModifikator == null)
			{
				mobjBlikModifikator = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.strednik_krouzek);
			}
			break;
		}
		if (byModifikator != KlavesaKlavesnice.eModifikatory.hacek && byModifikator != KlavesaKlavesnice.eModifikatory.krouzek)
		{
			return;
		}
		switch (mobjBlikModifikator.Shift)
		{
		case KlavesaKlavesnice.eShift.levy:
			mobjBlikModifikatorShift = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.levyShift);
			if (mobjBlikModifikatorShift == null)
			{
				mobjBlikModifikatorShift = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.levyShift);
			}
			break;
		case KlavesaKlavesnice.eShift.pravy:
			mobjBlikModifikatorShift = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.pravyShift);
			if (mobjBlikModifikatorShift == null)
			{
				mobjBlikModifikatorShift = new KlavesaKlavesnice(KlavesaKlavesnice.eKlavesy.pravyShift);
			}
			break;
		}
	}

	private static void VygenerovatNadpisApopis(KlavesaKlavesnice klavesa, bool bShift, KlavesaKlavesnice.eModifikatory byModifikator, KlavesaKlavesnice mobjBlikModifikator, KlavesaKlavesnice mobjBlikModifikatorShift, bool bJeZnama, bool bVyzkousejteSiTo, out string sNadpis, out string sPopis)
	{
		if (bShift)
		{
			sNadpis = klavesa.NadpisShift(byModifikator);
		}
		else
		{
			sNadpis = klavesa.Nadpis(byModifikator);
		}
		if (bJeZnama && !klavesa.JeBezobsahovaKlavesa())
		{
			string empty = string.Empty;
			string text = string.Empty;
			string text2 = string.Empty;
			switch (byModifikator)
			{
			case KlavesaKlavesnice.eModifikatory.delka:
			case KlavesaKlavesnice.eModifikatory.hacek:
			case KlavesaKlavesnice.eModifikatory.krouzek:
				if (bShift || mobjBlikModifikatorShift != null)
				{
					text = HYL.MountBlue.Resources.Texty.Klavesnice_popis_shift;
				}
				switch (byModifikator)
				{
				case KlavesaKlavesnice.eModifikatory.delka:
					text2 = mobjBlikModifikator.Nazev(KlavesaKlavesnice.eModifikatory.zadne);
					break;
				case KlavesaKlavesnice.eModifikatory.hacek:
				case KlavesaKlavesnice.eModifikatory.krouzek:
					text2 = mobjBlikModifikator.NazevShift(KlavesaKlavesnice.eModifikatory.zadne);
					break;
				}
				empty = ((!bShift) ? klavesa.ObsahZakladni().Nazev : klavesa.ObsahZakladni().NazevShift);
				sPopis = string.Format(HYL.MountBlue.Resources.Texty.Klavesnice_popis_znama_klavesa_s_modifikatorem, sNadpis, empty, Prsty.ToString7pad(klavesa.Prst), text2, Prsty.ToString7pad(mobjBlikModifikator.Prst), text);
				break;
			default:
				if (bShift)
				{
					empty = klavesa.Nazev(byModifikator);
					text = HYL.MountBlue.Resources.Texty.Klavesnice_popis_shift_a;
				}
				else
				{
					empty = klavesa.NazevShift(byModifikator);
					text = string.Empty;
				}
				sPopis = string.Format(HYL.MountBlue.Resources.Texty.Klavesnice_popis_znama_klavesa_se_shiftem, sNadpis, empty, text, Prsty.ToString(klavesa.Prst));
				break;
			}
		}
		else
		{
			string text3 = string.Empty;
			string text4 = string.Empty;
			if (bShift || mobjBlikModifikatorShift != null)
			{
				text3 = "\n\n" + HYL.MountBlue.Resources.Texty.Klavesnice_popis_shift;
			}
			if (bVyzkousejteSiTo)
			{
				text4 = "\n\n" + HYL.MountBlue.Resources.Texty.Klavesnice_popis_vyzkousejte;
			}
			sPopis = string.Format(HYL.MountBlue.Resources.Texty.Klavesnice_popis_neznama_klavesa, sNadpis, Prsty.ToString7pad(klavesa.Prst), text4, text3);
		}
	}

	private static void OveritSpravnostZadani(KlavesaKlavesnice klavesa, ref bool bShift, ref KlavesaKlavesnice.eModifikatory byModifikator)
	{
		if (bShift && klavesa.Shift == KlavesaKlavesnice.eShift.zadny)
		{
			bShift = false;
		}
		switch (byModifikator)
		{
		case KlavesaKlavesnice.eModifikatory.delka:
			if (!klavesa.MaDelku)
			{
				byModifikator = KlavesaKlavesnice.eModifikatory.zadne;
			}
			break;
		case KlavesaKlavesnice.eModifikatory.hacek:
			if (!klavesa.MaHacek)
			{
				byModifikator = KlavesaKlavesnice.eModifikatory.zadne;
			}
			break;
		case KlavesaKlavesnice.eModifikatory.krouzek:
			if (!klavesa.MaKrouzek)
			{
				byModifikator = KlavesaKlavesnice.eModifikatory.zadne;
			}
			break;
		}
	}

	internal void VykreslitKlavesnici(Graphics G, int iVelkyKrok, int iMalyKrok)
	{
		if (mbmpPodkladovyObrazek != null)
		{
			G.DrawImage(mbmpPodkladovyObrazek, new Point(0, 0));
		}
		if (mlstAnimace.Count > 0)
		{
			mlstAnimace[iVelkyKrok % mlstAnimace.Count].VykreslitAnimacniKrok(G, iMalyKrok);
		}
	}

	internal bool MaMalouAnimaci(int iVelkyKrok)
	{
		KlavesniceAnimace klavesniceAnimace = mlstAnimace[iVelkyKrok % mlstAnimace.Count];
		if (!klavesniceAnimace.KrouzkyKlavesa)
		{
			return klavesniceAnimace.KrouzkyShift;
		}
		return true;
	}

	internal void VytvoritPodkladovyObrazek()
	{
		Image pngKlavesniceRuce = HYL.MountBlue.Resources.Grafika.pngKlavesniceRuce;
		mbmpPodkladovyObrazek = new Bitmap(pngKlavesniceRuce.Size.Width, pngKlavesniceRuce.Size.Height);
		Graphics graphics = Graphics.FromImage(mbmpPodkladovyObrazek);
		graphics.DrawImage(pngKlavesniceRuce, 0, 0, pngKlavesniceRuce.Size.Width, pngKlavesniceRuce.Size.Height);
		foreach (KlavesaKlavesnice item in (IEnumerable)this)
		{
			item.VykreslitKlavesu(graphics, HYL.MountBlue.Resources.Grafika.pngKlavesniceModra);
		}
	}

	internal GraphicsPath VytvoritCestuZnamychKlaves()
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		foreach (KlavesaKlavesnice item in (IEnumerable)this)
		{
			Rectangle oblastVobrazku = item.OblastVobrazku;
			if (oblastVobrazku.IsEmpty)
			{
				KlavesaKlavesnice.eBezobsahoveKlavesy klavesa = (KlavesaKlavesnice.eBezobsahoveKlavesy)item.Klavesa;
				if (klavesa == KlavesaKlavesnice.eBezobsahoveKlavesy.Enter)
				{
					graphicsPath.AddPolygon(KlavesaKlavesnice.BodyKlavesyEnter);
				}
			}
			else
			{
				graphicsPath.AddRectangle(oblastVobrazku);
			}
		}
		return graphicsPath;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return mlstKlavesy.GetEnumerator();
	}
}
