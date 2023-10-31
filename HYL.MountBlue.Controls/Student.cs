using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

public class Student : UserControl
{
	private struct KlasifikaceTTT
	{
		public string Nadpis;

		public string Popis;

		public override string ToString()
		{
			return Nadpis + "\n\n" + Popis;
		}
	}

	private const int MaxPocetHvezdicek = 8;

	private const int MaxPocetKlasifikaci = 26;

	private StudentSkolni student;

	private Trida trida;

	private KlasifPolozka[] klasifikace;

	private List<KlasifikaceTTT> klasifikaceTTT;

	private bool bVykricnik;

	private IContainer components;

	private LinkLabel lnkJmeno;

	private Panel pnlObecne;

	private Label lblPoznamka;

	private Label lblVelkeOdmeny;

	private Label lblLekce;

	private PictureBox picVelkeOdmeny;

	private LinkLabel lnkUpravit;

	private Panel pnlKlasifikace;

	private PictureBox picVlajka;

	private ObrazkoveTlacitko cmdOpakovat;

	private ObrazkoveTlacitko cmdPovolit;

	private ObrazkoveTlacitko cmdZakazat;

	private ToolTip ttt;

	private PictureBox picKlasifikace;

	private Label lblVyprsi;

	public Student()
	{
		InitializeComponent();
		klasifikaceTTT = new List<KlasifikaceTTT>();
		ToolTipText.NastavitToolTipText(ttt);
		lnkUpravit.Text = Texty.Student_lnkUpravit;
		ZobrazitZalozku(ZalozkyTrida.eZalozky.SeznamStudentu);
	}

	public Student(StudentSkolni stu, Trida tr)
		: this()
	{
		student = stu;
		trida = tr;
		Obnovit();
	}

	public void Obnovit()
	{
		lnkJmeno.Text = student.CelePrijmeniAjmeno;
		picVlajka.Visible = student.VyukaDokoncena;
		lblLekce.Visible = !picVlajka.Visible;
		lblLekce.Text = student.Cviceni.Lekce.ToString();
		int num = student.PocetVelkychOdmen;
		lblVelkeOdmeny.Text = num.ToString();
		if (num > 8)
		{
			num = 8;
		}
		picVelkeOdmeny.Width = picVelkeOdmeny.Height * num;
		StringBuilder stringBuilder = new StringBuilder();
		if (student.VystupDokoncen)
		{
			stringBuilder.Append(string.Format(Texty.Student_vystupDokoncen, student.DatumDokonceni));
		}
		else if (!student.JeNaMaxCviceni)
		{
			stringBuilder.Append(string.Format(Texty.Student_vracenZpet, student.CviceniMax.Lekce, student.Cviceni.Lekce));
		}
		if (!student.Aktivni)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(Texty.Student_carka);
			}
			stringBuilder.Append(Texty.Sprava_uzivNeaktivni);
		}
		lblPoznamka.Text = stringBuilder.ToString();
		klasifikace = _Lekce.Lekce().Klasifikace.SeznamKlasifikaci(student);
		bVykricnik = false;
		bool klasifikacePovolena = student.KlasifikacePovolena;
		bool flag = false;
		bool flag2 = false;
		klasifikaceTTT.Clear();
		KlasifPolozka[] array = klasifikace;
		foreach (KlasifPolozka klasifPolozka in array)
		{
			KlasifikaceTTT item = default(KlasifikaceTTT);
			item.Nadpis = string.Format(Texty.Student_klasifikaceXlekce, klasifPolozka.CviceniPsani.KlasifikaceOd - 1);
			switch (klasifPolozka.Stav)
			{
			case KlasifPolozka.eStav.neaktivni:
				item.Popis = Texty.Student_klasifikaceNeaktivni;
				break;
			case KlasifPolozka.eStav.nesplneno:
				flag = true;
				item.Popis = Texty.Student_klasifikaceNesplneno;
				break;
			case KlasifPolozka.eStav.nesplnenoUrgentni:
				flag = true;
				bVykricnik = true;
				item.Popis = Texty.Student_klasifikaceNesplnenoUrgentni;
				break;
			case KlasifPolozka.eStav.splneno:
				flag2 = true;
				item.Popis = Texty.Student_klasifikaceSplneno;
				break;
			case KlasifPolozka.eStav.moznostVyuzitOdmeny:
				flag = true;
				item.Popis = Texty.Student_klasifikaceMoznostOdmeny;
				break;
			case KlasifPolozka.eStav.nuceneOpakovani:
				item.Popis = Texty.Student_klasifikaceOpakovani;
				break;
			default:
				item.Popis = string.Empty;
				break;
			}
			klasifikaceTTT.Add(item);
		}
		bool jeUzivatelPrihlaseny = student.JeUzivatelPrihlaseny;
		if (jeUzivatelPrihlaseny)
		{
			pnlObecne.BackColor = Barva.ZaznamStudentPrihlaseny;
			pnlKlasifikace.BackColor = Barva.ZaznamStudentPrihlaseny;
			lnkJmeno.LinkColor = Barva.StudentPrihlaseny;
			lnkUpravit.LinkColor = Barva.StudentPrihlaseny;
		}
		else
		{
			pnlObecne.BackColor = Barva.ZaznamStudentNeprihlaseny;
			pnlKlasifikace.BackColor = Barva.ZaznamStudentNeprihlaseny;
			lnkJmeno.LinkColor = Barva.StudentNeprihlaseny;
			lnkUpravit.LinkColor = Barva.StudentNeprihlaseny;
		}
		cmdOpakovat.Visible = jeUzivatelPrihlaseny && !klasifikacePovolena && flag2;
		cmdPovolit.Visible = jeUzivatelPrihlaseny && !klasifikacePovolena && flag;
		cmdZakazat.Visible = jeUzivatelPrihlaseny && klasifikacePovolena && !student.KlasifikaciPravePise;
		lblVyprsi.Visible = jeUzivatelPrihlaseny && klasifikacePovolena;
		if (lblVyprsi.Visible)
		{
			if (cmdZakazat.Visible)
			{
				string arg = student.KlasifikacePovolenaDo.ToString("t");
				lblVyprsi.Text = string.Format(Texty.Student_lblVyprsi, arg);
				ttt.SetToolTip(lblVyprsi, string.Format(Texty.Student_lblVyprsi_ttt, arg, 20));
			}
			else
			{
				ttt.RemoveAll();
				lblVyprsi.Text = Texty.Student_lblVyprsi_pise;
			}
		}
		else
		{
			ttt.RemoveAll();
		}
		picKlasifikace.Invalidate();
	}

	public void ZobrazitZalozku(ZalozkyTrida.eZalozky zalozka)
	{
		switch (zalozka)
		{
		case ZalozkyTrida.eZalozky.SeznamStudentu:
			pnlObecne.Visible = true;
			pnlKlasifikace.Visible = false;
			break;
		case ZalozkyTrida.eZalozky.Klasifikace:
			pnlObecne.Visible = false;
			pnlKlasifikace.Visible = true;
			break;
		}
	}

	private void lnkJmeno_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		trida.ZobrazitHistoriiEvent(student.UID);
	}

	private void lnkUpravit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		PUzivatel pUzivatel = PUzivatele.UzivatelPodleTypu(student);
		pUzivatel.ZobrazitUpravitUzivatele();
		Obnovit();
	}

	private void cmdPovolit_TlacitkoStisknuto()
	{
		student.KlasifikacePovolit();
		Obnovit();
	}

	private void cmdOpakovat_TlacitkoStisknuto()
	{
		OpakovatKlasifikaci.ZobrazitOpakovatKlasifikaci(this, student);
		Obnovit();
	}

	private void picKlasifikace_Paint(object sender, PaintEventArgs e)
	{
		if (klasifikace == null)
		{
			return;
		}
		Graphics graphics = e.Graphics;
		graphics.Clear(picKlasifikace.BackColor);
		Font font = new Font("Arial", 8f, FontStyle.Bold);
		StringFormat stringFormat = new StringFormat();
		stringFormat.LineAlignment = StringAlignment.Center;
		stringFormat.Alignment = StringAlignment.Center;
		int num = 0;
		KlasifPolozka[] array = klasifikace;
		foreach (KlasifPolozka klasifPolozka in array)
		{
			Color color = Color.Empty;
			Color color2 = Color.Empty;
			switch (klasifPolozka.Stav)
			{
			case KlasifPolozka.eStav.nesplneno:
			case KlasifPolozka.eStav.nesplnenoUrgentni:
				color = Color.Red;
				break;
			case KlasifPolozka.eStav.splneno:
				color = Color.DarkGreen;
				color2 = Color.White;
				break;
			case KlasifPolozka.eStav.moznostVyuzitOdmeny:
				color = Color.LimeGreen;
				color2 = Color.Black;
				break;
			case KlasifPolozka.eStav.nuceneOpakovani:
				color = Color.Yellow;
				color2 = Color.Black;
				break;
			}
			Rectangle rectangle = new Rectangle(num++ * 13 + 1, 4, 12, 16);
			if (num <= 26)
			{
				if (color != Color.Empty)
				{
					graphics.FillRectangle(new SolidBrush(color), rectangle);
				}
				if (color2 != Color.Empty)
				{
					graphics.DrawString(klasifPolozka.Znamka.ToString(), font, new SolidBrush(color2), rectangle, stringFormat);
				}
			}
		}
		graphics.DrawImage(GrafikaSkolni.pngKlasifikace, new Rectangle(new Point(0, 0), GrafikaSkolni.pngKlasifikace.Size));
		if (bVykricnik)
		{
			graphics.DrawIcon(Grafika.icoVykricnik, 340, 3);
		}
	}

	private void cmdZakazat_TlacitkoStisknuto()
	{
		if (student.KlasifikaciPravePise)
		{
			MsgBoxBublina.ZobrazitMessageBox(Texty.Student_msgKlasifikaciNelzeZrusit, Texty.Student_msgKlasifikaciNelzeZrusit_Title, eMsgBoxTlacitka.OK);
		}
		else
		{
			student.KlasifikaceZakazat();
		}
		Obnovit();
	}

	private void picKlasifikace_MouseDown(object sender, MouseEventArgs e)
	{
		int num = e.Location.X / 13;
		ttt.Hide(picKlasifikace);
		ttt.Show(string.Empty, picKlasifikace, new Point(0, 0), 1);
		ttt.Hide(picKlasifikace);
		if (num < klasifikaceTTT.Count)
		{
			ttt.Show(klasifikaceTTT[num].ToString(), picKlasifikace, new Point(num * 13 + 6, 12), 4000);
		}
		else if (bVykricnik && e.Location.X >= 340)
		{
			ttt.Show(Texty.Student_klasifikaceNesplnenoUrgentni, picKlasifikace, new Point(348, 12), 4000);
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
		this.lnkJmeno = new System.Windows.Forms.LinkLabel();
		this.pnlObecne = new System.Windows.Forms.Panel();
		this.lnkUpravit = new System.Windows.Forms.LinkLabel();
		this.picVelkeOdmeny = new System.Windows.Forms.PictureBox();
		this.lblPoznamka = new System.Windows.Forms.Label();
		this.lblVelkeOdmeny = new System.Windows.Forms.Label();
		this.lblLekce = new System.Windows.Forms.Label();
		this.pnlKlasifikace = new System.Windows.Forms.Panel();
		this.lblVyprsi = new System.Windows.Forms.Label();
		this.picKlasifikace = new System.Windows.Forms.PictureBox();
		this.cmdZakazat = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdPovolit = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOpakovat = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.ttt = new System.Windows.Forms.ToolTip(this.components);
		this.picVlajka = new System.Windows.Forms.PictureBox();
		this.pnlObecne.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picVelkeOdmeny).BeginInit();
		this.pnlKlasifikace.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.picKlasifikace).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picVlajka).BeginInit();
		base.SuspendLayout();
		this.lnkJmeno.AutoEllipsis = true;
		this.lnkJmeno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lnkJmeno.LinkColor = System.Drawing.Color.Blue;
		this.lnkJmeno.Location = new System.Drawing.Point(3, 4);
		this.lnkJmeno.Name = "lnkJmeno";
		this.lnkJmeno.Size = new System.Drawing.Size(159, 20);
		this.lnkJmeno.TabIndex = 0;
		this.lnkJmeno.TabStop = true;
		this.lnkJmeno.Text = "???";
		this.lnkJmeno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lnkJmeno.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkJmeno_LinkClicked);
		this.pnlObecne.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.pnlObecne.BackColor = System.Drawing.Color.WhiteSmoke;
		this.pnlObecne.Controls.Add(this.lnkUpravit);
		this.pnlObecne.Controls.Add(this.picVelkeOdmeny);
		this.pnlObecne.Controls.Add(this.lblPoznamka);
		this.pnlObecne.Controls.Add(this.lblVelkeOdmeny);
		this.pnlObecne.Location = new System.Drawing.Point(209, 2);
		this.pnlObecne.Name = "pnlObecne";
		this.pnlObecne.Size = new System.Drawing.Size(524, 25);
		this.pnlObecne.TabIndex = 1;
		this.lnkUpravit.AutoSize = true;
		this.lnkUpravit.Location = new System.Drawing.Point(222, 5);
		this.lnkUpravit.Name = "lnkUpravit";
		this.lnkUpravit.Size = new System.Drawing.Size(25, 14);
		this.lnkUpravit.TabIndex = 8;
		this.lnkUpravit.TabStop = true;
		this.lnkUpravit.Text = "???";
		this.lnkUpravit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkUpravit_LinkClicked);
		this.picVelkeOdmeny.BackgroundImage = HYL.MountBlue.Resources.Grafika.pngVelkaHvezda;
		this.picVelkeOdmeny.Location = new System.Drawing.Point(41, 1);
		this.picVelkeOdmeny.Name = "picVelkeOdmeny";
		this.picVelkeOdmeny.Size = new System.Drawing.Size(177, 22);
		this.picVelkeOdmeny.TabIndex = 7;
		this.picVelkeOdmeny.TabStop = false;
		this.lblPoznamka.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblPoznamka.AutoEllipsis = true;
		this.lblPoznamka.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblPoznamka.Location = new System.Drawing.Point(283, 4);
		this.lblPoznamka.Name = "lblPoznamka";
		this.lblPoznamka.Size = new System.Drawing.Size(237, 17);
		this.lblPoznamka.TabIndex = 6;
		this.lblPoznamka.Text = "???";
		this.lblPoznamka.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblVelkeOdmeny.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblVelkeOdmeny.Location = new System.Drawing.Point(3, 2);
		this.lblVelkeOdmeny.Name = "lblVelkeOdmeny";
		this.lblVelkeOdmeny.Size = new System.Drawing.Size(35, 21);
		this.lblVelkeOdmeny.TabIndex = 5;
		this.lblVelkeOdmeny.Text = "???";
		this.lblVelkeOdmeny.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblLekce.Font = new System.Drawing.Font("Arial", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblLekce.Location = new System.Drawing.Point(167, 4);
		this.lblLekce.Name = "lblLekce";
		this.lblLekce.Size = new System.Drawing.Size(37, 21);
		this.lblLekce.TabIndex = 4;
		this.lblLekce.Text = "???";
		this.lblLekce.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.pnlKlasifikace.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.pnlKlasifikace.BackColor = System.Drawing.Color.FromArgb(186, 231, 247);
		this.pnlKlasifikace.Controls.Add(this.lblVyprsi);
		this.pnlKlasifikace.Controls.Add(this.picKlasifikace);
		this.pnlKlasifikace.Controls.Add(this.cmdZakazat);
		this.pnlKlasifikace.Controls.Add(this.cmdPovolit);
		this.pnlKlasifikace.Controls.Add(this.cmdOpakovat);
		this.pnlKlasifikace.Location = new System.Drawing.Point(209, 2);
		this.pnlKlasifikace.Name = "pnlKlasifikace";
		this.pnlKlasifikace.Size = new System.Drawing.Size(525, 25);
		this.pnlKlasifikace.TabIndex = 2;
		this.lblVyprsi.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblVyprsi.Location = new System.Drawing.Point(358, 3);
		this.lblVyprsi.Name = "lblVyprsi";
		this.lblVyprsi.Size = new System.Drawing.Size(115, 18);
		this.lblVyprsi.TabIndex = 11;
		this.lblVyprsi.Text = "???";
		this.lblVyprsi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.picKlasifikace.Cursor = System.Windows.Forms.Cursors.Hand;
		this.picKlasifikace.Location = new System.Drawing.Point(2, 1);
		this.picKlasifikace.Name = "picKlasifikace";
		this.picKlasifikace.Size = new System.Drawing.Size(357, 23);
		this.picKlasifikace.TabIndex = 10;
		this.picKlasifikace.TabStop = false;
		this.picKlasifikace.MouseDown += new System.Windows.Forms.MouseEventHandler(picKlasifikace_MouseDown);
		this.picKlasifikace.Paint += new System.Windows.Forms.PaintEventHandler(picKlasifikace_Paint);
		this.cmdZakazat.BackColor = System.Drawing.Color.FromArgb(186, 231, 247);
		this.cmdZakazat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdZakazat.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdZakazat.ForeColor = System.Drawing.Color.White;
		this.cmdZakazat.Location = new System.Drawing.Point(476, 1);
		this.cmdZakazat.Name = "cmdZakazat";
		this.cmdZakazat.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlZakazatN;
		this.cmdZakazat.Size = new System.Drawing.Size(48, 23);
		this.cmdZakazat.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlZakazatD;
		this.cmdZakazat.TabIndex = 8;
		this.cmdZakazat.ZakazanyObrazek = null;
		this.cmdZakazat.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlZakazatH;
		this.cmdZakazat.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdZakazat_TlacitkoStisknuto);
		this.cmdPovolit.BackColor = System.Drawing.Color.FromArgb(186, 231, 247);
		this.cmdPovolit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdPovolit.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdPovolit.ForeColor = System.Drawing.Color.White;
		this.cmdPovolit.Location = new System.Drawing.Point(361, 1);
		this.cmdPovolit.Name = "cmdPovolit";
		this.cmdPovolit.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlPovolitN;
		this.cmdPovolit.Size = new System.Drawing.Size(48, 23);
		this.cmdPovolit.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlPovolitD;
		this.cmdPovolit.TabIndex = 7;
		this.cmdPovolit.ZakazanyObrazek = null;
		this.cmdPovolit.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlPovolitH;
		this.cmdPovolit.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdPovolit_TlacitkoStisknuto);
		this.cmdOpakovat.BackColor = System.Drawing.Color.FromArgb(186, 231, 247);
		this.cmdOpakovat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOpakovat.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOpakovat.ForeColor = System.Drawing.Color.White;
		this.cmdOpakovat.Location = new System.Drawing.Point(410, 1);
		this.cmdOpakovat.Name = "cmdOpakovat";
		this.cmdOpakovat.NormalniObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlOpakovatN;
		this.cmdOpakovat.Size = new System.Drawing.Size(115, 23);
		this.cmdOpakovat.StisknutyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlOpakovatD;
		this.cmdOpakovat.TabIndex = 6;
		this.cmdOpakovat.ZakazanyObrazek = null;
		this.cmdOpakovat.ZvyraznenyObrazek = HYL.MountBlue.Resources.GrafikaSkolni.pngTlOpakovatH;
		this.cmdOpakovat.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOpakovat_TlacitkoStisknuto);
		this.picVlajka.Image = HYL.MountBlue.Resources.Grafika.pngVlajkaFinale;
		this.picVlajka.Location = new System.Drawing.Point(175, 5);
		this.picVlajka.Name = "picVlajka";
		this.picVlajka.Size = new System.Drawing.Size(18, 19);
		this.picVlajka.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
		this.picVlajka.TabIndex = 5;
		this.picVlajka.TabStop = false;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.picVlajka);
		base.Controls.Add(this.lblLekce);
		base.Controls.Add(this.lnkJmeno);
		base.Controls.Add(this.pnlKlasifikace);
		base.Controls.Add(this.pnlObecne);
		this.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.ForeColor = System.Drawing.Color.Black;
		base.Name = "Student";
		base.Size = new System.Drawing.Size(737, 27);
		this.pnlObecne.ResumeLayout(false);
		this.pnlObecne.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.picVelkeOdmeny).EndInit();
		this.pnlKlasifikace.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.picKlasifikace).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picVlajka).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
