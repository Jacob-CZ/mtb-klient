using System.Windows.Forms;

namespace HYL.MountBlue.Classes.Grafika;

internal static class ToolTipText
{
	internal static void NastavitToolTipText(ToolTip ttt)
	{
		ttt.ForeColor = Barva.TextToolTipTextu;
		ttt.BackColor = Barva.PozadiToolTipTextu;
		ttt.IsBalloon = true;
		ttt.UseAnimation = true;
		ttt.UseFading = true;
	}
}
