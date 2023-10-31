using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace HYL.MountBlue.Classes.Grafika;

internal static class CestaNaHoru
{
	internal struct LekceNaMape
	{
		internal Point[] cara;

		internal Point tecka;
	}

	internal const int MaxLekciNaHore = 46;

	internal const int PolomerTecky = 4;

	private static LekceNaMape[] lekce;

	static CestaNaHoru()
	{
		lekce = new LekceNaMape[47];
		lekce[1].tecka = new Point(102, 680);
		lekce[2].cara = new Point[4]
		{
			new Point(93, 680),
			new Point(64, 675),
			new Point(46, 653),
			new Point(66, 638)
		};
		lekce[2].tecka = new Point(74, 634);
		lekce[3].cara = new Point[4]
		{
			new Point(81, 631),
			new Point(87, 626),
			new Point(122, 629),
			new Point(134, 626)
		};
		lekce[3].tecka = new Point(141, 625);
		lekce[4].cara = new Point[4]
		{
			new Point(150, 624),
			new Point(180, 624),
			new Point(187, 618),
			new Point(195, 618)
		};
		lekce[4].tecka = new Point(203, 617);
		lekce[5].cara = new Point[4]
		{
			new Point(211, 616),
			new Point(232, 611),
			new Point(260, 607),
			new Point(275, 606)
		};
		lekce[5].tecka = new Point(282, 606);
		lekce[6].cara = new Point[4]
		{
			new Point(291, 604),
			new Point(307, 600),
			new Point(333, 604),
			new Point(345, 605)
		};
		lekce[6].tecka = new Point(354, 605);
		lekce[7].cara = new Point[4]
		{
			new Point(362, 606),
			new Point(378, 607),
			new Point(395, 606),
			new Point(406, 608)
		};
		lekce[7].tecka = new Point(414, 610);
		lekce[8].cara = new Point[4]
		{
			new Point(422, 609),
			new Point(438, 611),
			new Point(454, 610),
			new Point(466, 610)
		};
		lekce[8].tecka = new Point(475, 610);
		lekce[9].cara = new Point[4]
		{
			new Point(482, 609),
			new Point(492, 608),
			new Point(518, 607),
			new Point(527, 607)
		};
		lekce[9].tecka = new Point(536, 607);
		lekce[10].cara = new Point[4]
		{
			new Point(543, 606),
			new Point(555, 607),
			new Point(577, 603),
			new Point(587, 601)
		};
		lekce[10].tecka = new Point(595, 601);
		lekce[11].cara = new Point[4]
		{
			new Point(602, 598),
			new Point(606, 593),
			new Point(636, 593),
			new Point(647, 595)
		};
		lekce[11].tecka = new Point(654, 596);
		lekce[12].cara = new Point[4]
		{
			new Point(662, 596),
			new Point(688, 598),
			new Point(695, 595),
			new Point(697, 597)
		};
		lekce[12].tecka = new Point(705, 598);
		lekce[13].cara = new Point[4]
		{
			new Point(713, 598),
			new Point(718, 599),
			new Point(751, 596),
			new Point(756, 589)
		};
		lekce[13].tecka = new Point(760, 584);
		lekce[14].cara = new Point[4]
		{
			new Point(767, 577),
			new Point(780, 561),
			new Point(805, 565),
			new Point(806, 557)
		};
		lekce[14].tecka = new Point(811, 551);
		lekce[15].cara = new Point[4]
		{
			new Point(806, 545),
			new Point(777, 516),
			new Point(747, 534),
			new Point(736, 533)
		};
		lekce[15].tecka = new Point(729, 535);
		lekce[16].cara = new Point[4]
		{
			new Point(720, 536),
			new Point(682, 543),
			new Point(640, 529),
			new Point(629, 521)
		};
		lekce[16].tecka = new Point(622, 519);
		lekce[17].cara = new Point[4]
		{
			new Point(615, 519),
			new Point(593, 533),
			new Point(554, 537),
			new Point(542, 537)
		};
		lekce[17].tecka = new Point(535, 536);
		lekce[18].cara = new Point[4]
		{
			new Point(525, 535),
			new Point(515, 533),
			new Point(474, 540),
			new Point(461, 538)
		};
		lekce[18].tecka = new Point(453, 539);
		lekce[19].cara = new Point[4]
		{
			new Point(445, 539),
			new Point(430, 537),
			new Point(410, 542),
			new Point(401, 541)
		};
		lekce[19].tecka = new Point(394, 540);
		lekce[20].cara = new Point[4]
		{
			new Point(385, 539),
			new Point(373, 535),
			new Point(347, 529),
			new Point(321, 530)
		};
		lekce[20].tecka = new Point(314, 529);
		lekce[21].cara = new Point[4]
		{
			new Point(306, 529),
			new Point(301, 529),
			new Point(252, 527),
			new Point(241, 528)
		};
		lekce[21].tecka = new Point(233, 528);
		lekce[22].cara = new Point[4]
		{
			new Point(225, 529),
			new Point(219, 530),
			new Point(200, 532),
			new Point(182, 521)
		};
		lekce[23].cara = new Point[4]
		{
			new Point(176, 499),
			new Point(201, 457),
			new Point(156, 465),
			new Point(203, 421)
		};
		lekce[23].tecka = new Point(208, 418);
		lekce[24].cara = new Point[4]
		{
			new Point(217, 417),
			new Point(236, 416),
			new Point(278, 438),
			new Point(290, 437)
		};
		lekce[24].tecka = new Point(297, 437);
		lekce[25].cara = new Point[4]
		{
			new Point(305, 436),
			new Point(316, 434),
			new Point(361, 443),
			new Point(390, 432)
		};
		lekce[25].tecka = new Point(397, 431);
		lekce[26].cara = new Point[4]
		{
			new Point(404, 428),
			new Point(414, 424),
			new Point(426, 426),
			new Point(429, 425)
		};
		lekce[26].tecka = new Point(437, 424);
		lekce[27].cara = new Point[4]
		{
			new Point(445, 424),
			new Point(453, 423),
			new Point(475, 434),
			new Point(479, 433)
		};
		lekce[27].tecka = new Point(487, 434);
		lekce[28].cara = new Point[4]
		{
			new Point(495, 435),
			new Point(513, 441),
			new Point(560, 440),
			new Point(569, 440)
		};
		lekce[28].tecka = new Point(577, 439);
		lekce[29].cara = new Point[4]
		{
			new Point(585, 438),
			new Point(603, 432),
			new Point(622, 424),
			new Point(625, 420)
		};
		lekce[29].tecka = new Point(631, 415);
		lekce[30].cara = new Point[4]
		{
			new Point(635, 408),
			new Point(642, 389),
			new Point(639, 375),
			new Point(635, 375)
		};
		lekce[30].tecka = new Point(628, 374);
		lekce[31].cara = new Point[4]
		{
			new Point(620, 376),
			new Point(609, 383),
			new Point(563, 392),
			new Point(547, 392)
		};
		lekce[31].tecka = new Point(541, 392);
		lekce[32].cara = new Point[4]
		{
			new Point(534, 393),
			new Point(532, 393),
			new Point(530, 397),
			new Point(531, 398)
		};
		lekce[32].tecka = new Point(528, 404);
		lekce[33].cara = new Point[4]
		{
			new Point(521, 403),
			new Point(519, 403),
			new Point(514, 400),
			new Point(508, 397)
		};
		lekce[33].tecka = new Point(502, 394);
		lekce[34].cara = new Point[4]
		{
			new Point(494, 390),
			new Point(486, 388),
			new Point(474, 368),
			new Point(412, 375)
		};
		lekce[34].tecka = new Point(405, 377);
		lekce[35].cara = new Point[4]
		{
			new Point(397, 377),
			new Point(365, 378),
			new Point(332, 370),
			new Point(332, 370)
		};
		lekce[35].tecka = new Point(326, 369);
		lekce[36].cara = new Point[4]
		{
			new Point(318, 367),
			new Point(311, 354),
			new Point(295, 358),
			new Point(287, 354)
		};
		lekce[36].tecka = new Point(281, 349);
		lekce[37].cara = new Point[4]
		{
			new Point(281, 340),
			new Point(283, 335),
			new Point(293, 327),
			new Point(299, 326)
		};
		lekce[38].cara = new Point[4]
		{
			new Point(324, 324),
			new Point(340, 326),
			new Point(383, 326),
			new Point(387, 318)
		};
		lekce[38].tecka = new Point(392, 313);
		lekce[39].cara = new Point[4]
		{
			new Point(398, 307),
			new Point(418, 280),
			new Point(394, 282),
			new Point(394, 282)
		};
		lekce[39].tecka = new Point(386, 283);
		lekce[40].cara = new Point[4]
		{
			new Point(378, 282),
			new Point(367, 285),
			new Point(344, 283),
			new Point(333, 283)
		};
		lekce[40].tecka = new Point(327, 282);
		lekce[41].cara = new Point[4]
		{
			new Point(319, 283),
			new Point(316, 283),
			new Point(306, 283),
			new Point(301, 281)
		};
		lekce[41].tecka = new Point(294, 277);
		lekce[42].cara = new Point[4]
		{
			new Point(302, 273),
			new Point(309, 270),
			new Point(308, 254),
			new Point(314, 252)
		};
		lekce[43].cara = new Point[4]
		{
			new Point(326, 243),
			new Point(328, 239),
			new Point(350, 246),
			new Point(360, 242)
		};
		lekce[43].tecka = new Point(369, 239);
		lekce[44].cara = new Point[4]
		{
			new Point(367, 233),
			new Point(365, 229),
			new Point(349, 233),
			new Point(343, 229)
		};
		lekce[44].tecka = new Point(337, 225);
		lekce[45].cara = new Point[4]
		{
			new Point(338, 218),
			new Point(338, 213),
			new Point(345, 210),
			new Point(346, 206)
		};
		lekce[45].tecka = new Point(351, 201);
		lekce[46].cara = new Point[4]
		{
			new Point(352, 193),
			new Point(348, 189),
			new Point(347, 182),
			new Point(349, 180)
		};
		lekce[46].tecka = new Point(351, 174);
	}

	internal static void VykreslitCestu(Graphics G, int iDosazenaLekce, bool bPosledniLekceJinouBarvou)
	{
		Pen pen = new Pen(Barva.CestaNaHoru, 2.5f);
		Brush brush = new SolidBrush(Barva.CestaNaHoru);
		pen.StartCap = LineCap.Round;
		pen.EndCap = LineCap.Round;
		pen.DashCap = DashCap.Round;
		pen.DashStyle = DashStyle.Dash;
		int num = Math.Min(lekce.GetUpperBound(0), iDosazenaLekce);
		if (bPosledniLekceJinouBarvou)
		{
			num--;
		}
		for (int i = lekce.GetLowerBound(0); i <= num; i++)
		{
			LekceNaMape lekceNaMape = (LekceNaMape)lekce.GetValue(i);
			if (lekceNaMape.cara != null && lekceNaMape.cara.Length == 4)
			{
				G.DrawBezier(pen, lekceNaMape.cara[0], lekceNaMape.cara[1], lekceNaMape.cara[2], lekceNaMape.cara[3]);
			}
			if (!lekceNaMape.tecka.IsEmpty)
			{
				G.FillEllipse(brush, new Rectangle(lekceNaMape.tecka.X - 4, lekceNaMape.tecka.Y - 4, 8, 8));
			}
		}
		if (bPosledniLekceJinouBarvou)
		{
			LekceNaMape lekceNaMape2 = (LekceNaMape)lekce.GetValue(++num);
			Brush brush2 = new SolidBrush(Barva.CestaNaHoruPosledniLekce);
			Pen pen2 = (Pen)pen.Clone();
			pen2.Color = Barva.CestaNaHoruPosledniLekce;
			if (lekceNaMape2.cara != null && lekceNaMape2.cara.Length == 4)
			{
				G.DrawBezier(pen2, lekceNaMape2.cara[0], lekceNaMape2.cara[1], lekceNaMape2.cara[2], lekceNaMape2.cara[3]);
			}
			if (!lekceNaMape2.tecka.IsEmpty)
			{
				G.FillEllipse(brush2, new Rectangle(lekceNaMape2.tecka.X - 4, lekceNaMape2.tecka.Y - 4, 8, 8));
			}
		}
	}
}
