using System;

namespace HYL.MountBlue.Classes.Texty;

internal class DataDiff
{
	private int[] data;

	private bool[] zmeneno;

	internal int[] Data => data;

	internal bool[] Zmeneno => zmeneno;

	internal int Delka => data.Length;

	private DataDiff(int delkaPole)
	{
		data = new int[delkaPole];
		zmeneno = new bool[delkaPole + 2];
	}

	internal DataDiff(Veta veta)
		: this(veta.PocetSlov)
	{
		for (int i = 0; i < veta.PocetSlov; i++)
		{
			data[i] = veta[i].Text.GetHashCode();
		}
	}

	internal DataDiff(Slovo slovo)
		: this(slovo.Text.Length)
	{
		for (int i = 0; i < slovo.Text.Length; i++)
		{
			data[i] = Convert.ToInt32(slovo.Text[i]);
		}
	}

	internal void Zmenit(int index, bool hodnota)
	{
		zmeneno[index] = hodnota;
	}
}
