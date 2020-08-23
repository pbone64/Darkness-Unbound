using DarknessUnbound.Helpers;
using DarknessUnbound.Items.Currency;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DarknessUnbound
{
    public partial class DarknessUnbound : Mod
    {
        private void Detours() => On.Terraria.ItemText.NewText += ItemText_NewText;

        private void ItemText_NewText(On.Terraria.ItemText.orig_NewText orig, Terraria.Item newItem, int stack, bool noStack, bool longText)
        {
			bool flag = newItem.type == ModContent.ItemType<HellstoneCoin>() || (newItem.type >= 71 && newItem.type <= 74);
			if (!Main.showItemText || newItem.Name == null || !newItem.active || Main.netMode == 2)
			{
				return;
			}
			for (int k = 0; k < 20; k++)
			{
				if ((!Main.itemText[k].active || (!(Main.itemText[k].name == newItem.AffixName()) && (!flag || !Main.itemText[k].coinText)) || Main.itemText[k].NoStack) | noStack)
				{
					continue;
				}
				string text3 = newItem.Name + " (" + (Main.itemText[k].stack + stack).ToString() + ")";
				string text2 = newItem.Name;
				if (Main.itemText[k].stack > 1)
				{
					text2 = text2 + " (" + Main.itemText[k].stack.ToString() + ")";
				}
				Vector2 vector2 = Main.fontMouseText.MeasureString(text2);
				vector2 = Main.fontMouseText.MeasureString(text3);
				if (Main.itemText[k].lifeTime < 0)
				{
					Main.itemText[k].scale = 1f;
				}
				if (Main.itemText[k].lifeTime < 60)
				{
					Main.itemText[k].lifeTime = 60;
				}
				if (flag && Main.itemText[k].coinText)
				{
					int num = 0;
					if (newItem.type == 71)
					{
						num += newItem.stack;
					}
					else if (newItem.type == 72)
					{
						num += 100 * newItem.stack;
					}
					else if (newItem.type == 73)
					{
						num += 10000 * newItem.stack;
					}
					else if (newItem.type == 74)
					{
						num += 1000000 * newItem.stack;
					}
					else if (newItem.type == ModContent.ItemType<HellstoneCoin>())
					{
						num += 100000000 * newItem.stack;
					}
					Main.itemText[k].coinValue += num;
					text3 = DetourUtils.ValueToName(Main.itemText[k].coinValue);
					vector2 = Main.fontMouseText.MeasureString(text3);
					Main.itemText[k].name = text3;
					if (Main.itemText[k].coinValue >= 100000000)
					{
						if (Main.itemText[k].lifeTime < 440)
						{
							Main.itemText[k].lifeTime = 440;
						}
						Main.itemText[k].color = Color.DarkOrange;
					}
					else if (Main.itemText[k].coinValue >= 1000000)
					{
						if (Main.itemText[k].lifeTime < 300)
						{
							Main.itemText[k].lifeTime = 300;
						}
						Main.itemText[k].color = new Color(220, 220, 198);
					}
					else if (Main.itemText[k].coinValue >= 10000)
					{
						if (Main.itemText[k].lifeTime < 240)
						{
							Main.itemText[k].lifeTime = 240;
						}
						Main.itemText[k].color = new Color(224, 201, 92);
					}
					else if (Main.itemText[k].coinValue >= 100)
					{
						if (Main.itemText[k].lifeTime < 180)
						{
							Main.itemText[k].lifeTime = 180;
						}
						Main.itemText[k].color = new Color(181, 192, 193);
					}
					else if (Main.itemText[k].coinValue >= 1)
					{
						if (Main.itemText[k].lifeTime < 120)
						{
							Main.itemText[k].lifeTime = 120;
						}
						Main.itemText[k].color = new Color(246, 138, 96);
					}
				}
				Main.itemText[k].stack += stack;
				Main.itemText[k].scale = 0f;
				Main.itemText[k].rotation = 0f;
				Main.itemText[k].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector2.X * 0.5f;
				Main.itemText[k].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector2.Y * 0.5f;
				Main.itemText[k].velocity.Y = -7f;
				if (Main.itemText[k].coinText)
				{
					Main.itemText[k].stack = 1;
				}
				return;
			}
			int num4 = -1;
			for (int j = 0; j < 20; j++)
			{
				if (!Main.itemText[j].active)
				{
					num4 = j;
					break;
				}
			}
			if (num4 == -1)
			{
				double num3 = Main.bottomWorld;
				for (int i = 0; i < 20; i++)
				{
					if (num3 > (double)Main.itemText[i].position.Y)
					{
						num4 = i;
						num3 = Main.itemText[i].position.Y;
					}
				}
			}
			if (num4 < 0)
			{
				return;
			}
			string text4 = newItem.AffixName();
			if (stack > 1)
			{
				text4 = text4 + " (" + stack.ToString() + ")";
			}
			Vector2 vector3 = Main.fontMouseText.MeasureString(text4);
			Main.itemText[num4].alpha = 1f;
			Main.itemText[num4].alphaDir = -1;
			Main.itemText[num4].active = true;
			Main.itemText[num4].scale = 0f;
			Main.itemText[num4].NoStack = noStack;
			Main.itemText[num4].rotation = 0f;
			Main.itemText[num4].position.X = newItem.position.X + (float)newItem.width * 0.5f - vector3.X * 0.5f;
			Main.itemText[num4].position.Y = newItem.position.Y + (float)newItem.height * 0.25f - vector3.Y * 0.5f;
			Main.itemText[num4].color = Color.White;
			if (newItem.rare == 1)
			{
				Main.itemText[num4].color = new Color(150, 150, 255);
			}
			else if (newItem.rare == 2)
			{
				Main.itemText[num4].color = new Color(150, 255, 150);
			}
			else if (newItem.rare == 3)
			{
				Main.itemText[num4].color = new Color(255, 200, 150);
			}
			else if (newItem.rare == 4)
			{
				Main.itemText[num4].color = new Color(255, 150, 150);
			}
			else if (newItem.rare == 5)
			{
				Main.itemText[num4].color = new Color(255, 150, 255);
			}
			else if (newItem.rare == -11)
			{
				Main.itemText[num4].color = new Color(255, 175, 0);
			}
			else if (newItem.rare == -1)
			{
				Main.itemText[num4].color = new Color(130, 130, 130);
			}
			else if (newItem.rare == 6)
			{
				Main.itemText[num4].color = new Color(210, 160, 255);
			}
			else if (newItem.rare == 7)
			{
				Main.itemText[num4].color = new Color(150, 255, 10);
			}
			else if (newItem.rare == 8)
			{
				Main.itemText[num4].color = new Color(255, 255, 10);
			}
			else if (newItem.rare == 9)
			{
				Main.itemText[num4].color = new Color(5, 200, 255);
			}
			else if (newItem.rare == 10)
			{
				Main.itemText[num4].color = new Color(255, 40, 100);
			}
			else if (newItem.rare >= 11)
			{
				Main.itemText[num4].color = new Color(180, 40, 255);
			}
			Main.itemText[num4].expert = newItem.expert;
			Main.itemText[num4].name = newItem.AffixName();
			Main.itemText[num4].stack = stack;
			Main.itemText[num4].velocity.Y = -7f;
			Main.itemText[num4].lifeTime = 60;
			if (longText)
			{
				Main.itemText[num4].lifeTime *= 5;
			}
			Main.itemText[num4].coinValue = 0;
			Main.itemText[num4].coinText = newItem.type == ModContent.ItemType<HellstoneCoin>() || (newItem.type >= 71 && newItem.type <= 74);
			if (!Main.itemText[num4].coinText)
			{
				return;
			}
			if (newItem.type == 71)
			{
				Main.itemText[num4].coinValue += Main.itemText[num4].stack;
			}
			else if (newItem.type == 72)
			{
				Main.itemText[num4].coinValue += 100 * Main.itemText[num4].stack;
			}
			else if (newItem.type == 73)
			{
				Main.itemText[num4].coinValue += 10000 * Main.itemText[num4].stack;
			}
			else if (newItem.type == 74)
			{
				Main.itemText[num4].coinValue += 1000000 * Main.itemText[num4].stack;
			}
			else if (newItem.type == ModContent.ItemType<HellstoneCoin>())
			{
				Main.itemText[num4].coinValue += 100000000 * Main.itemText[num4].stack;
			}
			Main.itemText[num4].ValueToName(num4);
			Main.itemText[num4].stack = 1;
			int num2 = num4;
			if (Main.itemText[num2].coinValue >= 100000000)
			{
				if (Main.itemText[num2].lifeTime < 440)
				{
					Main.itemText[num2].lifeTime = 440;
				}
				Main.itemText[num2].color = Color.DarkOrange;
			}
			else if (Main.itemText[num2].coinValue >= 1000000)
			{
				if (Main.itemText[num2].lifeTime < 300)
				{
					Main.itemText[num2].lifeTime = 300;
				}
				Main.itemText[num2].color = new Color(220, 220, 198);
			}
			else if (Main.itemText[num2].coinValue >= 10000)
			{
				if (Main.itemText[num2].lifeTime < 240)
				{
					Main.itemText[num2].lifeTime = 240;
				}
				Main.itemText[num2].color = new Color(224, 201, 92);
			}
			else if (Main.itemText[num2].coinValue >= 100)
			{
				if (Main.itemText[num2].lifeTime < 180)
				{
					Main.itemText[num2].lifeTime = 180;
				}
				Main.itemText[num2].color = new Color(181, 192, 193);
			}
			else if (Main.itemText[num2].coinValue >= 1)
			{
				if (Main.itemText[num2].lifeTime < 120)
				{
					Main.itemText[num2].lifeTime = 120;
				}
				Main.itemText[num2].color = new Color(246, 138, 96);
			}
		}
	}
}
