using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Uzivatele.Historie;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class MojeZnamky : _Dialog
{
	private const string grpNesplnene = "nesplnene";

	private const string grpSplnene = "splnene";

	private const string grpNeaktivni = "neaktivni";

	private const string grpTrenink = "trenink";

	private const string icoVykricnik = "vykricnik";

	private const string icoOdmena = "odmena";

	private HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student student;

	private uint cviceniID;

	private uint nuceneCviceniID;

	private KlasifPolozka[] polozky;

	private IContainer components;

	private PictureBox picVelkaOdmena;

	private Label lblOdmenyVelke;

	private ListView lvwKlasifikace;

	private ObrazkoveTlacitko cmdTlZavrit;

	private ColumnHeader clhKlasifikace;

	private ColumnHeader clhDatum;

	private ColumnHeader clhZnamka;

	private Label lblInfoObecne;

	private Label lblPocetMalychJednicek;

	private PictureBox picLine1;

	private PictureBox picLine2;

	private Label lblDetail;

	private Label lblInfo2;

	private Label lblInfo1;

	private ColumnHeader clhDatum2;

	private ColumnHeader clhZnamka2;

	private ColumnHeader clhHrubeUhozy;

	private ColumnHeader clhChyby;

	private ColumnHeader clhChybovost;

	private ListView lvwDetail;

	private ObrazkoveTlacitko cmdZacitKlas;

	private ImageList imlIkony;

	public MojeZnamky(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student stu)
	{
		InitializeComponent();
		Text = Texty.MojeZnamky_Title;
		lblInfoObecne.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_lblInfoObecne);
		lblPocetMalychJednicek.Text = Texty.MojeZnamky_lblPocetMalychJednicek;
		clhKlasifikace.Text = Texty.MojeZnamky_clhKlasifikace;
		clhDatum.Text = Texty.MojeZnamky_clhDatum;
		clhDatum2.Text = Texty.MojeZnamky_clhDatum;
		clhZnamka.Text = Texty.MojeZnamky_clhZnamka;
		clhZnamka2.Text = Texty.MojeZnamky_clhZnamka;
		clhHrubeUhozy.Text = Texty.MojeZnamky_clhHrube;
		clhChyby.Text = Texty.MojeZnamky_clhChyby;
		clhChybovost.Text = Texty.MojeZnamky_clhChybovost;
		imlIkony.Images.Add("vykricnik", Grafika.icoVykricnik);
		imlIkony.Images.Add("odmena", Grafika.icoOdmena);
		student = stu;
		cviceniID = 0u;
		nuceneCviceniID = 0u;
		lblOdmenyVelke.Text = stu.PocetVelkychOdmen.ToString();
		NacistKlasifikace();
	}

	private void MojeZnamky_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && lvwKlasifikace.SelectedItems.Count == 1 && cmdZacitKlas.Visible && cmdZacitKlas.Enabled)
		{
			cmdZacitKlas_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
		else if (e.KeyData == Keys.Escape)
		{
			cmdTlZavrit_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
	}

	private void cmdTlZavrit_TlacitkoStisknuto()
	{
		Close();
	}

	private void lvwKlasifikace_SelectedIndexChanged(object sender, EventArgs e)
	{
		lvwKlasifikace_SelectedIndexChanged();
	}

	private void lvwKlasifikace_SelectedIndexChanged()
	{
		if (lvwKlasifikace.SelectedItems.Count == 1)
		{
			ZobrazitDetaily(lvwKlasifikace.SelectedItems[0]);
		}
		else
		{
			ZobrazitDetaily(null);
		}
	}

	private void cmdZacitKlas_TlacitkoStisknuto()
	{
		if (cmdZacitKlas.Visible && cmdZacitKlas.Enabled && lvwKlasifikace.SelectedItems.Count == 1)
		{
			if (!student.KlasifikacePovolena)
			{
				MsgBoxBublina.ZobrazitMessageBox(Texty.MojeZnamky_msgUcitelMusiPovolit, Texty.MojeZnamky_msgUcitelMusiPovolit_Title, eMsgBoxTlacitka.OK);
			}
			if (student.KlasifikacePovolena)
			{
				int num = (int)lvwKlasifikace.SelectedItems[0].Tag;
				cviceniID = polozky[num].CviceniPsani.ID;
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}
	}

	internal static bool ZobrazitMojeZnamky(IWin32Window owner, HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student student, out uint CviceniID)
	{
		using MojeZnamky mojeZnamky = new MojeZnamky(student);
		bool result = mojeZnamky.ShowDialog(owner) == DialogResult.OK;
		CviceniID = mojeZnamky.cviceniID;
		return result;
	}

	private void ZobrazitDetaily(ListViewItem lvi)
	{
		if (lvi == null)
		{
			NastavitViditelnost(prvky1: false, prvky2: false);
			return;
		}
		lblDetail.Text = lvi.Text;
		int num = (int)lvi.Tag;
		KlasifPolozka klasifPolozka = polozky[num];
		switch (klasifPolozka.Stav)
		{
		case KlasifPolozka.eStav.neaktivni:
		{
			string text = HYL.MountBlue.Classes.Text.TextMuzZena(student.Pohlavi, Texty.MojeZnamky_info_nemuzePsatM, Texty.MojeZnamky_info_nemuzePsatZ);
			lblInfo1.Text = string.Format(HYL.MountBlue.Classes.Text.TextNBSP(text), klasifPolozka.CviceniPsani.KlasifikaceOd - 1);
			lblInfo2.Text = string.Empty;
			lvwDetail.Items.Clear();
			NastavitViditelnost(prvky1: true, prvky2: false);
			break;
		}
		case KlasifPolozka.eStav.nesplneno:
			lblInfo1.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_muzePsat);
			if (student.VyukaDokoncena || klasifPolozka.CviceniPsani.KlasifikaceDo >= 46)
			{
				lblInfo2.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_nezapomenout_2);
			}
			else
			{
				lblInfo2.Text = HYL.MountBlue.Classes.Text.TextNBSP(string.Format(Texty.MojeZnamky_info_nezapomenout_1, klasifPolozka.CviceniPsani.KlasifikaceDo + 1));
			}
			NastavitViditelnost(prvky1: true, prvky2: true);
			break;
		case KlasifPolozka.eStav.nesplnenoUrgentni:
			lblInfo1.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_muzePsat);
			lblInfo2.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_pozor);
			NastavitViditelnost(prvky1: true, prvky2: true);
			break;
		case KlasifPolozka.eStav.splneno:
			lblInfo1.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_klasifikaceSplnena);
			lblInfo2.Text = string.Empty;
			NastavitViditelnost(prvky1: true, prvky2: true);
			break;
		case KlasifPolozka.eStav.moznostVyuzitOdmeny:
			lblInfo1.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_muzePsat);
			if (student.VyukaDokoncena || klasifPolozka.CviceniPsani.KlasifikaceDo >= 46)
			{
				lblInfo2.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_muzeOpakovat_2);
			}
			else
			{
				lblInfo2.Text = string.Format(HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_muzeOpakovat_1), klasifPolozka.CviceniPsani.KlasifikaceDo + 1);
			}
			NastavitViditelnost(prvky1: true, prvky2: true);
			break;
		case KlasifPolozka.eStav.nuceneOpakovani:
			lblInfo1.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_muzePsat);
			lblInfo2.Text = HYL.MountBlue.Classes.Text.TextNBSP(Texty.MojeZnamky_info_musiOpakovat);
			NastavitViditelnost(prvky1: true, prvky2: true);
			break;
		}
		if (klasifPolozka.Stav == KlasifPolozka.eStav.splneno || klasifPolozka.Stav == KlasifPolozka.eStav.moznostVyuzitOdmeny || klasifPolozka.Stav == KlasifPolozka.eStav.nuceneOpakovani)
		{
			lvwDetail.Items.Clear();
			uint iD = klasifPolozka.CviceniPsani.ID;
			foreach (ZaznKlasifCvic item in student.Klasifikace)
			{
				if (item.ID == iD)
				{
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Text = HYL.MountBlue.Classes.Text.DatumToString(item.DatumAcas, zobrazitCas: true, Klient.Stanice.AktualniDatumAcas);
					listViewItem.SubItems.Add(item.Znamka.ToString());
					listViewItem.SubItems.Add(item.HrubeUhozy.ToString());
					listViewItem.SubItems.Add(item.PocetChyb.ToString());
					listViewItem.SubItems.Add(item.Chybovost.ToString("0.00") + " %");
					lvwDetail.Items.Add(listViewItem);
				}
			}
			int num2 = lvwDetail.Items.Count - 1;
			if (num2 >= 0)
			{
				lvwDetail.Items[num2].Font = new Font(lvwDetail.Items[num2].Font, FontStyle.Bold);
			}
		}
		else
		{
			lvwDetail.Items.Clear();
		}
		cmdZacitKlas.Enabled = (nuceneCviceniID == 0 || klasifPolozka.CviceniPsani.ID == nuceneCviceniID) && (klasifPolozka.Stav == KlasifPolozka.eStav.nesplneno || klasifPolozka.Stav == KlasifPolozka.eStav.moznostVyuzitOdmeny || klasifPolozka.Stav == KlasifPolozka.eStav.nesplnenoUrgentni || klasifPolozka.Stav == KlasifPolozka.eStav.nuceneOpakovani);
	}

	private void NacistKlasifikace()
	{
		nuceneCviceniID = 0u;
		lvwKlasifikace.Items.Clear();
		lvwKlasifikace.Groups.Clear();
		lvwKlasifikace.Groups.Add("nesplnene", Texty.MojeZnamky_grpNesplnene);
		lvwKlasifikace.Groups.Add("splnene", Texty.MojeZnamky_grpSplnene);
		lvwKlasifikace.Groups.Add("neaktivni", Texty.MojeZnamky_grpNeaktivni);
		polozky = _Lekce.Lekce().Klasifikace.SeznamKlasifikaci(student);
		for (int i = 0; i < polozky.Length; i++)
		{
			KlasifPolozka klasifPolozka = polozky[i];
			ListViewItem listViewItem = new ListViewItem(string.Format(Texty.MojeZnamky_klasifikacePoXtabore, klasifPolozka.CviceniPsani.KlasifikaceOd - 1));
			if (klasifPolozka.CviceniPsani.OznaceniCviceni.JeTrenink)
			{
				listViewItem.Group = lvwKlasifikace.Groups["trenink"];
			}
			else
			{
				switch (klasifPolozka.Stav)
				{
				case KlasifPolozka.eStav.neaktivni:
					listViewItem.ForeColor = Color.Gray;
					listViewItem.Group = lvwKlasifikace.Groups["neaktivni"];
					break;
				case KlasifPolozka.eStav.nesplneno:
					listViewItem.ForeColor = Color.Red;
					listViewItem.Group = lvwKlasifikace.Groups["nesplnene"];
					break;
				case KlasifPolozka.eStav.nesplnenoUrgentni:
					listViewItem.ForeColor = Color.Red;
					listViewItem.ImageKey = "vykricnik";
					listViewItem.Group = lvwKlasifikace.Groups["nesplnene"];
					break;
				case KlasifPolozka.eStav.splneno:
					listViewItem.ForeColor = Color.DarkGreen;
					listViewItem.Group = lvwKlasifikace.Groups["splnene"];
					break;
				case KlasifPolozka.eStav.moznostVyuzitOdmeny:
					listViewItem.ForeColor = Color.DarkGreen;
					listViewItem.ImageKey = "odmena";
					listViewItem.Group = lvwKlasifikace.Groups["splnene"];
					break;
				case KlasifPolozka.eStav.nuceneOpakovani:
					listViewItem.ForeColor = Color.Red;
					listViewItem.ImageKey = "vykricnik";
					listViewItem.Group = lvwKlasifikace.Groups["nesplnene"];
					break;
				}
			}
			if (klasifPolozka.Stav == KlasifPolozka.eStav.splneno || klasifPolozka.Stav == KlasifPolozka.eStav.moznostVyuzitOdmeny || klasifPolozka.Stav == KlasifPolozka.eStav.nuceneOpakovani)
			{
				listViewItem.SubItems.Add(HYL.MountBlue.Classes.Text.DatumToString(klasifPolozka.Datum, zobrazitCas: true, Klient.Stanice.AktualniDatumAcas));
				listViewItem.SubItems.Add(klasifPolozka.Znamka.ToString());
			}
			if (klasifPolozka.Stav == KlasifPolozka.eStav.nuceneOpakovani)
			{
				nuceneCviceniID = klasifPolozka.CviceniPsani.ID;
			}
			listViewItem.Tag = i;
			lvwKlasifikace.Items.Add(listViewItem);
		}
		if (lvwKlasifikace.Groups["nesplnene"].Items.Count > 0)
		{
			lvwKlasifikace.Groups["nesplnene"].Items[0].Selected = true;
		}
		else if (lvwKlasifikace.Groups["splnene"].Items.Count > 0)
		{
			lvwKlasifikace.Groups["splnene"].Items[0].Selected = true;
		}
		else if (lvwKlasifikace.Groups["neaktivni"].Items.Count > 0)
		{
			lvwKlasifikace.Groups["neaktivni"].Items[0].Selected = true;
		}
	}

	private void NastavitViditelnost(bool prvky1, bool prvky2)
	{
		lblDetail.Visible = prvky1;
		lblInfo1.Visible = prvky1;
		lblInfo2.Visible = prvky1;
		cmdZacitKlas.Visible = prvky2;
		lvwDetail.Visible = prvky2;
	}

	private void lvwKlasifikace_DoubleClick(object sender, EventArgs e)
	{
		cmdZacitKlas_TlacitkoStisknuto();
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
		this.components = new System.ComponentModel.Container();
		this.picVelkaOdmena = new System.Windows.Forms.PictureBox();
		this.lblOdmenyVelke = new System.Windows.Forms.Label();
		this.lvwKlasifikace = new System.Windows.Forms.ListView();
		this.clhKlasifikace = new System.Windows.Forms.ColumnHeader();
		this.clhDatum = new System.Windows.Forms.ColumnHeader();
		this.clhZnamka = new System.Windows.Forms.ColumnHeader();
		this.imlIkony = new System.Windows.Forms.ImageList(this.components);
		this.cmdTlZavrit = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblInfoObecne = new System.Windows.Forms.Label();
		this.lblPocetMalychJednicek = new System.Windows.Forms.Label();
		this.picLine1 = new System.Windows.Forms.PictureBox();
		this.picLine2 = new System.Windows.Forms.PictureBox();
		this.lblDetail = new System.Windows.Forms.Label();
		this.lblInfo2 = new System.Windows.Forms.Label();
		this.lblInfo1 = new System.Windows.Forms.Label();
		this.clhDatum2 = new System.Windows.Forms.ColumnHeader();
		this.clhZnamka2 = new System.Windows.Forms.ColumnHeader();
		this.clhHrubeUhozy = new System.Windows.Forms.ColumnHeader();
		this.clhChyby = new System.Windows.Forms.ColumnHeader();
		this.clhChybovost = new System.Windows.Forms.ColumnHeader();
		this.lvwDetail = new System.Windows.Forms.ListView();
		this.cmdZacitKlas = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		((System.ComponentModel.ISupportInitialize)this.picVelkaOdmena).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLine1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLine2).BeginInit();
		base.SuspendLayout();
		this.picVelkaOdmena.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.picVelkaOdmena.Image = HYL.MountBlue.Resources.Grafika.pngVelkaHvezda;
		this.picVelkaOdmena.Location = new System.Drawing.Point(591, 173);
		this.picVelkaOdmena.Name = "picVelkaOdmena";
		this.picVelkaOdmena.Size = new System.Drawing.Size(22, 21);
		this.picVelkaOdmena.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
		this.picVelkaOdmena.TabIndex = 22;
		this.picVelkaOdmena.TabStop = false;
		this.lblOdmenyVelke.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblOdmenyVelke.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblOdmenyVelke.Location = new System.Drawing.Point(618, 174);
		this.lblOdmenyVelke.Name = "lblOdmenyVelke";
		this.lblOdmenyVelke.Size = new System.Drawing.Size(39, 21);
		this.lblOdmenyVelke.TabIndex = 3;
		this.lblOdmenyVelke.Text = "???";
		this.lblOdmenyVelke.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lvwKlasifikace.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lvwKlasifikace.BackColor = System.Drawing.Color.WhiteSmoke;
		this.lvwKlasifikace.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lvwKlasifikace.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.clhKlasifikace, this.clhDatum, this.clhZnamka });
		this.lvwKlasifikace.Cursor = System.Windows.Forms.Cursors.Default;
		this.lvwKlasifikace.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lvwKlasifikace.ForeColor = System.Drawing.Color.Black;
		this.lvwKlasifikace.FullRowSelect = true;
		this.lvwKlasifikace.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.lvwKlasifikace.Location = new System.Drawing.Point(2, 46);
		this.lvwKlasifikace.MultiSelect = false;
		this.lvwKlasifikace.Name = "lvwKlasifikace";
		this.lvwKlasifikace.Size = new System.Drawing.Size(423, 465);
		this.lvwKlasifikace.SmallImageList = this.imlIkony;
		this.lvwKlasifikace.TabIndex = 0;
		this.lvwKlasifikace.UseCompatibleStateImageBehavior = false;
		this.lvwKlasifikace.View = System.Windows.Forms.View.Details;
		this.lvwKlasifikace.DoubleClick += new System.EventHandler(lvwKlasifikace_DoubleClick);
		this.lvwKlasifikace.SelectedIndexChanged += new System.EventHandler(lvwKlasifikace_SelectedIndexChanged);
		this.clhKlasifikace.Text = "???";
		this.clhKlasifikace.Width = 179;
		this.clhDatum.Text = "???";
		this.clhDatum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.clhDatum.Width = 145;
		this.clhZnamka.Text = "???";
		this.clhZnamka.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.clhZnamka.Width = 65;
		this.imlIkony.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imlIkony.ImageSize = new System.Drawing.Size(16, 16);
		this.imlIkony.TransparentColor = System.Drawing.Color.Transparent;
		this.cmdTlZavrit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdTlZavrit.BackColor = System.Drawing.Color.White;
		this.cmdTlZavrit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.cmdTlZavrit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdTlZavrit.ForeColor = System.Drawing.Color.Black;
		this.cmdTlZavrit.Location = new System.Drawing.Point(666, 518);
		this.cmdTlZavrit.Name = "cmdTlZavrit";
		this.cmdTlZavrit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdTlZavrit.Size = new System.Drawing.Size(63, 23);
		this.cmdTlZavrit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdTlZavrit.TabIndex = 9;
		this.cmdTlZavrit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdTlZavrit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdTlZavrit.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdTlZavrit_TlacitkoStisknuto);
		this.lblInfoObecne.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfoObecne.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfoObecne.Location = new System.Drawing.Point(435, 55);
		this.lblInfoObecne.Name = "lblInfoObecne";
		this.lblInfoObecne.Size = new System.Drawing.Size(294, 111);
		this.lblInfoObecne.TabIndex = 1;
		this.lblInfoObecne.Text = "???";
		this.lblPocetMalychJednicek.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblPocetMalychJednicek.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblPocetMalychJednicek.Location = new System.Drawing.Point(435, 173);
		this.lblPocetMalychJednicek.Name = "lblPocetMalychJednicek";
		this.lblPocetMalychJednicek.Size = new System.Drawing.Size(147, 21);
		this.lblPocetMalychJednicek.TabIndex = 2;
		this.lblPocetMalychJednicek.Text = "???";
		this.lblPocetMalychJednicek.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.picLine1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.picLine1.BackColor = System.Drawing.Color.Black;
		this.picLine1.Location = new System.Drawing.Point(430, 205);
		this.picLine1.Name = "picLine1";
		this.picLine1.Size = new System.Drawing.Size(306, 2);
		this.picLine1.TabIndex = 31;
		this.picLine1.TabStop = false;
		this.picLine2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.picLine2.BackColor = System.Drawing.Color.Black;
		this.picLine2.Location = new System.Drawing.Point(3, 511);
		this.picLine2.Name = "picLine2";
		this.picLine2.Size = new System.Drawing.Size(733, 1);
		this.picLine2.TabIndex = 32;
		this.picLine2.TabStop = false;
		this.lblDetail.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblDetail.BackColor = System.Drawing.Color.WhiteSmoke;
		this.lblDetail.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblDetail.Location = new System.Drawing.Point(430, 207);
		this.lblDetail.Name = "lblDetail";
		this.lblDetail.Size = new System.Drawing.Size(306, 25);
		this.lblDetail.TabIndex = 4;
		this.lblDetail.Text = "???";
		this.lblDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblInfo2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo2.Location = new System.Drawing.Point(435, 301);
		this.lblInfo2.Name = "lblInfo2";
		this.lblInfo2.Size = new System.Drawing.Size(294, 59);
		this.lblInfo2.TabIndex = 6;
		this.lblInfo2.Text = "???";
		this.lblInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblInfo1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo1.Location = new System.Drawing.Point(435, 234);
		this.lblInfo1.Name = "lblInfo1";
		this.lblInfo1.Size = new System.Drawing.Size(294, 63);
		this.lblInfo1.TabIndex = 5;
		this.lblInfo1.Text = "???";
		this.lblInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.clhDatum2.Text = "???";
		this.clhDatum2.Width = 155;
		this.clhZnamka2.Text = "???";
		this.clhZnamka2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.clhHrubeUhozy.Text = "???";
		this.clhHrubeUhozy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.clhHrubeUhozy.Width = 89;
		this.clhChyby.Text = "???";
		this.clhChyby.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.clhChyby.Width = 83;
		this.clhChybovost.Text = "???";
		this.clhChybovost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.clhChybovost.Width = 76;
		this.lvwDetail.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lvwDetail.BackColor = System.Drawing.Color.WhiteSmoke;
		this.lvwDetail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[5] { this.clhDatum2, this.clhZnamka2, this.clhHrubeUhozy, this.clhChyby, this.clhChybovost });
		this.lvwDetail.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lvwDetail.FullRowSelect = true;
		this.lvwDetail.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.lvwDetail.Location = new System.Drawing.Point(430, 408);
		this.lvwDetail.Name = "lvwDetail";
		this.lvwDetail.Size = new System.Drawing.Size(306, 99);
		this.lvwDetail.TabIndex = 8;
		this.lvwDetail.UseCompatibleStateImageBehavior = false;
		this.lvwDetail.View = System.Windows.Forms.View.Details;
		this.cmdZacitKlas.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdZacitKlas.BackColor = System.Drawing.Color.White;
		this.cmdZacitKlas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZacitKlas.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZacitKlas.ForeColor = System.Drawing.Color.White;
		this.cmdZacitKlas.Location = new System.Drawing.Point(521, 363);
		this.cmdZacitKlas.Name = "cmdZacitKlas";
		this.cmdZacitKlas.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZacitKlasN;
		this.cmdZacitKlas.Size = new System.Drawing.Size(126, 39);
		this.cmdZacitKlas.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZacitKlasD;
		this.cmdZacitKlas.TabIndex = 7;
		this.cmdZacitKlas.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZacitKlasZ;
		this.cmdZacitKlas.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZacitKlasH;
		this.cmdZacitKlas.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZacitKlas_TlacitkoStisknuto);
		base.ClientSize = new System.Drawing.Size(741, 550);
		base.Controls.Add(this.lblDetail);
		base.Controls.Add(this.lvwDetail);
		base.Controls.Add(this.lblInfo2);
		base.Controls.Add(this.lblInfo1);
		base.Controls.Add(this.picVelkaOdmena);
		base.Controls.Add(this.lblOdmenyVelke);
		base.Controls.Add(this.lblPocetMalychJednicek);
		base.Controls.Add(this.picLine2);
		base.Controls.Add(this.cmdZacitKlas);
		base.Controls.Add(this.picLine1);
		base.Controls.Add(this.cmdTlZavrit);
		base.Controls.Add(this.lvwKlasifikace);
		base.Controls.Add(this.lblInfoObecne);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "MojeZnamky";
		base.Opacity = 0.9990000128746033;
		this.Text = "???";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(MojeZnamky_KeyDown);
		base.Controls.SetChildIndex(this.lblInfoObecne, 0);
		base.Controls.SetChildIndex(this.lvwKlasifikace, 0);
		base.Controls.SetChildIndex(this.cmdTlZavrit, 0);
		base.Controls.SetChildIndex(this.picLine1, 0);
		base.Controls.SetChildIndex(this.cmdZacitKlas, 0);
		base.Controls.SetChildIndex(this.picLine2, 0);
		base.Controls.SetChildIndex(this.lblPocetMalychJednicek, 0);
		base.Controls.SetChildIndex(this.lblOdmenyVelke, 0);
		base.Controls.SetChildIndex(this.picVelkaOdmena, 0);
		base.Controls.SetChildIndex(this.lblInfo1, 0);
		base.Controls.SetChildIndex(this.lblInfo2, 0);
		base.Controls.SetChildIndex(this.lvwDetail, 0);
		base.Controls.SetChildIndex(this.lblDetail, 0);
		((System.ComponentModel.ISupportInitialize)this.picVelkaOdmena).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLine1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLine2).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
