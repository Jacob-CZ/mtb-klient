using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using HYL.MountBlue.Controls;

namespace HYL.MountBlue.Classes.Texty;

internal class VetaKresleni : Veta
{
	internal const byte PocetZnakuNaRadku = 60;

	internal const byte LevyOkraj = 4;

	internal const byte PravyOkraj = 4;

	internal const byte HorniOkraj = 4;

	internal const byte DolniOkraj = 10;

	internal const byte RychlostRolovani = 170;

	internal const byte KrokuNaRadek = 15;

	internal const byte FixniPocetRadkuProRolovani = 3;

	protected TextBoxMB mrefUTextBox;

	private bool mbolRuntime;

	private StringFormat mfmtFormat;

	private Font mfntPismo;

	private Brush mbshBarvaPisma;

	private Color mclrBarvaPozadi;

	private Brush mbshBarvaZvyrazneni;

	private float msngVyskaZvyrazneni;

	private float msngTopZvyrazneni;

	private int mintSirkaRadku;

	private float msngVyskaRadku;

	private float msngVyskaPisma;

	private float msngSirkaMezery;

	protected int mintCelkovaVyska;

	private bool mbolOpisZpravaDoleva;

	private float msngKrokRolovani;

	protected float msngTopRolovani;

	protected float msngTopRolovaniCil;

	protected object mobjTopRolovaniLOCK;

	private bool mbolRolovani;

	private System.Timers.Timer mtmrRolovani;

	protected List<VetaRadek> mlstRadky;

	private VetaRadek uhozy_NoveSlovo_sobjAktRadek;

	internal int SirkaRadku => mintSirkaRadku;

	internal float VyskaRadku => msngVyskaRadku;

	internal float VyskaPisma => msngVyskaPisma;

	internal int CelkovaVyska => mintCelkovaVyska;

	internal Font Pismo => mfntPismo;

	internal float TopZvyrazneni => msngTopZvyrazneni;

	internal float VyskaZvyrazeni => msngVyskaZvyrazneni;

	internal Brush BarvaPisma => mbshBarvaPisma;

	internal Color BarvaPozadi => mclrBarvaPozadi;

	internal Brush BarvaZvyrazneni => mbshBarvaZvyrazneni;

	internal new SlovoKresleni this[int index] => (SlovoKresleni)mlstSlova[index];

	internal int PocetRadku => mlstRadky.Count;

	internal StringFormat FormatTextu => mfmtFormat;

	internal bool PosuvnikZobrazeny => RefUTextBox().Posuvnik.Enabled;

	internal bool PosuvnikZpristupneny
	{
		get
		{
			return !RefUTextBox().Posuvnik.PouzeProCteni;
		}
		set
		{
			RefUTextBox().Posuvnik.PouzeProCteni = !value;
			RefUTextBox().Posuvnik.NovaHodnota(0);
		}
	}

	internal VetaKresleni(string VstupniVeta, TextBoxMB rUTextBox, Color BarvaZvyrazneni, bool ZpristupnitPosuvnik, Font fPismo, Color cBarvaPisma, Color cBarvaPozadi, string CelaVstupniVeta, bool OpisZpravaDoleva)
	{
		mlstRadky = new List<VetaRadek>();
		mstrText = VstupniVeta;
		mtmrRolovani = new System.Timers.Timer();
		mtmrRolovani.Interval = 170.0;
		mfmtFormat = new StringFormat();
		mfmtFormat.Trimming = StringTrimming.Word;
		mobjTopRolovaniLOCK = new object();
		mbolOpisZpravaDoleva = OpisZpravaDoleva;
		mrefUTextBox = rUTextBox;
		mrefUTextBox.Cursor = Cursors.Default;
		mrefUTextBox.HodnotaPosuvnikuZmenena += mrefUTextBox_HodnotaPosuvnikuZmenena;
		mrefUTextBox.Paint += mrefScrollTextBox_Paint;
		mrefUTextBox.Resize += mrefUTextBox_Resize;
		mtmrRolovani.Elapsed += mtmrRolovani_Tick;
		msngTopRolovani = 0f;
		mfntPismo = fPismo;
		mbshBarvaPisma = new SolidBrush(cBarvaPisma);
		mclrBarvaPozadi = cBarvaPozadi;
		mbshBarvaZvyrazneni = new SolidBrush(BarvaZvyrazneni);
		Graphics g = RefUTextBox().CreateGraphics();
		SpocitatSirkuRadku(g);
		SpocitatVyskuRadkuAvyskuPisma(g);
		SpocitatKrokRolovani(g);
		SpocitatSirkuMezery(g);
		SpocitatVykresleni(g);
		SpocitatCelkovouVysku();
		SpocitatParametryPosuvniku();
		if (PosuvnikZobrazeny)
		{
			if (CelaVstupniVeta != null && VstupniVeta.Length < CelaVstupniVeta.Length)
			{
				mstrText = CelaVstupniVeta;
				SpocitatVykresleni(g);
				SpocitatCelkovouVysku();
				SpocitatParametryPosuvniku();
			}
			if (OpisZpravaDoleva)
			{
				msngTopRolovani = RefUTextBox().DisplayRectangle.Height - CelkovaVyska;
				msngTopRolovaniCil = msngTopRolovani;
				SpocitatParametryPosuvniku();
			}
		}
		PosuvnikZpristupneny = ZpristupnitPosuvnik;
		SpocitatUhozy();
	}

	internal virtual void CleanUp()
	{
		mtmrRolovani.Stop();
		mtmrRolovani.Dispose();
		mrefUTextBox.HodnotaPosuvnikuZmenena -= mrefUTextBox_HodnotaPosuvnikuZmenena;
		mrefUTextBox.Paint -= mrefScrollTextBox_Paint;
		mrefUTextBox.Resize -= mrefUTextBox_Resize;
		mtmrRolovani.Elapsed -= mtmrRolovani_Tick;
	}

	protected override void uhozy_NoveSlovo(string TextSlova, int ZacatekSlova, int PocetUhozu)
	{
		if (uhozy_NoveSlovo_sobjAktRadek == null || !uhozy_NoveSlovo_sobjAktRadek.JeTextObsazenVradku(ZacatekSlova))
		{
			uhozy_NoveSlovo_sobjAktRadek = null;
			foreach (VetaRadek item in mlstRadky)
			{
				if (item.JeTextObsazenVradku(ZacatekSlova))
				{
					uhozy_NoveSlovo_sobjAktRadek = item;
					break;
				}
			}
		}
		Rectangle[] rctZnaky = null;
		if (TextSlova.Contains("\n"))
		{
			SpocitatCharRangesProEnter(uhozy_NoveSlovo_sobjAktRadek, out rctZnaky, TextSlova, ZacatekSlova);
		}
		else
		{
			SpocitatCharRanges(uhozy_NoveSlovo_sobjAktRadek, out rctZnaky, TextSlova, ZacatekSlova);
		}
		mlstSlova.Add(new SlovoKresleni(this, base.PocetSlov + 1, TextSlova, ZacatekSlova, PocetUhozu, rctZnaky));
		mintPocetUhozu += PocetUhozu;
	}

	private void SpocitatCharRanges(VetaRadek objRadek, out Rectangle[] rctZnaky, string TextSlova, int ZacatekSlova)
	{
		rctZnaky = new Rectangle[0];
		if (objRadek == null)
		{
			return;
		}
		int num = ZacatekSlova - objRadek.ZacatekRadku;
		CharacterRange[] array;
		if (TextSlova.Length <= 32)
		{
			array = new CharacterRange[TextSlova.Length];
			for (int i = 0; i <= TextSlova.Length - 1; i++)
			{
				ref CharacterRange reference = ref array[i];
				reference = new CharacterRange(num + i, 1);
			}
		}
		else
		{
			array = new CharacterRange[1]
			{
				new CharacterRange(num, TextSlova.Length)
			};
		}
		RectangleF layoutRect = new RectangleF(objRadek.Left, objRadek.Top, SirkaRadku, VyskaRadku);
		Graphics graphics = RefUTextBox().CreateGraphics();
		StringFormat stringFormat = new StringFormat(mfmtFormat);
		stringFormat.SetMeasurableCharacterRanges(array);
		try
		{
			Region[] array2 = graphics.MeasureCharacterRanges(objRadek.TextRadku, mfntPismo, layoutRect, stringFormat);
			rctZnaky = new Rectangle[array2.Length];
			int num2 = 0;
			for (int j = array2.GetLowerBound(0); j <= array2.GetUpperBound(0); j++)
			{
				ref Rectangle reference2 = ref rctZnaky[num2];
				reference2 = Rectangle.Round(array2[j].GetBounds(graphics));
				num2++;
			}
		}
		catch
		{
		}
	}

	private void SpocitatCharRangesProEnter(VetaRadek objRadek, out Rectangle[] rctZnaky, string TextSlova, int ZacatekSlova)
	{
		rctZnaky = new Rectangle[0];
		if (objRadek == null)
		{
			return;
		}
		CharacterRange[] array = new CharacterRange[1];
		int num = ZacatekSlova - objRadek.ZacatekRadku;
		if (num <= 0)
		{
			rctZnaky = new Rectangle[1]
			{
				new Rectangle(objRadek.Left + (int)(VyskaPisma * 0.2f), objRadek.Top, (int)VyskaPisma, (int)VyskaPisma)
			};
			return;
		}
		ref CharacterRange reference = ref array[0];
		reference = new CharacterRange(0, num);
		RectangleF layoutRect = new RectangleF(objRadek.Left, objRadek.Top, SirkaRadku, VyskaRadku);
		Graphics graphics = RefUTextBox().CreateGraphics();
		StringFormat stringFormat = new StringFormat(mfmtFormat);
		stringFormat.SetMeasurableCharacterRanges(array);
		try
		{
			Region[] array2 = graphics.MeasureCharacterRanges(objRadek.TextRadku, mfntPismo, layoutRect, stringFormat);
			Rectangle rectangle = Rectangle.Round(array2[0].GetBounds(graphics));
			rctZnaky = new Rectangle[1]
			{
				new Rectangle(rectangle.Left + rectangle.Width, rectangle.Top, (int)VyskaPisma, (int)VyskaPisma)
			};
		}
		catch
		{
		}
	}

	protected void SpocitatVykresleni(Graphics G)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(Text);
		SizeF layoutArea = new SizeF(SirkaRadku, VyskaRadku);
		int num = 0;
		mlstRadky.Clear();
		while (stringBuilder.Length > 0)
		{
			int num2 = 180;
			if (num2 > stringBuilder.Length)
			{
				num2 = stringBuilder.Length;
			}
			int charactersFitted;
			int linesFilled;
			SizeF sizeF = G.MeasureString(stringBuilder.ToString(0, num2), mfntPismo, layoutArea, mfmtFormat, out charactersFitted, out linesFilled);
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append(stringBuilder.ToString(0, charactersFitted));
			bool flag = stringBuilder2.ToString().Contains("\n");
			if (stringBuilder.Length == charactersFitted)
			{
				int length = stringBuilder2.ToString().TrimEnd('~').Length;
				stringBuilder2.Replace('~', ' ', 0, length);
			}
			else
			{
				stringBuilder2.Replace('~', ' ');
			}
			stringBuilder2.Replace("\n", "");
			int iLeftEnter = -1;
			if (flag)
			{
				sizeF = G.MeasureString(stringBuilder2.ToString(), mfntPismo, layoutArea, mfmtFormat, out var _, out linesFilled);
				iLeftEnter = (int)(4f + sizeF.Width);
			}
			PridatRadek(stringBuilder2.ToString(), PocetRadku + 1, num, SpocitatSirkuRadku(stringBuilder2.ToString(), sizeF.Width), 4, 4 + (int)((float)PocetRadku * VyskaRadku), iLeftEnter);
			num += charactersFitted;
			stringBuilder.Remove(0, charactersFitted);
		}
	}

	private int SpocitatSirkuRadku(string TextRadku, float BeznaSirka)
	{
		int num = PocetMezerNaKonciTextu(TextRadku);
		int num2 = (int)(BeznaSirka + (float)num * msngSirkaMezery);
		if (num2 > SirkaRadku)
		{
			num2 = SirkaRadku;
		}
		return num2;
	}

	private int PocetMezerNaKonciTextu(string str)
	{
		int length = str.Length;
		int length2 = str.TrimEnd(' ').Length;
		return length - length2;
	}

	private void SpocitatSirkuMezery(Graphics G)
	{
		SizeF sizeF = G.MeasureString('X'.ToString() + ' ' + 'X', mfntPismo);
		SizeF sizeF2 = G.MeasureString('X'.ToString() + 'X', mfntPismo);
		msngSirkaMezery = sizeF.Width - sizeF2.Width;
	}

	private void PridatRadek(string sTextRadku, int iCisloRadku, int iZacatekRadku, int iSirkaRadku, int iLeft, int iTop, int iLeftEnter)
	{
		mlstRadky.Add(new VetaRadek(sTextRadku, iCisloRadku, iZacatekRadku, iSirkaRadku, iLeft, iTop, iLeftEnter, this));
	}

	private void SpocitatKrokRolovani(Graphics G)
	{
		msngKrokRolovani = mfntPismo.GetHeight(G) / 15f;
	}

	private void SpocitatSirkuRadku(Graphics G)
	{
		int val = RefUTextBox().DisplayRectangle.Width - RefUTextBox().Posuvnik.Width - 4 - 4 - (int)mfntPismo.GetHeight(G);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append('X', 60);
		int val2 = (int)G.MeasureString(stringBuilder.ToString(), mfntPismo, 4000, mfmtFormat).Width;
		mintSirkaRadku = Math.Min(val, val2);
	}

	private void SpocitatVyskuRadkuAvyskuPisma(Graphics G)
	{
		msngVyskaPisma = mfntPismo.GetHeight(G);
		msngVyskaRadku = msngVyskaPisma * 1.1f;
		msngVyskaZvyrazneni = msngVyskaPisma * 0.15f;
		msngTopZvyrazneni = msngVyskaPisma * 0.9f - msngVyskaZvyrazneni;
	}

	protected virtual void SpocitatCelkovouVysku()
	{
		mintCelkovaVyska = 4 + (int)Math.Floor((float)PocetRadku * VyskaRadku) + 10;
	}

	protected void SpocitatParametryPosuvniku()
	{
		if (RefUTextBox().DisplayRectangle.Height < CelkovaVyska)
		{
			float num = RefUTextBox().DisplayRectangle.Height;
			RefUTextBox().Posuvnik.Enabled = true;
			RefUTextBox().Posuvnik.Minimum = 0;
			RefUTextBox().Posuvnik.Maximum = CelkovaVyska * 100;
			RefUTextBox().Posuvnik.SmallChange = (int)(msngKrokRolovani * 100f);
			RefUTextBox().Posuvnik.LargeChange = (int)(num * 100f);
			RefUTextBox().Posuvnik.NovaHodnota(-(int)(msngTopRolovani * 100f));
			mbolRolovani = true;
		}
		else
		{
			RefUTextBox().Posuvnik.Enabled = false;
			mbolRolovani = false;
		}
	}

	internal virtual void Vykreslit(Graphics G)
	{
		G.SmoothingMode = SmoothingMode.AntiAlias;
		G.Clear(BarvaPozadi);
		int height = RefUTextBox().DisplayRectangle.Height;
		foreach (SlovoKresleni item in (IEnumerable)this)
		{
			item.VykreslitZvyrazneni(G, this, msngTopRolovani, height);
		}
		bool flag = false;
		foreach (VetaRadek item2 in mlstRadky)
		{
			if (item2.VykreslitRadek(G, this, msngTopRolovani, height))
			{
				flag = true;
			}
			else if (flag)
			{
				break;
			}
		}
	}

	private void mrefUTextBox_HodnotaPosuvnikuZmenena()
	{
		lock (mobjTopRolovaniLOCK)
		{
			msngTopRolovani = -RefUTextBox().Posuvnik.Value / 100;
		}
		RefUTextBox().Invalidate();
	}

	private void mrefScrollTextBox_Paint(object sender, PaintEventArgs e)
	{
		if (mbolRuntime)
		{
			Vykreslit(e.Graphics);
		}
	}

	private void mrefUTextBox_Resize(object sender, EventArgs e)
	{
		if (!mbolRuntime)
		{
			return;
		}
		lock (mobjTopRolovaniLOCK)
		{
			SpocitatParametryPosuvniku();
			if ((float)CelkovaVyska + msngTopRolovani < (float)RefUTextBox().DisplayRectangle.Height)
			{
				msngTopRolovani = RefUTextBox().DisplayRectangle.Height - CelkovaVyska;
				if (msngTopRolovani > 0f)
				{
					msngTopRolovani = 0f;
				}
				RefUTextBox().Posuvnik.NovaHodnota(-(int)(msngTopRolovani * 100f));
			}
		}
	}

	internal TextBoxMB RefUTextBox()
	{
		return mrefUTextBox;
	}

	internal void PosunoutNaRadek(int iCisloRadku)
	{
		if (!mbolRolovani)
		{
			return;
		}
		lock (mobjTopRolovaniLOCK)
		{
			int num = iCisloRadku - 3;
			if (num < 0)
			{
				num = 0;
			}
			float num2 = ((!mbolOpisZpravaDoleva) ? ((0f - VyskaRadku) * (float)num) : ((0f - VyskaRadku) * (float)(PocetRadku - num)));
			float num3 = RefUTextBox().DisplayRectangle.Height - CelkovaVyska;
			if (num2 < num3)
			{
				num2 = num3;
			}
			if (msngTopRolovaniCil != num2)
			{
				msngTopRolovaniCil = num2;
				StartRolovani();
			}
		}
	}

	protected void StartRolovani()
	{
		if (mbolRolovani && !mtmrRolovani.Enabled)
		{
			mtmrRolovani.Start();
		}
	}

	protected void StopRolovani()
	{
		mtmrRolovani.Stop();
	}

	private void mtmrRolovani_Tick(object sender, EventArgs e)
	{
		lock (mobjTopRolovaniLOCK)
		{
			if (msngTopRolovani > msngTopRolovaniCil)
			{
				msngTopRolovani -= msngKrokRolovani;
				if (msngTopRolovani <= msngTopRolovaniCil)
				{
					StopRolovani();
				}
			}
			else
			{
				msngTopRolovani += msngKrokRolovani;
				if (msngTopRolovani >= msngTopRolovaniCil)
				{
					StopRolovani();
				}
			}
		}
		RefUTextBox().Posuvnik.NovaHodnota(-(int)(msngTopRolovani * 100f));
		RefUTextBox().Invalidate();
	}

	internal VetaRadek Radek(int cisloRadku)
	{
		return mlstRadky[cisloRadku];
	}

	internal void SpustitRuntime()
	{
		mbolRuntime = true;
	}
}
