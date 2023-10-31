using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Properties;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Uzivatele.Prihlasen;

internal static class PUzivatele
{
	private static PUzivatel prihlasenyUzivatel;

	internal static bool JeUzivatelPrihlaseny => PrihlasenyUzivatel != null;

	internal static PUzivatel PrihlasenyUzivatel => prihlasenyUzivatel;

	internal static string UzivJmenoPosledniho
	{
		get
		{
			return Settings.Default.UzivJmeno;
		}
		set
		{
			Settings.Default.UzivJmeno = value;
			Settings.Default.Save();
		}
	}

	internal static bool SkrytDokonceneVystupy
	{
		get
		{
			return Settings.Default.SkrytDokoncene;
		}
		set
		{
			Settings.Default.SkrytDokoncene = value;
			Settings.Default.Save();
		}
	}

	internal static void ZobrazitPrihlaseni()
	{
		_Plocha.NactiPlochu(new PrihlasitSkolni());
	}

	internal static void PrihlasitUzivatele(string uzivJmeno)
	{
		HYL.MountBlue.Classes.Klient.Klient.Stanice.PrihlasitUzivatele(uzivJmeno);
		HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uz = HYL.MountBlue.Classes.Klient.Klient.Stanice.PrihlasenyUzivatel;
		prihlasenyUzivatel = UzivatelPodleTypu(uz);
		UzivJmenoPosledniho = uzivJmeno;
		PrihlasenyUzivatel.InicializovatUzivatele();
	}

	internal static PUzivatel UzivatelPodleTypu(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uz)
	{
		if (uz is Admin)
		{
			return new PAdmin((Admin)uz);
		}
		if (uz is Ucitel)
		{
			return new PUcitel((Ucitel)uz);
		}
		if (uz is StudentSkolni)
		{
			return new PStudentSkolni((StudentSkolni)uz);
		}
		return null;
	}

	internal static void OdhlasitUzivatele(bool bZobrazitPrihlaseni)
	{
		HYL.MountBlue.Classes.Klient.Klient.Stanice.OdhlasitUzivatele();
		prihlasenyUzivatel = null;
		if (bZobrazitPrihlaseni)
		{
			ZobrazitPrihlaseni();
		}
	}

	internal static bool PridatStudenta(out uint uid, int tridaID)
	{
		uid = 0u;
		if (!KontrolaMaximaUzivatelu())
		{
			return false;
		}
		StudentSkolni studentSkolni = (StudentSkolni)HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele.VytvoritNovehoUzivatele(Uzivatele.eTypNovehoUzivatele.StudentSkolni);
		studentSkolni.TridaID = tridaID;
		PStudentSkolni pStudentSkolni = new PStudentSkolni(studentSkolni);
		bool flag = pStudentSkolni.ZobrazitNovyUzivatel();
		if (flag)
		{
			studentSkolni.AvatarCislo = HYL.MountBlue.Classes.Grafika.Avatary.VychoziAvatar(studentSkolni.Pohlavi);
		}
		else
		{
			HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele.OdebratUzivatele(studentSkolni.UID);
		}
		uid = studentSkolni.UID;
		return flag;
	}

	internal static bool PridatUcitele(out uint uid)
	{
		uid = 0u;
		if (!KontrolaMaximaUzivatelu())
		{
			return false;
		}
		Ucitel ucitel = (Ucitel)HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele.VytvoritNovehoUzivatele(Uzivatele.eTypNovehoUzivatele.Ucitel);
		PUcitel pUcitel = new PUcitel(ucitel);
		bool flag = pUcitel.ZobrazitNovyUzivatel();
		if (!flag)
		{
			HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele.OdebratUzivatele(ucitel.UID);
		}
		uid = ucitel.UID;
		return flag;
	}

	internal static bool PridatStudentaZeSkoly(out uint uid)
	{
		uid = 0u;
		if (!KontrolaMaximaUzivatelu())
		{
			return false;
		}
		uid = 0u;
		return false;
	}

	private static bool KontrolaMaximaUzivatelu()
	{
		if (HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele.PocetUzivatelu <= 5000)
		{
			return true;
		}
		MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Uzivatele_msgMaxPocet, 5000), HYL.MountBlue.Resources.Texty.Uzivatele_msgMaxPocet_Title, eMsgBoxTlacitka.OK);
		return false;
	}
}
