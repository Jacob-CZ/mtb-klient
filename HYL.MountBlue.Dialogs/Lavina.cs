using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Lavina : _Bublina
{
	private Ucitel ucitel;

	private StudentSkolni student;

	private int iTridaID;

	private IContainer components;

	private ObrazkoveTlacitko cmdZavrit2;

	private Label lblInfoUcitel;

	private PictureBox picLavina;

	private ObrazkoveTlacitko cmdZastavitVystup;

	private ObrazkoveTlacitko cmdOdvolatLavinu;

	private Label lblInfoStudent;

	private Timer tmrKontrola;

	public Lavina()
	{
		InitializeComponent();
		cmdZavrit2.BackColor = Barva.PozadiTlacitkaZavrit;
		Text = Texty.Lavina_Title;
		lblInfoUcitel.Text = Texty.Lavina_lblInfoUcitel;
		lblInfoStudent.Text = Texty.Lavina_lblInfoStudent;
	}

	public Lavina(Ucitel ucitel, int tridaID)
		: this()
	{
		this.ucitel = ucitel;
		iTridaID = tridaID;
		cmdZastavitVystup.Visible = true;
		cmdOdvolatLavinu.Visible = true;
		lblInfoUcitel.Visible = true;
		lblInfoStudent.Visible = false;
		ObnovitTlacitka();
		tmrKontrola.Stop();
	}

	public Lavina(StudentSkolni student)
		: this()
	{
		this.student = student;
		cmdZastavitVystup.Visible = false;
		cmdOdvolatLavinu.Visible = false;
		lblInfoUcitel.Visible = false;
		lblInfoStudent.Visible = true;
		tmrKontrola.Start();
	}

	private void Lavina_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Alt && e.KeyCode == Keys.F4)
		{
			e.SuppressKeyPress = true;
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.Escape)
		{
			e.SuppressKeyPress = true;
			e.Handled = true;
			cmdZavrit2_TlacitkoStisknuto();
		}
	}

	internal static void ZobrazitLavinu(Ucitel ucitel, int tridaID)
	{
		using Lavina lavina = new Lavina(ucitel, tridaID);
		lavina.BublinaZobrazitDialog();
	}

	internal static void ZobrazitLavinu(StudentSkolni student)
	{
		using Lavina lavina = new Lavina(student);
		lavina.BublinaZobrazitDialog();
	}

	private void cmdZastavitVystup_TlacitkoStisknuto()
	{
		if (ucitel != null)
		{
			ucitel.SpustitLavinu(iTridaID);
		}
		ObnovitTlacitka();
	}

	private void cmdOdvolatLavinu_TlacitkoStisknuto()
	{
		if (ucitel != null)
		{
			ucitel.ZastavitLavinu();
		}
		ObnovitTlacitka();
		Close();
	}

	private void tmrKontrola_Tick(object sender, EventArgs e)
	{
		if (student != null && !student.JeSpustenaLavina())
		{
			Close();
		}
	}

	private void ObnovitTlacitka()
	{
		if (ucitel != null)
		{
			cmdZastavitVystup.Enabled = !ucitel.JeSpustenaLavina(iTridaID);
			cmdOdvolatLavinu.Enabled = !cmdZastavitVystup.Enabled;
			if (cmdZastavitVystup.Enabled)
			{
				cmdZastavitVystup.Focus();
			}
			else if (cmdOdvolatLavinu.Enabled)
			{
				cmdOdvolatLavinu.Focus();
			}
		}
	}

	private void cmdZavrit2_TlacitkoStisknuto()
	{
		Close();
	}

	private void Lavina_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (student != null && student.JeSpustenaLavina())
		{
			MsgBoxMB.ZobrazitMessageBox(Texty.Lavina_msgStaleHrozi, Text, eMsgBoxTlacitka.OK);
			e.Cancel = true;
		}
		else if (ucitel != null && ucitel.JeSpustenaLavina(iTridaID))
		{
			DialogResult dialogResult = MsgBoxMB.ZobrazitMessageBox(Texty.Lavina_msgOdvolat, Texty.Lavina_msgOdvolat_Title, eMsgBoxTlacitka.AnoNe);
			if (dialogResult == DialogResult.Yes)
			{
				cmdOdvolatLavinu_TlacitkoStisknuto();
			}
			else
			{
				e.Cancel = true;
			}
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
		this.components = new System.ComponentModel.Container();
		this.cmdZavrit2 = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblInfoUcitel = new System.Windows.Forms.Label();
		this.picLavina = new System.Windows.Forms.PictureBox();
		this.cmdZastavitVystup = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOdvolatLavinu = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblInfoStudent = new System.Windows.Forms.Label();
		this.tmrKontrola = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)this.picLavina).BeginInit();
		base.SuspendLayout();
		this.cmdZavrit2.BackColor = System.Drawing.Color.Red;
		this.cmdZavrit2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZavrit2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZavrit2.ForeColor = System.Drawing.Color.White;
		this.cmdZavrit2.Location = new System.Drawing.Point(448, 12);
		this.cmdZavrit2.Name = "cmdZavrit2";
		this.cmdZavrit2.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavN;
		this.cmdZavrit2.Size = new System.Drawing.Size(24, 24);
		this.cmdZavrit2.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavD;
		this.cmdZavrit2.TabIndex = 20;
		this.cmdZavrit2.TabStop = false;
		this.cmdZavrit2.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavZ;
		this.cmdZavrit2.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlZavH;
		this.cmdZavrit2.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZavrit2_TlacitkoStisknuto);
		this.lblInfoUcitel.Location = new System.Drawing.Point(181, 66);
		this.lblInfoUcitel.Name = "lblInfoUcitel";
		this.lblInfoUcitel.Size = new System.Drawing.Size(275, 146);
		this.lblInfoUcitel.TabIndex = 0;
		this.lblInfoUcitel.Text = "???";
		this.picLavina.Image = HYL.MountBlue.Resources.GrafikaSkolni.pngLavinaCedule;
		this.picLavina.Location = new System.Drawing.Point(12, 47);
		this.picLavina.Name = "picLavina";
		this.picLavina.Size = new System.Drawing.Size(149, 252);
		this.picLavina.TabIndex = 22;
		this.picLavina.TabStop = false;
		this.cmdZastavitVystup.BackColor = System.Drawing.Color.White;
		this.cmdZastavitVystup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZastavitVystup.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZastavitVystup.ForeColor = System.Drawing.Color.White;
		this.cmdZastavitVystup.Location = new System.Drawing.Point(210, 235);
		this.cmdZastavitVystup.Name = "cmdZastavitVystup";
		this.cmdZastavitVystup.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlLavina1N;
		this.cmdZastavitVystup.Size = new System.Drawing.Size(106, 38);
		this.cmdZastavitVystup.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlLavina1D;
		this.cmdZastavitVystup.TabIndex = 2;
		this.cmdZastavitVystup.ZakazanyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlLavina1Z;
		this.cmdZastavitVystup.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlLavina1H;
		this.cmdZastavitVystup.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZastavitVystup_TlacitkoStisknuto);
		this.cmdOdvolatLavinu.BackColor = System.Drawing.Color.White;
		this.cmdOdvolatLavinu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOdvolatLavinu.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOdvolatLavinu.ForeColor = System.Drawing.Color.White;
		this.cmdOdvolatLavinu.Location = new System.Drawing.Point(322, 235);
		this.cmdOdvolatLavinu.Name = "cmdOdvolatLavinu";
		this.cmdOdvolatLavinu.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlLavina2N;
		this.cmdOdvolatLavinu.Size = new System.Drawing.Size(106, 38);
		this.cmdOdvolatLavinu.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlLavina2D;
		this.cmdOdvolatLavinu.TabIndex = 3;
		this.cmdOdvolatLavinu.ZakazanyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlLavina2Z;
		this.cmdOdvolatLavinu.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlLavina2H;
		this.cmdOdvolatLavinu.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOdvolatLavinu_TlacitkoStisknuto);
		this.lblInfoStudent.Font = new System.Drawing.Font("Arial", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfoStudent.Location = new System.Drawing.Point(181, 66);
		this.lblInfoStudent.Name = "lblInfoStudent";
		this.lblInfoStudent.Size = new System.Drawing.Size(275, 207);
		this.lblInfoStudent.TabIndex = 1;
		this.lblInfoStudent.Text = "???";
		this.lblInfoStudent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.tmrKontrola.Interval = 15000;
		this.tmrKontrola.Tick += new System.EventHandler(tmrKontrola_Tick);
		base.ClientSize = new System.Drawing.Size(549, 301);
		base.Controls.Add(this.cmdOdvolatLavinu);
		base.Controls.Add(this.cmdZastavitVystup);
		base.Controls.Add(this.picLavina);
		base.Controls.Add(this.lblInfoUcitel);
		base.Controls.Add(this.lblInfoStudent);
		base.Controls.Add(this.cmdZavrit2);
		base.KeyPreview = true;
		base.Name = "Lavina";
		base.Opacity = 0.0;
		this.Text = "Pozor, lavina!";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Lavina_FormClosing);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Lavina_KeyDown);
		base.Controls.SetChildIndex(this.cmdZavrit2, 0);
		base.Controls.SetChildIndex(this.lblInfoStudent, 0);
		base.Controls.SetChildIndex(this.lblInfoUcitel, 0);
		base.Controls.SetChildIndex(this.picLavina, 0);
		base.Controls.SetChildIndex(this.cmdZastavitVystup, 0);
		base.Controls.SetChildIndex(this.cmdOdvolatLavinu, 0);
		((System.ComponentModel.ISupportInitialize)this.picLavina).EndInit();
		base.ResumeLayout(false);
	}
}
