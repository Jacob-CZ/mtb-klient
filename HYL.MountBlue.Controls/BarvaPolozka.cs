using System.Drawing;

namespace HYL.MountBlue.Controls;

public class BarvaPolozka
{
	private string mstrJmeno;

	private Color mclrBarva;

	private Color mclrBarvaTextu;

	public Color Barva => mclrBarva;

	public Color BarvaTextuPolozky => mclrBarvaTextu;

	public string JmenoBarvy => mstrJmeno;

	public BarvaPolozka(Color barva, Color barvaTextu, string jmenoBarvy)
	{
		mclrBarva = barva;
		mclrBarvaTextu = barvaTextu;
		mstrJmeno = jmenoBarvy;
	}

	public override bool Equals(object obj)
	{
		if (obj != null && obj is BarvaPolozka)
		{
			return ((BarvaPolozka)obj).Barva == Barva;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return Barva.GetHashCode();
	}
}
