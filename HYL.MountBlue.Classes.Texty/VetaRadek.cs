using System.Drawing;

namespace HYL.MountBlue.Classes.Texty;

internal class VetaRadek
{
	private string mstrTextRadku;

	private int mintCisloRadku;

	private int mintZacatekRadku;

	private int mintSirkaRadku;

	private int mintLeft;

	private int mintTop;

	private int mintLeftEnter;

	private VetaKresleni mobjVetaKresleni;

	internal string TextRadku => mstrTextRadku;

	internal string TextRadkuIsEnterem
	{
		get
		{
			if (mintLeftEnter >= 0)
			{
				return TextRadku + '\n';
			}
			return TextRadku;
		}
	}

	internal int CisloRadku => mintCisloRadku;

	internal int ZacatekRadku => mintZacatekRadku;

	internal int SirkaRadku => mintSirkaRadku;

	internal int Left => mintLeft;

	internal int LeftEnter => mintLeftEnter;

	internal int Top => mintTop;

	internal VetaRadek(string sTextRadku, int iCisloRadku, int iZacatekRadku, int iSirkaRadku, int iLeft, int iTop, int iLeftEnter, VetaKresleni VetaKresleni)
	{
		mstrTextRadku = sTextRadku;
		mintCisloRadku = iCisloRadku;
		mintZacatekRadku = iZacatekRadku;
		mintSirkaRadku = iSirkaRadku;
		mintLeft = iLeft;
		mintTop = iTop;
		mintLeftEnter = iLeftEnter;
		mobjVetaKresleni = VetaKresleni;
		if (sTextRadku.Length == 0)
		{
			mintLeftEnter += (int)(VetaKresleni.VyskaPisma * 0.2f);
		}
	}

	internal bool JeTextObsazenVradku(int zacatek)
	{
		if (ZacatekRadku <= zacatek)
		{
			return ZacatekRadku + TextRadkuIsEnterem.Length > zacatek;
		}
		return false;
	}

	internal bool VykreslitRadek(Graphics G, VetaKresleni VetaKresleni, float sngTop, int intHeight)
	{
		if (sngTop + (float)Top + (float)intHeight > 0f && sngTop + (float)Top < (float)intHeight)
		{
			if (mintLeftEnter >= 0)
			{
				VykresliSipku(G, VetaKresleni, mintLeftEnter, sngTop + (float)Top, VetaKresleni.VyskaRadku);
			}
			G.DrawString(TextRadku, VetaKresleni.Pismo, VetaKresleni.BarvaPisma, Left, sngTop + (float)Top);
			return true;
		}
		return false;
	}

	internal static void VykresliSipku(Graphics G, VetaKresleni VetaKresleni, float X, float Y, float Size)
	{
		float num = Size * 0.06f;
		G.FillPolygon(points: new PointF[9]
		{
			new PointF(X, Y + Size * 0.5f),
			new PointF(X + Size * 0.25f, Y + Size * 0.25f),
			new PointF(X + Size * 0.25f, Y + Size * 0.5f - num),
			new PointF(X + Size * 0.5f - num, Y + Size * 0.5f - num),
			new PointF(X + Size * 0.5f - num, Y + num),
			new PointF(X + Size * 0.5f + num, Y + num),
			new PointF(X + Size * 0.5f + num, Y + Size * 0.5f + num),
			new PointF(X + Size * 0.25f, Y + Size * 0.5f + num),
			new PointF(X + Size * 0.25f, Y + Size * 0.75f)
		}, brush: VetaKresleni.BarvaPisma);
	}
}
