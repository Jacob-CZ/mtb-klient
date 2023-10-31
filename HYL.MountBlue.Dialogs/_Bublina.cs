using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Forms;

namespace HYL.MountBlue.Dialogs;

internal class _Bublina : _Dialog
{
	private readonly Point PoziceUstPruvodce = new Point(890, 475);

	private IContainer components;

	protected override GraphicsPath RegionOkna => SpecialniGrafika.CestaBubliny(DisplayRectangle, OblastOkna);

	protected override bool OknoLzePresouvat => false;

	protected override Rectangle OblastOkna
	{
		get
		{
			Rectangle displayRectangle = DisplayRectangle;
			displayRectangle.Width -= 65;
			return displayRectangle;
		}
	}

	protected virtual Point PoziceSpickyBubliny => new Point(DisplayRectangle.Width, DisplayRectangle.Height - 18);

	internal _Bublina()
	{
		InitializeComponent();
	}

	protected override void NastavitVychoziPozici()
	{
		HlavniOkno hlavniOkno = _Plocha.HlavniOkno;
		Point location = new Point(hlavniOkno.Left + (hlavniOkno.Width - 1000) / 2 + PoziceUstPruvodce.X - PoziceSpickyBubliny.X, hlavniOkno.Top + PoziceUstPruvodce.Y - PoziceSpickyBubliny.Y);
		base.Location = location;
	}

	protected virtual void NastavitVelikostOkna(Size oblastOkna)
	{
		oblastOkna.Width += 65;
		base.Size = oblastOkna;
	}

	internal DialogResult BublinaZobrazitDialog()
	{
		HlavniOkno hlavniOkno = _Plocha.HlavniOkno;
		return ShowDialog(hlavniOkno);
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
		base.SuspendLayout();
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(368, 191);
		this.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(250, 150);
		base.Name = "_Bublina";
		base.Opacity = 1.0;
		this.Text = "???";
		base.ResumeLayout(false);
	}
}
