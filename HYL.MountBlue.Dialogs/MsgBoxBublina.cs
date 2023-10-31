using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Controls;

namespace HYL.MountBlue.Dialogs;

internal class MsgBoxBublina : _Bublina
{
	private const int Okraj = 12;

	private AutoSizeLabel asl;

	private IContainer components;

	private MsgBoxTlacitka msgTlacitka;

	private MsgBoxBublina(string textZpravy, string textZahlavi, eMsgBoxTlacitka tlacitka)
	{
		InitializeComponent();
		Text = textZahlavi;
		int num = 12;
		if (textZahlavi.Length > 0)
		{
			num += base.VyskaZahlavi;
		}
		asl = new AutoSizeLabel(CreateGraphics(), textZpravy, new Point(12, num), Font, new StringFormat(), ForeColor);
		Size oblastOkna = asl.Velikost.ToSize();
		oblastOkna.Width += 24;
		oblastOkna.Height += num + 24 + msgTlacitka.Height;
		NastavitVelikostOkna(oblastOkna);
		msgTlacitka.TlacitkaOkna = tlacitka;
	}

	private void msgTlacitka_Tlacitko(DialogResult dlgres)
	{
		base.DialogResult = dlgres;
		Close();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		asl.Vykreslit(e.Graphics);
	}

	internal static DialogResult ZobrazitMessageBox(string textZpravy, string textZahlavi, eMsgBoxTlacitka tlacitka)
	{
		using MsgBoxBublina msgBoxBublina = new MsgBoxBublina(HYL.MountBlue.Classes.Text.TextNBSP(textZpravy), HYL.MountBlue.Classes.Text.TextNBSP(textZahlavi), tlacitka);
		return msgBoxBublina.BublinaZobrazitDialog();
	}

	protected override void OnFadeIn()
	{
		SpecialniGrafika.FadeIn(this);
	}

	protected override void OnFadeOut()
	{
		SpecialniGrafika.FadeOut(this);
	}

	private void MsgBoxBublina_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Alt && e.KeyCode == Keys.F4)
		{
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
		else if (msgTlacitka.StisknutaKlavesa(e))
		{
			e.SuppressKeyPress = true;
			e.Handled = true;
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
		this.msgTlacitka = new HYL.MountBlue.Controls.MsgBoxTlacitka();
		base.SuspendLayout();
		this.msgTlacitka.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.msgTlacitka.BackColor = System.Drawing.Color.White;
		this.msgTlacitka.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.msgTlacitka.Location = new System.Drawing.Point(8, 113);
		this.msgTlacitka.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.msgTlacitka.MaximumSize = new System.Drawing.Size(700, 30);
		this.msgTlacitka.MinimumSize = new System.Drawing.Size(150, 30);
		this.msgTlacitka.Name = "msgTlacitka";
		this.msgTlacitka.Size = new System.Drawing.Size(237, 30);
		this.msgTlacitka.TabIndex = 21;
		this.msgTlacitka.TlacitkaOkna = HYL.MountBlue.Controls.eMsgBoxTlacitka.OK;
		this.msgTlacitka.Tlacitko += new HYL.MountBlue.Controls.MsgBoxTlacitka.StisknutoTlacitko(msgTlacitka_Tlacitko);
		base.ClientSize = new System.Drawing.Size(318, 150);
		base.Controls.Add(this.msgTlacitka);
		this.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "MsgBoxBublina";
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(MsgBoxBublina_KeyDown);
		base.Controls.SetChildIndex(this.msgTlacitka, 0);
		base.ResumeLayout(false);
	}
}
