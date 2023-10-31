using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Grafika;

internal static class Avatary
{
	public const int VelikostNahledu = 64;

	public static readonly Size VelikostAvataru = new Size(115, 115);

	public static readonly int[] AvataryIDuzivatel = new int[4] { 0, 1, 2, 3 };

	public static readonly int[] AvataryIDucitel = new int[1] { 50 };

	public static readonly int[] AvataryIDadmin = new int[1] { 60 };

	public static void ToComboBox(ref AvatarComboBox lst, int[] avataryID)
	{
		foreach (int avatarID in avataryID)
		{
			lst.Items.Add(new AvatarPolozka(Avatar(avatarID), vlastniAvatar: false, avatarID));
		}
	}

	public static Image Avatar(int avatarID)
	{
		return avatarID switch
		{
			0 => HYL.MountBlue.Resources.Avatary.pngAvatarZena1, 
			1 => HYL.MountBlue.Resources.Avatary.pngAvatarZena2, 
			2 => HYL.MountBlue.Resources.Avatary.pngAvatarMuz1, 
			3 => HYL.MountBlue.Resources.Avatary.pngAvatarMuz2, 
			50 => HYL.MountBlue.Resources.Avatary.pngAvatarUcitel, 
			60 => HYL.MountBlue.Resources.Avatary.pngAvatarAdmin, 
			_ => null, 
		};
	}

	public static int VychoziAvatar(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel.ePohlavi pohlavi)
	{
		return pohlavi switch
		{
			HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel.ePohlavi.muz => 2, 
			HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel.ePohlavi.zena => 0, 
			_ => 0, 
		};
	}

	public static bool ZobrazitNajitSoubor(out string cesta)
	{
		using OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = HYL.MountBlue.Resources.Texty.Avatary_dlgFiltrSouboru;
		openFileDialog.CheckFileExists = true;
		openFileDialog.CheckPathExists = true;
		openFileDialog.Multiselect = false;
		openFileDialog.Title = HYL.MountBlue.Resources.Texty.Avatary_dlg_Title;
		openFileDialog.ValidateNames = true;
		bool result = openFileDialog.ShowDialog(_Plocha.HlavniOkno) == DialogResult.OK;
		cesta = openFileDialog.FileName;
		return result;
	}

	public static bool ZpracovatObrazek(string cesta, ref Image image)
	{
		try
		{
			Image image2 = Image.FromFile(cesta);
			double num = Math.Max((float)VelikostAvataru.Width / (float)image2.Size.Width, (float)VelikostAvataru.Height / (float)image2.Size.Height);
			Size size = new Size((int)((double)image2.Size.Width * num) + 2, (int)((double)image2.Size.Height * num) + 2);
			Point location = new Point((VelikostAvataru.Width - size.Width) / 2, (VelikostAvataru.Height - size.Height) / 2);
			Rectangle rect = new Rectangle(location, size);
			Bitmap bitmap = new Bitmap(VelikostAvataru.Width, VelikostAvataru.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.DrawImage(image2, rect);
			image = bitmap;
			return true;
		}
		catch (Exception ex)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Avatary_msgChyba, ex.Message), HYL.MountBlue.Resources.Texty.Avatary_msgChyba_Title, eMsgBoxTlacitka.OK);
			return false;
		}
	}
}
