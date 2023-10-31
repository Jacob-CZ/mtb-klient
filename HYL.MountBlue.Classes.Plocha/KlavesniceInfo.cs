using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klavesnice;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class KlavesniceInfo : _Klavesnice
{
	private ObrazkoveTlacitko cmdNavratDomu;

	private ListView lvwKlavesy;

	public KlavesniceInfo(PStudent prihlStudent)
		: base(prihlStudent)
	{
	}

	internal override void NacistObjektKlavesnice()
	{
		objKlavesnice = new HYL.MountBlue.Classes.Klavesnice.Klavesnice();
		objKlavesnice.PridatRozsah(PrihlasenyStudent.Student.ZnameKlavesy);
	}

	internal override string TextNadpisu()
	{
		return HYL.MountBlue.Resources.Texty.KlavesniceInfo_Title;
	}

	internal override string TextPopisu()
	{
		int pocet = objKlavesnice.Pocet;
		if (pocet == 51 || PrihlasenyStudent.Student.VyukaDokoncena)
		{
			return string.Format(HYL.MountBlue.Resources.Texty.KlavesniceInfo_final, 51);
		}
		if (pocet == 1)
		{
			return HYL.MountBlue.Resources.Texty.KlavesniceInfo_1;
		}
		if (pocet <= 4)
		{
			return string.Format(HYL.MountBlue.Resources.Texty.KlavesniceInfo_234, pocet);
		}
		return string.Format(HYL.MountBlue.Resources.Texty.KlavesniceInfo_5, pocet);
	}

	internal override bool FixniVyska()
	{
		return false;
	}

	internal override int VyskaTlacitek()
	{
		return 43;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdNavratDomu = new ObrazkoveTlacitko();
		lvwKlavesy = new ListView();
		cmdNavratDomu.Anchor = AnchorStyles.Top;
		cmdNavratDomu.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 147);
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
		lvwKlavesy.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
		lvwKlavesy.BackColor = Barva.PozadiTextBoxu;
		location = new Point(73, 476);
		location.Offset(pntZacatek);
		lvwKlavesy.Location = location;
		lvwKlavesy.Name = "lvwKlavesy";
		lvwKlavesy.BorderStyle = BorderStyle.Fixed3D;
		lvwKlavesy.MultiSelect = false;
		lvwKlavesy.View = View.Details;
		lvwKlavesy.LabelEdit = false;
		lvwKlavesy.Font = new Font("Arial", 9f, FontStyle.Regular);
		lvwKlavesy.LabelWrap = false;
		lvwKlavesy.HeaderStyle = ColumnHeaderStyle.None;
		lvwKlavesy.FullRowSelect = true;
		lvwKlavesy.ShowGroups = true;
		lvwKlavesy.ForeColor = Barva.TextObecny;
		lvwKlavesy.Columns.Add(HYL.MountBlue.Resources.Texty.KlavesniceInfo_clhTabor, 150, HorizontalAlignment.Left);
		lvwKlavesy.Columns.Add(HYL.MountBlue.Resources.Texty.KlavesniceInfo_clhKlavesy, 500, HorizontalAlignment.Left);
		lvwKlavesy.Size = new Size(712, _Plocha.HlavniOkno.DisplayRectangle.Height - 540);
		lvwKlavesy.TabIndex = 0;
		_Lekce.Lekce().Klavesy.KlavesyDoListView(lvwKlavesy, PrihlasenyStudent.Student);
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdNavratDomu);
		deleg(lvwKlavesy);
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
