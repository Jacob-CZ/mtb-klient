using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Uzivatele.Prihlasen;

internal abstract class PUzivatel
{
	private HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel;

	internal HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel Uzivatel => uzivatel;

	internal Image Avatar => AvatarUzivatele(Uzivatel);

	public PUzivatel(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uziv)
	{
		uzivatel = uziv;
	}

	internal static Image AvatarUzivatele(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uziv)
	{
		Image image = null;
		if (uziv.VlastniAvatar)
		{
			image = AvatarUzivatele(uziv.AvatarImage);
		}
		Image image2 = HYL.MountBlue.Classes.Grafika.Avatary.Avatar(uziv.AvatarCislo);
		if (uziv.VlastniAvatar && image != null)
		{
			return image;
		}
		if (image2 != null)
		{
			return image2;
		}
		return HYL.MountBlue.Classes.Grafika.Avatary.Avatar(HYL.MountBlue.Classes.Grafika.Avatary.VychoziAvatar(uziv.Pohlavi));
	}

	internal static Image AvatarUzivatele(byte[] avatar)
	{
		if (avatar == null)
		{
			return null;
		}
		return Image.FromStream(new MemoryStream(avatar));
	}

	public static byte[] AvatarUzivatele(Image image)
	{
		if (image == null)
		{
			return null;
		}
		MemoryStream memoryStream = new MemoryStream();
		image.Save(memoryStream, ImageFormat.Jpeg);
		memoryStream.Close();
		return memoryStream.GetBuffer();
	}

	internal abstract void ZobrazitDomu();

	public virtual void InicializovatUzivatele()
	{
		ZobrazitDomu();
	}

	public bool OdhlasitUzivatele(bool bZobrazitPrihlaseni)
	{
		bool flag = false;
		_Plocha.ZakazatZobrazeniBubliny = true;
		flag = MsgBoxBublina.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Uzivatele_msgboxOdhlasit, Uzivatel.CeleJmeno), HYL.MountBlue.Resources.Texty.Uzivatele_msgboxOdhlasitTitle, eMsgBoxTlacitka.AnoNe) == DialogResult.Yes;
		if (flag)
		{
			OdhlaseniUzivatele();
			Uzivatel.Ulozit();
			PUzivatele.OdhlasitUzivatele(bZobrazitPrihlaseni);
		}
		else
		{
			_Plocha.ZakazatZobrazeniBubliny = false;
			_Plocha.NastavitViditelnostBubliny(bViditelnostBubliny: true);
		}
		return flag;
	}

	protected virtual void OdhlaseniUzivatele()
	{
	}

	internal bool ZobrazitUpravitUzivatele()
	{
		return ZobrazitDialogUzivatel(Uzivatel, ParametryDialoguUpravit(), novyUzivatel: false);
	}

	internal bool ZobrazitNovyUzivatel()
	{
		return ZobrazitDialogUzivatel(Uzivatel, ParametryDialoguNovy(), novyUzivatel: true);
	}

	protected abstract HYL.MountBlue.Dialogs.Uzivatel.eParametry ParametryDialoguNovy();

	protected abstract HYL.MountBlue.Dialogs.Uzivatel.eParametry ParametryDialoguUpravit();

	internal static bool ZobrazitDialogUzivatel(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel, HYL.MountBlue.Dialogs.Uzivatel.eParametry parametry, bool novyUzivatel)
	{
		using HYL.MountBlue.Dialogs.Uzivatel uzivatel2 = new HYL.MountBlue.Dialogs.Uzivatel(uzivatel, parametry, novyUzivatel);
		bool flag = uzivatel2.ShowDialog(_Plocha.HlavniOkno) == DialogResult.OK;
		if (flag)
		{
			_Plocha.AktualniPlocha.ObnovitGrafiku();
		}
		return flag;
	}
}
