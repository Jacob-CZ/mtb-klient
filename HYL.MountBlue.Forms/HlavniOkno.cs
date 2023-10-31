using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Forms;

internal class HlavniOkno : Form
{
	private enum ePresunyOkna : byte
	{
		zadny = 0,
		levy = 2,
		pravy = 4,
		horni = 8,
		dolni = 0x10,
		celeOkno = 0x20
	}

	private const int cOkraj = 12;

	private bool bIsMaximized;

	private Rectangle rOblastBezOkraje;

	private ePresunyOkna mbytPresun;

	private Point mpntPoziceMysi;

	private IContainer components;

	private MinMaxZavrit ucMiMaZa;

	public bool JeOknoMaximalizovane
	{
		get
		{
			Rectangle pracovniOblast = PracovniOblast;
			if (base.Location == pracovniOblast.Location)
			{
				return base.Size == pracovniOblast.Size;
			}
			return false;
		}
	}

	internal Rectangle PracovniOblast
	{
		get
		{
			Rectangle workingArea = Screen.GetWorkingArea(this);
			workingArea.Width++;
			workingArea.Height++;
			return workingArea;
		}
	}

	internal HlavniOkno()
	{
		InitializeComponent();
		SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
		Klient.StartLeasingu();
		MaximalizovatOkno();
		Text = Texty._JmenoMB;
		base.Opacity = 0.0;
	}

	private void fHlavniOkno_Load(object sender, EventArgs e)
	{
		NastavitRegion();
		BackColor = Barva.PozadiHlavnihoOkna;
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		Activate();
		BringToFront();
		SpecialniGrafika.FadeIn(this);
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		base.OnFormClosing(e);
		if (!e.Cancel)
		{
			SpecialniGrafika.FadeOut(this);
		}
	}

	private void HlavniOkno_KeyDown(object sender, KeyEventArgs e)
	{
		if (_Plocha.AktualniPlocha.StisknutaKlavesa(e))
		{
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
	}

	private void fHlavniOkno_MouseDown(object sender, MouseEventArgs e)
	{
		if (bIsMaximized || !ucMiMaZa.Enabled)
		{
			if (Cursor != Cursors.Default)
			{
				Cursor = Cursors.Default;
			}
			return;
		}
		mbytPresun = ePresunyOkna.zadny;
		if (e.Button == MouseButtons.Left)
		{
			if (e.X < 12)
			{
				mbytPresun |= ePresunyOkna.levy;
			}
			else if (e.X > DisplayRectangle.Width - 12)
			{
				mbytPresun |= ePresunyOkna.pravy;
			}
			if (e.Y < 12)
			{
				mbytPresun |= ePresunyOkna.horni;
			}
			else if (e.Y > DisplayRectangle.Height - 12)
			{
				mbytPresun |= ePresunyOkna.dolni;
			}
			if (mbytPresun == ePresunyOkna.zadny)
			{
				mbytPresun = ePresunyOkna.celeOkno;
			}
			mpntPoziceMysi = new Point(-e.X, -e.Y);
			if (mbytPresun == ePresunyOkna.celeOkno)
			{
				_Plocha.AktualniPlocha.PresunOkna(bPresouvaSe: true);
				return;
			}
			_Plocha.AktualniPlocha.ZmenaVelikosti(bMeniSeVelikost: true);
			base.Region = null;
			Invalidate();
		}
	}

	private void fHlavniOkno_MouseMove(object sender, MouseEventArgs e)
	{
		if (bIsMaximized)
		{
			if (Cursor != Cursors.Default)
			{
				Cursor = Cursors.Default;
			}
		}
		else if (e.Button == MouseButtons.Left)
		{
			Point location = e.Location;
			Point location2 = base.Location;
			location.Offset(mpntPoziceMysi);
			if (mbytPresun == ePresunyOkna.celeOkno)
			{
				location2.Offset(location);
				base.Location = location2;
				return;
			}
			Size size = base.Size;
			if ((int)(mbytPresun & ePresunyOkna.levy) > 0)
			{
				location2.X += location.X;
				size.Width -= location.X;
			}
			else if ((int)(mbytPresun & ePresunyOkna.pravy) > 0)
			{
				mpntPoziceMysi.X -= location.X;
				size.Width += location.X;
			}
			if ((int)(mbytPresun & ePresunyOkna.horni) > 0)
			{
				location2.Y += location.Y;
				size.Height -= location.Y;
			}
			else if ((int)(mbytPresun & ePresunyOkna.dolni) > 0)
			{
				mpntPoziceMysi.Y -= location.Y;
				size.Height += location.Y;
			}
			if (size.Width < MinimumSize.Width)
			{
				if ((int)(mbytPresun & ePresunyOkna.levy) > 0)
				{
					location2.X -= MinimumSize.Width - size.Width;
				}
				size.Width = MinimumSize.Width;
			}
			if (size.Height < MinimumSize.Height)
			{
				if ((int)(mbytPresun & ePresunyOkna.horni) > 0)
				{
					location2.Y -= MinimumSize.Height - size.Height;
				}
				size.Height = MinimumSize.Height;
			}
			if (size.Width > MaximumSize.Width)
			{
				if ((int)(mbytPresun & ePresunyOkna.levy) > 0)
				{
					location2.X += size.Width - MaximumSize.Width;
				}
				size.Width = MaximumSize.Width;
			}
			if (size.Height > MaximumSize.Height)
			{
				if ((int)(mbytPresun & ePresunyOkna.horni) > 0)
				{
					location2.Y += size.Height - MaximumSize.Height;
				}
				size.Height = MaximumSize.Height;
			}
			base.Location = location2;
			base.Size = size;
		}
		else if (!rOblastBezOkraje.Contains(e.Location))
		{
			if ((e.X < 12 && e.Y < 12) || (e.X > DisplayRectangle.Width - 12 && e.Y > DisplayRectangle.Height - 12))
			{
				Cursor = Cursors.SizeNWSE;
			}
			else if ((e.X < 12 && e.Y > DisplayRectangle.Height - 12) || (e.X > DisplayRectangle.Width - 12 && e.Y < 12))
			{
				Cursor = Cursors.SizeNESW;
			}
			else if (e.X < 12 || e.X > DisplayRectangle.Width - 12)
			{
				Cursor = Cursors.SizeWE;
			}
			else if (e.Y < 12 || e.Y > DisplayRectangle.Height - 12)
			{
				Cursor = Cursors.SizeNS;
			}
		}
		else if (Cursor != Cursors.Default)
		{
			Cursor = Cursors.Default;
		}
	}

	private void HlavniOkno_MouseUp(object sender, MouseEventArgs e)
	{
		if (mbytPresun != 0 && mbytPresun != ePresunyOkna.celeOkno)
		{
			NastavitRegion();
			ObnovitOblastBezOkraje();
			_Plocha.AktualniPlocha.ZmenaVelikosti(bMeniSeVelikost: false);
		}
		else if (mbytPresun == ePresunyOkna.celeOkno)
		{
			_Plocha.AktualniPlocha.PresunOkna(bPresouvaSe: false);
		}
		mbytPresun = ePresunyOkna.zadny;
	}

	private void ObnovitOblastBezOkraje()
	{
		rOblastBezOkraje = DisplayRectangle;
		rOblastBezOkraje.Inflate(-12, -12);
	}

	private void ObnovitOkno()
	{
		if (_Plocha.AktualniPlocha != null)
		{
			_Plocha.AktualniPlocha.ZmenaVelikosti(bMeniSeVelikost: true);
		}
		base.Size = MinimumSize;
		CenterToScreen();
		bIsMaximized = false;
		NastavitRegion();
		ObnovitOblastBezOkraje();
		if (_Plocha.AktualniPlocha != null)
		{
			_Plocha.AktualniPlocha.ZmenaVelikosti(bMeniSeVelikost: false);
		}
	}

	private void MaximalizovatOkno()
	{
		if (_Plocha.AktualniPlocha != null)
		{
			_Plocha.AktualniPlocha.ZmenaVelikosti(bMeniSeVelikost: true);
		}
		Rectangle pracovniOblast = PracovniOblast;
		base.Location = pracovniOblast.Location;
		base.Size = pracovniOblast.Size;
		bIsMaximized = true;
		NastavitRegion();
		ObnovitOblastBezOkraje();
		if (_Plocha.AktualniPlocha != null)
		{
			_Plocha.AktualniPlocha.ZmenaVelikosti(bMeniSeVelikost: false);
		}
	}

	private void NastavitRegion()
	{
		GraphicsPath path = SpecialniGrafika.CestaZaoblenehoObdelniku(DisplayRectangle, 16);
		base.Region = new Region(path);
	}

	internal void ViditelnostMiMaZa(bool zobrazit)
	{
		ucMiMaZa.Visible = zobrazit;
	}

	internal void ZpristupneniMiMaZa(bool zpristupnit)
	{
		ucMiMaZa.Enabled = zpristupnit;
	}

	private void ucMiMaZa_TlacitkoMinimalizovat()
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void ucMiMaZa_TlacitkoMaximalizovat()
	{
		if (JeOknoMaximalizovane)
		{
			ObnovitOkno();
		}
		else
		{
			MaximalizovatOkno();
		}
	}

	private void ucMiMaZa_TlacitkoZavrit()
	{
		Close();
	}

	private void HlavniOkno_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (e.CloseReason != CloseReason.ApplicationExitCall && e.CloseReason != CloseReason.TaskManagerClosing && e.CloseReason != CloseReason.WindowsShutDown)
		{
			e.Cancel = !ucMiMaZa.Enabled || (PUzivatele.JeUzivatelPrihlaseny && !PUzivatele.PrihlasenyUzivatel.OdhlasitUzivatele(bZobrazitPrihlaseni: false));
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HYL.MountBlue.Forms.HlavniOkno));
		this.ucMiMaZa = new HYL.MountBlue.Controls.MinMaxZavrit();
		base.SuspendLayout();
		this.ucMiMaZa.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.ucMiMaZa.BackColor = System.Drawing.Color.Silver;
		this.ucMiMaZa.BackgroundImage = (System.Drawing.Image)resources.GetObject("ucMiMaZa.BackgroundImage");
		this.ucMiMaZa.Cursor = System.Windows.Forms.Cursors.Arrow;
		this.ucMiMaZa.Location = new System.Drawing.Point(883, 12);
		this.ucMiMaZa.MaximumSize = new System.Drawing.Size(106, 33);
		this.ucMiMaZa.MinimumSize = new System.Drawing.Size(106, 33);
		this.ucMiMaZa.Name = "ucMiMaZa";
		this.ucMiMaZa.Size = new System.Drawing.Size(106, 33);
		this.ucMiMaZa.TabIndex = 15;
		this.ucMiMaZa.TlacitkoZavrit += new HYL.MountBlue.Controls.MinMaxZavrit.TlacitkoStisknuto(ucMiMaZa_TlacitkoZavrit);
		this.ucMiMaZa.TlacitkoMinimalizovat += new HYL.MountBlue.Controls.MinMaxZavrit.TlacitkoStisknuto(ucMiMaZa_TlacitkoMinimalizovat);
		this.ucMiMaZa.TlacitkoMaximalizovat += new HYL.MountBlue.Controls.MinMaxZavrit.TlacitkoStisknuto(ucMiMaZa_TlacitkoMaximalizovat);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Silver;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		base.ClientSize = new System.Drawing.Size(1001, 738);
		base.Controls.Add(this.ucMiMaZa);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.ForeColor = System.Drawing.Color.White;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = HYL.MountBlue.Resources.Grafika.icoMountBlue;
		base.KeyPreview = true;
		this.MaximumSize = new System.Drawing.Size(2200, 2000);
		this.MinimumSize = new System.Drawing.Size(1001, 738);
		base.Name = "HlavniOkno";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "???";
		base.Load += new System.EventHandler(fHlavniOkno_Load);
		base.MouseUp += new System.Windows.Forms.MouseEventHandler(HlavniOkno_MouseUp);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(fHlavniOkno_MouseDown);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(HlavniOkno_FormClosing);
		base.MouseMove += new System.Windows.Forms.MouseEventHandler(fHlavniOkno_MouseMove);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(HlavniOkno_KeyDown);
		base.ResumeLayout(false);
	}
}
