using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HYL.MountBlue.Controls;

public class Klasifikace : UserControl
{
	private IContainer components;

	public Klasifikace()
	{
		InitializeComponent();
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
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.Font = new System.Drawing.Font("Arial", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 238);
		this.ForeColor = System.Drawing.Color.Black;
		base.Name = "Klasifikace";
		base.Size = new System.Drawing.Size(509, 173);
		base.ResumeLayout(false);
	}
}
