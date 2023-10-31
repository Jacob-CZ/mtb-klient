using System.Collections;
using System.Collections.Generic;

namespace HYL.MountBlue.Classes.Texty;

internal class Veta : IEnumerable
{
	protected string mstrText;

	protected int mintPocetUhozu;

	protected List<Slovo> mlstSlova;

	internal virtual string Text => mstrText;

	internal int Delka => mstrText.Length;

	internal int PocetUhozu => mintPocetUhozu;

	internal int PocetSlov => mlstSlova.Count;

	internal virtual Slovo this[int index] => mlstSlova[index];

	internal Veta(string VstupniVeta)
		: this()
	{
		mstrText = VstupniVeta;
		SpocitatUhozy();
	}

	protected Veta()
	{
		mlstSlova = new List<Slovo>();
		mintPocetUhozu = 0;
	}

	protected void SpocitatUhozy()
	{
		Uhozy.NoveSlovo UdalostNoveSlovo = uhozy_NoveSlovo;
		new Uhozy(mstrText, ref UdalostNoveSlovo);
	}

	protected virtual void uhozy_NoveSlovo(string TextSlova, int ZacatekSlova, int PocetUhozu)
	{
		Slovo item = new Slovo(this, mlstSlova.Count + 1, TextSlova, ZacatekSlova, PocetUhozu);
		mlstSlova.Add(item);
		mintPocetUhozu += PocetUhozu;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return mlstSlova.GetEnumerator();
	}
}
