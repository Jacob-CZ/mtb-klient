using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

public class ZalozkyTrida : UserControl
{
	public enum eZalozky : byte
	{
		SeznamStudentu,
		Klasifikace
	}

	public delegate void ZmenaZalozkyDeleg(eZalozky novaZalozka);

	private eZalozky aktivniZalozka;

	private IContainer components;

	private ObrazkoveTlacitko cmdZalozkaSeznam;

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
			case eZalozky.SeznamStudentu:
				cmdZalozkaSeznam.Enabled = false;
				cmdZalozkaKlasifikace.Enabled = true;
				break;
			case eZalozky.Klasifikace:
				cmdZalozkaSeznam.Enabled = true;
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

	public ZalozkyTrida()
	{
		InitializeComponent();
	}

	public void NastavitDalsiZalozku()
	{
		switch (aktivniZalozka)
		{
		case eZalozky.SeznamStudentu:
			AktivniZalozka = eZalozky.Klasifikace;
			break;
		case eZalozky.Klasifikace:
			AktivniZalozka = eZalozky.SeznamStudentu;
			break;
		}
	}

	private void cmdZalozkaSeznam_TlacitkoStisknuto()
	{
		AktivniZalozka = eZalozky.SeznamStudentu;
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
		this.cmdZalozkaSeznam = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdZalozkaKlasifikace = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		base.SuspendLayout();
		this.cmdZalozkaSeznam.BackColor = System.Drawing.Color.White;
		this.cmdZalozkaSeznam.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZalozkaSeznam.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZalozkaSeznam.ForeColor = System.Drawing.Color.White;
		this.cmdZalozkaSeznam.Location = new System.Drawing.Point(0, 0);
		this.cmdZalozkaSeznam.Name = "cmdZalozkaSeznam";
		this.cmdZalozkaSeznam.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalozka1N;
		this.cmdZalozkaSeznam.Size = new System.Drawing.Size(139, 29);
		this.cmdZalozkaSeznam.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalozka1D;
		this.cmdZalozkaSeznam.TabIndex = 0;
		this.cmdZalozkaSeznam.ZakazanyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalozka1Z;
		this.cmdZalozkaSeznam.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalozka1H;
		this.cmdZalozkaSeznam.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZalozkaSeznam_TlacitkoStisknuto);
		this.cmdZalozkaKlasifikace.BackColor = System.Drawing.Color.White;
		this.cmdZalozkaKlasifikace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZalozkaKlasifikace.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZalozkaKlasifikace.ForeColor = System.Drawing.Color.White;
		this.cmdZalozkaKlasifikace.Location = new System.Drawing.Point(140, 0);
		this.cmdZalozkaKlasifikace.Name = "cmdZalozkaKlasifikace";
		this.cmdZalozkaKlasifikace.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalozka2N;
		this.cmdZalozkaKlasifikace.Size = new System.Drawing.Size(119, 29);
		this.cmdZalozkaKlasifikace.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalozka2D;
		this.cmdZalozkaKlasifikace.TabIndex = 1;
		this.cmdZalozkaKlasifikace.ZakazanyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalozka2Z;
		this.cmdZalozkaKlasifikace.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngZalozka2H;
		this.cmdZalozkaKlasifikace.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZalozkaKlasifikace_TlacitkoStisknuto);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.cmdZalozkaKlasifikace);
		base.Controls.Add(this.cmdZalozkaSeznam);
		this.MaximumSize = new System.Drawing.Size(259, 29);
		this.MinimumSize = new System.Drawing.Size(259, 29);
		base.Name = "ZalozkyTrida";
		base.Size = new System.Drawing.Size(259, 29);
		base.ResumeLayout(false);
	}
}
