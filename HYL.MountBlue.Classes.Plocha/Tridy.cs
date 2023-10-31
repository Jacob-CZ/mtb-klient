using System;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Classes.Uzivatele.Tridy;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class Tridy : _PlochaSprava
{
	private const string icoTrida = "trida";

	private ObrazkoveTlacitko cmdOtevritTridu;

	private ObrazkoveTlacitko cmdNastaveni;

	private ObrazkoveTlacitko cmdSezeni;

	private ObrazkoveTlacitko cmdOdhlasit;

	private ListView lvwTridy;

	private ImageList imlTridy;

	private PUcitel PrihlasenyUcitel => (PUcitel)PrihlasenyUzivatel;

	public Tridy(PUcitel ucitel)
		: base(ucitel)
	{
	}

	internal override bool FixniVyska()
	{
		return false;
	}

	internal override int VyskaTlacitek()
	{
		return 148;
	}

	protected override int TridaID()
	{
		return 0;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdOtevritTridu = new ObrazkoveTlacitko();
		cmdNastaveni = new ObrazkoveTlacitko();
		cmdSezeni = new ObrazkoveTlacitko();
		cmdOdhlasit = new ObrazkoveTlacitko();
		lvwTridy = new ListView();
		imlTridy = new ImageList();
		cmdOtevritTridu.Anchor = AnchorStyles.Top;
		cmdOtevritTridu.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 147);
		location.Offset(pntZacatek);
		cmdOtevritTridu.Location = location;
		cmdOtevritTridu.Name = "cmdOtevritTridu";
		cmdOtevritTridu.NormalniObrazek = GrafikaSkolni.pngTlOtevritTriduN;
		cmdOtevritTridu.Size = new Size(126, 43);
		cmdOtevritTridu.StisknutyObrazek = GrafikaSkolni.pngTlOtevritTriduD;
		cmdOtevritTridu.TabIndex = 2;
		cmdOtevritTridu.ZakazanyObrazek = GrafikaSkolni.pngTlOtevritTriduZ;
		cmdOtevritTridu.ZvyraznenyObrazek = GrafikaSkolni.pngTlOtevritTriduH;
		cmdOtevritTridu.TlacitkoStisknuto += cmdOtevritTridu_TlacitkoStisknuto;
		ttt.SetToolTip(cmdOtevritTridu, HYL.MountBlue.Resources.Texty.Sprava_cmdOtevritTridu_TTT);
		cmdNastaveni.Anchor = AnchorStyles.Top;
		cmdNastaveni.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 194);
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
		cmdSezeni.Anchor = AnchorStyles.Top;
		cmdSezeni.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 223);
		location.Offset(pntZacatek);
		cmdSezeni.Location = location;
		cmdSezeni.Name = "cmdSezeni";
		cmdSezeni.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlSezeniN;
		cmdSezeni.Size = new Size(126, 43);
		cmdSezeni.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlSezeniD;
		cmdSezeni.TabIndex = 6;
		cmdSezeni.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlSezeniZ;
		cmdSezeni.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlSezeniH;
		cmdSezeni.TlacitkoStisknuto += cmdSezeni_TlacitkoStisknuto;
		ttt.SetToolTip(cmdSezeni, HYL.MountBlue.Resources.Texty.Plocha_cmdSezeni_TTT);
		cmdOdhlasit.Anchor = AnchorStyles.Top;
		cmdOdhlasit.BackColor = Barva.PozadiTlacitekNaPlose;
		location = new Point(854, 270);
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
		lvwTridy.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
		lvwTridy.BackColor = Color.White;
		lvwTridy.BorderStyle = BorderStyle.None;
		location = new Point(44, 180);
		location.Offset(pntZacatek);
		lvwTridy.Location = location;
		lvwTridy.Name = "lvwTridy";
		lvwTridy.MultiSelect = false;
		lvwTridy.View = View.LargeIcon;
		lvwTridy.LabelEdit = false;
		lvwTridy.Font = new Font("Arial", 11f, FontStyle.Bold);
		lvwTridy.LabelWrap = false;
		lvwTridy.HideSelection = false;
		lvwTridy.ShowGroups = false;
		lvwTridy.ForeColor = Barva.TextObecny;
		lvwTridy.Size = new Size(775, _Plocha.HlavniOkno.DisplayRectangle.Height - 245);
		lvwTridy.TabIndex = 0;
		lvwTridy.LargeImageList = imlTridy;
		lvwTridy.KeyDown += lvwTridy_KeyDown;
		lvwTridy.DoubleClick += lvwTridy_DoubleClick;
		lvwTridy.SelectedIndexChanged += lvwTridy_SelectedIndexChanged;
		imlTridy.ImageSize = new Size(135, 85);
		imlTridy.ColorDepth = ColorDepth.Depth24Bit;
		imlTridy.Images.Add("trida", GrafikaSkolni.pngTrida);
		NacistTridy(PrihlasenyUcitel.Ucitel.PosledniVybranaTridaID);
		OtevritTriduObnovit();
	}

	private void NacistTridy(int zvyraznitTriduID)
	{
		lvwTridy.Items.Clear();
		HYL.MountBlue.Classes.Uzivatele.Tridy.Trida[] array = PrihlasenyUcitel.Ucitel.SerazenySeznamTrid();
		HYL.MountBlue.Classes.Uzivatele.Tridy.Trida[] array2 = array;
		foreach (HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida in array2)
		{
			ListViewItem listViewItem = new ListViewItem();
			listViewItem.Text = trida.ToString();
			listViewItem.ImageKey = "trida";
			listViewItem.Tag = trida.TridaID;
			lvwTridy.Items.Add(listViewItem);
			if (trida.TridaID == zvyraznitTriduID)
			{
				listViewItem.Selected = true;
			}
		}
		if (lvwTridy.SelectedItems.Count == 1)
		{
			lvwTridy.SelectedItems[0].EnsureVisible();
		}
	}

	internal override void ZacatekPlochy()
	{
		base.ZacatekPlochy();
		lvwTridy.Focus();
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdOtevritTridu);
		deleg(cmdNastaveni);
		deleg(cmdSezeni);
		deleg(cmdOdhlasit);
		deleg(lvwTridy);
	}

	internal override void DeinicializovatPrvky()
	{
		base.DeinicializovatPrvky();
		imlTridy.Dispose();
	}

	private void lvwTridy_SelectedIndexChanged(object sender, EventArgs e)
	{
		OtevritTriduObnovit();
	}

	private void OtevritTriduObnovit()
	{
		cmdOtevritTridu.Enabled = lvwTridy.SelectedItems.Count == 1;
	}

	private void lvwTridy_DoubleClick(object sender, EventArgs e)
	{
		cmdOtevritTridu_TlacitkoStisknuto();
	}

	private void lvwTridy_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmdOtevritTridu_TlacitkoStisknuto();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void cmdOtevritTridu_TlacitkoStisknuto()
	{
		if (lvwTridy.SelectedItems.Count == 1)
		{
			int tridaID = (int)lvwTridy.SelectedItems[0].Tag;
			PrihlasenyUcitel.OtevritTridu(tridaID, ZalozkyTrida.eZalozky.SeznamStudentu);
		}
	}

	private void cmdSezeni_TlacitkoStisknuto()
	{
		PrihlasenyUcitel.ZobrazitSpravneSezeni();
	}

	internal override bool StisknutaKlavesa(KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.O && cmdOtevritTridu.Enabled)
		{
			cmdOtevritTridu_TlacitkoStisknuto();
			return true;
		}
		if (e.Control && e.KeyCode == Keys.N && cmdNastaveni.Enabled)
		{
			cmdNastaveni_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.F12)
		{
			cmdSezeni_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.Escape)
		{
			cmdOdhlasit_TlacitkoStisknuto();
			return true;
		}
		return base.StisknutaKlavesa(e);
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
}
