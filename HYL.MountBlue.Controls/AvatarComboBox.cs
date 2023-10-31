using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HYL.MountBlue.Controls;

public class AvatarComboBox : ComboBox
{
	private IContainer components;

	public AvatarComboBox()
	{
		InitializeComponent();
	}

	protected override void OnDrawItem(DrawItemEventArgs e)
	{
		base.OnDrawItem(e);
		if (e.Index >= 0 && e.Index < base.Items.Count && base.Items[e.Index] is AvatarPolozka)
		{
			AvatarPolozka avatarPolozka = (AvatarPolozka)base.Items[e.Index];
			Rectangle bounds = e.Bounds;
			e.Graphics.FillRectangle(new SolidBrush(Color.White), bounds);
			bounds.Location = new Point((bounds.Width - bounds.Height) / 2, bounds.Top);
			bounds.Width = base.ItemHeight;
			e.Graphics.DrawImage(avatarPolozka.Avatar, bounds);
			if ((e.State & DrawItemState.Selected) > DrawItemState.None && (e.State & DrawItemState.ComboBoxEdit) == 0)
			{
				Pen pen = new Pen(Color.Black, 1f);
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
		base.DropDownHeight = 300;
		base.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		base.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Font = new System.Drawing.Font("Arial", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 238);
		this.ForeColor = System.Drawing.Color.White;
		base.IntegralHeight = false;
		base.ItemHeight = 64;
		base.MaxDropDownItems = 20;
		base.Size = new System.Drawing.Size(121, 70);
		base.ResumeLayout(false);
	}
}
