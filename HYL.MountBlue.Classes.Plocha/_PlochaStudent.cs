using HYL.MountBlue.Classes.Uzivatele.Prihlasen;

namespace HYL.MountBlue.Classes.Plocha;

internal abstract class _PlochaStudent : _Plocha
{
	protected PStudent PrihlasenyStudent;

	public _PlochaStudent(PStudent prihlStudent)
	{
		PrihlasenyStudent = prihlStudent;
	}
}
