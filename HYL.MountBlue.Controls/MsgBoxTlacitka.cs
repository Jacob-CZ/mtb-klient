using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

internal class MsgBoxTlacitka : UserControl
{
	public delegate void StisknutoTlacitko(DialogResult dlgres);

	private eMsgBoxTlacitka msgTlacitka;

	private IContainer components;

	private ObrazkoveTlacitko cmdAno;

	private ObrazkoveTlacitko cmdNe;

	private ObrazkoveTlacitko cmdOK;

	private ObrazkoveTlacitko cmdStorno;

	private ObrazkoveTlacitko cmdOK2;

	private ToolTip ttt;

	[Category("Konfigurace okna")]
	[Description("Tlačítka, která budou vidět v okně.")]
	public eMsgBoxTlacitka TlacitkaOkna
	{
		get
		{
			return msgTlacitka;
		}
		set
		{
			msgTlacitka = value;
			switch (msgTlacitka)
			{
			case eMsgBoxTlacitka.AnoNe:
				cmdAno.Visible = true;
				cmdNe.Visible = true;
				cmdOK.Visible = false;
				cmdOK2.Visible = false;
				cmdStorno.Visible = false;
				break;
			case eMsgBoxTlacitka.OkStorno:
				cmdAno.Visible = false;
				cmdNe.Visible = false;
				cmdOK.Visible = false;
				cmdOK2.Visible = true;
				cmdStorno.Visible = true;
				break;
			default:
				cmdAno.Visible = false;
				cmdNe.Visible = false;
				cmdOK.Visible = true;
				cmdOK2.Visible = false;
				cmdStorno.Visible = false;
				break;
			}
		}
	}

	public event StisknutoTlacitko Tlacitko;

	public MsgBoxTlacitka()
	{
		InitializeComponent();
		ToolTipText.NastavitToolTipText(ttt);
		ttt.SetToolTip(cmdOK, Texty.MsgBoxObsah_cmdOK_ttt);
		ttt.SetToolTip(cmdOK2, Texty.MsgBoxObsah_cmdOK2_ttt);
		ttt.SetToolTip(cmdStorno, Texty.MsgBoxObsah_cmdStorno_ttt);
		ttt.SetToolTip(cmdAno, Texty.MsgBoxObsah_cmdAno_ttt);
		ttt.SetToolTip(cmdNe, Texty.MsgBoxObsah_cmdNe_ttt);
	}

	public bool StisknutaKlavesa(KeyEventArgs e)
	{
		switch (msgTlacitka)
		{
		case eMsgBoxTlacitka.AnoNe:
			if (e.KeyCode == Keys.A)
			{
				cmdAno_TlacitkoStisknuto();
			}
			else
			{
				if (e.KeyCode != Keys.N && e.KeyCode != Keys.Escape)
				{
					return false;
				}
				cmdNe_TlacitkoStisknuto();
			}
			return true;
		case eMsgBoxTlacitka.OkStorno:
			if (e.KeyCode == Keys.O)
			{
				cmdOK2_TlacitkoStisknuto();
			}
			else
			{
				if (e.KeyCode != Keys.S && e.KeyCode != Keys.Escape)
				{
					return false;
				}
				cmdStorno_TlacitkoStisknuto();
			}
			return true;
		default:
			if (e.KeyCode == Keys.O || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Return)
			{
				cmdOK_TlacitkoStisknuto();
				return true;
			}
			return false;
		}
	}

	private void cmdOK_TlacitkoStisknuto()
	{
		if (this.Tlacitko != null)
		{
			this.Tlacitko(DialogResult.OK);
		}
	}

	private void cmdNe_TlacitkoStisknuto()
	{
		if (this.Tlacitko != null)
		{
			this.Tlacitko(DialogResult.No);
		}
	}

	private void cmdAno_TlacitkoStisknuto()
	{
		if (this.Tlacitko != null)
		{
			this.Tlacitko(DialogResult.Yes);
		}
	}

	private void cmdOK2_TlacitkoStisknuto()
	{
		if (this.Tlacitko != null)
		{
			this.Tlacitko(DialogResult.OK);
		}
	}

	private void cmdStorno_TlacitkoStisknuto()
	{
		if (this.Tlacitko != null)
		{
			this.Tlacitko(DialogResult.Cancel);
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
		this.cmdOK = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdNe = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdAno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdStorno = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.cmdOK2 = new HYL.MountBlue.Controls.ObrazkoveTlacitko();
		this.ttt = new System.Windows.Forms.ToolTip(this.components);
		base.SuspendLayout();
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.BackColor = System.Drawing.Color.White;
		this.cmdOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK.ForeColor = System.Drawing.Color.White;
		this.cmdOK.Location = new System.Drawing.Point(85, 5);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK.Size = new System.Drawing.Size(60, 23);
		this.cmdOK.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK.TabIndex = 2;
		this.cmdOK.Visible = false;
		this.cmdOK.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK_TlacitkoStisknuto);
		this.cmdNe.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdNe.BackColor = System.Drawing.Color.White;
		this.cmdNe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdNe.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdNe.ForeColor = System.Drawing.Color.White;
		this.cmdNe.Location = new System.Drawing.Point(78, 5);
		this.cmdNe.Name = "cmdNe";
		this.cmdNe.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlNeN;
		this.cmdNe.Size = new System.Drawing.Size(60, 23);
		this.cmdNe.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNeD;
		this.cmdNe.TabIndex = 1;
		this.cmdNe.Visible = false;
		this.cmdNe.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNeZ;
		this.cmdNe.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlNeH;
		this.cmdNe.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdNe_TlacitkoStisknuto);
		this.cmdAno.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdAno.BackColor = System.Drawing.Color.White;
		this.cmdAno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdAno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdAno.ForeColor = System.Drawing.Color.White;
		this.cmdAno.Location = new System.Drawing.Point(12, 5);
		this.cmdAno.Name = "cmdAno";
		this.cmdAno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlAnoN;
		this.cmdAno.Size = new System.Drawing.Size(60, 23);
		this.cmdAno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlAnoD;
		this.cmdAno.TabIndex = 0;
		this.cmdAno.Visible = false;
		this.cmdAno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlAnoZ;
		this.cmdAno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlAnoH;
		this.cmdAno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdAno_TlacitkoStisknuto);
		this.cmdStorno.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdStorno.BackColor = System.Drawing.Color.White;
		this.cmdStorno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdStorno.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdStorno.ForeColor = System.Drawing.Color.White;
		this.cmdStorno.Location = new System.Drawing.Point(73, 5);
		this.cmdStorno.Name = "cmdStorno";
		this.cmdStorno.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoN;
		this.cmdStorno.Size = new System.Drawing.Size(70, 23);
		this.cmdStorno.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoD;
		this.cmdStorno.TabIndex = 6;
		this.cmdStorno.Visible = false;
		this.cmdStorno.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoZ;
		this.cmdStorno.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlStornoH;
		this.cmdStorno.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdStorno_TlacitkoStisknuto);
		this.cmdOK2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.cmdOK2.BackColor = System.Drawing.Color.White;
		this.cmdOK2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.cmdOK2.Font = new System.Drawing.Font("Arial", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.cmdOK2.ForeColor = System.Drawing.Color.White;
		this.cmdOK2.Location = new System.Drawing.Point(7, 5);
		this.cmdOK2.Name = "cmdOK2";
		this.cmdOK2.NormalniObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkN;
		this.cmdOK2.Size = new System.Drawing.Size(60, 23);
		this.cmdOK2.StisknutyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkD;
		this.cmdOK2.TabIndex = 5;
		this.cmdOK2.Visible = false;
		this.cmdOK2.ZakazanyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkZ;
		this.cmdOK2.ZvyraznenyObrazek = HYL.MountBlue.Resources.Grafika.pngTlOkH;
		this.cmdOK2.TlacitkoStisknuto += new HYL.MountBlue.Controls.ObrazkoveTlacitko.Stisknuto(cmdOK2_TlacitkoStisknuto);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.cmdStorno);
		base.Controls.Add(this.cmdOK2);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.cmdNe);
		base.Controls.Add(this.cmdAno);
		this.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		base.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
		this.MaximumSize = new System.Drawing.Size(700, 30);
		this.MinimumSize = new System.Drawing.Size(150, 30);
		base.Name = "MsgBoxTlacitka";
		base.Size = new System.Drawing.Size(150, 30);
		base.ResumeLayout(false);
	}
}
