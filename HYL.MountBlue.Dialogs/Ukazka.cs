using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Ukazka : _Bublina
{
	internal delegate void KonecUkazky();

	internal delegate void PrehratUkazku(KonecUkazky konec);

	private enum eStavTlacitka
	{
		jesteNespustene,
		prehravaSe,
		skonciloPrehravani
	}

	private PrehratUkazku spustitUkazku;

	private bool bPrehratAutomaticky;

	private IContainer components;

	private ObrazkoveTlacitko cmdZavritUkazku;

	private ObrazkoveTlacitko cmdPrehratUkazku;

	private ObrazkoveTlacitko cmdZopakovatUkazku;

	private Label lblInfo;

	private Timer tmrZpruhlednit;

	public Ukazka(Psani psani, PrehratUkazku ukazka)
	{
		InitializeComponent();
		spustitUkazku = ukazka;
		NastavitTlacitka(eStavTlacitka.jesteNespustene);
		Text = psani.ZpusobToTitleUkazka();
		lblInfo.Text = HYL.MountBlue.Classes.Text.TextNBSP(psani.ZpusobToInfoUkazka());
		bPrehratAutomaticky = psani.ZobrazitUkazku;
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		if (bPrehratAutomaticky)
		{
			cmdPrehratUkazku_TlacitkoStisknuto();
		}
	}

	private void Ukazka_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && cmdPrehratUkazku.Visible && cmdPrehratUkazku.Enabled)
		{
			cmdPrehratUkazku_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.Return && cmdZopakovatUkazku.Visible && cmdZopakovatUkazku.Enabled)
		{
			cmdZopakovatUkazku_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.Escape && cmdZavritUkazku.Visible && cmdZavritUkazku.Enabled)
		{
			cmdZavritUkazku_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
	}

	private void cmdPrehratUkazku_TlacitkoStisknuto()
	{
		NastavitTlacitka(eStavTlacitka.prehravaSe);
		spustitUkazku(PrehravaniSkoncilo);
	}

	private void cmdZopakovatUkazku_TlacitkoStisknuto()
	{
		NastavitTlacitka(eStavTlacitka.prehravaSe);
		spustitUkazku(PrehravaniSkoncilo);
	}

	internal void PrehravaniSkoncilo()
	{
		NastavitTlacitka(eStavTlacitka.skonciloPrehravani);
	}

	private void cmdZavritUkazku_TlacitkoStisknuto()
	{
		Close();
	}

	private void NastavitTlacitka(eStavTlacitka stav)
	{
		switch (stav)
		{
		case eStavTlacitka.jesteNespustene:
			cmdPrehratUkazku.Visible = true;
			cmdPrehratUkazku.Enabled = true;
			cmdZopakovatUkazku.Visible = false;
			cmdPrehratUkazku.Focus();
			tmrZpruhlednit.Stop();
			break;
		case eStavTlacitka.prehravaSe:
			cmdPrehratUkazku.Visible = false;
			cmdZopakovatUkazku.Visible = true;
			cmdZopakovatUkazku.Enabled = false;
			cmdZavritUkazku.Focus();
			tmrZpruhlednit.Start();
			break;
		case eStavTlacitka.skonciloPrehravani:
			cmdPrehratUkazku.Visible = false;
			cmdZopakovatUkazku.Visible = true;
			cmdZopakovatUkazku.Enabled = true;
			cmdZopakovatUkazku.Focus();
			tmrZpruhlednit.Stop();
			if (base.Opacity == 0.6499999761581421)
			{
				SpecialniGrafika.FadeIn(this, 0.15f, 0.65f, 0.999f);
			}
			break;
		}
	}

	public static void ZobrazitUkazku(Psani psani, PrehratUkazku ukazka)
	{
		using Ukazka ukazka2 = new Ukazka(psani, ukazka);
		ukazka2.BublinaZobrazitDialog();
	}

	private void tmrZpruhlednit_Tick(object sender, EventArgs e)
	{
		tmrZpruhlednit.Stop();
		SpecialniGrafika.FadeOut(this, 0.15f, 0.65f, 0.999f);
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
		this.cmdZavritUkazku = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdPrehratUkazku = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdZopakovatUkazku = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblInfo = new System.Windows.Forms.Label();
		this.tmrZpruhlednit = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.cmdZavritUkazku.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdZavritUkazku.BackColor = System.Drawing.Color.White;
		this.cmdZavritUkazku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZavritUkazku.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZavritUkazku.ForeColor = System.Drawing.Color.White;
		this.cmdZavritUkazku.Location = new System.Drawing.Point(193, 115);
		this.cmdZavritUkazku.Name = "cmdZavritUkazku";
		this.cmdZavritUkazku.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavUkazN;
		this.cmdZavritUkazku.Size = new System.Drawing.Size(134, 23);
		this.cmdZavritUkazku.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavUkazD;
		this.cmdZavritUkazku.TabIndex = 3;
		this.cmdZavritUkazku.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavUkazZ;
		this.cmdZavritUkazku.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavUkazH;
		this.cmdZavritUkazku.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZavritUkazku_TlacitkoStisknuto);
		this.cmdPrehratUkazku.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdPrehratUkazku.BackColor = System.Drawing.Color.White;
		this.cmdPrehratUkazku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdPrehratUkazku.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdPrehratUkazku.ForeColor = System.Drawing.Color.White;
		this.cmdPrehratUkazku.Location = new System.Drawing.Point(12, 115);
		this.cmdPrehratUkazku.Name = "cmdPrehratUkazku";
		this.cmdPrehratUkazku.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZobrUkazN;
		this.cmdPrehratUkazku.Size = new System.Drawing.Size(170, 23);
		this.cmdPrehratUkazku.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZobrUkazD;
		this.cmdPrehratUkazku.TabIndex = 1;
		this.cmdPrehratUkazku.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZobrUkazZ;
		this.cmdPrehratUkazku.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZobrUkazH;
		this.cmdPrehratUkazku.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdPrehratUkazku_TlacitkoStisknuto);
		this.cmdZopakovatUkazku.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdZopakovatUkazku.BackColor = System.Drawing.Color.White;
		this.cmdZopakovatUkazku.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZopakovatUkazku.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZopakovatUkazku.ForeColor = System.Drawing.Color.White;
		this.cmdZopakovatUkazku.Location = new System.Drawing.Point(12, 115);
		this.cmdZopakovatUkazku.Name = "cmdZopakovatUkazku";
		this.cmdZopakovatUkazku.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZopUkazN;
		this.cmdZopakovatUkazku.Size = new System.Drawing.Size(170, 23);
		this.cmdZopakovatUkazku.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZopUkazD;
		this.cmdZopakovatUkazku.TabIndex = 2;
		this.cmdZopakovatUkazku.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZopUkazZ;
		this.cmdZopakovatUkazku.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZopUkazH;
		this.cmdZopakovatUkazku.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZopakovatUkazku_TlacitkoStisknuto);
		this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo.Location = new System.Drawing.Point(12, 47);
		this.lblInfo.Name = "lblInfo";
		this.lblInfo.Size = new System.Drawing.Size(315, 65);
		this.lblInfo.TabIndex = 0;
		this.lblInfo.Text = "???";
		this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.tmrZpruhlednit.Interval = 10000;
		this.tmrZpruhlednit.Tick += new System.EventHandler(tmrZpruhlednit_Tick);
		base.ClientSize = new System.Drawing.Size(407, 150);
		base.Controls.Add(this.lblInfo);
		base.Controls.Add(this.cmdZavritUkazku);
		base.Controls.Add(this.cmdZopakovatUkazku);
		base.Controls.Add(this.cmdPrehratUkazku);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "Ukazka";
		base.Opacity = 0.0;
		base.VyskaZahlavi = 40;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Ukazka_KeyDown);
		base.Controls.SetChildIndex(this.cmdPrehratUkazku, 0);
		base.Controls.SetChildIndex(this.cmdZopakovatUkazku, 0);
		base.Controls.SetChildIndex(this.cmdZavritUkazku, 0);
		base.Controls.SetChildIndex(this.lblInfo, 0);
		base.ResumeLayout(false);
	}
}
