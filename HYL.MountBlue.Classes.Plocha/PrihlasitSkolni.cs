using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class PrihlasitSkolni : _Prihlasit
{
	private TextBox txtUzivJmeno;

	private TextBox txtHeslo;

	internal override bool FixniVyska()
	{
		return true;
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		_Plocha.VykreslitText(G, Text.TextNBSP(HYL.MountBlue.Resources.Texty.PrihlasitSkolni_Info1), Color.Gray, 8, FontStyle.Bold, new Rectangle(284, 310, 295, 39), StringAlignment.Center, StringAlignment.Center);
		_Plocha.VykreslitText(G, Text.TextNBSP(HYL.MountBlue.Resources.Texty.PrihlasitSkolni_Info2), Color.Gray, 8, FontStyle.Bold, new Rectangle(284, 410, 295, 45), StringAlignment.Center, StringAlignment.Center);
		_Plocha.VykreslitText(G, HYL.MountBlue.Resources.Texty.PrihlasitSkolni_UzivJmeno, Color.Black, 8, FontStyle.Bold, new Rectangle(264, 358, 120, 21), StringAlignment.Center, StringAlignment.Far);
		_Plocha.VykreslitText(G, HYL.MountBlue.Resources.Texty.PrihlasitSkolni_Heslo, Color.Black, 8, FontStyle.Bold, new Rectangle(264, 385, 120, 21), StringAlignment.Center, StringAlignment.Far);
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		txtUzivJmeno = new TextBox();
		txtHeslo = new TextBox();
		txtUzivJmeno.Anchor = AnchorStyles.Top;
		txtUzivJmeno.BackColor = Color.White;
		txtUzivJmeno.BorderStyle = BorderStyle.FixedSingle;
		txtUzivJmeno.ForeColor = Color.Black;
		txtUzivJmeno.CharacterCasing = CharacterCasing.Lower;
		Point location = new Point(390, 358);
		location.Offset(pntZacatek);
		txtUzivJmeno.Location = location;
		txtUzivJmeno.MaxLength = 32;
		txtUzivJmeno.Name = "txtUzivJmeno";
		txtUzivJmeno.Size = new Size(160, 21);
		txtUzivJmeno.TabIndex = 0;
		ttt.SetToolTip(txtUzivJmeno, HYL.MountBlue.Resources.Texty.PrihlasitSkolni_txtUzivJmeno_TTT);
		txtHeslo.Anchor = AnchorStyles.Top;
		txtHeslo.BackColor = Color.White;
		txtHeslo.BorderStyle = BorderStyle.FixedSingle;
		txtHeslo.ForeColor = Color.Black;
		location = new Point(390, 385);
		location.Offset(pntZacatek);
		txtHeslo.Location = location;
		txtHeslo.MaxLength = 32;
		txtHeslo.Name = "txtHeslo";
		txtHeslo.Size = new Size(160, 21);
		txtHeslo.TabIndex = 1;
		txtHeslo.UseSystemPasswordChar = true;
		ttt.SetToolTip(txtHeslo, HYL.MountBlue.Resources.Texty.PrihlasitSkolni_txtHeslo_TTT);
		txtUzivJmeno.Text = PUzivatele.UzivJmenoPosledniho;
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(txtUzivJmeno);
		deleg(txtHeslo);
	}

	internal override void ZacatekPlochy()
	{
		base.ZacatekPlochy();
		txtUzivJmeno.Focus();
	}

	protected override void cmdPrihlasit_TlacitkoStisknuto()
	{
		string text = txtUzivJmeno.Text;
		string text2 = HashMD5.SpocitatHashMD5(txtHeslo.Text);
		HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel = HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele[text];
		if (uzivatel == null)
		{
			MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PrihlasitSkolni_msgboxUzivNeexistuje, HYL.MountBlue.Resources.Texty.PrihlasitSkolni_msgboxUziv_Title, eMsgBoxTlacitka.OK);
			txtUzivJmeno.Focus();
		}
		else if (!uzivatel.Aktivni)
		{
			MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PrihlasitSkolni_msgboxUzivNeaktivni, HYL.MountBlue.Resources.Texty.PrihlasitSkolni_msgboxUziv_Title, eMsgBoxTlacitka.OK);
			txtUzivJmeno.Focus();
		}
		else if (uzivatel.JeUzivatelPrihlaseny)
		{
			MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PrihlasitSkolni_msgboxUzivUzPrihlaseny, HYL.MountBlue.Resources.Texty.PrihlasitSkolni_msgboxUziv_Title, eMsgBoxTlacitka.OK);
			txtUzivJmeno.Focus();
		}
		else if (uzivatel.Heslo != text2)
		{
			MsgBoxBublina.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.PrihlasitSkolni_msgboxUzivSpatneHeslo, HYL.MountBlue.Resources.Texty.PrihlasitSkolni_msgboxUziv_Title, eMsgBoxTlacitka.OK);
			txtHeslo.Text = "";
			txtHeslo.Focus();
		}
		else if (!uzivatel.VynucenaZmenaHesla || ZmenaHesla.ZobrazitZmenuHesla(_Plocha.HlavniOkno, uzivatel, bVynucenaZmenaHesla: true))
		{
			PUzivatele.PrihlasitUzivatele(text);
		}
	}
}
