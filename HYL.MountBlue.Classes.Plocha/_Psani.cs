using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal abstract class _Psani : _PlochaStudent
{
	protected HYL.MountBlue.Classes.Cviceni.Psani CviceniPsani;

	protected TextBoxMB tmbZadani;

	protected TextBoxMB tmbOpis;

	protected Slapoty ucSlapoty;

	protected ObrazkoveTlacitko cmdPrejitNaDalsi;

	protected ObrazkoveTlacitko cmdVyhodnotit;

	protected ObrazkoveTlacitko cmdNavratDomu;

	private static int PocetStiskuF6;

	public _Psani(PStudent prihlStudent, HYL.MountBlue.Classes.Cviceni.Psani cvicPs)
		: base(prihlStudent)
	{
		CviceniPsani = cvicPs;
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		VykreslitZahlavi(G, CviceniPsani.SkutecnyPocet, CviceniPsani.ZpusobToString(), CviceniPsani.Backspace, CviceniPsani.Odmena);
		VykreslitBilyPodklad(G);
		VykreslitModryPodklad(G);
	}

	private void VykreslitZahlavi(Graphics G, int iPocetOpisu, string sOpsatJak, bool bBackspacePovoleny, bool bMaCviceniOdmenu)
	{
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngPsaniZahlavi, 21, 21);
		_Plocha.VykreslitText(G, HYL.MountBlue.Resources.Texty.Psani_OpisteText, Barva.PismoZahlaviOkna, 12, FontStyle.Bold, new Rectangle(27, 27, 200, 66), StringAlignment.Center, StringAlignment.Far);
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngPsaniKolik, 247, 42);
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngPsaniJak, 336, 42);
		int num = 18;
		if (sOpsatJak.Length > 20)
		{
			num = 11;
		}
		_Plocha.VykreslitText(G, string.Format(HYL.MountBlue.Resources.Texty.Psani_OpisteXkrat, iPocetOpisu), Color.Black, new Font("Courier New", 18f, FontStyle.Bold), new Rectangle(252, 47, 54, 30), StringAlignment.Center, StringAlignment.Center);
		_Plocha.VykreslitText(G, sOpsatJak, Color.Black, new Font("Courier New", num, FontStyle.Bold), new Rectangle(341, 47, 355, 32), StringAlignment.Center, StringAlignment.Center);
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngBackspace, 734, 44);
		if (!bBackspacePovoleny)
		{
			_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngBackspaceNo, 743, 39);
		}
		if (bMaCviceniOdmenu)
		{
			_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngMalaHvezda, 810, 52);
		}
	}

	private void VykreslitBilyPodklad(Graphics G)
	{
		int num = 94;
		int num2 = _Plocha.HlavniOkno.DisplayRectangle.Height - 21 - num - HYL.MountBlue.Resources.Grafika.pngPsaniPodklad2.Height;
		G.DrawImage(rect: new Rectangle(21, 94, HYL.MountBlue.Resources.Grafika.pngPsaniPodklad1.Width, num2), image: HYL.MountBlue.Resources.Grafika.pngPsaniPodklad1);
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngPsaniPodklad2, 21, num2 + num);
	}

	private void VykreslitModryPodklad(Graphics G)
	{
		Brush brush = new SolidBrush(Barva.ObdelnikZaTextBoxem);
		Rectangle rObdelnik = new Rectangle(60, 125, 736, 195);
		rObdelnik.Inflate(8, 8);
		GraphicsPath path = SpecialniGrafika.CestaZaoblenehoObdelniku(rObdelnik, 4);
		G.FillPath(brush, path);
		rObdelnik = new Rectangle(60, 386, 736, _Plocha.HlavniOkno.DisplayRectangle.Height - 443);
		rObdelnik.Inflate(8, 8);
		path = SpecialniGrafika.CestaZaoblenehoObdelniku(rObdelnik, 4);
		G.FillPath(brush, path);
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		int num = 147;
		if (CviceniPsani.OznaceniCviceni.JeTrenink || !CviceniPsani.Klasifikace)
		{
			cmdPrejitNaDalsi = new ObrazkoveTlacitko();
		}
		cmdVyhodnotit = new ObrazkoveTlacitko();
		cmdNavratDomu = new ObrazkoveTlacitko();
		tmbOpis = new TextBoxMB();
		tmbZadani = new TextBoxMB();
		ucSlapoty = new Slapoty();
		tmbOpis.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
		tmbOpis.BackColor = PrihlasenyStudent.Uzivatel.BarvaPozadi;
		Point location = new Point(60, 386);
		location.Offset(pntZacatek);
		tmbOpis.Location = location;
		tmbOpis.Name = "tmbOpis";
		tmbOpis.BorderStyle = BorderStyle.Fixed3D;
		tmbOpis.Size = new Size(736, _Plocha.HlavniOkno.DisplayRectangle.Height - 443);
		tmbOpis.TabIndex = 0;
		tmbZadani.Anchor = AnchorStyles.Top;
		tmbZadani.BackColor = PrihlasenyStudent.Uzivatel.BarvaPozadi;
		location = new Point(60, 125);
		location.Offset(pntZacatek);
		tmbZadani.Location = location;
		tmbZadani.Name = "tmbZadani";
		tmbZadani.BorderStyle = BorderStyle.Fixed3D;
		tmbZadani.Size = new Size(736, 195);
		tmbZadani.TabIndex = 1;
		ucSlapoty.Anchor = AnchorStyles.Top;
		ucSlapoty.BackColor = Color.White;
		ucSlapoty.CelkovyCas = TimeSpan.FromMinutes(CviceniPsani.Cas);
		location = new Point(60, 334);
		location.Offset(pntZacatek);
		ucSlapoty.Location = location;
		ucSlapoty.MaximumSize = new Size(736, 38);
		ucSlapoty.MinimumSize = new Size(736, 38);
		ucSlapoty.Name = "ucSlapoty";
		ucSlapoty.Size = new Size(736, 38);
		ucSlapoty.TabIndex = 2;
		if (cmdPrejitNaDalsi != null)
		{
			cmdPrejitNaDalsi.Anchor = AnchorStyles.Top;
			cmdPrejitNaDalsi.BackColor = Barva.PozadiTlacitekNaPlose;
			location = new Point(854, num);
			location.Offset(pntZacatek);
			cmdPrejitNaDalsi.Location = location;
			cmdPrejitNaDalsi.Name = "cmdPrejitNaDalsi";
			cmdPrejitNaDalsi.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlDalsiN;
			cmdPrejitNaDalsi.Size = new Size(126, 65);
			cmdPrejitNaDalsi.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDalsiD;
			cmdPrejitNaDalsi.TabIndex = 3;
			cmdPrejitNaDalsi.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDalsiZ;
			cmdPrejitNaDalsi.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDalsiH;
			ttt.SetToolTip(cmdPrejitNaDalsi, HYL.MountBlue.Resources.Texty.Cviceni_cmdPrejitNaDalsi_TTT);
			num += 70;
		}
		cmdVyhodnotit.Anchor = AnchorStyles.Top;
		cmdVyhodnotit.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, num);
		location.Offset(pntZacatek);
		cmdVyhodnotit.Location = location;
		cmdVyhodnotit.Name = "cmdVyhodnotit";
		cmdVyhodnotit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlVyhodnotitN;
		cmdVyhodnotit.Size = new Size(126, 43);
		cmdVyhodnotit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlVyhodnotitD;
		cmdVyhodnotit.TabIndex = 4;
		cmdVyhodnotit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlVyhodnotitZ;
		cmdVyhodnotit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlVyhodnotitH;
		ttt.SetToolTip(cmdVyhodnotit, HYL.MountBlue.Resources.Texty.Cviceni_cmdVyhodnotit_TTT);
		num += 48;
		cmdNavratDomu.Anchor = AnchorStyles.Top;
		cmdNavratDomu.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, num);
		location.Offset(pntZacatek);
		cmdNavratDomu.Location = location;
		cmdNavratDomu.Name = "cmdNastaveni";
		cmdNavratDomu.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuN;
		cmdNavratDomu.Size = new Size(126, 43);
		cmdNavratDomu.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuD;
		cmdNavratDomu.TabIndex = 5;
		cmdNavratDomu.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuZ;
		cmdNavratDomu.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuH;
		cmdNavratDomu.TlacitkoStisknuto += cmdNavratDomu_TlacitkoStisknuto;
		ttt.SetToolTip(cmdNavratDomu, HYL.MountBlue.Resources.Texty.Cviceni_cmdNavratDomu_TTT);
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(ucSlapoty);
		deleg(tmbOpis);
		deleg(tmbZadani);
		deleg(cmdNavratDomu);
		deleg(cmdVyhodnotit);
		deleg(cmdPrejitNaDalsi);
	}

	protected virtual void cmdNavratDomu_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitDomu();
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if (++PocetStiskuF6 > 5)
			{
				ZobrazitOknoScviceniID();
			}
			return true;
		}
		PocetStiskuF6 = 0;
		if (e.KeyCode == Keys.Escape && cmdNavratDomu.Enabled)
		{
			cmdNavratDomu_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}

	private void ZobrazitOknoScviceniID()
	{
		PocetStiskuF6 = 0;
		string arg = CviceniPsani.IDsLekci();
		MsgBoxBublina.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.msgboxCviceniID, arg), HYL.MountBlue.Resources.Texty.msgboxCviceniID_Title, eMsgBoxTlacitka.OK);
	}

	internal override bool FixniVyska()
	{
		return false;
	}

	internal override int VyskaTlacitek()
	{
		return 161;
	}
}
