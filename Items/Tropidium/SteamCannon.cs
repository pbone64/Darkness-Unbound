using DarknessUnbound.Projectiles.Tropidium;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
	public class SteamCannon : DarknessItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Steam Cannon");
			Tooltip.SetDefault("Blasts hot steam a short distance ahead");

			Item.staff[item.type] = true;
		}

		public override void SafeSetDefaults()
		{
			item.damage = 25;
			item.magic = true;
			item.mana = 6;
			//item.width = 44;
			//item.height = 46;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = 32500;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item34;
			item.autoReuse = true;
			item.channel = true;
			item.shoot = ModContent.ProjectileType<SteamBubble>();
			item.shootSpeed = 25f;
			item.noMelee = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Vector2 posDiff = position += new Vector2(Main.rand.Next(-5, 5));

			Projectile.NewProjectile(posDiff.X, posDiff.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);

			return false;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(insert bar);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}