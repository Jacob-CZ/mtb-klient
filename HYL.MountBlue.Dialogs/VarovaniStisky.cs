using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class VarovaniStisky : _Bublina
{
	internal const int OdpocetVterin = 4;

	private int iPocetVterin;

	private IContainer components;

	private Label lblInfo;

	private ObrazkoveTlacitko cmdOK;

	private Label lblOdpocet;

	private Timer tmrOdpocet;

	public VarovaniStisky(bool bBudeZacinatOdZacatku, bool bDosazenoMaxDelky)
	{
		InitializeComponent();
		Text = Texty.VarovaniStisky_Title;
		if (bDosazenoMaxDelky)
		{
			if (bBudeZacinatOdZacatku)
			{
				lblInfo.Text = Texty.VarovaniStisky_MaxDelka_1;
			}
			else
			{
				lblInfo.Text = Texty.VarovaniStisky_MaxDelka_0;
			}
		}
		else if (bBudeZacinatOdZacatku)
		{
			lblInfo.Text = Texty.VarovaniStisky_StejnaKlavesa_1;
		}
		else
		{
			lblInfo.Text = Texty.VarovaniStisky_StejnaKlavesa_0;
		}
		iPocetVterin = 4;
		tmrOdpocet.Start();
		NastavitOdpocet();
	}

	private void NastavitOdpocet()
	{
		lblOdpocet.Text = iPocetVterin.ToString();
		if (iPocetVterin == 0)
		{
			lblOdpocet.Visible = false;
			tmrOdpocet.Stop();
			cmdOK.Enabled = true;
			cmdOK.Focus();
		}
	}

	private void tmrOdpocet_Tick(object sender, EventArgs e)
	{
		iPocetVterin--;
		NastavitOdpocet();
	}

	internal static void ZobrazitDialog(bool bBudeZacinatOdZacatku, bool bDosazenoMaxDelky)
	{
		using VarovaniStisky varovaniStisky = new VarovaniStisky(bBudeZacinatOdZacatku, bDosazenoMaxDelky);
		varovaniStisky.ShowDialog(_Plocha.HlavniOkno);
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		Close();
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
		this.lblInfo = new System.Windows.Forms.Label();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblOdpocet = new System.Windows.Forms.Label();
		this.tmrOdpocet = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo.ForeColor = System.Drawing.Color.Black;
		this.lblInfo.Location = new System.Drawing.Point(12, 53);
		this.lblInfo.Name = "lblInfo";
		this.lblInfo.Size = new System.Drawing.Size(267, 104);
		this.lblInfo.TabIndex = 2;
		this.lblInfo.Text = "???";
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Enabled = false;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(219, 160);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 0;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.lblOdpocet.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblOdpocet.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblOdpocet.ForeColor = System.Drawing.Color.Silver;
		this.lblOdpocet.Location = new System.Drawing.Point(185, 160);
		this.lblOdpocet.Name = "lblOdpocet";
		this.lblOdpocet.Size = new System.Drawing.Size(28, 23);
		this.lblOdpocet.TabIndex = 1;
		this.lblOdpocet.Text = "?";
		this.lblOdpocet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.tmrOdpocet.Interval = 1000;
		this.tmrOdpocet.Tick += new System.EventHandler(tmrOdpocet_Tick);
		base.ClientSize = new System.Drawing.Size(356, 195);
		base.Controls.Add(this.lblOdpocet);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.lblInfo);
		base.Name = "VarovaniStisky";
		base.Opacity = 0.0;
		base.VyskaZahlavi = 40;
		base.Controls.SetChildIndex(this.lblInfo, 0);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.lblOdpocet, 0);
		base.ResumeLayout(false);
	}
}
