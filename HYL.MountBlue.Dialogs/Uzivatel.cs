using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Uzivatele;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Classes.Uzivatele.Tridy;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Uzivatel : _Dialog
{
	public enum eParametry : uint
	{
		Jmeno = 2u,
		Tituly = 4u,
		UzivJmeno = 8u,
		Trida = 0x10u,
		Pismo = 0x20u,
		PocatecniLekce = 0x40u,
		DomaciVyuka = 0x80u,
		Email = 0x100u,
		UIDzeSkoly = 0x200u,
		Avatar = 0x400u,
		HesloZmenit = 0x1000u,
		HesloZrusit = 0x2000u,
		HesloVynulovat = 0x4000u,
		Aktivovat = 0x8000u,
		PresunNaTabor = 0x10000u
	}

	private struct TridaPolozka
	{
		public int TridaID;

		public string Popis;

		public override string ToString()
		{
			return Popis;
		}
	}

	private struct LekcePolozka
	{
		public int CisloLekce;

		public string Popis;

		public override string ToString()
		{
			return Popis;
		}
	}

	private bool bolJmeno;

	private bool bolTituly;

	private bool bolUzivJmeno;

	private bool bolTrida;

	private bool bolPismo;

	private bool bolPocatecniLekce;

	private bool bolDomaciVyuka;

	private bool bolEmail;

	private bool bolUIDzeSkoly;

	private bool bolAvatar;

	private bool bolAkce;

	private bool bolHesloZmenit;

	private bool bolHesloZrusit;

	private bool bolHesloVynulovat;

	private bool bolAktivovat;

	private bool bolPresunNaTabor;

	protected HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel;

	private Image iAvatarVlastni;

	private bool bAvatarZmenen;

	private eParametry parametry;

	private bool bNovyUzivatel;

	private IContainer components;

	private PictureBox picLineJmeno;

	private Panel pnlZapati;

	private PictureBox picLineAvatar;

	private AvatarComboBox lstAvatar;

	private Label lblAvatar;

	private ObrazkoveTlacitko cmdStorno;

	private ObrazkoveTlacitko cmdOK;

	private LinkLabel lnkAvatarVlastniVlozit;

	private GroupBox grpPismo;

	private RadioButton optPismoVelke;

	private RadioButton optPismoStredni;

	private RadioButton optPismoMale;

	private Label lblUkazka;

	private ColorComboBox ccbBarvaPozadi;

	private ColorComboBox ccbBarvaPisma;

	private Label lblBarvaPozadi;

	private Label lblBarvaPisma;

	private Label lblVelikostPisma;

	private LinkLabel lnkHesloZmenit;

	private Label lblUIDzeSkolyInfo;

	private Panel pnlHlavni;

	private Panel pnlJmeno;

	private TextBox txtPrijmeni;

	private GroupBox grpPohlavi;

	private RadioButton optZena;

	private RadioButton optMuz;

	private Label lblPohlavi;

	private TextBox txtJmeno;

	private Label lblPrijmeni;

	private Label lblJmeno;

	private Panel pnlTituly;

	private TextBox txtTitulyZa;

	private TextBox txtTitulyPred;

	private Label lblTitulyZa;

	private Label lblTitulyPred;

	private PictureBox picLineTituly;

	private Panel pnlUzivJmeno;

	private TextBox txtUzivJmeno;

	private Label lblUzivJmeno;

	private PictureBox picLineUzivJmeno;

	private Panel pnlTrida;

	private ComboBox lstTrida;

	private Label lblTrida;

	private PictureBox picLineTrida;

	private Panel pnlPismo;

	private PictureBox picLinePismo;

	private Panel pnlPocatecniLekce;

	private ComboBox lstPocatecniLekce;

	private PictureBox picLinePocatecniLekce;

	private CheckBox chkPocatecniLekce;

	private Panel pnlDomaciVyuka;

	private TextBox txtUID;

	private Label lblUID;

	private CheckBox chkDomaciVyuka;

	private PictureBox picLineDomaciVyuka;

	private LinkLabel lnkZaslatUIDnaEmail;

	private Panel pnlEmail;

	private TextBox txtEmail;

	private Label lblEmail;

	private PictureBox picLineEmail;

	private Panel pnlUIDzeSkoly;

	private PictureBox picLineUIDzeSkoly;

	private TextBox txtUIDzeSkoly;

	private Label lblUIDzeSkoly;

	private Panel pnlAvatar;

	private Panel pnlAkce;

	private Label lblAkce;

	private Panel pnlHesloVynulovat;

	private LinkLabel lnkHesloVynulovat;

	private Panel pnlHesloZmenit;

	private Panel pnlAktivovat;

	private LinkLabel lnkAktivovat;

	private Panel pnlPresunNaTabor;

	private LinkLabel lnkPresunNaTabor;

	private Panel pnlAkceZapati;

	private PictureBox pictureBox1;

	private Panel pnlHesloZrusit;

	private LinkLabel lnkHesloZrusit;

	public Uzivatel(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel, eParametry param, bool novyUzivatel)
	{
		InitializeComponent();
		if (param < eParametry.Jmeno)
		{
			throw new ArgumentOutOfRangeException("param", param, "Musí být zadána alespoň minimální hodnota.");
		}
		if (novyUzivatel)
		{
			Text = Texty.Uzivatel_Title_novy;
		}
		else
		{
			Text = Texty.Uzivatel_Title_upravit;
		}
		parametry = param;
		this.uzivatel = uzivatel;
		bNovyUzivatel = novyUzivatel;
		NacistHodnoty();
		int num = 0;
		if (bolJmeno)
		{
			num += pnlJmeno.Height;
		}
		if (bolTituly)
		{
			num += pnlTituly.Height;
		}
		if (bolUzivJmeno)
		{
			num += pnlUzivJmeno.Height;
		}
		if (bolTrida)
		{
			num += pnlTrida.Height;
		}
		if (bolPismo)
		{
			num += pnlPismo.Height;
		}
		if (bolPocatecniLekce)
		{
			num += pnlPocatecniLekce.Height;
		}
		if (bolDomaciVyuka)
		{
			num += pnlDomaciVyuka.Height;
		}
		if (bolEmail)
		{
			num += pnlEmail.Height;
		}
		if (bolUIDzeSkoly)
		{
			num += pnlUIDzeSkoly.Height;
		}
		if (bolAvatar)
		{
			num += pnlAvatar.Height;
		}
		if (bolAkce)
		{
			num += pnlAkce.Height + pnlAkceZapati.Height;
			if (bolHesloZmenit)
			{
				num += pnlHesloZmenit.Height;
			}
			if (bolHesloZrusit)
			{
				num += pnlHesloZrusit.Height;
			}
			if (bolHesloVynulovat)
			{
				num += pnlHesloVynulovat.Height;
			}
			if (bolAktivovat)
			{
				num += pnlAktivovat.Height;
			}
			if (bolPresunNaTabor)
			{
				num += pnlPresunNaTabor.Height;
			}
		}
		num += pnlZapati.Height;
		pnlHlavni.Height = num;
		base.Height = 47 + num;
		PocatecniLekceObnovit();
		HesloObnovit();
		DomaciVyukaObnovit();
		AktivovatObnovit();
		PresunNaTaborObnovit();
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		if (ValidovatHodnoty())
		{
			UlozitHodnoty();
			uzivatel.Ulozit();
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void NacistHodnoty()
	{
		bolJmeno = (parametry & eParametry.Jmeno) != 0;
		bolTituly = (parametry & eParametry.Tituly) != 0;
		bolUzivJmeno = (parametry & eParametry.UzivJmeno) != 0;
		bolTrida = (parametry & eParametry.Trida) != 0 && uzivatel is StudentSkolni;
		bolPismo = (parametry & eParametry.Pismo) != 0;
		bolPocatecniLekce = (parametry & eParametry.PocatecniLekce) != 0 && uzivatel is StudentSkolni;
		bolDomaciVyuka = (parametry & eParametry.DomaciVyuka) != 0 && uzivatel is StudentSkolni;
		bolEmail = (parametry & eParametry.Email) != 0;
		bolUIDzeSkoly = ((parametry & eParametry.UIDzeSkoly) != 0) & (uzivatel is StudentZeSkoly);
		bolAvatar = (parametry & eParametry.Avatar) != 0;
		bolHesloZmenit = (parametry & eParametry.HesloZmenit) != 0;
		bolHesloZrusit = (parametry & eParametry.HesloZrusit) != 0;
		bolHesloVynulovat = (parametry & eParametry.HesloVynulovat) != 0;
		bolAktivovat = (parametry & eParametry.Aktivovat) != 0;
		bolPresunNaTabor = (parametry & eParametry.PresunNaTabor) != 0;
		bolAkce = bolHesloZmenit || bolHesloZrusit || bolHesloVynulovat || bolAktivovat || bolPresunNaTabor;
		pnlJmeno.Visible = bolJmeno;
		pnlTituly.Visible = bolTituly;
		pnlUzivJmeno.Visible = bolUzivJmeno;
		pnlTrida.Visible = bolTrida;
		pnlPismo.Visible = bolPismo;
		pnlPocatecniLekce.Visible = bolPocatecniLekce;
		pnlDomaciVyuka.Visible = bolDomaciVyuka;
		pnlEmail.Visible = bolEmail;
		pnlUIDzeSkoly.Visible = bolUIDzeSkoly;
		pnlAvatar.Visible = bolAvatar;
		pnlAkce.Visible = bolAkce;
		pnlAkceZapati.Visible = bolAkce;
		pnlHesloZmenit.Visible = bolHesloZmenit;
		pnlHesloZrusit.Visible = bolHesloZrusit;
		pnlHesloVynulovat.Visible = bolHesloVynulovat;
		pnlAktivovat.Visible = bolAktivovat;
		pnlPresunNaTabor.Visible = bolPresunNaTabor;
		if (bolJmeno)
		{
			lblJmeno.Text = Texty.Uzivatel_lblJmeno;
			lblPrijmeni.Text = Texty.Uzivatel_lblPrijmeni;
			lblPohlavi.Text = Texty.Uzivatel_lblPohlavi;
			optMuz.Text = Texty.Uzivatel_optMuz;
			optZena.Text = Texty.Uzivatel_optZena;
			txtJmeno.Text = uzivatel.Jmeno;
			txtPrijmeni.Text = uzivatel.Prijmeni;
			HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel.ePohlavi pohlavi = uzivatel.Pohlavi;
			if (pohlavi == HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel.ePohlavi.zena)
			{
				optZena.Checked = true;
			}
			else
			{
				optMuz.Checked = true;
			}
		}
		if (bolTituly)
		{
			lblTitulyPred.Text = Texty.Uzivatel_lblTitulyPred;
			lblTitulyZa.Text = Texty.Uzivatel_lblTitulyZa;
			txtTitulyPred.Text = uzivatel.TitulyPred;
			txtTitulyZa.Text = uzivatel.TitulyZa;
		}
		if (bolUzivJmeno)
		{
			lblUzivJmeno.Text = Texty.Uzivatel_lblUzivJmeno;
			txtUzivJmeno.Text = uzivatel.UzivJmeno;
		}
		if (bolTrida)
		{
			lblTrida.Text = Texty.Uzivatel_lblTrida;
			lstTrida.Items.Clear();
			int tridaID = ((StudentSkolni)uzivatel).TridaID;
			int selectedIndex = 0;
			TridaPolozka tridaPolozka = default(TridaPolozka);
			tridaPolozka.TridaID = 0;
			tridaPolozka.Popis = Texty.Uzivatel_lstTrida_neuvedna;
			lstTrida.Items.Add(tridaPolozka);
			HYL.MountBlue.Classes.Uzivatele.Tridy.Trida[] array = Klient.Stanice.AktualniTridy.SerazenySeznamTrid();
			foreach (HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida in array)
			{
				tridaPolozka = default(TridaPolozka);
				tridaPolozka.TridaID = trida.TridaID;
				tridaPolozka.Popis = string.Format(Texty.Uzivatel_tridaPolozka, trida.ToString(), trida.UcitelTridyToString());
				lstTrida.Items.Add(tridaPolozka);
				if (tridaPolozka.TridaID == tridaID)
				{
					selectedIndex = lstTrida.Items.Count - 1;
				}
			}
			if (lstTrida.SelectedIndex < 0 && lstTrida.Items.Count > 0)
			{
				lstTrida.SelectedIndex = selectedIndex;
			}
		}
		if (bolPismo)
		{
			lblVelikostPisma.Text = Texty.Uzivatel_lblVelikostPisma;
			optPismoMale.Text = Texty.Uzivatel_optPismoMale;
			optPismoStredni.Text = Texty.Uzivatel_optPismoStredni;
			optPismoVelke.Text = Texty.Uzivatel_optPismoVelke;
			lblBarvaPisma.Text = Texty.Uzivatel_lblBarvaPisma;
			lblBarvaPozadi.Text = Texty.Uzivatel_lblBarvaPozadi;
			lblUkazka.Text = Texty.Uzivatel_lblUkazka;
			switch (uzivatel.VelikostPisma)
			{
			case Pismo.eVelikostPisma.mala:
				optPismoMale.Checked = true;
				break;
			case Pismo.eVelikostPisma.velka:
				optPismoVelke.Checked = true;
				break;
			default:
				optPismoStredni.Checked = true;
				break;
			}
			Barva.BarvyToComboBox(ref ccbBarvaPisma);
			Barva.BarvyToComboBox(ref ccbBarvaPozadi);
			int num = ccbBarvaPisma.Items.IndexOf(new BarvaPolozka(uzivatel.BarvaPisma, Color.Black, string.Empty));
			if (num == -1)
			{
				num = 1;
			}
			ccbBarvaPisma.SelectedIndex = num;
			num = ccbBarvaPozadi.Items.IndexOf(new BarvaPolozka(uzivatel.BarvaPozadi, Color.Black, string.Empty));
			if (num == -1)
			{
				num = 0;
			}
			ccbBarvaPozadi.SelectedIndex = num;
		}
		if (bolPocatecniLekce)
		{
			chkPocatecniLekce.Text = Texty.Uzivatel_chkPocatecniLekce;
			lstPocatecniLekce.Items.Clear();
			Klavesy.sLekceKlavesy[] array2 = _Lekce.Lekce().Klavesy.KlavesyDoArrayListu();
			Klavesy.sLekceKlavesy[] array3 = array2;
			for (int j = 0; j < array3.Length; j++)
			{
				Klavesy.sLekceKlavesy sLekceKlavesy = array3[j];
				if (sLekceKlavesy.Lekce > 1)
				{
					LekcePolozka lekcePolozka = default(LekcePolozka);
					lekcePolozka.CisloLekce = sLekceKlavesy.Lekce;
					lekcePolozka.Popis = string.Format(Texty.Uzivatel_lstPocatecniLekce_pol, sLekceKlavesy.Lekce, sLekceKlavesy.Text);
					lstPocatecniLekce.Items.Add(lekcePolozka);
				}
			}
			if (lstPocatecniLekce.Items.Count > 0)
			{
				lstPocatecniLekce.SelectedIndex = 0;
			}
		}
		if (bolDomaciVyuka)
		{
			chkDomaciVyuka.Text = Texty.Uzivatel_chkDomaciVyuka;
			lblUID.Text = Texty.Uzivatel_lblUID;
			lnkZaslatUIDnaEmail.Text = Texty.Uzivatel_lnkZaslatUIDnaEmail;
			chkDomaciVyuka.Checked = ((StudentSkolni)uzivatel).DomaciVyuka;
			txtUID.Text = uzivatel.UID.ToString();
		}
		if (bolEmail)
		{
			lblEmail.Text = Texty.Uzivatel_lblEmail;
			txtEmail.Text = uzivatel.Email;
		}
		if (bolUIDzeSkoly)
		{
			lblUIDzeSkolyInfo.Text = Texty.Uzivatel_lblUIDzeSkolyInfo;
			lblUIDzeSkoly.Text = Texty.Uzivatel_lblUIDzeSkoly;
			txtUIDzeSkoly.Text = string.Empty;
		}
		if (bolAvatar)
		{
			lblAvatar.Text = Texty.Uzivatel_lblObrazek;
			lnkAvatarVlastniVlozit.Text = Texty.Uzivatel_lnkAvatarVlastniVlozit;
			iAvatarVlastni = PUzivatel.AvatarUzivatele(uzivatel.AvatarImage);
			NacistAvatary(uzivatel.VlastniAvatar, uzivatel.AvatarCislo);
		}
		if (bolAkce)
		{
			lblAkce.Text = Texty.Uzivatel_lblAkce;
		}
		if (bolHesloZrusit)
		{
			lnkHesloZrusit.Text = Texty.Uzivatel_lnkHesloZrusit;
		}
		if (bolHesloVynulovat)
		{
			lnkHesloVynulovat.Text = Texty.Uzivatel_lnkHesloVynulovat;
		}
		if (bolPresunNaTabor)
		{
			lnkPresunNaTabor.Text = Texty.Uzivatel_lnkPresunoutNaTabor;
		}
	}

	private void NacistAvatary(bool bVybratVlastni, int iCisloAvataru)
	{
		lstAvatar.Items.Clear();
		HYL.MountBlue.Classes.Grafika.Avatary.ToComboBox(ref lstAvatar, HYL.MountBlue.Classes.Grafika.Avatary.AvataryIDuzivatel);
		if (uzivatel is Admin)
		{
			HYL.MountBlue.Classes.Grafika.Avatary.ToComboBox(ref lstAvatar, HYL.MountBlue.Classes.Grafika.Avatary.AvataryIDadmin);
		}
		else if (uzivatel is Ucitel)
		{
			HYL.MountBlue.Classes.Grafika.Avatary.ToComboBox(ref lstAvatar, HYL.MountBlue.Classes.Grafika.Avatary.AvataryIDucitel);
		}
		if (iAvatarVlastni != null)
		{
			lstAvatar.Items.Add(new AvatarPolozka(iAvatarVlastni, vlastniAvatar: true, -1));
		}
		for (int i = 0; i < lstAvatar.Items.Count; i++)
		{
			AvatarPolozka avatarPolozka = (AvatarPolozka)lstAvatar.Items[i];
			if ((bVybratVlastni && avatarPolozka.VlastniAvatar) || (!bVybratVlastni && avatarPolozka.AvatarID == iCisloAvataru))
			{
				lstAvatar.SelectedIndex = i;
				break;
			}
		}
		if (lstAvatar.SelectedIndex < 0)
		{
			lstAvatar.SelectedIndex = 0;
		}
	}

	protected virtual bool ValidovatHodnoty()
	{
		if (bolJmeno && txtJmeno.Text.Trim().Length == 0 && txtPrijmeni.Text.Trim().Length == 0)
		{
			MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgboxChybiJmenoPrijmeni, Texty.Uzivatel_msgboxChybiJmenoPrijmeni_Title, eMsgBoxTlacitka.OK);
			txtJmeno.Focus();
			return false;
		}
		if (bolUzivJmeno)
		{
			if (!HYL.MountBlue.Classes.Text.JeUzivJmenoKorektni(txtUzivJmeno.Text))
			{
				MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgUzivJmenoSpatne, Text, eMsgBoxTlacitka.OK);
				return false;
			}
			HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel = Klient.Stanice.AktualniUzivatele[txtUzivJmeno.Text];
			if (uzivatel != null && uzivatel.UID != this.uzivatel.UID)
			{
				MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgUzivJmenoExistuje, Text, eMsgBoxTlacitka.OK);
				return false;
			}
		}
		if (bolPismo && ccbBarvaPozadi.SelectedIndex == ccbBarvaPisma.SelectedIndex)
		{
			string textZpravy = HYL.MountBlue.Classes.Text.TextMuzZena(this.uzivatel.Pohlavi, Texty.Uzivatel_msgboxStejnaBarvaM, Texty.Uzivatel_msgboxStejnaBarvaZ);
			MsgBoxBublina.ZobrazitMessageBox(textZpravy, Texty.Uzivatel_msgboxStejnaBarva_Title, eMsgBoxTlacitka.OK);
			ccbBarvaPisma.Focus();
			return false;
		}
		if (bolPocatecniLekce && chkPocatecniLekce.Checked)
		{
			int cisloLekce = ((LekcePolozka)lstPocatecniLekce.SelectedItem).CisloLekce;
			if (MsgBoxBublina.ZobrazitMessageBox(string.Format(Texty.Uzivatel_msgUpozorneniPocLekce, cisloLekce), Text, eMsgBoxTlacitka.AnoNe) == DialogResult.No)
			{
				return false;
			}
		}
		if (bolEmail && txtEmail.Text.Length > 0 && !HYL.MountBlue.Classes.Text.JeTextEmail(txtEmail.Text))
		{
			MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgChybnyEmail, Text, eMsgBoxTlacitka.OK);
			return false;
		}
		if (bolUIDzeSkoly && !UID.OveritJmenoSouboru(txtUIDzeSkoly.Text, out var _))
		{
			MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgChybneUIDzeSkoly, Text, eMsgBoxTlacitka.OK);
			return false;
		}
		return true;
	}

	protected virtual void UlozitHodnoty()
	{
		if (bolJmeno)
		{
			uzivatel.Jmeno = txtJmeno.Text.Trim();
			uzivatel.Prijmeni = txtPrijmeni.Text.Trim();
			if (optZena.Checked)
			{
				uzivatel.Pohlavi = HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel.ePohlavi.zena;
			}
			else
			{
				uzivatel.Pohlavi = HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel.ePohlavi.muz;
			}
		}
		if (bolTituly)
		{
			uzivatel.TitulyPred = txtTitulyPred.Text.Trim();
			uzivatel.TitulyZa = txtTitulyZa.Text.Trim();
		}
		if (bolUzivJmeno)
		{
			uzivatel.UzivJmeno = txtUzivJmeno.Text;
		}
		if (bolTrida)
		{
			((StudentSkolni)uzivatel).TridaID = ((TridaPolozka)lstTrida.SelectedItem).TridaID;
		}
		if (bolPismo)
		{
			uzivatel.VelikostPisma = PismoOptToEnum();
			if (ccbBarvaPisma.SelectedIndex >= 0 && ccbBarvaPisma.SelectedItem is BarvaPolozka)
			{
				uzivatel.BarvaPisma = ((BarvaPolozka)ccbBarvaPisma.SelectedItem).Barva;
			}
			if (ccbBarvaPozadi.SelectedIndex >= 0 && ccbBarvaPozadi.SelectedItem is BarvaPolozka)
			{
				uzivatel.BarvaPozadi = ((BarvaPolozka)ccbBarvaPozadi.SelectedItem).Barva;
			}
		}
		if (bolPocatecniLekce && chkPocatecniLekce.Checked)
		{
			int cisloLekce = ((LekcePolozka)lstPocatecniLekce.SelectedItem).CisloLekce;
			((HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student)uzivatel).NastavitCviceni(new OznaceniCviceni(cisloLekce));
		}
		if (bolDomaciVyuka)
		{
			((StudentSkolni)uzivatel).DomaciVyuka = chkDomaciVyuka.Checked;
		}
		if (bolEmail)
		{
			uzivatel.Email = txtEmail.Text;
		}
		if (bolUIDzeSkoly)
		{
			UID.OveritJmenoSouboru(txtUIDzeSkoly.Text, out var uid);
			((StudentZeSkoly)uzivatel).SkolniUID = uid;
		}
		if (bolAvatar)
		{
			AvatarPolozka avatarPolozka = (AvatarPolozka)lstAvatar.SelectedItem;
			uzivatel.VlastniAvatar = avatarPolozka.VlastniAvatar;
			if (!avatarPolozka.VlastniAvatar)
			{
				uzivatel.AvatarCislo = avatarPolozka.AvatarID;
			}
			if (bAvatarZmenen)
			{
				uzivatel.UlozitAvatar(PUzivatel.AvatarUzivatele(iAvatarVlastni));
			}
		}
	}

	protected virtual void cmdStorno_TlacitkoStisknuto()
	{
		Close();
	}

	private void NastaveniUziv_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmdOK_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.Escape)
		{
			cmdStorno_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
	}

	private void NovyUzivatel_Resize(object sender, EventArgs e)
	{
		pnlZapati.Location = new Point(pnlZapati.Left, base.Height - pnlZapati.Left - pnlZapati.Height);
	}

	private void lnkAvatarVlastniVlozit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (HYL.MountBlue.Classes.Grafika.Avatary.ZobrazitNajitSoubor(out var cesta) && HYL.MountBlue.Classes.Grafika.Avatary.ZpracovatObrazek(cesta, ref iAvatarVlastni))
		{
			NacistAvatary(bVybratVlastni: true, 0);
			bAvatarZmenen = true;
		}
	}

	private void PrepocitatNahled()
	{
		ColorComboBox colorComboBox = ccbBarvaPisma;
		if (colorComboBox.SelectedIndex >= 0 && colorComboBox.Items.Count > 0 && colorComboBox.SelectedItem is BarvaPolozka)
		{
			lblUkazka.ForeColor = ((BarvaPolozka)colorComboBox.SelectedItem).Barva;
		}
		colorComboBox = ccbBarvaPozadi;
		if (colorComboBox.SelectedIndex >= 0 && colorComboBox.Items.Count > 0 && colorComboBox.SelectedItem is BarvaPolozka)
		{
			lblUkazka.BackColor = ((BarvaPolozka)colorComboBox.SelectedItem).Barva;
		}
		lblUkazka.Font = new Font("Courier New", (float)PismoOptToEnum(), FontStyle.Regular);
	}

	private void optPismo_CheckedChanged(object sender, EventArgs e)
	{
		PrepocitatNahled();
	}

	private void ccbBarva_SelectedIndexChanged(object sender, EventArgs e)
	{
		PrepocitatNahled();
	}

	private Pismo.eVelikostPisma PismoOptToEnum()
	{
		Pismo.eVelikostPisma result = Pismo.eVelikostPisma.stredni;
		if (optPismoMale.Checked)
		{
			result = Pismo.eVelikostPisma.mala;
		}
		else if (optPismoVelke.Checked)
		{
			result = Pismo.eVelikostPisma.velka;
		}
		return result;
	}

	private void chkPocatecniLekce_CheckedChanged(object sender, EventArgs e)
	{
		PocatecniLekceObnovit();
	}

	private void PocatecniLekceObnovit()
	{
		lstPocatecniLekce.Enabled = chkPocatecniLekce.Checked;
	}

	private void chkDomaciVyuka_CheckedChanged(object sender, EventArgs e)
	{
		DomaciVyukaObnovit();
	}

	private void DomaciVyukaObnovit()
	{
		bool @checked = chkDomaciVyuka.Checked;
		lblUID.Enabled = @checked;
		txtUID.Enabled = @checked;
		lnkZaslatUIDnaEmail.Enabled = @checked;
	}

	private void PresunNaTaborObnovit()
	{
		if (bolPresunNaTabor)
		{
			StudentSkolni studentSkolni = (StudentSkolni)uzivatel;
			lnkPresunNaTabor.Enabled = !studentSkolni.VyukaDokoncena && studentSkolni.CviceniMax.Lekce > 1;
		}
	}

	private void lnkZaslatUIDnaEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
	}

	private void lnkPresunNaTabor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		PresunNaTaborObnovit();
		if (lnkPresunNaTabor.Enabled)
		{
			if (uzivatel.JeUzivatelPrihlaseny)
			{
				MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgNelzePresunout, Text, eMsgBoxTlacitka.OK);
			}
			else
			{
				PresunNaTabor.ZobrazitPresunoutNaTabor(this, (StudentSkolni)uzivatel);
			}
		}
	}

	private void lnkHesloZmenit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if ((!uzivatel.MaHeslo || Heslo.OveritHeslo(uzivatel.Heslo, this)) && ZmenaHesla.ZobrazitZmenuHesla(this, uzivatel, bVynucenaZmenaHesla: false))
		{
			HesloObnovit();
		}
	}

	private void HesloObnovit()
	{
		if (bolHesloZmenit)
		{
			if (uzivatel.MaHeslo)
			{
				lnkHesloZmenit.Text = Texty.Uzivatel_lnkHesloZmenit_zm;
			}
			else
			{
				lnkHesloZmenit.Text = Texty.Uzivatel_lnkHesloZmenit_nast;
			}
		}
		if (bolHesloZrusit)
		{
			lnkHesloZrusit.Enabled = uzivatel.MaHeslo;
		}
		if (bolHesloVynulovat)
		{
			lnkHesloVynulovat.Enabled = uzivatel.MaHeslo;
		}
	}

	private void lnkHesloZrusit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (Heslo.OveritHeslo(uzivatel.Heslo, this))
		{
			uzivatel.VynulovatHeslo(bVynutitZmenu: false);
			HesloObnovit();
		}
	}

	private void lnkHesloVynulovat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgOpravduVynulovat, Text, eMsgBoxTlacitka.AnoNe) == DialogResult.Yes)
		{
			uzivatel.VynulovatHeslo(bVynutitZmenu: true);
			HesloObnovit();
		}
	}

	private void AktivovatObnovit()
	{
		if (uzivatel.Aktivni)
		{
			lnkAktivovat.Text = Texty.Uzivatel_lnkAktivovatDeakt;
		}
		else
		{
			lnkAktivovat.Text = Texty.Uzivatel_lnkAktivovatAkt;
		}
	}

	private void lnkAktivovat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (uzivatel.Aktivni)
		{
			if (MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgUzivDeaktivovat, Text, eMsgBoxTlacitka.AnoNe) == DialogResult.Yes)
			{
				uzivatel.Aktivni = false;
				uzivatel.Ulozit();
			}
		}
		else
		{
			uzivatel.Aktivni = true;
			uzivatel.Ulozit();
			MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgUzivAktivovan, Text, eMsgBoxTlacitka.OK);
		}
		AktivovatObnovit();
	}

	private void txtUzivJmeno_Enter(object sender, EventArgs e)
	{
		if (txtUzivJmeno.Text.Length == 0)
		{
			txtUzivJmeno.Text = string.Format(Texty.Uzivatel_uzivJmenoFormat, HYL.MountBlue.Classes.Text.TextBezDiakritiky(txtJmeno.Text), HYL.MountBlue.Classes.Text.TextBezDiakritiky(txtPrijmeni.Text));
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.picLineJmeno = new System.Windows.Forms.PictureBox();
		this.pnlZapati = new System.Windows.Forms.Panel();
		this.cmdStorno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lnkAvatarVlastniVlozit = new System.Windows.Forms.LinkLabel();
		this.picLineAvatar = new System.Windows.Forms.PictureBox();
		this.lstAvatar = new HYL.MountBlue.Controls.AvatarComboBox();
		this.lblAvatar = new System.Windows.Forms.Label();
		this.grpPismo = new System.Windows.Forms.GroupBox();
		this.optPismoVelke = new System.Windows.Forms.RadioButton();
		this.optPismoStredni = new System.Windows.Forms.RadioButton();
		this.optPismoMale = new System.Windows.Forms.RadioButton();
		this.lblUkazka = new System.Windows.Forms.Label();
		this.ccbBarvaPozadi = new HYL.MountBlue.Controls.ColorComboBox();
		this.ccbBarvaPisma = new HYL.MountBlue.Controls.ColorComboBox();
		this.lblBarvaPozadi = new System.Windows.Forms.Label();
		this.lblBarvaPisma = new System.Windows.Forms.Label();
		this.lblVelikostPisma = new System.Windows.Forms.Label();
		this.lnkHesloZmenit = new System.Windows.Forms.LinkLabel();
		this.lblUIDzeSkolyInfo = new System.Windows.Forms.Label();
		this.pnlHlavni = new System.Windows.Forms.Panel();
		this.pnlAkceZapati = new System.Windows.Forms.Panel();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.pnlPresunNaTabor = new System.Windows.Forms.Panel();
		this.lnkPresunNaTabor = new System.Windows.Forms.LinkLabel();
		this.pnlAktivovat = new System.Windows.Forms.Panel();
		this.lnkAktivovat = new System.Windows.Forms.LinkLabel();
		this.pnlHesloVynulovat = new System.Windows.Forms.Panel();
		this.lnkHesloVynulovat = new System.Windows.Forms.LinkLabel();
		this.pnlHesloZrusit = new System.Windows.Forms.Panel();
		this.lnkHesloZrusit = new System.Windows.Forms.LinkLabel();
		this.pnlHesloZmenit = new System.Windows.Forms.Panel();
		this.pnlAkce = new System.Windows.Forms.Panel();
		this.lblAkce = new System.Windows.Forms.Label();
		this.pnlAvatar = new System.Windows.Forms.Panel();
		this.pnlUIDzeSkoly = new System.Windows.Forms.Panel();
		this.picLineUIDzeSkoly = new System.Windows.Forms.PictureBox();
		this.txtUIDzeSkoly = new System.Windows.Forms.TextBox();
		this.lblUIDzeSkoly = new System.Windows.Forms.Label();
		this.pnlEmail = new System.Windows.Forms.Panel();
		this.picLineEmail = new System.Windows.Forms.PictureBox();
		this.txtEmail = new System.Windows.Forms.TextBox();
		this.lblEmail = new System.Windows.Forms.Label();
		this.pnlDomaciVyuka = new System.Windows.Forms.Panel();
		this.lnkZaslatUIDnaEmail = new System.Windows.Forms.LinkLabel();
		this.txtUID = new System.Windows.Forms.TextBox();
		this.lblUID = new System.Windows.Forms.Label();
		this.chkDomaciVyuka = new System.Windows.Forms.CheckBox();
		this.picLineDomaciVyuka = new System.Windows.Forms.PictureBox();
		this.pnlPocatecniLekce = new System.Windows.Forms.Panel();
		this.chkPocatecniLekce = new System.Windows.Forms.CheckBox();
		this.lstPocatecniLekce = new System.Windows.Forms.ComboBox();
		this.picLinePocatecniLekce = new System.Windows.Forms.PictureBox();
		this.pnlPismo = new System.Windows.Forms.Panel();
		this.picLinePismo = new System.Windows.Forms.PictureBox();
		this.pnlTrida = new System.Windows.Forms.Panel();
		this.lstTrida = new System.Windows.Forms.ComboBox();
		this.lblTrida = new System.Windows.Forms.Label();
		this.picLineTrida = new System.Windows.Forms.PictureBox();
		this.pnlUzivJmeno = new System.Windows.Forms.Panel();
		this.txtUzivJmeno = new System.Windows.Forms.TextBox();
		this.lblUzivJmeno = new System.Windows.Forms.Label();
		this.picLineUzivJmeno = new System.Windows.Forms.PictureBox();
		this.pnlTituly = new System.Windows.Forms.Panel();
		this.txtTitulyZa = new System.Windows.Forms.TextBox();
		this.txtTitulyPred = new System.Windows.Forms.TextBox();
		this.lblTitulyZa = new System.Windows.Forms.Label();
		this.lblTitulyPred = new System.Windows.Forms.Label();
		this.picLineTituly = new System.Windows.Forms.PictureBox();
		this.pnlJmeno = new System.Windows.Forms.Panel();
		this.txtPrijmeni = new System.Windows.Forms.TextBox();
		this.grpPohlavi = new System.Windows.Forms.GroupBox();
		this.optZena = new System.Windows.Forms.RadioButton();
		this.optMuz = new System.Windows.Forms.RadioButton();
		this.lblPohlavi = new System.Windows.Forms.Label();
		this.txtJmeno = new System.Windows.Forms.TextBox();
		this.lblPrijmeni = new System.Windows.Forms.Label();
		this.lblJmeno = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.picLineJmeno).BeginInit();
		this.pnlZapati.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineAvatar).BeginInit();
		this.grpPismo.SuspendLayout();
		this.pnlHlavni.SuspendLayout();
		this.pnlAkceZapati.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		this.pnlPresunNaTabor.SuspendLayout();
		this.pnlAktivovat.SuspendLayout();
		this.pnlHesloVynulovat.SuspendLayout();
		this.pnlHesloZrusit.SuspendLayout();
		this.pnlHesloZmenit.SuspendLayout();
		this.pnlAkce.SuspendLayout();
		this.pnlAvatar.SuspendLayout();
		this.pnlUIDzeSkoly.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineUIDzeSkoly).BeginInit();
		this.pnlEmail.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineEmail).BeginInit();
		this.pnlDomaciVyuka.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineDomaciVyuka).BeginInit();
		this.pnlPocatecniLekce.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLinePocatecniLekce).BeginInit();
		this.pnlPismo.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLinePismo).BeginInit();
		this.pnlTrida.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineTrida).BeginInit();
		this.pnlUzivJmeno.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineUzivJmeno).BeginInit();
		this.pnlTituly.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineTituly).BeginInit();
		this.pnlJmeno.SuspendLayout();
		this.grpPohlavi.SuspendLayout();
		base.SuspendLayout();
		this.picLineJmeno.BackColor = System.Drawing.Color.Black;
		this.picLineJmeno.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLineJmeno.Location = new System.Drawing.Point(0, 100);
		this.picLineJmeno.Name = "picLineJmeno";
		this.picLineJmeno.Size = new System.Drawing.Size(394, 1);
		this.picLineJmeno.TabIndex = 38;
		this.picLineJmeno.TabStop = false;
		this.pnlZapati.Controls.Add(this.cmdStorno);
		this.pnlZapati.Controls.Add(this.cmdOK);
		this.pnlZapati.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.pnlZapati.Location = new System.Drawing.Point(0, 788);
		this.pnlZapati.Name = "pnlZapati";
		this.pnlZapati.Size = new System.Drawing.Size(394, 31);
		this.pnlZapati.TabIndex = 17;
		this.cmdStorno.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdStorno.BackColor = System.Drawing.Color.White;
		this.cmdStorno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdStorno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdStorno.ForeColor = System.Drawing.Color.White;
		this.cmdStorno.Location = new System.Drawing.Point(323, 4);
		this.cmdStorno.Name = "cmdStorno";
		this.cmdStorno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoN;
		this.cmdStorno.Size = new System.Drawing.Size(70, 23);
		this.cmdStorno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoD;
		this.cmdStorno.TabIndex = 1;
		this.cmdStorno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoZ;
		this.cmdStorno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoH;
		this.cmdStorno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdStorno_TlacitkoStisknuto);
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(247, 4);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlUlozitN;
		this.cmdOK.Size = new System.Drawing.Size(70, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlUlozitD;
		this.cmdOK.TabIndex = 0;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlUlozitZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlUlozitH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.lnkAvatarVlastniVlozit.Location = new System.Drawing.Point(232, 7);
		this.lnkAvatarVlastniVlozit.Name = "lnkAvatarVlastniVlozit";
		this.lnkAvatarVlastniVlozit.Size = new System.Drawing.Size(151, 68);
		this.lnkAvatarVlastniVlozit.TabIndex = 2;
		this.lnkAvatarVlastniVlozit.TabStop = true;
		this.lnkAvatarVlastniVlozit.Text = "???";
		this.lnkAvatarVlastniVlozit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lnkAvatarVlastniVlozit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkAvatarVlastniVlozit_LinkClicked);
		this.picLineAvatar.BackColor = System.Drawing.Color.Black;
		this.picLineAvatar.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLineAvatar.Location = new System.Drawing.Point(0, 80);
		this.picLineAvatar.Name = "picLineAvatar";
		this.picLineAvatar.Size = new System.Drawing.Size(394, 1);
		this.picLineAvatar.TabIndex = 55;
		this.picLineAvatar.TabStop = false;
		this.lstAvatar.BackColor = System.Drawing.Color.WhiteSmoke;
		this.lstAvatar.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.lstAvatar.DropDownHeight = 300;
		this.lstAvatar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lstAvatar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lstAvatar.Font = new System.Drawing.Font("Arial", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lstAvatar.ForeColor = System.Drawing.Color.White;
		this.lstAvatar.FormattingEnabled = true;
		this.lstAvatar.IntegralHeight = false;
		this.lstAvatar.ItemHeight = 64;
		this.lstAvatar.Location = new System.Drawing.Point(108, 5);
		this.lstAvatar.MaxDropDownItems = 20;
		this.lstAvatar.Name = "lstAvatar";
		this.lstAvatar.Size = new System.Drawing.Size(107, 70);
		this.lstAvatar.TabIndex = 1;
		this.lblAvatar.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblAvatar.Location = new System.Drawing.Point(7, 7);
		this.lblAvatar.Name = "lblAvatar";
		this.lblAvatar.Size = new System.Drawing.Size(95, 40);
		this.lblAvatar.TabIndex = 0;
		this.lblAvatar.Text = "???";
		this.lblAvatar.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.grpPismo.Controls.Add(this.optPismoVelke);
		this.grpPismo.Controls.Add(this.optPismoStredni);
		this.grpPismo.Controls.Add(this.optPismoMale);
		this.grpPismo.ForeColor = System.Drawing.Color.Black;
		this.grpPismo.Location = new System.Drawing.Point(132, 5);
		this.grpPismo.Name = "grpPismo";
		this.grpPismo.Size = new System.Drawing.Size(237, 36);
		this.grpPismo.TabIndex = 1;
		this.grpPismo.TabStop = false;
		this.optPismoVelke.AutoSize = true;
		this.optPismoVelke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.optPismoVelke.Location = new System.Drawing.Point(162, 11);
		this.optPismoVelke.Name = "optPismoVelke";
		this.optPismoVelke.Size = new System.Drawing.Size(45, 19);
		this.optPismoVelke.TabIndex = 2;
		this.optPismoVelke.TabStop = true;
		this.optPismoVelke.Text = "???";
		this.optPismoVelke.UseVisualStyleBackColor = true;
		this.optPismoVelke.CheckedChanged += new System.EventHandler(optPismo_CheckedChanged);
		this.optPismoStredni.AutoSize = true;
		this.optPismoStredni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.optPismoStredni.Location = new System.Drawing.Point(86, 11);
		this.optPismoStredni.Name = "optPismoStredni";
		this.optPismoStredni.Size = new System.Drawing.Size(45, 19);
		this.optPismoStredni.TabIndex = 1;
		this.optPismoStredni.TabStop = true;
		this.optPismoStredni.Text = "???";
		this.optPismoStredni.UseVisualStyleBackColor = true;
		this.optPismoStredni.CheckedChanged += new System.EventHandler(optPismo_CheckedChanged);
		this.optPismoMale.AutoSize = true;
		this.optPismoMale.Checked = true;
		this.optPismoMale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.optPismoMale.Location = new System.Drawing.Point(9, 11);
		this.optPismoMale.Name = "optPismoMale";
		this.optPismoMale.Size = new System.Drawing.Size(45, 19);
		this.optPismoMale.TabIndex = 0;
		this.optPismoMale.TabStop = true;
		this.optPismoMale.Text = "???";
		this.optPismoMale.UseVisualStyleBackColor = true;
		this.optPismoMale.CheckedChanged += new System.EventHandler(optPismo_CheckedChanged);
		this.lblUkazka.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lblUkazka.Font = new System.Drawing.Font("Courier New", 18f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblUkazka.Location = new System.Drawing.Point(245, 50);
		this.lblUkazka.Name = "lblUkazka";
		this.lblUkazka.Size = new System.Drawing.Size(124, 66);
		this.lblUkazka.TabIndex = 6;
		this.lblUkazka.Text = "???";
		this.lblUkazka.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.ccbBarvaPozadi.BackColor = System.Drawing.Color.WhiteSmoke;
		this.ccbBarvaPozadi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.ccbBarvaPozadi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ccbBarvaPozadi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.ccbBarvaPozadi.Font = new System.Drawing.Font("Arial", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.ccbBarvaPozadi.ForeColor = System.Drawing.Color.White;
		this.ccbBarvaPozadi.FormattingEnabled = true;
		this.ccbBarvaPozadi.ItemHeight = 24;
		this.ccbBarvaPozadi.Location = new System.Drawing.Point(132, 86);
		this.ccbBarvaPozadi.MaxDropDownItems = 20;
		this.ccbBarvaPozadi.Name = "ccbBarvaPozadi";
		this.ccbBarvaPozadi.Size = new System.Drawing.Size(107, 30);
		this.ccbBarvaPozadi.TabIndex = 5;
		this.ccbBarvaPozadi.SelectedIndexChanged += new System.EventHandler(ccbBarva_SelectedIndexChanged);
		this.ccbBarvaPisma.BackColor = System.Drawing.Color.WhiteSmoke;
		this.ccbBarvaPisma.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		this.ccbBarvaPisma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.ccbBarvaPisma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.ccbBarvaPisma.Font = new System.Drawing.Font("Arial", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.ccbBarvaPisma.ForeColor = System.Drawing.Color.White;
		this.ccbBarvaPisma.FormattingEnabled = true;
		this.ccbBarvaPisma.ItemHeight = 24;
		this.ccbBarvaPisma.Location = new System.Drawing.Point(132, 50);
		this.ccbBarvaPisma.MaxDropDownItems = 20;
		this.ccbBarvaPisma.Name = "ccbBarvaPisma";
		this.ccbBarvaPisma.Size = new System.Drawing.Size(107, 30);
		this.ccbBarvaPisma.TabIndex = 3;
		this.ccbBarvaPisma.SelectedIndexChanged += new System.EventHandler(ccbBarva_SelectedIndexChanged);
		this.lblBarvaPozadi.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblBarvaPozadi.Location = new System.Drawing.Point(21, 86);
		this.lblBarvaPozadi.Name = "lblBarvaPozadi";
		this.lblBarvaPozadi.Size = new System.Drawing.Size(105, 30);
		this.lblBarvaPozadi.TabIndex = 4;
		this.lblBarvaPozadi.Text = "???";
		this.lblBarvaPozadi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblBarvaPisma.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblBarvaPisma.Location = new System.Drawing.Point(21, 50);
		this.lblBarvaPisma.Name = "lblBarvaPisma";
		this.lblBarvaPisma.Size = new System.Drawing.Size(105, 30);
		this.lblBarvaPisma.TabIndex = 2;
		this.lblBarvaPisma.Text = "???";
		this.lblBarvaPisma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblVelikostPisma.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblVelikostPisma.Location = new System.Drawing.Point(21, 15);
		this.lblVelikostPisma.Name = "lblVelikostPisma";
		this.lblVelikostPisma.Size = new System.Drawing.Size(105, 21);
		this.lblVelikostPisma.TabIndex = 0;
		this.lblVelikostPisma.Text = "???";
		this.lblVelikostPisma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lnkHesloZmenit.Dock = System.Windows.Forms.DockStyle.Right;
		this.lnkHesloZmenit.Location = new System.Drawing.Point(50, 0);
		this.lnkHesloZmenit.Name = "lnkHesloZmenit";
		this.lnkHesloZmenit.Size = new System.Drawing.Size(344, 17);
		this.lnkHesloZmenit.TabIndex = 0;
		this.lnkHesloZmenit.TabStop = true;
		this.lnkHesloZmenit.Text = "???";
		this.lnkHesloZmenit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lnkHesloZmenit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkHesloZmenit_LinkClicked);
		this.lblUIDzeSkolyInfo.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblUIDzeSkolyInfo.Location = new System.Drawing.Point(26, 6);
		this.lblUIDzeSkolyInfo.Name = "lblUIDzeSkolyInfo";
		this.lblUIDzeSkolyInfo.Size = new System.Drawing.Size(323, 33);
		this.lblUIDzeSkolyInfo.TabIndex = 0;
		this.lblUIDzeSkolyInfo.Text = "???";
		this.pnlHlavni.Controls.Add(this.pnlAkceZapati);
		this.pnlHlavni.Controls.Add(this.pnlPresunNaTabor);
		this.pnlHlavni.Controls.Add(this.pnlAktivovat);
		this.pnlHlavni.Controls.Add(this.pnlHesloVynulovat);
		this.pnlHlavni.Controls.Add(this.pnlHesloZrusit);
		this.pnlHlavni.Controls.Add(this.pnlZapati);
		this.pnlHlavni.Controls.Add(this.pnlHesloZmenit);
		this.pnlHlavni.Controls.Add(this.pnlAkce);
		this.pnlHlavni.Controls.Add(this.pnlAvatar);
		this.pnlHlavni.Controls.Add(this.pnlUIDzeSkoly);
		this.pnlHlavni.Controls.Add(this.pnlEmail);
		this.pnlHlavni.Controls.Add(this.pnlDomaciVyuka);
		this.pnlHlavni.Controls.Add(this.pnlPocatecniLekce);
		this.pnlHlavni.Controls.Add(this.pnlPismo);
		this.pnlHlavni.Controls.Add(this.pnlTrida);
		this.pnlHlavni.Controls.Add(this.pnlUzivJmeno);
		this.pnlHlavni.Controls.Add(this.pnlTituly);
		this.pnlHlavni.Controls.Add(this.pnlJmeno);
		this.pnlHlavni.Location = new System.Drawing.Point(7, 41);
		this.pnlHlavni.Name = "pnlHlavni";
		this.pnlHlavni.Size = new System.Drawing.Size(394, 819);
		this.pnlHlavni.TabIndex = 0;
		this.pnlAkceZapati.BackColor = System.Drawing.Color.Gainsboro;
		this.pnlAkceZapati.Controls.Add(this.pictureBox1);
		this.pnlAkceZapati.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlAkceZapati.Location = new System.Drawing.Point(0, 780);
		this.pnlAkceZapati.Name = "pnlAkceZapati";
		this.pnlAkceZapati.Size = new System.Drawing.Size(394, 7);
		this.pnlAkceZapati.TabIndex = 16;
		this.pictureBox1.BackColor = System.Drawing.Color.Black;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.pictureBox1.Location = new System.Drawing.Point(0, 6);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(394, 1);
		this.pictureBox1.TabIndex = 73;
		this.pictureBox1.TabStop = false;
		this.pnlPresunNaTabor.BackColor = System.Drawing.Color.Gainsboro;
		this.pnlPresunNaTabor.Controls.Add(this.lnkPresunNaTabor);
		this.pnlPresunNaTabor.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlPresunNaTabor.Location = new System.Drawing.Point(0, 763);
		this.pnlPresunNaTabor.Name = "pnlPresunNaTabor";
		this.pnlPresunNaTabor.Size = new System.Drawing.Size(394, 17);
		this.pnlPresunNaTabor.TabIndex = 15;
		this.lnkPresunNaTabor.Dock = System.Windows.Forms.DockStyle.Right;
		this.lnkPresunNaTabor.Location = new System.Drawing.Point(50, 0);
		this.lnkPresunNaTabor.Name = "lnkPresunNaTabor";
		this.lnkPresunNaTabor.Size = new System.Drawing.Size(344, 17);
		this.lnkPresunNaTabor.TabIndex = 0;
		this.lnkPresunNaTabor.TabStop = true;
		this.lnkPresunNaTabor.Text = "???";
		this.lnkPresunNaTabor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lnkPresunNaTabor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkPresunNaTabor_LinkClicked);
		this.pnlAktivovat.BackColor = System.Drawing.Color.Gainsboro;
		this.pnlAktivovat.Controls.Add(this.lnkAktivovat);
		this.pnlAktivovat.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlAktivovat.Location = new System.Drawing.Point(0, 746);
		this.pnlAktivovat.Name = "pnlAktivovat";
		this.pnlAktivovat.Size = new System.Drawing.Size(394, 17);
		this.pnlAktivovat.TabIndex = 14;
		this.lnkAktivovat.Dock = System.Windows.Forms.DockStyle.Right;
		this.lnkAktivovat.Location = new System.Drawing.Point(50, 0);
		this.lnkAktivovat.Name = "lnkAktivovat";
		this.lnkAktivovat.Size = new System.Drawing.Size(344, 17);
		this.lnkAktivovat.TabIndex = 0;
		this.lnkAktivovat.TabStop = true;
		this.lnkAktivovat.Text = "???";
		this.lnkAktivovat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lnkAktivovat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkAktivovat_LinkClicked);
		this.pnlHesloVynulovat.BackColor = System.Drawing.Color.Gainsboro;
		this.pnlHesloVynulovat.Controls.Add(this.lnkHesloVynulovat);
		this.pnlHesloVynulovat.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlHesloVynulovat.Location = new System.Drawing.Point(0, 729);
		this.pnlHesloVynulovat.Name = "pnlHesloVynulovat";
		this.pnlHesloVynulovat.Size = new System.Drawing.Size(394, 17);
		this.pnlHesloVynulovat.TabIndex = 13;
		this.lnkHesloVynulovat.Dock = System.Windows.Forms.DockStyle.Right;
		this.lnkHesloVynulovat.Location = new System.Drawing.Point(50, 0);
		this.lnkHesloVynulovat.Name = "lnkHesloVynulovat";
		this.lnkHesloVynulovat.Size = new System.Drawing.Size(344, 17);
		this.lnkHesloVynulovat.TabIndex = 0;
		this.lnkHesloVynulovat.TabStop = true;
		this.lnkHesloVynulovat.Text = "???";
		this.lnkHesloVynulovat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lnkHesloVynulovat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkHesloVynulovat_LinkClicked);
		this.pnlHesloZrusit.BackColor = System.Drawing.Color.Gainsboro;
		this.pnlHesloZrusit.Controls.Add(this.lnkHesloZrusit);
		this.pnlHesloZrusit.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlHesloZrusit.Location = new System.Drawing.Point(0, 712);
		this.pnlHesloZrusit.Name = "pnlHesloZrusit";
		this.pnlHesloZrusit.Size = new System.Drawing.Size(394, 17);
		this.pnlHesloZrusit.TabIndex = 12;
		this.lnkHesloZrusit.Dock = System.Windows.Forms.DockStyle.Right;
		this.lnkHesloZrusit.Location = new System.Drawing.Point(50, 0);
		this.lnkHesloZrusit.Name = "lnkHesloZrusit";
		this.lnkHesloZrusit.Size = new System.Drawing.Size(344, 17);
		this.lnkHesloZrusit.TabIndex = 0;
		this.lnkHesloZrusit.TabStop = true;
		this.lnkHesloZrusit.Text = "???";
		this.lnkHesloZrusit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lnkHesloZrusit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkHesloZrusit_LinkClicked);
		this.pnlHesloZmenit.BackColor = System.Drawing.Color.Gainsboro;
		this.pnlHesloZmenit.Controls.Add(this.lnkHesloZmenit);
		this.pnlHesloZmenit.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlHesloZmenit.Location = new System.Drawing.Point(0, 695);
		this.pnlHesloZmenit.Name = "pnlHesloZmenit";
		this.pnlHesloZmenit.Size = new System.Drawing.Size(394, 17);
		this.pnlHesloZmenit.TabIndex = 11;
		this.pnlAkce.BackColor = System.Drawing.Color.Gainsboro;
		this.pnlAkce.Controls.Add(this.lblAkce);
		this.pnlAkce.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlAkce.Location = new System.Drawing.Point(0, 675);
		this.pnlAkce.Name = "pnlAkce";
		this.pnlAkce.Size = new System.Drawing.Size(394, 20);
		this.pnlAkce.TabIndex = 10;
		this.lblAkce.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblAkce.Location = new System.Drawing.Point(13, 3);
		this.lblAkce.Name = "lblAkce";
		this.lblAkce.Size = new System.Drawing.Size(113, 14);
		this.lblAkce.TabIndex = 0;
		this.lblAkce.Text = "???";
		this.lblAkce.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.pnlAvatar.Controls.Add(this.lnkAvatarVlastniVlozit);
		this.pnlAvatar.Controls.Add(this.picLineAvatar);
		this.pnlAvatar.Controls.Add(this.lblAvatar);
		this.pnlAvatar.Controls.Add(this.lstAvatar);
		this.pnlAvatar.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlAvatar.Location = new System.Drawing.Point(0, 594);
		this.pnlAvatar.Name = "pnlAvatar";
		this.pnlAvatar.Size = new System.Drawing.Size(394, 81);
		this.pnlAvatar.TabIndex = 9;
		this.pnlUIDzeSkoly.Controls.Add(this.picLineUIDzeSkoly);
		this.pnlUIDzeSkoly.Controls.Add(this.txtUIDzeSkoly);
		this.pnlUIDzeSkoly.Controls.Add(this.lblUIDzeSkolyInfo);
		this.pnlUIDzeSkoly.Controls.Add(this.lblUIDzeSkoly);
		this.pnlUIDzeSkoly.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlUIDzeSkoly.Location = new System.Drawing.Point(0, 521);
		this.pnlUIDzeSkoly.Name = "pnlUIDzeSkoly";
		this.pnlUIDzeSkoly.Size = new System.Drawing.Size(394, 73);
		this.pnlUIDzeSkoly.TabIndex = 8;
		this.picLineUIDzeSkoly.BackColor = System.Drawing.Color.Black;
		this.picLineUIDzeSkoly.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLineUIDzeSkoly.Location = new System.Drawing.Point(0, 72);
		this.picLineUIDzeSkoly.Name = "picLineUIDzeSkoly";
		this.picLineUIDzeSkoly.Size = new System.Drawing.Size(394, 1);
		this.picLineUIDzeSkoly.TabIndex = 72;
		this.picLineUIDzeSkoly.TabStop = false;
		this.txtUIDzeSkoly.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtUIDzeSkoly.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtUIDzeSkoly.ForeColor = System.Drawing.Color.Black;
		this.txtUIDzeSkoly.Location = new System.Drawing.Point(132, 43);
		this.txtUIDzeSkoly.MaxLength = 8;
		this.txtUIDzeSkoly.Name = "txtUIDzeSkoly";
		this.txtUIDzeSkoly.Size = new System.Drawing.Size(76, 21);
		this.txtUIDzeSkoly.TabIndex = 2;
		this.lblUIDzeSkoly.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblUIDzeSkoly.Location = new System.Drawing.Point(3, 43);
		this.lblUIDzeSkoly.Name = "lblUIDzeSkoly";
		this.lblUIDzeSkoly.Size = new System.Drawing.Size(123, 21);
		this.lblUIDzeSkoly.TabIndex = 1;
		this.lblUIDzeSkoly.Text = "???";
		this.lblUIDzeSkoly.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.pnlEmail.Controls.Add(this.picLineEmail);
		this.pnlEmail.Controls.Add(this.txtEmail);
		this.pnlEmail.Controls.Add(this.lblEmail);
		this.pnlEmail.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlEmail.Location = new System.Drawing.Point(0, 486);
		this.pnlEmail.Name = "pnlEmail";
		this.pnlEmail.Size = new System.Drawing.Size(394, 35);
		this.pnlEmail.TabIndex = 7;
		this.picLineEmail.BackColor = System.Drawing.Color.Black;
		this.picLineEmail.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLineEmail.Location = new System.Drawing.Point(0, 34);
		this.picLineEmail.Name = "picLineEmail";
		this.picLineEmail.Size = new System.Drawing.Size(394, 1);
		this.picLineEmail.TabIndex = 68;
		this.picLineEmail.TabStop = false;
		this.txtEmail.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtEmail.ForeColor = System.Drawing.Color.Black;
		this.txtEmail.Location = new System.Drawing.Point(132, 7);
		this.txtEmail.MaxLength = 256;
		this.txtEmail.Name = "txtEmail";
		this.txtEmail.Size = new System.Drawing.Size(237, 21);
		this.txtEmail.TabIndex = 1;
		this.lblEmail.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblEmail.Location = new System.Drawing.Point(13, 7);
		this.lblEmail.Name = "lblEmail";
		this.lblEmail.Size = new System.Drawing.Size(113, 21);
		this.lblEmail.TabIndex = 0;
		this.lblEmail.Text = "???";
		this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.pnlDomaciVyuka.Controls.Add(this.lnkZaslatUIDnaEmail);
		this.pnlDomaciVyuka.Controls.Add(this.txtUID);
		this.pnlDomaciVyuka.Controls.Add(this.lblUID);
		this.pnlDomaciVyuka.Controls.Add(this.chkDomaciVyuka);
		this.pnlDomaciVyuka.Controls.Add(this.picLineDomaciVyuka);
		this.pnlDomaciVyuka.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlDomaciVyuka.Location = new System.Drawing.Point(0, 426);
		this.pnlDomaciVyuka.Name = "pnlDomaciVyuka";
		this.pnlDomaciVyuka.Size = new System.Drawing.Size(394, 60);
		this.pnlDomaciVyuka.TabIndex = 6;
		this.lnkZaslatUIDnaEmail.AutoSize = true;
		this.lnkZaslatUIDnaEmail.Enabled = false;
		this.lnkZaslatUIDnaEmail.Location = new System.Drawing.Point(215, 32);
		this.lnkZaslatUIDnaEmail.Name = "lnkZaslatUIDnaEmail";
		this.lnkZaslatUIDnaEmail.Size = new System.Drawing.Size(28, 15);
		this.lnkZaslatUIDnaEmail.TabIndex = 3;
		this.lnkZaslatUIDnaEmail.TabStop = true;
		this.lnkZaslatUIDnaEmail.Text = "???";
		this.lnkZaslatUIDnaEmail.Visible = false;
		this.lnkZaslatUIDnaEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkZaslatUIDnaEmail_LinkClicked);
		this.txtUID.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtUID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtUID.ForeColor = System.Drawing.Color.Black;
		this.txtUID.Location = new System.Drawing.Point(132, 29);
		this.txtUID.MaxLength = 8;
		this.txtUID.Name = "txtUID";
		this.txtUID.ReadOnly = true;
		this.txtUID.Size = new System.Drawing.Size(76, 21);
		this.txtUID.TabIndex = 2;
		this.lblUID.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblUID.Location = new System.Drawing.Point(3, 29);
		this.lblUID.Name = "lblUID";
		this.lblUID.Size = new System.Drawing.Size(123, 21);
		this.lblUID.TabIndex = 1;
		this.lblUID.Text = "???";
		this.lblUID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkDomaciVyuka.AutoSize = true;
		this.chkDomaciVyuka.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.chkDomaciVyuka.Location = new System.Drawing.Point(24, 5);
		this.chkDomaciVyuka.Name = "chkDomaciVyuka";
		this.chkDomaciVyuka.Size = new System.Drawing.Size(44, 19);
		this.chkDomaciVyuka.TabIndex = 0;
		this.chkDomaciVyuka.Text = "???";
		this.chkDomaciVyuka.UseVisualStyleBackColor = true;
		this.chkDomaciVyuka.CheckedChanged += new System.EventHandler(chkDomaciVyuka_CheckedChanged);
		this.picLineDomaciVyuka.BackColor = System.Drawing.Color.Black;
		this.picLineDomaciVyuka.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLineDomaciVyuka.Location = new System.Drawing.Point(0, 59);
		this.picLineDomaciVyuka.Name = "picLineDomaciVyuka";
		this.picLineDomaciVyuka.Size = new System.Drawing.Size(394, 1);
		this.picLineDomaciVyuka.TabIndex = 49;
		this.picLineDomaciVyuka.TabStop = false;
		this.pnlPocatecniLekce.Controls.Add(this.chkPocatecniLekce);
		this.pnlPocatecniLekce.Controls.Add(this.lstPocatecniLekce);
		this.pnlPocatecniLekce.Controls.Add(this.picLinePocatecniLekce);
		this.pnlPocatecniLekce.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlPocatecniLekce.Location = new System.Drawing.Point(0, 363);
		this.pnlPocatecniLekce.Name = "pnlPocatecniLekce";
		this.pnlPocatecniLekce.Size = new System.Drawing.Size(394, 63);
		this.pnlPocatecniLekce.TabIndex = 5;
		this.chkPocatecniLekce.AutoSize = true;
		this.chkPocatecniLekce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.chkPocatecniLekce.Location = new System.Drawing.Point(24, 9);
		this.chkPocatecniLekce.Name = "chkPocatecniLekce";
		this.chkPocatecniLekce.Size = new System.Drawing.Size(44, 19);
		this.chkPocatecniLekce.TabIndex = 0;
		this.chkPocatecniLekce.Text = "???";
		this.chkPocatecniLekce.UseVisualStyleBackColor = true;
		this.chkPocatecniLekce.CheckedChanged += new System.EventHandler(chkPocatecniLekce_CheckedChanged);
		this.lstPocatecniLekce.BackColor = System.Drawing.Color.WhiteSmoke;
		this.lstPocatecniLekce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lstPocatecniLekce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lstPocatecniLekce.FormattingEnabled = true;
		this.lstPocatecniLekce.Location = new System.Drawing.Point(19, 30);
		this.lstPocatecniLekce.Name = "lstPocatecniLekce";
		this.lstPocatecniLekce.Size = new System.Drawing.Size(350, 23);
		this.lstPocatecniLekce.TabIndex = 1;
		this.picLinePocatecniLekce.BackColor = System.Drawing.Color.Black;
		this.picLinePocatecniLekce.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLinePocatecniLekce.Location = new System.Drawing.Point(0, 62);
		this.picLinePocatecniLekce.Name = "picLinePocatecniLekce";
		this.picLinePocatecniLekce.Size = new System.Drawing.Size(394, 1);
		this.picLinePocatecniLekce.TabIndex = 46;
		this.picLinePocatecniLekce.TabStop = false;
		this.pnlPismo.Controls.Add(this.picLinePismo);
		this.pnlPismo.Controls.Add(this.lblUkazka);
		this.pnlPismo.Controls.Add(this.lblVelikostPisma);
		this.pnlPismo.Controls.Add(this.lblBarvaPisma);
		this.pnlPismo.Controls.Add(this.lblBarvaPozadi);
		this.pnlPismo.Controls.Add(this.ccbBarvaPisma);
		this.pnlPismo.Controls.Add(this.grpPismo);
		this.pnlPismo.Controls.Add(this.ccbBarvaPozadi);
		this.pnlPismo.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlPismo.Location = new System.Drawing.Point(0, 236);
		this.pnlPismo.Name = "pnlPismo";
		this.pnlPismo.Size = new System.Drawing.Size(394, 127);
		this.pnlPismo.TabIndex = 4;
		this.picLinePismo.BackColor = System.Drawing.Color.Black;
		this.picLinePismo.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLinePismo.Location = new System.Drawing.Point(0, 126);
		this.picLinePismo.Name = "picLinePismo";
		this.picLinePismo.Size = new System.Drawing.Size(394, 1);
		this.picLinePismo.TabIndex = 67;
		this.picLinePismo.TabStop = false;
		this.pnlTrida.Controls.Add(this.lstTrida);
		this.pnlTrida.Controls.Add(this.lblTrida);
		this.pnlTrida.Controls.Add(this.picLineTrida);
		this.pnlTrida.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlTrida.Location = new System.Drawing.Point(0, 201);
		this.pnlTrida.Name = "pnlTrida";
		this.pnlTrida.Size = new System.Drawing.Size(394, 35);
		this.pnlTrida.TabIndex = 3;
		this.lstTrida.BackColor = System.Drawing.Color.WhiteSmoke;
		this.lstTrida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lstTrida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lstTrida.FormattingEnabled = true;
		this.lstTrida.Location = new System.Drawing.Point(132, 6);
		this.lstTrida.Name = "lstTrida";
		this.lstTrida.Size = new System.Drawing.Size(237, 23);
		this.lstTrida.TabIndex = 1;
		this.lblTrida.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblTrida.Location = new System.Drawing.Point(13, 7);
		this.lblTrida.Name = "lblTrida";
		this.lblTrida.Size = new System.Drawing.Size(113, 21);
		this.lblTrida.TabIndex = 0;
		this.lblTrida.Text = "???";
		this.lblTrida.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.picLineTrida.BackColor = System.Drawing.Color.Black;
		this.picLineTrida.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLineTrida.Location = new System.Drawing.Point(0, 34);
		this.picLineTrida.Name = "picLineTrida";
		this.picLineTrida.Size = new System.Drawing.Size(394, 1);
		this.picLineTrida.TabIndex = 43;
		this.picLineTrida.TabStop = false;
		this.pnlUzivJmeno.Controls.Add(this.txtUzivJmeno);
		this.pnlUzivJmeno.Controls.Add(this.lblUzivJmeno);
		this.pnlUzivJmeno.Controls.Add(this.picLineUzivJmeno);
		this.pnlUzivJmeno.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlUzivJmeno.Location = new System.Drawing.Point(0, 166);
		this.pnlUzivJmeno.Name = "pnlUzivJmeno";
		this.pnlUzivJmeno.Size = new System.Drawing.Size(394, 35);
		this.pnlUzivJmeno.TabIndex = 2;
		this.txtUzivJmeno.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtUzivJmeno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtUzivJmeno.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
		this.txtUzivJmeno.ForeColor = System.Drawing.Color.Black;
		this.txtUzivJmeno.Location = new System.Drawing.Point(132, 7);
		this.txtUzivJmeno.MaxLength = 32;
		this.txtUzivJmeno.Name = "txtUzivJmeno";
		this.txtUzivJmeno.Size = new System.Drawing.Size(237, 21);
		this.txtUzivJmeno.TabIndex = 1;
		this.txtUzivJmeno.Enter += new System.EventHandler(txtUzivJmeno_Enter);
		this.lblUzivJmeno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblUzivJmeno.Location = new System.Drawing.Point(13, 7);
		this.lblUzivJmeno.Name = "lblUzivJmeno";
		this.lblUzivJmeno.Size = new System.Drawing.Size(113, 21);
		this.lblUzivJmeno.TabIndex = 0;
		this.lblUzivJmeno.Text = "???";
		this.lblUzivJmeno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.picLineUzivJmeno.BackColor = System.Drawing.Color.Black;
		this.picLineUzivJmeno.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLineUzivJmeno.Location = new System.Drawing.Point(0, 34);
		this.picLineUzivJmeno.Name = "picLineUzivJmeno";
		this.picLineUzivJmeno.Size = new System.Drawing.Size(394, 1);
		this.picLineUzivJmeno.TabIndex = 41;
		this.picLineUzivJmeno.TabStop = false;
		this.pnlTituly.Controls.Add(this.txtTitulyZa);
		this.pnlTituly.Controls.Add(this.txtTitulyPred);
		this.pnlTituly.Controls.Add(this.lblTitulyZa);
		this.pnlTituly.Controls.Add(this.lblTitulyPred);
		this.pnlTituly.Controls.Add(this.picLineTituly);
		this.pnlTituly.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlTituly.Location = new System.Drawing.Point(0, 101);
		this.pnlTituly.Name = "pnlTituly";
		this.pnlTituly.Size = new System.Drawing.Size(394, 65);
		this.pnlTituly.TabIndex = 1;
		this.txtTitulyZa.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtTitulyZa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtTitulyZa.ForeColor = System.Drawing.Color.Black;
		this.txtTitulyZa.Location = new System.Drawing.Point(132, 35);
		this.txtTitulyZa.MaxLength = 32;
		this.txtTitulyZa.Name = "txtTitulyZa";
		this.txtTitulyZa.Size = new System.Drawing.Size(131, 21);
		this.txtTitulyZa.TabIndex = 3;
		this.txtTitulyPred.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtTitulyPred.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtTitulyPred.ForeColor = System.Drawing.Color.Black;
		this.txtTitulyPred.Location = new System.Drawing.Point(132, 8);
		this.txtTitulyPred.MaxLength = 32;
		this.txtTitulyPred.Name = "txtTitulyPred";
		this.txtTitulyPred.Size = new System.Drawing.Size(131, 21);
		this.txtTitulyPred.TabIndex = 1;
		this.lblTitulyZa.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblTitulyZa.Location = new System.Drawing.Point(3, 35);
		this.lblTitulyZa.Name = "lblTitulyZa";
		this.lblTitulyZa.Size = new System.Drawing.Size(123, 21);
		this.lblTitulyZa.TabIndex = 2;
		this.lblTitulyZa.Text = "???";
		this.lblTitulyZa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblTitulyPred.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblTitulyPred.Location = new System.Drawing.Point(3, 8);
		this.lblTitulyPred.Name = "lblTitulyPred";
		this.lblTitulyPred.Size = new System.Drawing.Size(123, 21);
		this.lblTitulyPred.TabIndex = 0;
		this.lblTitulyPred.Text = "???";
		this.lblTitulyPred.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.picLineTituly.BackColor = System.Drawing.Color.Black;
		this.picLineTituly.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.picLineTituly.Location = new System.Drawing.Point(0, 64);
		this.picLineTituly.Name = "picLineTituly";
		this.picLineTituly.Size = new System.Drawing.Size(394, 1);
		this.picLineTituly.TabIndex = 43;
		this.picLineTituly.TabStop = false;
		this.pnlJmeno.Controls.Add(this.txtPrijmeni);
		this.pnlJmeno.Controls.Add(this.grpPohlavi);
		this.pnlJmeno.Controls.Add(this.lblPohlavi);
		this.pnlJmeno.Controls.Add(this.txtJmeno);
		this.pnlJmeno.Controls.Add(this.lblPrijmeni);
		this.pnlJmeno.Controls.Add(this.lblJmeno);
		this.pnlJmeno.Controls.Add(this.picLineJmeno);
		this.pnlJmeno.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlJmeno.Location = new System.Drawing.Point(0, 0);
		this.pnlJmeno.Name = "pnlJmeno";
		this.pnlJmeno.Size = new System.Drawing.Size(394, 101);
		this.pnlJmeno.TabIndex = 0;
		this.txtPrijmeni.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtPrijmeni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtPrijmeni.ForeColor = System.Drawing.Color.Black;
		this.txtPrijmeni.Location = new System.Drawing.Point(132, 36);
		this.txtPrijmeni.MaxLength = 32;
		this.txtPrijmeni.Name = "txtPrijmeni";
		this.txtPrijmeni.Size = new System.Drawing.Size(237, 21);
		this.txtPrijmeni.TabIndex = 3;
		this.grpPohlavi.Controls.Add(this.optZena);
		this.grpPohlavi.Controls.Add(this.optMuz);
		this.grpPohlavi.Location = new System.Drawing.Point(132, 58);
		this.grpPohlavi.Name = "grpPohlavi";
		this.grpPohlavi.Size = new System.Drawing.Size(132, 36);
		this.grpPohlavi.TabIndex = 5;
		this.grpPohlavi.TabStop = false;
		this.optZena.AutoSize = true;
		this.optZena.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.optZena.Location = new System.Drawing.Point(68, 11);
		this.optZena.Name = "optZena";
		this.optZena.Size = new System.Drawing.Size(45, 19);
		this.optZena.TabIndex = 1;
		this.optZena.TabStop = true;
		this.optZena.Text = "???";
		this.optZena.UseVisualStyleBackColor = true;
		this.optMuz.AutoSize = true;
		this.optMuz.Checked = true;
		this.optMuz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.optMuz.Location = new System.Drawing.Point(10, 11);
		this.optMuz.Name = "optMuz";
		this.optMuz.Size = new System.Drawing.Size(45, 19);
		this.optMuz.TabIndex = 0;
		this.optMuz.TabStop = true;
		this.optMuz.Text = "???";
		this.optMuz.UseVisualStyleBackColor = true;
		this.lblPohlavi.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblPohlavi.Location = new System.Drawing.Point(16, 66);
		this.lblPohlavi.Name = "lblPohlavi";
		this.lblPohlavi.Size = new System.Drawing.Size(110, 21);
		this.lblPohlavi.TabIndex = 4;
		this.lblPohlavi.Text = "???";
		this.lblPohlavi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtJmeno.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtJmeno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtJmeno.ForeColor = System.Drawing.Color.Black;
		this.txtJmeno.Location = new System.Drawing.Point(132, 9);
		this.txtJmeno.MaxLength = 32;
		this.txtJmeno.Name = "txtJmeno";
		this.txtJmeno.Size = new System.Drawing.Size(237, 21);
		this.txtJmeno.TabIndex = 1;
		this.lblPrijmeni.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblPrijmeni.Location = new System.Drawing.Point(13, 36);
		this.lblPrijmeni.Name = "lblPrijmeni";
		this.lblPrijmeni.Size = new System.Drawing.Size(113, 21);
		this.lblPrijmeni.TabIndex = 2;
		this.lblPrijmeni.Text = "???";
		this.lblPrijmeni.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblJmeno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblJmeno.Location = new System.Drawing.Point(13, 9);
		this.lblJmeno.Name = "lblJmeno";
		this.lblJmeno.Size = new System.Drawing.Size(113, 21);
		this.lblJmeno.TabIndex = 0;
		this.lblJmeno.Text = "???";
		this.lblJmeno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.ClientSize = new System.Drawing.Size(409, 866);
		base.Controls.Add(this.pnlHlavni);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		this.MinimumSize = new System.Drawing.Size(400, 150);
		base.Name = "Uzivatel";
		base.Opacity = 0.9990000128746033;
		this.Text = "???";
		base.VyskaZahlavi = 40;
		base.Resize += new System.EventHandler(NovyUzivatel_Resize);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(NastaveniUziv_KeyDown);
		base.Controls.SetChildIndex(this.pnlHlavni, 0);
		((System.ComponentModel.ISupportInitialize)this.picLineJmeno).EndInit();
		this.pnlZapati.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.picLineAvatar).EndInit();
		this.grpPismo.ResumeLayout(false);
		this.grpPismo.PerformLayout();
		this.pnlHlavni.ResumeLayout(false);
		this.pnlAkceZapati.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		this.pnlPresunNaTabor.ResumeLayout(false);
		this.pnlAktivovat.ResumeLayout(false);
		this.pnlHesloVynulovat.ResumeLayout(false);
		this.pnlHesloZrusit.ResumeLayout(false);
		this.pnlHesloZmenit.ResumeLayout(false);
		this.pnlAkce.ResumeLayout(false);
		this.pnlAvatar.ResumeLayout(false);
		this.pnlUIDzeSkoly.ResumeLayout(false);
		this.pnlUIDzeSkoly.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineUIDzeSkoly).EndInit();
		this.pnlEmail.ResumeLayout(false);
		this.pnlEmail.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineEmail).EndInit();
		this.pnlDomaciVyuka.ResumeLayout(false);
		this.pnlDomaciVyuka.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineDomaciVyuka).EndInit();
		this.pnlPocatecniLekce.ResumeLayout(false);
		this.pnlPocatecniLekce.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picLinePocatecniLekce).EndInit();
		this.pnlPismo.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.picLinePismo).EndInit();
		this.pnlTrida.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.picLineTrida).EndInit();
		this.pnlUzivJmeno.ResumeLayout(false);
		this.pnlUzivJmeno.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineUzivJmeno).EndInit();
		this.pnlTituly.ResumeLayout(false);
		this.pnlTituly.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picLineTituly).EndInit();
		this.pnlJmeno.ResumeLayout(false);
		this.pnlJmeno.PerformLayout();
		this.grpPohlavi.ResumeLayout(false);
		this.grpPohlavi.PerformLayout();
		base.ResumeLayout(false);
	}
}
