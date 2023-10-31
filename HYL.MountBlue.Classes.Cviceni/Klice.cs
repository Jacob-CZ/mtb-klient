using System.Text.RegularExpressions;

namespace HYL.MountBlue.Classes.Cviceni;

internal static class Klice
{
	private const string RegexKlic = "^(([A-Z0-9]){6})$";

	public static string GenerujKlic(uint uid, int Lekce, uint CviceniID, bool ZeSkoly)
	{
		uint num = CviceniID;
		if (!ZeSkoly)
		{
			num += 1163;
		}
		uint cislo = (uint)((int)(uid / 9857) + Lekce * 10151) + num * 661849;
		return ID.CisloNaText(cislo, 6);
	}

	public static bool OveritKlic(string klic, ref uint uid, ref int lekce, ref uint cviceni, ref bool zeSkoly)
	{
		Regex regex = new Regex("^(([A-Z0-9]){6})$");
		if (!regex.IsMatch(klic))
		{
			return false;
		}
		uint num = ID.TextNaCislo(klic);
		if (num == 0)
		{
			return false;
		}
		zeSkoly = false;
		cviceni = num / 661849;
		if (cviceni >= 1163)
		{
			cviceni -= 1163u;
		}
		else
		{
			zeSkoly = true;
		}
		uint num2 = num % 661849;
		lekce = (int)(num2 / 10151);
		uid = num2 % 10151 * 9857;
		return true;
	}
}
