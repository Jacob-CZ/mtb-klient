using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

public class Vysledky : _Bublina
{
	private bool zobrazitCisteUhozy;

	private IContainer components;

	private Label lblChyby;

	private Label lblChybovost;

	private Label lblUhozyHrube;

	private Label lblUhozyCiste;

	private Label lblUhozyCisteL;

	private Label lblUhozyHrubeL;

	private Label lblChybovostL;

	private Label lblChybyL;

	public bool ZobrazitCisteUhozy
	{
		get
		{
			return zobrazitCisteUhozy;
		}
		set
		{
			zobrazitCisteUhozy = value;
		}
	}

	protected override GraphicsPath RegionOkna => SpecialniGrafika.CestaBublinyVysledky(DisplayRectangle, OblastOkna);

	protected override Point PoziceSpickyBubliny => new Point(83, DisplayRectangle.Height);

	protected override Rectangle OblastOkna
	{
		get
		{
			Rectangle displayRectangle = DisplayRectangle;
			displayRectangle.Height -= 40;
			return displayRectangle;
		}
	}

	public Vysledky(bool zobrazitCisteUhozy)
	{
		ZobrazitCisteUhozy = zobrazitCisteUhozy;
		InitializeComponent();
		base.VyskaZahlavi = 30;
		Text = Texty.Vysledky_Title;
		lblChybyL.Text = Texty.Vysledky_lblChyby;
		lblChybovostL.Text = Texty.Vysledky_lblChybovost;
		lblUhozyHrubeL.Text = Texty.Vysledky_lblUhozyHrube;
		lblUhozyCisteL.Text = Texty.Vysledky_lblUhozyCiste;
		lblUhozyHrube.Visible = !zobrazitCisteUhozy;
		lblUhozyHrubeL.Visible = !zobrazitCisteUhozy;
		lblUhozyCiste.Visible = zobrazitCisteUhozy;
		lblUhozyCisteL.Visible = zobrazitCisteUhozy;
	}

	internal void NastavitHodnotyVysledku(int iChyby, float fChybovost, int iHrubeUhozy)
	{
		lblChyby.Text = iChyby.ToString("# ##0");
		if (fChybovost >= 10f)
		{
			lblChybovost.Text = fChybovost.ToString("0") + " %";
		}
		else
		{
			lblChybovost.Text = fChybovost.ToString("0.00") + " %";
		}
		lblUhozyHrube.Text = iHrubeUhozy.ToString("# ##0");
	}

	internal void NastavitHodnotyVysledku(int iChyby, float fChybovost, int iHrubeUhozy, int iCisteUhozy)
	{
		NastavitHodnotyVysledku(iChyby, fChybovost, iHrubeUhozy);
		lblUhozyCiste.Text = iCisteUhozy.ToString("# ##0");
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
		this.lblChyby = new System.Windows.Forms.Label();
		this.lblChybovost = new System.Windows.Forms.Label();
		this.lblUhozyHrube = new System.Windows.Forms.Label();
		this.lblUhozyCiste = new System.Windows.Forms.Label();
		this.lblUhozyCisteL = new System.Windows.Forms.Label();
		this.lblUhozyHrubeL = new System.Windows.Forms.Label();
		this.lblChybovostL = new System.Windows.Forms.Label();
		this.lblChybyL = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.lblChyby.ForeColor = System.Drawing.Color.Black;
		this.lblChyby.Location = new System.Drawing.Point(108, 33);
		this.lblChyby.Name = "lblChyby";
		this.lblChyby.Size = new System.Drawing.Size(51, 23);
		this.lblChyby.TabIndex = 0;
		this.lblChyby.Text = "???";
		this.lblChyby.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblChybovost.ForeColor = System.Drawing.Color.Black;
		this.lblChybovost.Location = new System.Drawing.Point(108, 55);
		this.lblChybovost.Name = "lblChybovost";
		this.lblChybovost.Size = new System.Drawing.Size(51, 23);
		this.lblChybovost.TabIndex = 1;
		this.lblChybovost.Text = "???";
		this.lblChybovost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblUhozyHrube.ForeColor = System.Drawing.Color.Black;
		this.lblUhozyHrube.Location = new System.Drawing.Point(108, 77);
		this.lblUhozyHrube.Name = "lblUhozyHrube";
		this.lblUhozyHrube.Size = new System.Drawing.Size(51, 23);
		this.lblUhozyHrube.TabIndex = 2;
		this.lblUhozyHrube.Text = "???";
		this.lblUhozyHrube.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblUhozyCiste.ForeColor = System.Drawing.Color.Black;
		this.lblUhozyCiste.Location = new System.Drawing.Point(108, 77);
		this.lblUhozyCiste.Name = "lblUhozyCiste";
		this.lblUhozyCiste.Size = new System.Drawing.Size(51, 23);
		this.lblUhozyCiste.TabIndex = 3;
		this.lblUhozyCiste.Text = "???";
		this.lblUhozyCiste.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblUhozyCiste.Visible = false;
		this.lblUhozyCisteL.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblUhozyCisteL.ForeColor = System.Drawing.Color.Black;
		this.lblUhozyCisteL.Location = new System.Drawing.Point(11, 77);
		this.lblUhozyCisteL.Name = "lblUhozyCisteL";
		this.lblUhozyCisteL.Size = new System.Drawing.Size(93, 23);
		this.lblUhozyCisteL.TabIndex = 7;
		this.lblUhozyCisteL.Text = "???";
		this.lblUhozyCisteL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblUhozyCisteL.Visible = false;
		this.lblUhozyHrubeL.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblUhozyHrubeL.ForeColor = System.Drawing.Color.Black;
		this.lblUhozyHrubeL.Location = new System.Drawing.Point(11, 77);
		this.lblUhozyHrubeL.Name = "lblUhozyHrubeL";
		this.lblUhozyHrubeL.Size = new System.Drawing.Size(93, 23);
		this.lblUhozyHrubeL.TabIndex = 6;
		this.lblUhozyHrubeL.Text = "???";
		this.lblUhozyHrubeL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblChybovostL.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblChybovostL.ForeColor = System.Drawing.Color.Black;
		this.lblChybovostL.Location = new System.Drawing.Point(11, 55);
		this.lblChybovostL.Name = "lblChybovostL";
		this.lblChybovostL.Size = new System.Drawing.Size(93, 23);
		this.lblChybovostL.TabIndex = 5;
		this.lblChybovostL.Text = "???";
		this.lblChybovostL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblChybyL.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblChybyL.ForeColor = System.Drawing.Color.Black;
		this.lblChybyL.Location = new System.Drawing.Point(11, 33);
		this.lblChybyL.Name = "lblChybyL";
		this.lblChybyL.Size = new System.Drawing.Size(93, 23);
		this.lblChybyL.TabIndex = 4;
		this.lblChybyL.Text = "???";
		this.lblChybyL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		base.Controls.Add(this.lblUhozyCisteL);
		base.Controls.Add(this.lblUhozyHrubeL);
		base.Controls.Add(this.lblChybovostL);
		base.Controls.Add(this.lblChybyL);
		base.Controls.Add(this.lblUhozyCiste);
		base.Controls.Add(this.lblUhozyHrube);
		base.Controls.Add(this.lblChybovost);
		base.Controls.Add(this.lblChyby);
		this.MaximumSize = new System.Drawing.Size(180, 160);
		this.MinimumSize = new System.Drawing.Size(180, 140);
		base.Name = "Vysledky";
		base.Size = new System.Drawing.Size(180, 140);
		base.ResumeLayout(false);
	}
}
