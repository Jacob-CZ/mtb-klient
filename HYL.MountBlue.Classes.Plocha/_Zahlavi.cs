using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Classes.Uzivatele.Tridy;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal abstract class _Zahlavi : _PlochaStudent
{
	private PictureBox picHYL;

	private PictureBox picOaplikaciMB;

	public _Zahlavi(PStudent prihlStudent)
		: base(prihlStudent)
	{
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		VykreslitZahlavi(G);
	}

	private void VykreslitZahlavi(Graphics G)
	{
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngZahlavi, 21, 21);
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngMalaHvezda, 519, 83);
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngVelkaHvezda, 572, 78);
		VykreslitAvatar(G);
		VykreslitTextyZahlavi(G);
	}

	private void VykreslitAvatar(Graphics G)
	{
		_Plocha.VykreslitObrazek(G, PrihlasenyStudent.Avatar, 690, 36);
	}

	private void VykreslitTextyZahlavi(Graphics G)
	{
		string text = Text.TextMuzZena(PrihlasenyStudent.Uzivatel.Pohlavi, HYL.MountBlue.Resources.Texty.Zahlavi_PrihlasenyUzivatelM, HYL.MountBlue.Resources.Texty.Zahlavi_PrihlasenyUzivatelZ);
		_Plocha.VykreslitText(G, text, Barva.PismoZahlaviOkna, 8, FontStyle.Regular, new Rectangle(227, 29, 130, 22), StringAlignment.Far, StringAlignment.Far);
		_Plocha.VykreslitText(G, PrihlasenyStudent.Uzivatel.CeleJmeno, Barva.PismoZahlaviOkna, 11, FontStyle.Bold, new Rectangle(367, 29, 320, 25), StringAlignment.Far, StringAlignment.Near);
		_Plocha.VykreslitText(G, HYL.MountBlue.Resources.Texty.Zahlavi_Trida, Barva.PismoZahlaviOkna, 8, FontStyle.Regular, new Rectangle(227, 56, 130, 18), StringAlignment.Center, StringAlignment.Far);
		if (PrihlasenyStudent.Student is StudentSkolni)
		{
			StudentSkolni studentSkolni = (StudentSkolni)PrihlasenyStudent.Student;
			HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida = studentSkolni.Trida;
			if (trida != null)
			{
				Ucitel ucitel = trida.UcitelTridy();
				if (ucitel == null)
				{
					_Plocha.VykreslitText(G, trida.ToString(), Barva.PismoZahlaviOkna, 9, FontStyle.Bold, new Rectangle(367, 56, 320, 18), StringAlignment.Center, StringAlignment.Near);
				}
				else
				{
					_Plocha.VykreslitText(G, string.Format(HYL.MountBlue.Resources.Texty.Zahlavi_tridaUcitel, trida.ToString(), ucitel.CeleJmenoStituly), Barva.PismoZahlaviOkna, 9, FontStyle.Bold, new Rectangle(367, 56, 320, 18), StringAlignment.Center, StringAlignment.Near);
				}
			}
		}
		_Plocha.VykreslitText(G, HYL.MountBlue.Resources.Texty.Zahlavi_AktualniTabor, Barva.PismoZahlaviOkna, 8, FontStyle.Regular, new Rectangle(227, 78, 130, 23), StringAlignment.Center, StringAlignment.Far);
		_Plocha.VykreslitText(G, HYL.MountBlue.Resources.Texty.Zahlavi_Odmeny, Barva.PismoZahlaviOkna, 8, FontStyle.Regular, new Rectangle(445, 78, 62, 23), StringAlignment.Center, StringAlignment.Far);
		_Plocha.VykreslitText(G, PrihlasenyStudent.Student.Cviceni.Lekce.ToString(), Barva.PismoZahlaviOkna, 11, FontStyle.Bold, new Rectangle(367, 78, 58, 23), StringAlignment.Center, StringAlignment.Near);
		_Plocha.VykreslitText(G, PrihlasenyStudent.Student.PocetMalychOdmen.ToString(), Barva.PismoZahlaviOkna, 11, FontStyle.Bold, new Rectangle(534, 78, 35, 23), StringAlignment.Center, StringAlignment.Near);
		_Plocha.VykreslitText(G, PrihlasenyStudent.Student.PocetVelkychOdmen.ToString(), Barva.PismoZahlaviOkna, 11, FontStyle.Bold, new Rectangle(597, 78, 35, 23), StringAlignment.Center, StringAlignment.Near);
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		picHYL = new PictureBox();
		picOaplikaciMB = new PictureBox();
		((ISupportInitialize)picHYL).BeginInit();
		((ISupportInitialize)picOaplikaciMB).BeginInit();
		picHYL.Anchor = AnchorStyles.Top;
		picHYL.BackColor = Color.White;
		picHYL.Cursor = Cursors.Hand;
		picHYL.Image = HYL.MountBlue.Resources.Grafika.pngZahlaviHYL;
		Point location = new Point(120, 32);
		location.Offset(pntZacatek);
		picHYL.Location = location;
		picHYL.Name = "picHYL";
		picHYL.Size = new Size(88, 68);
		picHYL.TabIndex = 3;
		picHYL.TabStop = false;
		ttt.SetToolTip(picHYL, HYL.MountBlue.Resources.Texty.Plocha_picWebHYL_TTT);
		picHYL.Visible = true;
		picHYL.Click += picHYL_Click;
		picOaplikaciMB.Anchor = AnchorStyles.Top;
		picOaplikaciMB.BackColor = Color.White;
		picOaplikaciMB.Cursor = Cursors.Hand;
		picOaplikaciMB.Image = HYL.MountBlue.Resources.Grafika.pngZahlaviMB;
		location = new Point(31, 32);
		location.Offset(pntZacatek);
		picOaplikaciMB.Location = location;
		picOaplikaciMB.Name = "picOaplikaciMB";
		picOaplikaciMB.Size = new Size(86, 68);
		picOaplikaciMB.TabIndex = 4;
		picOaplikaciMB.TabStop = false;
		ttt.SetToolTip(picOaplikaciMB, HYL.MountBlue.Resources.Texty.Plocha_picOaplikaci_TTT);
		picOaplikaciMB.Visible = true;
		picOaplikaciMB.Click += picOaplikaciMB_Click;
		((ISupportInitialize)picHYL).EndInit();
		((ISupportInitialize)picOaplikaciMB).EndInit();
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(picOaplikaciMB);
		deleg(picHYL);
	}

	private void picOaplikaciMB_Click(object sender, EventArgs e)
	{
		Oaplikaci.ZobrazitDialog(_Plocha.HlavniOkno);
	}

	private void picHYL_Click(object sender, EventArgs e)
	{
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			picOaplikaciMB_Click(null, null);
			return true;
		}
		return base.StisknutaKlavesa(e);
	}
}
