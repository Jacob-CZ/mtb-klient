using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using HYL.MountBlue.Classes.Klavesnice;
using HYL.MountBlue.Classes.Uzivatele.Uzivatel;
using HYL.MountBlue.Resources;

namespace HYL.MountBlue.Classes.Lekce;

internal class Klavesy : IEnumerable<Klavesa>, IEnumerable
{
	internal struct sLekceKlavesy
	{
		public int Lekce;

		public string Text;
	}

	private List<Klavesa> klavesy;

	internal Klavesa this[int index] => klavesy[index];

	public Klavesy(XmlDocument xmlKlavesy)
	{
		klavesy = new List<Klavesa>();
		try
		{
			XmlElement documentElement = xmlKlavesy.DocumentElement;
			XmlNodeList xmlNodeList = documentElement.SelectNodes("lekce");
			foreach (XmlNode item2 in xmlNodeList)
			{
				int iLekce = int.Parse(item2.Attributes["cislo"].Value);
				foreach (XmlNode item3 in item2.SelectNodes("klavesa"))
				{
					KlavesaKlavesnice.eKlavesy byKlavesa = (KlavesaKlavesnice.eKlavesy)byte.Parse(item3.Attributes["cislo_klavesy"].Value);
					KlavesaKlavesnice.eModifikatory byModifikator = (KlavesaKlavesnice.eModifikatory)byte.Parse(item3.Attributes["modifikator"].Value);
					Prsty.ePrst byPrst = (Prsty.ePrst)byte.Parse(item3.Attributes["prst"].Value);
					bool bShift = item3.Attributes["shift"].Value != "0";
					bool bJeZnama = item3.Attributes["je_znama"].Value != "0";
					Klavesa item = new Klavesa(iLekce, byKlavesa, byModifikator, byPrst, bShift, bJeZnama);
					klavesy.Add(item);
				}
			}
			if (klavesy.Count == 0)
			{
				throw new Exception("Nebyla načtena žádná vyučovaná klávesa.");
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Došlo k chybě při načítání vyučovaných kláves.\n\n" + ex.Message);
		}
	}

	internal bool NajitNadpisApopis(KlavesaKlavesnice.eKlavesy klavesa, out string sNadpis, out string sPopis)
	{
		sNadpis = null;
		sPopis = null;
		using (IEnumerator<Klavesa> enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Klavesa current = enumerator.Current;
				if (current.CisloKlavesy == klavesa && !current.JeZnama)
				{
					sNadpis = current.Nadpis;
					sPopis = string.Format(HYL.MountBlue.Resources.Texty.Klavesy_klavesaXtabor, current.Popis, current.Lekce);
					return true;
				}
			}
		}
		return false;
	}

	internal sLekceKlavesy[] KlavesyDoArrayListu()
	{
		List<sLekceKlavesy> list = new List<sLekceKlavesy>();
		List<string> list2 = new List<string>();
		List<string> list3 = new List<string>();
		int num = -1;
		using (IEnumerator<Klavesa> enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Klavesa current = enumerator.Current;
				if (num != current.Lekce)
				{
					string text = null;
					if (list2.Count > 0)
					{
						text = ZjistiText(list2);
					}
					else if (list3.Count > 0)
					{
						text = ZjistiText(list3);
					}
					if (text != null)
					{
						sLekceKlavesy item = default(sLekceKlavesy);
						item.Lekce = num;
						item.Text = text;
						list.Add(item);
						list2.Clear();
						list3.Clear();
					}
				}
				num = current.Lekce;
				if (!current.JeZnama)
				{
					list2.Add(current.Nadpis);
				}
				else
				{
					list3.Add(current.Nadpis);
				}
			}
		}
		string text2 = null;
		if (list2.Count > 0)
		{
			text2 = ZjistiText(list2);
		}
		else if (list3.Count > 0)
		{
			text2 = ZjistiText(list3);
		}
		if (text2 != null)
		{
			sLekceKlavesy item2 = default(sLekceKlavesy);
			item2.Lekce = num;
			item2.Text = text2;
			list.Add(item2);
		}
		return list.ToArray();
	}

	internal void KlavesyDoListView(ListView lvw, Student student)
	{
		lvw.Items.Clear();
		lvw.Groups.Clear();
		sLekceKlavesy[] array = KlavesyDoArrayListu();
		ListViewGroup group = new ListViewGroup(HYL.MountBlue.Resources.Texty.Klavesy_coUzUmim);
		ListViewGroup group2 = new ListViewGroup(HYL.MountBlue.Resources.Texty.Klavesy_coMeCeka);
		lvw.Groups.Add(group);
		lvw.Groups.Add(group2);
		int lekce = student.Cviceni.Lekce;
		bool vyukaDokoncena = student.VyukaDokoncena;
		sLekceKlavesy[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			sLekceKlavesy sLekceKlavesy = array2[i];
			ListViewItem listViewItem = new ListViewItem();
			listViewItem.Text = string.Format(HYL.MountBlue.Resources.Texty.Klavesy_Xtabor, sLekceKlavesy.Lekce);
			listViewItem.SubItems.Add(sLekceKlavesy.Text);
			if (vyukaDokoncena || sLekceKlavesy.Lekce < lekce)
			{
				listViewItem.Group = group;
			}
			else
			{
				listViewItem.Group = group2;
			}
			lvw.Items.Add(listViewItem);
		}
	}

	private string ZjistiText(List<string> mlstKlavesy)
	{
		if (mlstKlavesy.Count == 0)
		{
			return string.Empty;
		}
		if (mlstKlavesy.Count == 1)
		{
			return mlstKlavesy[0];
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < mlstKlavesy.Count - 1; i++)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(HYL.MountBlue.Resources.Texty.Klavesy_carka);
			}
			if (i > 0)
			{
				stringBuilder.Append(ZmensitPrvniPismeno(mlstKlavesy[i]));
			}
			else
			{
				stringBuilder.Append(mlstKlavesy[i]);
			}
		}
		stringBuilder.Append(HYL.MountBlue.Resources.Texty.Klavesy_spojkaA);
		stringBuilder.Append(ZmensitPrvniPismeno(mlstKlavesy[mlstKlavesy.Count - 1]));
		return stringBuilder.ToString();
	}

	private string ZmensitPrvniPismeno(string text)
	{
		if (text.Length < 2)
		{
			return text;
		}
		return text.ToLower()[0] + text.Substring(1);
	}

	public IEnumerator<Klavesa> GetEnumerator()
	{
		return klavesy.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return klavesy.GetEnumerator();
	}
}
