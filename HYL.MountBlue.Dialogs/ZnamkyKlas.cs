using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Classes.Cviceni;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class ZnamkyKlas : _Bublina
{
	private IContainer components;

	private Label lblInfo;

	private Label lblZnamka;

	private Label lblHodnota;

	private PictureBox picLine1;

	private Label lblHodnota1;

	private Label lblZnamka1;

	private Label lblZnamka2;

	private Label lblHodnota2;

	private Label lblZnamka3;

	private Label lblHodnota3;

	private Label lblZnamka4;

	private Label lblHodnota4;

	private Label lblZnamka5;

	private Label lblHodnotaJinak;

	private ObrazkoveTlacitko cmdOK;

	private PictureBox picLine2;

	public ZnamkyKlas(Psani psani)
	{
		InitializeComponent();
		Text = Texty.ZnamkyKlas_Title;
		lblInfo.Text = string.Format(HYL.MountBlue.Classes.Text.TextNBSP(Texty.ZnamkyKlas_lblInfo), Psani.KriteriumToString(psani.VyhodnoceniKriterium));
		lblZnamka.Text = Texty.ZnamkyKlas_lblZnamka;
		lblHodnota.Text = Texty.ZnamkyKlas_lblHodnota;
		if (psani.PocetPodminek == 4)
		{
			string text = ((psani.VyhodnoceniKriterium != Psani.eKriterium.pocetChybRel) ? "0" : "0.00");
			lblHodnota1.Text = psani.Podminka(0).Hodnota.ToString(text);
			lblHodnota2.Text = psani.Podminka(1).Hodnota.ToString(text);
			lblHodnota3.Text = psani.Podminka(2).Hodnota.ToString(text);
			lblHodnota4.Text = psani.Podminka(3).Hodnota.ToString(text);
		}
		lblHodnotaJinak.Text = Texty.ZnamkyKlas_lblHodnotaJinak;
	}

	public static void ZobrazitInformace(Psani psani)
	{
		using ZnamkyKlas znamkyKlas = new ZnamkyKlas(psani);
		znamkyKlas.BublinaZobrazitDialog();
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		Close();
	}

	private void ZnamkyKlas_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			cmdOK_TlacitkoStisknuto();
			e.SuppressKeyPress = true;
			e.Handled = true;
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
		this.lblInfo = new System.Windows.Forms.Label();
		this.lblZnamka = new System.Windows.Forms.Label();
		this.lblHodnota = new System.Windows.Forms.Label();
		this.picLine1 = new System.Windows.Forms.PictureBox();
		this.lblHodnota1 = new System.Windows.Forms.Label();
		this.lblZnamka1 = new System.Windows.Forms.Label();
		this.lblZnamka2 = new System.Windows.Forms.Label();
		this.lblHodnota2 = new System.Windows.Forms.Label();
		this.lblZnamka3 = new System.Windows.Forms.Label();
		this.lblHodnota3 = new System.Windows.Forms.Label();
		this.lblZnamka4 = new System.Windows.Forms.Label();
		this.lblHodnota4 = new System.Windows.Forms.Label();
		this.lblZnamka5 = new System.Windows.Forms.Label();
		this.lblHodnotaJinak = new System.Windows.Forms.Label();
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.picLine2 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.picLine1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picLine2).BeginInit();
		base.SuspendLayout();
		this.lblInfo.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo.Location = new System.Drawing.Point(12, 49);
		this.lblInfo.Name = "lblInfo";
		this.lblInfo.Size = new System.Drawing.Size(276, 68);
		this.lblInfo.TabIndex = 21;
		this.lblInfo.Text = "???";
		this.lblZnamka.Location = new System.Drawing.Point(57, 120);
		this.lblZnamka.Name = "lblZnamka";
		this.lblZnamka.Size = new System.Drawing.Size(58, 16);
		this.lblZnamka.TabIndex = 22;
		this.lblZnamka.Text = "???";
		this.lblZnamka.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblHodnota.Location = new System.Drawing.Point(173, 120);
		this.lblHodnota.Name = "lblHodnota";
		this.lblHodnota.Size = new System.Drawing.Size(61, 16);
		this.lblHodnota.TabIndex = 23;
		this.lblHodnota.Text = "???";
		this.lblHodnota.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.picLine1.BackColor = System.Drawing.Color.Black;
		this.picLine1.Location = new System.Drawing.Point(12, 139);
		this.picLine1.Name = "picLine1";
		this.picLine1.Size = new System.Drawing.Size(276, 1);
		this.picLine1.TabIndex = 24;
		this.picLine1.TabStop = false;
		this.lblHodnota1.Location = new System.Drawing.Point(173, 146);
		this.lblHodnota1.Name = "lblHodnota1";
		this.lblHodnota1.Size = new System.Drawing.Size(61, 16);
		this.lblHodnota1.TabIndex = 26;
		this.lblHodnota1.Text = "???";
		this.lblHodnota1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblZnamka1.Location = new System.Drawing.Point(57, 146);
		this.lblZnamka1.Name = "lblZnamka1";
		this.lblZnamka1.Size = new System.Drawing.Size(58, 16);
		this.lblZnamka1.TabIndex = 25;
		this.lblZnamka1.Text = "1";
		this.lblZnamka1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblZnamka2.Location = new System.Drawing.Point(57, 165);
		this.lblZnamka2.Name = "lblZnamka2";
		this.lblZnamka2.Size = new System.Drawing.Size(58, 16);
		this.lblZnamka2.TabIndex = 25;
		this.lblZnamka2.Text = "2";
		this.lblZnamka2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblHodnota2.Location = new System.Drawing.Point(173, 165);
		this.lblHodnota2.Name = "lblHodnota2";
		this.lblHodnota2.Size = new System.Drawing.Size(61, 16);
		this.lblHodnota2.TabIndex = 26;
		this.lblHodnota2.Text = "???";
		this.lblHodnota2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblZnamka3.Location = new System.Drawing.Point(57, 185);
		this.lblZnamka3.Name = "lblZnamka3";
		this.lblZnamka3.Size = new System.Drawing.Size(58, 16);
		this.lblZnamka3.TabIndex = 25;
		this.lblZnamka3.Text = "3";
		this.lblZnamka3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblHodnota3.Location = new System.Drawing.Point(173, 185);
		this.lblHodnota3.Name = "lblHodnota3";
		this.lblHodnota3.Size = new System.Drawing.Size(61, 16);
		this.lblHodnota3.TabIndex = 26;
		this.lblHodnota3.Text = "???";
		this.lblHodnota3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblZnamka4.Location = new System.Drawing.Point(57, 205);
		this.lblZnamka4.Name = "lblZnamka4";
		this.lblZnamka4.Size = new System.Drawing.Size(58, 16);
		this.lblZnamka4.TabIndex = 25;
		this.lblZnamka4.Text = "4";
		this.lblZnamka4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblHodnota4.Location = new System.Drawing.Point(173, 205);
		this.lblHodnota4.Name = "lblHodnota4";
		this.lblHodnota4.Size = new System.Drawing.Size(61, 16);
		this.lblHodnota4.TabIndex = 26;
		this.lblHodnota4.Text = "???";
		this.lblHodnota4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblZnamka5.Location = new System.Drawing.Point(57, 225);
		this.lblZnamka5.Name = "lblZnamka5";
		this.lblZnamka5.Size = new System.Drawing.Size(58, 16);
		this.lblZnamka5.TabIndex = 25;
		this.lblZnamka5.Text = "5";
		this.lblZnamka5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.lblHodnotaJinak.Location = new System.Drawing.Point(142, 225);
		this.lblHodnotaJinak.Name = "lblHodnotaJinak";
		this.lblHodnotaJinak.Size = new System.Drawing.Size(123, 16);
		this.lblHodnotaJinak.TabIndex = 26;
		this.lblHodnotaJinak.Text = "???";
		this.lblHodnotaJinak.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(228, 260);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 38;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.picLine2.BackColor = System.Drawing.Color.Black;
		this.picLine2.Location = new System.Drawing.Point(12, 249);
		this.picLine2.Name = "picLine2";
		this.picLine2.Size = new System.Drawing.Size(276, 1);
		this.picLine2.TabIndex = 39;
		this.picLine2.TabStop = false;
		base.ClientSize = new System.Drawing.Size(368, 295);
		base.Controls.Add(this.picLine2);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.lblHodnotaJinak);
		base.Controls.Add(this.lblZnamka5);
		base.Controls.Add(this.lblHodnota4);
		base.Controls.Add(this.lblZnamka4);
		base.Controls.Add(this.lblHodnota3);
		base.Controls.Add(this.lblZnamka3);
		base.Controls.Add(this.lblHodnota2);
		base.Controls.Add(this.lblZnamka2);
		base.Controls.Add(this.lblHodnota1);
		base.Controls.Add(this.lblZnamka1);
		base.Controls.Add(this.picLine1);
		base.Controls.Add(this.lblHodnota);
		base.Controls.Add(this.lblInfo);
		base.Controls.Add(this.lblZnamka);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "ZnamkyKlas";
		base.Opacity = 0.0;
		base.VyskaZahlavi = 40;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(ZnamkyKlas_KeyDown);
		base.Controls.SetChildIndex(this.lblZnamka, 0);
		base.Controls.SetChildIndex(this.lblInfo, 0);
		base.Controls.SetChildIndex(this.lblHodnota, 0);
		base.Controls.SetChildIndex(this.picLine1, 0);
		base.Controls.SetChildIndex(this.lblZnamka1, 0);
		base.Controls.SetChildIndex(this.lblHodnota1, 0);
		base.Controls.SetChildIndex(this.lblZnamka2, 0);
		base.Controls.SetChildIndex(this.lblHodnota2, 0);
		base.Controls.SetChildIndex(this.lblZnamka3, 0);
		base.Controls.SetChildIndex(this.lblHodnota3, 0);
		base.Controls.SetChildIndex(this.lblZnamka4, 0);
		base.Controls.SetChildIndex(this.lblHodnota4, 0);
		base.Controls.SetChildIndex(this.lblZnamka5, 0);
		base.Controls.SetChildIndex(this.lblHodnotaJinak, 0);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.picLine2, 0);
		((System.ComponentModel.ISupportInitialize)this.picLine1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picLine2).EndInit();
		base.ResumeLayout(false);
	}
}
