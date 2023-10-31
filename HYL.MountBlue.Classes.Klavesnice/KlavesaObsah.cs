using System;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Klavesnice;

internal class KlavesaObsah
{
	private char mchrZnak;

	private char mchrZnakShift;

	private KlavesaKlavesnice.eTypKlavesy mbytTyp;

	private KlavesaKlavesnice.eTypKlavesy mbytTypShift;

	private string mstrNazev;

	private string mstrNazevShift;

	internal char Znak => mchrZnak;

	internal char ZnakShift => mchrZnakShift;

	internal KlavesaKlavesnice.eTypKlavesy Typ => mbytTyp;

	internal KlavesaKlavesnice.eTypKlavesy TypShift => mbytTypShift;

	internal string Nazev => ZjistitNazevZnaku(mchrZnak, mstrNazev, mbytTyp);

	internal string NazevShift => ZjistitNazevZnaku(mchrZnakShift, mstrNazevShift, mbytTypShift);

	internal string Nadpis => ZjistitNadpisZnaku(Nazev, Typ);

	internal string NadpisShift => ZjistitNadpisZnaku(NazevShift, TypShift);

	internal KlavesaObsah()
	{
		throw new Exception("Veřejný konstruktor nelze použít pro založení instance třídy.");
	}

	private KlavesaObsah(char cZnak, char cZnakShift, KlavesaKlavesnice.eTypKlavesy byTyp, KlavesaKlavesnice.eTypKlavesy byTypShift)
	{
		mchrZnak = cZnak;
		mchrZnakShift = cZnakShift;
		mbytTyp = byTyp;
		mbytTypShift = byTypShift;
	}

	private KlavesaObsah(char cZnak, char cZnakShift, KlavesaKlavesnice.eTypKlavesy byTyp, KlavesaKlavesnice.eTypKlavesy byTypShift, string sNazev, string sNazevShift)
		: this(cZnak, cZnakShift, byTyp, byTypShift)
	{
		if (sNazev != null && sNazev.Length > 0)
		{
			mstrNazev = sNazev;
		}
		if (sNazevShift != null && sNazevShift.Length > 0)
		{
			mstrNazevShift = sNazevShift;
		}
	}

	private static string ZjistitNazevZnaku(char cZnak, string sNazev, KlavesaKlavesnice.eTypKlavesy byTyp)
	{
		if (sNazev != null && sNazev.Length > 0)
		{
			return sNazev;
		}
		if (byTyp == KlavesaKlavesnice.eTypKlavesy.cislovka)
		{
			return $"{cZnak}";
		}
		return $"'{cZnak}'";
	}

	private static string ZjistitNadpisZnaku(string sNazev, KlavesaKlavesnice.eTypKlavesy byTyp)
	{
		return byTyp switch
		{
			KlavesaKlavesnice.eTypKlavesy.znak => string.Format(HYL.MountBlue.Resources.Texty.Klavesa_znak, sNazev), 
			KlavesaKlavesnice.eTypKlavesy.pismeno => string.Format(HYL.MountBlue.Resources.Texty.Klavesa_pismeno, sNazev), 
			KlavesaKlavesnice.eTypKlavesy.cislovka => string.Format(HYL.MountBlue.Resources.Texty.Klavesa_cislovka, sNazev), 
			_ => string.Format(HYL.MountBlue.Resources.Texty.Klavesa_klavesa, sNazev), 
		};
	}

	public override string ToString()
	{
		if (Nazev == NazevShift)
		{
			return Nazev;
		}
		return Nazev + '/' + NazevShift;
	}

	internal static KlavesaObsah NactiDefiniciZakladni(KlavesaKlavesnice.eKlavesy klavesa)
	{
		return klavesa switch
		{
			KlavesaKlavesnice.eKlavesy.strednik_krouzek => new KlavesaObsah(';', '°', KlavesaKlavesnice.eTypKlavesy.znak, KlavesaKlavesnice.eTypKlavesy.znak, "; [středník]", "° [kroužek]"), 
			KlavesaKlavesnice.eKlavesy.plus_1 => new KlavesaObsah('+', '1', KlavesaKlavesnice.eTypKlavesy.znak, KlavesaKlavesnice.eTypKlavesy.cislovka, "+ [plus]", null), 
			KlavesaKlavesnice.eKlavesy.e_sHackem_2 => new KlavesaObsah('ě', '2', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.s_sHackem_3 => new KlavesaObsah('š', '3', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.c_sHackem_4 => new KlavesaObsah('č', '4', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.r_sHackem_5 => new KlavesaObsah('ř', '5', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.z_sHackem_6 => new KlavesaObsah('ž', '6', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.y_sDelkou_7 => new KlavesaObsah('ý', '7', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.a_sDelkou_8 => new KlavesaObsah('á', '8', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.i_sDelkou_9 => new KlavesaObsah('í', '9', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.e_sDelkou_0 => new KlavesaObsah('é', '0', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.cislovka), 
			KlavesaKlavesnice.eKlavesy.rovnaSe_procento => new KlavesaObsah('=', '%', KlavesaKlavesnice.eTypKlavesy.znak, KlavesaKlavesnice.eTypKlavesy.znak, "= [rovnítko]", "% [procento]"), 
			KlavesaKlavesnice.eKlavesy.delka_hacek => new KlavesaObsah('\u00b4', 'ˇ', KlavesaKlavesnice.eTypKlavesy.znak, KlavesaKlavesnice.eTypKlavesy.znak, "\u00b4 [délka]", "ˇ [háček]"), 
			KlavesaKlavesnice.eKlavesy.q_Q => new KlavesaObsah('q', 'Q', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.w_W => new KlavesaObsah('w', 'W', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.e_E => new KlavesaObsah('e', 'E', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.r_R => new KlavesaObsah('r', 'R', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.t_T => new KlavesaObsah('t', 'T', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.z_Z => new KlavesaObsah('z', 'Z', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.u_U => new KlavesaObsah('u', 'U', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.i_I => new KlavesaObsah('i', 'I', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.o_O => new KlavesaObsah('o', 'O', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.p_P => new KlavesaObsah('p', 'P', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.u_sDelkou_lomeno => new KlavesaObsah('ú', '/', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.znak, null, "/ [lomítko]"), 
			KlavesaKlavesnice.eKlavesy.prava_leva_zavorka => new KlavesaObsah(')', '(', KlavesaKlavesnice.eTypKlavesy.klavesa, KlavesaKlavesnice.eTypKlavesy.klavesa, ") [pravá závorka]", "( [levá závorka]"), 
			KlavesaKlavesnice.eKlavesy.a_A => new KlavesaObsah('a', 'A', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.s_S => new KlavesaObsah('s', 'S', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.d_D => new KlavesaObsah('d', 'D', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.f_F => new KlavesaObsah('f', 'F', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.g_G => new KlavesaObsah('g', 'G', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.h_H => new KlavesaObsah('h', 'H', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.j_J => new KlavesaObsah('j', 'J', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.k_K => new KlavesaObsah('k', 'K', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.l_L => new KlavesaObsah('l', 'L', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.u_sKrouzkem_uvozovky => new KlavesaObsah('ů', '"', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.klavesa, null, "\" [uvozovky]"), 
			KlavesaKlavesnice.eKlavesy.paragraf_vykricnik => new KlavesaObsah('§', '!', KlavesaKlavesnice.eTypKlavesy.znak, KlavesaKlavesnice.eTypKlavesy.znak, "§ [paragraf]", "! [vykřičník]"), 
			KlavesaKlavesnice.eKlavesy.y_Y => new KlavesaObsah('y', 'Y', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.x_X => new KlavesaObsah('x', 'X', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.c_C => new KlavesaObsah('c', 'C', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.v_V => new KlavesaObsah('v', 'V', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.b_B => new KlavesaObsah('b', 'B', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.n_N => new KlavesaObsah('n', 'N', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.m_M => new KlavesaObsah('m', 'M', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.carka_otaznik => new KlavesaObsah(',', '?', KlavesaKlavesnice.eTypKlavesy.znak, KlavesaKlavesnice.eTypKlavesy.znak, ", [čárka]", "? [otazník]"), 
			KlavesaKlavesnice.eKlavesy.tecka_dvojtecka => new KlavesaObsah('.', ':', KlavesaKlavesnice.eTypKlavesy.znak, KlavesaKlavesnice.eTypKlavesy.znak, ". [tečka]", ": [dvojtečka]"), 
			KlavesaKlavesnice.eKlavesy.pomlcka_podtrzitko => new KlavesaObsah('-', '_', KlavesaKlavesnice.eTypKlavesy.znak, KlavesaKlavesnice.eTypKlavesy.znak, "- [pomlčka, mínus]", "_ [podtržítko]"), 
			KlavesaKlavesnice.eKlavesy.mezernik => new KlavesaObsah(' ', ' ', KlavesaKlavesnice.eTypKlavesy.klavesa, KlavesaKlavesnice.eTypKlavesy.klavesa, "[mezera]", "[mezera]"), 
			_ => null, 
		};
	}

	internal static KlavesaObsah NactiDefiniciDelka(KlavesaKlavesnice.eKlavesy klavesa)
	{
		return klavesa switch
		{
			KlavesaKlavesnice.eKlavesy.e_E => new KlavesaObsah('é', 'É', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.u_U => new KlavesaObsah('ú', 'Ú', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.i_I => new KlavesaObsah('í', 'Í', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.o_O => new KlavesaObsah('ó', 'Ó', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.a_A => new KlavesaObsah('á', 'Á', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.y_Y => new KlavesaObsah('ý', 'Ý', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			_ => null, 
		};
	}

	internal static KlavesaObsah NactiDefiniciHacek(KlavesaKlavesnice.eKlavesy klavesa)
	{
		return klavesa switch
		{
			KlavesaKlavesnice.eKlavesy.e_E => new KlavesaObsah('ě', 'Ě', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.r_R => new KlavesaObsah('ř', 'Ř', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.t_T => new KlavesaObsah('ť', 'Ť', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.z_Z => new KlavesaObsah('ž', 'Ž', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.s_S => new KlavesaObsah('š', 'Š', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.d_D => new KlavesaObsah('ď', 'Ď', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.c_C => new KlavesaObsah('č', 'Č', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			KlavesaKlavesnice.eKlavesy.n_N => new KlavesaObsah('ň', 'Ň', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno), 
			_ => null, 
		};
	}

	internal static KlavesaObsah NactiDefiniciKrouzek(KlavesaKlavesnice.eKlavesy klavesa)
	{
		KlavesaKlavesnice.eKlavesy eKlavesy = klavesa;
		if (eKlavesy == KlavesaKlavesnice.eKlavesy.u_U)
		{
			return new KlavesaObsah('ů', 'Ů', KlavesaKlavesnice.eTypKlavesy.pismeno, KlavesaKlavesnice.eTypKlavesy.pismeno);
		}
		return null;
	}
}
