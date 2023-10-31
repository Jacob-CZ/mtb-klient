using System.Drawing;

namespace HYL.MountBlue.Classes.Texty;

internal class SlovoKresleni : Slovo
{
	internal enum eStavSlova
	{
		nove = 0,
		nenalezeno = 1,
		nalezeno = 5,
		akceptovano = 10
	}

	internal enum eZvyrazneni : byte
	{
		zadne = 0,
		castecne = 128,
		uplne = byte.MaxValue
	}

	private eStavSlova mintStav;

	private Rectangle[] mrctZnaky;

	private eZvyrazneni mbytZvyraznit;

	private bool mbolZobrazitNekdeVsuvku;

	private bool[] mbolZvyraznitZnaky;

	private bool[] mbolZvyraznitPredZnakem;

	private bool mbolZvyraznitZaSlovem;

	internal eStavSlova Stav
	{
		get
		{
			return mintStav;
		}
		set
		{
			mintStav = value;
		}
	}

	internal eZvyrazneni ZvyraznitSlovo
	{
		get
		{
			return mbytZvyraznit;
		}
		set
		{
			switch (value)
			{
			case eZvyrazneni.uplne:
				NastavitZvyrazneniVsechZnaku(hodnota: true);
				break;
			case eZvyrazneni.zadne:
				NastavitZvyrazneniVsechZnaku(hodnota: false);
				break;
			}
		}
	}

	internal bool ZvyraznitPredSlovem
	{
		get
		{
			return ZvyraznenoPredZnakem(0);
		}
		set
		{
			ZvyraznitPredZnakem(0, value);
		}
	}

	internal bool ZvyraznitZaSlovem
	{
		get
		{
			return mbolZvyraznitZaSlovem;
		}
		set
		{
			mbolZvyraznitZaSlovem = value;
		}
	}

	internal SlovoKresleni(Veta oVeta, int iIndex, string sTextSlova, int iZacatekSlova, int iPocetUhozu, Rectangle[] rctZnaky)
		: base(oVeta, iIndex, sTextSlova, iZacatekSlova, iPocetUhozu)
	{
		mrctZnaky = rctZnaky;
		ZkontrolovatOblastiZnaku();
		if (mrctZnaky != null)
		{
			int num = mrctZnaky.Length;
			mbolZvyraznitZnaky = new bool[num];
			mbolZvyraznitPredZnakem = new bool[num];
			mbytZvyraznit = eZvyrazneni.zadne;
		}
		mintStav = eStavSlova.nove;
	}

	private void ZkontrolovatOblastiZnaku()
	{
		int lowerBound = mrctZnaky.GetLowerBound(0);
		int upperBound = mrctZnaky.GetUpperBound(0);
		if (upperBound - lowerBound <= 0)
		{
			return;
		}
		for (int i = lowerBound + 1; i <= upperBound; i++)
		{
			if (mrctZnaky[i].Left == 0 && mrctZnaky[i].Top == 0 && mrctZnaky[i].Width == 0 && mrctZnaky[i].Height == 0)
			{
				mrctZnaky[i].Location = new Point(mrctZnaky[i - 1].Left + mrctZnaky[i - 1].Width, mrctZnaky[i - 1].Top);
				mrctZnaky[i].Size = new Size(mrctZnaky[i - 1].Width, mrctZnaky[i - 1].Height);
			}
		}
	}

	internal void ZvyraznitZnak(int index, bool value)
	{
		if (mbolZvyraznitZnaky != null && index >= 0 && index <= mbolZvyraznitZnaky.GetUpperBound(0))
		{
			mbolZvyraznitZnaky[index] = value;
			ObnovitPromennouZvyraznit();
		}
	}

	internal void ZvyraznitPredZnakem(int index, bool value)
	{
		if (mbolZvyraznitPredZnakem != null && index >= 0 && index <= mbolZvyraznitPredZnakem.GetUpperBound(0))
		{
			mbolZvyraznitPredZnakem[index] = value;
			if (value)
			{
				mbolZobrazitNekdeVsuvku = true;
			}
			else
			{
				ObnovitPromennouNekdeVsuvka();
			}
		}
	}

	private void ObnovitPromennouNekdeVsuvka()
	{
		if (mbolZvyraznitPredZnakem == null)
		{
			return;
		}
		bool flag = false;
		bool[] array = mbolZvyraznitPredZnakem;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				flag = true;
			}
		}
		if (flag)
		{
			mbolZobrazitNekdeVsuvku = true;
		}
		else
		{
			mbolZobrazitNekdeVsuvku = false;
		}
	}

	internal bool ZvyraznenoPredZnakem(int index)
	{
		if (mbolZvyraznitPredZnakem == null)
		{
			return false;
		}
		if (index < 0)
		{
			return false;
		}
		if (index <= mbolZvyraznitPredZnakem.GetUpperBound(0))
		{
			return mbolZvyraznitPredZnakem[index];
		}
		return false;
	}

	internal bool ZvyraznenyZnak(int index)
	{
		if (mbolZvyraznitZnaky == null)
		{
			return ZvyraznitSlovo == eZvyrazneni.uplne;
		}
		if (index < 0 || index > mbolZvyraznitZnaky.GetUpperBound(0))
		{
			return false;
		}
		return mbolZvyraznitZnaky[index];
	}

	private void NastavitZvyrazneniVsechZnaku(bool hodnota)
	{
		if (mbolZvyraznitZnaky != null)
		{
			for (int i = mbolZvyraznitZnaky.GetLowerBound(0); i <= mbolZvyraznitZnaky.GetUpperBound(0); i++)
			{
				mbolZvyraznitZnaky[i] = hodnota;
			}
		}
		if (hodnota)
		{
			mbytZvyraznit = eZvyrazneni.uplne;
		}
		else
		{
			mbytZvyraznit = eZvyrazneni.zadne;
		}
	}

	private void ObnovitPromennouZvyraznit()
	{
		if (mbolZvyraznitZnaky == null)
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		bool[] array = mbolZvyraznitZnaky;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i])
			{
				flag = true;
			}
			else
			{
				flag2 = true;
			}
		}
		if (flag && !flag2)
		{
			mbytZvyraznit = eZvyrazneni.uplne;
		}
		else if (!flag && flag2)
		{
			mbytZvyraznit = eZvyrazneni.zadne;
		}
		else
		{
			mbytZvyraznit = eZvyrazneni.castecne;
		}
	}

	internal void VykreslitZvyrazneni(Graphics G, VetaKresleni vetaKresleni, float sngTop, int intHeight)
	{
		if (mbolZobrazitNekdeVsuvku && mrctZnaky != null)
		{
			for (int i = mbolZvyraznitPredZnakem.GetLowerBound(0); i <= mbolZvyraznitPredZnakem.GetUpperBound(0); i++)
			{
				Rectangle rectangle = mrctZnaky[i];
				if (mbolZvyraznitPredZnakem[i] && sngTop + (float)rectangle.Top + (float)rectangle.Height > 0f && sngTop + (float)rectangle.Top < (float)intHeight)
				{
					RectangleF rect = new RectangleF(rectangle.Left - 1, (float)rectangle.Top + sngTop, vetaKresleni.VyskaPisma, vetaKresleni.VyskaPisma);
					VykresliVsuvku(G, vetaKresleni, rect);
				}
			}
		}
		if ((int)ZvyraznitSlovo > 0 && mrctZnaky != null)
		{
			for (int j = mbolZvyraznitZnaky.GetLowerBound(0); j <= mbolZvyraznitZnaky.GetUpperBound(0); j++)
			{
				Rectangle rectangle2 = mrctZnaky[j];
				if (mbolZvyraznitZnaky[j] && sngTop + (float)rectangle2.Top + (float)rectangle2.Height > 0f && sngTop + (float)rectangle2.Top < (float)intHeight)
				{
					G.FillRectangle(rect: new RectangleF(rectangle2.Left - 1, (float)rectangle2.Top + sngTop + vetaKresleni.TopZvyrazneni, rectangle2.Width, vetaKresleni.VyskaZvyrazeni), brush: vetaKresleni.BarvaZvyrazneni);
				}
			}
		}
		if (ZvyraznitZaSlovem && mrctZnaky != null)
		{
			int upperBound = mrctZnaky.GetUpperBound(0);
			Rectangle rectangle3 = mrctZnaky[upperBound];
			if (sngTop + (float)rectangle3.Top + (float)rectangle3.Height > 0f && sngTop + (float)rectangle3.Top < (float)intHeight)
			{
				RectangleF rect3 = new RectangleF(rectangle3.Left + rectangle3.Width, (float)rectangle3.Top + sngTop, vetaKresleni.VyskaPisma, vetaKresleni.VyskaPisma);
				VykresliVsuvku(G, vetaKresleni, rect3);
			}
		}
	}

	internal new VetaKresleni Veta()
	{
		return (VetaKresleni)mrefVeta;
	}

	private void VykresliVsuvku(Graphics G, VetaKresleni vetaKresleni, RectangleF rect)
	{
		float num = rect.Width * 0.2f;
		float num2 = rect.Width * 0.1f;
		G.FillPolygon(points: new PointF[6]
		{
			new PointF(rect.X - num - num2, rect.Y),
			new PointF(rect.X - num2, rect.Y + num),
			new PointF(rect.X - num2, rect.Y + rect.Height),
			new PointF(rect.X, rect.Y + rect.Height),
			new PointF(rect.X, rect.Y + num),
			new PointF(rect.X + num, rect.Y)
		}, brush: vetaKresleni.BarvaZvyrazneni);
	}
}
