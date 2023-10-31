using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HYL.MountBlue.Controls;

internal class ObrazkoveTlacitko : UserControl
{
	internal enum eStavTlacitka : byte
	{
		normalni,
		zvyrazneny,
		stisknuty
	}

	public delegate void Stisknuto();

	private eStavTlacitka aktualniStavTlacitka;

	private Image mimgZvyrazneny;

	private Image mimgStisknuty;

	private Image mimgZakazany;

	private Image mimgNormalni;

	private IContainer components;

	[Description("Obrázek, který bude vidět, když uživatel bude kurzorem na tlačítku (Hover).")]
	[Category("Konfigurace tlačítka")]
	public Image ZvyraznenyObrazek
	{
		get
		{
			return mimgZvyrazneny;
		}
		set
		{
			mimgZvyrazneny = value;
		}
	}

	[Category("Konfigurace tlačítka")]
	[Description("Obrázek, který bude vidět při stisknutém tlačítku.")]
	public Image StisknutyObrazek
	{
		get
		{
			return mimgStisknuty;
		}
		set
		{
			mimgStisknuty = value;
		}
	}

	[Category("Konfigurace tlačítka")]
	[Description("Obrázek, který bude vidět v normálním stavu tlačítka.")]
	public Image NormalniObrazek
	{
		get
		{
			return mimgNormalni;
		}
		set
		{
			mimgNormalni = value;
		}
	}

	[Category("Konfigurace tlačítka")]
	[Description("Obrázek, který je vidět při zakázaném tlačítku.")]
	public Image ZakazanyObrazek
	{
		get
		{
			return mimgZakazany;
		}
		set
		{
			mimgZakazany = value;
		}
	}

	public event Stisknuto TlacitkoStisknuto;

	public ObrazkoveTlacitko()
	{
		InitializeComponent();
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
	}

	protected override void OnMouseDown(MouseEventArgs mevent)
	{
		base.OnMouseDown(mevent);
		if (mevent.Button == MouseButtons.Left && aktualniStavTlacitka != eStavTlacitka.stisknuty)
		{
			aktualniStavTlacitka = eStavTlacitka.stisknuty;
			Invalidate();
		}
	}

	protected override void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		if (aktualniStavTlacitka != eStavTlacitka.zvyrazneny)
		{
			aktualniStavTlacitka = eStavTlacitka.zvyrazneny;
			Invalidate();
		}
	}

	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		if (aktualniStavTlacitka != 0)
		{
			aktualniStavTlacitka = eStavTlacitka.normalni;
			Invalidate();
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (e.Button == MouseButtons.Left)
		{
			bool flag = DisplayRectangle.Contains(e.Location);
			if (flag && aktualniStavTlacitka != eStavTlacitka.stisknuty)
			{
				aktualniStavTlacitka = eStavTlacitka.stisknuty;
				Invalidate();
			}
			else if (!flag && aktualniStavTlacitka != 0)
			{
				aktualniStavTlacitka = eStavTlacitka.normalni;
				Invalidate();
			}
		}
	}

	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		base.OnMouseUp(mevent);
		if (aktualniStavTlacitka == eStavTlacitka.stisknuty && mevent.Button == MouseButtons.Left)
		{
			aktualniStavTlacitka = eStavTlacitka.zvyrazneny;
			Invalidate();
			if (this.TlacitkoStisknuto != null)
			{
				this.TlacitkoStisknuto();
			}
		}
	}

	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
		aktualniStavTlacitka = eStavTlacitka.normalni;
		Invalidate();
	}

	protected override void OnKeyDown(KeyEventArgs kevent)
	{
		base.OnKeyDown(kevent);
		if ((aktualniStavTlacitka != eStavTlacitka.stisknuty && kevent.KeyCode == Keys.Space) || kevent.KeyCode == Keys.Return)
		{
			aktualniStavTlacitka = eStavTlacitka.stisknuty;
			Invalidate();
		}
		else if (kevent.KeyCode == Keys.Escape && aktualniStavTlacitka == eStavTlacitka.stisknuty)
		{
			aktualniStavTlacitka = eStavTlacitka.normalni;
			Invalidate();
		}
	}

	protected override void OnKeyUp(KeyEventArgs kevent)
	{
		base.OnKeyUp(kevent);
		if (aktualniStavTlacitka == eStavTlacitka.stisknuty && (kevent.KeyCode == Keys.Space || kevent.KeyCode == Keys.Return))
		{
			aktualniStavTlacitka = eStavTlacitka.normalni;
			Invalidate();
			if (this.TlacitkoStisknuto != null)
			{
				this.TlacitkoStisknuto();
			}
		}
	}

	protected override void OnPaint(PaintEventArgs pevent)
	{
		base.OnPaint(pevent);
		Graphics graphics = pevent.Graphics;
		if (!base.Enabled)
		{
			VykreslitObrazek(graphics, mimgZakazany);
			return;
		}
		switch (aktualniStavTlacitka)
		{
		case eStavTlacitka.zvyrazneny:
			VykreslitObrazek(graphics, mimgZvyrazneny);
			break;
		case eStavTlacitka.stisknuty:
			VykreslitObrazek(graphics, mimgStisknuty);
			break;
		default:
			VykreslitObrazek(graphics, mimgNormalni);
			break;
		}
	}

	protected override void OnEnter(EventArgs e)
	{
		base.OnEnter(e);
		if (aktualniStavTlacitka != eStavTlacitka.zvyrazneny)
		{
			aktualniStavTlacitka = eStavTlacitka.zvyrazneny;
			Invalidate();
		}
	}

	protected override void OnLeave(EventArgs e)
	{
		base.OnLeave(e);
		if (aktualniStavTlacitka != 0)
		{
			aktualniStavTlacitka = eStavTlacitka.normalni;
			Invalidate();
		}
	}

	private void VykreslitObrazek(Graphics G, Image obrazek)
	{
		G.Clear(BackColor);
		if (obrazek != null)
		{
			G.DrawImage(obrazek, DisplayRectangle);
		}
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
		this.BackColor = System.Drawing.Color.Blue;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.ForeColor = System.Drawing.Color.White;
		base.Name = "ObrazkoveTlacitko";
		base.Size = new System.Drawing.Size(140, 23);
		base.ResumeLayout(false);
	}
}
