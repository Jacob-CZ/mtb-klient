namespace HYL.MountBlue.Classes.Obecne;

internal class ObecnaDataSkolni : _ObecnaData
{
	internal override float MaximalniPovolenaChybovost()
	{
		return 1f;
	}

	internal override int MaximalniPovolenyPocetChybNormalne()
	{
		return 3;
	}

	internal override int MaximalniPovolenyPocetChybUzkousky()
	{
		return 5;
	}
}
