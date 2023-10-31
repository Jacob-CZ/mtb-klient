using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class SezeniCviceni : _Sezeni
{
	private HYL.MountBlue.Classes.Cviceni.Sezeni CviceniSezeni;

	private ObrazkoveTlacitko cmdPrejitNaDalsi;

	private ObrazkoveTlacitko cmdNavratDomu;

	public SezeniCviceni(PStudent prihlStudent, HYL.MountBlue.Classes.Cviceni.Sezeni cvicSez)
		: base(prihlStudent)
	{
		CviceniSezeni = cvicSez;
		PrihlasenyStudent.PrejitNaDalsiCviceni();
	}

	internal override bool FixniVyska()
	{
		return true;
	}

	internal override int VyskaTlacitek()
	{
		return 112;
	}

	internal override string TextPopisu()
	{
		return CviceniSezeni.Popis;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdPrejitNaDalsi = new ObrazkoveTlacitko();
		cmdNavratDomu = new ObrazkoveTlacitko();
		cmdPrejitNaDalsi.Anchor = AnchorStyles.Top;
		cmdPrejitNaDalsi.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 147);
		location.Offset(pntZacatek);
		cmdPrejitNaDalsi.Location = location;
		cmdPrejitNaDalsi.Name = "cmdZacitPsat";
		cmdPrejitNaDalsi.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlDalsiN;
		cmdPrejitNaDalsi.Size = new Size(126, 65);
		cmdPrejitNaDalsi.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDalsiD;
		cmdPrejitNaDalsi.TabIndex = 7;
		cmdPrejitNaDalsi.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDalsiZ;
		cmdPrejitNaDalsi.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDalsiH;
		cmdPrejitNaDalsi.TlacitkoStisknuto += cmdPrejitNaDalsi_TlacitkoStisknuto;
		ttt.SetToolTip(cmdPrejitNaDalsi, HYL.MountBlue.Resources.Texty.Cviceni_cmdPrejitNaDalsi_TTT);
		cmdNavratDomu.Anchor = AnchorStyles.Top;
		cmdNavratDomu.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 217);
		location.Offset(pntZacatek);
		cmdNavratDomu.Location = location;
		cmdNavratDomu.Name = "cmdNastaveni";
		cmdNavratDomu.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuN;
		cmdNavratDomu.Size = new Size(126, 43);
		cmdNavratDomu.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuD;
		cmdNavratDomu.TabIndex = 8;
		cmdNavratDomu.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuZ;
		cmdNavratDomu.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuH;
		cmdNavratDomu.TlacitkoStisknuto += base.cmdNavratDomu_TlacitkoStisknuto;
		ttt.SetToolTip(cmdNavratDomu, HYL.MountBlue.Resources.Texty.Cviceni_cmdNavratDomu_TTT);
	}

	private void cmdPrejitNaDalsi_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitPrejitNaDalsiCviceni();
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdPrejitNaDalsi);
		deleg(cmdNavratDomu);
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmdPrejitNaDalsi_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}
}
