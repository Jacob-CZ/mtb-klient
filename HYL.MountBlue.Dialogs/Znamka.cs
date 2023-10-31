using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Dialogs;

internal class Znamka : _Bublina
{
	private IContainer components;

	private Label lblInfo;

	private ObrazkoveTlacitko cmdOK;

	private Label lblZnamka;

	public Znamka(byte znamka)
	{
		InitializeComponent();
		Text = Texty.Znamka_Title;
		lblInfo.Text = Texty.Znamka_lblInfo;
		lblZnamka.Text = znamka.ToString();
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		Close();
	}

	public static void ZobrazitZnamku(byte znamka)
	{
		using Znamka znamka2 = new Znamka(znamka);
		znamka2.BublinaZobrazitDialog();
	}

	private void Znamka_KeyDown(object sender, KeyEventArgs e)
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
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.lblZnamka = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblInfo.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.lblInfo.Location = new System.Drawing.Point(7, 38);
		this.lblInfo.Name = "lblInfo";
		this.lblInfo.Size = new System.Drawing.Size(171, 27);
		this.lblInfo.TabIndex = 1;
		this.lblInfo.Text = "???";
		this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(115, 122);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 0;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.lblZnamka.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblZnamka.Font = new System.Drawing.Font("Arial", 21.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblZnamka.Location = new System.Drawing.Point(6, 73);
		this.lblZnamka.Name = "lblZnamka";
		this.lblZnamka.Size = new System.Drawing.Size(172, 43);
		this.lblZnamka.TabIndex = 2;
		this.lblZnamka.Text = "???";
		this.lblZnamka.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		base.ClientSize = new System.Drawing.Size(250, 157);
		base.Controls.Add(this.lblInfo);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.lblZnamka);
		this.ForeColor = System.Drawing.Color.Black;
		base.KeyPreview = true;
		base.Name = "Znamka";
		base.Opacity = 0.0;
		base.VyskaZahlavi = 35;
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Znamka_KeyDown);
		base.Controls.SetChildIndex(this.lblZnamka, 0);
		base.Controls.SetChildIndex(this.cmdOK, 0);
		base.Controls.SetChildIndex(this.lblInfo, 0);
		base.ResumeLayout(false);
	}
}
