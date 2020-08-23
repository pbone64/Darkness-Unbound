using Terraria;
using Terraria.Localization;

namespace DarknessUnbound.Helpers
{
    /// <summary>
    /// Utilities for detours.
    /// </summary>
    internal static class DetourUtils
	{
		internal static string ValueToName(int coinValue)
		{
			int num10 = 0;
			int num9 = 0;
			int num8 = 0;
			int num7 = 0;
			int num6 = 0;
			string text2 = "";
			int num5 = coinValue;
			while (num5 > 0)
			{
				if (num5 >= 100000000)
				{
					num5 -= 100000000;
					num10++;
				}
				if (num5 >= 1000000)
				{
					num5 -= 1000000;
					num9++;
				}
				else if (num5 >= 10000)
				{
					num5 -= 10000;
					num8++;
				}
				else if (num5 >= 100)
				{
					num5 -= 100;
					num7++;
				}
				else if (num5 >= 1)
				{
					num5--;
					num6++;
				}
			}
			text2 = "";
			if (num10 > 0)
			{
				text2 = text2 + num10.ToString() + string.Format(" {0} ", "Hellstone");
			}
			if (num9 > 0)
			{
				text2 = text2 + num9.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));
			}
			if (num8 > 0)
			{
				text2 = text2 + num8.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));
			}
			if (num7 > 0)
			{
				text2 = text2 + num7.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));
			}
			if (num6 > 0)
			{
				text2 = text2 + num6.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
			}
			if (text2.Length > 1)
			{
				text2 = text2.Substring(0, text2.Length - 1);
			}
			return text2;
		}

		internal static void ValueToName(this ItemText itemText, int i)
		{
			int num10 = 0;
			int num9 = 0;
			int num8 = 0;
			int num7 = 0;
			int num6 = 0;
			int num5 = Main.itemText[i].coinValue;
			while (num5 > 0)
			{
				if (num5 >= 100000000)
				{
					num5 -= 100000000;
					num10++;
				}
				if (num5 >= 1000000)
				{
					num5 -= 1000000;
					num9++;
				}
				else if (num5 >= 10000)
				{
					num5 -= 10000;
					num8++;
				}
				else if (num5 >= 100)
				{
					num5 -= 100;
					num7++;
				}
				else if (num5 >= 1)
				{
					num5--;
					num6++;
				}
			}
			Main.itemText[i].name = "";
			if (num10 > 0)
			{
				Main.itemText[i].name = Main.itemText[i].name + num10.ToString() + string.Format(" {0} ", "Hellstone");
			}
			if (num9 > 0)
			{
				Main.itemText[i].name = Main.itemText[i].name + num9.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Platinum"));
			}
			if (num8 > 0)
			{
				Main.itemText[i].name = Main.itemText[i].name + num8.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Gold"));
			}
			if (num7 > 0)
			{
				Main.itemText[i].name = Main.itemText[i].name + num7.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Silver"));
			}
			if (num6 > 0)
			{
				Main.itemText[i].name = Main.itemText[i].name + num6.ToString() + string.Format(" {0} ", Language.GetTextValue("Currency.Copper"));
			}
			if (Main.itemText[i].name.Length > 1)
			{
				Main.itemText[i].name = Main.itemText[i].name.Substring(0, Main.itemText[i].name.Length - 1);
			}
		}
	}
}
