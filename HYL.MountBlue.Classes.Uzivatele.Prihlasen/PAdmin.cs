using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Dialogs;

namespace HYL.MountBlue.Classes.Uzivatele.Prihlasen;

internal class PAdmin : PUzivatel
{
	public PAdmin(Admin adm)
		: base(adm)
	{
	}

	internal override void ZobrazitDomu()
	{
		_Plocha.NactiPlochu(new Sprava(this));
	}

	protected override HYL.MountBlue.Dialogs.Uzivatel.eParametry ParametryDialoguNovy()
	{
		return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)0u;
	}

	protected override HYL.MountBlue.Dialogs.Uzivatel.eParametry ParametryDialoguUpravit()
	{
		return (HYL.MountBlue.Dialogs.Uzivatel.eParametry)5134u;
	}
}
