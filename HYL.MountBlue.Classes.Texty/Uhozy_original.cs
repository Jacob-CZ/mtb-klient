namespace HYL.MountBlue.Classes.Texty;

internal class Uhozy_original
{
	private enum eStavyLA
	{
		qS,
		qA,
		qC,
		qD,
		qL,
		qR,
		qLC,
		qRC,
		qM
	}

	internal delegate void NoveSlovo(string TextSlova, int ZacatekSlova, int PocetUhozu);

	private const string _Lruka = "qwertasdfgyxcvběščřž";

	private const string _Rruka = "zuiopúhjklůnm-ýáíé";

	private const string _LrukaShift = "QWERTASDFGYXCVB";

	private const string _RrukaShift = "ZUIOPHJKLNM_";

	private const string _LrukaCisla = "123456";

	private const string _RrukaCisla = "7890";

	private const string _XX = "óëäüö";

	private const string _XL = "ÓÍÚÜÖ";

	private const string _XR = "ÝÁÉËÄ";

	private const string _LX = "ňťďľ";

	private const string _LL = "ŽŇĽ";

	private const string _LR = "ĚŠČŘŤĎ";

	private const string _RL = "Ů";

	private const string _odd1 = ";+)§,.=\u00b4\u00a8";

	private const string _odd2 = "°/(!?:%ˇ'\"";

	private const string _oddCisla = ",.";

	internal const char NezlomitelnaMezera = '~';

	internal const char Mezera = ' ';

	private static readonly string _mezera = ' '.ToString() + '~';

	internal static readonly string PovoleneZnaky = "qwertasdfgyxcvběščřžzuiopúhjklůnm-ýáíéQWERTASDFGYXCVBZUIOPHJKLNM_1234567890óëäüöÓÍÚÜÖÝÁÉËÄňťďľŽŇĽĚŠČŘŤĎŮ;+)§,.=\u00b4\u00a8°/(!?:%ˇ'\",." + ' ' + '~';

	private string mstrVstup;

	private int mintPozice;

	private char mchrZnak;

	private int mintZacatekSlova;

	private int mintPocetUhozu;

	private eStavyLA mintStavLA;

	private string mstrNeznameZnaky;

	private string strSlovo;

	private NoveSlovo mdelegNoveSlovo;

	internal string NeznameZnaky => mstrNeznameZnaky;

	internal Uhozy_original(string VstupniVeta, ref NoveSlovo UdalostNoveSlovo)
	{
		mdelegNoveSlovo = UdalostNoveSlovo;
		AnalyzujVetu(VstupniVeta);
	}

	private void AnalyzujVetu(string VstupniVeta)
	{
		mstrVstup = VstupniVeta;
		mintPozice = 0;
		mintZacatekSlova = 0;
		mintPocetUhozu = 0;
		mintStavLA = eStavyLA.qS;
		mstrNeznameZnaky = string.Empty;
		strSlovo = string.Empty;
		while (GetNextChar())
		{
			switch (mintStavLA)
			{
			case eStavyLA.qS:
				vetaS();
				break;
			case eStavyLA.qA:
				vetaA();
				break;
			case eStavyLA.qC:
				vetaC();
				break;
			case eStavyLA.qD:
				vetaD();
				break;
			case eStavyLA.qL:
				vetaL();
				break;
			case eStavyLA.qR:
				vetaR();
				break;
			case eStavyLA.qLC:
				vetaLC();
				break;
			case eStavyLA.qRC:
				vetaRC();
				break;
			case eStavyLA.qM:
				vetaM();
				break;
			}
		}
		zacitNoveSlovo();
	}

	private bool GetNextChar()
	{
		if (mintPozice >= mstrVstup.Length || mstrVstup.Length == 0)
		{
			mintPozice = mstrVstup.Length;
			mchrZnak = '\0';
			return false;
		}
		mchrZnak = mstrVstup[mintPozice];
		mintPozice++;
		return true;
	}

	private void zacitNoveSlovo()
	{
		if (strSlovo.Length > 0)
		{
			mdelegNoveSlovo(strSlovo, mintZacatekSlova, mintPocetUhozu);
			strSlovo = string.Empty;
			mintZacatekSlova = mintPozice - 1;
			mintPocetUhozu = 0;
		}
	}

	private bool analyzaZnaku(string vetaKporovnani, int pricistPocetUhozu, eStavyLA novyStavLA, bool noveSlovo)
	{
		if (vetaKporovnani.IndexOf(mchrZnak) >= 0)
		{
			analyzaZnaku(pricistPocetUhozu, novyStavLA, noveSlovo);
			return true;
		}
		return false;
	}

	private void analyzaZnaku(int pricistPocetUhozu, eStavyLA novyStavLA, bool noveSlovo)
	{
		if (mintStavLA == eStavyLA.qD && novyStavLA != eStavyLA.qLC && novyStavLA != eStavyLA.qRC)
		{
			if (strSlovo.Length == 1)
			{
				zacitNoveSlovo();
			}
			else if (strSlovo.Length > 1)
			{
				char c = strSlovo[strSlovo.Length - 1];
				strSlovo = strSlovo.Substring(0, strSlovo.Length - 1);
				mintPozice--;
				mintPocetUhozu--;
				zacitNoveSlovo();
				mintPozice++;
				mintPocetUhozu++;
				strSlovo = c.ToString();
			}
		}
		mintStavLA = novyStavLA;
		if (noveSlovo)
		{
			zacitNoveSlovo();
		}
		mintPocetUhozu += pricistPocetUhozu;
		if (pricistPocetUhozu > 0)
		{
			strSlovo += mchrZnak;
		}
	}

	private void vetaS()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("QWERTASDFGYXCVB", 2, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("ZUIOPHJKLNM_", 2, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("123456", 2, eStavyLA.qRC, noveSlovo: true) && !analyzaZnaku("7890", 2, eStavyLA.qLC, noveSlovo: true) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("ňťďľ", 3, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("ŽŇĽ", 3, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("ĚŠČŘŤĎ", 4, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("Ů", 4, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qM, noveSlovo: true))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}

	private void vetaA()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("QWERTASDFGYXCVB", 2, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ZUIOPHJKLNM_", 2, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("123456", 2, eStavyLA.qRC, noveSlovo: true) && !analyzaZnaku("7890", 2, eStavyLA.qLC, noveSlovo: true) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ňťďľ", 3, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ŽŇĽ", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ĚŠČŘŤĎ", 4, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("Ů", 4, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qM, noveSlovo: true))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}

	private void vetaL()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("QWERTASDFGYXCVB", 2, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ZUIOPHJKLNM_", 1, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("123456", 2, eStavyLA.qRC, noveSlovo: true) && !analyzaZnaku("7890", 1, eStavyLA.qLC, noveSlovo: true) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ňťďľ", 2, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ŽŇĽ", 2, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ĚŠČŘŤĎ", 3, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("Ů", 4, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qM, noveSlovo: true))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}

	private void vetaR()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("QWERTASDFGYXCVB", 1, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ZUIOPHJKLNM_", 2, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("123456", 1, eStavyLA.qRC, noveSlovo: true) && !analyzaZnaku("7890", 2, eStavyLA.qLC, noveSlovo: true) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ňťďľ", 3, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ŽŇĽ", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ĚŠČŘŤĎ", 4, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("Ů", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qM, noveSlovo: true))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}

	private void vetaC()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("QWERTASDFGYXCVB", 2, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("ZUIOPHJKLNM_", 2, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("123456", 2, eStavyLA.qRC, noveSlovo: false) && !analyzaZnaku("7890", 2, eStavyLA.qLC, noveSlovo: false) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("ňťďľ", 3, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("ŽŇĽ", 3, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("ĚŠČŘŤĎ", 4, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("Ů", 4, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qC, noveSlovo: false))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}

	private void vetaD()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("QWERTASDFGYXCVB", 2, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("ZUIOPHJKLNM_", 2, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("123456", 2, eStavyLA.qRC, noveSlovo: false) && !analyzaZnaku("7890", 2, eStavyLA.qLC, noveSlovo: false) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("ňťďľ", 3, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("ŽŇĽ", 3, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("ĚŠČŘŤĎ", 4, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("Ů", 4, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qM, noveSlovo: true))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}

	private void vetaLC()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("QWERTASDFGYXCVB", 2, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ZUIOPHJKLNM_", 1, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("123456", 2, eStavyLA.qRC, noveSlovo: false) && !analyzaZnaku("7890", 1, eStavyLA.qLC, noveSlovo: false) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ňťďľ", 2, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ŽŇĽ", 2, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ĚŠČŘŤĎ", 3, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("Ů", 4, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(",.", 1, eStavyLA.qD, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qC, noveSlovo: false))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}

	private void vetaRC()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("QWERTASDFGYXCVB", 1, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ZUIOPHJKLNM_", 2, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("123456", 1, eStavyLA.qRC, noveSlovo: false) && !analyzaZnaku("7890", 2, eStavyLA.qLC, noveSlovo: false) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("ňťďľ", 3, eStavyLA.qA, noveSlovo: false) && !analyzaZnaku("ŽŇĽ", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("ĚŠČŘŤĎ", 4, eStavyLA.qR, noveSlovo: false) && !analyzaZnaku("Ů", 3, eStavyLA.qL, noveSlovo: false) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(",.", 1, eStavyLA.qD, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qC, noveSlovo: false))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}

	private void vetaM()
	{
		if (!analyzaZnaku("qwertasdfgyxcvběščřž", 1, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("zuiopúhjklůnm-ýáíé", 1, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("QWERTASDFGYXCVB", 2, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("ZUIOPHJKLNM_", 2, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("123456", 2, eStavyLA.qRC, noveSlovo: true) && !analyzaZnaku("7890", 2, eStavyLA.qLC, noveSlovo: true) && !analyzaZnaku("óëäüö", 2, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("ÓÍÚÜÖ", 3, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("ÝÁÉËÄ", 3, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("ňťďľ", 3, eStavyLA.qA, noveSlovo: true) && !analyzaZnaku("ŽŇĽ", 3, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("ĚŠČŘŤĎ", 4, eStavyLA.qR, noveSlovo: true) && !analyzaZnaku("Ů", 4, eStavyLA.qL, noveSlovo: true) && !analyzaZnaku("\t", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\n", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("\r", 0, eStavyLA.qS, noveSlovo: false) && !analyzaZnaku(";+)§,.=\u00b4\u00a8", 1, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku("°/(!?:%ˇ'\"", 2, eStavyLA.qS, noveSlovo: true) && !analyzaZnaku(_mezera, 1, eStavyLA.qM, noveSlovo: false))
		{
			analyzaZnaku(1, eStavyLA.qS, noveSlovo: true);
			mstrNeznameZnaky += mchrZnak;
		}
	}
}
