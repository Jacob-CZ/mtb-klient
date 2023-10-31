using System.Drawing;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Klavesnice;

internal class KlavesaKlavesnice
{
	internal enum eKlavesy : byte
	{
		strednik_krouzek,
		plus_1,
		e_sHackem_2,
		s_sHackem_3,
		c_sHackem_4,
		r_sHackem_5,
		z_sHackem_6,
		y_sDelkou_7,
		a_sDelkou_8,
		i_sDelkou_9,
		e_sDelkou_0,
		rovnaSe_procento,
		delka_hacek,
		backspace,
		q_Q,
		w_W,
		e_E,
		r_R,
		t_T,
		z_Z,
		u_U,
		i_I,
		o_O,
		p_P,
		u_sDelkou_lomeno,
		prava_leva_zavorka,
		enter,
		a_A,
		s_S,
		d_D,
		f_F,
		g_G,
		h_H,
		j_J,
		k_K,
		l_L,
		u_sKrouzkem_uvozovky,
		paragraf_vykricnik,
		levyShift,
		y_Y,
		x_X,
		c_C,
		v_V,
		b_B,
		n_N,
		m_M,
		carka_otaznik,
		tecka_dvojtecka,
		pomlcka_podtrzitko,
		pravyShift,
		mezernik
	}

	internal enum eShift : byte
	{
		zadny,
		levy,
		pravy
	}

	internal enum eModifikatory : byte
	{
		zadne = 0,
		delka = 2,
		hacek = 4,
		krouzek = 8
	}

	internal enum eBezobsahoveKlavesy : byte
	{
		LevyShift = 38,
		PravyShift = 49,
		Backspace = 13,
		Enter = 26
	}

	internal enum eTypKlavesy : byte
	{
		klavesa,
		znak,
		pismeno,
		cislovka
	}

	internal const eKlavesy CISLO_KLAVESY_MIN = eKlavesy.strednik_krouzek;

	internal const eKlavesy CISLO_KLAVESY_MAX = eKlavesy.mezernik;

	internal static readonly Point[] BodyKlavesyEnter = new Point[6]
	{
		new Point(481, 50),
		new Point(536, 50),
		new Point(536, 121),
		new Point(457, 121),
		new Point(457, 85),
		new Point(481, 85)
	};

	private eKlavesy mbytKlavesa;

	private Prsty.ePrst mbytPrst;

	private Rectangle mrctOblast;

	private eShift mbytShift;

	private eModifikatory mbytModifikatory;

	private KlavesaObsah mobjZakladni;

	private KlavesaObsah mobjDelka;

	private KlavesaObsah mobjHacek;

	private KlavesaObsah mobjKrouzek;

	internal eKlavesy Klavesa => mbytKlavesa;

	internal Prsty.ePrst Prst => mbytPrst;

	internal eShift Shift => mbytShift;

	internal eModifikatory Modifikatory => mbytModifikatory;

	internal Rectangle OblastVobrazku => mrctOblast;

	internal Point StredOblastiVobrazku
	{
		get
		{
			if (mrctOblast.IsEmpty)
			{
				eBezobsahoveKlavesy klavesa = (eBezobsahoveKlavesy)Klavesa;
				if (klavesa == eBezobsahoveKlavesy.Enter)
				{
					return new Point(509, 100);
				}
			}
			return new Point(mrctOblast.Left + mrctOblast.Width / 2, mrctOblast.Top + mrctOblast.Height / 2);
		}
	}

	internal bool MaZakladni => mobjZakladni != null;

	internal bool MaDelku => mobjDelka != null;

	internal bool MaHacek => mobjHacek != null;

	internal bool MaKrouzek => mobjKrouzek != null;

	internal KlavesaKlavesnice(eKlavesy byKlavesa)
	{
		NactiDefinici(byKlavesa);
		mobjZakladni = KlavesaObsah.NactiDefiniciZakladni(byKlavesa);
		mobjDelka = KlavesaObsah.NactiDefiniciDelka(byKlavesa);
		mobjHacek = KlavesaObsah.NactiDefiniciHacek(byKlavesa);
		mobjKrouzek = KlavesaObsah.NactiDefiniciKrouzek(byKlavesa);
	}

	internal KlavesaObsah ObsahZakladni()
	{
		return mobjZakladni;
	}

	internal KlavesaObsah ObsahDelka()
	{
		return mobjDelka;
	}

	internal KlavesaObsah ObsahHacek()
	{
		return mobjHacek;
	}

	internal KlavesaObsah ObsahKrouzek()
	{
		return mobjKrouzek;
	}

	internal bool JeBezobsahovaKlavesa()
	{
		switch ((eBezobsahoveKlavesy)Klavesa)
		{
		case eBezobsahoveKlavesy.Backspace:
		case eBezobsahoveKlavesy.Enter:
		case eBezobsahoveKlavesy.LevyShift:
		case eBezobsahoveKlavesy.PravyShift:
			return true;
		default:
			return false;
		}
	}

	internal bool JeBezobsahovaKlavesa(eBezobsahoveKlavesy typ)
	{
		return (uint)Klavesa == (uint)typ;
	}

	internal void VykreslitKlavesu(Graphics G, Image obrazek)
	{
		if (mrctOblast.IsEmpty)
		{
			eBezobsahoveKlavesy klavesa = (eBezobsahoveKlavesy)Klavesa;
			if (klavesa == eBezobsahoveKlavesy.Enter)
			{
				Brush brush = new TextureBrush(obrazek);
				G.FillPolygon(brush, BodyKlavesyEnter);
			}
		}
		else
		{
			G.DrawImage(obrazek, mrctOblast, mrctOblast, GraphicsUnit.Pixel);
		}
	}

	internal string Nadpis(eModifikatory byModifikator)
	{
		if (JeBezobsahovaKlavesa())
		{
			return NadpisBezobsahoveKlavesy((eBezobsahoveKlavesy)Klavesa);
		}
		return byModifikator switch
		{
			eModifikatory.zadne => ObsahZakladni().Nadpis, 
			eModifikatory.delka => ObsahDelka().Nadpis, 
			eModifikatory.hacek => ObsahHacek().Nadpis, 
			eModifikatory.krouzek => ObsahKrouzek().Nadpis, 
			_ => string.Empty, 
		};
	}

	internal string NadpisShift(eModifikatory byModifikator)
	{
		if (JeBezobsahovaKlavesa())
		{
			return NadpisBezobsahoveKlavesy((eBezobsahoveKlavesy)Klavesa);
		}
		return byModifikator switch
		{
			eModifikatory.zadne => ObsahZakladni().NadpisShift, 
			eModifikatory.delka => ObsahDelka().NadpisShift, 
			eModifikatory.hacek => ObsahHacek().NadpisShift, 
			eModifikatory.krouzek => ObsahKrouzek().NadpisShift, 
			_ => string.Empty, 
		};
	}

	internal string Nazev(eModifikatory byModifikator)
	{
		if (JeBezobsahovaKlavesa())
		{
			return NazevBezobsahoveKlavesy((eBezobsahoveKlavesy)Klavesa);
		}
		return byModifikator switch
		{
			eModifikatory.zadne => ObsahZakladni().Nazev, 
			eModifikatory.delka => ObsahDelka().Nazev, 
			eModifikatory.hacek => ObsahHacek().Nazev, 
			eModifikatory.krouzek => ObsahKrouzek().Nazev, 
			_ => string.Empty, 
		};
	}

	internal string NazevShift(eModifikatory byModifikator)
	{
		if (JeBezobsahovaKlavesa())
		{
			return NazevBezobsahoveKlavesy((eBezobsahoveKlavesy)Klavesa);
		}
		return byModifikator switch
		{
			eModifikatory.zadne => ObsahZakladni().NazevShift, 
			eModifikatory.delka => ObsahDelka().NazevShift, 
			eModifikatory.hacek => ObsahHacek().NazevShift, 
			eModifikatory.krouzek => ObsahKrouzek().NazevShift, 
			_ => string.Empty, 
		};
	}

	internal static string NadpisBezobsahoveKlavesy(eBezobsahoveKlavesy byBezobsahovaKlavesa)
	{
		return string.Format(HYL.MountBlue.Resources.Texty.Klavesa_klavesa, NazevBezobsahoveKlavesy(byBezobsahovaKlavesa));
	}

	internal static string NazevBezobsahoveKlavesy(eBezobsahoveKlavesy byBezobsahovaKlavesa)
	{
		return byBezobsahovaKlavesa switch
		{
			eBezobsahoveKlavesy.LevyShift => HYL.MountBlue.Resources.Texty.Klavesa_levyShift, 
			eBezobsahoveKlavesy.PravyShift => HYL.MountBlue.Resources.Texty.Klavesa_pravyShift, 
			eBezobsahoveKlavesy.Enter => HYL.MountBlue.Resources.Texty.Klavesa_enter, 
			eBezobsahoveKlavesy.Backspace => HYL.MountBlue.Resources.Texty.Klavesa_backspace, 
			_ => string.Empty, 
		};
	}

	public override string ToString()
	{
		if (JeBezobsahovaKlavesa())
		{
			return NazevBezobsahoveKlavesy((eBezobsahoveKlavesy)Klavesa);
		}
		if (MaZakladni)
		{
			return ObsahZakladni().ToString();
		}
		return string.Empty;
	}

	private void NactiDefinici(eKlavesy klavesa)
	{
		mbytKlavesa = klavesa;
		switch (klavesa)
		{
		case eKlavesy.strednik_krouzek:
			mbytPrst = Prsty.ePrst.levy_malik;
			mrctOblast = new Rectangle(17, 15, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.plus_1:
			mbytPrst = Prsty.ePrst.levy_malik;
			mrctOblast = new Rectangle(52, 15, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.e_sHackem_2:
			mbytPrst = Prsty.ePrst.levy_malik;
			mrctOblast = new Rectangle(87, 15, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.s_sHackem_3:
			mbytPrst = Prsty.ePrst.levy_prstenik;
			mrctOblast = new Rectangle(121, 15, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.c_sHackem_4:
			mbytPrst = Prsty.ePrst.levy_prostrednik;
			mrctOblast = new Rectangle(156, 15, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.r_sHackem_5:
			mbytPrst = Prsty.ePrst.levy_ukazovak;
			mrctOblast = new Rectangle(190, 15, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.z_sHackem_6:
			mbytPrst = Prsty.ePrst.levy_ukazovak;
			mrctOblast = new Rectangle(225, 15, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.y_sDelkou_7:
			mbytPrst = Prsty.ePrst.pravy_ukazovak;
			mrctOblast = new Rectangle(259, 15, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.a_sDelkou_8:
			mbytPrst = Prsty.ePrst.pravy_ukazovak;
			mrctOblast = new Rectangle(293, 15, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.i_sDelkou_9:
			mbytPrst = Prsty.ePrst.pravy_prostrednik;
			mrctOblast = new Rectangle(328, 15, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.e_sDelkou_0:
			mbytPrst = Prsty.ePrst.pravy_prstenik;
			mrctOblast = new Rectangle(362, 15, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.rovnaSe_procento:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(397, 15, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.delka_hacek:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(431, 15, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.backspace:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(466, 15, 70, 35);
			mbytShift = eShift.zadny;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.q_Q:
			mbytPrst = Prsty.ePrst.levy_malik;
			mrctOblast = new Rectangle(67, 50, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.w_W:
			mbytPrst = Prsty.ePrst.levy_prstenik;
			mrctOblast = new Rectangle(102, 50, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.e_E:
			mbytPrst = Prsty.ePrst.levy_prostrednik;
			mrctOblast = new Rectangle(136, 50, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = (eModifikatory)6;
			break;
		case eKlavesy.r_R:
			mbytPrst = Prsty.ePrst.levy_ukazovak;
			mrctOblast = new Rectangle(171, 50, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.hacek;
			break;
		case eKlavesy.t_T:
			mbytPrst = Prsty.ePrst.levy_ukazovak;
			mrctOblast = new Rectangle(205, 50, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.hacek;
			break;
		case eKlavesy.z_Z:
			mbytPrst = Prsty.ePrst.pravy_ukazovak;
			mrctOblast = new Rectangle(240, 50, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.hacek;
			break;
		case eKlavesy.u_U:
			mbytPrst = Prsty.ePrst.pravy_ukazovak;
			mrctOblast = new Rectangle(274, 50, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = (eModifikatory)10;
			break;
		case eKlavesy.i_I:
			mbytPrst = Prsty.ePrst.pravy_prostrednik;
			mrctOblast = new Rectangle(309, 50, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.delka;
			break;
		case eKlavesy.o_O:
			mbytPrst = Prsty.ePrst.pravy_prstenik;
			mrctOblast = new Rectangle(343, 50, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.delka;
			break;
		case eKlavesy.p_P:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(377, 50, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.u_sDelkou_lomeno:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(412, 50, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.prava_leva_zavorka:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(446, 50, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.enter:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(0, 0, 0, 0);
			mbytShift = eShift.zadny;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.a_A:
			mbytPrst = Prsty.ePrst.levy_malik;
			mrctOblast = new Rectangle(77, 85, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.delka;
			break;
		case eKlavesy.s_S:
			mbytPrst = Prsty.ePrst.levy_prstenik;
			mrctOblast = new Rectangle(111, 85, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.hacek;
			break;
		case eKlavesy.d_D:
			mbytPrst = Prsty.ePrst.levy_prostrednik;
			mrctOblast = new Rectangle(146, 85, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.hacek;
			break;
		case eKlavesy.f_F:
			mbytPrst = Prsty.ePrst.levy_ukazovak;
			mrctOblast = new Rectangle(180, 85, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.g_G:
			mbytPrst = Prsty.ePrst.levy_ukazovak;
			mrctOblast = new Rectangle(215, 85, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.h_H:
			mbytPrst = Prsty.ePrst.pravy_ukazovak;
			mrctOblast = new Rectangle(249, 85, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.j_J:
			mbytPrst = Prsty.ePrst.pravy_ukazovak;
			mrctOblast = new Rectangle(283, 85, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.k_K:
			mbytPrst = Prsty.ePrst.pravy_prostrednik;
			mrctOblast = new Rectangle(318, 85, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.l_L:
			mbytPrst = Prsty.ePrst.pravy_prstenik;
			mrctOblast = new Rectangle(352, 85, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.u_sKrouzkem_uvozovky:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(387, 85, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.paragraf_vykricnik:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(421, 85, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.levyShift:
			mbytPrst = Prsty.ePrst.levy_malik;
			mrctOblast = new Rectangle(10, 121, 91, 35);
			mbytShift = eShift.zadny;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.y_Y:
			mbytPrst = Prsty.ePrst.levy_malik;
			mrctOblast = new Rectangle(101, 121, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.delka;
			break;
		case eKlavesy.x_X:
			mbytPrst = Prsty.ePrst.levy_prstenik;
			mrctOblast = new Rectangle(136, 121, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.c_C:
			mbytPrst = Prsty.ePrst.levy_prostrednik;
			mrctOblast = new Rectangle(170, 121, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.hacek;
			break;
		case eKlavesy.v_V:
			mbytPrst = Prsty.ePrst.levy_ukazovak;
			mrctOblast = new Rectangle(205, 121, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.b_B:
			mbytPrst = Prsty.ePrst.levy_ukazovak;
			mrctOblast = new Rectangle(239, 121, 35, 35);
			mbytShift = eShift.pravy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.n_N:
			mbytPrst = Prsty.ePrst.pravy_ukazovak;
			mrctOblast = new Rectangle(274, 121, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.hacek;
			break;
		case eKlavesy.m_M:
			mbytPrst = Prsty.ePrst.pravy_ukazovak;
			mrctOblast = new Rectangle(308, 121, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.carka_otaznik:
			mbytPrst = Prsty.ePrst.pravy_prostrednik;
			mrctOblast = new Rectangle(342, 121, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.tecka_dvojtecka:
			mbytPrst = Prsty.ePrst.pravy_prstenik;
			mrctOblast = new Rectangle(377, 121, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.pomlcka_podtrzitko:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(411, 121, 35, 35);
			mbytShift = eShift.levy;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.pravyShift:
			mbytPrst = Prsty.ePrst.pravy_malik;
			mrctOblast = new Rectangle(446, 121, 91, 35);
			mbytShift = eShift.zadny;
			mbytModifikatory = eModifikatory.zadne;
			break;
		case eKlavesy.mezernik:
			mbytPrst = Prsty.ePrst.palec;
			mrctOblast = new Rectangle(171, 157, 206, 35);
			mbytShift = eShift.zadny;
			mbytModifikatory = eModifikatory.zadne;
			break;
		}
	}
}
