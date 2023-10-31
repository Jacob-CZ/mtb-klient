using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;

namespace HYL.MountBlue.Controls;

public class KlavesaInfo : _Bublina
{
	private IContainer components;

	private Label lblPopis;

	[Description("Popis klávesy.")]
	[Category("Konfigurace prvku")]
	public string Popis
	{
		get
		{
			return lblPopis.Text;
		}
		set
		{
			lblPopis.Text = value;
		}
	}

	protected override GraphicsPath RegionOkna => SpecialniGrafika.CestaBublinyNovaKlavesa(DisplayRectangle, OblastOkna);

	protected override Point PoziceSpickyBubliny => new Point(DisplayRectangle.Width, DisplayRectangle.Height);

	protected override Rectangle OblastOkna
	{
		get
		{
			Rectangle displayRectangle = DisplayRectangle;
			displayRectangle.Width -= 65;
			displayRectangle.Height -= 130;
			return displayRectangle;
		}
	}

	public KlavesaInfo()
	{
		InitializeComponent();
		base.VyskaZahlavi = 45;
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
		this.lblPopis = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.lblPopis.BackColor = System.Drawing.Color.White;
		this.lblPopis.ForeColor = System.Drawing.Color.Black;
		this.lblPopis.Location = new System.Drawing.Point(7, 48);
		this.lblPopis.Name = "lblPopis";
		this.lblPopis.Size = new System.Drawing.Size(175, 112);
		this.lblPopis.TabIndex = 0;
		this.lblPopis.Text = "???";
		this.lblPopis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.Controls.Add(this.lblPopis);
		this.MaximumSize = new System.Drawing.Size(256, 304);
		this.MinimumSize = new System.Drawing.Size(256, 304);
		base.Name = "KlavesaInfo";
		base.Size = new System.Drawing.Size(256, 304);
		base.ResumeLayout(false);
	}
}
