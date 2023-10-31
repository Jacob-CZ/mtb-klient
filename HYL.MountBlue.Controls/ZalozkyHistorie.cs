using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

public class ZalozkyHistorie : UserControl
{
	public enum eZalozky : byte
	{
		Historie,
		Klasifikace
	}

	public delegate void ZmenaZalozkyDeleg(eZalozky novaZalozka);

	private eZalozky aktivniZalozka;

	private IContainer components;

	private ObrazkoveTlacitko cmdZalozkaHistorie;

	private ObrazkoveTlacitko cmdZalozkaKlasifikace;

	public eZalozky AktivniZalozka
	{
		get
		{
			return aktivniZalozka;
		}
		set
		{
			aktivniZalozka = value;
			switch (aktivniZalozka)
			{
			case eZalozky.Historie:
				cmdZalozkaHistorie.Enabled = false;
				cmdZalozkaKlasifikace.Enabled = true;
				break;
			case eZalozky.Klasifikace:
				cmdZalozkaHistorie.Enabled = true;
				cmdZalozkaKlasifikace.Enabled = false;
				break;
			}
			if (this.ZmenaZalozky != null)
			{
				this.ZmenaZalozky(aktivniZalozka);
			}
		}
	}

	public event ZmenaZalozkyDeleg ZmenaZalozky;

	public ZalozkyHistorie()
	{
		InitializeComponent();
	}

	public void NastavitDalsiZalozku()
	{
		switch (aktivniZalozka)
		{
		case eZalozky.Historie:
			AktivniZalozka = eZalozky.Klasifikace;
			break;
		case eZalozky.Klasifikace:
			AktivniZalozka = eZalozky.Historie;
			break;
		}
	}

	private void cmdZalozkaHistorie_TlacitkoStisknuto()
	{
		AktivniZalozka = eZalozky.Historie;
	}

	private void cmdZalozkaKlasifikace_TlacitkoStisknuto()
	{
		AktivniZalozka = eZalozky.Klasifikace;
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
		this.cmdZalozkaHistorie = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdZalozkaKlasifikace = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		base.SuspendLayout();
		this.cmdZalozkaHistorie.BackColor = System.Drawing.Color.White;
		this.cmdZalozkaHistorie.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZalozkaHistorie.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZalozkaHistorie.ForeColor = System.Drawing.Color.White;
		this.cmdZalozkaHistorie.Location = new System.Drawing.Point(0, 0);
		this.cmdZalozkaHistorie.Name = "cmdZalozkaHistorie";
		this.cmdZalozkaHistorie.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalHistorie1N;
		this.cmdZalozkaHistorie.Size = new System.Drawing.Size(88, 26);
		this.cmdZalozkaHistorie.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalHistorie1D;
		this.cmdZalozkaHistorie.TabIndex = 0;
		this.cmdZalozkaHistorie.ZakazanyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalHistorie1Z;
		this.cmdZalozkaHistorie.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalHistorie1H;
		this.cmdZalozkaHistorie.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZalozkaHistorie_TlacitkoStisknuto);
		this.cmdZalozkaKlasifikace.BackColor = System.Drawing.Color.White;
		this.cmdZalozkaKlasifikace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZalozkaKlasifikace.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZalozkaKlasifikace.ForeColor = System.Drawing.Color.White;
		this.cmdZalozkaKlasifikace.Location = new System.Drawing.Point(89, 0);
		this.cmdZalozkaKlasifikace.Name = "cmdZalozkaKlasifikace";
		this.cmdZalozkaKlasifikace.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalHistorie2N;
		this.cmdZalozkaKlasifikace.Size = new System.Drawing.Size(88, 26);
		this.cmdZalozkaKlasifikace.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalHistorie2D;
		this.cmdZalozkaKlasifikace.TabIndex = 1;
		this.cmdZalozkaKlasifikace.ZakazanyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalHistorie2Z;
		this.cmdZalozkaKlasifikace.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalHistorie2H;
		this.cmdZalozkaKlasifikace.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZalozkaKlasifikace_TlacitkoStisknuto);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.cmdZalozkaKlasifikace);
		base.Controls.Add(this.cmdZalozkaHistorie);
		this.MaximumSize = new System.Drawing.Size(176, 26);
		this.MinimumSize = new System.Drawing.Size(176, 26);
		base.Name = "ZalozkyHistorie";
		base.Size = new System.Drawing.Size(176, 26);
		base.ResumeLayout(false);
	}
}
