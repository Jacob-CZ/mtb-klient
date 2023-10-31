using System.Drawing;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Grafika;

internal static class Barva
{
	internal static readonly Color PozadiHlavnihoOkna = Color.FromArgb(140, 216, 246);

	internal static readonly Color OkrajBubliny = Color.FromArgb(169, 174, 169);

	internal static readonly Color OkrajDialogu = OkrajBubliny;

	internal static readonly Color PozadiTlacitekNaPlose = Color.FromArgb(215, 242, 252);

	internal static readonly Color PozadiTlacitkaZavrit = Color.FromArgb(219, 55, 50);

	internal static readonly Color PozadiTlacitekMinMaxZavrit = Color.FromArgb(206, 237, 249);

	internal static readonly Color PozadiTextBoxu = Color.WhiteSmoke;

	internal static readonly Color ObdelnikZaTextBoxem = Color.FromArgb(185, 230, 246);

	internal static readonly Color CestaNaHoru = Color.FromArgb(242, 101, 34);

	internal static readonly Color CestaNaHoruPosledniLekce = Color.FromArgb(0, 102, 170);

	internal static readonly Color KrouzekNadPrstem = Color.Red;

	internal static readonly Color KrouzkyKlavesniceAnim = Color.FromArgb(80, Color.Red);

	internal static readonly Color VyrezanyObdelnik = Color.FromArgb(180, Color.White);

	internal static readonly Color VrcholovaTabulkaPozadi = Color.FromArgb(150, Color.White);

	internal static readonly Color VrcholovaTabulkaObrys = Color.FromArgb(0, 102, 170);

	internal static readonly Color VrcholovaTabulkaPismo = Color.Black;

	internal static readonly Color PismoZahlaviOkna = Color.White;

	internal static readonly Color PodtrzeniChybyZadani = Color.FromArgb(42, 179, 230);

	internal static readonly Color PodtrzeniChybyOpis = Color.Red;

	internal static readonly Color DokoncenyUzivatel = Color.Gray;

	internal static readonly Color PozadiToolTipTextu = Color.White;

	internal static readonly Color TextToolTipTextu = Color.Black;

	internal static readonly Color TextObecny = Color.Black;

	internal static readonly Color ZaznamStudentPrihlaseny = Color.FromArgb(186, 231, 247);

	internal static readonly Color ZaznamStudentNeprihlaseny = Color.WhiteSmoke;

	internal static readonly Color StudentPrihlaseny = Color.Blue;

	internal static readonly Color StudentNeprihlaseny = Color.DarkGray;

	internal static readonly Color HistorieJmenoStudenta = Color.FromArgb(15, 107, 172);

	public static void BarvyToComboBox(ref ColorComboBox ccb)
	{
		ccb.Items.Add(new BarvaPolozka(Color.White, Color.Black, HYL.MountBlue.Resources.Texty.Barvy_bila));
		ccb.Items.Add(new BarvaPolozka(Color.Black, Color.White, HYL.MountBlue.Resources.Texty.Barvy_cerna));
		ccb.Items.Add(new BarvaPolozka(Color.Gray, Color.White, HYL.MountBlue.Resources.Texty.Barvy_seda));
		ccb.Items.Add(new BarvaPolozka(Color.Navy, Color.White, HYL.MountBlue.Resources.Texty.Barvy_tmaveModra));
		ccb.Items.Add(new BarvaPolozka(Color.Blue, Color.White, HYL.MountBlue.Resources.Texty.Barvy_modra));
		ccb.Items.Add(new BarvaPolozka(Color.Green, Color.White, HYL.MountBlue.Resources.Texty.Barvy_zelena));
		ccb.Items.Add(new BarvaPolozka(Color.LightGreen, Color.Black, HYL.MountBlue.Resources.Texty.Barvy_svetleZelena));
		ccb.Items.Add(new BarvaPolozka(Color.DarkRed, Color.White, HYL.MountBlue.Resources.Texty.Barvy_cervena));
		ccb.Items.Add(new BarvaPolozka(Color.Brown, Color.White, HYL.MountBlue.Resources.Texty.Barvy_hneda));
		ccb.Items.Add(new BarvaPolozka(Color.Orange, Color.White, HYL.MountBlue.Resources.Texty.Barvy_oranzova));
		ccb.Items.Add(new BarvaPolozka(Color.Yellow, Color.Black, HYL.MountBlue.Resources.Texty.Barvy_zluta));
		ccb.Items.Add(new BarvaPolozka(Color.DarkViolet, Color.White, HYL.MountBlue.Resources.Texty.Barvy_fialova));
		ccb.Items.Add(new BarvaPolozka(Color.DeepPink, Color.White, HYL.MountBlue.Resources.Texty.Barvy_ruzova));
	}
}
