using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

public class Sezeni : _Bublina
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

	protected override GraphicsPath RegionOkna => SpecialniGrafika.CestaBublinySezeni(DisplayRectangle, OblastOkna);

	protected override Point PoziceSpickyBubliny => new Point(DisplayRectangle.Width, 0);

	protected override Rectangle OblastOkna
	{
		get
		{
			Rectangle displayRectangle = DisplayRectangle;
			displayRectangle.Width -= 81;
			displayRectangle.Height = 102;
			displayRectangle.Location = new Point(displayRectangle.Left, displayRectangle.Top + 135);
			return displayRectangle;
		}
	}

	public Sezeni()
	{
		InitializeComponent();
		Text = Texty.Sezeni_Title;
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
		this.lblPopis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblPopis.BackColor = System.Drawing.Color.White;
		this.lblPopis.ForeColor = System.Drawing.Color.Black;
		this.lblPopis.Location = new System.Drawing.Point(7, 173);
		this.lblPopis.Name = "lblPopis";
		this.lblPopis.Size = new System.Drawing.Size(318, 55);
		this.lblPopis.TabIndex = 0;
		this.lblPopis.Text = string.Empty;
		this.lblPopis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.Controls.Add(this.lblPopis);
		this.MaximumSize = new System.Drawing.Size(417, 237);
		this.MinimumSize = new System.Drawing.Size(417, 237);
		base.Name = "Sezeni";
		base.Size = new System.Drawing.Size(417, 237);
		base.ResumeLayout(false);
	}
}
