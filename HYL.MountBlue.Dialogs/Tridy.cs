using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Uzivatele.Tridy;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Forms;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Tridy : _Dialog
{
	private const string icoTrida = "trida";

	private bool bBylaZmena;

	private IContainer components;

	private ObrazkoveTlacitko cmdTlZavrit;

	private PictureBox picLine1;

	private LinkLabel lnkPridatTridu;

	private LinkLabel lnkUpravitTridu;

	private LinkLabel lnkOdebratTridu;

	private ImageList imlIkony;

	private ListView lvwTridy;

	private ColumnHeader clhTrida;

	private ColumnHeader clhUcitel;

	private ColumnHeader clhPocetStudentu;

	private ToolTip ttt;

	public bool BylaZmena => bBylaZmena;

	public Tridy()
	{
		InitializeComponent();
		ToolTipText.NastavitToolTipText(ttt);
		Text = Texty.Tridy_Title;
		lnkPridatTridu.Text = Texty.Tridy_lnkPridatTridu;
		lnkUpravitTridu.Text = Texty.Tridy_lnkUpravitTridu;
		lnkOdebratTridu.Text = Texty.Tridy_lnkOdebratTridu;
		ttt.SetToolTip(lnkPridatTridu, Texty.Tridy_lnkPridatTridu_TTT);
		ttt.SetToolTip(lnkUpravitTridu, Texty.Tridy_lnkUpravitTridu_TTT);
		ttt.SetToolTip(lnkOdebratTridu, Texty.Tridy_lnkOdebratTridu_TTT);
		clhTrida.Text = Texty.Tridy_clhTrida;
		clhUcitel.Text = Texty.Tridy_clhUcitel;
		clhPocetStudentu.Text = Texty.Tridy_clhPocetStudentu;
		imlIkony.Images.Add("trida", GrafikaSkolni.pngTridaIco);
		NacistTridy(0);
		ObnovitPrvky();
	}

	private void NacistTridy(int iZvyraznitTriduID)
	{
		lvwTridy.BeginUpdate();
		lvwTridy.Items.Clear();
		HYL.MountBlue.Classes.Uzivatele.Tridy.Trida[] array = Klient.Stanice.AktualniTridy.SerazenySeznamTrid();
		foreach (HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida in array)
		{
			ListViewItem listViewItem = new ListViewItem();
			listViewItem.Text = trida.ToString();
			listViewItem.SubItems.Add(trida.UcitelTridyToString());
			listViewItem.SubItems.Add(trida.PocetStudentuVeTride().ToString());
			listViewItem.ImageKey = "trida";
			listViewItem.Tag = trida.TridaID;
			lvwTridy.Items.Add(listViewItem);
			if (trida.TridaID == iZvyraznitTriduID)
			{
				listViewItem.Selected = true;
			}
		}
		if (iZvyraznitTriduID == 0 && lvwTridy.Items.Count > 0)
		{
			lvwTridy.Items[0].Selected = true;
		}
		if (lvwTridy.SelectedItems.Count == 1)
		{
			lvwTridy.SelectedItems[0].EnsureVisible();
		}
		lvwTridy.EndUpdate();
	}

	public static bool ZobrazitTridy(HlavniOkno owner)
	{
		using Tridy tridy = new Tridy();
		tridy.ShowDialog(owner);
		return tridy.BylaZmena;
	}

	private void cmdTlZavrit_TlacitkoStisknuto()
	{
		Close();
	}

	private void Tridy_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Control && e.KeyCode == Keys.Insert)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			PridatTridu();
		}
		else if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.Return)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			UpravitTridu();
		}
		else if (e.Control && e.KeyCode == Keys.Delete)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			OdebratTridu();
		}
		else if (e.KeyCode == Keys.Escape)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			cmdTlZavrit_TlacitkoStisknuto();
		}
	}

	private void lnkPridatTridu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		PridatTridu();
	}

	private void lnkUpravitTridu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		UpravitTridu();
	}

	private void lnkOdebratTridu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		OdebratTridu();
	}

	private void PridatTridu()
	{
		HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida = Klient.Stanice.AktualniTridy.PridatNovouTridu();
		using Trida trida2 = new Trida(trida, novaTrida: true);
		if (trida2.ShowDialog(this) == DialogResult.OK)
		{
			NacistTridy(trida.TridaID);
			bBylaZmena = true;
		}
	}

	private void UpravitTridu()
	{
		if (lvwTridy.SelectedItems.Count != 1)
		{
			return;
		}
		int num = (int)lvwTridy.SelectedItems[0].Tag;
		HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida = Klient.Stanice.AktualniTridy[num];
		if (trida == null)
		{
			return;
		}
		using Trida trida2 = new Trida(trida, novaTrida: false);
		if (trida2.ShowDialog(this) == DialogResult.OK)
		{
			NacistTridy(num);
			bBylaZmena = true;
		}
	}

	private void OdebratTridu()
	{
		if (lvwTridy.SelectedItems.Count == 1)
		{
			int tridaID = (int)lvwTridy.SelectedItems[0].Tag;
			HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida = Klient.Stanice.AktualniTridy[tridaID];
			if (trida.PocetStudentuVeTride() != 0)
			{
				MsgBoxMB.ZobrazitMessageBox(Texty.Tridy_msgNelzeOdebrat, Texty.Tridy_msgNelzeOdebrat_Title, eMsgBoxTlacitka.OK);
			}
			else if (MsgBoxMB.ZobrazitMessageBox(string.Format(Texty.Tridy_msgOpravduOdebrat, trida.Rocnik, trida.Oznaceni), Texty.Tridy_msgOpravduOdebrat_Title, eMsgBoxTlacitka.AnoNe) != DialogResult.No)
			{
				Klient.Stanice.AktualniTridy.OdebratTridu(tridaID);
				NacistTridy(0);
				bBylaZmena = true;
			}
		}
	}

	private void lvwTridy_SelectedIndexChanged(object sender, EventArgs e)
	{
		ObnovitPrvky();
	}

	private void ObnovitPrvky()
	{
		lnkUpravitTridu.Enabled = lvwTridy.SelectedItems.Count == 1;
		lnkOdebratTridu.Enabled = lnkUpravitTridu.Enabled;
	}

	private void lvwTridy_DoubleClick(object sender, EventArgs e)
	{
		UpravitTridu();
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
		this.cmdTlZavrit = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.picLine1 = new System.Windows.Forms.PictureBox();
		this.lnkPridatTridu = new System.Windows.Forms.LinkLabel();
		this.lnkUpravitTridu = new System.Windows.Forms.LinkLabel();
		this.lnkOdebratTridu = new System.Windows.Forms.LinkLabel();
		this.imlIkony = new System.Windows.Forms.ImageList(this.components);
		this.lvwTridy = new System.Windows.Forms.ListView();
		this.clhTrida = new System.Windows.Forms.ColumnHeader();
		this.clhUcitel = new System.Windows.Forms.ColumnHeader();
		this.clhPocetStudentu = new System.Windows.Forms.ColumnHeader();
		this.ttt = new System.Windows.Forms.ToolTip(this.components);
		((System.ComponentModel.ISupportInitialize)this.picLine1).BeginInit();
		base.SuspendLayout();
		this.cmdTlZavrit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdTlZavrit.BackColor = System.Drawing.Color.White;
		this.cmdTlZavrit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.cmdTlZavrit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdTlZavrit.ForeColor = System.Drawing.Color.Black;
		this.cmdTlZavrit.Location = new System.Drawing.Point(351, 270);
		this.cmdTlZavrit.Name = "cmdTlZavrit";
		this.cmdTlZavrit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdTlZavrit.Size = new System.Drawing.Size(63, 23);
		this.cmdTlZavrit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdTlZavrit.TabIndex = 4;
		this.cmdTlZavrit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdTlZavrit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdTlZavrit.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdTlZavrit_TlacitkoStisknuto);
		this.picLine1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.picLine1.BackColor = System.Drawing.Color.Black;
		this.picLine1.Location = new System.Drawing.Point(1, 263);
		this.picLine1.Name = "picLine1";
		this.picLine1.Size = new System.Drawing.Size(423, 1);
		this.picLine1.TabIndex = 27;
		this.picLine1.TabStop = false;
		this.lnkPridatTridu.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lnkPridatTridu.Location = new System.Drawing.Point(19, 274);
		this.lnkPridatTridu.Name = "lnkPridatTridu";
		this.lnkPridatTridu.Size = new System.Drawing.Size(89, 15);
		this.lnkPridatTridu.TabIndex = 1;
		this.lnkPridatTridu.TabStop = true;
		this.lnkPridatTridu.Text = "???";
		this.lnkPridatTridu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lnkPridatTridu.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkPridatTridu_LinkClicked);
		this.lnkUpravitTridu.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lnkUpravitTridu.Location = new System.Drawing.Point(114, 274);
		this.lnkUpravitTridu.Name = "lnkUpravitTridu";
		this.lnkUpravitTridu.Size = new System.Drawing.Size(89, 15);
		this.lnkUpravitTridu.TabIndex = 2;
		this.lnkUpravitTridu.TabStop = true;
		this.lnkUpravitTridu.Text = "???";
		this.lnkUpravitTridu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lnkUpravitTridu.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkUpravitTridu_LinkClicked);
		this.lnkOdebratTridu.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lnkOdebratTridu.Location = new System.Drawing.Point(209, 274);
		this.lnkOdebratTridu.Name = "lnkOdebratTridu";
		this.lnkOdebratTridu.Size = new System.Drawing.Size(89, 15);
		this.lnkOdebratTridu.TabIndex = 3;
		this.lnkOdebratTridu.TabStop = true;
		this.lnkOdebratTridu.Text = "???";
		this.lnkOdebratTridu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lnkOdebratTridu.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkOdebratTridu_LinkClicked);
		this.imlIkony.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
		this.imlIkony.ImageSize = new System.Drawing.Size(20, 20);
		this.imlIkony.TransparentColor = System.Drawing.Color.Transparent;
		this.lvwTridy.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lvwTridy.BackColor = System.Drawing.Color.White;
		this.lvwTridy.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lvwTridy.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.clhTrida, this.clhUcitel, this.clhPocetStudentu });
		this.lvwTridy.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lvwTridy.ForeColor = System.Drawing.Color.Black;
		this.lvwTridy.FullRowSelect = true;
		this.lvwTridy.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
		this.lvwTridy.HideSelection = false;
		this.lvwTridy.Location = new System.Drawing.Point(2, 46);
		this.lvwTridy.MultiSelect = false;
		this.lvwTridy.Name = "lvwTridy";
		this.lvwTridy.Size = new System.Drawing.Size(422, 216);
		this.lvwTridy.SmallImageList = this.imlIkony;
		this.lvwTridy.TabIndex = 0;
		this.lvwTridy.UseCompatibleStateImageBehavior = false;
		this.lvwTridy.View = System.Windows.Forms.View.Details;
		this.lvwTridy.DoubleClick += new System.EventHandler(lvwTridy_DoubleClick);
		this.lvwTridy.SelectedIndexChanged += new System.EventHandler(lvwTridy_SelectedIndexChanged);
		this.clhTrida.Text = "???";
		this.clhTrida.Width = 108;
		this.clhUcitel.Text = "???";
		this.clhUcitel.Width = 175;
		this.clhPocetStudentu.Text = "???";
		this.clhPocetStudentu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.clhPocetStudentu.Width = 89;
		base.ClientSize = new System.Drawing.Size(426, 301);
		base.Controls.Add(this.lvwTridy);
		base.Controls.Add(this.lnkOdebratTridu);
		base.Controls.Add(this.lnkUpravitTridu);
		base.Controls.Add(this.lnkPridatTridu);
		base.Controls.Add(this.picLine1);
		base.Controls.Add(this.cmdTlZavrit);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "Tridy";
		base.Opacity = 0.9990000128746033;
		this.Text = "???";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Tridy_KeyDown);
		base.Controls.SetChildIndex(this.cmdTlZavrit, 0);
		base.Controls.SetChildIndex(this.picLine1, 0);
		base.Controls.SetChildIndex(this.lnkPridatTridu, 0);
		base.Controls.SetChildIndex(this.lnkUpravitTridu, 0);
		base.Controls.SetChildIndex(this.lnkOdebratTridu, 0);
		base.Controls.SetChildIndex(this.lvwTridy, 0);
		((System.ComponentModel.ISupportInitialize)this.picLine1).EndInit();
		base.ResumeLayout(false);
	}
}
