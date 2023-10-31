using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klavesnice;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal abstract class _Klavesnice : _Zahlavi
{
	protected HYL.MountBlue.Classes.Klavesnice.Klavesnice objKlavesnice;

	protected HYL.MountBlue.Controls.Klavesnice ucKlavesnice;

	private KlavesaInfo ucKlavesaInfo;

	public _Klavesnice(PStudent prihlStudent)
		: base(prihlStudent)
	{
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		VykreslitBilyPodklad(G);
		VykreslitModryPodklad(G);
	}

	internal abstract void NacistObjektKlavesnice();

	internal abstract string TextNadpisu();

	internal abstract string TextPopisu();

	private void VykreslitBilyPodklad(Graphics G)
	{
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngPodklad1, 21, 112);
		int num = 628;
		if (!FixniVyska())
		{
			num = _Plocha.HlavniOkno.DisplayRectangle.Height - 106;
		}
		G.DrawImage(HYL.MountBlue.Resources.Grafika.pngPodklad2, 21, 173, 821, num - 173);
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngPodklad3, 21, num);
	}

	private void VykreslitModryPodklad(Graphics G)
	{
		Rectangle rObdelnik = new Rectangle(73, 476, 712, 200);
		if (!FixniVyska())
		{
			rObdelnik.Height = _Plocha.HlavniOkno.DisplayRectangle.Height - 540;
		}
		rObdelnik.Inflate(8, 8);
		GraphicsPath path = SpecialniGrafika.CestaZaoblenehoObdelniku(rObdelnik, 4);
		Brush brush = new SolidBrush(Barva.ObdelnikZaTextBoxem);
		G.FillPath(brush, path);
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		NacistObjektKlavesnice();
		ucKlavesnice = new HYL.MountBlue.Controls.Klavesnice(objKlavesnice);
		ucKlavesaInfo = new KlavesaInfo();
		ucKlavesnice.Anchor = AnchorStyles.Top;
		ucKlavesnice.BackColor = Color.White;
		ucKlavesnice.BackgroundImageLayout = ImageLayout.None;
		Point location = new Point(61, 145);
		location.Offset(pntZacatek);
		ucKlavesnice.Location = location;
		ucKlavesnice.MaximumSize = new Size(550, 322);
		ucKlavesnice.MinimumSize = new Size(550, 322);
		ucKlavesnice.Name = "ucKlavesnice";
		ucKlavesnice.Size = new Size(550, 322);
		ucKlavesnice.TabIndex = 7;
		ucKlavesnice.KliknutoNaKlavesu += ucKlavesnice_KliknutoNaKlavesu;
		ucKlavesaInfo.Anchor = AnchorStyles.Top;
		ucKlavesaInfo.BackColor = Color.White;
		ucKlavesaInfo.Location = new Point(562, 119);
		ucKlavesaInfo.Name = "ucNovaKlavesa";
		ucKlavesaInfo.Size = new Size(256, 304);
		ucKlavesaInfo.TabIndex = 6;
		ucKlavesaInfo.Text = Text.TextNBSP(TextNadpisu());
		ucKlavesaInfo.Popis = Text.TextNBSP(TextPopisu());
		ucKlavesaInfo.NastavitPozici(_Plocha.HlavniOkno);
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(ucKlavesnice);
		deleg(ucKlavesaInfo);
	}

	internal override void ZmenaVelikosti(bool bMeniSeVelikost)
	{
		base.ZmenaVelikosti(bMeniSeVelikost);
		if (bMeniSeVelikost)
		{
			ucKlavesnice.StopAnimace();
		}
		else
		{
			ucKlavesnice.StartAnimace();
		}
	}

	internal override void PresunOkna(bool bPresouvaSe)
	{
		base.PresunOkna(bPresouvaSe);
		if (bPresouvaSe)
		{
			ucKlavesnice.StopAnimace();
		}
		else
		{
			ucKlavesnice.StartAnimace();
		}
	}

	private void ucKlavesnice_KliknutoNaKlavesu(string nadpis, string popis)
	{
		MsgBoxBublina.ZobrazitMessageBox(popis, nadpis, eMsgBoxTlacitka.OK);
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			cmdNavratDomu_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}

	protected void cmdNavratDomu_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitDomu();
	}
}
