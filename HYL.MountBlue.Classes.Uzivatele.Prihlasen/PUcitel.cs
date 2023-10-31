using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;

namespace HYL.MountBlue.Classes.Uzivatele.Prihlasen;

internal class PUcitel : PUzivatel
{
	internal Ucitel Ucitel => (Ucitel)base.Uzivatel;

	public PUcitel(Ucitel ucit)
		: base(ucit)
	{
	}

	internal override void ZobrazitDomu()
	{
		_Plocha.NactiPlochu(new HYL.MountBlue.Classes.Plocha.Tridy(this));
	}

	internal void ZobrazitSpravneSezeni()
	{
		_Plocha.NactiPlochu(new SezeniUcitel(this));
	}

	internal void ZobrazitSpravneSezeni(int tridaID)
	{
		_Plocha.NactiPlochu(new SezeniUcitel(this, tridaID));
	}

	internal void ZobrazitHistorii(int tridaID, uint uid, ZalozkyHistorie.eZalozky zalozka)
	{
		_Plocha.NactiPlochu(new HYL.MountBlue.Classes.Plocha.Historie(this, tridaID, uid, zalozka));
	}

	internal void OtevritTridu(int tridaID, ZalozkyTrida.eZalozky zalozka)
	{
		Ucitel.PosledniVybranaTridaID = tridaID;
		_Plocha.NactiPlochu(new HYL.MountBlue.Classes.Plocha.Trida(this, tridaID, zalozka));
	}

	protected override HYL.MountBlue.Dialogs.Uzivatel.eParametry ParametryDialoguNovy()
	{
		if (PUzivatele.PrihlasenyUzivatel is PAdmin)
		{
			return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)14u;
		}
		return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)0u;
	}

	protected override HYL.MountBlue.Dialogs.Uzivatel.eParametry ParametryDialoguUpravit()
	{
		if (PUzivatele.PrihlasenyUzivatel is PAdmin)
		{
			return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)49166u;
		}
		return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)5156u;
	}
}
