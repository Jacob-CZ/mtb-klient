using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class _Dialog : Form, IDisposable
{
	internal const int PolomerZaobleni = 14;

	private const int OkrajTextuZahlavi = 4;

	private Point mpntPoziceMysi;

	private int mintVyskaZahlavi = 9;

	private GraphicsPath CestaOkraje;

	internal readonly Font PismoZahlavi;

	internal readonly StringFormat FormatZahlavi;

	private Rectangle mrctTextZahlavi;

	private IContainer components;

	private ObrazkoveTlacitko cmdZavrit;

	[Description("Výška oranžovo-červeného záhlaví.")]
	[Category("Konfigurace dialogu")]
	public int VyskaZahlavi
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

	protected virtual bool OknoLzePresouvat => true;

	protected virtual GraphicsPath RegionOkna => SpecialniGrafika.CestaZaoblenehoObdelniku(OblastOkna, 14);

	protected virtual Rectangle OblastOkna => DisplayRectangle;

	public _Dialog()
	{
		InitializeComponent();
		base.FormBorderStyle = FormBorderStyle.None;
		VyskaZahlavi = 45;
		PismoZahlavi = new Font(Font.FontFamily, 11f, FontStyle.Bold);
		FormatZahlavi = new StringFormat();
		FormatZahlavi.LineAlignment = StringAlignment.Center;
		FormatZahlavi.Alignment = StringAlignment.Center;
		FormatZahlavi.Trimming = StringTrimming.EllipsisWord;
		NastavitRegion();
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, value: true);
		base.Location = new Point(-1200, -1000);
	}

	~_Dialog()
	{
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		VykreslitZahlavi(e.Graphics);
		e.Graphics.SmoothingMode = SmoothingMode.Default;
		VykreslitOkraj(e.Graphics);
	}

	private void cmdZavrit_TlacitkoStisknuto()
	{
		base.DialogResult = DialogResult.Cancel;
		Close();
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		if (OknoLzePresouvat && e.Button == MouseButtons.Left && e.Y <= mintVyskaZahlavi)
		{
			mpntPoziceMysi = new Point(-e.X, -e.Y);
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left && mpntPoziceMysi.X != 0 && mpntPoziceMysi.Y != 0)
		{
			Point location = e.Location;
			Point location2 = base.Location;
			location.Offset(mpntPoziceMysi);
			location2.Offset(location);
			base.Location = location2;
		}
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		mpntPoziceMysi = new Point(0, 0);
	}

	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
		PrepocitatHodnoty();
		NastavitRegion();
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		Invalidate();
	}

	private void PrepocitatHodnoty()
	{
		if (mintVyskaZahlavi > 0)
		{
			int num = 4;
			int num2 = 0;
			num = (mintVyskaZahlavi - cmdZavrit.Height) / 2 + 1;
			num2 = cmdZavrit.Width;
			Point location = new Point(DisplayRectangle.Width - num - num2 - 1, num);
			cmdZavrit.Location = location;
			if (!cmdZavrit.Visible)
			{
				num2 = 0;
			}
			mrctTextZahlavi = new Rectangle(OblastOkna.Left + num, OblastOkna.Top + 4, OblastOkna.Width - 2 * num - num2, mintVyskaZahlavi - 8);
		}
		Invalidate();
	}

	private void VykreslitOkraj(Graphics G)
	{
		G.DrawPath(new Pen(Barva.OkrajDialogu, 2f), CestaOkraje);
	}

	private void VykreslitZahlavi(Graphics G)
	{
		if (mintVyskaZahlavi > 0 && Text.Length != 0)
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

	protected virtual void NastavitVychoziPozici()
	{
		FormStartPosition formStartPosition = VychoziPozice();
		if (base.Owner == null && formStartPosition == FormStartPosition.CenterParent)
		{
			CenterToScreen();
			return;
		}
		switch (formStartPosition)
		{
		case FormStartPosition.CenterParent:
			CenterToParent();
			break;
		case FormStartPosition.CenterScreen:
			CenterToScreen();
			break;
		}
	}

	protected override void OnFormClosed(FormClosedEventArgs e)
	{
		OnFadeOut();
		base.OnFormClosed(e);
	}

	protected virtual void OnFadeIn()
	{
		SpecialniGrafika.FadeIn(this);
	}

	protected virtual void OnFadeOut()
	{
		SpecialniGrafika.FadeOut(this);
	}

	protected override void OnClosed(EventArgs e)
	{
		base.OnClosed(e);
		_Plocha.NastavitViditelnostBubliny(bViditelnostBubliny: true);
	}

	protected virtual FormStartPosition VychoziPozice()
	{
		return FormStartPosition.CenterParent;
	}

	private void _Dialog_Shown(object sender, EventArgs e)
	{
		try
		{
			NastavitVychoziPozici();
			_Plocha.NastavitViditelnostBubliny(bViditelnostBubliny: false);
			OnFadeIn();
		}
		catch
		{
		}
	}

	private void _Dialog_Load(object sender, EventArgs e)
	{
		base.Opacity = 0.0;
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
		this.cmdZavrit = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		base.SuspendLayout();
		this.cmdZavrit.BackColor = HYL.MountBlue.Classes.Grafika.Barva.PozadiTlacitkaZavrit;
		this.cmdZavrit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZavrit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZavrit.ForeColor = System.Drawing.Color.White;
		this.cmdZavrit.Location = new System.Drawing.Point(420, 12);
		this.cmdZavrit.Name = "cmdZavrit";
		this.cmdZavrit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavN;
		this.cmdZavrit.Size = new System.Drawing.Size(24, 24);
		this.cmdZavrit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavD;
		this.cmdZavrit.TabIndex = 20;
		this.cmdZavrit.TabStop = false;
		this.cmdZavrit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavZ;
		this.cmdZavrit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavH;
		this.cmdZavrit.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZavrit_TlacitkoStisknuto);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(462, 204);
		base.ControlBox = false;
		base.Controls.Add(this.cmdZavrit);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = HYL.MountBlue.Resources.Grafika.icoMountBlue;
		this.MinimumSize = new System.Drawing.Size(300, 150);
		base.Name = "_Dialog";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Load += new System.EventHandler(_Dialog_Load);
		base.Shown += new System.EventHandler(_Dialog_Shown);
		base.ResumeLayout(false);
	}
}
