using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Odmena : _Bublina
{
	private IContainer components;

	private Label lblPochvala;

	private ObrazkoveTlacitko cmdOK;

	private Label lblVymena;

	public Odmena(bool bZobrazitInfoOvymene)
	{
		InitializeComponent();
		Random random = new Random();
		string text = random.Next(6) switch
		{
			0 => Texty.Psani_Odmena_1, 
			1 => Texty.Psani_Odmena_2, 
			2 => Texty.Psani_Odmena_3, 
			3 => Texty.Psani_Odmena_4, 
			4 => Texty.Psani_Odmena_5, 
			_ => Texty.Psani_Odmena_6, 
		};
		Text = Texty.Psani_Odmena_Title;
		lblPochvala.Text = text;
		lblVymena.Text = Texty.Psani_Odmena_vymena;
		lblVymena.Visible = bZobrazitInfoOvymene;
	}

	public static void ZobrazitOdmenu(bool bZobrazitInfoOvymene)
	{
		using Odmena odmena = new Odmena(bZobrazitInfoOvymene);
		odmena.BublinaZobrazitDialog();
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
		this.lblPochvala = new System.Windows.Forms.Label();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblVymena = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.lblPochvala.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblPochvala.Font = new System.Drawing.Font("Arial", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblPochvala.Location = new System.Drawing.Point(5, 44);
		this.lblPochvala.Name = "lblPochvala";
		this.lblPochvala.Size = new System.Drawing.Size(281, 57);
		this.lblPochvala.TabIndex = 1;
		this.lblPochvala.Text = "???";
		this.lblPochvala.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(218, 115);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 0;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.lblVymena.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblVymena.Location = new System.Drawing.Point(12, 106);
		this.lblVymena.Name = "lblVymena";
		this.lblVymena.Size = new System.Drawing.Size(196, 38);
		this.lblVymena.TabIndex = 2;
		this.lblVymena.Text = "???";
		this.lblVymena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		base.ClientSize = new System.Drawing.Size(356, 150);
		base.Controls.Add(this.lblVymena);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.lblPochvala);
		this.ForeColor = System.Drawing.Color.Black;
		base.Name = "Odmena";
		base.VyskaZahlavi = 35;
		base.Controls.SetChildIndex(this.lblPochvala, 0);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.lblVymena, 0);
		base.ResumeLayout(false);
	}
}
