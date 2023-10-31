namespace HYL.MountBlue.Classes.Texty;

internal class Vyhodnoceni
{
	private VetaKresleni mobjZadani;

	private VetaKresleni mobjOpis;

	private bool mboolPorovnejCelyText;

	private int mintPocetChyb;

	internal int PocetChyb => mintPocetChyb;

	internal Vyhodnoceni(VetaKresleni oVetaZadani, VetaKresleni oVetaOpisu, bool porovnejCelyText)
	{
		mobjZadani = oVetaZadani;
		mobjOpis = oVetaOpisu;
		mintPocetChyb = 0;
		mboolPorovnejCelyText = porovnejCelyText;
	}

	internal VetaKresleni VetaZadani()
	{
		return mobjZadani;
	}

	internal VetaKresleni VetaOpisu()
	{
		return mobjOpis;
	}

	internal void Vyhodnot()
	{
		DiffLCS diffLCS = new DiffLCS(mobjZadani, mobjOpis);
		diffLCS.SpocitatDiff(mobjZadani, mobjOpis, mboolPorovnejCelyText, out mintPocetChyb);
	}
}
