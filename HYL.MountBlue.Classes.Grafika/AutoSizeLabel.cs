using System.Drawing;

namespace HYL.MountBlue.Classes.Grafika;

internal class AutoSizeLabel
{
	public const int MinimalniSirka = 100;

	public const int MaximalniSirka = 700;

	public const int Prirustek = 50;

	public const float PomerStran = 2.5f;

	private string text;

	private PointF pozice;

	private Font fnt;

	private StringFormat fmt;

	private Color clr;

	private int minWdt;

	private SizeF siz;

	public string Text => text;

	public PointF Pozice => pozice;

	public Font Pismo => fnt;

	public StringFormat Format => fmt;

	public SizeF Velikost => siz;

	public RectangleF Oblast => new RectangleF(pozice, Velikost);

	public Color BarvaPisma => clr;

	public AutoSizeLabel(Graphics G, string text, PointF pozice, Font fnt, StringFormat fmt, Color clr)
		: this(G, text, pozice, fnt, fmt, clr, 100)
	{
	}

	public AutoSizeLabel(Graphics G, string text, PointF pozice, Font fnt, StringFormat fmt, Color clr, int minimumWidth)
	{
		this.text = text;
		this.pozice = pozice;
		this.fnt = fnt;
		this.fmt = fmt;
		this.clr = clr;
		if (minimumWidth < 100)
		{
			minWdt = 100;
		}
		else
		{
			minWdt = minimumWidth;
		}
		SpocitatObdelnik(G);
	}

	private void SpocitatObdelnik(Graphics G)
	{
		int num = minWdt;
		SizeF sizeF;
		do
		{
			sizeF = G.MeasureString(Text, Pismo, num, Format);
			num += 50;
		}
		while (sizeF.Width <= sizeF.Height * 2.5f && num < 700);
		siz = sizeF;
	}

	public void Vykreslit(Graphics G)
	{
		G.DrawString(Text, Pismo, new SolidBrush(BarvaPisma), Oblast, Format);
	}
}
