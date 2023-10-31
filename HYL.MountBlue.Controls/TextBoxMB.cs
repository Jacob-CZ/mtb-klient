using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace HYL.MountBlue.Controls;

internal class TextBoxMB : UserControl
{
	internal delegate void HodnotaPosuvnikuZmenenaEventHandler();

	private IContainer components;

	internal Posuvnik Posuvnik;

	internal event HodnotaPosuvnikuZmenenaEventHandler HodnotaPosuvnikuZmenena;

	public TextBoxMB()
	{
		InitializeComponent();
		SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, value: true);
	}

	private void Posuvnik_ValueChanged(object sender, EventArgs e)
	{
		if (!Posuvnik.PouzeProCteni && this.HodnotaPosuvnikuZmenena != null)
		{
			this.HodnotaPosuvnikuZmenena();
		}
	}

	private void TextBoxMB_MouseWheel(object sender, MouseEventArgs e)
	{
		if (!Posuvnik.PouzeProCteni)
		{
			int num = Posuvnik.Value;
			if (e.Delta > 0)
			{
				num -= Posuvnik.LargeChange / 5;
			}
			else if (e.Delta < 0)
			{
				num += Posuvnik.LargeChange / 5;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (num > Posuvnik.Maximum - Posuvnik.LargeChange)
			{
				num = Posuvnik.Maximum - Posuvnik.LargeChange;
			}
			Posuvnik.Value = num;
		}
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	[System.Diagnostics.DebuggerStepThrough]
	private void InitializeComponent()
	{
		this.Posuvnik = new HYL.MountBlue.Controls.Posuvnik();
		base.SuspendLayout();
		this.Posuvnik.Cursor = System.Windows.Forms.Cursors.Arrow;
		this.Posuvnik.Dock = System.Windows.Forms.DockStyle.Right;
		this.Posuvnik.Enabled = false;
		this.Posuvnik.Location = new System.Drawing.Point(383, 0);
		this.Posuvnik.Name = "Posuvnik";
		this.Posuvnik.PouzeProCteni = false;
		this.Posuvnik.Size = new System.Drawing.Size(17, 279);
		this.Posuvnik.TabIndex = 0;
		this.Posuvnik.ValueChanged += new System.EventHandler(Posuvnik_ValueChanged);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.Posuvnik);
		base.Name = "uTextBox";
		base.Size = new System.Drawing.Size(400, 279);
		base.ResumeLayout(false);
		base.MouseWheel += new System.Windows.Forms.MouseEventHandler(TextBoxMB_MouseWheel);
	}
}
