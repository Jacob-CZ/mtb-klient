using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Forms;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Oaplikaci : _Dialog
{
	private IContainer components;

	private ObrazkoveTlacitko cmdTlZavrit;

	private Label lblIkona;

	private Label lblNazev;

	private Label lblVyrobce;

	private Label lblCopyright;

	private PictureBox picLine2;

	private PictureBox picLine1;

	private Label lblLicence;

	private Label lblNazevLabel;

	private Label lblVyrobceLabel;

	private Label lblCopyrightLabel;

	private LinkLabel lnkWebMB;

	private PictureBox picSplash;

	internal Oaplikaci()
	{
		InitializeComponent();
		Text = Texty.Oaplikaci_Title;
		lblNazevLabel.Text = Texty.Oaplikaci_lblNazev;
		lblVyrobceLabel.Text = Texty.Oaplikaci_lblVyrobce;
		lblCopyrightLabel.Text = Texty.Oaplikaci_lblCopyright;
		lnkWebMB.Text = Texty._wwwMB;
		lblNazev.Text = Application.ProductName + ' ' + Application.ProductVersion;
		lblVyrobce.Text = Application.CompanyName;
		lblCopyright.Text = Texty._Copyright;
		lblLicence.Text = Klient.Stanice.LicenceText;
	}

	internal static void ZobrazitDialog(IWin32Window owner)
	{
		Oaplikaci oaplikaci = new Oaplikaci();
		oaplikaci.ShowDialog(owner);
		oaplikaci.Dispose();
	}

	private void cmdTlZavrit_TlacitkoStisknuto()
	{
		Close();
	}

	private void lnkWebMB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start(Texty._wwwMB);
	}

	private void Oaplikaci_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			cmdTlZavrit_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
	}

	private void picSplash_Paint(object sender, PaintEventArgs e)
	{
		Splash.VykreslitSplash(e.Graphics, picSplash.DisplayRectangle);
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
		this.cmdTlZavrit = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblIkona = new System.Windows.Forms.Label();
		this.lblNazev = new System.Windows.Forms.Label();
		this.lblVyrobce = new System.Windows.Forms.Label();
		this.lblCopyright = new System.Windows.Forms.Label();
		this.picLine2 = new System.Windows.Forms.PictureBox();
		this.picLine1 = new System.Windows.Forms.PictureBox();
		this.lblLicence = new System.Windows.Forms.Label();
		this.lblNazevLabel = new System.Windows.Forms.Label();
		this.lblVyrobceLabel = new System.Windows.Forms.Label();
		this.lblCopyrightLabel = new System.Windows.Forms.Label();
		this.lnkWebMB = new System.Windows.Forms.LinkLabel();
		this.picSplash = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.picLine2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLine1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picSplash).BeginInit();
		base.SuspendLayout();
		this.cmdTlZavrit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdTlZavrit.BackColor = System.Drawing.Color.White;
		this.cmdTlZavrit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
		this.cmdTlZavrit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdTlZavrit.ForeColor = System.Drawing.Color.Black;
		this.cmdTlZavrit.Location = new System.Drawing.Point(435, 516);
		this.cmdTlZavrit.Name = "cmdTlZavrit";
		this.cmdTlZavrit.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdTlZavrit.Size = new System.Drawing.Size(63, 23);
		this.cmdTlZavrit.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdTlZavrit.TabIndex = 0;
		this.cmdTlZavrit.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdTlZavrit.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdTlZavrit.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdTlZavrit_TlacitkoStisknuto);
		this.lblIkona.Image = HYL.MountBlue.Resources.Grafika.pngMountBlue;
		this.lblIkona.Location = new System.Drawing.Point(22, 372);
		this.lblIkona.Name = "lblIkona";
		this.lblIkona.Size = new System.Drawing.Size(64, 64);
		this.lblIkona.TabIndex = 2;
		this.lblNazev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblNazev.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblNazev.ForeColor = System.Drawing.Color.Black;
		this.lblNazev.Location = new System.Drawing.Point(204, 367);
		this.lblNazev.Name = "lblNazev";
		this.lblNazev.Size = new System.Drawing.Size(294, 25);
		this.lblNazev.TabIndex = 4;
		this.lblNazev.Text = "???";
		this.lblNazev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblVyrobce.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblVyrobce.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblVyrobce.ForeColor = System.Drawing.Color.Black;
		this.lblVyrobce.Location = new System.Drawing.Point(204, 394);
		this.lblVyrobce.Name = "lblVyrobce";
		this.lblVyrobce.Size = new System.Drawing.Size(294, 25);
		this.lblVyrobce.TabIndex = 6;
		this.lblVyrobce.Text = "???";
		this.lblVyrobce.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblCopyright.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblCopyright.ForeColor = System.Drawing.Color.Black;
		this.lblCopyright.Location = new System.Drawing.Point(204, 419);
		this.lblCopyright.Name = "lblCopyright";
		this.lblCopyright.Size = new System.Drawing.Size(294, 25);
		this.lblCopyright.TabIndex = 8;
		this.lblCopyright.Text = "???";
		this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.picLine2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.picLine2.BackColor = System.Drawing.Color.Black;
		this.picLine2.Location = new System.Drawing.Point(22, 451);
		this.picLine2.Name = "picLine2";
		this.picLine2.Size = new System.Drawing.Size(473, 1);
		this.picLine2.TabIndex = 27;
		this.picLine2.TabStop = false;
		this.picLine1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.picLine1.BackColor = System.Drawing.Color.Black;
		this.picLine1.Location = new System.Drawing.Point(25, 506);
		this.picLine1.Name = "picLine1";
		this.picLine1.Size = new System.Drawing.Size(473, 1);
		this.picLine1.TabIndex = 26;
		this.picLine1.TabStop = false;
		this.lblLicence.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblLicence.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblLicence.ForeColor = System.Drawing.Color.Black;
		this.lblLicence.Location = new System.Drawing.Point(22, 455);
		this.lblLicence.Name = "lblLicence";
		this.lblLicence.Size = new System.Drawing.Size(476, 49);
		this.lblLicence.TabIndex = 10;
		this.lblLicence.Text = "???";
		this.lblLicence.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblNazevLabel.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblNazevLabel.ForeColor = System.Drawing.Color.Black;
		this.lblNazevLabel.Location = new System.Drawing.Point(94, 372);
		this.lblNazevLabel.Name = "lblNazevLabel";
		this.lblNazevLabel.Size = new System.Drawing.Size(104, 15);
		this.lblNazevLabel.TabIndex = 3;
		this.lblNazevLabel.Text = "???";
		this.lblVyrobceLabel.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblVyrobceLabel.ForeColor = System.Drawing.Color.Black;
		this.lblVyrobceLabel.Location = new System.Drawing.Point(94, 399);
		this.lblVyrobceLabel.Name = "lblVyrobceLabel";
		this.lblVyrobceLabel.Size = new System.Drawing.Size(104, 15);
		this.lblVyrobceLabel.TabIndex = 5;
		this.lblVyrobceLabel.Text = "???";
		this.lblCopyrightLabel.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblCopyrightLabel.ForeColor = System.Drawing.Color.Black;
		this.lblCopyrightLabel.Location = new System.Drawing.Point(94, 424);
		this.lblCopyrightLabel.Name = "lblCopyrightLabel";
		this.lblCopyrightLabel.Size = new System.Drawing.Size(96, 15);
		this.lblCopyrightLabel.TabIndex = 7;
		this.lblCopyrightLabel.Text = "???";
		this.lnkWebMB.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.lnkWebMB.AutoSize = true;
		this.lnkWebMB.Location = new System.Drawing.Point(22, 520);
		this.lnkWebMB.Name = "lnkWebMB";
		this.lnkWebMB.Size = new System.Drawing.Size(28, 15);
		this.lnkWebMB.TabIndex = 1;
		this.lnkWebMB.TabStop = true;
		this.lnkWebMB.Text = "???";
		this.lnkWebMB.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkWebMB_LinkClicked);
		this.picSplash.Location = new System.Drawing.Point(14, 54);
		this.picSplash.Name = "picSplash";
		this.picSplash.Size = new System.Drawing.Size(490, 298);
		this.picSplash.TabIndex = 28;
		this.picSplash.TabStop = false;
		this.picSplash.Paint += new System.Windows.Forms.PaintEventHandler(picSplash_Paint);
		base.ClientSize = new System.Drawing.Size(513, 551);
		base.Controls.Add(this.picSplash);
		base.Controls.Add(this.lnkWebMB);
		base.Controls.Add(this.lblCopyrightLabel);
		base.Controls.Add(this.lblVyrobceLabel);
		base.Controls.Add(this.lblNazevLabel);
		base.Controls.Add(this.lblLicence);
		base.Controls.Add(this.picLine2);
		base.Controls.Add(this.picLine1);
		base.Controls.Add(this.lblCopyright);
		base.Controls.Add(this.lblVyrobce);
		base.Controls.Add(this.lblNazev);
		base.Controls.Add(this.lblIkona);
		base.Controls.Add(this.cmdTlZavrit);
		base.KeyPreview = true;
		base.Name = "Oaplikaci";
		base.Opacity = 0.9990000128746033;
		this.Text = "???";
		base.VyskaZahlavi = 40;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Oaplikaci_KeyDown);
		base.Controls.SetChildIndex(this.cmdTlZavrit, 0);
		base.Controls.SetChildIndex(this.lblIkona, 0);
		base.Controls.SetChildIndex(this.lblNazev, 0);
		base.Controls.SetChildIndex(this.lblVyrobce, 0);
		base.Controls.SetChildIndex(this.lblCopyright, 0);
		base.Controls.SetChildIndex(this.picLine1, 0);
		base.Controls.SetChildIndex(this.picLine2, 0);
		base.Controls.SetChildIndex(this.lblLicence, 0);
		base.Controls.SetChildIndex(this.lblNazevLabel, 0);
		base.Controls.SetChildIndex(this.lblVyrobceLabel, 0);
		base.Controls.SetChildIndex(this.lblCopyrightLabel, 0);
		base.Controls.SetChildIndex(this.lnkWebMB, 0);
		base.Controls.SetChildIndex(this.picSplash, 0);
		((System.ComponentModel.ISupportInitialize)this.picLine2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLine1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picSplash).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
