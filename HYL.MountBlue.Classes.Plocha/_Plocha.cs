using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Forms;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal abstract class _Plocha
{
	protected const int cOkraj = 12;

	internal const string VychoziPismoJmeno = "Arial";

	internal const float VychoziPismoVelikost = 9f;

	internal static _Plocha AktualniPlocha;

	protected static Font VychoziPismo;

	internal static HlavniOkno HlavniOkno;

	protected ToolTip ttt;

	private static int PocetZobrazenychDlgOken;

	internal static bool ZakazatZobrazeniBubliny;

	internal Point ZacatekBeznePlochy => new Point((HlavniOkno.DisplayRectangle.Width - 1000) / 2, 0);

	static _Plocha()
	{
		PocetZobrazenychDlgOken = 0;
		ZakazatZobrazeniBubliny = false;
		VychoziPismo = new Font("Arial", 9f, FontStyle.Regular);
	}

	internal _Plocha()
	{
		PocetZobrazenychDlgOken = 0;
		ZakazatZobrazeniBubliny = false;
	}

	~_Plocha()
	{
	}

	internal static void NactiPlochu(_Plocha plocha)
	{
		HlavniOkno.Cursor = Cursors.WaitCursor;
		if (plocha == null)
		{
			throw new ArgumentException("Parametr 'plocha' nemůže být null!");
		}
		HlavniOkno.SuspendLayout();
		if (AktualniPlocha != null)
		{
			AktualniPlocha.KonecPlochy();
			AktualniPlocha.OdebratPrvky();
			AktualniPlocha.DeinicializovatPrvky();
			AktualniPlocha = null;
		}
		AktualniPlocha = plocha;
		AktualniPlocha.InicializovatPrvky(AktualniPlocha.ZacatekBeznePlochy);
		AktualniPlocha.NastavitGrafikuDoPozadi();
		AktualniPlocha.PridatPrvky();
		HlavniOkno.ResumeLayout(performLayout: false);
		HlavniOkno.Cursor = Cursors.Default;
		AktualniPlocha.ZacatekPlochy();
	}

	internal void NastavitGrafikuDoPozadi()
	{
		Size size = HlavniOkno.DisplayRectangle.Size;
		Bitmap bitmap = new Bitmap(size.Width, size.Height);
		Graphics g = Graphics.FromImage(bitmap);
		Vykreslit(g);
		HlavniOkno.BackgroundImage = bitmap;
	}

	public void ObnovitGrafiku()
	{
		NastavitGrafikuDoPozadi();
	}

	internal void ZrusitGrafikuVpozadi()
	{
		HlavniOkno.BackgroundImage = null;
		GC.Collect();
		GC.WaitForPendingFinalizers();
	}

	protected virtual void Vykreslit(Graphics G)
	{
		G.SmoothingMode = SmoothingMode.AntiAlias;
		VykreslitPlochu(G);
		Point zacatekBeznePlochy = ZacatekBeznePlochy;
		G.TranslateTransform(zacatekBeznePlochy.X, zacatekBeznePlochy.Y);
		VykreslitVyrezanyObdelnik(G, FixniVyska());
		VykreslitPruvodce(G);
	}

	public static void NastavitViditelnostBubliny(bool bViditelnostBubliny)
	{
		if (AktualniPlocha != null)
		{
			if (bViditelnostBubliny && !ZakazatZobrazeniBubliny && --PocetZobrazenychDlgOken == 0)
			{
				AktualniPlocha.ZpracovatPrvky(AktualniPlocha.ZobrazitBublinu);
			}
			else if (!bViditelnostBubliny && PocetZobrazenychDlgOken++ == 0)
			{
				AktualniPlocha.ZpracovatPrvky(AktualniPlocha.SkrytBublinu);
			}
		}
	}

	protected virtual void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
	}

	internal virtual void InicializovatPrvky(Point pntZacatek)
	{
		ttt = new ToolTip();
		ToolTipText.NastavitToolTipText(ttt);
	}

	internal virtual void DeinicializovatPrvky()
	{
		ttt.Dispose();
	}

	internal void PridatPrvky()
	{
		ZpracovatPrvky(HlavniOkno.Controls.Add);
	}

	internal void OdebratPrvky()
	{
		ZpracovatPrvky(HlavniOkno.Controls.Remove);
		ZpracovatPrvky(DisposePrvek);
	}

	internal virtual void ZacatekPlochy()
	{
	}

	internal virtual void KonecPlochy()
	{
	}

	internal virtual void ZmenaVelikosti(bool bMeniSeVelikost)
	{
		if (bMeniSeVelikost)
		{
			HlavniOkno.SuspendLayout();
			ZpracovatPrvky(SkrytPrvek);
			ZrusitGrafikuVpozadi();
			HlavniOkno.ViditelnostMiMaZa(zobrazit: false);
			HlavniOkno.Refresh();
		}
		else
		{
			HlavniOkno.ResumeLayout();
			ZpracovatPrvky(ZobrazitPrvek);
			NastavitGrafikuDoPozadi();
			HlavniOkno.ViditelnostMiMaZa(zobrazit: true);
			HlavniOkno.Refresh();
		}
	}

	protected void ZobrazitPrvek(Control prvek)
	{
		if (prvek != null)
		{
			prvek.Visible = true;
		}
	}

	protected void SkrytPrvek(Control prvek)
	{
		if (prvek != null)
		{
			prvek.Visible = false;
		}
	}

	protected void ZobrazitBublinu(Control prvek)
	{
		if (prvek != null && prvek is _Bublina)
		{
			prvek.Visible = true;
		}
	}

	protected void SkrytBublinu(Control prvek)
	{
		if (prvek != null && prvek is _Bublina)
		{
			prvek.Visible = false;
		}
	}

	protected void DisposePrvek(Control prvek)
	{
		if (prvek != null)
		{
			((IDisposable)prvek)?.Dispose();
		}
	}

	internal virtual bool StisknutaKlavesa(KeyEventArgs e)
	{
		return false;
	}

	internal virtual void PresunOkna(bool bPresouvaSe)
	{
	}

	internal abstract bool FixniVyska();

	internal abstract int VyskaTlacitek();

	private void VykreslitPlochu(Graphics G)
	{
		G.Clear(Barva.PozadiHlavnihoOkna);
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngLevyHorni, new Rectangle(0, 0, 12, 12));
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngPravyHorni, new Rectangle(HlavniOkno.DisplayRectangle.Width - 12 - 1, 0, 12, 12));
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngPravyDolni, new Rectangle(HlavniOkno.DisplayRectangle.Width - 12 - 1, HlavniOkno.DisplayRectangle.Height - 12 - 1, 12, 12));
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngLevyDolni, new Rectangle(0, HlavniOkno.DisplayRectangle.Height - 12 - 1, 12, 12));
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngLevy, new Rectangle(0, 12, 12, HlavniOkno.DisplayRectangle.Height - 24 - 1));
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngHorni, new Rectangle(12, 0, HlavniOkno.DisplayRectangle.Width - 24 - 1, 12));
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngPravy, new Rectangle(HlavniOkno.DisplayRectangle.Width - 12 - 1, 12, 12, HlavniOkno.DisplayRectangle.Height - 24 - 1));
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngDolni, new Rectangle(12, HlavniOkno.DisplayRectangle.Height - 12 - 1, HlavniOkno.DisplayRectangle.Width - 24 - 1, 12));
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngStred, new Rectangle(12, 12, HlavniOkno.DisplayRectangle.Width - 24, HlavniOkno.DisplayRectangle.Height - 24));
	}

	private void VykreslitVyrezanyObdelnik(Graphics G, bool bFixniVyska)
	{
		Rectangle rObdelnik = new Rectangle(12, 12, 974, 712);
		if (!bFixniVyska)
		{
			rObdelnik.Height = HlavniOkno.DisplayRectangle.Height - 24;
		}
		Brush brush = new SolidBrush(Barva.VyrezanyObdelnik);
		G.FillPath(brush, SpecialniGrafika.CestaVyrezanehoObdelniku(rObdelnik, VyskaTlacitek()));
	}

	private void VykreslitPruvodce(Graphics G)
	{
		VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngPruvodce, 850, 440);
	}

	internal static void VykreslitObrazek(Graphics G, Image img, Point pnt)
	{
		G.DrawImage(img, new Rectangle(pnt, img.Size));
	}

	internal static void VykreslitObrazek(Graphics G, Image img, int x, int y)
	{
		VykreslitObrazek(G, img, new Point(x, y));
	}

	internal static void VykreslitText(Graphics G, string text, Color barvaPisma, int velikostPisma, FontStyle typPisma, Rectangle oblast, StringAlignment vertikalne, StringAlignment horizontalne)
	{
		VykreslitText(G, text, barvaPisma, new Font(VychoziPismo.FontFamily, velikostPisma, typPisma), oblast, vertikalne, horizontalne);
	}

	internal static void VykreslitText(Graphics G, string text, Color barvaPisma, Font fnt, Rectangle oblast, StringAlignment vertikalne, StringAlignment horizontalne)
	{
		StringFormat stringFormat = new StringFormat();
		stringFormat.LineAlignment = vertikalne;
		stringFormat.Alignment = horizontalne;
		Brush brush = new SolidBrush(barvaPisma);
		G.DrawString(text, fnt, brush, oblast, stringFormat);
	}
}
