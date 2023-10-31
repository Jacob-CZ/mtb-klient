using HYL.MountBlue.Classes.Klavesnice;

namespace HYL.MountBlue.Classes.Lekce;

internal class Klavesa
{
	private KlavesaKlavesnice.eKlavesy mbytCisloKlavesy;

	private KlavesaKlavesnice.eModifikatory mbytModifikator;

	private Prsty.ePrst mbytPrst;

	private bool mbolShift;

	private bool mbolJeZnama;

	private int mintLekce;

	private string mstrNadpis;

	private string mstrPopis;

	internal KlavesaKlavesnice.eKlavesy CisloKlavesy => mbytCisloKlavesy;

	internal KlavesaKlavesnice.eModifikatory Modifikator => mbytModifikator;

	internal Prsty.ePrst Prst => mbytPrst;

	internal bool Shift => mbolShift;

	internal bool JeZnama => mbolJeZnama;

	internal int Lekce => mintLekce;

	internal string Nadpis => mstrNadpis;

	internal string Popis => mstrPopis;

	public Klavesa(int iLekce, KlavesaKlavesnice.eKlavesy byKlavesa, KlavesaKlavesnice.eModifikatory byModifikator, Prsty.ePrst byPrst, bool bShift, bool bJeZnama)
	{
		mintLekce = iLekce;
		mbytCisloKlavesy = byKlavesa;
		mbytModifikator = byModifikator;
		mbytPrst = byPrst;
		mbolShift = bShift;
		mbolJeZnama = bJeZnama;
		HYL.MountBlue.Classes.Klavesnice.Klavesnice.ZjistitNadpisApopis(mbytCisloKlavesy, mbolShift, mbytModifikator, mbolJeZnama, out mstrNadpis, out mstrPopis);
	}
}
