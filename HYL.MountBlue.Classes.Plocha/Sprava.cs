using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Classes.Uzivatele.Tridy;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class Sprava : _PlochaSprava
{
	private const string uciteleGroupID = "group_ucitele";

	private const string neznamaGroupID = "group_neznama";

	private const string tridaGroupID = "group_";

	private const string icoMuz = "muz";

	private const string icoZena = "zena";

	private ObrazkoveTlacitko cmdTridy;

	private ObrazkoveTlacitko cmdPridatUcitele;

	private ObrazkoveTlacitko cmdPridatStudenta;

	private ObrazkoveTlacitko cmdNastaveni;

	private ObrazkoveTlacitko cmdOdhlasit;

	private LinkLabel lnkUpravitUziv;

	private LinkLabel lnkOdebratUziv;

	private LinkLabel lnkObnovit;

	private ListView lvwUzivatele;

	private ImageList imlUzivatele;

	public Sprava(PAdmin admin)
		: base(admin)
	{
	}

	internal override bool FixniVyska()
	{
		return false;
	}

	internal override int VyskaTlacitek()
	{
		return 185;
	}

	protected override int TridaID()
	{
		return 0;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdTridy = new ObrazkoveTlacitko();
		cmdPridatUcitele = new ObrazkoveTlacitko();
		cmdPridatStudenta = new ObrazkoveTlacitko();
		cmdNastaveni = new ObrazkoveTlacitko();
		cmdOdhlasit = new ObrazkoveTlacitko();
		lnkUpravitUziv = new LinkLabel();
		lnkOdebratUziv = new LinkLabel();
		lnkObnovit = new LinkLabel();
		lvwUzivatele = new ListView();
		imlUzivatele = new ImageList();
		cmdTridy.Anchor = AnchorStyles.Top;
		cmdTridy.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 147);
		location.Offset(pntZacatek);
		cmdTridy.Location = location;
		cmdTridy.Name = "cmdTridy";
		cmdTridy.NormalniObrazek = GrafikaSkolni.pngTlTridyN;
		cmdTridy.Size = new Size(126, 43);
		cmdTridy.StisknutyObrazek = GrafikaSkolni.pngTlTridyD;
		cmdTridy.TabIndex = 2;
		cmdTridy.ZakazanyObrazek = GrafikaSkolni.pngTlTridyZ;
		cmdTridy.ZvyraznenyObrazek = GrafikaSkolni.pngTlTridyH;
		cmdTridy.TlacitkoStisknuto += cmdTridy_TlacitkoStisknuto;
		ttt.SetToolTip(cmdTridy, HYL.MountBlue.Resources.Texty.Sprava_cmdTridy_TTT);
		cmdPridatUcitele.Anchor = AnchorStyles.Top;
		cmdPridatUcitele.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 194);
		location.Offset(pntZacatek);
		cmdPridatUcitele.Location = location;
		cmdPridatUcitele.Name = "cmdPridatUcitele";
		cmdPridatUcitele.NormalniObrazek = GrafikaSkolni.pngTlPrUcitelN;
		cmdPridatUcitele.Size = new Size(126, 38);
		cmdPridatUcitele.StisknutyObrazek = GrafikaSkolni.pngTlPrUcitelD;
		cmdPridatUcitele.TabIndex = 3;
		cmdPridatUcitele.ZakazanyObrazek = GrafikaSkolni.pngTlPrUcitelZ;
		cmdPridatUcitele.ZvyraznenyObrazek = GrafikaSkolni.pngTlPrUcitelH;
		cmdPridatUcitele.TlacitkoStisknuto += cmdPridatUcitele_TlacitkoStisknuto;
		ttt.SetToolTip(cmdPridatUcitele, HYL.MountBlue.Resources.Texty.Sprava_cmdPridatUcitele_TTT);
		cmdPridatStudenta.Anchor = AnchorStyles.Top;
		cmdPridatStudenta.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 236);
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
		cmdNastaveni.Anchor = AnchorStyles.Top;
		cmdNastaveni.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 278);
		location.Offset(pntZacatek);
		cmdNastaveni.Location = location;
		cmdNastaveni.Name = "cmdNastaveni";
		cmdNastaveni.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniN;
		cmdNastaveni.Size = new Size(126, 25);
		cmdNastaveni.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniD;
		cmdNastaveni.TabIndex = 5;
		cmdNastaveni.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniZ;
		cmdNastaveni.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNastaveniH;
		cmdNastaveni.TlacitkoStisknuto += cmdNastaveni_TlacitkoStisknuto;
		ttt.SetToolTip(cmdNastaveni, HYL.MountBlue.Resources.Texty.Plocha_cmdNastaveni_TTT);
		cmdOdhlasit.Anchor = AnchorStyles.Top;
		cmdOdhlasit.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 307);
		location.Offset(pntZacatek);
		cmdOdhlasit.Location = location;
		cmdOdhlasit.Name = "cmdOdhlasit";
		cmdOdhlasit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOdhlasitN;
		cmdOdhlasit.Size = new Size(126, 25);
		cmdOdhlasit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOdhlasitD;
		cmdOdhlasit.TabIndex = 7;
		cmdOdhlasit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOdhlasitZ;
		cmdOdhlasit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOdhlasitH;
		cmdOdhlasit.TlacitkoStisknuto += cmdOdhlasit_TlacitkoStisknuto;
		ttt.SetToolTip(cmdOdhlasit, HYL.MountBlue.Resources.Texty.Plocha_cmdOdhlasitSe_TTT);
		lvwUzivatele.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
		lvwUzivatele.BackColor = Barva.PozadiTextBoxu;
		location = new Point(44, 180);
		location.Offset(pntZacatek);
		lvwUzivatele.Location = location;
		lvwUzivatele.Name = "lvwUzivatele";
		lvwUzivatele.BorderStyle = BorderStyle.Fixed3D;
		lvwUzivatele.MultiSelect = false;
		lvwUzivatele.View = View.Details;
		lvwUzivatele.LabelEdit = false;
		lvwUzivatele.Font = new Font("Arial", 9f, FontStyle.Regular);
		lvwUzivatele.LabelWrap = false;
		lvwUzivatele.HideSelection = false;
		lvwUzivatele.HeaderStyle = ColumnHeaderStyle.Nonclickable;
		lvwUzivatele.FullRowSelect = true;
		lvwUzivatele.ShowGroups = true;
		lvwUzivatele.ForeColor = Barva.TextObecny;
		lvwUzivatele.Columns.Add(HYL.MountBlue.Resources.Texty.Sprava_clhJmeno, 180, HorizontalAlignment.Left);
		lvwUzivatele.Columns.Add(HYL.MountBlue.Resources.Texty.Sprava_clhUzivJmeno, 135, HorizontalAlignment.Left);
		lvwUzivatele.Columns.Add(HYL.MountBlue.Resources.Texty.Sprava_clhDatovySoubor, 135, HorizontalAlignment.Left);
		lvwUzivatele.Columns.Add(HYL.MountBlue.Resources.Texty.Sprava_clhPoznamka, 290, HorizontalAlignment.Left);
		lvwUzivatele.Size = new Size(775, _Plocha.HlavniOkno.DisplayRectangle.Height - 295);
		lvwUzivatele.TabIndex = 0;
		lvwUzivatele.SmallImageList = imlUzivatele;
		lvwUzivatele.KeyDown += lvwUzivatele_KeyDown;
		lvwUzivatele.DoubleClick += lvwUzivatele_DoubleClick;
		lvwUzivatele.SelectedIndexChanged += lvwUzivatele_SelectedIndexChanged;
		imlUzivatele.ImageSize = new Size(20, 20);
		imlUzivatele.ColorDepth = ColorDepth.Depth24Bit;
		imlUzivatele.Images.Add("muz", GrafikaSkolni.pngMuzIco);
		imlUzivatele.Images.Add("zena", GrafikaSkolni.pngZenaIco);
		lnkUpravitUziv.Anchor = AnchorStyles.Bottom;
		lnkUpravitUziv.BackColor = Color.White;
		location = new Point(50, _Plocha.HlavniOkno.DisplayRectangle.Height - 110);
		location.Offset(pntZacatek);
		lnkUpravitUziv.Location = location;
		lnkUpravitUziv.Name = "lnkUpravitUziv";
		lnkUpravitUziv.Size = new Size(113, 20);
		lnkUpravitUziv.TabIndex = 9;
		lnkUpravitUziv.TabStop = true;
		lnkUpravitUziv.Text = HYL.MountBlue.Resources.Texty.Sprava_lnkUpravitUziv;
		lnkUpravitUziv.TextAlign = ContentAlignment.MiddleLeft;
		lnkUpravitUziv.LinkClicked += lnkUpravitUziv_LinkClicked;
		ttt.SetToolTip(lnkUpravitUziv, HYL.MountBlue.Resources.Texty.Sprava_lnkUpravitUziv_TTT);
		lnkOdebratUziv.Anchor = AnchorStyles.Bottom;
		lnkOdebratUziv.BackColor = Color.White;
		location = new Point(170, _Plocha.HlavniOkno.DisplayRectangle.Height - 110);
		location.Offset(pntZacatek);
		lnkOdebratUziv.Location = location;
		lnkOdebratUziv.Name = "lnkOdebratUziv";
		lnkOdebratUziv.Size = new Size(165, 20);
		lnkOdebratUziv.TabIndex = 10;
		lnkOdebratUziv.TabStop = true;
		lnkOdebratUziv.Text = HYL.MountBlue.Resources.Texty.Sprava_lnkOdebratUziv;
		lnkOdebratUziv.TextAlign = ContentAlignment.MiddleLeft;
		lnkOdebratUziv.LinkClicked += lnkOdebratUziv_LinkClicked;
		ttt.SetToolTip(lnkOdebratUziv, HYL.MountBlue.Resources.Texty.Sprava_lnkOdebratUziv_TTT);
		lnkObnovit.Anchor = AnchorStyles.Bottom;
		lnkObnovit.BackColor = Color.White;
		location = new Point(365, _Plocha.HlavniOkno.DisplayRectangle.Height - 110);
		location.Offset(pntZacatek);
		lnkObnovit.Location = location;
		lnkObnovit.Name = "lnkObnovit";
		lnkObnovit.Size = new Size(165, 20);
		lnkObnovit.TabIndex = 11;
		lnkObnovit.TabStop = true;
		lnkObnovit.Text = HYL.MountBlue.Resources.Texty.Sprava_lnkObnovit;
		lnkObnovit.TextAlign = ContentAlignment.MiddleLeft;
		lnkObnovit.LinkClicked += lnkObnovit_LinkClicked;
		ttt.SetToolTip(lnkObnovit, HYL.MountBlue.Resources.Texty.Sprava_lnkObnovit_TTT);
		NacistUzivatele(0u);
		UpravitOdebratObnovit();
	}

	private void lnkObnovit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		ObnovitSeznam();
	}

	private void lvwUzivatele_SelectedIndexChanged(object sender, EventArgs e)
	{
		UpravitOdebratObnovit();
	}

	private void lnkUpravitUziv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		UpravitUzivatele();
	}

	private void lnkOdebratUziv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		OdebratUzivatele();
	}

	private void lvwUzivatele_DoubleClick(object sender, EventArgs e)
	{
		UpravitUzivatele();
	}

	private void UpravitOdebratObnovit()
	{
		lnkUpravitUziv.Enabled = lvwUzivatele.SelectedItems.Count == 1;
		lnkOdebratUziv.Enabled = lnkUpravitUziv.Enabled;
	}

	private void lvwUzivatele_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			UpravitUzivatele();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	internal override void ZacatekPlochy()
	{
		base.ZacatekPlochy();
		lvwUzivatele.Focus();
	}

	internal override void DeinicializovatPrvky()
	{
		base.DeinicializovatPrvky();
		imlUzivatele.Dispose();
	}

	private void NacistUzivatele(uint uiZvyraznitUzivateleUID)
	{
		lvwUzivatele.BeginUpdate();
		lvwUzivatele.Items.Clear();
		lvwUzivatele.Groups.Clear();
		lvwUzivatele.Groups.Add(new ListViewGroup("group_ucitele", HYL.MountBlue.Resources.Texty.Sprava_groupUcitele));
		HYL.MountBlue.Classes.Uzivatele.Tridy.Trida[] array = HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniTridy.SerazenySeznamTrid();
		foreach (HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida in array)
		{
			lvwUzivatele.Groups.Add(new ListViewGroup("group_" + trida.TridaID, string.Format(HYL.MountBlue.Resources.Texty.Sprava_groupTrida, trida.ToString(), trida.UcitelTridyToString())));
		}
		lvwUzivatele.Groups.Add(new ListViewGroup("group_neznama", HYL.MountBlue.Resources.Texty.Sprava_groupNeznamaTrida));
		HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel[] array2 = HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele.SerazenySeznamUzivatelu();
		foreach (HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel in array2)
		{
			ListViewItem listViewItem = PridatPolozku(uzivatel);
			if (uzivatel.UID == uiZvyraznitUzivateleUID)
			{
				listViewItem.Selected = true;
			}
		}
		if (lvwUzivatele.SelectedItems.Count == 1)
		{
			lvwUzivatele.SelectedItems[0].EnsureVisible();
		}
		lvwUzivatele.EndUpdate();
	}

	private void NacistPolozku(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uziv, ref ListViewItem lvi)
	{
		lvi.Text = uziv.CeleJmeno;
		if (lvi.SubItems.Count <= 1)
		{
			lvi.SubItems.Add(string.Empty);
			lvi.SubItems.Add(string.Empty);
			lvi.SubItems.Add(string.Empty);
		}
		lvi.SubItems[1].Text = uziv.UzivJmeno;
		lvi.SubItems[2].Text = Path.GetFileName(uziv.SouborUzivatele);
		StringBuilder stringBuilder = new StringBuilder();
		if (!uziv.Aktivni)
		{
			stringBuilder.Append(HYL.MountBlue.Resources.Texty.Sprava_uzivNeaktivni);
		}
		if (uziv.JeUzivatelPrihlaseny)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(HYL.MountBlue.Resources.Texty.Sprava_uzivCarka);
			}
			stringBuilder.Append(HYL.MountBlue.Resources.Texty.Sprava_uzivPrihlasen);
		}
		if (uziv is Ucitel)
		{
			Ucitel ucitel = (Ucitel)uziv;
			StringBuilder stringBuilder2 = new StringBuilder();
			HYL.MountBlue.Classes.Uzivatele.Tridy.Trida[] array = ucitel.SerazenySeznamTrid();
			foreach (HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida in array)
			{
				if (stringBuilder2.Length > 0)
				{
					stringBuilder2.Append(HYL.MountBlue.Resources.Texty.Sprava_uzivCarka);
				}
				stringBuilder2.Append(trida.ToString());
			}
			if (stringBuilder2.Length > 0)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(HYL.MountBlue.Resources.Texty.Sprava_uzivCarka);
				}
				stringBuilder.Append(string.Format(HYL.MountBlue.Resources.Texty.Sprava_uzivUci, stringBuilder2.ToString()));
			}
		}
		lvi.SubItems[3].Text = stringBuilder.ToString();
		if (uziv.Pohlavi == HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel.ePohlavi.muz)
		{
			lvi.ImageKey = "muz";
		}
		else
		{
			lvi.ImageKey = "zena";
		}
		if (uziv.JeUzivatelPrihlaseny)
		{
			lvi.Font = new Font(lvi.Font, FontStyle.Bold);
		}
		else
		{
			lvi.Font = new Font(lvi.Font, FontStyle.Regular);
		}
		ListViewGroup listViewGroup = null;
		try
		{
			if (uziv is Ucitel)
			{
				listViewGroup = lvwUzivatele.Groups["group_ucitele"];
			}
			else if (uziv is StudentSkolni)
			{
				listViewGroup = lvwUzivatele.Groups["group_" + ((StudentSkolni)uziv).TridaID];
			}
			if (listViewGroup == null)
			{
				listViewGroup = lvwUzivatele.Groups["group_neznama"];
			}
		}
		catch
		{
		}
		lvi.Tag = uziv.UID;
		lvi.Group = listViewGroup;
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdTridy);
		deleg(cmdPridatUcitele);
		deleg(cmdPridatStudenta);
		deleg(cmdNastaveni);
		deleg(cmdOdhlasit);
		deleg(lvwUzivatele);
		deleg(lnkUpravitUziv);
		deleg(lnkOdebratUziv);
		deleg(lnkObnovit);
	}

	private void cmdNastaveni_TlacitkoStisknuto()
	{
		if (PrihlasenyUzivatel.ZobrazitUpravitUzivatele())
		{
			_Plocha.AktualniPlocha.ObnovitGrafiku();
		}
	}

	private void cmdOdhlasit_TlacitkoStisknuto()
	{
		PrihlasenyUzivatel.OdhlasitUzivatele(bZobrazitPrihlaseni: true);
	}

	private void cmdTisk_TlacitkoStisknuto()
	{
	}

	private void cmdPridatStudenta_TlacitkoStisknuto()
	{
		if (PUzivatele.PridatStudenta(out var uid, 0))
		{
			PridatPolozku(HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele[uid]);
		}
	}

	private void cmdPridatUcitele_TlacitkoStisknuto()
	{
		if (PUzivatele.PridatUcitele(out var uid))
		{
			PridatPolozku(HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele[uid]);
		}
	}

	private ListViewItem PridatPolozku(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uziv)
	{
		ListViewItem lvi = new ListViewItem();
		NacistPolozku(uziv, ref lvi);
		lvwUzivatele.Items.Add(lvi);
		return lvi;
	}

	private void cmdTridy_TlacitkoStisknuto()
	{
		uint uiZvyraznitUzivateleUID = 0u;
		if (lvwUzivatele.SelectedItems.Count == 1)
		{
			uiZvyraznitUzivateleUID = (uint)lvwUzivatele.SelectedItems[0].Tag;
		}
		if (HYL.MountBlue.Dialogs.Tridy.ZobrazitTridy(_Plocha.HlavniOkno))
		{
			NacistUzivatele(uiZvyraznitUzivateleUID);
		}
	}

	private void UpravitUzivatele()
	{
		if (lvwUzivatele.SelectedItems.Count == 1)
		{
			ListViewItem lvi = lvwUzivatele.SelectedItems[0];
			uint uid = (uint)lvi.Tag;
			HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel = HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele[uid];
			PUzivatel pUzivatel = PUzivatele.UzivatelPodleTypu(uzivatel);
			pUzivatel.ZobrazitUpravitUzivatele();
			NacistPolozku(uzivatel, ref lvi);
		}
	}

	private void OdebratUzivatele()
	{
		if (lvwUzivatele.SelectedItems.Count != 1)
		{
			return;
		}
		uint uid = (uint)lvwUzivatele.SelectedItems[0].Tag;
		HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel = HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele[uid];
		if (MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Sprava_msgOpravduOdebrat, uzivatel.CeleJmeno), HYL.MountBlue.Resources.Texty.Sprava_msgOpravduOdebrat_Title, eMsgBoxTlacitka.AnoNe) == DialogResult.Yes)
		{
			if (uzivatel.JeUzivatelPrihlaseny)
			{
				MsgBoxMB.ZobrazitMessageBox(HYL.MountBlue.Resources.Texty.Sprava_msgNelzeOdebrat, HYL.MountBlue.Resources.Texty.Sprava_msgNelzeOdebrat_Title, eMsgBoxTlacitka.OK);
			}
			else if (HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele.OdebratUzivatele(uid))
			{
				lvwUzivatele.Items.Remove(lvwUzivatele.SelectedItems[0]);
			}
		}
	}

	private void ObnovitSeznam()
	{
		uint uiZvyraznitUzivateleUID = 0u;
		if (lvwUzivatele.SelectedItems.Count == 1)
		{
			uiZvyraznitUzivateleUID = (uint)lvwUzivatele.SelectedItems[0].Tag;
		}
		NacistUzivatele(uiZvyraznitUzivateleUID);
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.T && cmdTridy.Enabled)
		{
			cmdTridy_TlacitkoStisknuto();
			return true;
		}
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.U && cmdPridatUcitele.Enabled)
		{
			cmdPridatUcitele_TlacitkoStisknuto();
			return true;
		}
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S && cmdPridatStudenta.Enabled)
		{
			cmdPridatStudenta_TlacitkoStisknuto();
			return true;
		}
		if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N && cmdNastaveni.Enabled)
		{
			cmdNastaveni_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.F2)
		{
			UpravitUzivatele();
			return true;
		}
		if (e.Control && e.KeyCode == Keys.Delete)
		{
			OdebratUzivatele();
			return true;
		}
		if (e.KeyCode == Keys.Escape)
		{
			cmdOdhlasit_TlacitkoStisknuto();
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
