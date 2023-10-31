using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal abstract class _Prihlasit : _Plocha
{
	protected ObrazkoveTlacitko cmdPrihlasit;

	internal override int VyskaTlacitek()
	{
		return 25;
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		VykreslitPodkladOkna(G);
	}

	private void VykreslitPodkladOkna(Graphics G)
	{
		Rectangle rectangle = ((!FixniVyska()) ? new Rectangle(207, 207, 450, _Plocha.HlavniOkno.DisplayRectangle.Height - 414) : new Rectangle(262, 255, 340, 200));
		GraphicsPath path = SpecialniGrafika.CestaZaoblenehoObdelniku(rectangle, 10);
		G.FillPath(new SolidBrush(Color.White), path);
		G.IntersectClip(new Region(path));
		Rectangle rectangle2 = rectangle;
		rectangle2.Height = 50;
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngZahlaviDialogu, rectangle2);
		G.ResetClip();
		G.DrawPath(new Pen(Barva.OkrajDialogu, 1f), path);
		_Plocha.VykreslitText(G, HYL.MountBlue.Resources.Texty.Prihlasit_DoMB, Barva.PismoZahlaviOkna, 12, FontStyle.Bold, rectangle2, StringAlignment.Center, StringAlignment.Center);
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdPrihlasit = new ObrazkoveTlacitko();
		cmdPrihlasit.Anchor = AnchorStyles.Top;
		cmdPrihlasit.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 147);
		location.Offset(pntZacatek);
		cmdPrihlasit.Location = location;
		cmdPrihlasit.Name = "cmdPrihlasit";
		cmdPrihlasit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlPrihlasitN;
		cmdPrihlasit.Size = new Size(126, 25);
		cmdPrihlasit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlPrihlasitD;
		cmdPrihlasit.TabIndex = 5;
		cmdPrihlasit.Visible = true;
		cmdPrihlasit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlPrihlasitZ;
		cmdPrihlasit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlPrihlasitH;
		cmdPrihlasit.TlacitkoStisknuto += cmdPrihlasit_TlacitkoStisknuto;
		ttt.SetToolTip(cmdPrihlasit, HYL.MountBlue.Resources.Texty.Prihlasit_cmdPrihlasit_TTT);
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdPrihlasit);
	}

	protected abstract void cmdPrihlasit_TlacitkoStisknuto();

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmdPrihlasit_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}
}
