using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Uzivatele.Historie;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Dialogs;

namespace HYL.MountBlue.Classes.Uzivatele.Prihlasen;

internal class PStudentSkolni : PStudent
{
	internal StudentSkolni StudentSkolni => (StudentSkolni)base.Uzivatel;

	public PStudentSkolni(StudentSkolni stu)
		: base(stu)
	{
	}

	protected override void ZobrazitOknoLavina()
	{
		Lavina.ZobrazitLavinu(StudentSkolni);
	}

	protected override HYL.MountBlue.Dialogs.Uzivatel.eParametry ParametryDialoguNovy()
	{
		if (PUzivatele.PrihlasenyUzivatel is PAdmin)
		{
			return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)90u;
		}
		if (PUzivatele.PrihlasenyUzivatel is PUcitel)
		{
			return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)74u;
		}
		return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)0u;
	}

	protected override HYL.MountBlue.Dialogs.Uzivatel.eParametry ParametryDialoguUpravit()
	{
		if (PUzivatele.PrihlasenyUzivatel is PAdmin)
		{
			return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)49178u;
		}
		if (PUzivatele.PrihlasenyUzivatel is PUcitel)
		{
			return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)114698u;
		}
		return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)5536u;
	}

	public override void InicializovatUzivatele()
	{
		if (StudentSkolni.DomaciVyuka && !base.Student.VyukaDokoncena && StudentSkolni.JeNaMaxCviceni)
		{
			PostupovyKlic.ZobrazitPostupovyKlic(this, zadatKlic: true);
		}
		base.InicializovatUzivatele();
	}

	protected override void OdhlaseniUzivatele()
	{
		base.OdhlaseniUzivatele();
		if (StudentSkolni.DomaciVyuka && StudentSkolni.JeNaMaxCviceni)
		{
			PostupovyKlic.ZobrazitPostupovyKlic(this, zadatKlic: false);
		}
	}

	public override string VygenerovatPostupovyKlic()
	{
		_Cviceni cviceni = AktualniCviceni;
		if (cviceni.OznaceniCviceni.JeTrenink)
		{
			OznaceniCviceni oznCvic = new OznaceniCviceni(46);
			Zaznam[] array = base.Student.Historie.NacistHistoriiUzivatele();
			Zaznam[] array2 = array;
			foreach (Zaznam zaznam in array2)
			{
				if (zaznam is ZaznCviceni zaznCviceni && !zaznCviceni.OznaceniCviceni.JeTrenink)
				{
					oznCvic = zaznCviceni.OznaceniCviceni;
				}
			}
			cviceni = _Cviceni.NactiCviceni(oznCvic);
		}
		return Klice.GenerujKlic(base.Uzivatel.UID, cviceni.OznaceniCviceni.Lekce, cviceni.ID, ZeSkoly: true);
	}

	public override ePostupovyKlic NacistPostupovyKlic(string klic)
	{
		uint uid = 0u;
		int lekce = 0;
		uint cviceni = 0u;
		bool zeSkoly = false;
		if (Klice.OveritKlic(klic, ref uid, ref lekce, ref cviceni, ref zeSkoly))
		{
			if (uid != base.Student.UID)
			{
				return ePostupovyKlic.spatny;
			}
			if (zeSkoly)
			{
				return ePostupovyKlic.zeSkoly;
			}
			if (lekce < base.Student.Cviceni.Lekce)
			{
				return ePostupovyKlic.mensiNez;
			}
			if (!_Lekce.Lekce().NajitCisloCviceni(lekce, cviceni, out var vetev, out var cviceni2))
			{
				return ePostupovyKlic.spatny;
			}
			OznaceniCviceni oznaceniCviceni = new OznaceniCviceni(lekce, vetev, cviceni2);
			if (base.Student.Cviceni.Equals(oznaceniCviceni))
			{
				return ePostupovyKlic.stejny;
			}
			if (base.Student.Cviceni > oznaceniCviceni)
			{
				return ePostupovyKlic.mensiNez;
			}
			ZaznPrenosPostupu zaznam = new ZaznPrenosPostupu(base.Student.Cviceni, oznaceniCviceni, klic);
			base.Uzivatel.Historie.PridatZaznam(zaznam);
			base.Student.NastavitCviceni(oznaceniCviceni);
			return ePostupovyKlic.OK;
		}
		return ePostupovyKlic.spatny;
	}
}
