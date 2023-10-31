using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class OpakovatKlasifikaci : _Dialog
{
	private struct KlasifikacePolozka
	{
		public uint KlasifikaceID;

		public string Popis;

		public override string ToString()
		{
			return Popis;
		}
	}

	private StudentSkolni student;

	private IContainer components;

	private PictureBox picLine;

	private ObrazkoveTlacitko cmdStorno;

	private ObrazkoveTlacitko cmdOK;

	private Label lblInfo;

	private ComboBox lstKlasifikace;

	public OpakovatKlasifikaci(StudentSkolni student)
	{
		InitializeComponent();
		this.student = student;
		Text = Texty.OpakovatKlasifikaci_Title;
		lblInfo.Text = Texty.OpakovatKlasifikaci_lblInfo;
		NacistKlasifikace();
	}

	public static void ZobrazitOpakovatKlasifikaci(IWin32Window owner, StudentSkolni stu)
	{
		using OpakovatKlasifikaci opakovatKlasifikaci = new OpakovatKlasifikaci(stu);
		opakovatKlasifikaci.ShowDialog(owner);
	}

	private void NacistKlasifikace()
	{
		lstKlasifikace.Items.Clear();
		KlasifPolozka[] array = _Lekce.Lekce().Klasifikace.SeznamKlasifikaci(student);
		KlasifPolozka[] array2 = array;
		foreach (KlasifPolozka klasifPolozka in array2)
		{
			switch (klasifPolozka.Stav)
			{
			case KlasifPolozka.eStav.splneno:
			case KlasifPolozka.eStav.moznostVyuzitOdmeny:
			{
				KlasifikacePolozka klasifikacePolozka = default(KlasifikacePolozka);
				klasifikacePolozka.KlasifikaceID = klasifPolozka.CviceniPsani.ID;
				klasifikacePolozka.Popis = string.Format(Texty.OpakovatKlasifikaci_klpol, klasifPolozka.CviceniPsani.KlasifikaceOd - 1);
				lstKlasifikace.Items.Add(klasifikacePolozka);
				break;
			}
			}
		}
		if (lstKlasifikace.Items.Count > 0)
		{
			lstKlasifikace.SelectedIndex = 0;
		}
	}

	private void cmdStorno_TlacitkoStisknuto()
	{
		Close();
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		if (lstKlasifikace.SelectedIndex >= 0)
		{
			KlasifikacePolozka klasifikacePolozka = (KlasifikacePolozka)lstKlasifikace.SelectedItem;
			student.KlasifikaceNastavitOpakovani(klasifikacePolozka.KlasifikaceID);
			Close();
		}
	}

	private void OpakovatKlasifikaci_KeyDown(object sender, KeyEventArgs e)
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
		this.picLine = new System.Windows.Forms.PictureBox();
		this.cmdStorno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblInfo = new System.Windows.Forms.Label();
		this.lstKlasifikace = new System.Windows.Forms.ComboBox();
		((System.ComponentModel.ISupportInitialize)this.picLine).BeginInit();
		base.SuspendLayout();
		this.picLine.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.picLine.BackColor = System.Drawing.Color.Black;
		this.picLine.Location = new System.Drawing.Point(11, 114);
		this.picLine.Name = "picLine";
		this.picLine.Size = new System.Drawing.Size(319, 1);
		this.picLine.TabIndex = 44;
		this.picLine.TabStop = false;
		this.cmdStorno.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdStorno.BackColor = System.Drawing.Color.White;
		this.cmdStorno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdStorno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdStorno.ForeColor = System.Drawing.Color.White;
		this.cmdStorno.Location = new System.Drawing.Point(260, 120);
		this.cmdStorno.Name = "cmdStorno";
		this.cmdStorno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoN;
		this.cmdStorno.Size = new System.Drawing.Size(70, 23);
		this.cmdStorno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoD;
		this.cmdStorno.TabIndex = 43;
		this.cmdStorno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoZ;
		this.cmdStorno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoH;
		this.cmdStorno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdStorno_TlacitkoStisknuto);
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(194, 120);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 42;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo.Location = new System.Drawing.Point(8, 45);
		this.lblInfo.Name = "lblInfo";
		this.lblInfo.Size = new System.Drawing.Size(322, 34);
		this.lblInfo.TabIndex = 45;
		this.lblInfo.Text = "???";
		this.lstKlasifikace.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lstKlasifikace.BackColor = System.Drawing.Color.Gainsboro;
		this.lstKlasifikace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.lstKlasifikace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lstKlasifikace.FormattingEnabled = true;
		this.lstKlasifikace.Location = new System.Drawing.Point(43, 82);
		this.lstKlasifikace.Name = "lstKlasifikace";
		this.lstKlasifikace.Size = new System.Drawing.Size(254, 23);
		this.lstKlasifikace.TabIndex = 46;
		base.ClientSize = new System.Drawing.Size(341, 151);
		base.Controls.Add(this.lstKlasifikace);
		base.Controls.Add(this.lblInfo);
		base.Controls.Add(this.picLine);
		base.Controls.Add(this.cmdStorno);
		base.Controls.Add(this.cmdOK);
		base.KeyPreview = true;
		base.Name = "OpakovatKlasifikaci";
		base.Opacity = 0.9990000128746033;
		this.Text = "???";
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(OpakovatKlasifikaci_KeyDown);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.cmdStorno, 0);
		base.Controls.SetChildIndex(this.picLine, 0);
		base.Controls.SetChildIndex(this.lblInfo, 0);
		base.Controls.SetChildIndex(this.lstKlasifikace, 0);
		((System.ComponentModel.ISupportInitialize)this.picLine).EndInit();
		base.ResumeLayout(false);
	}
}
