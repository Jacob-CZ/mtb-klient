using System.Security.Cryptography;
using System.Text;

namespace HYL.MountBlue.Classes;

internal static class HashMD5
{
	public static string SpocitatHashMD5(string text)
	{
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
		string text2 = string.Empty;
		byte[] array2 = array;
		foreach (byte b in array2)
		{
			text2 += $"{b:X}";
		}
		return text2.ToLower();
	}
}
