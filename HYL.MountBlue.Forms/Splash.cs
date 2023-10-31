using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Grafika;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Forms;

internal class Splash : Form
{
	public delegate void ZobrazitSplashDeleg(int delkaZobrazeniMS);

	private IContainer components;

	private Timer tmrZavrit;

	internal static void ZobrazitSplash(int delkaZobrazeniMS)
	{
		Splash splash = new Splash(delkaZobrazeniMS);
		splash.ShowDialog();
		splash.Dispose();
	}

	public Splash(int delkaZobrazeniMS)
	{
		InitializeComponent();
		Text = Texty._JmenoMB;
		CenterToScreen();
		VykreslitObrazekSplash();
		tmrZavrit.Interval = delkaZobrazeniMS;
		base.Opacity = 0.0;
	}

	private void VykreslitObrazekSplash()
	{
		Size size = DisplayRectangle.Size;
		Bitmap bitmap = new Bitmap(size.Width, size.Height);
		Graphics g = Graphics.FromImage(bitmap);
		Vykreslit(g);
		BackgroundImage = bitmap;
	}

	private void Vykreslit(Graphics G)
	{
		G.CopyFromScreen(base.Location, new Point(0, 0), base.Size);
		VykreslitSplash(G, DisplayRectangle);
	}

	public static void VykreslitSplash(Graphics G, Rectangle rct)
	{
		G.DrawImage(Grafika.pngSplash, rct);
	}

	private void tmrZavrit_Tick(object sender, EventArgs e)
	{
		Zavrit();
	}

	private void Splash_Click(object sender, EventArgs e)
	{
		Zavrit();
	}

	private void Splash_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Space)
		{
			Zavrit();
		}
	}

	private void Zavrit()
	{
		tmrZavrit.Stop();
		Close();
	}

	protected override void OnShown(EventArgs e)
	{
		base.OnShown(e);
		SpecialniGrafika.FadeIn(this);
		tmrZavrit.Start();
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		SpecialniGrafika.FadeOut(this);
		base.OnFormClosing(e);
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
		this.tmrZavrit = new System.Windows.Forms.Timer(this.components);
		base.SuspendLayout();
		this.tmrZavrit.Interval = 2000;
		this.tmrZavrit.Tick += new System.EventHandler(tmrZavrit_Tick);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(490, 298);
		base.ControlBox = false;
		this.DoubleBuffered = true;
		this.ForeColor = System.Drawing.Color.White;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = HYL.MountBlue.Resources.Grafika.icoMountBlue;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Splash";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Click += new System.EventHandler(Splash_Click);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(Splash_KeyDown);
		base.ResumeLayout(false);
	}
}
