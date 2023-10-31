using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Controls;

namespace HYL.MountBlue.Dialogs;

internal sealed class MsgBoxMB : _Dialog
{
	private const int Okraj = 12;

	private AutoSizeLabel asl;

	private IContainer components;

	private MsgBoxTlacitka msgTlacitka;

	private MsgBoxMB(string textZpravy, string textZahlavi, eMsgBoxTlacitka tlacitka)
	{
		InitializeComponent();
		Text = textZahlavi;
		int num = 12;
		if (textZahlavi.Length > 0)
		{
			num += base.VyskaZahlavi;
		}
		asl = new AutoSizeLabel(CreateGraphics(), textZpravy, new Point(12, num), Font, new StringFormat(), ForeColor, MinimumSize.Width - 24);
		Size size = asl.Velikost.ToSize();
		size.Width += 24;
		size.Height += num + 24 + msgTlacitka.Height;
		base.Size = size;
		msgTlacitka.TlacitkaOkna = tlacitka;
	}

	private void msg_Tlacitko(DialogResult dlgres)
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
		using MsgBoxMB msgBoxMB = new MsgBoxMB(HYL.MountBlue.Classes.Text.TextNBSP(textZpravy), HYL.MountBlue.Classes.Text.TextNBSP(textZahlavi), tlacitka);
		return msgBoxMB.ShowDialog(_Plocha.HlavniOkno);
	}

	private void MsgBoxMB_KeyDown(object sender, KeyEventArgs e)
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
		this.msgTlacitka.Location = new System.Drawing.Point(9, 111);
		this.msgTlacitka.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.msgTlacitka.MaximumSize = new System.Drawing.Size(700, 30);
		this.msgTlacitka.MinimumSize = new System.Drawing.Size(150, 30);
		this.msgTlacitka.Name = "msgTlacitka";
		this.msgTlacitka.Size = new System.Drawing.Size(281, 30);
		this.msgTlacitka.TabIndex = 21;
		this.msgTlacitka.TlacitkaOkna = HYL.MountBlue.Controls.eMsgBoxTlacitka.AnoNe;
		this.msgTlacitka.Tlacitko += new HYL.MountBlue.Controls.MsgBoxTlacitka.StisknutoTlacitko(msg_Tlacitko);
		base.ClientSize = new System.Drawing.Size(300, 150);
		base.Controls.Add(this.msgTlacitka);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "MsgBoxMB";
		this.Text = "???";
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(MsgBoxMB_KeyDown);
		base.Controls.SetChildIndex(this.msgTlacitka, 0);
		base.ResumeLayout(false);
	}
}
