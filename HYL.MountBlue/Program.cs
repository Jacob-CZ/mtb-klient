using System;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;
using HYL.MountBlue.Classes;
using HYL.MountBlue.Classes.Klient;
using HYL.MountBlue.Classes.Lekce;
using HYL.MountBlue.Classes.Plocha;
using HYL.MountBlue.Classes.Uzivatele.Prihlasen;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Forms;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue;

internal static class Program
{
	[STAThread]
	private static int Main(string[] args)
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.ThreadException += Application_ThreadException;
		AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		HYL.MountBlue.Classes.Klient.NastaveniSite nastaveniSite = HYL.MountBlue.Classes.Klient.NastaveniSite.NacistNastaveniSite();
		if (nastaveniSite == null && !HYL.MountBlue.Classes.Klient.NastaveniSite.ZobrazitDialogNastaveni())
		{
			return 2;
		}
		SingleInstance singleInstance = new SingleInstance("0217f2cc-bb5c-4cea-8a90-47ab8b7b8c7f");
		if (singleInstance.IsRunning)
		{
			MsgBoxMB.ZobrazitMessageBox(Texty._JenJednaInstanceMB, Application.ProductName, eMsgBoxTlacitka.OK);
		}
		else
		{
			Splash.ZobrazitSplashDeleg zobrazitSplashDeleg = Splash.ZobrazitSplash;
			IAsyncResult result = zobrazitSplashDeleg.BeginInvoke(3000, null, null);
			Klient.InicializovatKlienta();
			_Lekce.InicializovatLekce();
			zobrazitSplashDeleg.EndInvoke(result);
			if (Klient.Stanice != null)
			{
				_Plocha.HlavniOkno = new HlavniOkno();
				PUzivatele.ZobrazitPrihlaseni();
				Application.Run(_Plocha.HlavniOkno);
				_Plocha.HlavniOkno.Dispose();
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			singleInstance.ReleaseMutex();
		}
		return 0;
	}

	private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		if (e.ExceptionObject is Exception)
		{
			ChybaAplikace.ZobrazitChybu((Exception)e.ExceptionObject);
		}
	}

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
	{
		if ((e.Exception is SocketException || e.Exception is RemotingException) && ChybaAplikace.ZobrazitOknoRestartovat(Texty.ChybaAplikace_msgChybaServer + "\n\n"))
		{
			Application.Restart();
		}
		else
		{
			ChybaAplikace.ZobrazitChybu(e.Exception);
		}
	}
}
