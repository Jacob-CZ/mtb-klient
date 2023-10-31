using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace HYL.MountBlue.Classes.Grafika;

internal static class SpecialniGrafika
{
	internal const float FadeMinimum = 0f;

	internal const float FadeMaximum = 0.999f;

	internal const float FadeMiddle = 0.65f;

	internal const int FadeTimeMilliseconds = 5;

	internal const float FadeVychoziKrok = 0.15f;

	internal static void FadeIn(Form frm, float krok, float fMinimum, float fMaximum)
	{
		frm.Refresh();
		for (float num = fMinimum; num < fMaximum; num += krok)
		{
			frm.Opacity = num;
			Application.DoEvents();
			Thread.Sleep(5);
		}
		frm.Opacity = fMaximum;
		frm.Refresh();
	}

	internal static void FadeIn(Form frm)
	{
		FadeIn(frm, 0.15f, 0f, 0.999f);
	}

	internal static void FadeOut(Form frm, float krok, float fMinimum, float fMaximum)
	{
		frm.Refresh();
		for (float num = fMaximum; num > fMinimum; num -= krok)
		{
			Application.DoEvents();
			Thread.Sleep(5);
			frm.Opacity = num;
			Application.DoEvents();
			Thread.Sleep(5);
		}
		frm.Opacity = fMinimum;
		frm.Refresh();
	}

	internal static void FadeOut(Form frm)
	{
		FadeOut(frm, 0.15f, 0f, 0.999f);
	}

	internal static GraphicsPath CestaZaoblenehoObdelniku(Rectangle rObdelnik, int iPolomer)
	{
		int num = iPolomer * 2;
		Rectangle rect = new Rectangle(size: new Size(num, num), location: rObdelnik.Location);
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddArc(rect, 180f, 90f);
		rect.X = rObdelnik.Right - num - 1;
		graphicsPath.AddArc(rect, 270f, 90f);
		rect.Y = rObdelnik.Bottom - num - 1;
		graphicsPath.AddArc(rect, 0f, 90f);
		rect.X = rObdelnik.Left;
		graphicsPath.AddArc(rect, 90f, 90f);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	internal static GraphicsPath CestaVyrezanehoObdelniku(Rectangle rObdelnik, int iVyskaTlacitek)
	{
		int num = 20;
		if (iVyskaTlacitek != 0 && iVyskaTlacitek <= num)
		{
			throw new ArgumentOutOfRangeException("iVyskaTlacitek", "Výška tlačítek může být 0 nebo větší než dvojnásobek poloměru zaoblení.");
		}
		Rectangle rect = new Rectangle(size: new Size(num, num), location: rObdelnik.Location);
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddArc(rect, 180f, 90f);
		rect.X = rObdelnik.Right - num - 136;
		graphicsPath.AddArc(rect, 270f, 90f);
		if (iVyskaTlacitek > 0)
		{
			rect.X = rObdelnik.Right - 136;
			rect.Y = 137 - num;
			graphicsPath.AddArc(rect, 180f, -90f);
			rect.X = rObdelnik.Right - num - 1;
			rect.Y = 137;
			graphicsPath.AddArc(rect, 270f, 90f);
			rect.X = rObdelnik.Right - num - 1;
			rect.Y = 137 + iVyskaTlacitek;
			graphicsPath.AddArc(rect, 0f, 90f);
			rect.X = rObdelnik.Right - 136;
			rect.Y = 137 + iVyskaTlacitek + num;
			graphicsPath.AddArc(rect, 270f, -90f);
		}
		rect.X = rObdelnik.Right - 136;
		rect.Y = 633 - num;
		graphicsPath.AddArc(rect, 180f, -90f);
		rect.X = rObdelnik.Right - num - 1;
		rect.Y = 633;
		graphicsPath.AddArc(rect, 270f, 90f);
		rect.X = rObdelnik.Right - num - 1;
		rect.Y = 687 - num;
		graphicsPath.AddArc(rect, 0f, 90f);
		rect.X = rObdelnik.Right - 136;
		rect.Y = 687;
		graphicsPath.AddArc(rect, 270f, -90f);
		rect.X = rObdelnik.Right - 136 - num;
		rect.Y = rObdelnik.Bottom - num - 1;
		graphicsPath.AddArc(rect, 0f, 90f);
		rect.X = rObdelnik.Left;
		graphicsPath.AddArc(rect, 90f, 90f);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	internal static GraphicsPath CestaBubliny(Rectangle rCeleOkno, Rectangle rAktivniOblast)
	{
		int num = 20;
		Rectangle rect = new Rectangle(size: new Size(num, num), location: rAktivniOblast.Location);
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddArc(rect, 180f, 90f);
		rect.X = rAktivniOblast.Right - num - 1;
		graphicsPath.AddArc(rect, 270f, 90f);
		Point pt = new Point(rAktivniOblast.Right - 1, rAktivniOblast.Bottom - 25 - 40);
		Point point = new Point(rCeleOkno.Right - 1, rCeleOkno.Bottom - 18);
		Point point2 = new Point(rAktivniOblast.Right + 16 - 1, rAktivniOblast.Bottom - 25);
		Point pt2 = new Point(rAktivniOblast.Right - 1, rAktivniOblast.Bottom - 25);
		graphicsPath.AddLine(pt, point);
		graphicsPath.AddLine(point, point2);
		graphicsPath.AddLine(point2, pt2);
		rect.Y = rAktivniOblast.Bottom - num - 1;
		graphicsPath.AddArc(rect, 0f, 90f);
		rect.X = rAktivniOblast.Left;
		graphicsPath.AddArc(rect, 90f, 90f);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	internal static GraphicsPath CestaBublinyNovaKlavesa(Rectangle rCeleOkno, Rectangle rAktivniOblast)
	{
		int num = 20;
		Rectangle rect = new Rectangle(size: new Size(num, num), location: rAktivniOblast.Location);
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddArc(rect, 180f, 90f);
		rect.X = rAktivniOblast.Right - num - 1;
		graphicsPath.AddArc(rect, 270f, 90f);
		rect.Y = rAktivniOblast.Bottom - num - 1;
		graphicsPath.AddArc(rect, 0f, 90f);
		Point pt = new Point(rAktivniOblast.Right - 25, rAktivniOblast.Bottom - 1);
		Point point = new Point(rCeleOkno.Right - 1, rCeleOkno.Bottom - 1);
		Point pt2 = new Point(rAktivniOblast.Right - 50, rAktivniOblast.Bottom - 1);
		graphicsPath.AddLine(pt, point);
		graphicsPath.AddLine(point, pt2);
		rect.X = rAktivniOblast.Left;
		graphicsPath.AddArc(rect, 90f, 90f);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	internal static GraphicsPath CestaBublinySezeni(Rectangle rCeleOkno, Rectangle rAktivniOblast)
	{
		int num = 20;
		Rectangle rect = new Rectangle(size: new Size(num, num), location: rAktivniOblast.Location);
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddArc(rect, 180f, 90f);
		rect.X = rAktivniOblast.Right - num - 1;
		graphicsPath.AddArc(rect, 270f, 90f);
		Point pt = new Point(rAktivniOblast.Right - 1, rAktivniOblast.Bottom - 20 - 35);
		Point point = new Point(rCeleOkno.Right - 1, 0);
		Point pt2 = new Point(rAktivniOblast.Right - 1, rAktivniOblast.Bottom - 20);
		graphicsPath.AddLine(pt, point);
		graphicsPath.AddLine(point, pt2);
		rect.Y = rAktivniOblast.Bottom - num - 1;
		graphicsPath.AddArc(rect, 0f, 90f);
		rect.X = rAktivniOblast.Left;
		graphicsPath.AddArc(rect, 90f, 90f);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}

	internal static GraphicsPath CestaBublinyVysledky(Rectangle rCeleOkno, Rectangle rAktivniOblast)
	{
		int num = 20;
		Rectangle rect = new Rectangle(size: new Size(num, num), location: rAktivniOblast.Location);
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddArc(rect, 180f, 90f);
		rect.X = rAktivniOblast.Right - num - 1;
		graphicsPath.AddArc(rect, 270f, 90f);
		rect.Y = rAktivniOblast.Bottom - num - 1;
		graphicsPath.AddArc(rect, 0f, 90f);
		Point pt = new Point(55, rAktivniOblast.Bottom - 1);
		Point point = new Point(83, rCeleOkno.Bottom - 1);
		Point pt2 = new Point(30, rAktivniOblast.Bottom - 1);
		graphicsPath.AddLine(pt, point);
		graphicsPath.AddLine(point, pt2);
		rect.X = rAktivniOblast.Left;
		graphicsPath.AddArc(rect, 90f, 90f);
		graphicsPath.CloseFigure();
		return graphicsPath;
	}
}
