using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

public class Trida : UserControl
{
	public delegate void ZobrazitHistoriiDeleg(uint uid, ZalozkyHistorie.eZalozky zalozka);

	private Dictionary<uint, Student> seznam;

	private int tridaID;

	private ZalozkyTrida.eZalozky zalozka;

	private IContainer components;

	private Panel pnlZahlavi;

	private Panel pnlObsah;

	private Label lblJmeno;

	private Label lblVelkeOdmeny;

	private Label lblLekce;

	private Label lblPoznamka;

	private Label lblKlasifikace;

	public ZalozkyTrida.eZalozky AktualniZalozka => zalozka;

	public event ZobrazitHistoriiDeleg ZobrazitHistorii;

	public Trida()
	{
		InitializeComponent();
		seznam = new Dictionary<uint, Student>();
		lblJmeno.Text = Texty.Trida_lblJmeno;
		lblLekce.Text = Texty.Trida_lblLekce;
		lblVelkeOdmeny.Text = Texty.Trida_lblVelkeOdmeny;
		lblPoznamka.Text = Texty.Trida_lblPoznamka;
		lblKlasifikace.Text = Texty.Trida_lblKlasifikace;
	}

	public Trida(int tridaID)
		: this()
	{
		this.tridaID = tridaID;
		NacistStudenty();
	}

	private void NacistStudenty()
	{
		if (seznam.Count <= 0)
		{
			HYL.MountBlue.Classes.Uzivatele.Uzivatel.Student[] array = Klient.Stanice.AktualniTridy[tridaID].StudentiTridy();
			for (int i = 0; i < array.Length; i++)
			{
				StudentSkolni student = (StudentSkolni)array[i];
				PridanNovyStudent(student);
			}
		}
	}

	public void ZobrazitZalozku(ZalozkyTrida.eZalozky zalozka)
	{
		SuspendLayout();
		this.zalozka = zalozka;
		ZobrazitZahlavi(zalozka);
		foreach (Student value in seznam.Values)
		{
			value.ZobrazitZalozku(zalozka);
		}
		ResumeLayout();
	}

	public void PridanNovyStudent(uint uid)
	{
		PridanNovyStudent((StudentSkolni)Klient.Stanice.AktualniUzivatele[uid]);
	}

	private void PridanNovyStudent(StudentSkolni student)
	{
		Student student2 = new Student(student, this);
		student2.Dock = DockStyle.Top;
		seznam.Add(student.UID, student2);
		pnlObsah.Controls.Add(student2);
		student2.BringToFront();
	}

	public void ZobrazitZahlavi(ZalozkyTrida.eZalozky zalozka)
	{
		switch (zalozka)
		{
		case ZalozkyTrida.eZalozky.SeznamStudentu:
			lblVelkeOdmeny.Visible = true;
			lblPoznamka.Visible = true;
			lblKlasifikace.Visible = false;
			break;
		case ZalozkyTrida.eZalozky.Klasifikace:
			lblVelkeOdmeny.Visible = false;
			lblPoznamka.Visible = false;
			lblKlasifikace.Visible = true;
			break;
		}
	}

	public void Obnovit()
	{
		foreach (Student value in seznam.Values)
		{
			value.Obnovit();
		}
	}

	public void ZobrazitHistoriiEvent(uint uid)
	{
		if (this.ZobrazitHistorii != null)
		{
			if (AktualniZalozka == ZalozkyTrida.eZalozky.SeznamStudentu)
			{
				this.ZobrazitHistorii(uid, ZalozkyHistorie.eZalozky.Historie);
			}
			else
			{
				this.ZobrazitHistorii(uid, ZalozkyHistorie.eZalozky.Klasifikace);
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
		this.pnlZahlavi = new System.Windows.Forms.Panel();
		this.lblPoznamka = new System.Windows.Forms.Label();
		this.lblVelkeOdmeny = new System.Windows.Forms.Label();
		this.lblLekce = new System.Windows.Forms.Label();
		this.lblJmeno = new System.Windows.Forms.Label();
		this.pnlObsah = new System.Windows.Forms.Panel();
		this.lblKlasifikace = new System.Windows.Forms.Label();
		this.pnlZahlavi.SuspendLayout();
		base.SuspendLayout();
		this.pnlZahlavi.BackColor = System.Drawing.Color.SkyBlue;
		this.pnlZahlavi.Controls.Add(this.lblKlasifikace);
		this.pnlZahlavi.Controls.Add(this.lblPoznamka);
		this.pnlZahlavi.Controls.Add(this.lblVelkeOdmeny);
		this.pnlZahlavi.Controls.Add(this.lblLekce);
		this.pnlZahlavi.Controls.Add(this.lblJmeno);
		this.pnlZahlavi.Dock = System.Windows.Forms.DockStyle.Top;
		this.pnlZahlavi.Location = new System.Drawing.Point(0, 0);
		this.pnlZahlavi.Name = "pnlZahlavi";
		this.pnlZahlavi.Size = new System.Drawing.Size(760, 22);
		this.pnlZahlavi.TabIndex = 0;
		this.lblPoznamka.AutoSize = true;
		this.lblPoznamka.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblPoznamka.Location = new System.Drawing.Point(492, 4);
		this.lblPoznamka.Name = "lblPoznamka";
		this.lblPoznamka.Size = new System.Drawing.Size(25, 14);
		this.lblPoznamka.TabIndex = 3;
		this.lblPoznamka.Text = "???";
		this.lblVelkeOdmeny.AutoSize = true;
		this.lblVelkeOdmeny.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblVelkeOdmeny.Location = new System.Drawing.Point(207, 4);
		this.lblVelkeOdmeny.Name = "lblVelkeOdmeny";
		this.lblVelkeOdmeny.Size = new System.Drawing.Size(25, 14);
		this.lblVelkeOdmeny.TabIndex = 2;
		this.lblVelkeOdmeny.Text = "???";
		this.lblLekce.AutoSize = true;
		this.lblLekce.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblLekce.Location = new System.Drawing.Point(166, 4);
		this.lblLekce.Name = "lblLekce";
		this.lblLekce.Size = new System.Drawing.Size(25, 14);
		this.lblLekce.TabIndex = 1;
		this.lblLekce.Text = "???";
		this.lblJmeno.AutoSize = true;
		this.lblJmeno.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblJmeno.Location = new System.Drawing.Point(4, 4);
		this.lblJmeno.Name = "lblJmeno";
		this.lblJmeno.Size = new System.Drawing.Size(25, 14);
		this.lblJmeno.TabIndex = 0;
		this.lblJmeno.Text = "???";
		this.pnlObsah.AutoScroll = true;
		this.pnlObsah.AutoScrollMargin = new System.Drawing.Size(0, 2);
		this.pnlObsah.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pnlObsah.Location = new System.Drawing.Point(0, 22);
		this.pnlObsah.Name = "pnlObsah";
		this.pnlObsah.Size = new System.Drawing.Size(760, 418);
		this.pnlObsah.TabIndex = 1;
		this.lblKlasifikace.AutoSize = true;
		this.lblKlasifikace.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblKlasifikace.Location = new System.Drawing.Point(207, 4);
		this.lblKlasifikace.Name = "lblKlasifikace";
		this.lblKlasifikace.Size = new System.Drawing.Size(25, 14);
		this.lblKlasifikace.TabIndex = 4;
		this.lblKlasifikace.Text = "???";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		base.Controls.Add(this.pnlObsah);
		base.Controls.Add(this.pnlZahlavi);
		this.ForeColor = System.Drawing.Color.Black;
		base.Name = "Trida";
		base.Size = new System.Drawing.Size(760, 440);
		this.pnlZahlavi.ResumeLayout(false);
		this.pnlZahlavi.PerformLayout();
		base.ResumeLayout(false);
	}
}
