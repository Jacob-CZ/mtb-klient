using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HYL.MountBlue.Controls;

public class ColorComboBox : ComboBox
{
	private readonly StringFormat fmt;

	private IContainer components;

	public ColorComboBox()
	{
		InitializeComponent();
		fmt = new StringFormat();
		fmt.Alignment = StringAlignment.Center;
		fmt.LineAlignment = StringAlignment.Center;
	}

	protected override void OnDrawItem(DrawItemEventArgs e)
	{
		base.OnDrawItem(e);
		if (e.Index >= 0 && e.Index < base.Items.Count && base.Items[e.Index] is BarvaPolozka)
		{
			BarvaPolozka barvaPolozka = (BarvaPolozka)base.Items[e.Index];
			Rectangle bounds = e.Bounds;
			bounds.Inflate(-2, -2);
			e.Graphics.FillRectangle(new SolidBrush(barvaPolozka.Barva), bounds);
			e.Graphics.DrawString(barvaPolozka.JmenoBarvy, Font, new SolidBrush(barvaPolozka.BarvaTextuPolozky), bounds, fmt);
			if ((e.State & DrawItemState.Selected) > DrawItemState.None && (e.State & DrawItemState.ComboBoxEdit) == 0)
			{
				Pen pen = new Pen(barvaPolozka.BarvaTextuPolozky, 1f);
				bounds.Inflate(-4, -4);
				e.Graphics.DrawRectangle(pen, bounds);
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
		base.SuspendLayout();
		this.BackColor = System.Drawing.Color.White;
		base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
		base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		base.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Font = new System.Drawing.Font("Arial", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.ForeColor = System.Drawing.Color.White;
		base.ItemHeight = 24;
		base.MaxDropDownItems = 20;
		base.Size = new System.Drawing.Size(121, 30);
		base.ResumeLayout(false);
	}
}
