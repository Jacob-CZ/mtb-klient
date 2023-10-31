using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class SezeniUcitel : _Plocha
{
	private PUcitel PrihlasenyUcitel;

	private int tridaID;

	private Sezeni ucSezeni;

	private ObrazkoveTlacitko cmdNavratDomu;

	public SezeniUcitel(PUcitel ucitel, int tridaID)
	{
		PrihlasenyUcitel = ucitel;
		this.tridaID = tridaID;
	}

	public SezeniUcitel(PUcitel ucitel)
		: this(ucitel, 0)
	{
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		VykreslitSpravneSezeni(G);
	}

	private void VykreslitSpravneSezeni(Graphics G)
	{
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngSezeni, 43, 43);
	}

	internal override bool FixniVyska()
	{
		return true;
	}

	internal override int VyskaTlacitek()
	{
		return 42;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		ucSezeni = new Sezeni();
		cmdNavratDomu = new ObrazkoveTlacitko();
		ucSezeni.Anchor = AnchorStyles.Top;
		ucSezeni.BackColor = Color.White;
		ucSezeni.Name = "ucSezeni";
		ucSezeni.Size = new Size(417, 237);
		ucSezeni.TabIndex = 2;
		ucSezeni.NastavitPozici(_Plocha.HlavniOkno);
		ucSezeni.Text = HYL.MountBlue.Resources.Texty.Sezeni_BublinaNadpis;
		ucSezeni.Popis = Text.TextNBSP(HYL.MountBlue.Resources.Texty.SezeniUcitel_BublinaInfo);
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
		cmdNavratDomu.TlacitkoStisknuto += cmdNavratDomu_TlacitkoStisknuto;
		ttt.SetToolTip(cmdNavratDomu, HYL.MountBlue.Resources.Texty.Cviceni_cmdNavratDomu_TTT);
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(ucSezeni);
		deleg(cmdNavratDomu);
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmdNavratDomu_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.Escape)
		{
			cmdNavratDomu_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}

	protected void cmdNavratDomu_TlacitkoStisknuto()
	{
		if (tridaID == 0)
		{
			PrihlasenyUcitel.ZobrazitDomu();
		}
		else
		{
			PrihlasenyUcitel.OtevritTridu(tridaID, ZalozkyTrida.eZalozky.SeznamStudentu);
		}
	}
}
