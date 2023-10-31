using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class Hora : _Zahlavi
{
	private ObrazkoveTlacitko cmdTrenink;

	private ObrazkoveTlacitko cmdZacitPsat;

	private ObrazkoveTlacitko cmdNastaveni;

	private ObrazkoveTlacitko cmdSezeni;

	private ObrazkoveTlacitko cmdCoUzUmim;

	private ObrazkoveTlacitko cmdMojeZnamky;

	private ObrazkoveTlacitko cmdOdhlasit;

	private Info ucInfo;

	public Hora(PStudent prihlStudent)
		: base(prihlStudent)
	{
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		VykreslitHoru(G);
		VykreslitVlajkuAtabulku(G, PrihlasenyStudent.Student);
		CestaNaHoru.VykreslitCestu(G, PrihlasenyStudent.Student.Cviceni.Lekce, PrihlasenyStudent.Student.Cviceni.JePrvniCviceniVlekci);
	}

	private void VykreslitHoru(Graphics G)
	{
		_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngHora, 21, 112);
	}

	private void VykreslitVlajkuAtabulku(Graphics G, HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student stu)
	{
		if (stu.VystupDokoncen)
		{
			_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngVlajkaFinale, 353, 150);
			VykreslitVrcholovouTabulku(G, stu.DatumDokonceni, stu.CeleJmenoStituly, stu.Pohlavi == Uzivatel.ePohlavi.muz);
		}
		else
		{
			_Plocha.VykreslitObrazek(G, HYL.MountBlue.Resources.Grafika.pngVlajka, 353, 156);
		}
	}

	private void VykreslitVrcholovouTabulku(Graphics G, DateTime DatumVystupu, string CeleJmeno, bool bMuz)
	{
		Brush brush = new SolidBrush(Barva.VrcholovaTabulkaPozadi);
		Rectangle rectangle = new Rectangle(120, 120, 200, 90);
		GraphicsPath path = SpecialniGrafika.CestaZaoblenehoObdelniku(rectangle, 8);
		G.FillPath(brush, path);
		rectangle.Inflate(-4, -4);
		path = SpecialniGrafika.CestaZaoblenehoObdelniku(rectangle, 4);
		Pen pen = new Pen(Barva.VrcholovaTabulkaObrys);
		G.DrawPath(pen, path);
		rectangle.Inflate(-3, -3);
		Brush brush2 = new SolidBrush(Barva.VrcholovaTabulkaPismo);
		StringFormat stringFormat = new StringFormat();
		stringFormat.Alignment = StringAlignment.Center;
		stringFormat.LineAlignment = StringAlignment.Center;
		string text = ((!bMuz) ? HYL.MountBlue.Resources.Texty.Hora_VrcholovaTabulkaZ : HYL.MountBlue.Resources.Texty.Hora_VrcholovaTabulkaM);
		string s = string.Format(Text.TextNBSP(text), DatumVystupu, CeleJmeno);
		G.DrawString(s, _Plocha.VychoziPismo, brush2, rectangle, stringFormat);
	}

	internal override bool FixniVyska()
	{
		return true;
	}

	internal override int VyskaTlacitek()
	{
		return 240;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdOdhlasit = new ObrazkoveTlacitko();
		cmdMojeZnamky = new ObrazkoveTlacitko();
		cmdCoUzUmim = new ObrazkoveTlacitko();
		cmdSezeni = new ObrazkoveTlacitko();
		cmdNastaveni = new ObrazkoveTlacitko();
		cmdZacitPsat = new ObrazkoveTlacitko();
		cmdTrenink = new ObrazkoveTlacitko();
		if (PrihlasenyStudent.Student.JeNaPrvnimCviceni)
		{
			ucInfo = new Info();
		}
		cmdOdhlasit.Anchor = AnchorStyles.Top;
		cmdOdhlasit.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 362);
		location.Offset(pntZacatek);
		cmdOdhlasit.Location = location;
		cmdOdhlasit.Name = "cmdOdhlasit";
		cmdOdhlasit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOdhlasitN;
		cmdOdhlasit.Size = new Size(126, 25);
		cmdOdhlasit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOdhlasitD;
		cmdOdhlasit.TabIndex = 12;
		cmdOdhlasit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOdhlasitZ;
		cmdOdhlasit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOdhlasitH;
		cmdOdhlasit.TlacitkoStisknuto += cmdOdhlasit_TlacitkoStisknuto;
		ttt.SetToolTip(cmdOdhlasit, HYL.MountBlue.Resources.Texty.Plocha_cmdOdhlasitSe_TTT);
		cmdMojeZnamky.Anchor = AnchorStyles.Top;
		cmdMojeZnamky.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 314);
		location.Offset(pntZacatek);
		cmdMojeZnamky.Location = location;
		cmdMojeZnamky.Name = "cmdMojeZnamky";
		cmdMojeZnamky.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZnamkyN;
		cmdMojeZnamky.Size = new Size(126, 43);
		cmdMojeZnamky.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZnamkyD;
		cmdMojeZnamky.TabIndex = 11;
		cmdMojeZnamky.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZnamkyZ;
		cmdMojeZnamky.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZnamkyH;
		cmdMojeZnamky.TlacitkoStisknuto += cmdMojeZnamky_TlacitkoStisknuto;
		ttt.SetToolTip(cmdMojeZnamky, HYL.MountBlue.Resources.Texty.Hora_cmdMojeZnamky_TTT);
		cmdCoUzUmim.Anchor = AnchorStyles.Top;
		cmdCoUzUmim.BackColor = Barva.PozadiTlacitekNaPlose;
		cmdCoUzUmim.Enabled = PrihlasenyStudent.Student.ZnameKlavesy != null && PrihlasenyStudent.Student.ZnameKlavesy.Length > 0;
		location = new Point(854, 266);
		location.Offset(pntZacatek);
		cmdCoUzUmim.Location = location;
		cmdCoUzUmim.Name = "cmdCoUzUmim";
		cmdCoUzUmim.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlCoUmimN;
		cmdCoUzUmim.Size = new Size(126, 43);
		cmdCoUzUmim.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlCoUmimD;
		cmdCoUzUmim.TabIndex = 10;
		cmdCoUzUmim.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlCoUmimZ;
		cmdCoUzUmim.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlCoUmimH;
		cmdCoUzUmim.TlacitkoStisknuto += cmdCoUzUmim_TlacitkoStisknuto;
		ttt.SetToolTip(cmdCoUzUmim, HYL.MountBlue.Resources.Texty.Hora_cmdCoUzUmim_TTT);
		cmdSezeni.Anchor = AnchorStyles.Top;
		cmdSezeni.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 218);
		location.Offset(pntZacatek);
		cmdSezeni.Location = location;
		cmdSezeni.Name = "cmdSezeni";
		cmdSezeni.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlSezeniN;
		cmdSezeni.Size = new Size(126, 43);
		cmdSezeni.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlSezeniD;
		cmdSezeni.TabIndex = 9;
		cmdSezeni.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlSezeniZ;
		cmdSezeni.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlSezeniH;
		cmdSezeni.TlacitkoStisknuto += cmdSezeni_TlacitkoStisknuto;
		ttt.SetToolTip(cmdSezeni, HYL.MountBlue.Resources.Texty.Plocha_cmdSezeni_TTT);
		cmdNastaveni.Anchor = AnchorStyles.Top;
		cmdNastaveni.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 188);
		location.Offset(pntZacatek);
		cmdNastaveni.Location = location;
		cmdNastaveni.Name = "cmdNastaveni";
		cmdNastaveni.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniN;
		cmdNastaveni.Size = new Size(126, 25);
		cmdNastaveni.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniD;
		cmdNastaveni.TabIndex = 8;
		cmdNastaveni.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniZ;
		cmdNastaveni.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniH;
		cmdNastaveni.TlacitkoStisknuto += cmdNastaveni_TlacitkoStisknuto;
		ttt.SetToolTip(cmdNastaveni, HYL.MountBlue.Resources.Texty.Plocha_cmdNastaveni_TTT);
		cmdZacitPsat.Visible = !PrihlasenyStudent.Student.VyukaDokoncena;
		cmdTrenink.Visible = PrihlasenyStudent.Student.VyukaDokoncena;
		if (!PrihlasenyStudent.Student.VyukaDokoncena)
		{
			cmdZacitPsat.Anchor = AnchorStyles.Top;
			cmdZacitPsat.BackColor = Barva.PozadiTlacitekNaPlose;
			cmdZacitPsat.Enabled = !PrihlasenyStudent.Student.VyukaDokoncena;
			location = new Point(854, 147);
			location.Offset(pntZacatek);
			cmdZacitPsat.Location = location;
			cmdZacitPsat.Name = "cmdZacitPsat";
			cmdZacitPsat.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZacitPsatN;
			cmdZacitPsat.Size = new Size(126, 37);
			cmdZacitPsat.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZacitPsatD;
			cmdZacitPsat.TabIndex = 7;
			cmdZacitPsat.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZacitPsatZ;
			cmdZacitPsat.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZacitPsatH;
			cmdZacitPsat.TlacitkoStisknuto += cmdZacitPsat_TlacitkoStisknuto;
			ttt.SetToolTip(cmdZacitPsat, HYL.MountBlue.Resources.Texty.Hora_cmdZacitPsat_TTT);
		}
		else
		{
			cmdTrenink.Anchor = AnchorStyles.Top;
			cmdTrenink.BackColor = Barva.PozadiTlacitekNaPlose;
			cmdTrenink.Enabled = PrihlasenyStudent.Student.VyukaDokoncena;
			location = new Point(854, 147);
			location.Offset(pntZacatek);
			cmdTrenink.Location = location;
			cmdTrenink.Size = new Size(126, 37);
			cmdTrenink.TabIndex = 7;
			cmdTrenink.Name = "cmdTrenink";
			cmdTrenink.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlTreninkN;
			cmdTrenink.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlTreninkD;
			cmdTrenink.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlTreninkZ;
			cmdTrenink.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlTreninkH;
			cmdTrenink.TlacitkoStisknuto += cmdTrenink_TlacitkoStisknuto;
			ttt.SetToolTip(cmdTrenink, HYL.MountBlue.Resources.Texty.Hora_cmdTrenink_TTT);
		}
		if (ucInfo != null)
		{
			ucInfo.Anchor = AnchorStyles.Top;
			ucInfo.BackColor = Color.White;
			ucInfo.Font = new Font("Arial", 9f, FontStyle.Bold, GraphicsUnit.Point, 238);
			ucInfo.Name = "ucInfo";
			ucInfo.TabIndex = 16;
			ucInfo.NastavitText(Text.TextNBSP(HYL.MountBlue.Resources.Texty.Hora_UvitaciText));
			ucInfo.Size = ucInfo.DoporucenaVelikost();
			ucInfo.NastavitPozici(_Plocha.HlavniOkno);
		}
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdOdhlasit);
		deleg(cmdMojeZnamky);
		deleg(cmdCoUzUmim);
		deleg(cmdSezeni);
		deleg(cmdNastaveni);
		deleg(cmdZacitPsat);
		deleg(cmdTrenink);
		deleg(ucInfo);
	}

	private void cmdZacitPsat_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitZacitPsat();
	}

	private void cmdTrenink_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitTrenink();
	}

	private void cmdNastaveni_TlacitkoStisknuto()
	{
		if (PrihlasenyStudent.ZobrazitUpravitUzivatele())
		{
			_Plocha.AktualniPlocha.ObnovitGrafiku();
		}
	}

	private void cmdOdhlasit_TlacitkoStisknuto()
	{
		PrihlasenyStudent.OdhlasitUzivatele(bZobrazitPrihlaseni: true);
	}

	private void cmdSezeni_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitSpravneSezeni();
	}

	private void cmdCoUzUmim_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitCoUzUmim();
	}

	private void cmdMojeZnamky_TlacitkoStisknuto()
	{
		PrihlasenyStudent.ZobrazitMojeZnamky();
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Return && cmdZacitPsat.Enabled)
		{
			cmdZacitPsat_TlacitkoStisknuto();
			return true;
		}
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N && cmdNastaveni.Enabled)
		{
			cmdNastaveni_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.F12 && cmdSezeni.Enabled)
		{
			cmdSezeni_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.F5 && cmdCoUzUmim.Enabled)
		{
			cmdCoUzUmim_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.F11 && cmdMojeZnamky.Enabled)
		{
			cmdMojeZnamky_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.Escape)
		{
			cmdOdhlasit_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}
}
