using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace HYL.MountBlue.Controls;

internal class Posuvnik : VScrollBar
{
	private delegate void DelegateNovaHodnota(int hodnota);

	private bool mbolPouzeProCteni;

	private bool mbolManualniZmenaHodnoty;

	private int mintHodnota;

	private IContainer components;

	internal bool PouzeProCteni
	{
		get
		{
			return mbolPouzeProCteni;
		}
		set
		{
			mbolPouzeProCteni = value;
		}
	}

	internal void NovaHodnota(int hodnota)
	{
		if (base.InvokeRequired)
		{
			DelegateNovaHodnota method = NovaHodnota;
			Invoke(method, hodnota);
		}
		else if (hodnota >= base.Minimum && hodnota <= base.Maximum)
		{
			mbolManualniZmenaHodnoty = true;
			mintHodnota = hodnota;
			base.Value = hodnota;
			mbolManualniZmenaHodnoty = false;
		}
	}

	protected override void OnScroll(ScrollEventArgs se)
	{
		if (mbolPouzeProCteni)
		{
			se.NewValue = se.OldValue;
		}
		else
		{
			base.OnScroll(se);
		}
	}

	protected override void OnValueChanged(EventArgs e)
	{
		base.OnValueChanged(e);
		if (!mbolManualniZmenaHodnoty)
		{
			if (mbolPouzeProCteni)
			{
				base.Value = mintHodnota;
			}
			else
			{
				mintHodnota = base.Value;
			}
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
		base.SuspendLayout();
		base.Name = "uPosuvnik";
		base.Size = new System.Drawing.Size(405, 332);
		base.ResumeLayout(false);
	}
}
