using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using HYL.MountBlue.Controls;

namespace HYL.MountBlue.Classes.Texty;

internal class VetaKresleniRT : VetaKresleni
{
	internal delegate void ZobrazitVarovneOknoDelegate(bool bBudeZacinatOdZacatku, bool bDosazenoMaxDelky);

	internal delegate void PosunNaRadekDelegate(int iCisloRadku);

	internal delegate void VyhodnotitDelegate();

	internal delegate void ZacalPsatDelegate();

	internal delegate void PripravenDelegate();

	internal delegate void PridanZnakDelegate(char znak);

	internal const int RychlostBlikani = 650;

	internal const int AutomatickeVyhodnoceni = 4;

	internal const int InterniCasovac = 200;

	internal const int MaxPocetStejnychZnaku = 10;

	private Timer mtmrKurzor;

	private Timer mtmrCasovac;

	private bool mbolBackspace;

	private bool mbolKlasifikace;

	private bool mbolSlepaObrazovka;

	private StringBuilder mstbText;

	private StringBuilder mstbSlepyText;

	private Pen mpenKurzor;

	private bool mbolKurzor;

	private int mintPocetVterin;

	private int mintPocetVterinNeaktivni;

	private int mintMaxDelka;

	private int mrefUTextBox_KeyPress_sintPocetOpakovani;

	private char mrefUTextBox_KeyPress_schrPosledniZnak = ' ';

	private int Vykreslit_sintDelkaTextu;

	private int mintPosledniVterina;

	internal Pen BarvaKurzoru => mpenKurzor;

	internal bool BackspacePovolen => mbolBackspace;

	internal override string Text
	{
		get
		{
			if (mbolSlepaObrazovka)
			{
				return TextSlepeObrazovky;
			}
			return NapsanyText;
		}
	}

	internal string TextSlepeObrazovky
	{
		get
		{
			if (mstbSlepyText == null)
			{
				return string.Empty;
			}
			return mstbSlepyText.ToString();
		}
	}

	internal string NapsanyText
	{
		get
		{
			if (mstbText == null)
			{
				return string.Empty;
			}
			return mstbText.ToString();
		}
	}

	internal int PocetVterin => mintPocetVterin;

	internal event ZobrazitVarovneOknoDelegate ZobrazitVarovneOkno;

	internal event PosunNaRadekDelegate PosunNaRadek;

	internal event VyhodnotitDelegate Vyhodnotit;

	internal event ZacalPsatDelegate ZacalPsat;

	internal event PripravenDelegate Pripraven;

	internal event PridanZnakDelegate PridanZnak;

	internal VetaKresleniRT(TextBoxMB rUTextBox, Font fPismo, Color cBarvaPisma, Color cBarvaPozadi, bool PovolitBackspace, bool Klasifikace, bool SlepaObrazovka, int iMaxDelkaTextu)
		: base(string.Empty, rUTextBox, Color.Red, ZpristupnitPosuvnik: false, fPismo, cBarvaPisma, cBarvaPozadi, "", OpisZpravaDoleva: false)
	{
		mrefUTextBox.Cursor = Cursors.IBeam;
		mtmrKurzor = new Timer();
		mtmrKurzor.Interval = 650;
		mtmrCasovac = new Timer();
		mtmrCasovac.Interval = 200;
		mtmrCasovac.Stop();
		mrefUTextBox.GotFocus += mrefUTextBox_GotFocus;
		mrefUTextBox.KeyPress += mrefUTextBox_KeyPress;
		mrefUTextBox.KeyDown += mrefUTextBox_KeyDown;
		mrefUTextBox.LostFocus += mrefUTextBox_LostFocus;
		mtmrKurzor.Tick += mtmrKurzor_Tick;
		mtmrCasovac.Tick += mtmrCasovac_Tick;
		mpenKurzor = new Pen(cBarvaPisma);
		VynulovatZnaky();
		mbolBackspace = PovolitBackspace;
		mbolKlasifikace = Klasifikace;
		mbolSlepaObrazovka = SlepaObrazovka;
		mintMaxDelka = iMaxDelkaTextu;
	}

	internal override void CleanUp()
	{
		mtmrKurzor.Stop();
		mtmrKurzor.Dispose();
		mrefUTextBox.GotFocus -= mrefUTextBox_GotFocus;
		mrefUTextBox.KeyPress -= mrefUTextBox_KeyPress;
		mrefUTextBox.KeyDown -= mrefUTextBox_KeyDown;
		mrefUTextBox.LostFocus -= mrefUTextBox_LostFocus;
		mtmrKurzor.Tick -= mtmrKurzor_Tick;
		mtmrCasovac.Tick -= mtmrCasovac_Tick;
	}

	private void mtmrKurzor_Tick(object sender, EventArgs e)
	{
		mbolKurzor = !mbolKurzor;
		RefUTextBox().Invalidate();
	}

	private void PridatZnak(char znak)
	{
		if (mstbText.Length >= mintMaxDelka)
		{
			if (mbolBackspace && !mbolKlasifikace)
			{
				StopCasovace();
				if (this.ZobrazitVarovneOkno != null)
				{
					this.ZobrazitVarovneOkno(bBudeZacinatOdZacatku: true, bDosazenoMaxDelky: true);
				}
				VynulovatZnaky();
				return;
			}
			mtmrCasovac.Stop();
			if (this.ZobrazitVarovneOkno != null)
			{
				this.ZobrazitVarovneOkno(bBudeZacinatOdZacatku: false, bDosazenoMaxDelky: true);
			}
			mintPocetVterinNeaktivni = 0;
			mtmrCasovac.Start();
		}
		else
		{
			mstbText.Append(znak);
			if (char.IsWhiteSpace(znak) || znak == '\n')
			{
				mstbSlepyText.Append(znak);
			}
			else
			{
				mstbSlepyText.Append(Convert.ToChar(9679));
			}
			if (this.PridanZnak != null)
			{
				this.PridanZnak(znak);
			}
		}
	}

	internal void NastavitText(string text)
	{
		mstbText.Append(text);
		mstbSlepyText.Append(text);
	}

	private void OdebratZnak()
	{
		mstbText.Remove(mstbText.Length - 1, 1);
		mstbSlepyText.Remove(mstbSlepyText.Length - 1, 1);
	}

	private void VynulovatZnaky()
	{
		mstbText = new StringBuilder();
		mstbSlepyText = new StringBuilder();
	}

	internal void PridatZnakUkazky(char znak)
	{
		PridatZnak(znak);
	}

	internal void VynulovatZnakyUkazky()
	{
		mstbText = new StringBuilder();
		mstbSlepyText = new StringBuilder();
		RefUTextBox().Invalidate();
	}

	private void mrefUTextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		bool flag = false;
		if (Convert.ToInt32(e.KeyChar) == 8)
		{
			if (mbolBackspace && mstbText.Length > 0)
			{
				OdebratZnak();
				mrefUTextBox_KeyPress_schrPosledniZnak = ' ';
				mrefUTextBox_KeyPress_sintPocetOpakovani = 0;
				flag = true;
			}
		}
		else if (Convert.ToInt32(e.KeyChar) == 13)
		{
			if (mstbText.Length == 0)
			{
				StartCasovace();
			}
			PridatZnak('\n');
			if (mrefUTextBox_KeyPress_schrPosledniZnak == e.KeyChar)
			{
				mrefUTextBox_KeyPress_sintPocetOpakovani++;
			}
			else
			{
				mrefUTextBox_KeyPress_schrPosledniZnak = e.KeyChar;
				mrefUTextBox_KeyPress_sintPocetOpakovani = 0;
			}
			flag = true;
		}
		else if (Uhozy.PovoleneZnaky.Contains(e.KeyChar.ToString()))
		{
			if (mstbText.Length == 0)
			{
				StartCasovace();
			}
			PridatZnak(e.KeyChar);
			if (mrefUTextBox_KeyPress_schrPosledniZnak == e.KeyChar)
			{
				mrefUTextBox_KeyPress_sintPocetOpakovani++;
			}
			else
			{
				mrefUTextBox_KeyPress_schrPosledniZnak = e.KeyChar;
				mrefUTextBox_KeyPress_sintPocetOpakovani = 0;
			}
			flag = true;
		}
		if (mrefUTextBox_KeyPress_sintPocetOpakovani >= 10)
		{
			if (mbolBackspace && !mbolKlasifikace)
			{
				StopCasovace();
				if (this.ZobrazitVarovneOkno != null)
				{
					this.ZobrazitVarovneOkno(bBudeZacinatOdZacatku: true, bDosazenoMaxDelky: false);
				}
				VynulovatZnaky();
			}
			else
			{
				mtmrCasovac.Stop();
				if (this.ZobrazitVarovneOkno != null)
				{
					this.ZobrazitVarovneOkno(bBudeZacinatOdZacatku: false, bDosazenoMaxDelky: false);
				}
				mintPocetVterinNeaktivni = 0;
				mtmrCasovac.Start();
			}
			mrefUTextBox_KeyPress_schrPosledniZnak = ' ';
			mrefUTextBox_KeyPress_sintPocetOpakovani = 0;
		}
		if (flag)
		{
			e.Handled = true;
			RestartCasovace();
			RefUTextBox().Invalidate();
		}
	}

	private void mrefUTextBox_KeyDown(object sender, KeyEventArgs e)
	{
		bool flag = false;
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Back && mbolBackspace && mstbText.Length > 0)
		{
			if (mstbText.Length >= 1 && mstbText.ToString(mstbText.Length - 1, 1) == "\n")
			{
				OdebratZnak();
			}
			else
			{
				bool flag2 = false;
				while (mstbText.Length > 0 && (char.IsWhiteSpace(mstbText[mstbText.Length - 1]) || mstbText[mstbText.Length - 1] == '~'))
				{
					OdebratZnak();
				}
				while (mstbText.Length > 0 && (char.IsPunctuation(mstbText[mstbText.Length - 1]) || char.IsSymbol(mstbText[mstbText.Length - 1])))
				{
					OdebratZnak();
					flag2 = true;
				}
				if (!flag2)
				{
					while (mstbText.Length > 0 && char.IsLetterOrDigit(mstbText[mstbText.Length - 1]))
					{
						OdebratZnak();
					}
				}
			}
			flag = true;
		}
		if (flag)
		{
			e.Handled = true;
			e.SuppressKeyPress = false;
			RestartCasovace();
			RefUTextBox().Invalidate();
		}
	}

	private void mrefUTextBox_GotFocus(object sender, EventArgs e)
	{
		mbolKurzor = true;
		RefUTextBox().Invalidate();
		mtmrKurzor.Start();
	}

	private void mrefUTextBox_LostFocus(object sender, EventArgs e)
	{
		mtmrKurzor.Stop();
		if (mbolKurzor)
		{
			mbolKurzor = false;
			RefUTextBox().Invalidate();
		}
	}

	protected override void SpocitatCelkovouVysku()
	{
		mintCelkovaVyska = 4 + (int)(Math.Floor(((float)base.PocetRadku + 1.5f) * base.VyskaRadku) + 10.0);
	}

	internal override void Vykreslit(Graphics G)
	{
		if (Vykreslit_sintDelkaTextu != mstbText.Length)
		{
			int pocetRadku = base.PocetRadku;
			SpocitatVykresleni(G);
			SpocitatCelkovouVysku();
			SpocitatParametryPosuvniku();
			SpocitatRolovaniDoKonce();
			Vykreslit_sintDelkaTextu = mstbText.Length;
			if (pocetRadku != base.PocetRadku && this.PosunNaRadek != null)
			{
				this.PosunNaRadek(base.PocetRadku);
			}
		}
		G.SmoothingMode = SmoothingMode.AntiAlias;
		G.Clear(base.BarvaPozadi);
		int height = RefUTextBox().DisplayRectangle.Height;
		int num = 4;
		int num2 = 4;
		bool flag = false;
		foreach (VetaRadek item in mlstRadky)
		{
			if (item.VykreslitRadek(G, this, msngTopRolovani, height))
			{
				flag = true;
				if (item.LeftEnter >= 0)
				{
					num2 = 4;
					num = (int)((float)item.Top + base.VyskaRadku);
				}
				else
				{
					num2 = item.Left + item.SirkaRadku;
					num = item.Top;
				}
			}
			else if (flag)
			{
				break;
			}
		}
		if (mbolKurzor)
		{
			G.DrawLine(BarvaKurzoru, num2, (float)num + msngTopRolovani, num2, (float)num + msngTopRolovani + base.VyskaPisma);
		}
	}

	private void SpocitatRolovaniDoKonce()
	{
		lock (mobjTopRolovaniLOCK)
		{
			float num = (float)base.CelkovaVyska + msngTopRolovani;
			if (num > (float)RefUTextBox().DisplayRectangle.Height && msngTopRolovaniCil > (float)(RefUTextBox().DisplayRectangle.Height - base.CelkovaVyska))
			{
				msngTopRolovaniCil = RefUTextBox().DisplayRectangle.Height - base.CelkovaVyska;
				if (msngTopRolovani + (float)base.CelkovaVyska - 2f * base.VyskaRadku > (float)RefUTextBox().DisplayRectangle.Height)
				{
					msngTopRolovani = msngTopRolovaniCil + base.VyskaRadku;
				}
				else
				{
					StartRolovani();
				}
			}
			else if (msngTopRolovani < 0f && num < (float)RefUTextBox().DisplayRectangle.Height)
			{
				msngTopRolovani = RefUTextBox().DisplayRectangle.Height - base.CelkovaVyska;
				if (msngTopRolovani > 0f)
				{
					msngTopRolovani = 0f;
				}
				msngTopRolovaniCil = msngTopRolovani;
			}
		}
	}

	private void StartCasovace()
	{
		mtmrCasovac.Start();
		if (this.ZacalPsat != null)
		{
			this.ZacalPsat();
		}
		mintPosledniVterina = DateTime.Now.Second;
	}

	private void RestartCasovace()
	{
		if (mstbText.Length == 0 && !mbolKlasifikace)
		{
			StopCasovace();
		}
		else
		{
			mtmrCasovac.Stop();
			VyhodnotCasovac();
			mtmrCasovac.Start();
		}
		mtmrKurzor.Stop();
		mtmrKurzor.Start();
		mbolKurzor = true;
		mintPocetVterinNeaktivni = 0;
	}

	private void StopCasovace()
	{
		mtmrCasovac.Stop();
		mintPocetVterin = 0;
		mintPocetVterinNeaktivni = 0;
		if (this.Pripraven != null)
		{
			this.Pripraven();
		}
	}

	private void mtmrCasovac_Tick(object sender, EventArgs e)
	{
		VyhodnotCasovac();
	}

	private void VyhodnotCasovac()
	{
		int second = DateTime.Now.Second;
		if (mintPosledniVterina == second)
		{
			return;
		}
		mintPosledniVterina = second;
		mintPocetVterin++;
		mintPocetVterinNeaktivni++;
		if (mintPocetVterinNeaktivni >= 4)
		{
			mtmrCasovac.Stop();
			if (this.Vyhodnotit != null)
			{
				this.Vyhodnotit();
			}
		}
	}
}
