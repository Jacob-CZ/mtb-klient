using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class ZmenaHesla : _Dialog
{
	private HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel;

	private IContainer components;

	private ObrazkoveTlacitko cmdStorno;

	private ObrazkoveTlacitko cmdOK;

	private TextBox txtNove1;

	private Label lblNove1;

	private TextBox txtNove2;

	private Label lblNove2;

	private PictureBox picLine2;

	private Label lblInfo;

	public ZmenaHesla(HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel, bool bVynucenaZmena)
	{
		InitializeComponent();
		Text = Texty.ZmenaHesla_Title;
		lblNove1.Text = Texty.ZmenaHesla_lblNove1;
		lblNove2.Text = Texty.ZmenaHesla_lblNove2;
		if (bVynucenaZmena)
		{
			lblInfo.Text = Texty.ZmenaHesla_lblInfo_Vynucena + " " + Texty.ZmenaHesla_lblInfo;
		}
		else
		{
			lblInfo.Text = Texty.ZmenaHesla_lblInfo;
		}
		this.uzivatel = uzivatel;
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		if (txtNove1.Text.Length == 0 || txtNove2.Text.Length == 0 || txtNove1.Text != txtNove2.Text)
		{
			MsgBoxMB.ZobrazitMessageBox(Texty.ZmenaHesla_msgbox2xStejne, Texty.ZmenaHesla_msgbox2xStejne_Title, eMsgBoxTlacitka.OK);
			txtNove1.Focus();
			return;
		}
		string text = HashMD5.SpocitatHashMD5(txtNove1.Text);
		if (text == uzivatel.Heslo)
		{
			MsgBoxMB.ZobrazitMessageBox(Texty.ZmenaHesla_msgboxStejne, Texty.ZmenaHesla_msgboxStejne_Title, eMsgBoxTlacitka.OK);
			txtNove1.Focus();
		}
		else if (txtNove1.Text.Length < 4)
		{
			MsgBoxMB.ZobrazitMessageBox(Texty.ZmenaHesla_msgboxKratke, Texty.ZmenaHesla_msgboxKratke_Title, eMsgBoxTlacitka.OK);
			txtNove1.Focus();
		}
		else
		{
			uzivatel.NastavitHeslo(text, bVynutitZmenu: false);
			base.DialogResult = DialogResult.OK;
			Close();
		}
	}

	private void cmdStorno_TlacitkoStisknuto()
	{
		Close();
	}

	public static bool ZobrazitZmenuHesla(IWin32Window owner, HYL.MountBlue.Classes.Uzivatele.Uzivatel.Uzivatel uzivatel, bool bVynucenaZmenaHesla)
	{
		using ZmenaHesla zmenaHesla = new ZmenaHesla(uzivatel, bVynucenaZmenaHesla);
		return zmenaHesla.ShowDialog(owner) == DialogResult.OK;
	}

	private void ZmenaHesla_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			cmdOK_TlacitkoStisknuto();
		}
		else if (e.KeyCode == Keys.Escape)
		{
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
		this.cmdStorno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.txtNove1 = new System.Windows.Forms.TextBox();
		this.lblNove1 = new System.Windows.Forms.Label();
		this.txtNove2 = new System.Windows.Forms.TextBox();
		this.lblNove2 = new System.Windows.Forms.Label();
		this.picLine2 = new System.Windows.Forms.PictureBox();
		this.lblInfo = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.picLine2).BeginInit();
		base.SuspendLayout();
		this.cmdStorno.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdStorno.BackColor = System.Drawing.Color.White;
		this.cmdStorno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdStorno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdStorno.ForeColor = System.Drawing.Color.White;
		this.cmdStorno.Location = new System.Drawing.Point(259, 152);
		this.cmdStorno.Name = "cmdStorno";
		this.cmdStorno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoN;
		this.cmdStorno.Size = new System.Drawing.Size(70, 23);
		this.cmdStorno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoD;
		this.cmdStorno.TabIndex = 5;
		this.cmdStorno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoZ;
		this.cmdStorno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoH;
		this.cmdStorno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdStorno_TlacitkoStisknuto);
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(193, 152);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 4;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.txtNove1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtNove1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtNove1.ForeColor = System.Drawing.Color.Black;
		this.txtNove1.Location = new System.Drawing.Point(162, 78);
		this.txtNove1.MaxLength = 20;
		this.txtNove1.Name = "txtNove1";
		this.txtNove1.Size = new System.Drawing.Size(154, 21);
		this.txtNove1.TabIndex = 1;
		this.txtNove1.UseSystemPasswordChar = true;
		this.lblNove1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblNove1.ForeColor = System.Drawing.Color.Black;
		this.lblNove1.Location = new System.Drawing.Point(66, 78);
		this.lblNove1.Name = "lblNove1";
		this.lblNove1.Size = new System.Drawing.Size(90, 21);
		this.lblNove1.TabIndex = 0;
		this.lblNove1.Text = "???";
		this.lblNove1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtNove2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtNove2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtNove2.ForeColor = System.Drawing.Color.Black;
		this.txtNove2.Location = new System.Drawing.Point(162, 105);
		this.txtNove2.MaxLength = 20;
		this.txtNove2.Name = "txtNove2";
		this.txtNove2.Size = new System.Drawing.Size(154, 21);
		this.txtNove2.TabIndex = 3;
		this.txtNove2.UseSystemPasswordChar = true;
		this.lblNove2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lblNove2.ForeColor = System.Drawing.Color.Black;
		this.lblNove2.Location = new System.Drawing.Point(14, 105);
		this.lblNove2.Name = "lblNove2";
		this.lblNove2.Size = new System.Drawing.Size(141, 21);
		this.lblNove2.TabIndex = 2;
		this.lblNove2.Text = "???";
		this.lblNove2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.picLine2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.picLine2.BackColor = System.Drawing.Color.Black;
		this.picLine2.Location = new System.Drawing.Point(12, 142);
		this.picLine2.Name = "picLine2";
		this.picLine2.Size = new System.Drawing.Size(317, 1);
		this.picLine2.TabIndex = 34;
		this.picLine2.TabStop = false;
		this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo.ForeColor = System.Drawing.Color.Gray;
		this.lblInfo.Location = new System.Drawing.Point(13, 43);
		this.lblInfo.Name = "lblInfo";
		this.lblInfo.Size = new System.Drawing.Size(316, 32);
		this.lblInfo.TabIndex = 35;
		this.lblInfo.Text = "???";
		this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.ClientSize = new System.Drawing.Size(341, 187);
		base.Controls.Add(this.lblInfo);
		base.Controls.Add(this.picLine2);
		base.Controls.Add(this.lblNove2);
		base.Controls.Add(this.txtNove2);
		base.Controls.Add(this.txtNove1);
		base.Controls.Add(this.lblNove1);
		base.Controls.Add(this.cmdStorno);
		base.Controls.Add(this.cmdOK);
		base.KeyPreview = true;
		base.Name = "ZmenaHesla";
		base.Opacity = 1.0;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "???";
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(ZmenaHesla_KeyDown);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.cmdStorno, 0);
		base.Controls.SetChildIndex(this.lblNove1, 0);
		base.Controls.SetChildIndex(this.txtNove1, 0);
		base.Controls.SetChildIndex(this.txtNove2, 0);
		base.Controls.SetChildIndex(this.lblNove2, 0);
		base.Controls.SetChildIndex(this.picLine2, 0);
		base.Controls.SetChildIndex(this.lblInfo, 0);
		((System.ComponentModel.ISupportInitialize)this.picLine2).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
