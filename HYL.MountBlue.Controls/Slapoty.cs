using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Controls;

internal class Slapoty : UserControl
{
	internal delegate void VyprselCas();

	private const string FormatCasu = "{0}:{1:00}";

	private readonly int[] SirkySlapot = new int[18]
	{
		36, 64, 93, 122, 152, 181, 210, 241, 270, 300,
		328, 357, 386, 414, 443, 472, 501, 532
	};

	private int mintCelkovyCas = 1;

	private int mintUplynulyCas;

	private int mintPosledniVterina;

	private IContainer components;

	private Label lblPopisek;

	private Label lblZbyvajiciCas;

	private PictureBox picSlapoty;

	private Timer tmrSlapoty;

	private PictureBox picSlapotyHnede;

	[Description("Celkový čas šlápot prvku.")]
	[Category("Konfigurace šlápot")]
	internal TimeSpan CelkovyCas
	{
		get
		{
			return TimeSpan.FromSeconds(mintCelkovyCas);
		}
		set
		{
			mintCelkovyCas = (int)value.TotalSeconds;
			if (mintCelkovyCas <= 0)
			{
				lblZbyvajiciCas.ForeColor = Color.Silver;
				lblPopisek.ForeColor = Color.Silver;
			}
			else
			{
				lblZbyvajiciCas.ForeColor = Color.Black;
				lblPopisek.ForeColor = Color.Black;
			}
			mintUplynulyCas = 0;
			Obnovit();
		}
	}

	internal bool SlapotyBezi => tmrSlapoty.Enabled;

	internal TimeSpan UplynulyCas => TimeSpan.FromSeconds(mintUplynulyCas);

	internal event VyprselCas CasVyprsel;

	internal Slapoty()
	{
		InitializeComponent();
		lblPopisek.Text = Texty.Slapoty_lblPopisek;
		lblZbyvajiciCas.Text = Texty.Slapoty_lblZbyvajiciCas;
	}

	internal void Start()
	{
		if (mintCelkovyCas >= 30)
		{
			mintUplynulyCas = 0;
			mintPosledniVterina = DateTime.Now.Second;
			tmrSlapoty.Start();
			Obnovit();
		}
	}

	internal void Stop()
	{
		if (mintCelkovyCas >= 30)
		{
			tmrSlapoty.Stop();
			Obnovit();
		}
	}

	internal void Vynulovat()
	{
		if (mintCelkovyCas >= 30)
		{
			tmrSlapoty.Stop();
			mintUplynulyCas = 0;
			Obnovit();
		}
	}

	internal void NastavitDokonceno()
	{
		if (mintCelkovyCas >= 30)
		{
			mintUplynulyCas = mintCelkovyCas;
			Stop();
		}
	}

	private void Obnovit()
	{
		int num = mintCelkovyCas - mintUplynulyCas;
		lblZbyvajiciCas.Text = $"{num / 60}:{num % 60:00}";
		if (mintCelkovyCas == 0)
		{
			picSlapotyHnede.Width = 0;
			return;
		}
		if (mintUplynulyCas == 0 && !tmrSlapoty.Enabled)
		{
			picSlapotyHnede.Width = 0;
			return;
		}
		if (mintUplynulyCas >= mintCelkovyCas)
		{
			picSlapotyHnede.Width = picSlapoty.Width;
			return;
		}
		int num2 = mintUplynulyCas * SirkySlapot.Length / mintCelkovyCas;
		picSlapotyHnede.Width = SirkySlapot[num2];
	}

	private void tmrSlapoty_Tick(object sender, EventArgs e)
	{
		int second = DateTime.Now.Second;
		if (mintPosledniVterina == second)
		{
			return;
		}
		mintPosledniVterina = second;
		mintUplynulyCas++;
		if (mintUplynulyCas >= mintCelkovyCas)
		{
			mintUplynulyCas = mintCelkovyCas;
			Stop();
			if (this.CasVyprsel != null)
			{
				this.CasVyprsel();
			}
		}
		else
		{
			Obnovit();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HYL.MountBlue.Controls.Slapoty));
		this.lblPopisek = new System.Windows.Forms.Label();
		this.lblZbyvajiciCas = new System.Windows.Forms.Label();
		this.picSlapoty = new System.Windows.Forms.PictureBox();
		this.tmrSlapoty = new System.Windows.Forms.Timer(this.components);
		this.picSlapotyHnede = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.picSlapoty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.picSlapotyHnede).BeginInit();
		base.SuspendLayout();
		this.lblPopisek.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblPopisek.ForeColor = System.Drawing.Color.Black;
		this.lblPopisek.Location = new System.Drawing.Point(-3, 2);
		this.lblPopisek.Name = "lblPopisek";
		this.lblPopisek.Size = new System.Drawing.Size(91, 29);
		this.lblPopisek.TabIndex = 0;
		this.lblPopisek.Text = "???";
		this.lblPopisek.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblZbyvajiciCas.Font = new System.Drawing.Font("Arial", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.lblZbyvajiciCas.ForeColor = System.Drawing.Color.Black;
		this.lblZbyvajiciCas.Image = (System.Drawing.Image)resources.GetObject("lblZbyvajiciCas.Image");
		this.lblZbyvajiciCas.Location = new System.Drawing.Point(664, 2);
		this.lblZbyvajiciCas.Name = "lblZbyvajiciCas";
		this.lblZbyvajiciCas.Size = new System.Drawing.Size(67, 31);
		this.lblZbyvajiciCas.TabIndex = 1;
		this.lblZbyvajiciCas.Text = "???";
		this.lblZbyvajiciCas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.picSlapoty.Image = (System.Drawing.Image)resources.GetObject("picSlapoty.Image");
		this.picSlapoty.Location = new System.Drawing.Point(94, 4);
		this.picSlapoty.Name = "picSlapoty";
		this.picSlapoty.Size = new System.Drawing.Size(564, 27);
		this.picSlapoty.TabIndex = 2;
		this.picSlapoty.TabStop = false;
		this.tmrSlapoty.Tick += new System.EventHandler(tmrSlapoty_Tick);
		this.picSlapotyHnede.Image = (System.Drawing.Image)resources.GetObject("picSlapotyHnede.Image");
		this.picSlapotyHnede.Location = new System.Drawing.Point(94, 4);
		this.picSlapotyHnede.Name = "picSlapotyHnede";
		this.picSlapotyHnede.Size = new System.Drawing.Size(214, 27);
		this.picSlapotyHnede.TabIndex = 3;
		this.picSlapotyHnede.TabStop = false;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.White;
		base.Controls.Add(this.picSlapotyHnede);
		base.Controls.Add(this.picSlapoty);
		base.Controls.Add(this.lblZbyvajiciCas);
		base.Controls.Add(this.lblPopisek);
		this.MaximumSize = new System.Drawing.Size(736, 38);
		this.MinimumSize = new System.Drawing.Size(736, 38);
		base.Name = "Slapoty";
		base.Size = new System.Drawing.Size(736, 38);
		((System.ComponentModel.ISupportInitialize)this.picSlapoty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.picSlapotyHnede).EndInit();
		base.ResumeLayout(false);
	}
}
