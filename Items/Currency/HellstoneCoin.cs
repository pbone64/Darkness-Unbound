using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Currency
{
	//TODO: [IL] Terraria.Item.UpdateItem - if (type == ModContent.ItemType<HellstoneCoin>()) num44 = 100000000f;
	//TODO: [Detour(?)] Terraria.itemText.NewText (ValueToName) - Coin text logic for hellstone coin (May be easier and better to detour the method, check if it's a coin, and rewrite the coin text system.)
	//TODO: Clean up Update code (taken from Vanilla).
	//TODO: Make coin item stats only be displayed if the player hsa a Coin Gun in their inventory.
	//TODO: [Detour/IL] Terraria.Main.MoveCoins - Will look at what this does later.
	//TODO: [IL] Terraria.Main.UpdateTime_SpawnTownNPCs - if (player[k].inventory[j].type == ModContent.ItemType<HellstoneCoin>()) num32 += player[k].inventory[j].stack * 100000000;
	//TODO: [IL/Detour] Terraria.Player.SellItem - CBA to go into detail.
	//TODO: [Detour] Terraria.Player.DoCoins - Do coins.
	//TODO: [IL/Detour] Terraria.Player.BuyItem - Some work here as well.
	//TODO: [IL/Detour] (private bool) Terraria.Player.TryPurchasing - I can hook into this easily, so not much to worry about there. Just need to adapt it to allow the use of hellstone coins.
	//TODO: [?] Terraria.Player.BuyItemOld - Not sure if this will require any changes.
	//TODO: [IL] Terraria.Player.ItemSpace - newItem.type == ModContent.ItemType<HellstoneCoin>();
	//TODO: Terraria.Player.GetItem - Check for hellstone coin as well.
	//TODO: Terraria.Player.DropCoins - Need I explain?
	public class HellstoneCoin : DarknessItem
    {
		public override void SetStaticDefaults() => ItemID.Sets.IsAMaterial[item.type] = false;

        public override void SafeSetDefaults()
        {
            item.CloneDefaults(ItemID.PlatinumCoin);
            item.damage = 450;
            item.shootSpeed = 4.5f;
            item.value = 500000000;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
			if (Main.netMode != 2 && Main.expertMode && item.owner == Main.myPlayer)
			{
				Rectangle rectangle3 = new Rectangle((int)item.position.X, (int)item.position.Y, item.width, item.height);
				for (int n = 0; n < 200; n++)
				{
					if (!Main.npc[n].active || Main.npc[n].lifeMax <= 5 || Main.npc[n].friendly || Main.npc[n].immortal || Main.npc[n].dontTakeDamage)
					{
						continue;
					}
					float num43 = item.stack;
					float num44 = 100000000f;
					num43 *= num44;
					float extraValue = Main.npc[n].extraValue;
					int num41 = Main.npc[n].realLife;
					if (num41 >= 0 && Main.npc[num41].active)
					{
						extraValue = Main.npc[num41].extraValue;
					}
					else
					{
						num41 = -1;
					}
					if (!(extraValue < num43))
					{
						continue;
					}
					Rectangle rectangle2 = new Rectangle((int)Main.npc[n].position.X, (int)Main.npc[n].position.Y, Main.npc[n].width, Main.npc[n].height);
					if (rectangle3.Intersects(rectangle2))
					{
						float num40 = (float)Main.rand.Next(50, 76) * 0.01f;
						if (num40 > 1f)
						{
							num40 = 1f;
						}
						int num39 = (int)((float)item.stack * num40);
						if (num39 < 1)
						{
							num39 = 1;
						}
						if (num39 > item.stack)
						{
							num39 = item.stack;
						}
						item.stack -= num39;
						float num38 = (float)num39 * num44;
						int num37 = n;
						if (num41 >= 0)
						{
							num37 = num41;
						}
						Main.npc[num37].extraValue += num38;
						if (Main.netMode == 0)
						{
							Main.npc[num37].moneyPing(item.position);
						}
						else
						{
							NetMessage.SendData(92, -1, -1, null, num37, num38, item.position.X, item.position.Y);
						}
						if (item.stack <= 0)
						{
							SetDefaults();
							item.active = false;
						}
						NetMessage.SendData(21);
					}
				}
			}
		}

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			Color color = Lighting.GetColor((int)((double)item.position.X + (double)item.width * 0.5) / 16, (int)((double)item.position.Y + (double)item.height * 0.5) / 16);
			Texture2D hellstoneCoin = ModContent.GetTexture("DarknessUnbound/Items/Currency/Coin_4");

			if (!Main.gamePaused && Math.Abs(item.velocity.X) + Math.Abs(item.velocity.Y) > 0.2)
            {
				float num = (float)Main.rand.Next(500) - (Math.Abs(item.velocity.X) + Math.Abs(item.velocity.Y)) * 20f;
				int num2 = 4;
				if (item.isBeingGrabbed)
				{
					num /= 100f;
				}
				if (num < (float)((int)color.R / 70 + 1))
				{
					int num17 = Dust.NewDust(item.position - new Vector2(1f, 2f), item.width, item.height, DustID.CopperCoin, 0f, 0f, 254, Color.OrangeRed, 0.25f);
					Main.dust[num17].velocity *= 0f;
				}
			}
			float num18 = item.velocity.X * 0.2f;
			Color currentColor = item.GetAlpha(color);
			float num16 = item.height - Main.itemTexture[item.type].Height;
			float num15 = item.width / 2 - Main.itemTexture[item.type].Width / 2;
			int num14 = 5;
			Main.itemFrameCounter[whoAmI]++;
			if (Main.itemFrameCounter[whoAmI] > 5)
			{
				Main.itemFrameCounter[whoAmI] = 0;
				Main.itemFrame[whoAmI]++;
			}
			if (Main.itemFrame[whoAmI] > 7)
			{
				Main.itemFrame[whoAmI] = 0;
			}
			int width = hellstoneCoin.Width;
			int num13 = hellstoneCoin.Height / 8;
			num15 = item.width / 2 - hellstoneCoin.Width / 2;
			spriteBatch.Draw(hellstoneCoin, new Vector2(item.position.X - Main.screenPosition.X + (float)(width / 2) + num15, item.position.Y - Main.screenPosition.Y + (float)(num13 / 2) + num16), new Microsoft.Xna.Framework.Rectangle(0, Main.itemFrame[whoAmI] * num13 + 1, Main.itemTexture[item.type].Width, num13), currentColor, num18, new Vector2(width / 2, num13 / 2), scale, SpriteEffects.None, 0f);

			return false;
        }

		public override void GrabRange(Player player, ref int grabRange) => grabRange += player.goldRing ? Item.coinGrabRange : 0;

        public override bool GrabStyle(Player player)
        {
			Vector2 vector5 = new Vector2(item.position.X + (float)(item.width / 2), item.position.Y + (float)(item.height / 2));
			float num31 = item.Center.X - vector5.X;
			float num30 = item.Center.Y - vector5.Y;
			float num29 = (float)Math.Sqrt(num31 * num31 + num30 * num30);
			num29 = 12f / num29;
			num31 *= num29;
			num30 *= num29;
			int num25 = 5;
			item.velocity.X = (item.velocity.X * (float)(num25 - 1) + num31) / (float)num25;
			item.velocity.Y = (item.velocity.Y * (float)(num25 - 1) + num30) / (float)num25;

			return true;
        }

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) => false;

        public override void AddRecipes()
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumCoin, 100);
			recipe.SetResult(this);
        }
    }
}
