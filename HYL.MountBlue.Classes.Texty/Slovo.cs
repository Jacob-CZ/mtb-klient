namespace HYL.MountBlue.Classes.Texty;

internal class Slovo
{
	private string mstrText;

	private int mintZacatek;

	private int mintPocetUhozu;

	private bool mbolEnterNaKonci;

	protected Veta mrefVeta;

	private int mintIndex;

	internal bool JeSlovoMezera
	{
		get
		{
			if (Text.Length > 0)
			{
				return Text.ToCharArray(0, 1)[0] == ' ';
			}
			return false;
		}
	}

	internal int Index => mintIndex;

	internal string Text => mstrText;

	internal int Zacatek => mintZacatek;

	internal int Delka => mstrText.Length;

	internal bool EnterNaKonci => mbolEnterNaKonci;

	internal int PocetUhozu => mintPocetUhozu;

	internal Slovo(Veta Veta, int Index, string TextSlova, int ZacatekSlova, int PocetUhozu)
	{
		mrefVeta = Veta;
		mintIndex = Index;
		if (EnterNaKonciSlova(TextSlova))
		{
			mbolEnterNaKonci = true;
		}
		mstrText = TextSlova;
		mintZacatek = ZacatekSlova;
		mintPocetUhozu = PocetUhozu;
	}

	internal static bool EnterNaKonciSlova(string str)
	{
		return str.Contains("\n");
	}

	internal virtual Veta Veta()
	{
		return mrefVeta;
	}
}
