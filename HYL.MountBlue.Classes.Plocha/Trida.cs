using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class Trida : _PlochaSprava
{
	private int tridaID;

	private ZalozkyTrida.eZalozky zalozka;

	private ObrazkoveTlacitko cmdPridatStudenta;

	private ObrazkoveTlacitko cmdTextyCviceni;

	private ObrazkoveTlacitko cmdNastaveni;

	private ObrazkoveTlacitko cmdLavina;

	private ObrazkoveTlacitko cmdSezeni;

	private ObrazkoveTlacitko cmdZavritTridu;

	private ZalozkyTrida zalTrida;

	private HYL.MountBlue.Controls.Trida ucTrida;

	private Timer tmrObnovit;

	private PUcitel PrihlasenyUcitel => (PUcitel)PrihlasenyUzivatel;

	public Trida(PUcitel ucitel, int tridaID, ZalozkyTrida.eZalozky zalozka)
		: base(ucitel)
	{
		this.tridaID = tridaID;
		this.zalozka = zalozka;
	}

	internal override bool FixniVyska()
	{
		return false;
	}

	internal override int VyskaTlacitek()
	{
		return 246;
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		Rectangle rObdelnik = new Rectangle(47, 182, 764, _Plocha.HlavniOkno.DisplayRectangle.Height - 250);
		rObdelnik.Inflate(8, 8);
		GraphicsPath path = SpecialniGrafika.CestaZaoblenehoObdelniku(rObdelnik, 4);
		Brush brush = new SolidBrush(Barva.ObdelnikZaTextBoxem);
		G.FillPath(brush, path);
	}

	protected override int TridaID()
	{
		return tridaID;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdPridatStudenta = new ObrazkoveTlacitko();
		cmdTextyCviceni = new ObrazkoveTlacitko();
		cmdNastaveni = new ObrazkoveTlacitko();
		cmdLavina = new ObrazkoveTlacitko();
		cmdSezeni = new ObrazkoveTlacitko();
		cmdZavritTridu = new ObrazkoveTlacitko();
		zalTrida = new ZalozkyTrida();
		ucTrida = new HYL.MountBlue.Controls.Trida(tridaID);
		tmrObnovit = new Timer();
		cmdPridatStudenta.Anchor = AnchorStyles.Top;
		cmdPridatStudenta.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 147);
		location.Offset(pntZacatek);
		cmdPridatStudenta.Location = location;
		cmdPridatStudenta.Name = "cmdPridatStudenta";
		cmdPridatStudenta.NormalniObrazek = GrafikaSkolni.pngTlPrStudentN;
		cmdPridatStudenta.Size = new Size(126, 38);
		cmdPridatStudenta.StisknutyObrazek = GrafikaSkolni.pngTlPrStudentD;
		cmdPridatStudenta.TabIndex = 4;
		cmdPridatStudenta.ZakazanyObrazek = GrafikaSkolni.pngTlPrStudentZ;
		cmdPridatStudenta.ZvyraznenyObrazek = GrafikaSkolni.pngTlPrStudentH;
		cmdPridatStudenta.TlacitkoStisknuto += cmdPridatStudenta_TlacitkoStisknuto;
		ttt.SetToolTip(cmdPridatStudenta, HYL.MountBlue.Resources.Texty.Plocha_cmdPridatStudenta_TTT);
		cmdTextyCviceni.Anchor = AnchorStyles.Top;
		cmdTextyCviceni.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 189);
		location.Offset(pntZacatek);
		cmdTextyCviceni.Location = location;
		cmdTextyCviceni.Name = "cmdTextyCviceni";
		cmdTextyCviceni.NormalniObrazek = GrafikaSkolni.pngTlTextyN;
		cmdTextyCviceni.Size = new Size(126, 38);
		cmdTextyCviceni.StisknutyObrazek = GrafikaSkolni.pngTlTextyD;
		cmdTextyCviceni.TabIndex = 5;
		cmdTextyCviceni.ZakazanyObrazek = GrafikaSkolni.pngTlTextyZ;
		cmdTextyCviceni.ZvyraznenyObrazek = GrafikaSkolni.pngTlTextyH;
		cmdTextyCviceni.TlacitkoStisknuto += cmdTextyCviceni_TlacitkoStisknuto;
		ttt.SetToolTip(cmdTextyCviceni, HYL.MountBlue.Resources.Texty.Plocha_cmdTextyCviceni_TTT);
		cmdNastaveni.Anchor = AnchorStyles.Top;
		cmdNastaveni.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 231);
		location.Offset(pntZacatek);
		cmdNastaveni.Location = location;
		cmdNastaveni.Name = "cmdNastaveni";
		cmdNastaveni.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniN;
		cmdNastaveni.Size = new Size(126, 25);
		cmdNastaveni.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniD;
		cmdNastaveni.TabIndex = 7;
		cmdNastaveni.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniZ;
		cmdNastaveni.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniH;
		cmdNastaveni.TlacitkoStisknuto += cmdNastaveni_TlacitkoStisknuto;
		ttt.SetToolTip(cmdNastaveni, HYL.MountBlue.Resources.Texty.Plocha_cmdNastaveni_TTT);
		cmdLavina.Anchor = AnchorStyles.Top;
		cmdLavina.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 260);
		location.Offset(pntZacatek);
		cmdLavina.Location = location;
		cmdLavina.Name = "cmdLavina";
		cmdLavina.NormalniObrazek = GrafikaSkolni.pngTlLavina0N;
		cmdLavina.Size = new Size(126, 38);
		cmdLavina.StisknutyObrazek = GrafikaSkolni.pngTlLavina0D;
		cmdLavina.TabIndex = 8;
		cmdLavina.ZakazanyObrazek = GrafikaSkolni.pngTlLavina0Z;
		cmdLavina.ZvyraznenyObrazek = GrafikaSkolni.pngTlLavina0H;
		cmdLavina.TlacitkoStisknuto += cmdLavina_TlacitkoStisknuto;
		ttt.SetToolTip(cmdLavina, HYL.MountBlue.Resources.Texty.Plocha_cmdLavina_TTT);
		cmdSezeni.Anchor = AnchorStyles.Top;
		cmdSezeni.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 302);
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
		cmdZavritTridu.Anchor = AnchorStyles.Top;
		cmdZavritTridu.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 350);
		location.Offset(pntZacatek);
		cmdZavritTridu.Location = location;
		cmdZavritTridu.Name = "cmdZavritTridu";
		cmdZavritTridu.NormalniObrazek = GrafikaSkolni.pngTlZavritTriduN;
		cmdZavritTridu.Size = new Size(126, 43);
		cmdZavritTridu.StisknutyObrazek = GrafikaSkolni.pngTlZavritTriduD;
		cmdZavritTridu.TabIndex = 10;
		cmdZavritTridu.ZakazanyObrazek = GrafikaSkolni.pngTlZavritTriduZ;
		cmdZavritTridu.ZvyraznenyObrazek = GrafikaSkolni.pngTlZavritTriduH;
		cmdZavritTridu.TlacitkoStisknuto += cmdZavritTridu_TlacitkoStisknuto;
		ttt.SetToolTip(cmdZavritTridu, HYL.MountBlue.Resources.Texty.Plocha_cmdZavritTridu_TTT);
		zalTrida.Anchor = AnchorStyles.Top;
		zalTrida.BackColor = Color.White;
		location = new Point(50, 145);
		location.Offset(pntZacatek);
		zalTrida.Location = location;
		zalTrida.Size = new Size(259, 29);
		zalTrida.ZmenaZalozky += zalTrida_ZmenaZalozky;
		ucTrida.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
		ucTrida.BackColor = Color.White;
		location = new Point(47, 182);
		location.Offset(pntZacatek);
		ucTrida.Location = location;
		ucTrida.Name = "ucTrida";
		ucTrida.BorderStyle = BorderStyle.Fixed3D;
		ucTrida.Size = new Size(764, _Plocha.HlavniOkno.DisplayRectangle.Height - 250);
		ucTrida.TabIndex = 0;
		ucTrida.ZobrazitHistorii += ucTrida_ZobrazitHistorii;
		tmrObnovit.Enabled = false;
		tmrObnovit.Interval = 30000;
		tmrObnovit.Tick += tmrObnovit_Tick;
	}

	private void ObnovitSeznam()
	{
		ucTrida.Obnovit();
	}

	private void ucTrida_ZobrazitHistorii(uint uid, ZalozkyHistorie.eZalozky zalozka)
	{
		PrihlasenyUcitel.ZobrazitHistorii(tridaID, uid, zalozka);
	}

	private void tmrObnovit_Tick(object sender, EventArgs e)
	{
		ObnovitSeznam();
	}

	private void zalTrida_ZmenaZalozky(ZalozkyTrida.eZalozky novaZalozka)
	{
		ucTrida.ZobrazitZalozku(novaZalozka);
		ucTrida.Focus();
	}

	private void cmdLavina_TlacitkoStisknuto()
	{
		Lavina.ZobrazitLavinu(PrihlasenyUcitel.Ucitel, tridaID);
	}

	private void cmdTextyCviceni_TlacitkoStisknuto()
	{
		TextyCviceni.ZobrazitTextyCviceni(PrihlasenyUcitel.Ucitel);
	}

	internal override void ZacatekPlochy()
	{
		base.ZacatekPlochy();
		zalTrida.AktivniZalozka = zalozka;
		ucTrida.Focus();
		tmrObnovit.Start();
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdPridatStudenta);
		deleg(cmdTextyCviceni);
		deleg(cmdNastaveni);
		deleg(cmdLavina);
		deleg(cmdSezeni);
		deleg(cmdZavritTridu);
		deleg(zalTrida);
		deleg(ucTrida);
	}

	internal override void DeinicializovatPrvky()
	{
		base.DeinicializovatPrvky();
		tmrObnovit.Stop();
		tmrObnovit.Dispose();
	}

	private void cmdNastaveni_TlacitkoStisknuto()
	{
		if (PrihlasenyUzivatel.ZobrazitUpravitUzivatele())
		{
			_Plocha.AktualniPlocha.ObnovitGrafiku();
		}
	}

	private void cmdZavritTridu_TlacitkoStisknuto()
	{
		PrihlasenyUzivatel.ZobrazitDomu();
	}

	private void cmdTisk_TlacitkoStisknuto()
	{
	}

	private void cmdPridatStudenta_TlacitkoStisknuto()
	{
		if (PUzivatele.PridatStudenta(out var uid, tridaID))
		{
			zalTrida.AktivniZalozka = ZalozkyTrida.eZalozky.SeznamStudentu;
			ucTrida.PridanNovyStudent(uid);
		}
	}

	private void cmdSezeni_TlacitkoStisknuto()
	{
		PrihlasenyUcitel.ZobrazitSpravneSezeni(tridaID);
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.S && cmdPridatStudenta.Enabled)
		{
			cmdPridatStudenta_TlacitkoStisknuto();
			return true;
		}
		if (e.Control && e.KeyCode == Keys.T && cmdTextyCviceni.Enabled)
		{
			cmdTextyCviceni_TlacitkoStisknuto();
			return true;
		}
		if (e.Control && e.KeyCode == Keys.N && cmdNastaveni.Enabled)
		{
			cmdNastaveni_TlacitkoStisknuto();
			return true;
		}
		if (e.Control && e.KeyCode == Keys.L && cmdLavina.Enabled)
		{
			cmdLavina_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.F12)
		{
			cmdSezeni_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.Escape)
		{
			cmdZavritTridu_TlacitkoStisknuto();
			return true;
		}
		if (e.Control && e.KeyCode == Keys.Tab)
		{
			zalTrida.NastavitDalsiZalozku();
			return true;
		}
		if (e.KeyCode == Keys.F5)
		{
			ObnovitSeznam();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}
}
