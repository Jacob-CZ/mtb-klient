using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;

namespace HYL.MountBlue.Controls;

public class Info : _Bublina
{
	private const int Okraj = 20;

	private AutoSizeLabel asl;

	private IContainer components;

	public string Informace
	{
		get
		{
			if (asl == null)
			{
				return "";
			}
			return asl.Text;
		}
	}

	public Info()
	{
		InitializeComponent();
	}

	internal void NastavitText(string text)
	{
		asl = new AutoSizeLabel(CreateGraphics(), text, new Point(20, 20), Font, new StringFormat(), ForeColor, MinimumSize.Width - 40);
	}

	internal Size DoporucenaVelikost()
	{
		if (asl == null)
		{
			return base.Size;
		}
		Size oblastOkna = asl.Velikost.ToSize();
		oblastOkna.Width += 40;
		oblastOkna.Height += 40;
		return base.VelikostOkna(oblastOkna);
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		if (asl != null)
		{
			asl.Vykreslit(e.Graphics);
		}
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
		this.BackColor = System.Drawing.Color.White;
		this.ForeColor = System.Drawing.Color.Black;
		base.Name = "Info";
		base.Size = new System.Drawing.Size(246, 150);
		base.ResumeLayout(false);
	}
}
