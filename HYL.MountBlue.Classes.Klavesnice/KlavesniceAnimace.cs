using System.Drawing;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Klavesnice;

internal class KlavesniceAnimace
{
	private KlavesaKlavesnice mobjBlikKlavesa;

	private KlavesaKlavesnice mobjBlikShift;

	private bool mbolKrouzkyKlavesa;

	private bool mbolKrouzkyShift;

	private Rectangle[] mrctKrouzkyKlavesa;

	private Rectangle[] mrctKrouzkyShift;

	internal bool KrouzkyKlavesa => mbolKrouzkyKlavesa;

	internal bool KrouzkyShift => mbolKrouzkyShift;

	internal KlavesniceAnimace(KlavesaKlavesnice oBlikKlavesa, KlavesaKlavesnice oBlikShift, bool bKrouzky)
	{
		mobjBlikKlavesa = oBlikKlavesa;
		mobjBlikShift = oBlikShift;
		if (bKrouzky)
		{
			mbolKrouzkyKlavesa = oBlikKlavesa != null;
			mbolKrouzkyShift = !mbolKrouzkyKlavesa;
		}
		else
		{
			mbolKrouzkyKlavesa = false;
			mbolKrouzkyShift = false;
		}
		if (mobjBlikKlavesa != null)
		{
			Point pnt = Prsty.SouradnicePrstu(mobjBlikKlavesa.Prst);
			Point stredOblastiVobrazku = mobjBlikKlavesa.StredOblastiVobrazku;
			mrctKrouzkyKlavesa = SpocitatOblastiKrouzku(pnt, stredOblastiVobrazku);
		}
		if (mobjBlikShift != null)
		{
			Point pnt2 = Prsty.SouradnicePrstu(mobjBlikShift.Prst);
			Point stredOblastiVobrazku2 = mobjBlikShift.StredOblastiVobrazku;
			mrctKrouzkyShift = SpocitatOblastiKrouzku(pnt2, stredOblastiVobrazku2);
		}
	}

	internal bool VykreslujeSe(KlavesaKlavesnice.eKlavesy byKlavesa)
	{
		if (mobjBlikKlavesa != null && mobjBlikKlavesa.Klavesa == byKlavesa)
		{
			return true;
		}
		if (mobjBlikShift != null && mobjBlikShift.Klavesa == byKlavesa)
		{
			return true;
		}
		return false;
	}

	internal void VykreslitAnimacniKrok(Graphics G, int iMalyKrok)
	{
		if (mobjBlikKlavesa != null)
		{
			if (!mbolKrouzkyKlavesa || iMalyKrok >= 3)
			{
				mobjBlikKlavesa.VykreslitKlavesu(G, HYL.MountBlue.Resources.Grafika.pngKlavesniceOranz);
			}
			VykreslitKrouzky(G, mrctKrouzkyKlavesa, mbolKrouzkyKlavesa, iMalyKrok);
		}
		if (mobjBlikShift != null)
		{
			if (!mbolKrouzkyShift || iMalyKrok >= 3)
			{
				mobjBlikShift.VykreslitKlavesu(G, HYL.MountBlue.Resources.Grafika.pngKlavesniceOranz);
			}
			VykreslitKrouzky(G, mrctKrouzkyShift, mbolKrouzkyShift, iMalyKrok);
		}
	}

	private void VykreslitKrouzky(Graphics G, Rectangle[] rOblasti, bool bVykreslitKrouzky, int iCisloKrouzku)
	{
		Pen pen = new Pen(Barva.KrouzekNadPrstem, 4f);
		G.DrawEllipse(pen, rOblasti[0]);
		if (bVykreslitKrouzky && iCisloKrouzku >= 1 && iCisloKrouzku <= 3)
		{
			pen = new Pen(Barva.KrouzkyKlavesniceAnim, 3f);
			G.DrawEllipse(pen, rOblasti[iCisloKrouzku]);
		}
	}

	private static Rectangle[] SpocitatOblastiKrouzku(Point pnt1, Point pnt2)
	{
		Point pnt3 = SpocitatBodNaPrimce(pnt1, pnt2, 0.4f);
		Point pnt4 = SpocitatBodNaPrimce(pnt1, pnt2, 0.75f);
		return new Rectangle[4]
		{
			SpocitatOblastKrouzku(pnt1, 70),
			SpocitatOblastKrouzku(pnt3, 54),
			SpocitatOblastKrouzku(pnt4, 40),
			SpocitatOblastKrouzku(pnt2, 28)
		};
	}

	private static Rectangle SpocitatOblastKrouzku(Point pnt, int prumer)
	{
		return new Rectangle(pnt.X - prumer / 2, pnt.Y - prumer / 2, prumer, prumer);
	}

	private static Point SpocitatBodNaPrimce(Point pnt1, Point pnt2, float pomernaCast)
	{
		return new Point(pnt1.X + (int)((float)(pnt2.X - pnt1.X) * pomernaCast), pnt1.Y + (int)((float)(pnt2.Y - pnt1.Y) * pomernaCast));
	}
}
