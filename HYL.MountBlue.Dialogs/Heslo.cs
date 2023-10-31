using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Heslo : _Bublina
{
	private string hashHesla;

	private IContainer components;

	private Label lblHeslo;

	private TextBox txtHeslo;

	private ObrazkoveTlacitko cmdStorno;

	private ObrazkoveTlacitko cmdOK;

	public Heslo(string hashHesla)
	{
		InitializeComponent();
		Text = Texty.Heslo_Title;
		lblHeslo.Text = Texty.Heslo_lblHeslo;
		this.hashHesla = hashHesla;
	}

	public static bool OveritHeslo(string hashHesla, IWin32Window owner)
	{
		using Heslo heslo = new Heslo(hashHesla);
		if (heslo.BublinaZobrazitDialog() == DialogResult.OK)
		{
			return true;
		}
		return false;
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		if (HashMD5.SpocitatHashMD5(txtHeslo.Text) != hashHesla)
		{
			MsgBoxMB.ZobrazitMessageBox(Texty.Heslo_msgboxSpatneHeslo, Texty.Heslo_msgboxSpatneHeslo_Title, eMsgBoxTlacitka.OK);
			txtHeslo.Text = "";
			txtHeslo.Focus();
		}
		else
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void cmdStorno_TlacitkoStisknuto()
	{
		Close();
	}

	private void Heslo_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			e.SuppressKeyPress = true;
			e.Handled = true;
			cmdOK_TlacitkoStisknuto();
		}
		else if (e.KeyCode == Keys.Escape)
		{
			e.SuppressKeyPress = true;
			e.Handled = true;
			cmdStorno_TlacitkoStisknuto();
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
		this.lblHeslo = new System.Windows.Forms.Label();
		this.txtHeslo = new System.Windows.Forms.TextBox();
		this.cmdStorno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		base.SuspendLayout();
		this.lblHeslo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblHeslo.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblHeslo.ForeColor = System.Drawing.Color.Black;
		this.lblHeslo.Location = new System.Drawing.Point(12, 45);
		this.lblHeslo.Name = "lblHeslo";
		this.lblHeslo.Size = new System.Drawing.Size(162, 40);
		this.lblHeslo.TabIndex = 35;
		this.lblHeslo.Text = "???";
		this.txtHeslo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtHeslo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtHeslo.ForeColor = System.Drawing.Color.Black;
		this.txtHeslo.Location = new System.Drawing.Point(12, 92);
		this.txtHeslo.MaxLength = 20;
		this.txtHeslo.Name = "txtHeslo";
		this.txtHeslo.Size = new System.Drawing.Size(162, 22);
		this.txtHeslo.TabIndex = 36;
		this.txtHeslo.UseSystemPasswordChar = true;
		this.cmdStorno.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdStorno.BackColor = System.Drawing.Color.White;
		this.cmdStorno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdStorno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdStorno.ForeColor = System.Drawing.Color.White;
		this.cmdStorno.Location = new System.Drawing.Point(92, 125);
		this.cmdStorno.Name = "cmdStorno";
		this.cmdStorno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoN;
		this.cmdStorno.Size = new System.Drawing.Size(70, 23);
		this.cmdStorno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoD;
		this.cmdStorno.TabIndex = 38;
		this.cmdStorno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoZ;
		this.cmdStorno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoH;
		this.cmdStorno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdStorno_TlacitkoStisknuto);
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(26, 125);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 37;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		base.ClientSize = new System.Drawing.Size(251, 160);
		base.Controls.Add(this.lblHeslo);
		base.Controls.Add(this.txtHeslo);
		base.Controls.Add(this.cmdStorno);
		base.Controls.Add(this.cmdOK);
		base.KeyPreview = true;
		base.Name = "Heslo";
		base.Opacity = 1.0;
		this.Text = "???";
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Heslo_KeyDown);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.cmdStorno, 0);
		base.Controls.SetChildIndex(this.txtHeslo, 0);
		base.Controls.SetChildIndex(this.lblHeslo, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
