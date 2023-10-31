using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class PostupovyKlic : _Bublina
{
	private PStudent student;

	private bool bZadatKlic;

	private IContainer components;

	private Label lblInfo1;

	private TextBox txtPostupovyKlic;

	private ObrazkoveTlacitko cmdStorno;

	private ObrazkoveTlacitko cmdOK1;

	private LinkLabel lnkZaslatNaEmail;

	private ObrazkoveTlacitko cmdOK2;

	private Label lblInfo2;

	private LinkLabel lnkZkopirovatDoSchranky;

	public PostupovyKlic(PStudent stu, bool zadatKlic)
	{
		InitializeComponent();
		student = stu;
		bZadatKlic = zadatKlic;
		Text = Texty.PostupovyKlic_Title;
		if (bZadatKlic)
		{
			lblInfo1.Text = Texty.PostupovyKlic_lblInfo1zadatZdomu;
			lblInfo2.Text = Texty.PostupovyKlic_lblInfo2zadat;
			lblInfo2.Visible = true;
			lnkZaslatNaEmail.Visible = false;
			lnkZkopirovatDoSchranky.Visible = false;
			cmdOK1.Visible = true;
			cmdStorno.Visible = true;
			cmdOK2.Visible = false;
			txtPostupovyKlic.ReadOnly = false;
		}
		else
		{
			lblInfo1.Text = Texty.PostupovyKlic_lblInfo1zobrazitDomu;
			lblInfo2.Text = string.Empty;
			lblInfo2.Visible = false;
			lnkZaslatNaEmail.Text = Texty.PostupovyKlic_lnkZaslatNaEmail;
			lnkZaslatNaEmail.Visible = false;
			lnkZkopirovatDoSchranky.Text = Texty.PostupovyKlic_lnkZkopirovatDoSchranky;
			lnkZkopirovatDoSchranky.Visible = true;
			cmdOK1.Visible = false;
			cmdStorno.Visible = false;
			cmdOK2.Visible = true;
			txtPostupovyKlic.Text = stu.VygenerovatPostupovyKlic();
			txtPostupovyKlic.ReadOnly = true;
		}
	}

	public static void ZobrazitPostupovyKlic(PStudent stu, bool zadatKlic)
	{
		using PostupovyKlic postupovyKlic = new PostupovyKlic(stu, zadatKlic);
		postupovyKlic.BublinaZobrazitDialog();
	}

	private void cmdStorno_TlacitkoStisknuto()
	{
		Close();
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		if (bZadatKlic && txtPostupovyKlic.Text.Length > 0)
		{
			switch (student.NacistPostupovyKlic(txtPostupovyKlic.Text))
			{
			case PStudent.ePostupovyKlic.zDomu:
				MsgBoxMB.ZobrazitMessageBox(Texty.PostupovyKlic_msgZdomu, Text, eMsgBoxTlacitka.OK);
				break;
			case PStudent.ePostupovyKlic.zeSkoly:
				MsgBoxMB.ZobrazitMessageBox(Texty.PostupovyKlic_msgZeSkoly, Text, eMsgBoxTlacitka.OK);
				break;
			case PStudent.ePostupovyKlic.stejny:
				MsgBoxMB.ZobrazitMessageBox(Texty.PostupovyKlic_msgSteny, Text, eMsgBoxTlacitka.OK);
				base.DialogResult = DialogResult.Cancel;
				Close();
				break;
			case PStudent.ePostupovyKlic.mensiNez:
				MsgBoxMB.ZobrazitMessageBox(Texty.PostupovyKlic_msgMensiNez, Text, eMsgBoxTlacitka.OK);
				base.DialogResult = DialogResult.Cancel;
				Close();
				break;
			case PStudent.ePostupovyKlic.OK:
				base.DialogResult = DialogResult.OK;
				Close();
				break;
			default:
				MsgBoxMB.ZobrazitMessageBox(Texty.PostupovyKlic_msgSpatny, Text, eMsgBoxTlacitka.OK);
				break;
			}
		}
		else
		{
			Close();
		}
	}

	private void PostupovyKlic_KeyDown(object sender, KeyEventArgs e)
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

	private void lnkZaslatNaEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
	}

	private void lnkZkopirovatDoSchranky_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Clipboard.SetText(txtPostupovyKlic.Text);
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
		this.lblInfo1 = new System.Windows.Forms.Label();
		this.txtPostupovyKlic = new System.Windows.Forms.TextBox();
		this.cmdStorno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOK1 = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lnkZaslatNaEmail = new System.Windows.Forms.LinkLabel();
		this.cmdOK2 = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblInfo2 = new System.Windows.Forms.Label();
		this.lnkZkopirovatDoSchranky = new System.Windows.Forms.LinkLabel();
		base.SuspendLayout();
		this.lblInfo1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo1.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo1.ForeColor = System.Drawing.Color.Black;
		this.lblInfo1.Location = new System.Drawing.Point(12, 42);
		this.lblInfo1.Name = "lblInfo1";
		this.lblInfo1.Size = new System.Drawing.Size(269, 39);
		this.lblInfo1.TabIndex = 2;
		this.lblInfo1.Text = "???";
		this.txtPostupovyKlic.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.txtPostupovyKlic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtPostupovyKlic.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtPostupovyKlic.Font = new System.Drawing.Font("Courier New", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.txtPostupovyKlic.ForeColor = System.Drawing.Color.Black;
		this.txtPostupovyKlic.Location = new System.Drawing.Point(93, 89);
		this.txtPostupovyKlic.MaxLength = 6;
		this.txtPostupovyKlic.Name = "txtPostupovyKlic";
		this.txtPostupovyKlic.Size = new System.Drawing.Size(101, 31);
		this.txtPostupovyKlic.TabIndex = 0;
		this.txtPostupovyKlic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmdStorno.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdStorno.BackColor = System.Drawing.Color.White;
		this.cmdStorno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdStorno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdStorno.ForeColor = System.Drawing.Color.White;
		this.cmdStorno.Location = new System.Drawing.Point(139, 174);
		this.cmdStorno.Name = "cmdStorno";
		this.cmdStorno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoN;
		this.cmdStorno.Size = new System.Drawing.Size(70, 23);
		this.cmdStorno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoD;
		this.cmdStorno.TabIndex = 5;
		this.cmdStorno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoZ;
		this.cmdStorno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoH;
		this.cmdStorno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdStorno_TlacitkoStisknuto);
		this.cmdOK1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdOK1.BackColor = System.Drawing.Color.White;
		this.cmdOK1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK1.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK1.ForeColor = System.Drawing.Color.White;
		this.cmdOK1.Location = new System.Drawing.Point(73, 174);
		this.cmdOK1.Name = "cmdOK1";
		this.cmdOK1.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK1.Size = new System.Drawing.Size(60, 23);
		this.cmdOK1.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK1.TabIndex = 4;
		this.cmdOK1.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK1.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK1.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.lnkZaslatNaEmail.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lnkZaslatNaEmail.Location = new System.Drawing.Point(12, 125);
		this.lnkZaslatNaEmail.Name = "lnkZaslatNaEmail";
		this.lnkZaslatNaEmail.Size = new System.Drawing.Size(269, 16);
		this.lnkZaslatNaEmail.TabIndex = 1;
		this.lnkZaslatNaEmail.TabStop = true;
		this.lnkZaslatNaEmail.Text = "???";
		this.lnkZaslatNaEmail.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lnkZaslatNaEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkZaslatNaEmail_LinkClicked);
		this.cmdOK2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK2.BackColor = System.Drawing.Color.White;
		this.cmdOK2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK2.ForeColor = System.Drawing.Color.White;
		this.cmdOK2.Location = new System.Drawing.Point(221, 174);
		this.cmdOK2.Name = "cmdOK2";
		this.cmdOK2.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK2.Size = new System.Drawing.Size(60, 23);
		this.cmdOK2.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK2.TabIndex = 6;
		this.cmdOK2.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK2.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK2.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.lblInfo2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo2.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo2.ForeColor = System.Drawing.Color.Black;
		this.lblInfo2.Location = new System.Drawing.Point(12, 125);
		this.lblInfo2.Name = "lblInfo2";
		this.lblInfo2.Size = new System.Drawing.Size(269, 35);
		this.lblInfo2.TabIndex = 3;
		this.lblInfo2.Text = "???";
		this.lnkZkopirovatDoSchranky.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lnkZkopirovatDoSchranky.Location = new System.Drawing.Point(12, 144);
		this.lnkZkopirovatDoSchranky.Name = "lnkZkopirovatDoSchranky";
		this.lnkZkopirovatDoSchranky.Size = new System.Drawing.Size(269, 16);
		this.lnkZkopirovatDoSchranky.TabIndex = 21;
		this.lnkZkopirovatDoSchranky.TabStop = true;
		this.lnkZkopirovatDoSchranky.Text = "???";
		this.lnkZkopirovatDoSchranky.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lnkZkopirovatDoSchranky.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkZkopirovatDoSchranky_LinkClicked);
		base.ClientSize = new System.Drawing.Size(356, 209);
		base.Controls.Add(this.lnkZkopirovatDoSchranky);
		base.Controls.Add(this.cmdOK2);
		base.Controls.Add(this.lnkZaslatNaEmail);
		base.Controls.Add(this.lblInfo1);
		base.Controls.Add(this.txtPostupovyKlic);
		base.Controls.Add(this.cmdStorno);
		base.Controls.Add(this.cmdOK1);
		base.Controls.Add(this.lblInfo2);
		base.KeyPreview = true;
		base.Name = "PostupovyKlic";
		base.Opacity = 0.0;
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(PostupovyKlic_KeyDown);
		base.Controls.SetChildIndex(this.lblInfo2, 0);
		base.Controls.SetChildIndex(this.cmdOK1, 0);
		base.Controls.SetChildIndex(this.cmdStorno, 0);
		base.Controls.SetChildIndex(this.txtPostupovyKlic, 0);
		base.Controls.SetChildIndex(this.lblInfo1, 0);
		base.Controls.SetChildIndex(this.lnkZaslatNaEmail, 0);
		base.Controls.SetChildIndex(this.cmdOK2, 0);
		base.Controls.SetChildIndex(this.lnkZkopirovatDoSchranky, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
