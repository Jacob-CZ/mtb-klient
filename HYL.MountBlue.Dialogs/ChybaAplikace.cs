using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class ChybaAplikace : _Dialog
{
	private const string MailTo = "mailto:{0}";

	private static int iPocetZobrazeni;

	private IContainer components;

	private Label lblInfo1;

	private Label lblDetaily;

	private TextBox txtDetaily;

	private LinkLabel lnkZkopirovat;

	private Label lblInfo2;

	private LinkLabel lnkEmailPodpora;

	private ObrazkoveTlacitko cmdTlZavrit;

	public ChybaAplikace(string detaily)
	{
		InitializeComponent();
		Text = Texty.ChybaAplikace_Title;
		lblInfo1.Text = Texty.ChybaAplikace_lblInfo1;
		lblInfo2.Text = Texty.ChybaAplikace_lblInfo2;
		lblDetaily.Text = Texty.ChybaAplikace_lblDetaily;
		lnkZkopirovat.Text = Texty.ChybaAplikace_lnkZkopirovat;
		lnkEmailPodpora.Text = Texty._EmailPodpora;
		detaily = detaily.Replace("\r\n", "\n");
		detaily = detaily.Replace("\n\n", "\n");
		detaily = detaily.Replace("\r", "\n");
		detaily = detaily.Replace("\n", "\r\n");
		txtDetaily.Text = detaily;
	}

	public static void ZobrazitChybu(Exception ex)
	{
		using (ChybaAplikace chybaAplikace = new ChybaAplikace(ex.Message + "\n\n" + ex.Source + "\n\n" + ex.StackTrace))
		{
			chybaAplikace.ShowDialog();
		}
		if (iPocetZobrazeni++ > 0 && ZobrazitOknoRestartovat(string.Empty))
		{
			Application.Restart();
		}
	}

	public static bool ZobrazitOknoRestartovat(string text)
	{
		return MsgBoxMB.ZobrazitMessageBox(text + Texty.ChybaAplikace_msgRestartovat, Texty.ChybaAplikace_msgRestartovat_Title, eMsgBoxTlacitka.AnoNe) == DialogResult.Yes;
	}

	private void lnkZkopirovat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Clipboard.SetText(txtDetaily.Text);
		lnkZkopirovat.Enabled = false;
	}

	private void lnkEmailPodpora_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start($"mailto:{lnkEmailPodpora.Text}");
	}

	private void cmdTlZavrit_TlacitkoStisknuto()
	{
		Close();
	}

	private void ChybaAplikace_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			cmdTlZavrit_TlacitkoStisknuto();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HYL.MountBlue.Dialogs.ChybaAplikace));
		this.lblInfo1 = new System.Windows.Forms.Label();
		this.lblDetaily = new System.Windows.Forms.Label();
		this.txtDetaily = new System.Windows.Forms.TextBox();
		this.lnkZkopirovat = new System.Windows.Forms.LinkLabel();
		this.lblInfo2 = new System.Windows.Forms.Label();
		this.lnkEmailPodpora = new System.Windows.Forms.LinkLabel();
		this.cmdTlZavrit = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		base.SuspendLayout();
		this.lblInfo1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo1.Location = new System.Drawing.Point(10, 53);
		this.lblInfo1.Name = "lblInfo1";
		this.lblInfo1.Size = new System.Drawing.Size(362, 37);
		this.lblInfo1.TabIndex = 0;
		this.lblInfo1.Text = "???";
		this.lblDetaily.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblDetaily.Location = new System.Drawing.Point(11, 99);
		this.lblDetaily.Name = "lblDetaily";
		this.lblDetaily.Size = new System.Drawing.Size(115, 17);
		this.lblDetaily.TabIndex = 1;
		this.lblDetaily.Text = "???";
		this.txtDetaily.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtDetaily.BackColor = System.Drawing.Color.WhiteSmoke;
		this.txtDetaily.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.txtDetaily.Location = new System.Drawing.Point(14, 121);
		this.txtDetaily.Multiline = true;
		this.txtDetaily.Name = "txtDetaily";
		this.txtDetaily.ReadOnly = true;
		this.txtDetaily.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtDetaily.Size = new System.Drawing.Size(358, 127);
		this.txtDetaily.TabIndex = 0;
		this.lnkZkopirovat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lnkZkopirovat.Location = new System.Drawing.Point(162, 99);
		this.lnkZkopirovat.Name = "lnkZkopirovat";
		this.lnkZkopirovat.Size = new System.Drawing.Size(210, 17);
		this.lnkZkopirovat.TabIndex = 1;
		this.lnkZkopirovat.TabStop = true;
		this.lnkZkopirovat.Text = "???";
		this.lnkZkopirovat.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lnkZkopirovat.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkZkopirovat_LinkClicked);
		this.lblInfo2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo2.Location = new System.Drawing.Point(11, 250);
		this.lblInfo2.Name = "lblInfo2";
		this.lblInfo2.Size = new System.Drawing.Size(361, 33);
		this.lblInfo2.TabIndex = 2;
		this.lblInfo2.Text = "???";
		this.lnkEmailPodpora.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lnkEmailPodpora.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lnkEmailPodpora.Location = new System.Drawing.Point(11, 290);
		this.lnkEmailPodpora.Name = "lnkEmailPodpora";
		this.lnkEmailPodpora.Size = new System.Drawing.Size(193, 17);
		this.lnkEmailPodpora.TabIndex = 2;
		this.lnkEmailPodpora.TabStop = true;
		this.lnkEmailPodpora.Text = "???";
		this.lnkEmailPodpora.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkEmailPodpora_LinkClicked);
		this.cmdTlZavrit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdTlZavrit.BackColor = System.Drawing.Color.White;
		this.cmdTlZavrit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.cmdTlZavrit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdTlZavrit.ForeColor = System.Drawing.Color.Black;
		this.cmdTlZavrit.Location = new System.Drawing.Point(309, 286);
		this.cmdTlZavrit.Name = "cmdTlZavrit";
		this.cmdTlZavrit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdTlZavrit.Size = new System.Drawing.Size(63, 23);
		this.cmdTlZavrit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdTlZavrit.TabIndex = 3;
		this.cmdTlZavrit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdTlZavrit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdTlZavrit.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdTlZavrit_TlacitkoStisknuto);
		base.ClientSize = new System.Drawing.Size(384, 318);
		base.Controls.Add(this.cmdTlZavrit);
		base.Controls.Add(this.lnkEmailPodpora);
		base.Controls.Add(this.lblInfo2);
		base.Controls.Add(this.lnkZkopirovat);
		base.Controls.Add(this.txtDetaily);
		base.Controls.Add(this.lblDetaily);
		base.Controls.Add(this.lblInfo1);
		this.ForeColor = System.Drawing.Color.Black;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.KeyPreview = true;
		base.Name = "ChybaAplikace";
		base.Opacity = 0.9990000128746033;
		base.ShowIcon = true;
		base.ShowInTaskbar = true;
		this.Text = "???";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(ChybaAplikace_KeyDown);
		base.Controls.SetChildIndex(this.lblInfo1, 0);
		base.Controls.SetChildIndex(this.lblDetaily, 0);
		base.Controls.SetChildIndex(this.txtDetaily, 0);
		base.Controls.SetChildIndex(this.lnkZkopirovat, 0);
		base.Controls.SetChildIndex(this.lblInfo2, 0);
		base.Controls.SetChildIndex(this.lnkEmailPodpora, 0);
		base.Controls.SetChildIndex(this.cmdTlZavrit, 0);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
