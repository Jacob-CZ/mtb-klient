using System;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Uzivatele.Prihlasen;

internal abstract class PStudent : PUzivatel
{
	public enum ePostupovyKlic
	{
		spatny,
		zDomu,
		zeSkoly,
		stejny,
		mensiNez,
		OK
	}

	protected _Cviceni AktualniCviceni;

	private bool bPrechodDoDalsiLekce;

	internal HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student Student => (HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student)base.Uzivatel;

	public PStudent(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student stu)
		: base(stu)
	{
	}

	internal override void ZobrazitDomu()
	{
		bPrechodDoDalsiLekce = false;
		_Plocha.NactiPlochu(new Hora(this));
		if (Student.PraveDokonceno)
		{
			ZobrazitHlaskuVystupDokoncen();
		}
	}

	private void InicializovatCviceni()
	{
		AktualniCviceni = _Cviceni.NactiCviceni(Student.Cviceni);
	}

	public override void InicializovatUzivatele()
	{
		InicializovatCviceni();
		base.InicializovatUzivatele();
		if (!_Lekce.Lekce().Klasifikace.KlasifikaceSplneny(Student, bPouzeUrgentni: false))
		{
			MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PStudent_msgboxNezapomente, HYL.MountBlue.Resources.Texty.PStudent_msgboxNesplnenaKlasifikace_Title, eMsgBoxTlacitka.OK);
		}
	}

	internal void ZobrazitZacitPsat()
	{
		if (Student.JeSpustenaLavina())
		{
			ZobrazitOknoLavina();
		}
		if (!_Lekce.Lekce().Klasifikace.KlasifikaceSplneny(Student, bPouzeUrgentni: true))
		{
			_ = Student.KlasifikacePravePovolena;
			MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PStudent_msgboxNesplnenaKlasifikace, HYL.MountBlue.Resources.Texty.PStudent_msgboxNesplnenaKlasifikace_Title, eMsgBoxTlacitka.OK);
			ZobrazitMojeZnamky();
		}
		else
		{
			if (Student.KlasifikacePravePovolena && ZobrazitHlaskuNastavenaKlasifikace())
			{
				return;
			}
			if (AktualniCviceni is HYL.MountBlue.Classes.Cviceni.Klavesnice)
			{
				_Plocha.NactiPlochu(new KlavesniceCviceni(this, (HYL.MountBlue.Classes.Cviceni.Klavesnice)AktualniCviceni));
				return;
			}
			if (AktualniCviceni is HYL.MountBlue.Classes.Cviceni.Psani)
			{
				_Plocha.NactiPlochu(new HYL.MountBlue.Classes.Plocha.Psani(this, (HYL.MountBlue.Classes.Cviceni.Psani)AktualniCviceni));
				return;
			}
			if (!(AktualniCviceni is HYL.MountBlue.Classes.Cviceni.Sezeni))
			{
				throw new ApplicationException("Nebylo načteno žádné cvičení, nelze tedy pokračovat.");
			}
			_Plocha.NactiPlochu(new SezeniCviceni(this, (HYL.MountBlue.Classes.Cviceni.Sezeni)AktualniCviceni));
		}
	}

	internal void ZobrazitTrenink()
	{
		ZobrazitZacitPsat();
	}

	internal void ZobrazitPrejitNaDalsiCviceni()
	{
		if (!AktualniCviceni.OznaceniCviceni.JeTrenink)
		{
			if (Student.VyukaDokoncena)
			{
				ZobrazitDomu();
				return;
			}
			if (Student.JeSpustenaLavina())
			{
				ZobrazitOknoLavina();
			}
			if (bPrechodDoDalsiLekce)
			{
				ZobrazitDomu();
				if (MsgBoxBublina.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.PStudent_DalsiLekceText, Student.Cviceni.Lekce), HYL.MountBlue.Resources.Texty.PStudent_DalsiLekceNadpis, eMsgBoxTlacitka.AnoNe) == DialogResult.No)
				{
					return;
				}
			}
		}
		ZobrazitZacitPsat();
	}

	private bool ZobrazitHlaskuNastavenaKlasifikace()
	{
		DialogResult dialogResult = ((!Student.KlasifikaceOpakovani) ? MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PStudent_msgboxKlasifPovolena, HYL.MountBlue.Resources.Texty.PStudent_msgboxKlasifikace_Title, eMsgBoxTlacitka.AnoNe) : MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PStudent_msgboxKlasifOpakovani, HYL.MountBlue.Resources.Texty.PStudent_msgboxKlasifikace_Title, eMsgBoxTlacitka.AnoNe));
		if (dialogResult == DialogResult.Yes)
		{
			ZobrazitDomu();
			ZobrazitMojeZnamky();
			return true;
		}
		return false;
	}

	private void ZobrazitHlaskuVystupDokoncen()
	{
		Student.PraveDokonceno = false;
		if (Student.VystupDokoncen)
		{
			string textZpravy = Text.TextMuzZena(base.Uzivatel.Pohlavi, HYL.MountBlue.Resources.Texty.Hora_VystupDokoncenM, HYL.MountBlue.Resources.Texty.Hora_VystupDokoncenZ);
			MsgBoxBublina.ZobrazitMessageBox(textZpravy, HYL.MountBlue.Resources.Texty.Hora_VystupDokoncenTitle, eMsgBoxTlacitka.OK);
		}
		else if (Student.VyukaDokoncena && MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.Hora_VyukaDokoncena, HYL.MountBlue.Resources.Texty.Hora_VyukaDokoncenaTitle, eMsgBoxTlacitka.AnoNe) == DialogResult.Yes)
		{
			ZobrazitMojeZnamky();
		}
	}

	internal void PrejitNaDalsiCviceni()
	{
		if (!Student.Cviceni.JeTrenink)
		{
			PrejitNaDalsiCviceni(new OznaceniCviceni(Student.Cviceni, Student.Cviceni.Cviceni + 1));
		}
		else
		{
			PrejitNaDalsiCviceni(new OznaceniCviceni(Student.Cviceni, int.MaxValue));
		}
	}

	internal void PrejitNaDalsiCviceni(char vetev)
	{
		PrejitNaDalsiCviceni(new OznaceniCviceni(Student.Cviceni, vetev));
	}

	private void PrejitNaDalsiCviceni(OznaceniCviceni navrhCviceni)
	{
		OznaceniCviceni oznaceniCviceni = _Lekce.Lekce().OveritCisloCviceni(navrhCviceni);
		if ((oznaceniCviceni == null || oznaceniCviceni.JeTrenink) && !Student.VyukaDokoncena)
		{
			if (_Lekce.Lekce().Klasifikace.KlasifikaceSplneny(Student, bPouzeUrgentni: false))
			{
				Student.NastavitVystupDokoncen();
			}
			else
			{
				Student.NastavitVyukaDokoncena();
			}
		}
		if (oznaceniCviceni != null)
		{
			if (!oznaceniCviceni.JeTrenink)
			{
				bPrechodDoDalsiLekce = Student.Cviceni.Lekce != oznaceniCviceni.Lekce;
			}
			Student.NastavitCviceni(oznaceniCviceni);
			InicializovatCviceni();
		}
		base.Uzivatel.Ulozit();
	}

	internal void ZobrazitCoUzUmim()
	{
		_Plocha.NactiPlochu(new KlavesniceInfo(this));
	}

	internal void ZobrazitMojeZnamky()
	{
		if (MojeZnamky.ZobrazitMojeZnamky(_Plocha.HlavniOkno, Student, out var CviceniID))
		{
			ZobrazitKlasifikaci(_Lekce.Lekce().Klasifikace[CviceniID]);
		}
	}

	internal void ZobrazitVyhodnoceni(HYL.MountBlue.Classes.Cviceni.Psani cvPsani, string sTextOpisu, int iPocetVterin)
	{
		_Plocha.NactiPlochu(new PsaniVysl(this, cvPsani, sTextOpisu, iPocetVterin));
	}

	internal void ZobrazitKlasifikaci(HYL.MountBlue.Classes.Cviceni.Psani cvKlasifikace)
	{
		if (Student.JeSpustenaLavina())
		{
			ZobrazitOknoLavina();
		}
		else
		{
			_Plocha.NactiPlochu(new HYL.MountBlue.Classes.Plocha.Psani(this, cvKlasifikace));
		}
	}

	protected abstract void ZobrazitOknoLavina();

	internal void ZobrazitSpravneSezeni()
	{
		_Plocha.NactiPlochu(new SezeniInfo(this));
	}

	public abstract string VygenerovatPostupovyKlic();

	public abstract ePostupovyKlic NacistPostupovyKlic(string klic);
}
