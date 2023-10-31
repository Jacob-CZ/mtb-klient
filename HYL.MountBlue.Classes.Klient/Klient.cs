using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters;
using System.Windows.Forms;
using HYL.MountBlue.Classes.Log;
using HYL.MountBlue.Controls;
using HYL.MountBlue.Dialogs;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Klient;

internal static class Klient
{
	private static Stanice stanice;

	private static ILease itfLeasing;

	private static Timer tmrLeasing;

	internal static Stanice Stanice => stanice;

	internal static void InicializovatKlienta()
	{
		NastaveniSite nastaveniSite = NastaveniSite.NacistNastaveniSite();
		if (nastaveniSite == null)
		{
			return;
		}
		try
		{
			string appUrl = $"tcp://{nastaveniSite.Server}:{nastaveniSite.Port}/MountBlueServer.rem";
			BinaryClientFormatterSinkProvider clientSinkProvider = new BinaryClientFormatterSinkProvider();
			BinaryServerFormatterSinkProvider binaryServerFormatterSinkProvider = new BinaryServerFormatterSinkProvider();
			Hashtable hashtable = new Hashtable();
			binaryServerFormatterSinkProvider.TypeFilterLevel = TypeFilterLevel.Full;
			hashtable["port"] = 0;
			TcpChannel chnl = new TcpChannel(hashtable, clientSinkProvider, binaryServerFormatterSinkProvider);
			ChannelServices.RegisterChannel(chnl, ensureSecurity: false);
			RemotingConfiguration.RegisterActivatedClientType(typeof(Stanice), appUrl);
			stanice = new Stanice();
			itfLeasing = (ILease)RemotingServices.GetLifetimeService(stanice);
		}
		catch (TargetInvocationException ex)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Klient_msgNelzeSePripojitKserveru, ex.Message), HYL.MountBlue.Resources.Texty.Klient_msgNelzeSePripojitKserveru_Title, eMsgBoxTlacitka.OK);
		}
		catch (FileLoadException ex2)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Klient_msgChybaFLE, ex2.Message), HYL.MountBlue.Resources.Texty.Klient_msgNelzeSePripojitKserveru_Title, eMsgBoxTlacitka.OK);
		}
		catch (Exception ex3)
		{
			MsgBoxMB.ZobrazitMessageBox(string.Format(HYL.MountBlue.Resources.Texty.Klient_msgChyba, ex3.Message), HYL.MountBlue.Resources.Texty.Klient_msgNelzeSePripojitKserveru_Title, eMsgBoxTlacitka.OK);
		}
	}

	private static void Uzivatele_Chyba(string text, string zahlavi)
	{
		MsgBoxMB.ZobrazitMessageBox(text, zahlavi, eMsgBoxTlacitka.OK);
		HYL.MountBlue.Classes.Log.Log.ZapsatDoLogu(zahlavi + "–" + text);
	}

	internal static void StartLeasingu()
	{
		tmrLeasing = new Timer();
		tmrLeasing.Interval = 35000;
		tmrLeasing.Tick += tmrLeasing_Tick;
		tmrLeasing.Start();
	}

	private static void tmrLeasing_Tick(object sender, EventArgs e)
	{
		if (itfLeasing != null && itfLeasing.CurrentState == LeaseState.Active && itfLeasing.CurrentLeaseTime.TotalMinutes <= 1.0)
		{
			itfLeasing.Renew(TimeSpan.FromMinutes(2.0));
		}
	}
}
