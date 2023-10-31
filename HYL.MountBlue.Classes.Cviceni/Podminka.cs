using System;

namespace HYL.MountBlue.Classes.Cviceni;

public class Podminka
{
	private float msngHodnota;

	private char mchrVetev;

	public double Hodnota => msngHodnota;

	public char Vetev => mchrVetev;

	public Podminka(float hodnota, char vetev)
	{
		msngHodnota = (float)Math.Round(hodnota, 2);
		mchrVetev = vetev;
	}
}
