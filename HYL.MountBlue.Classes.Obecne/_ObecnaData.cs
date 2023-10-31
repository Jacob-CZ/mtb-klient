namespace HYL.MountBlue.Classes.Obecne;

internal abstract class _ObecnaData
{
	private static _ObecnaData obecData;

	internal static _ObecnaData ObecnaData => obecData;

	static _ObecnaData()
	{
		obecData = new ObecnaDataSkolni();
	}

	internal abstract float MaximalniPovolenaChybovost();

	internal abstract int MaximalniPovolenyPocetChybNormalne();

	internal int MaximalniPovolenyPocetChyb(bool bJeCviceniZkouska)
	{
		if (bJeCviceniZkouska)
		{
			return MaximalniPovolenyPocetChybUzkousky();
		}
		return MaximalniPovolenyPocetChybNormalne();
	}

	internal abstract int MaximalniPovolenyPocetChybUzkousky();
}
