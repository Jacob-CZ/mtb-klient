using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Uzivatele.Tridy;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Trida : _Dialog
{
	private struct RocnikPolozka
	{
		public string Nazev;

		public int ID;

		public override string ToString()
		{
			return Nazev;
		}
	}

	private struct UcitelPolozka
	{
		public string Jmeno;

		public uint UID;

		public override string ToString()
		{
			return Jmeno;
		}
	}

	private bool bNovaTrida;

	private HYL.MountBlue.Classes.Uzivatele.Tridy.Trida trida;

	private IContainer components;

	private ComboBox lstRocnikID;

	private ComboBox lstUcitelUID;

	private Label lblRocnik;

	private Label lblOznaceni;

	private Label lblUcitel;

	private TextBox txtOznaceni;

	private ObrazkoveTlacitko cmdStorno;

	private ObrazkoveTlacitko cmdOK;

	private PictureBox picLine1;

	public Trida(HYL.MountBlue.Classes.Uzivatele.Tridy.Trida tr, bool novaTrida)
	{
		InitializeComponent();
		Text = Texty.Trida_Title;
		lblRocnik.Text = Texty.Trida_lblRocnik;
		lblOznaceni.Text = Texty.Trida_lblOznaceni;
		lblUcitel.Text = Texty.Trida_lblUcitel;
		bNovaTrida = novaTrida;
		trida = tr;
		txtOznaceni.Text = tr.Oznaceni;
		lstRocnikID.Items.Clear();
		lstUcitelUID.Items.Clear();
		int selectedIndex = 0;
		int[] platneIDrocniku = Rocniky.PlatneIDrocniku;
		foreach (int num in platneIDrocniku)
		{
			RocnikPolozka rocnikPolozka = new RocnikPolozka
			{
				Nazev = Rocniky.RocnikPodleID(num),
				ID = num
			};
			lstRocnikID.Items.Add(rocnikPolozka);
			if (rocnikPolozka.ID == tr.RocnikID)
			{
				selectedIndex = lstRocnikID.Items.Count - 1;
			}
		}
		lstRocnikID.SelectedIndex = selectedIndex;
		UcitelPolozka ucitelPolozka = new UcitelPolozka
		{
			UID = 0u,
			Jmeno = Texty.Trida_ucitelNespecifikovany
		};
		lstUcitelUID.Items.Add(ucitelPolozka);
		selectedIndex = 0;
		Ucitel[] array = Klient.Stanice.AktualniUzivatele.SerazenySeznamUcitelu();
		foreach (Ucitel ucitel in array)
		{
			ucitelPolozka = new UcitelPolozka
			{
				UID = ucitel.UID,
				Jmeno = ucitel.CeleJmenoStituly
			};
			lstUcitelUID.Items.Add(ucitelPolozka);
			if (ucitelPolozka.UID == tr.UcitelUID)
			{
				selectedIndex = lstUcitelUID.Items.Count - 1;
			}
		}
		lstUcitelUID.SelectedIndex = selectedIndex;
	}

	private void cmdStorno_TlacitkoStisknuto()
	{
		Close();
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		if (txtOznaceni.Text.Trim().Length == 0)
		{
			MsgBoxMB.ZobrazitMessageBox(Texty.Trida_msgZadejteOznaceniTridy, Texty.Trida_msgZadejteOznaceniTridy_Title, eMsgBoxTlacitka.OK);
			txtOznaceni.Focus();
			return;
		}
		RocnikPolozka rocnikPolozka = (RocnikPolozka)lstRocnikID.SelectedItem;
		if (Klient.Stanice.AktualniTridy.ExistujeTrida(rocnikPolozka.ID, txtOznaceni.Text, trida.TridaID))
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(Texty.Trida_msgTridaExistuje, rocnikPolozka.Nazev, txtOznaceni.Text), Texty.Trida_msgTridaExistuje_Title, eMsgBoxTlacitka.OK);
			lstRocnikID.Focus();
			return;
		}
		UcitelPolozka ucitelPolozka = (UcitelPolozka)lstUcitelUID.SelectedItem;
		bool flag = false;
		if (bNovaTrida || rocnikPolozka.ID != trida.RocnikID || txtOznaceni.Text != trida.Oznaceni || ucitelPolozka.UID != trida.UcitelUID)
		{
			flag = true;
		}
		if (flag)
		{
			trida.RocnikID = rocnikPolozka.ID;
			trida.Oznaceni = txtOznaceni.Text;
			trida.UcitelUID = ucitelPolozka.UID;
			Klient.Stanice.AktualniTridy.Ulozit();
			base.DialogResult = DialogResult.OK;
		}
		else if (bNovaTrida)
		{
			Klient.Stanice.AktualniTridy.OdebratTridu(trida.TridaID);
		}
		Close();
	}

	private void txtOznaceni_Validating(object sender, CancelEventArgs e)
	{
		txtOznaceni.Text = txtOznaceni.Text.Trim();
	}

	private void Trida_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmdOK_TlacitkoStisknuto();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
		else if (e.KeyCode == Keys.Escape)
		{
			cmdStorno_TlacitkoStisknuto();
			e.Handled = true;
			e.SuppressKeyPress = true;
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
		this.lstRocnikID = new System.Windows.Forms.ComboBox();
		this.lstUcitelUID = new System.Windows.Forms.ComboBox();
		this.lblRocnik = new System.Windows.Forms.Label();
		this.lblOznaceni = new System.Windows.Forms.Label();
		this.lblUcitel = new System.Windows.Forms.Label();
		this.txtOznaceni = new System.Windows.Forms.TextBox();
		this.cmdStorno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.picLine1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.picLine1).BeginInit();
		base.SuspendLayout();
		this.lstRocnikID.BackColor = System.Drawing.Color.Gainsboro;
		this.lstRocnikID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lstRocnikID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lstRocnikID.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lstRocnikID.FormattingEnabled = true;
		this.lstRocnikID.Location = new System.Drawing.Point(71, 56);
		this.lstRocnikID.Name = "lstRocnikID";
		this.lstRocnikID.Size = new System.Drawing.Size(80, 23);
		this.lstRocnikID.TabIndex = 1;
		this.lstUcitelUID.BackColor = System.Drawing.Color.Gainsboro;
		this.lstUcitelUID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lstUcitelUID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lstUcitelUID.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lstUcitelUID.FormattingEnabled = true;
		this.lstUcitelUID.Location = new System.Drawing.Point(71, 85);
		this.lstUcitelUID.Name = "lstUcitelUID";
		this.lstUcitelUID.Size = new System.Drawing.Size(235, 23);
		this.lstUcitelUID.TabIndex = 5;
		this.lblRocnik.AutoSize = true;
		this.lblRocnik.Location = new System.Drawing.Point(15, 60);
		this.lblRocnik.Name = "lblRocnik";
		this.lblRocnik.Size = new System.Drawing.Size(28, 15);
		this.lblRocnik.TabIndex = 0;
		this.lblRocnik.Text = "???";
		this.lblOznaceni.AutoSize = true;
		this.lblOznaceni.Location = new System.Drawing.Point(157, 60);
		this.lblOznaceni.Name = "lblOznaceni";
		this.lblOznaceni.Size = new System.Drawing.Size(28, 15);
		this.lblOznaceni.TabIndex = 2;
		this.lblOznaceni.Text = "???";
		this.lblUcitel.AutoSize = true;
		this.lblUcitel.Location = new System.Drawing.Point(15, 89);
		this.lblUcitel.Name = "lblUcitel";
		this.lblUcitel.Size = new System.Drawing.Size(28, 15);
		this.lblUcitel.TabIndex = 4;
		this.lblUcitel.Text = "???";
		this.txtOznaceni.BackColor = System.Drawing.Color.Gainsboro;
		this.txtOznaceni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtOznaceni.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.txtOznaceni.ForeColor = System.Drawing.Color.Black;
		this.txtOznaceni.Location = new System.Drawing.Point(226, 57);
		this.txtOznaceni.MaxLength = 16;
		this.txtOznaceni.Name = "txtOznaceni";
		this.txtOznaceni.Size = new System.Drawing.Size(80, 21);
		this.txtOznaceni.TabIndex = 3;
		this.txtOznaceni.Validating += new System.ComponentModel.CancelEventHandler(txtOznaceni_Validating);
		this.cmdStorno.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdStorno.BackColor = System.Drawing.Color.White;
		this.cmdStorno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdStorno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdStorno.ForeColor = System.Drawing.Color.White;
		this.cmdStorno.Location = new System.Drawing.Point(236, 133);
		this.cmdStorno.Name = "cmdStorno";
		this.cmdStorno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoN;
		this.cmdStorno.Size = new System.Drawing.Size(70, 23);
		this.cmdStorno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoD;
		this.cmdStorno.TabIndex = 7;
		this.cmdStorno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoZ;
		this.cmdStorno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoH;
		this.cmdStorno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdStorno_TlacitkoStisknuto);
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(170, 133);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 6;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.picLine1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.picLine1.BackColor = System.Drawing.Color.Black;
		this.picLine1.Location = new System.Drawing.Point(9, 126);
		this.picLine1.Name = "picLine1";
		this.picLine1.Size = new System.Drawing.Size(306, 1);
		this.picLine1.TabIndex = 42;
		this.picLine1.TabStop = false;
		base.ClientSize = new System.Drawing.Size(323, 164);
		base.Controls.Add(this.picLine1);
		base.Controls.Add(this.cmdStorno);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.txtOznaceni);
		base.Controls.Add(this.lblUcitel);
		base.Controls.Add(this.lblOznaceni);
		base.Controls.Add(this.lblRocnik);
		base.Controls.Add(this.lstUcitelUID);
		base.Controls.Add(this.lstRocnikID);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "Trida";
		base.Opacity = 0.9990000128746033;
		this.Text = "???";
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Trida_KeyDown);
		base.Controls.SetChildIndex(this.lstRocnikID, 0);
		base.Controls.SetChildIndex(this.lstUcitelUID, 0);
		base.Controls.SetChildIndex(this.lblRocnik, 0);
		base.Controls.SetChildIndex(this.lblOznaceni, 0);
		base.Controls.SetChildIndex(this.lblUcitel, 0);
		base.Controls.SetChildIndex(this.txtOznaceni, 0);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.cmdStorno, 0);
		base.Controls.SetChildIndex(this.picLine1, 0);
		((System.ComponentModel.ISupportInitialize)this.picLine1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
