using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class SezeniInfo : _Sezeni
{
	private ObrazkoveTlacitko cmdNavratDomu;

	public SezeniInfo(PStudent prihlStudent)
		: base(prihlStudent)
	{
	}

	internal override bool FixniVyska()
	{
		return true;
	}

	internal override int VyskaTlacitek()
	{
		return 42;
	}

	internal override string TextPopisu()
	{
		return HYL.MountBlue.Resources.Texty.Sezeni_BublinaInfo;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdNavratDomu = new ObrazkoveTlacitko();
		cmdNavratDomu.Anchor = AnchorStyles.Top;
		cmdNavratDomu.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 147);
		location.Offset(pntZacatek);
		cmdNavratDomu.Location = location;
		cmdNavratDomu.Name = "cmdZacitPsat";
		cmdNavratDomu.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuN;
		cmdNavratDomu.Size = new Size(126, 43);
		cmdNavratDomu.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuD;
		cmdNavratDomu.TabIndex = 7;
		cmdNavratDomu.Visible = true;
		cmdNavratDomu.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuZ;
		cmdNavratDomu.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuH;
		cmdNavratDomu.TlacitkoStisknuto += base.cmdNavratDomu_TlacitkoStisknuto;
		ttt.SetToolTip(cmdNavratDomu, HYL.MountBlue.Resources.Texty.Cviceni_cmdNavratDomu_TTT);
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdNavratDomu);
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmdNavratDomu_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}
}
