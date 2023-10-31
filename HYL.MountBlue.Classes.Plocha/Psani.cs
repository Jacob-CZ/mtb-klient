using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Texty;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class Psani : _Psani
{
	private const int RychlostPsaniUkazky = 400;

	private VetaKresleni VetaZadani;

	private VetaKresleniRT VetaOpisRT;

	private int iNapsanyZnakUkazky;

	private Timer Casovac;

	private Ukazka.KonecUkazky KonecUkazky;

	private char[] ZnakyUkazky;

	public Psani(PStudent prihlStudent, HYL.MountBlue.Classes.Cviceni.Psani cvicPs)
		: base(prihlStudent, cvicPs)
	{
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		if (cmdPrejitNaDalsi != null)
		{
			cmdPrejitNaDalsi.Enabled = false;
		}
		ucSlapoty.CasVyprsel += Vyhodnotit;
		cmdVyhodnotit.TlacitkoStisknuto += Vyhodnotit;
		VetaZadani = new VetaKresleni(CviceniPsani.TextZadani, tmbZadani, Barva.PodtrzeniChybyZadani, ZpristupnitPosuvnik: false, Pismo.PismoTextu(PrihlasenyStudent.Uzivatel.VelikostPisma), PrihlasenyStudent.Uzivatel.BarvaPisma, PrihlasenyStudent.Uzivatel.BarvaPozadi, CviceniPsani.TextZadaniSopakovanim, CviceniPsani.Zpusob == HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.zpravaDoleva);
		VetaOpisRT = new VetaKresleniRT(tmbOpis, Pismo.PismoTextu(PrihlasenyStudent.Uzivatel.VelikostPisma), PrihlasenyStudent.Uzivatel.BarvaPisma, PrihlasenyStudent.Uzivatel.BarvaPozadi, CviceniPsani.Backspace, CviceniPsani.Vyhodnoceni == HYL.MountBlue.Classes.Cviceni.Psani.eVyhodnoceniTyp.klasifikace, CviceniPsani.Zpusob == HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.slepaObrazovka, 2 * CviceniPsani.TextZadaniSopakovanim.Length);
		VetaOpisRT.ZobrazitVarovneOkno += DelegZobrazitVarovneOkno;
		VetaOpisRT.PosunNaRadek += DelegPosunNaRadek;
		VetaOpisRT.Vyhodnotit += Vyhodnotit;
		VetaOpisRT.ZacalPsat += ZacalPsat;
		VetaOpisRT.Pripraven += Pripraven;
		if (VetaZadani.PosuvnikZobrazeny && CviceniPsani.Pocet > 1)
		{
			CviceniPsani.SkutecnyPocet = 1;
		}
		Pripraven();
	}

	internal override void ZacatekPlochy()
	{
		base.ZacatekPlochy();
		VetaZadani.SpustitRuntime();
		VetaOpisRT.SpustitRuntime();
		if (CviceniPsani.Klasifikace)
		{
			PrihlasenyStudent.Student.KlasifikaceZacalPsat();
		}
		if (CviceniPsani.Odmena && PrihlasenyStudent.Student.ZobrazitInfoOodmene())
		{
			MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.Psani_MuzeZiskatOdmenu, HYL.MountBlue.Resources.Texty.Psani_MuzeZiskatOdmenu_Title, eMsgBoxTlacitka.OK);
			PrihlasenyStudent.Student.ZobrazenoInfoOodmene();
		}
		if (CviceniPsani.Zpusob > HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.normalne)
		{
			ZobrazitUkazku();
		}
		else if (CviceniPsani.SkutecnyPocet > 1)
		{
			MsgBoxBublina.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Psani_opisNkrat, CviceniPsani.SkutecnyPocet), HYL.MountBlue.Resources.Texty.Psani_opisNkrat_Title, eMsgBoxTlacitka.OK);
		}
		if (!CviceniPsani.OznaceniCviceni.JeTrenink && CviceniPsani.Klasifikace)
		{
			ZnamkyKlas.ZobrazitInformace(CviceniPsani);
		}
		tmbOpis.Focus();
	}

	internal override void KonecPlochy()
	{
		base.KonecPlochy();
		VetaZadani.CleanUp();
		VetaOpisRT.CleanUp();
	}

	private void DelegZobrazitVarovneOkno(bool bBudeZacinatOdZacatku, bool bDosazenoMaxDelky)
	{
		VarovaniStisky.ZobrazitDialog(bBudeZacinatOdZacatku, bDosazenoMaxDelky);
		if (!bBudeZacinatOdZacatku && bDosazenoMaxDelky)
		{
			Vyhodnotit();
		}
	}

	private void DelegPosunNaRadek(int iCisloRadku)
	{
		VetaZadani.PosunoutNaRadek(iCisloRadku);
	}

	private void Vyhodnotit()
	{
		ucSlapoty.Stop();
		cmdVyhodnotit.Enabled = false;
		tmbOpis.Enabled = false;
		_Plocha.HlavniOkno.ZpristupneniMiMaZa(zpristupnit: true);
		if (CviceniPsani.Klasifikace)
		{
			PrihlasenyStudent.Student.KlasifikacePrestalPsat();
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(VetaOpisRT.NapsanyText);
		if (CviceniPsani.Zpusob != HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.bezMezer)
		{
			int num = 0;
			while (stringBuilder.Length > 0 && stringBuilder.ToString(stringBuilder.Length - 1, 1) == " " && num < 3)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
				num++;
			}
		}
		PrihlasenyStudent.ZobrazitVyhodnoceni(CviceniPsani, stringBuilder.ToString(), VetaOpisRT.PocetVterin);
	}

	private void ZobrazitUkazku()
	{
		if (CviceniPsani.Zpusob == HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.slovaObsahujici)
		{
			char[] array = CviceniPsani.Znaky.ToCharArray();
			foreach (SlovoKresleni item in (IEnumerable)VetaZadani)
			{
				char[] array2 = array;
				foreach (char value in array2)
				{
					if (item.Text.ToUpper().IndexOf(value) > -1)
					{
						item.ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.uplne;
						break;
					}
				}
			}
			tmbZadani.Invalidate();
		}
		else if (CviceniPsani.Zpusob == HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.slovaZacinajici)
		{
			char value2 = CviceniPsani.Znaky.ToCharArray()[0];
			foreach (SlovoKresleni item2 in (IEnumerable)VetaZadani)
			{
				if (item2.Text.ToUpper().IndexOf(value2) == 0)
				{
					item2.ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.uplne;
				}
			}
			tmbZadani.Invalidate();
		}
		ZnakyUkazky = CviceniPsani.CelyText.ToCharArray();
		Casovac = new Timer();
		Casovac.Interval = 400;
		Casovac.Tick += Casovac_Tick;
		Ukazka.ZobrazitUkazku(CviceniPsani, PrehratUkazku);
		Casovac.Stop();
		Casovac.Tick -= Casovac_Tick;
		Casovac = null;
		if (CviceniPsani.Zpusob == HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.slovaObsahujici || CviceniPsani.Zpusob == HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.slovaZacinajici)
		{
			foreach (SlovoKresleni item3 in (IEnumerable)VetaZadani)
			{
				item3.ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.zadne;
			}
			tmbZadani.Invalidate();
		}
		KonecUkazky = null;
		VetaOpisRT.VynulovatZnakyUkazky();
	}

	private void PrehratUkazku(Ukazka.KonecUkazky konec)
	{
		KonecUkazky = konec;
		iNapsanyZnakUkazky = 0;
		VetaOpisRT.VynulovatZnakyUkazky();
		Casovac.Start();
	}

	private void Casovac_Tick(object sender, EventArgs e)
	{
		if (iNapsanyZnakUkazky < ZnakyUkazky.Length)
		{
			VetaOpisRT.PridatZnakUkazky(ZnakyUkazky[iNapsanyZnakUkazky++]);
			while (iNapsanyZnakUkazky < ZnakyUkazky.Length && ZnakyUkazky[iNapsanyZnakUkazky] == ' ')
			{
				VetaOpisRT.PridatZnakUkazky(ZnakyUkazky[iNapsanyZnakUkazky++]);
			}
			tmbOpis.Invalidate();
		}
		else
		{
			Casovac.Stop();
			if (KonecUkazky != null)
			{
				KonecUkazky();
			}
		}
	}

	private void ZacalPsat()
	{
		cmdNavratDomu.Enabled = false;
		cmdVyhodnotit.Enabled = true;
		_Plocha.HlavniOkno.ZpristupneniMiMaZa(zpristupnit: false);
		ucSlapoty.Start();
	}

	private void Pripraven()
	{
		cmdNavratDomu.Enabled = true;
		cmdVyhodnotit.Enabled = false;
		_Plocha.HlavniOkno.ZpristupneniMiMaZa(zpristupnit: true);
		ucSlapoty.Vynulovat();
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Return && cmdVyhodnotit.Enabled)
		{
			Vyhodnotit();
			return true;
		}
		if (!cmdNavratDomu.Enabled)
		{
			return false;
		}
		return base.StisknutaKlavesa(e);
	}

	protected override void cmdNavratDomu_TlacitkoStisknuto()
	{
		base.cmdNavratDomu_TlacitkoStisknuto();
		if (CviceniPsani.Klasifikace && !CviceniPsani.OznaceniCviceni.JeTrenink)
		{
			PrihlasenyStudent.ZobrazitMojeZnamky();
		}
	}
}
