using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Texty;
using HYL.MountBlue.Classes.Uzivatele.Historie;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Plocha;

internal class Historie : _PlochaSprava
{
	private const string groupKlas = "klas";

	private const string groupKlasTrenink = "klasTrenink";

	private ZalozkyHistorie.eZalozky zalozka;

	private uint uiStudentUID;

	private int iTridaID;

	private StudentSkolni HistorieStudenta;

	private ObrazkoveTlacitko cmdNavratDomu;

	private ZalozkyHistorie zalHistorie;

	private ListView lvwHistorie;

	private TextBoxMB tmbZadani;

	private TextBoxMB tmbOpis;

	private Label lblDatum;

	private Label lblZpusob;

	private Label lblDobaPsaniL;

	private Label lblDobaPsani;

	private Label lblZnamkaL;

	private Label lblZnamka;

	private Label lblHrubeUhozyL;

	private Label lblHrubeUhozy;

	private Label lblCisteUhozyL;

	private Label lblCisteUhozy;

	private Label lblChybovostL;

	private Label lblChybovost;

	private Label lblPocetChybL;

	private Label lblPocetChyb;

	private Label lblPoznamka;

	private VetaKresleni vetaZadani;

	private VetaKresleni vetaOpis;

	private Zaznam[] zaznamy;

	private static int PocetStiskuF6;

	private PUcitel PrihlasenyUcitel => (PUcitel)PrihlasenyUzivatel;

	public Historie(PUcitel puc, int tridaID, uint studentUID, ZalozkyHistorie.eZalozky zalozka)
		: base(puc)
	{
		this.zalozka = zalozka;
		uiStudentUID = studentUID;
		iTridaID = tridaID;
		HistorieStudenta = (StudentSkolni)HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniUzivatele[studentUID];
	}

	protected override void Vykreslit(Graphics G)
	{
		base.Vykreslit(G);
		Brush brush = new SolidBrush(Barva.ObdelnikZaTextBoxem);
		Rectangle rObdelnik = new Rectangle(227, 220, 582, 200);
		rObdelnik.Inflate(8, 8);
		GraphicsPath path = SpecialniGrafika.CestaZaoblenehoObdelniku(rObdelnik, 4);
		G.FillPath(brush, path);
		rObdelnik = new Rectangle(227, 500, 582, _Plocha.HlavniOkno.DisplayRectangle.Height - 560);
		rObdelnik.Inflate(8, 8);
		path = SpecialniGrafika.CestaZaoblenehoObdelniku(rObdelnik, 4);
		G.FillPath(brush, path);
		rObdelnik = new Rectangle(218, 154, 448, 8);
		path = SpecialniGrafika.CestaZaoblenehoObdelniku(rObdelnik, 3);
		G.FillPath(brush, path);
		_Plocha.VykreslitText(G, HistorieStudenta.CeleJmeno, Barva.HistorieJmenoStudenta, 24, FontStyle.Bold, new Rectangle(226, 124, 438, 45), StringAlignment.Far, StringAlignment.Near);
	}

	internal override bool FixniVyska()
	{
		return false;
	}

	internal override int VyskaTlacitek()
	{
		return 42;
	}

	protected override int TridaID()
	{
		return iTridaID;
	}

	internal override void InicializovatPrvky(Point pntZacatek)
	{
		base.InicializovatPrvky(pntZacatek);
		cmdNavratDomu = new ObrazkoveTlacitko();
		zalHistorie = new ZalozkyHistorie();
		lvwHistorie = new ListView();
		tmbOpis = new TextBoxMB();
		tmbZadani = new TextBoxMB();
		lblDatum = new Label();
		lblZpusob = new Label();
		lblDobaPsaniL = new Label();
		lblDobaPsani = new Label();
		lblZnamkaL = new Label();
		lblZnamka = new Label();
		lblHrubeUhozyL = new Label();
		lblHrubeUhozy = new Label();
		lblCisteUhozyL = new Label();
		lblCisteUhozy = new Label();
		lblChybovostL = new Label();
		lblChybovost = new Label();
		lblPocetChybL = new Label();
		lblPocetChyb = new Label();
		lblPoznamka = new Label();
		cmdNavratDomu.Anchor = AnchorStyles.Top;
		cmdNavratDomu.BackColor = Barva.PozadiTlacitekNaPlose;
		Point location = new Point(854, 147);
		location.Offset(pntZacatek);
		cmdNavratDomu.Location = location;
		cmdNavratDomu.Name = "cmdNavratDomu";
		cmdNavratDomu.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuN;
		cmdNavratDomu.Size = new Size(126, 43);
		cmdNavratDomu.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuD;
		cmdNavratDomu.TabIndex = 7;
		cmdNavratDomu.Visible = true;
		cmdNavratDomu.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuZ;
		cmdNavratDomu.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlDomuH;
		cmdNavratDomu.TlacitkoStisknuto += cmdNavratDomu_TlacitkoStisknuto;
		ttt.SetToolTip(cmdNavratDomu, HYL.MountBlue.Resources.Texty.Cviceni_cmdNavratDomu_TTT);
		zalHistorie.Anchor = AnchorStyles.Top;
		zalHistorie.BackColor = Color.White;
		location = new Point(29, 128);
		location.Offset(pntZacatek);
		zalHistorie.Location = location;
		zalHistorie.Size = new Size(176, 26);
		zalHistorie.ZmenaZalozky += zalHistorie_ZmenaZalozky;
		lvwHistorie.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
		lvwHistorie.BackColor = Barva.PozadiTextBoxu;
		location = new Point(29, 154);
		location.Offset(pntZacatek);
		lvwHistorie.Location = location;
		lvwHistorie.Name = "lvwHistorie";
		lvwHistorie.BorderStyle = BorderStyle.Fixed3D;
		lvwHistorie.MultiSelect = false;
		lvwHistorie.Columns.Add(string.Empty, 180, HorizontalAlignment.Left);
		lvwHistorie.FullRowSelect = true;
		lvwHistorie.HeaderStyle = ColumnHeaderStyle.None;
		lvwHistorie.View = View.Details;
		lvwHistorie.LabelEdit = false;
		lvwHistorie.Font = new Font("Arial", 9f, FontStyle.Regular);
		lvwHistorie.LabelWrap = false;
		lvwHistorie.HideSelection = false;
		lvwHistorie.ShowGroups = true;
		lvwHistorie.ForeColor = Barva.TextObecny;
		lvwHistorie.Size = new Size(180, _Plocha.HlavniOkno.DisplayRectangle.Height - 205);
		lvwHistorie.TabIndex = 0;
		lvwHistorie.SelectedIndexChanged += lvwHistorie_SelectedIndexChanged;
		tmbZadani.Anchor = AnchorStyles.Top;
		tmbZadani.BackColor = PrihlasenyUcitel.Uzivatel.BarvaPozadi;
		location = new Point(227, 220);
		location.Offset(pntZacatek);
		tmbZadani.Location = location;
		tmbZadani.Name = "tmbZadani";
		tmbZadani.BorderStyle = BorderStyle.Fixed3D;
		tmbZadani.Size = new Size(582, 200);
		tmbZadani.TabIndex = 2;
		tmbOpis.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
		tmbOpis.BackColor = PrihlasenyUcitel.Uzivatel.BarvaPozadi;
		location = new Point(227, 500);
		location.Offset(pntZacatek);
		tmbOpis.Location = location;
		tmbOpis.Name = "tmbOpis";
		tmbOpis.BorderStyle = BorderStyle.Fixed3D;
		tmbOpis.Size = new Size(582, _Plocha.HlavniOkno.DisplayRectangle.Height - 560);
		tmbOpis.TabIndex = 1;
		lblDatum.Anchor = AnchorStyles.Top;
		lblDatum.AutoSize = false;
		lblDatum.BackColor = Color.White;
		lblDatum.Font = new Font("Arial", 12f, FontStyle.Bold);
		lblDatum.ForeColor = Barva.TextObecny;
		location = new Point(440, 165);
		location.Offset(pntZacatek);
		lblDatum.Location = location;
		lblDatum.Name = "lblDatum";
		lblDatum.Size = new Size(226, 20);
		lblDatum.Text = string.Empty;
		lblDatum.TextAlign = ContentAlignment.MiddleRight;
		lblZpusob.Anchor = AnchorStyles.Top;
		lblZpusob.AutoSize = false;
		lblZpusob.BackColor = Color.White;
		lblZpusob.Font = new Font("Arial", 10f, FontStyle.Bold);
		lblZpusob.ForeColor = Barva.TextObecny;
		location = new Point(227, 194);
		location.Offset(pntZacatek);
		lblZpusob.Location = location;
		lblZpusob.Name = "lblZpusob";
		lblZpusob.Size = new Size(440, 16);
		lblZpusob.Text = string.Empty;
		lblZpusob.TextAlign = ContentAlignment.MiddleLeft;
		lblDobaPsaniL.Anchor = AnchorStyles.Top;
		lblDobaPsaniL.AutoSize = false;
		lblDobaPsaniL.BackColor = Color.White;
		lblDobaPsaniL.Font = new Font("Arial", 9f, FontStyle.Regular);
		lblDobaPsaniL.ForeColor = Barva.TextObecny;
		location = new Point(227, 430);
		location.Offset(pntZacatek);
		lblDobaPsaniL.Location = location;
		lblDobaPsaniL.Name = "lblDobaPsaniL";
		lblDobaPsaniL.Size = new Size(120, 17);
		lblDobaPsaniL.Text = HYL.MountBlue.Resources.Texty.Historie_lblDobaPsani;
		lblDobaPsaniL.TextAlign = ContentAlignment.MiddleRight;
		lblDobaPsani.Anchor = AnchorStyles.Top;
		lblDobaPsani.AutoSize = false;
		lblDobaPsani.BackColor = Color.White;
		lblDobaPsani.Font = new Font("Arial", 10f, FontStyle.Bold);
		lblDobaPsani.ForeColor = Barva.TextObecny;
		location = new Point(357, 430);
		location.Offset(pntZacatek);
		lblDobaPsani.Location = location;
		lblDobaPsani.Name = "lblDobaPsani";
		lblDobaPsani.Size = new Size(50, 17);
		lblDobaPsani.Text = string.Empty;
		lblDobaPsani.TextAlign = ContentAlignment.MiddleLeft;
		lblZnamkaL.Anchor = AnchorStyles.Top;
		lblZnamkaL.AutoSize = false;
		lblZnamkaL.BackColor = Color.White;
		lblZnamkaL.Font = new Font("Arial", 9f, FontStyle.Regular);
		lblZnamkaL.ForeColor = Barva.TextObecny;
		location = new Point(227, 450);
		location.Offset(pntZacatek);
		lblZnamkaL.Location = location;
		lblZnamkaL.Name = "lblZnamkaL";
		lblZnamkaL.Size = new Size(120, 17);
		lblZnamkaL.Text = HYL.MountBlue.Resources.Texty.Historie_lblZnamka;
		lblZnamkaL.TextAlign = ContentAlignment.MiddleRight;
		lblZnamka.Anchor = AnchorStyles.Top;
		lblZnamka.AutoSize = false;
		lblZnamka.BackColor = Color.White;
		lblZnamka.Font = new Font("Arial", 10f, FontStyle.Bold);
		lblZnamka.ForeColor = Barva.TextObecny;
		location = new Point(357, 450);
		location.Offset(pntZacatek);
		lblZnamka.Location = location;
		lblZnamka.Name = "lblZnamka";
		lblZnamka.Size = new Size(50, 17);
		lblZnamka.Text = string.Empty;
		lblZnamka.TextAlign = ContentAlignment.MiddleLeft;
		lblHrubeUhozyL.Anchor = AnchorStyles.Top;
		lblHrubeUhozyL.AutoSize = false;
		lblHrubeUhozyL.BackColor = Color.White;
		lblHrubeUhozyL.Font = new Font("Arial", 9f, FontStyle.Regular);
		lblHrubeUhozyL.ForeColor = Barva.TextObecny;
		location = new Point(417, 430);
		location.Offset(pntZacatek);
		lblHrubeUhozyL.Location = location;
		lblHrubeUhozyL.Name = "lblHrubeUhozyL";
		lblHrubeUhozyL.Size = new Size(110, 17);
		lblHrubeUhozyL.Text = HYL.MountBlue.Resources.Texty.Historie_lblHrubeUhozy;
		lblHrubeUhozyL.TextAlign = ContentAlignment.MiddleRight;
		lblHrubeUhozy.Anchor = AnchorStyles.Top;
		lblHrubeUhozy.AutoSize = false;
		lblHrubeUhozy.BackColor = Color.White;
		lblHrubeUhozy.Font = new Font("Arial", 10f, FontStyle.Bold);
		lblHrubeUhozy.ForeColor = Barva.TextObecny;
		location = new Point(537, 430);
		location.Offset(pntZacatek);
		lblHrubeUhozy.Location = location;
		lblHrubeUhozy.Name = "lblHrubeUhozy";
		lblHrubeUhozy.Size = new Size(50, 17);
		lblHrubeUhozy.Text = string.Empty;
		lblHrubeUhozy.TextAlign = ContentAlignment.MiddleRight;
		lblCisteUhozyL.Anchor = AnchorStyles.Top;
		lblCisteUhozyL.AutoSize = false;
		lblCisteUhozyL.BackColor = Color.White;
		lblCisteUhozyL.Font = new Font("Arial", 9f, FontStyle.Regular);
		lblCisteUhozyL.ForeColor = Barva.TextObecny;
		location = new Point(417, 450);
		location.Offset(pntZacatek);
		lblCisteUhozyL.Location = location;
		lblCisteUhozyL.Name = "lblCisteUhozyL";
		lblCisteUhozyL.Size = new Size(110, 17);
		lblCisteUhozyL.Text = HYL.MountBlue.Resources.Texty.Historie_lblCisteUhozy;
		lblCisteUhozyL.TextAlign = ContentAlignment.MiddleRight;
		lblCisteUhozy.Anchor = AnchorStyles.Top;
		lblCisteUhozy.AutoSize = false;
		lblCisteUhozy.BackColor = Color.White;
		lblCisteUhozy.Font = new Font("Arial", 10f, FontStyle.Bold);
		lblCisteUhozy.ForeColor = Barva.TextObecny;
		location = new Point(537, 450);
		location.Offset(pntZacatek);
		lblCisteUhozy.Location = location;
		lblCisteUhozy.Name = "lblCisteUhozy";
		lblCisteUhozy.Size = new Size(50, 17);
		lblCisteUhozy.Text = string.Empty;
		lblCisteUhozy.TextAlign = ContentAlignment.MiddleRight;
		lblChybovostL.Anchor = AnchorStyles.Top;
		lblChybovostL.AutoSize = false;
		lblChybovostL.BackColor = Color.White;
		lblChybovostL.Font = new Font("Arial", 9f, FontStyle.Regular);
		lblChybovostL.ForeColor = Barva.TextObecny;
		location = new Point(590, 430);
		location.Offset(pntZacatek);
		lblChybovostL.Location = location;
		lblChybovostL.Name = "lblChybovostL";
		lblChybovostL.Size = new Size(110, 17);
		lblChybovostL.Text = HYL.MountBlue.Resources.Texty.Historie_lblChybovost;
		lblChybovostL.TextAlign = ContentAlignment.MiddleRight;
		lblChybovost.Anchor = AnchorStyles.Top;
		lblChybovost.AutoSize = false;
		lblChybovost.BackColor = Color.White;
		lblChybovost.Font = new Font("Arial", 10f, FontStyle.Bold);
		lblChybovost.ForeColor = Barva.TextObecny;
		location = new Point(705, 430);
		location.Offset(pntZacatek);
		lblChybovost.Location = location;
		lblChybovost.Name = "lblChybovost";
		lblChybovost.Size = new Size(80, 17);
		lblChybovost.Text = "100,00 %";
		lblChybovost.TextAlign = ContentAlignment.MiddleRight;
		lblPocetChybL.Anchor = AnchorStyles.Top;
		lblPocetChybL.AutoSize = false;
		lblPocetChybL.BackColor = Color.White;
		lblPocetChybL.Font = new Font("Arial", 9f, FontStyle.Regular);
		lblPocetChybL.ForeColor = Barva.TextObecny;
		location = new Point(590, 450);
		location.Offset(pntZacatek);
		lblPocetChybL.Location = location;
		lblPocetChybL.Name = "lblPocetChybL";
		lblPocetChybL.Size = new Size(110, 17);
		lblPocetChybL.Text = HYL.MountBlue.Resources.Texty.Historie_lblPocetChyb;
		lblPocetChybL.TextAlign = ContentAlignment.MiddleRight;
		lblPocetChyb.Anchor = AnchorStyles.Top;
		lblPocetChyb.AutoSize = false;
		lblPocetChyb.BackColor = Color.White;
		lblPocetChyb.Font = new Font("Arial", 10f, FontStyle.Bold);
		lblPocetChyb.ForeColor = Barva.TextObecny;
		location = new Point(705, 450);
		location.Offset(pntZacatek);
		lblPocetChyb.Location = location;
		lblPocetChyb.Name = "lblPocetChyb";
		lblPocetChyb.Size = new Size(80, 17);
		lblPocetChyb.Text = string.Empty;
		lblPocetChyb.TextAlign = ContentAlignment.MiddleRight;
		lblPoznamka.Anchor = AnchorStyles.Top;
		lblPoznamka.AutoSize = false;
		lblPoznamka.BackColor = Color.White;
		lblPoznamka.Font = new Font("Arial", 10f, FontStyle.Bold);
		lblPoznamka.ForeColor = Barva.TextObecny;
		location = new Point(227, 470);
		location.Offset(pntZacatek);
		lblPoznamka.Location = location;
		lblPoznamka.Name = "lblPoznamka";
		lblPoznamka.Size = new Size(558, 17);
		lblPoznamka.Text = string.Empty;
		lblPoznamka.TextAlign = ContentAlignment.MiddleCenter;
	}

	private void Obnovit()
	{
		ObnovitHistorii();
		zalHistorie_ZmenaZalozky(zalHistorie.AktivniZalozka);
	}

	private void zalHistorie_ZmenaZalozky(ZalozkyHistorie.eZalozky novaZalozka)
	{
		switch (novaZalozka)
		{
		case ZalozkyHistorie.eZalozky.Historie:
			NacistHistorii();
			break;
		case ZalozkyHistorie.eZalozky.Klasifikace:
			NacistKlasifikaci();
			break;
		}
	}

	private void ObnovitHistorii()
	{
		zaznamy = HistorieStudenta.Historie.NacistHistoriiUzivatele();
	}

	private void NacistHistorii()
	{
		lvwHistorie.BeginUpdate();
		lvwHistorie.Items.Clear();
		lvwHistorie.Groups.Clear();
		ListViewGroup listViewGroup = null;
		int num = -1;
		for (int i = 0; i < zaznamy.Length; i++)
		{
			Zaznam zaznam = zaznamy[i];
			if ((!(zaznam is ZaznCviceni) || zaznam is ZaznKlasifCvic) && !(zaznam is ZaznPrenosPostupu))
			{
				continue;
			}
			int num2 = num;
			if (zaznam is ZaznCviceni)
			{
				num2 = ((ZaznCviceni)zaznam).OznaceniCviceni.Lekce;
			}
			else if (zaznam is ZaznPrenosPostupu)
			{
				num2 = ((ZaznPrenosPostupu)zaznam).PostupZ.Lekce;
			}
			if (num2 != num || listViewGroup == null)
			{
				num = num2;
				listViewGroup = new ListViewGroup();
				listViewGroup.Header = string.Format(HYL.MountBlue.Resources.Texty.Historie_taborX, num);
				lvwHistorie.Groups.Add(listViewGroup);
			}
			if (listViewGroup != null)
			{
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = Text.DatumToString(zaznam.DatumAcas, zobrazitCas: true, HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniDatumAcas);
				if (zaznam is ZaznPrenosPostupu)
				{
					listViewItem.BackColor = Color.LightGray;
				}
				listViewItem.Group = listViewGroup;
				listViewItem.Tag = i;
				lvwHistorie.Items.Add(listViewItem);
			}
		}
		if (lvwHistorie.Items.Count > 0)
		{
			int index = lvwHistorie.Items.Count - 1;
			ListViewItem listViewItem2 = lvwHistorie.Items[index];
			listViewItem2.Selected = true;
			listViewItem2.Focused = true;
			listViewItem2.EnsureVisible();
		}
		else
		{
			ZobrazitDetail();
		}
		lvwHistorie.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
		lvwHistorie.EndUpdate();
		lvwHistorie.Focus();
	}

	private void NacistKlasifikaci()
	{
		lvwHistorie.BeginUpdate();
		lvwHistorie.Items.Clear();
		lvwHistorie.Groups.Clear();
		foreach (HYL.MountBlue.Classes.Cviceni.Psani item in _Lekce.Lekce().Klasifikace)
		{
			ListViewGroup group = new ListViewGroup("klas" + item.ID, string.Format(HYL.MountBlue.Resources.Texty.MojeZnamky_klasifikacePoXtabore, item.KlasifikaceOd - 1));
			lvwHistorie.Groups.Add(group);
		}
		ListViewGroup group2 = new ListViewGroup("klasTrenink", HYL.MountBlue.Resources.Texty.MojeZnamky_klasifikaceVTreninku);
		lvwHistorie.Groups.Add(group2);
		for (int i = 0; i < zaznamy.Length; i++)
		{
			Zaznam zaznam = zaznamy[i];
			if (zaznam is ZaznKlasifCvic)
			{
				ZaznKlasifCvic zaznKlasifCvic = (ZaznKlasifCvic)zaznam;
				ListViewItem listViewItem = new ListViewItem();
				listViewItem.Text = Text.DatumToString(zaznam.DatumAcas, zobrazitCas: true, HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniDatumAcas);
				listViewItem.Tag = i;
				if (zaznKlasifCvic.OznaceniCviceni.JeTrenink)
				{
					listViewItem.Group = lvwHistorie.Groups["klasTrenink"];
				}
				else
				{
					listViewItem.Group = lvwHistorie.Groups["klas" + zaznKlasifCvic.ID];
				}
				lvwHistorie.Items.Add(listViewItem);
			}
		}
		if (lvwHistorie.Items.Count > 0)
		{
			int index = lvwHistorie.Items.Count - 1;
			ListViewItem listViewItem2 = lvwHistorie.Items[index];
			listViewItem2.Selected = true;
			listViewItem2.Focused = true;
			listViewItem2.EnsureVisible();
		}
		else
		{
			ZobrazitDetail();
		}
		lvwHistorie.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
		lvwHistorie.EndUpdate();
		lvwHistorie.Focus();
	}

	private void ZobrazitDetail()
	{
		VynulovatPohled();
		if (lvwHistorie.SelectedItems.Count != 1 || lvwHistorie.SelectedItems[0].Tag == null)
		{
			return;
		}
		int num = (int)lvwHistorie.SelectedItems[0].Tag;
		if (num < 0)
		{
			return;
		}
		Zaznam zaznam = zaznamy[num];
		lblDatum.Text = Text.DatumToString(zaznam.DatumAcas, zobrazitCas: true, HYL.MountBlue.Classes.Klient.Klient.Stanice.AktualniDatumAcas);
		if (zaznam is ZaznPrenosPostupu)
		{
			ZaznPrenosPostupu zaznPrenosPostupu = (ZaznPrenosPostupu)zaznam;
			if (zaznPrenosPostupu.PostupZ.Lekce == zaznPrenosPostupu.PostupNa.Lekce)
			{
				lblPoznamka.Text = HYL.MountBlue.Resources.Texty.Historie_postupVlekci;
			}
			else
			{
				lblPoznamka.Text = string.Format(HYL.MountBlue.Resources.Texty.Historie_postupNalekci, zaznPrenosPostupu.PostupZ.Lekce, zaznPrenosPostupu.PostupNa.Lekce);
			}
			lblPoznamka.Visible = true;
			return;
		}
		if (zaznam is ZaznKlasifCvic)
		{
			ZaznKlasifCvic zaznKlasifCvic = (ZaznKlasifCvic)zaznam;
			if (zaznKlasifCvic.ZnamkaZadana)
			{
				lblZnamka.Text = zaznKlasifCvic.Znamka.ToString();
			}
			if (zaznKlasifCvic.VyuzitaOdmena)
			{
				lblPoznamka.Text = HYL.MountBlue.Resources.Texty.Historie_vyuzitaOdmena;
			}
		}
		if (zaznam is ZaznCviceni)
		{
			ZaznCviceni zaznCviceni = (ZaznCviceni)zaznam;
			lblZpusob.Text = zaznCviceni.PopisZadani;
			lblDobaPsani.Text = string.Format(HYL.MountBlue.Resources.Texty.Historie_dobaPsani, zaznCviceni.DobaPsani.Minutes, zaznCviceni.DobaPsani.Seconds);
			lblHrubeUhozy.Text = zaznCviceni.HrubeUhozy.ToString();
			if (zaznCviceni.CisteUhozyZadany)
			{
				lblCisteUhozy.Text = zaznCviceni.CisteUhozy.ToString();
			}
			if (zaznCviceni.Chybovost < 10f)
			{
				lblChybovost.Text = zaznCviceni.Chybovost.ToString("0.00") + " %";
			}
			else
			{
				lblChybovost.Text = zaznCviceni.Chybovost.ToString("0") + " %";
			}
			if (zaznCviceni.VracenZpet)
			{
				lblPoznamka.Text = HYL.MountBlue.Resources.Texty.Historie_vracenZpet;
			}
			else if (zaznCviceni.ZiskalOdmenu)
			{
				lblPoznamka.Text = HYL.MountBlue.Resources.Texty.Historie_ziskalOdmenu;
			}
			lblPocetChyb.Text = zaznCviceni.PocetChyb.ToString();
			if (vetaZadani != null)
			{
				vetaZadani.CleanUp();
			}
			vetaZadani = new VetaKresleni(zaznCviceni.TextZadani, tmbZadani, Barva.PodtrzeniChybyZadani, ZpristupnitPosuvnik: true, Pismo.PismoTextu(PrihlasenyUcitel.Uzivatel.VelikostPisma), PrihlasenyUcitel.Uzivatel.BarvaPisma, PrihlasenyUcitel.Uzivatel.BarvaPozadi, null, OpisZpravaDoleva: false);
			vetaZadani.SpustitRuntime();
			if (vetaOpis != null)
			{
				vetaOpis.CleanUp();
			}
			vetaOpis = new VetaKresleni(zaznCviceni.TextOpis, tmbOpis, Barva.PodtrzeniChybyOpis, ZpristupnitPosuvnik: true, Pismo.PismoTextu(PrihlasenyUcitel.Uzivatel.VelikostPisma), PrihlasenyUcitel.Uzivatel.BarvaPisma, PrihlasenyUcitel.Uzivatel.BarvaPozadi, null, OpisZpravaDoleva: false);
			vetaOpis.SpustitRuntime();
			Vyhodnoceni vyhodnoceni = new Vyhodnoceni(vetaZadani, vetaOpis, !zaznCviceni.OznaceniCviceni.JeTrenink);
			vyhodnoceni.Vyhodnot();
			tmbZadani.Invalidate();
			tmbOpis.Invalidate();
		}
	}

	private void VynulovatPohled()
	{
		lblDatum.Text = string.Empty;
		lblZpusob.Text = string.Empty;
		lblDobaPsani.Text = string.Empty;
		lblZnamka.Text = string.Empty;
		lblHrubeUhozy.Text = string.Empty;
		lblCisteUhozy.Text = string.Empty;
		lblChybovost.Text = string.Empty;
		lblPocetChyb.Text = string.Empty;
		lblPoznamka.Text = string.Empty;
		if (vetaZadani != null)
		{
			vetaZadani.CleanUp();
		}
		if (vetaOpis != null)
		{
			vetaOpis.CleanUp();
		}
		tmbZadani.Invalidate();
		tmbOpis.Invalidate();
	}

	internal override void ZacatekPlochy()
	{
		base.ZacatekPlochy();
		ObnovitHistorii();
		zalHistorie.AktivniZalozka = zalozka;
	}

	private void lvwHistorie_SelectedIndexChanged(object sender, EventArgs e)
	{
		ZobrazitDetail();
	}

	protected override void ZpracovatPrvky(ZpracovatPrvkyDelegate deleg)
	{
		base.ZpracovatPrvky(deleg);
		deleg(cmdNavratDomu);
		deleg(zalHistorie);
		deleg(lvwHistorie);
		deleg(tmbZadani);
		deleg(tmbOpis);
		deleg(lblDatum);
		deleg(lblZpusob);
		deleg(lblDobaPsaniL);
		deleg(lblDobaPsani);
		deleg(lblZnamkaL);
		deleg(lblZnamka);
		deleg(lblHrubeUhozyL);
		deleg(lblHrubeUhozy);
		deleg(lblCisteUhozyL);
		deleg(lblCisteUhozy);
		deleg(lblChybovostL);
		deleg(lblChybovost);
		deleg(lblPocetChybL);
		deleg(lblPocetChyb);
		deleg(lblPoznamka);
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
		if (e.KeyCode == Keys.Escape)
		{
			cmdNavratDomu_TlacitkoStisknuto();
			return true;
		}
		if (e.KeyCode == Keys.F5)
		{
			Obnovit();
			return true;
		}
		if (e.Control && e.KeyCode == Keys.Tab)
		{
			zalHistorie.NastavitDalsiZalozku();
			return true;
		}
		return base.StisknutaKlavesa(e);
	}

	private void ZobrazitOknoScviceniID()
	{
		PocetStiskuF6 = 0;
		if (lvwHistorie.SelectedItems.Count != 1 || lvwHistorie.SelectedItems[0].Tag == null)
		{
			return;
		}
		int num = (int)lvwHistorie.SelectedItems[0].Tag;
		if (num >= 0)
		{
			Zaznam zaznam = zaznamy[num];
			if (zaznam is ZaznCviceni)
			{
				ZaznCviceni zaznCviceni = (ZaznCviceni)zaznam;
				string arg = _Cviceni.IDsLekci(zaznCviceni.OznaceniCviceni.Lekce, zaznCviceni.ID);
				MsgBoxBublina.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.msgboxCviceniID, arg), HYL.MountBlue.Resources.Texty.msgboxCviceniID_Title, eMsgBoxTlacitka.OK);
			}
		}
	}

	protected void cmdNavratDomu_TlacitkoStisknuto()
	{
		if (zalHistorie.AktivniZalozka == ZalozkyHistorie.eZalozky.Historie)
		{
			PrihlasenyUcitel.OtevritTridu(iTridaID, ZalozkyTrida.eZalozky.SeznamStudentu);
		}
		else
		{
			PrihlasenyUcitel.OtevritTridu(iTridaID, ZalozkyTrida.eZalozky.Klasifikace);
		}
	}
}
