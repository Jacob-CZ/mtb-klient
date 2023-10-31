using System;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Obecne;
using HYL.MountBlue.Classes.Texty;
using HYL.MountBlue.Classes.Uzivatele.Historie;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class PsaniVysl : _Psani
{
	private const int HraniceProCas = 1;

	private string TextOpisu;

	private int PocetVterin;

	private bool ZiskalOdmenu;

	private byte Znamka;

	private bool ZustalNaStejnemCviceni;

	private bool ZustalNaStejnemTreninku;

	private Vysledky ucVysledky;

	private VetaKresleni VetaZadani;

	private VetaKresleni VetaOpis;

	public PsaniVysl(PStudent prihlStudent, HYL.MountBlue.Classes.Cviceni.Psani cvicPs, string sTextOpisu, int iPocetVterin)
		: base(prihlStudent, cvicPs)
	{
		TextOpisu = sTextOpisu;
		PocetVterin = iPocetVterin;
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		if ((CviceniPsani.Zpusob > HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.normalne && CviceniPsani.Zpusob != HYL.MountBlue.Classes.Cviceni.Psani.eZpusob.slepaObrazovka) || CviceniPsani.SkutecnyPocet > 1)
		{
			_Plocha.VykreslitText(G, HYL.MountBlue.Resources.Texty.PsaniVysl_SpravnyText, Color.Gray, 9, FontStyle.Regular, new Rectangle(64, 97, 300, 20), StringAlignment.Center, StringAlignment.Near);
		}
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		ucVysledky = new Vysledky(CviceniPsani.VyhodnoceniKriterium == HYL.MountBlue.Classes.Cviceni.Psani.eKriterium.cistychUhozuZaMin);
		ucVysledky.Anchor = AnchorStyles.Top;
		ucVysledky.BackColor = Color.White;
		ucVysledky.Location = new Point(650, 254);
		ucVysledky.MaximumSize = new Size(180, 150);
		ucVysledky.MinimumSize = new Size(180, 150);
		ucVysledky.Name = "ucVysledky";
		ucVysledky.Size = new Size(180, 150);
		ucVysledky.TabIndex = 16;
		ucVysledky.Visible = false;
		cmdVyhodnotit.Enabled = false;
		if (cmdPrejitNaDalsi != null)
		{
			cmdPrejitNaDalsi.TlacitkoStisknuto += cmdPrejitNaDalsi_TlacitkoStisknuto;
		}
		ucSlapoty.NastavitDokonceno();
		VetaZadani = new VetaKresleni(CviceniPsani.CelyText, tmbZadani, Barva.PodtrzeniChybyZadani, ZpristupnitPosuvnik: true, Pismo.PismoTextu(PrihlasenyStudent.Uzivatel.VelikostPisma), PrihlasenyStudent.Uzivatel.BarvaPisma, PrihlasenyStudent.Uzivatel.BarvaPozadi, string.Empty, OpisZpravaDoleva: false);
		VetaOpis = new VetaKresleni(TextOpisu, tmbOpis, Barva.PodtrzeniChybyOpis, ZpristupnitPosuvnik: true, Pismo.PismoTextu(PrihlasenyStudent.Uzivatel.VelikostPisma), PrihlasenyStudent.Uzivatel.BarvaPisma, PrihlasenyStudent.Uzivatel.BarvaPozadi, string.Empty, OpisZpravaDoleva: false);
		Vyhodnoceni vyhodnoceni = new Vyhodnoceni(VetaZadani, VetaOpis, !CviceniPsani.OznaceniCviceni.JeTrenink);
		vyhodnoceni.Vyhodnot();
		int pocetUhozu = VetaOpis.PocetUhozu;
		int pocetChyb = vyhodnoceni.PocetChyb;
		int num = -1;
		float num2;
		if (pocetUhozu > 0)
		{
			num2 = (float)pocetChyb * 100f / (float)pocetUhozu;
			if (num2 > 100f)
			{
				num2 = 100f;
			}
			num2 = (float)Math.Floor(num2 * 100f) / 100f;
		}
		else
		{
			num2 = 100f;
		}
		if ((CviceniPsani.Vyhodnoceni > HYL.MountBlue.Classes.Cviceni.Psani.eVyhodnoceniTyp.zadne && CviceniPsani.VyhodnoceniKriterium == HYL.MountBlue.Classes.Cviceni.Psani.eKriterium.cistychUhozuZaMin) || (CviceniPsani.Odmena && CviceniPsani.OdmenaKriterium == HYL.MountBlue.Classes.Cviceni.Psani.eKriterium.cistychUhozuZaMin))
		{
			int num3 = pocetUhozu - pocetChyb * CviceniPsani.Penalizace;
			if (num3 < 0)
			{
				num3 = 0;
			}
			num = (int)((decimal)num3 / (decimal)PocetVterin * 60m);
			ucVysledky.NastavitHodnotyVysledku(pocetChyb, num2, pocetUhozu, num);
			ucVysledky.Size = ucVysledky.MaximumSize;
		}
		else
		{
			ucVysledky.NastavitHodnotyVysledku(pocetChyb, num2, pocetUhozu);
		}
		ucVysledky.NastavitPozici(_Plocha.HlavniOkno);
		ZustalNaStejnemCviceni = num2 > _ObecnaData.ObecnaData.MaximalniPovolenaChybovost() && pocetChyb > _ObecnaData.ObecnaData.MaximalniPovolenyPocetChyb(CviceniPsani.Vetveni);
		ZustalNaStejnemTreninku = num2 > _ObecnaData.ObecnaData.MaximalniPovolenaChybovost() || (CviceniPsani.Cas != 0f && (double)(PocetVterin + 1) / 60.0 < (double)CviceniPsani.Cas) || (CviceniPsani.Rychlost != 0 && num < CviceniPsani.Rychlost);
		ZjistitVysledek(pocetChyb, num2, num, out var cNovaVetev, out var byZnamka, out var iOdmena);
		if ((!CviceniPsani.Klasifikace && !ZustalNaStejnemCviceni) || (!ZustalNaStejnemTreninku && CviceniPsani.OznaceniCviceni.JeTrenink))
		{
			if (CviceniPsani.Vyhodnoceni == HYL.MountBlue.Classes.Cviceni.Psani.eVyhodnoceniTyp.vetveni && cNovaVetev != '-')
			{
				PrihlasenyStudent.PrejitNaDalsiCviceni(cNovaVetev);
			}
			else
			{
				PrihlasenyStudent.PrejitNaDalsiCviceni();
			}
		}
		ZiskalOdmenu = (int)iOdmena > 0;
		if (ZiskalOdmenu)
		{
			PrihlasenyStudent.Student.PridatMalouOdmenu();
		}
		string popisZadani = string.Format(HYL.MountBlue.Resources.Texty.PsaniVysl_popisZadani, CviceniPsani.SkutecnyPocet, CviceniPsani.ZpusobToString());
		if (CviceniPsani.Klasifikace)
		{
			if (CviceniPsani.OznaceniCviceni.JeTrenink)
			{
				byZnamka = 0;
			}
			Znamka = byZnamka;
			bool flag = PrihlasenyStudent.Student.Klasifikace.MuzeOpakovatKlasifikaci(CviceniPsani.ID) && !PrihlasenyStudent.Student.KlasifikaceOpakovani;
			ZaznKlasifCvic zaznam = new ZaznKlasifCvic(CviceniPsani.ID, CviceniPsani.OznaceniCviceni, popisZadani, CviceniPsani.CelyText, TextOpisu, TimeSpan.FromSeconds(PocetVterin), pocetChyb, num2, pocetUhozu, num, byZnamka, flag);
			PrihlasenyStudent.Uzivatel.Historie.PridatZaznam(zaznam);
			ZaznKlasifCvic klcv = new ZaznKlasifCvic(CviceniPsani.ID, CviceniPsani.OznaceniCviceni, popisZadani, CviceniPsani.CelyText, TextOpisu, TimeSpan.FromSeconds(PocetVterin), pocetChyb, num2, pocetUhozu, num, byZnamka, flag);
			PrihlasenyStudent.Student.Klasifikace.PridatKlasifikaci(klcv);
			if (flag)
			{
				PrihlasenyStudent.Student.OdebratVelkouOdmenu();
			}
			PrihlasenyStudent.Student.KlasifikaceZakazat();
		}
		else
		{
			ZaznCviceni zaznam2 = new ZaznCviceni(CviceniPsani.ID, CviceniPsani.OznaceniCviceni, popisZadani, CviceniPsani.CelyText, TextOpisu, TimeSpan.FromSeconds(PocetVterin), pocetChyb, num2, pocetUhozu, num, ZiskalOdmenu, ZustalNaStejnemCviceni);
			PrihlasenyStudent.Uzivatel.Historie.PridatZaznam(zaznam2);
		}
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(ucVysledky);
	}

	internal override void ZacatekPlochy()
	{
		base.ZacatekPlochy();
		VetaZadani.SpustitRuntime();
		VetaOpis.SpustitRuntime();
		if (!CviceniPsani.OznaceniCviceni.JeTrenink)
		{
			if (CviceniPsani.Klasifikace)
			{
				HYL.MountBlue.Dialogs.Znamka.ZobrazitZnamku(Znamka);
			}
			else if (ZustalNaStejnemCviceni)
			{
				string textZpravy = Text.TextMuzZena(PrihlasenyStudent.Uzivatel.Pohlavi, HYL.MountBlue.Resources.Texty.PsaniVysl_msgboxSpatneNapsaneM, HYL.MountBlue.Resources.Texty.PsaniVysl_msgboxSpatneNapsaneZ);
				MsgBoxBublina.ZobrazitMessageBox(textZpravy, HYL.MountBlue.Resources.Texty.PsaniVysl_msgboxSpatneNapsane_Title, eMsgBoxTlacitka.OK);
			}
			else if (ZiskalOdmenu)
			{
				Odmena.ZobrazitOdmenu(PrihlasenyStudent.Student.PocetMalychOdmen == 0 && PrihlasenyStudent.Student.PocetVelkychOdmen > 0);
			}
		}
		ucVysledky.Visible = true;
	}

	internal override void KonecPlochy()
	{
		base.KonecPlochy();
		VetaZadani.CleanUp();
		VetaOpis.CleanUp();
	}

	private void cmdPrejitNaDalsi_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitPrejitNaDalsiCviceni();
	}

	private void ZjistitVysledek(int iPocetChyb, float sChybovost, int iCistychUhozuZaMin, out char cNovaVetev, out byte byZnamka, out HYL.MountBlue.Classes.Cviceni.Psani.eOdmenaTyp iOdmena)
	{
		cNovaVetev = '\0';
		byZnamka = 0;
		if (CviceniPsani.Vyhodnoceni > HYL.MountBlue.Classes.Cviceni.Psani.eVyhodnoceniTyp.zadne)
		{
			bool flag = false;
			byte b = 1;
			float num;
			int num2;
			switch (CviceniPsani.VyhodnoceniKriterium)
			{
			case HYL.MountBlue.Classes.Cviceni.Psani.eKriterium.pocetChybRel:
				num = sChybovost;
				num2 = 1;
				break;
			case HYL.MountBlue.Classes.Cviceni.Psani.eKriterium.cistychUhozuZaMin:
				num = iCistychUhozuZaMin;
				num2 = -1;
				break;
			default:
				num = iPocetChyb;
				num2 = 1;
				break;
			}
			foreach (Podminka item in CviceniPsani)
			{
				if ((double)(num * (float)num2) <= item.Hodnota * (double)num2)
				{
					switch (CviceniPsani.Vyhodnoceni)
					{
					case HYL.MountBlue.Classes.Cviceni.Psani.eVyhodnoceniTyp.vetveni:
						cNovaVetev = item.Vetev;
						break;
					case HYL.MountBlue.Classes.Cviceni.Psani.eVyhodnoceniTyp.klasifikace:
						byZnamka = b;
						break;
					}
					flag = true;
					break;
				}
				b++;
			}
			if (!flag)
			{
				switch (CviceniPsani.Vyhodnoceni)
				{
				case HYL.MountBlue.Classes.Cviceni.Psani.eVyhodnoceniTyp.vetveni:
					cNovaVetev = CviceniPsani.VychoziVetev;
					break;
				case HYL.MountBlue.Classes.Cviceni.Psani.eVyhodnoceniTyp.klasifikace:
					byZnamka = 5;
					break;
				}
			}
		}
		iOdmena = HYL.MountBlue.Classes.Cviceni.Psani.eOdmenaTyp.zadna;
		if (CviceniPsani.Odmena)
		{
			float num;
			int num2;
			switch (CviceniPsani.OdmenaKriterium)
			{
			case HYL.MountBlue.Classes.Cviceni.Psani.eKriterium.pocetChybRel:
				num = sChybovost;
				num2 = 1;
				break;
			case HYL.MountBlue.Classes.Cviceni.Psani.eKriterium.cistychUhozuZaMin:
				num = iCistychUhozuZaMin;
				num2 = -1;
				break;
			default:
				num = iPocetChyb;
				num2 = 1;
				break;
			}
			if (num * (float)num2 <= CviceniPsani.OdmenaHodnota * (float)num2)
			{
				iOdmena = CviceniPsani.OdmenaTyp;
			}
		}
	}

	protected override void cmdNavratDomu_TlacitkoStisknuto()
	{
		if (CviceniPsani.Klasifikace && Znamka > 1 && PrihlasenyStudent.Student.Cviceni.Lekce <= CviceniPsani.KlasifikaceDo && PrihlasenyStudent.Student.Klasifikace.MuzeOpakovatKlasifikaci(CviceniPsani.ID))
		{
			MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PsaniVysl_msgboxKlasifMuzeOpakovat, HYL.MountBlue.Resources.Texty.PsaniVysl_msgboxKlasifMuzeOpakovat_Title, eMsgBoxTlacitka.OK);
		}
		if (CviceniPsani.Klasifikace && _Lekce.Lekce().Klasifikace.KlasifikaceSplneny(PrihlasenyStudent.Student, bPouzeUrgentni: false) && PrihlasenyStudent.Student.VyukaDokoncena && !PrihlasenyStudent.Student.VystupDokoncen)
		{
			PrihlasenyStudent.Student.NastavitVystupDokoncen();
		}
		base.cmdNavratDomu_TlacitkoStisknuto();
		if (CviceniPsani.Klasifikace && !CviceniPsani.OznaceniCviceni.JeTrenink)
		{
			PrihlasenyStudent.ZobrazitMojeZnamky();
		}
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			if (cmdPrejitNaDalsi != null)
			{
				cmdPrejitNaDalsi_TlacitkoStisknuto();
				return true;
			}
			cmdNavratDomu_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}
}
