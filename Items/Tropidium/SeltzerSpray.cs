using DarknessUnbound.Projectiles.Tropidium;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
	public class SeltzerSpray : DarknessItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seltzer Spray");
			Tooltip.SetDefault("Shoots red and blue fizzy sparks" +
				"\nBlue sparks will home in on enemies" +
				"\nRed sparks will explode and leave behind a watery vortex");
		}
		public override void SafeSetDefaults()
		{
			item.damage = 15;
			item.magic = true;
			item.mana = 4;
			//item.width = 28;
			//item.height = 30;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3;
			item.value = 40000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item21;
			item.autoReuse = true;
			item.channel = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<SeltzerQuick>();
			item.shootSpeed = 17f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			type = Main.rand.Next(new int[] { type, ModContent.ProjectileType<SeltzerQuick>(), ModContent.ProjectileType<SeltzerVolatile>()});

			Vector2 posDiff = position += new Vector2(Main.rand.Next(-15, 15));
			position.X = posDiff.X;
			position.Y = posDiff.Y;

			return true;
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