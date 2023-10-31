using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klavesnice;
using HYL.MountBlue.Classes.Texty;
using HYL.MountBlue.Classes.Uzivatele.Historie;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class KlavesniceCviceni : _Klavesnice
{
	private const string IgnorovaneZnaky = "°\u00b4ˇ\u00a8";

	private HYL.MountBlue.Classes.Cviceni.Klavesnice cvicKlavesnice;

	private bool bolKontrolaZnaku;

	private bool bolJeEnter;

	private char chrVyucovanyZnak;

	private ObrazkoveTlacitko cmdPrejitNaDalsi;

	private ObrazkoveTlacitko cmdNavratDomu;

	private TextBoxMB tmbTest;

	private VetaKresleniRT VetaTest;

	internal KlavesniceCviceni(PStudent prihlStudent, HYL.MountBlue.Classes.Cviceni.Klavesnice cvic)
		: base(prihlStudent)
	{
		cvicKlavesnice = cvic;
	}

	internal override void NacistObjektKlavesnice()
	{
		objKlavesnice = new HYL.MountBlue.Classes.Klavesnice.Klavesnice();
		objKlavesnice.PridatRozsah(cvicKlavesnice.ZnameKlavesy);
		objKlavesnice.NastavitBlikajiciKlavesu(cvicKlavesnice.CisloKlavesy, cvicKlavesnice.Shift, cvicKlavesnice.Modifikator);
		chrVyucovanyZnak = objKlavesnice.Znak;
		bolKontrolaZnaku = chrVyucovanyZnak != 0 && !"°\u00b4ˇ\u00a8".Contains(chrVyucovanyZnak.ToString());
		bolJeEnter = objKlavesnice.BlikajiciKlavesa.JeBezobsahovaKlavesa() && objKlavesnice.BlikajiciKlavesa.Klavesa == KlavesaKlavesnice.eKlavesy.enter;
		if (objKlavesnice.Pocet >= 3)
		{
			PrihlasenyStudent.Student.NastavitZnameKlavesy(cvicKlavesnice.ZnameKlavesy);
		}
	}

	internal override string TextNadpisu()
	{
		return objKlavesnice.Nadpis;
	}

	internal override string TextPopisu()
	{
		return objKlavesnice.Popis;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdPrejitNaDalsi = new ObrazkoveTlacitko();
		cmdNavratDomu = new ObrazkoveTlacitko();
		tmbTest = new TextBoxMB();
		cmdPrejitNaDalsi.Anchor = AnchorStyles.Top;
		cmdPrejitNaDalsi.BackColor = Barva.PozadiTlacitekNaPlose;
		cmdPrejitNaDalsi.Enabled = !bolKontrolaZnaku;
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
		tmbTest.Anchor = AnchorStyles.Top;
		tmbTest.BackColor = PrihlasenyStudent.Uzivatel.BarvaPozadi;
		location = new Point(73, 476);
		location.Offset(pntZacatek);
		tmbTest.Location = location;
		tmbTest.Name = "tmbTest";
		tmbTest.BorderStyle = BorderStyle.Fixed3D;
		tmbTest.Size = new Size(712, 200);
		tmbTest.TabIndex = 0;
		ucKlavesnice.KliknutoMimo += ucKlavesnice_KliknutoMimo;
		VetaTest = new VetaKresleniRT(tmbTest, Pismo.PismoTextu(PrihlasenyStudent.Uzivatel.VelikostPisma), PrihlasenyStudent.Uzivatel.BarvaPisma, PrihlasenyStudent.Uzivatel.BarvaPozadi, PovolitBackspace: true, Klasifikace: false, SlepaObrazovka: false, 256);
		if (cvicKlavesnice.CisloKlavesy == KlavesaKlavesnice.eKlavesy.backspace)
		{
			VetaTest.NastavitText(HYL.MountBlue.Resources.Texty.KlavesniceCviceni_tmbTest_Backspace);
		}
		VetaTest.PridanZnak += VetaTest_PridanZnak;
	}

	private void ucKlavesnice_KliknutoMimo()
	{
		tmbTest.Focus();
	}

	private void VetaTest_PridanZnak(char znak)
	{
		if (!cmdPrejitNaDalsi.Enabled && bolKontrolaZnaku && chrVyucovanyZnak == znak)
		{
			cmdPrejitNaDalsi.Enabled = true;
		}
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdNavratDomu);
		deleg(cmdPrejitNaDalsi);
		deleg(tmbTest);
	}

	private void cmdPrejitNaDalsi_TlacitkoStisknuto()
	{
		ZaznKlavesa zaznam = new ZaznKlavesa((byte)cvicKlavesnice.CisloKlavesy, (byte)cvicKlavesnice.Modifikator, cvicKlavesnice.Shift);
		PrihlasenyStudent.Uzivatel.Historie.PridatZaznam(zaznam);
		PrihlasenyStudent.PrejitNaDalsiCviceni();
		PrihlasenyStudent.ZobrazitPrejitNaDalsiCviceni();
	}

	internal override void ZacatekPlochy()
	{
		base.ZacatekPlochy();
		VetaTest.SpustitRuntime();
		tmbTest.Focus();
	}

	internal override void KonecPlochy()
	{
		base.KonecPlochy();
		VetaTest.CleanUp();
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if ((!bolJeEnter || e.Modifiers == Keys.Control) && e.KeyCode == Keys.Return && cmdPrejitNaDalsi.Enabled)
		{
			cmdPrejitNaDalsi_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}

	internal override void ZmenaVelikosti(bool bMeniSeVelikost)
	{
		base.ZmenaVelikosti(bMeniSeVelikost);
		if (!bMeniSeVelikost)
		{
			tmbTest.Focus();
		}
	}

	internal override void PresunOkna(bool bPresouvaSe)
	{
		base.PresunOkna(bPresouvaSe);
		if (!bPresouvaSe)
		{
			tmbTest.Focus();
		}
	}

	internal override bool FixniVyska()
	{
		return true;
	}

	internal override int VyskaTlacitek()
	{
		return 112;
	}
}
