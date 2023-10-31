using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klavesnice;
using HYL.MountBlue.Classes.Lekce;

namespace HYL.MountBlue.Controls;

internal class Klavesnice : UserControl
{
	internal delegate void KliknutoNaKlavesuDeleg(string nadpis, string popis);

	internal delegate void KliknutoMimoDeleg();

	private HYL.MountBlue.Classes.Klavesnice.Klavesnice mobjKlavesnice;

	private int mintVelkyKrok;

	private int mintMalyKrok;

	private GraphicsPath gpthCestaZnamychKlaves;

	private IContainer components;

	private Timer tmrAnimaceVelka;

	private Timer tmrAnimaceMala;

	private ToolTip ttt;

	internal event KliknutoNaKlavesuDeleg KliknutoNaKlavesu;

	internal event KliknutoMimoDeleg KliknutoMimo;

	internal Klavesnice(HYL.MountBlue.Classes.Klavesnice.Klavesnice oKlavesnice)
	{
		InitializeComponent();
		mobjKlavesnice = oKlavesnice;
		mobjKlavesnice.VytvoritPodkladovyObrazek();
		gpthCestaZnamychKlaves = mobjKlavesnice.VytvoritCestuZnamychKlaves();
		mintVelkyKrok = 0;
		mintMalyKrok = 0;
		ToolTipText.NastavitToolTipText(ttt);
		StartAnimace();
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, value: true);
	}

	private void Klavesnice_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		mobjKlavesnice.VykreslitKlavesnici(e.Graphics, mintVelkyKrok, mintMalyKrok);
	}

	private void tmrAnimaceVelka_Tick(object sender, EventArgs e)
	{
		tmrAnimaceVelka.Stop();
		if (mobjKlavesnice.MaMalouAnimaci(++mintVelkyKrok))
		{
			mintMalyKrok = 0;
			Invalidate();
			tmrAnimaceMala.Start();
		}
		else
		{
			Invalidate();
			tmrAnimaceVelka.Start();
		}
	}

	private void tmrAnimaceMala_Tick(object sender, EventArgs e)
	{
		tmrAnimaceMala.Stop();
		Invalidate();
		if (++mintMalyKrok > 3)
		{
			mintMalyKrok = 0;
			mintVelkyKrok++;
			tmrAnimaceVelka.Start();
		}
		else
		{
			tmrAnimaceMala.Start();
		}
	}

	internal void StartAnimace()
	{
		if (mobjKlavesnice.DelkaAnimace > 1)
		{
			tmrAnimaceVelka.Start();
		}
	}

	internal void StopAnimace()
	{
		tmrAnimaceVelka.Stop();
	}

	private void Klavesnice_MouseMove(object sender, MouseEventArgs e)
	{
		if (gpthCestaZnamychKlaves != null)
		{
			if (gpthCestaZnamychKlaves.IsVisible(e.Location))
			{
				Cursor = Cursors.Hand;
			}
			else
			{
				Cursor = Cursors.Default;
			}
		}
	}

	private void Klavesnice_MouseDown(object sender, MouseEventArgs e)
	{
		if (gpthCestaZnamychKlaves != null && gpthCestaZnamychKlaves.IsVisible(e.Location))
		{
			if (this.KliknutoNaKlavesu == null)
			{
				return;
			}
			Point location = e.Location;
			{
				foreach (KlavesaKlavesnice item in (IEnumerable)mobjKlavesnice)
				{
					if (item.OblastVobrazku.Contains(location))
					{
						if (_Lekce.Lekce().Klavesy.NajitNadpisApopis(item.Klavesa, out var sNadpis, out var sPopis))
						{
							this.KliknutoNaKlavesu(sNadpis, sPopis);
						}
						if (this.KliknutoMimo != null)
						{
							this.KliknutoMimo();
						}
						break;
					}
				}
				return;
			}
		}
		if (this.KliknutoMimo != null)
		{
			this.KliknutoMimo();
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
		this.components = new System.ComponentModel.Container();
		this.tmrAnimaceVelka = new System.Windows.Forms.Timer(this.components);
		this.tmrAnimaceMala = new System.Windows.Forms.Timer(this.components);
		this.ttt = new System.Windows.Forms.ToolTip(this.components);
		base.SuspendLayout();
		this.tmrAnimaceVelka.Interval = 900;
		this.tmrAnimaceVelka.Tick += new System.EventHandler(tmrAnimaceVelka_Tick);
		this.tmrAnimaceMala.Tick += new System.EventHandler(tmrAnimaceMala_Tick);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.MaximumSize = new System.Drawing.Size(550, 322);
		this.MinimumSize = new System.Drawing.Size(550, 322);
		base.Name = "Klavesnice";
		base.Size = new System.Drawing.Size(550, 322);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(Klavesnice_MouseDown);
		base.MouseMove += new System.Windows.Forms.MouseEventHandler(Klavesnice_MouseMove);
		base.Paint += new System.Windows.Forms.PaintEventHandler(Klavesnice_Paint);
		base.ResumeLayout(false);
	}
}
