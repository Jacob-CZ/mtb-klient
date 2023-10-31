using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal abstract class _Sezeni : _PlochaStudent
{
	private Sezeni ucSezeni;

	public _Sezeni(PStudent prihlStudent)
		: base(prihlStudent)
	{
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		VykreslitSpravneSezeni(G);
	}

	private void VykreslitSpravneSezeni(Graphics G)
	{
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngSezeni, 43, 43);
	}

	internal abstract string TextPopisu();

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		ucSezeni = new Sezeni();
		ucSezeni.Anchor = AnchorStyles.Top;
		ucSezeni.BackColor = Color.White;
		ucSezeni.Name = "ucSezeni";
		ucSezeni.Size = new Size(417, 237);
		ucSezeni.TabIndex = 2;
		ucSezeni.NastavitPozici(_Plocha.HlavniOkno);
		ucSezeni.Text = HYL.MountBlue.Resources.Texty.Sezeni_BublinaNadpis;
		ucSezeni.Popis = Text.TextNBSP(TextPopisu());
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(ucSezeni);
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			cmdNavratDomu_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}

	protected void cmdNavratDomu_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitDomu();
	}
}
