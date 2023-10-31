using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class PresunNaTabor : _Dialog
{
	private struct LekcePolozka
	{
		public int CisloLekce;

		public string Popis;

		public override string ToString()
		{
			return Popis;
		}
	}

	private StudentSkolni student;

	private IContainer components;

	private RadioButton optPresunVpred;

	private RadioButton optPresunZpet;

	private ComboBox lstLekce;

	private ObrazkoveTlacitko cmdStorno;

	private ObrazkoveTlacitko cmdOK;

	private PictureBox picLine;

	public PresunNaTabor(StudentSkolni stu)
	{
		InitializeComponent();
		student = stu;
		Text = Texty.PresunNaTabor_Title;
		optPresunZpet.Text = Texty.PresunNaTabor_optZpet;
		NacistLekce();
		bool jeNaMaxCviceni = student.JeNaMaxCviceni;
		optPresunZpet.Enabled = jeNaMaxCviceni;
		optPresunVpred.Enabled = !jeNaMaxCviceni;
		if (optPresunZpet.Enabled)
		{
			optPresunZpet.Checked = true;
		}
		if (optPresunVpred.Enabled)
		{
			optPresunVpred.Checked = true;
		}
		if (jeNaMaxCviceni)
		{
			optPresunVpred.Text = Texty.PresunNaTabor_optVpred;
		}
		else
		{
			optPresunVpred.Text = string.Format(Texty.PresunNaTabor_optVpred0, student.CviceniMax.Lekce);
		}
		optPresun_CheckedChanged(null, null);
	}

	private void NacistLekce()
	{
		lstLekce.Items.Clear();
		Klavesy.sLekceKlavesy[] array = _Lekce.Lekce().Klavesy.KlavesyDoArrayListu();
		Klavesy.sLekceKlavesy[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Klavesy.sLekceKlavesy sLekceKlavesy = array2[i];
			if (sLekceKlavesy.Lekce < student.Cviceni.Lekce)
			{
				LekcePolozka lekcePolozka = default(LekcePolozka);
				lekcePolozka.CisloLekce = sLekceKlavesy.Lekce;
				lekcePolozka.Popis = string.Format(Texty.PresunNaTabor_lstLekce_pol, sLekceKlavesy.Lekce, sLekceKlavesy.Text);
				lstLekce.Items.Add(lekcePolozka);
			}
		}
		if (lstLekce.Items.Count > 0)
		{
			lstLekce.SelectedIndex = 0;
		}
	}

	public static void ZobrazitPresunoutNaTabor(IWin32Window owner, StudentSkolni stu)
	{
		using PresunNaTabor presunNaTabor = new PresunNaTabor(stu);
		presunNaTabor.ShowDialog(owner);
	}

	private void cmdStorno_TlacitkoStisknuto()
	{
		Close();
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		if (student.JeUzivatelPrihlaseny)
		{
			MsgBoxBublina.ZobrazitMessageBox(Texty.Uzivatel_msgNelzePresunout, Text, eMsgBoxTlacitka.OK);
		}
		else if (optPresunZpet.Checked)
		{
			if (lstLekce.SelectedIndex < 0)
			{
				return;
			}
			int cisloLekce = ((LekcePolozka)lstLekce.SelectedItem).CisloLekce;
			student.NastavitCviceniRucne(new OznaceniCviceni(cisloLekce));
		}
		else if (optPresunVpred.Checked)
		{
			student.NastavitCviceniRucne(student.CviceniMax);
		}
		Close();
	}

	private void PresunNaTabor_KeyDown(object sender, KeyEventArgs e)
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

	private void optPresun_CheckedChanged(object sender, EventArgs e)
	{
		lstLekce.Enabled = optPresunZpet.Checked;
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
		this.optPresunVpred = new System.Windows.Forms.RadioButton();
		this.optPresunZpet = new System.Windows.Forms.RadioButton();
		this.lstLekce = new System.Windows.Forms.ComboBox();
		this.cmdStorno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.picLine = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.picLine).BeginInit();
		base.SuspendLayout();
		this.optPresunVpred.AutoSize = true;
		this.optPresunVpred.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.optPresunVpred.Location = new System.Drawing.Point(19, 112);
		this.optPresunVpred.Name = "optPresunVpred";
		this.optPresunVpred.Size = new System.Drawing.Size(45, 19);
		this.optPresunVpred.TabIndex = 23;
		this.optPresunVpred.TabStop = true;
		this.optPresunVpred.Text = "???";
		this.optPresunVpred.UseVisualStyleBackColor = true;
		this.optPresunVpred.CheckedChanged += new System.EventHandler(optPresun_CheckedChanged);
		this.optPresunZpet.AutoSize = true;
		this.optPresunZpet.Checked = true;
		this.optPresunZpet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.optPresunZpet.Location = new System.Drawing.Point(19, 47);
		this.optPresunZpet.Name = "optPresunZpet";
		this.optPresunZpet.Size = new System.Drawing.Size(45, 19);
		this.optPresunZpet.TabIndex = 21;
		this.optPresunZpet.TabStop = true;
		this.optPresunZpet.Text = "???";
		this.optPresunZpet.UseVisualStyleBackColor = true;
		this.optPresunZpet.CheckedChanged += new System.EventHandler(optPresun_CheckedChanged);
		this.lstLekce.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lstLekce.BackColor = System.Drawing.Color.Gainsboro;
		this.lstLekce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lstLekce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lstLekce.FormattingEnabled = true;
		this.lstLekce.Location = new System.Drawing.Point(46, 72);
		this.lstLekce.Name = "lstLekce";
		this.lstLekce.Size = new System.Drawing.Size(278, 23);
		this.lstLekce.TabIndex = 22;
		this.cmdStorno.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdStorno.BackColor = System.Drawing.Color.White;
		this.cmdStorno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdStorno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdStorno.ForeColor = System.Drawing.Color.White;
		this.cmdStorno.Location = new System.Drawing.Point(283, 149);
		this.cmdStorno.Name = "cmdStorno";
		this.cmdStorno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoN;
		this.cmdStorno.Size = new System.Drawing.Size(70, 23);
		this.cmdStorno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoD;
		this.cmdStorno.TabIndex = 40;
		this.cmdStorno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoZ;
		this.cmdStorno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoH;
		this.cmdStorno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdStorno_TlacitkoStisknuto);
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(217, 149);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 39;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.picLine.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.picLine.BackColor = System.Drawing.Color.Black;
		this.picLine.Location = new System.Drawing.Point(12, 143);
		this.picLine.Name = "picLine";
		this.picLine.Size = new System.Drawing.Size(346, 1);
		this.picLine.TabIndex = 41;
		this.picLine.TabStop = false;
		base.ClientSize = new System.Drawing.Size(370, 184);
		base.Controls.Add(this.picLine);
		base.Controls.Add(this.cmdStorno);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.optPresunVpred);
		base.Controls.Add(this.optPresunZpet);
		base.Controls.Add(this.lstLekce);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "PresunNaTabor";
		base.Opacity = 0.9990000128746033;
		this.Text = "???";
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(PresunNaTabor_KeyDown);
		base.Controls.SetChildIndex(this.lstLekce, 0);
		base.Controls.SetChildIndex(this.optPresunZpet, 0);
		base.Controls.SetChildIndex(this.optPresunVpred, 0);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.cmdStorno, 0);
		base.Controls.SetChildIndex(this.picLine, 0);
		((System.ComponentModel.ISupportInitialize)this.picLine).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
