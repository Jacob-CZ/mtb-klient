using System;
using System.Collections;
using System.Text;

namespace HYL.MountBlue.Classes.Texty;

internal class DiffLCS
{
	internal struct Rozdily
	{
		internal int StartVzadani;

		internal int StartVopise;

		internal int ChybiZeZadani;

		internal int NavicVopise;
	}

	internal struct Souradnice
	{
		internal int X;

		internal int Y;
	}

	private DataDiff dataZadani;

	private DataDiff dataOpis;

	internal DiffLCS(Slovo slovoZadani, Slovo slovoOpis)
	{
		dataZadani = new DataDiff(slovoZadani);
		dataOpis = new DataDiff(slovoOpis);
	}

	internal DiffLCS(Veta vetaZadani, Veta vetaOpis)
	{
		dataZadani = new DataDiff(vetaZadani);
		dataOpis = new DataDiff(vetaOpis);
	}

	internal void SpocitatDiff(VetaKresleni vetaZadani, VetaKresleni vetaOpis, bool porovnejCelyText, out int iPocetChyb)
	{
		Rozdily[] array = SpocitatDiff(porovnejCelyText);
		iPocetChyb = 0;
		Rozdily[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Rozdily r = array2[i];
			int iPocetChyb2 = 0;
			if (r.ChybiZeZadani == r.NavicVopise && r.ChybiZeZadani >= 1)
			{
				for (int j = 0; j < r.ChybiZeZadani; j++)
				{
					DiffLCS diffLCS = new DiffLCS(vetaZadani[r.StartVzadani + j], vetaOpis[r.StartVopise + j]);
					diffLCS.SpocitatDiff(vetaZadani[r.StartVzadani + j], vetaOpis[r.StartVopise + j], porovnejCelyText);
				}
				iPocetChyb += PocetChyb(vetaZadani, vetaOpis, r);
				continue;
			}
			if (SlitaSlova(vetaZadani, vetaOpis, ref iPocetChyb2, r))
			{
				iPocetChyb += iPocetChyb2;
				continue;
			}
			if (r.ChybiZeZadani > 0)
			{
				for (int k = r.StartVzadani; k < Math.Min(r.StartVzadani + r.ChybiZeZadani, vetaZadani.PocetSlov); k++)
				{
					vetaZadani[k].ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.uplne;
				}
			}
			else if (r.StartVzadani < vetaZadani.PocetSlov)
			{
				vetaZadani[r.StartVzadani].ZvyraznitPredSlovem = true;
			}
			else if (vetaZadani.PocetSlov > 0)
			{
				vetaZadani[vetaZadani.PocetSlov - 1].ZvyraznitZaSlovem = true;
			}
			if (r.NavicVopise > 0)
			{
				for (int l = r.StartVopise; l < Math.Min(r.StartVopise + r.NavicVopise, vetaOpis.PocetSlov); l++)
				{
					vetaOpis[l].ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.uplne;
				}
			}
			else if (r.StartVopise < vetaOpis.PocetSlov)
			{
				vetaOpis[r.StartVopise].ZvyraznitPredSlovem = true;
			}
			else if (vetaOpis.PocetSlov > 0)
			{
				vetaOpis[vetaOpis.PocetSlov - 1].ZvyraznitZaSlovem = true;
			}
			iPocetChyb += PocetChyb(vetaZadani, vetaOpis, r);
		}
	}

	private int PocetChyb(VetaKresleni vetaZadani, VetaKresleni vetaOpis, Rozdily r)
	{
		int num = r.ChybiZeZadani;
		bool flag = false;
		for (int i = r.StartVzadani; i < Math.Min(r.StartVzadani + r.ChybiZeZadani, vetaZadani.PocetSlov); i++)
		{
			if (!vetaZadani[i].JeSlovoMezera)
			{
				flag = true;
			}
			else if (flag)
			{
				flag = false;
				num--;
			}
		}
		int num2 = r.NavicVopise;
		flag = false;
		for (int j = r.StartVopise; j < Math.Min(r.StartVopise + r.NavicVopise, vetaOpis.PocetSlov); j++)
		{
			if (!vetaOpis[j].JeSlovoMezera)
			{
				flag = true;
			}
			else if (flag)
			{
				flag = false;
				num2--;
			}
		}
		if (num2 > 0)
		{
			return Math.Max(num, 1);
		}
		return num;
	}

	private bool SlitaSlova(VetaKresleni vetaZadani, VetaKresleni vetaOpis, ref int iPocetChyb, Rozdily r)
	{
		if ((r.ChybiZeZadani & 1) > 0 && r.ChybiZeZadani < 10 && r.NavicVopise == 1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = r.StartVzadani; i < Math.Min(r.StartVzadani + r.ChybiZeZadani, vetaZadani.PocetSlov); i++)
			{
				if (!vetaZadani[i].JeSlovoMezera)
				{
					stringBuilder.Append(vetaZadani[i].Text);
				}
			}
			if (stringBuilder.ToString() != vetaOpis[r.StartVopise].Text)
			{
				return false;
			}
			int num = 0;
			for (int j = r.StartVzadani; j < Math.Min(r.StartVzadani + r.ChybiZeZadani, vetaZadani.PocetSlov); j++)
			{
				if (vetaZadani[j].JeSlovoMezera)
				{
					iPocetChyb++;
					vetaZadani[j].ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.uplne;
					vetaOpis[r.StartVopise].ZvyraznitPredZnakem(num, value: true);
				}
				else
				{
					num += vetaZadani[j].Delka;
				}
			}
			return true;
		}
		if ((r.NavicVopise & 1) > 0 && r.NavicVopise < 10 && r.ChybiZeZadani == 1)
		{
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int k = r.StartVopise; k < Math.Min(r.StartVopise + r.NavicVopise, vetaOpis.PocetSlov); k++)
			{
				if (!vetaOpis[k].JeSlovoMezera)
				{
					stringBuilder2.Append(vetaOpis[k].Text);
				}
			}
			if (stringBuilder2.ToString() != vetaZadani[r.StartVzadani].Text)
			{
				return false;
			}
			int num2 = 0;
			for (int l = r.StartVopise; l < Math.Min(r.StartVopise + r.NavicVopise, vetaOpis.PocetSlov); l++)
			{
				if (vetaOpis[l].JeSlovoMezera)
				{
					iPocetChyb++;
					vetaOpis[l].ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.uplne;
					vetaZadani[r.StartVzadani].ZvyraznitPredZnakem(num2, value: true);
				}
				else
				{
					num2 += vetaOpis[l].Delka;
				}
			}
			return true;
		}
		return false;
	}

	internal void SpocitatDiff(SlovoKresleni slovoZadani, SlovoKresleni slovoOpisu, bool porovnejCelyText)
	{
		Rozdily[] array = SpocitatDiff(porovnejCelyText);
		slovoZadani.ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.zadne;
		slovoOpisu.ZvyraznitSlovo = SlovoKresleni.eZvyrazneni.zadne;
		Rozdily[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Rozdily rozdily = array2[i];
			if (rozdily.ChybiZeZadani > 0)
			{
				for (int j = rozdily.StartVzadani; j < Math.Min(rozdily.StartVzadani + rozdily.ChybiZeZadani, slovoZadani.Delka); j++)
				{
					slovoZadani.ZvyraznitZnak(j, value: true);
				}
			}
			else if (rozdily.StartVzadani < slovoZadani.Delka)
			{
				slovoZadani.ZvyraznitPredZnakem(rozdily.StartVzadani, value: true);
			}
			else
			{
				slovoZadani.ZvyraznitZaSlovem = true;
			}
			if (rozdily.NavicVopise > 0)
			{
				for (int k = rozdily.StartVopise; k < Math.Min(rozdily.StartVopise + rozdily.NavicVopise, slovoOpisu.Delka); k++)
				{
					slovoOpisu.ZvyraznitZnak(k, value: true);
				}
			}
			else if (rozdily.StartVopise < slovoOpisu.Delka)
			{
				slovoOpisu.ZvyraznitPredZnakem(rozdily.StartVopise, value: true);
			}
			else
			{
				slovoOpisu.ZvyraznitZaSlovem = true;
			}
		}
	}

	private Rozdily[] SpocitatDiff(bool porovnejCelyText)
	{
		int num = dataZadani.Delka;
		int delka = dataOpis.Delka;
		if (!porovnejCelyText)
		{
			num = ((dataZadani.Delka < dataOpis.Delka) ? dataZadani.Delka : dataOpis.Delka);
		}
		int num2 = num + delka + 1;
		int[] downVector = new int[2 * num2 + 2];
		int[] upVector = new int[2 * num2 + 2];
		LCS(0, num, 0, delka, downVector, upVector, porovnejCelyText);
		Optimalizovat(dataZadani);
		Optimalizovat(dataOpis);
		return VytvoritRozdily(porovnejCelyText);
	}

	private void LCS(int LowerA, int UpperA, int LowerB, int UpperB, int[] DownVector, int[] UpVector, bool porovnejCelyText)
	{
		while (LowerA < UpperA && LowerB < UpperB && dataZadani.Data[LowerA] == dataOpis.Data[LowerB])
		{
			LowerA++;
			LowerB++;
		}
		while (LowerA < UpperA && LowerB < UpperB && dataZadani.Data[UpperA - 1] == dataOpis.Data[UpperB - 1])
		{
			UpperA--;
			UpperB--;
		}
		if (LowerA == UpperA)
		{
			while (LowerB < UpperB)
			{
				dataOpis.Zmenit(LowerB++, hodnota: true);
			}
		}
		else if (LowerB == UpperB)
		{
			while (LowerA < UpperA)
			{
				dataZadani.Zmenit(LowerA++, hodnota: true);
			}
		}
		else
		{
			Souradnice souradnice = OptimalniSouradnice(LowerA, UpperA, LowerB, UpperB, DownVector, UpVector, porovnejCelyText);
			LCS(LowerA, souradnice.X, LowerB, souradnice.Y, DownVector, UpVector, porovnejCelyText);
			LCS(souradnice.X, UpperA, souradnice.Y, UpperB, DownVector, UpVector, porovnejCelyText);
		}
	}

	private Souradnice OptimalniSouradnice(int LowerA, int UpperA, int LowerB, int UpperB, int[] DownVector, int[] UpVector, bool porovnejCelyText)
	{
		int num = dataZadani.Delka;
		int delka = dataOpis.Delka;
		if (!porovnejCelyText)
		{
			num = ((dataZadani.Delka < dataOpis.Delka) ? dataZadani.Delka : dataOpis.Delka);
		}
		int num2 = num + delka + 1;
		int num3 = LowerA - LowerB;
		int num4 = UpperA - UpperB;
		int num5 = UpperA - LowerA - (UpperB - LowerB);
		bool flag = (num5 & 1) != 0;
		int num6 = num2 - num3;
		int num7 = num2 - num4;
		int num8 = (UpperA - LowerA + UpperB - LowerB) / 2 + 1;
		DownVector[num6 + num3 + 1] = LowerA;
		UpVector[num7 + num4 - 1] = UpperA;
		Souradnice result = default(Souradnice);
		for (int i = 0; i <= num8; i++)
		{
			for (int j = num3 - i; j <= num3 + i; j += 2)
			{
				int num9;
				if (j == num3 - i)
				{
					num9 = DownVector[num6 + j + 1];
				}
				else
				{
					num9 = DownVector[num6 + j - 1] + 1;
					if (j < num3 + i && DownVector[num6 + j + 1] >= num9)
					{
						num9 = DownVector[num6 + j + 1];
					}
				}
				int num10 = num9 - j;
				while (num9 < UpperA && num10 < UpperB && dataZadani.Data[num9] == dataOpis.Data[num10])
				{
					num9++;
					num10++;
				}
				DownVector[num6 + j] = num9;
				if (flag && num4 - i < j && j < num4 + i && UpVector[num7 + j] <= DownVector[num6 + j])
				{
					result.X = DownVector[num6 + j];
					result.Y = DownVector[num6 + j] - j;
					return result;
				}
			}
			for (int k = num4 - i; k <= num4 + i; k += 2)
			{
				int num11;
				if (k == num4 + i)
				{
					num11 = UpVector[num7 + k - 1];
				}
				else
				{
					num11 = UpVector[num7 + k + 1] - 1;
					if (k > num4 - i && UpVector[num7 + k - 1] < num11)
					{
						num11 = UpVector[num7 + k - 1];
					}
				}
				int num12 = num11 - k;
				while (num11 > LowerA && num12 > LowerB && dataZadani.Data[num11 - 1] == dataOpis.Data[num12 - 1])
				{
					num11--;
					num12--;
				}
				UpVector[num7 + k] = num11;
				if (!flag && num3 - i <= k && k <= num3 + i && UpVector[num7 + k] <= DownVector[num6 + k])
				{
					result.X = DownVector[num6 + k];
					result.Y = DownVector[num6 + k] - k;
					return result;
				}
			}
		}
		throw new ApplicationException("Chyba při výpočtu Longest Common Subsequence!");
	}

	private Rozdily[] VytvoritRozdily(bool porovnejCelyText)
	{
		ArrayList arrayList = new ArrayList();
		int i = 0;
		int j = 0;
		int num = dataZadani.Delka;
		int delka = dataOpis.Delka;
		if (!porovnejCelyText)
		{
			num = ((dataZadani.Delka < dataOpis.Delka) ? dataZadani.Delka : dataOpis.Delka);
		}
		while (i < num || j < delka)
		{
			if (i < num && !dataZadani.Zmeneno[i] && j < delka && !dataOpis.Zmeneno[j])
			{
				i++;
				j++;
				continue;
			}
			int num2 = i;
			int num3 = j;
			for (; i < num && (j >= delka || dataZadani.Zmeneno[i]); i++)
			{
			}
			for (; j < delka && (i >= num || dataOpis.Zmeneno[j]); j++)
			{
			}
			if (num2 < i || num3 < j)
			{
				Rozdily rozdily = default(Rozdily);
				rozdily.StartVzadani = num2;
				rozdily.StartVopise = num3;
				rozdily.ChybiZeZadani = i - num2;
				rozdily.NavicVopise = j - num3;
				arrayList.Add(rozdily);
			}
		}
		Rozdily[] array = new Rozdily[arrayList.Count];
		arrayList.CopyTo(array);
		return array;
	}

	private static void Optimalizovat(DataDiff data)
	{
		int i = 0;
		while (i < data.Delka)
		{
			for (; i < data.Delka && !data.Zmeneno[i]; i++)
			{
			}
			int j;
			for (j = i; j < data.Delka && data.Zmeneno[j]; j++)
			{
			}
			if (j < data.Delka && data.Data[i] == data.Data[j])
			{
				data.Zmenit(i, hodnota: false);
				data.Zmenit(j, hodnota: true);
			}
			else
			{
				i = j;
			}
		}
	}
}
