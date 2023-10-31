using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Forms;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

public class _Bublina : UserControl
{
	private const int OkrajTextuZahlavi = 4;

	private readonly Point PoziceUstPruvodce = new Point(890, 475);

	private int mintVyskaZahlavi = 9;

	private GraphicsPath CestaOkraje;

	internal readonly Font PismoZahlavi;

	internal readonly StringFormat FormatZahlavi;

	private Rectangle mrctTextZahlavi;

	private string mstrText;

	private IContainer components;

	[Category("Konfigurace prvku")]
	[Description("Výška oranžovo-červeného záhlaví.")]
	internal int VyskaZahlavi
	{
		get
		{
			return mintVyskaZahlavi;
		}
		set
		{
			if (value <= 8 && value != 0)
			{
				throw new ArgumentOutOfRangeException("VyskaZahlavi", $"Hodnota musí být větší než {8} nebo 0.");
			}
			mintVyskaZahlavi = value;
			PrepocitatHodnoty();
		}
	}

	[Category("Konfigurace prvku")]
	[Description("Text v záhlaví prvku.")]
	public override string Text
	{
		get
		{
			return mstrText;
		}
		set
		{
			mstrText = value;
			PrepocitatHodnoty();
		}
	}

	protected virtual GraphicsPath RegionOkna => SpecialniGrafika.CestaBubliny(DisplayRectangle, OblastOkna);

	protected virtual Rectangle OblastOkna
	{
		get
		{
			Rectangle displayRectangle = DisplayRectangle;
			displayRectangle.Width -= 65;
			return displayRectangle;
		}
	}

	protected virtual Point PoziceSpickyBubliny => new Point(DisplayRectangle.Width, DisplayRectangle.Height - 18);

	public _Bublina()
	{
		InitializeComponent();
		VyskaZahlavi = 30;
		PismoZahlavi = new Font(Font.FontFamily, 11f, FontStyle.Bold);
		FormatZahlavi = new StringFormat();
		FormatZahlavi.LineAlignment = StringAlignment.Center;
		FormatZahlavi.Alignment = StringAlignment.Center;
		FormatZahlavi.Trimming = StringTrimming.EllipsisWord;
		NastavitRegion();
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		VykreslitZahlavi(e.Graphics);
		e.Graphics.SmoothingMode = SmoothingMode.Default;
		VykreslitOkraj(e.Graphics);
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		PrepocitatHodnoty();
		NastavitRegion();
	}

	private void PrepocitatHodnoty()
	{
		if (mintVyskaZahlavi > 0)
		{
			int num = 4;
			int num2 = 0;
			mrctTextZahlavi = new Rectangle(OblastOkna.Left + num, OblastOkna.Top + 4, OblastOkna.Width - 2 * num - num2, mintVyskaZahlavi - 8);
		}
		Invalidate();
	}

	internal void NastavitPozici(HlavniOkno hlavniOkno)
	{
		Point location = new Point((hlavniOkno.Width - 1000) / 2 + PoziceUstPruvodce.X - PoziceSpickyBubliny.X, PoziceUstPruvodce.Y - PoziceSpickyBubliny.Y);
		base.Location = location;
	}

	protected virtual Size VelikostOkna(Size oblastOkna)
	{
		oblastOkna.Width += 65;
		return oblastOkna;
	}

	private void VykreslitZahlavi(Graphics G)
	{
		if (mintVyskaZahlavi > 0 && Text != null && Text.Length != 0)
		{
			G.DrawImage(Grafika.pngZahlaviDialogu, new Rectangle(OblastOkna.Left, OblastOkna.Top, OblastOkna.Width, mintVyskaZahlavi));
			G.DrawString(Text, PismoZahlavi, new SolidBrush(Barva.PismoZahlaviOkna), mrctTextZahlavi, FormatZahlavi);
		}
	}

	private void NastavitRegion()
	{
		CestaOkraje = RegionOkna;
		base.Region = new Region(CestaOkraje);
	}

	private void VykreslitOkraj(Graphics G)
	{
		G.DrawPath(new Pen(Barva.OkrajBubliny, 2f), CestaOkraje);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		this.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		base.Name = "_Bublina";
		base.Size = new System.Drawing.Size(533, 236);
		base.ResumeLayout(false);
	}
}
