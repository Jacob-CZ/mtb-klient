using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class TextyCviceni : _Dialog
{
	private struct LekcePolozka
	{
		public int CisloLekce;

		public bool JeTrenink;

		public string Popis;

		public override string ToString()
		{
			return Popis;
		}
	}

	private const int TreninkMinutovka = 801;

	private const int Trenink5Minutovka = 802;

	private const int Trenink10Minutovka = 803;

	private Ucitel ucitel;

	private string[] texty;

	private IContainer components;

	private ComboBox lstLekce;

	private ObrazkoveTlacitko cmdTlZavrit;

	private Label lblLekce;

	private ListBox lstTexty;

	private Label lblTexty;

	private LinkLabel lnkZkopirovat;

	private LinkLabel lnkZkopirovatVse;

	private TextBox txtText;

	public TextyCviceni(Ucitel uc)
	{
		InitializeComponent();
		ucitel = uc;
		Text = Texty.TextyCviceni_Title;
		lblLekce.Text = Texty.TextyCviceni_lblLekce;
		lnkZkopirovat.Text = Texty.TextyCviceni_lnkZkopirovat;
		txtText.Font = Pismo.PismoTextu(ucitel.VelikostPisma);
		txtText.ForeColor = ucitel.BarvaPisma;
		txtText.BackColor = ucitel.BarvaPozadi;
		NacistLekce();
	}

	private void NacistLekce()
	{
		lstLekce.Items.Clear();
		Klavesy.sLekceKlavesy[] array = _Lekce.Lekce().Klavesy.KlavesyDoArrayListu();
		Klavesy.sLekceKlavesy[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Klavesy.sLekceKlavesy sLekceKlavesy = array2[i];
			LekcePolozka lekcePolozka = default(LekcePolozka);
			lekcePolozka.CisloLekce = sLekceKlavesy.Lekce;
			lekcePolozka.JeTrenink = false;
			lekcePolozka.Popis = string.Format(Texty.Uzivatel_lstPocatecniLekce_pol, sLekceKlavesy.Lekce, sLekceKlavesy.Text);
			lstLekce.Items.Add(lekcePolozka);
		}
		LekcePolozka lekcePolozka2 = default(LekcePolozka);
		lekcePolozka2.CisloLekce = 801;
		lekcePolozka2.JeTrenink = true;
		lekcePolozka2.Popis = Texty.Uzivatel_lstPocatecniLekce_treninkMinutovky;
		lstLekce.Items.Add(lekcePolozka2);
		LekcePolozka lekcePolozka3 = default(LekcePolozka);
		lekcePolozka3.CisloLekce = 802;
		lekcePolozka3.JeTrenink = true;
		lekcePolozka3.Popis = Texty.Uzivatel_lstPocatecniLekce_trenink5Minutovky;
		lstLekce.Items.Add(lekcePolozka3);
		LekcePolozka lekcePolozka4 = default(LekcePolozka);
		lekcePolozka4.CisloLekce = 803;
		lekcePolozka4.JeTrenink = true;
		lekcePolozka4.Popis = Texty.Uzivatel_lstPocatecniLekce_trenink10Minutovky;
		lstLekce.Items.Add(lekcePolozka4);
		if (lstLekce.Items.Count > 0)
		{
			lstLekce.SelectedIndex = 0;
		}
	}

	internal static void ZobrazitTextyCviceni(Ucitel ucitel)
	{
		using TextyCviceni textyCviceni = new TextyCviceni(ucitel);
		textyCviceni.ShowDialog();
	}

	private void lstLekce_SelectedIndexChanged(object sender, EventArgs e)
	{
		NacistLekci();
	}

	private void NacistLekci()
	{
		if (lstLekce.SelectedIndex < 0)
		{
			lblTexty.Text = string.Empty;
			lstTexty.Items.Clear();
			lnkZkopirovat.Visible = false;
			lnkZkopirovatVse.Visible = false;
			return;
		}
		Cursor = Cursors.WaitCursor;
		LekcePolozka lekcePolozka = (LekcePolozka)lstLekce.SelectedItem;
		lblTexty.Text = Texty.TextyCviceni_lblTexty_nacitam;
		lstTexty.Items.Clear();
		txtText.Text = string.Empty;
		txtText.Enabled = true;
		if (lekcePolozka.JeTrenink)
		{
			if (lekcePolozka.CisloLekce == 801)
			{
				lblTexty.Text = string.Format(Texty.TextyCviceni_lblTexty_treninkMinutovky, lekcePolozka.CisloLekce);
				lnkZkopirovatVse.Text = string.Format(Texty.TextyCviceni_lnkZkopirovatVse_treninkMinutovky, lekcePolozka.CisloLekce);
			}
			else if (lekcePolozka.CisloLekce == 802)
			{
				lblTexty.Text = string.Format(Texty.TextyCviceni_lblTexty_trenink5Minutovky, lekcePolozka.CisloLekce);
				lnkZkopirovatVse.Text = string.Format(Texty.TextyCviceni_lnkZkopirovatVse_trenink5Minutovky, lekcePolozka.CisloLekce);
			}
			else if (lekcePolozka.CisloLekce == 803)
			{
				lblTexty.Text = string.Format(Texty.TextyCviceni_lblTexty_trenink10Minutovky, lekcePolozka.CisloLekce);
				lnkZkopirovatVse.Text = string.Format(Texty.TextyCviceni_lnkZkopirovatVse_trenink10Minutovky, lekcePolozka.CisloLekce);
			}
		}
		else
		{
			lblTexty.Text = string.Format(Texty.TextyCviceni_lblTexty, lekcePolozka.CisloLekce);
			lnkZkopirovatVse.Text = string.Format(Texty.TextyCviceni_lnkZkopirovatVse, lekcePolozka.CisloLekce);
		}
		lnkZkopirovat.Visible = true;
		lnkZkopirovatVse.Visible = true;
		Application.DoEvents();
		texty = _Lekce.Lekce().TextyLekce(lekcePolozka.CisloLekce);
		string[] array = texty;
		foreach (string text in array)
		{
			lstTexty.Items.Add(StringEllipse(text));
		}
		if (lstTexty.Items.Count > 0)
		{
			lstTexty.SelectedIndex = 0;
		}
		Cursor = Cursors.Default;
	}

	private string StringEllipse(string text)
	{
		text = text.Replace("\r\n", " ");
		if (text.Length <= 70)
		{
			return text;
		}
		return text.Substring(0, 70) + Texty.TextyCviceni_lstTexty_triTecky;
	}

	private void NacistText()
	{
		if (lstTexty.SelectedIndex < 0)
		{
			txtText.Text = string.Empty;
			txtText.Enabled = false;
			lnkZkopirovat.Enabled = false;
		}
		else
		{
			txtText.Text = texty[lstTexty.SelectedIndex];
			txtText.Enabled = true;
			lnkZkopirovat.Enabled = true;
		}
	}

	private void lnkZkopirovat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Clipboard.SetText(txtText.Text);
	}

	private void lnkZkopirovatVse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string[] array = texty;
		foreach (string value in array)
		{
			stringBuilder.Append(value);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
		}
		Clipboard.SetText(stringBuilder.ToString());
	}

	private void TextyCviceni_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			cmdTlZavrit_TlacitkoStisknuto();
			e.Handled = true;
			e.SuppressKeyPress = true;
		}
	}

	private void cmdTlZavrit_TlacitkoStisknuto()
	{
		Close();
	}

	private void lstTexty_SelectedIndexChanged(object sender, EventArgs e)
	{
		NacistText();
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
		this.lstLekce = new System.Windows.Forms.ComboBox();
		this.cmdTlZavrit = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblLekce = new System.Windows.Forms.Label();
		this.lstTexty = new System.Windows.Forms.ListBox();
		this.lblTexty = new System.Windows.Forms.Label();
		this.lnkZkopirovat = new System.Windows.Forms.LinkLabel();
		this.lnkZkopirovatVse = new System.Windows.Forms.LinkLabel();
		this.txtText = new System.Windows.Forms.TextBox();
		base.SuspendLayout();
		this.lstLekce.BackColor = System.Drawing.Color.Gainsboro;
		this.lstLekce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lstLekce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lstLekce.FormattingEnabled = true;
		this.lstLekce.Location = new System.Drawing.Point(12, 81);
		this.lstLekce.Name = "lstLekce";
		this.lstLekce.Size = new System.Drawing.Size(558, 23);
		this.lstLekce.TabIndex = 1;
		this.lstLekce.SelectedIndexChanged += new System.EventHandler(lstLekce_SelectedIndexChanged);
		this.cmdTlZavrit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdTlZavrit.BackColor = System.Drawing.Color.White;
		this.cmdTlZavrit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.cmdTlZavrit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdTlZavrit.ForeColor = System.Drawing.Color.Black;
		this.cmdTlZavrit.Location = new System.Drawing.Point(507, 527);
		this.cmdTlZavrit.Name = "cmdTlZavrit";
		this.cmdTlZavrit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdTlZavrit.Size = new System.Drawing.Size(63, 23);
		this.cmdTlZavrit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdTlZavrit.TabIndex = 7;
		this.cmdTlZavrit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdTlZavrit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdTlZavrit.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdTlZavrit_TlacitkoStisknuto);
		this.lblLekce.AutoSize = true;
		this.lblLekce.Location = new System.Drawing.Point(12, 63);
		this.lblLekce.Name = "lblLekce";
		this.lblLekce.Size = new System.Drawing.Size(28, 15);
		this.lblLekce.TabIndex = 0;
		this.lblLekce.Text = "???";
		this.lstTexty.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lstTexty.BackColor = System.Drawing.Color.WhiteSmoke;
		this.lstTexty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.lstTexty.Font = new System.Drawing.Font("Courier New", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lstTexty.FormattingEnabled = true;
		this.lstTexty.ItemHeight = 15;
		this.lstTexty.Location = new System.Drawing.Point(12, 125);
		this.lstTexty.Name = "lstTexty";
		this.lstTexty.Size = new System.Drawing.Size(558, 182);
		this.lstTexty.TabIndex = 3;
		this.lstTexty.SelectedIndexChanged += new System.EventHandler(lstTexty_SelectedIndexChanged);
		this.lblTexty.Location = new System.Drawing.Point(12, 107);
		this.lblTexty.Name = "lblTexty";
		this.lblTexty.Size = new System.Drawing.Size(282, 15);
		this.lblTexty.TabIndex = 2;
		this.lblTexty.Text = "???";
		this.lnkZkopirovat.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lnkZkopirovat.AutoSize = true;
		this.lnkZkopirovat.Location = new System.Drawing.Point(12, 519);
		this.lnkZkopirovat.Name = "lnkZkopirovat";
		this.lnkZkopirovat.Size = new System.Drawing.Size(28, 15);
		this.lnkZkopirovat.TabIndex = 5;
		this.lnkZkopirovat.TabStop = true;
		this.lnkZkopirovat.Text = "???";
		this.lnkZkopirovat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkZkopirovat_LinkClicked);
		this.lnkZkopirovatVse.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lnkZkopirovatVse.AutoSize = true;
		this.lnkZkopirovatVse.Location = new System.Drawing.Point(12, 537);
		this.lnkZkopirovatVse.Name = "lnkZkopirovatVse";
		this.lnkZkopirovatVse.Size = new System.Drawing.Size(28, 15);
		this.lnkZkopirovatVse.TabIndex = 6;
		this.lnkZkopirovatVse.TabStop = true;
		this.lnkZkopirovatVse.Text = "???";
		this.lnkZkopirovatVse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkZkopirovatVse_LinkClicked);
		this.txtText.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtText.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtText.Font = new System.Drawing.Font("Courier New", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.txtText.Location = new System.Drawing.Point(12, 313);
		this.txtText.Multiline = true;
		this.txtText.Name = "txtText";
		this.txtText.ReadOnly = true;
		this.txtText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtText.Size = new System.Drawing.Size(557, 203);
		this.txtText.TabIndex = 4;
		base.ClientSize = new System.Drawing.Size(582, 562);
		base.Controls.Add(this.txtText);
		base.Controls.Add(this.lnkZkopirovatVse);
		base.Controls.Add(this.lnkZkopirovat);
		base.Controls.Add(this.lblTexty);
		base.Controls.Add(this.lstTexty);
		base.Controls.Add(this.lblLekce);
		base.Controls.Add(this.cmdTlZavrit);
		base.Controls.Add(this.lstLekce);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "TextyCviceni";
		base.Opacity = 0.9990000128746033;
		this.Text = "???";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(TextyCviceni_KeyDown);
		base.Controls.SetChildIndex(this.lstLekce, 0);
		base.Controls.SetChildIndex(this.cmdTlZavrit, 0);
		base.Controls.SetChildIndex(this.lblLekce, 0);
		base.Controls.SetChildIndex(this.lstTexty, 0);
		base.Controls.SetChildIndex(this.lblTexty, 0);
		base.Controls.SetChildIndex(this.lnkZkopirovat, 0);
		base.Controls.SetChildIndex(this.lnkZkopirovatVse, 0);
		base.Controls.SetChildIndex(this.txtText, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
