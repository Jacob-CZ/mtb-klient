using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

internal class MinMaxZavrit : UserControl
{
	public delegate void TlacitkoStisknuto();

	private IContainer components;

	private ObrazkoveTlacitko cmdMin;

	private ObrazkoveTlacitko cmdMax;

	private ObrazkoveTlacitko cmdZavrit;

	public event TlacitkoStisknuto TlacitkoMinimalizovat;

	public event TlacitkoStisknuto TlacitkoMaximalizovat;

	public event TlacitkoStisknuto TlacitkoZavrit;

	public MinMaxZavrit()
	{
		InitializeComponent();
		NastavitRegion();
	}

	private void cmdMin_TlacitkoStisknuto()
	{
		if (this.TlacitkoMinimalizovat != null)
		{
			this.TlacitkoMinimalizovat();
		}
	}

	private void cmdMax_TlacitkoStisknuto()
	{
		if (this.TlacitkoMaximalizovat != null)
		{
			this.TlacitkoMaximalizovat();
		}
	}

	private void cmdZavrit_TlacitkoStisknuto()
	{
		if (this.TlacitkoZavrit != null)
		{
			this.TlacitkoZavrit();
		}
	}

	private void NastavitRegion()
	{
		GraphicsPath path = SpecialniGrafika.CestaZaoblenehoObdelniku(DisplayRectangle, 8);
		base.Region = new Region(path);
	}

	protected override void OnEnabledChanged(EventArgs e)
	{
		base.OnEnabledChanged(e);
		cmdMin.Enabled = base.Enabled;
		cmdMax.Enabled = base.Enabled;
		cmdZavrit.Enabled = base.Enabled;
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
		this.cmdMax = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdMin = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		base.SuspendLayout();
		this.cmdZavrit.BackColor = HYL.MountBlue.Classes.Grafika.Barva.PozadiTlacitekMinMaxZavrit;
		this.cmdZavrit.Cursor = System.Windows.Forms.Cursors.Arrow;
		this.cmdZavrit.Location = new System.Drawing.Point(74, 5);
		this.cmdZavrit.Name = "cmdZavrit";
		this.cmdZavrit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavN;
		this.cmdZavrit.Size = new System.Drawing.Size(24, 24);
		this.cmdZavrit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavD;
		this.cmdZavrit.TabIndex = 52;
		this.cmdZavrit.TabStop = false;
		this.cmdZavrit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavZ;
		this.cmdZavrit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavH;
		this.cmdZavrit.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZavrit_TlacitkoStisknuto);
		this.cmdMax.BackColor = HYL.MountBlue.Classes.Grafika.Barva.PozadiTlacitekMinMaxZavrit;
		this.cmdMax.Cursor = System.Windows.Forms.Cursors.Arrow;
		this.cmdMax.Location = new System.Drawing.Point(42, 5);
		this.cmdMax.Name = "cmdMax";
		this.cmdMax.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlMaxN;
		this.cmdMax.Size = new System.Drawing.Size(24, 24);
		this.cmdMax.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlMaxD;
		this.cmdMax.TabIndex = 51;
		this.cmdMax.TabStop = false;
		this.cmdMax.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlMaxZ;
		this.cmdMax.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlMaxH;
		this.cmdMax.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdMax_TlacitkoStisknuto);
		this.cmdMin.BackColor = HYL.MountBlue.Classes.Grafika.Barva.PozadiTlacitekMinMaxZavrit;
		this.cmdMin.Cursor = System.Windows.Forms.Cursors.Arrow;
		this.cmdMin.Location = new System.Drawing.Point(10, 5);
		this.cmdMin.Name = "cmdMin";
		this.cmdMin.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlMinN;
		this.cmdMin.Size = new System.Drawing.Size(24, 24);
		this.cmdMin.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlMinD;
		this.cmdMin.TabIndex = 50;
		this.cmdMin.TabStop = false;
		this.cmdMin.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlMinZ;
		this.cmdMin.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlMinH;
		this.cmdMin.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdMin_TlacitkoStisknuto);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = HYL.MountBlue.Classes.Grafika.Barva.PozadiTlacitekMinMaxZavrit;
		base.Controls.Add(this.cmdZavrit);
		base.Controls.Add(this.cmdMax);
		base.Controls.Add(this.cmdMin);
		this.Cursor = System.Windows.Forms.Cursors.Arrow;
		this.MaximumSize = new System.Drawing.Size(106, 33);
		this.MinimumSize = new System.Drawing.Size(106, 33);
		base.Name = "MinMaxZavrit";
		base.Size = new System.Drawing.Size(106, 33);
		base.ResumeLayout(false);
	}
}
