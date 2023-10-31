using System.Drawing;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Klavesnice;

internal class Prsty
{
	internal enum ePrst : byte
	{
		jiny,
		levy_malik,
		levy_prstenik,
		levy_prostrednik,
		levy_ukazovak,
		palec,
		pravy_ukazovak,
		pravy_prostrednik,
		pravy_prstenik,
		pravy_malik
	}

	private const byte ALPHA = 120;

	internal static Point SouradnicePrstu(ePrst Prst)
	{
		return Prst switch
		{
			ePrst.levy_malik => new Point(73, 256), 
			ePrst.levy_prstenik => new Point(118, 217), 
			ePrst.levy_prostrednik => new Point(152, 203), 
			ePrst.levy_ukazovak => new Point(192, 214), 
			ePrst.palec => new Point(275, 286), 
			ePrst.pravy_ukazovak => new Point(360, 214), 
			ePrst.pravy_prostrednik => new Point(399, 203), 
			ePrst.pravy_prstenik => new Point(435, 217), 
			ePrst.pravy_malik => new Point(479, 256), 
			_ => new Point(0, 0), 
		};
	}

	internal static string ToString(ePrst prst)
	{
		return prst switch
		{
			ePrst.levy_malik => HYL.MountBlue.Resources.Texty.Prsty_prst1, 
			ePrst.levy_prstenik => HYL.MountBlue.Resources.Texty.Prsty_prst2, 
			ePrst.levy_prostrednik => HYL.MountBlue.Resources.Texty.Prsty_prst3, 
			ePrst.levy_ukazovak => HYL.MountBlue.Resources.Texty.Prsty_prst4, 
			ePrst.palec => HYL.MountBlue.Resources.Texty.Prsty_prst5, 
			ePrst.pravy_ukazovak => HYL.MountBlue.Resources.Texty.Prsty_prst6, 
			ePrst.pravy_prostrednik => HYL.MountBlue.Resources.Texty.Prsty_prst7, 
			ePrst.pravy_prstenik => HYL.MountBlue.Resources.Texty.Prsty_prst8, 
			ePrst.pravy_malik => HYL.MountBlue.Resources.Texty.Prsty_prst9, 
			ePrst.jiny => HYL.MountBlue.Resources.Texty.Prsty_prst0, 
			_ => string.Empty, 
		};
	}

	internal static string ToString7pad(ePrst prst)
	{
		return prst switch
		{
			ePrst.levy_malik => HYL.MountBlue.Resources.Texty.Prsty_prst1_7pad, 
			ePrst.levy_prstenik => HYL.MountBlue.Resources.Texty.Prsty_prst2_7pad, 
			ePrst.levy_prostrednik => HYL.MountBlue.Resources.Texty.Prsty_prst3_7pad, 
			ePrst.levy_ukazovak => HYL.MountBlue.Resources.Texty.Prsty_prst4_7pad, 
			ePrst.palec => HYL.MountBlue.Resources.Texty.Prsty_prst5_7pad, 
			ePrst.pravy_ukazovak => HYL.MountBlue.Resources.Texty.Prsty_prst6_7pad, 
			ePrst.pravy_prostrednik => HYL.MountBlue.Resources.Texty.Prsty_prst7_7pad, 
			ePrst.pravy_prstenik => HYL.MountBlue.Resources.Texty.Prsty_prst8_7pad, 
			ePrst.pravy_malik => HYL.MountBlue.Resources.Texty.Prsty_prst9_7pad, 
			ePrst.jiny => HYL.MountBlue.Resources.Texty.Prsty_prst0_7pad, 
			_ => string.Empty, 
		};
	}
}
