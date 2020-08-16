using DarknessUnbound.Projectiles.Tropidium;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
    public class FishHook : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Hook");
        }
        public override void SafeSetDefaults()
        {
            
            item.useAnimation = item.useTime = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 20;
            item.ranged = true;
            item.rare = ItemRarityID.Blue;
            item.channel = true;
            item.UseSound = item.UseSound = SoundID.Item5;
            item.crit = 10;
            item.shootSpeed = 25f;
            item.shoot = ModContent.ProjectileType<FishHookLaunched>();
            item.autoReuse = true;
            item.noMelee = true;
            item.noUseGraphic = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 wetlinextremeprofessionalhairgel = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            speedX = wetlinextremeprofessionalhairgel.X;
            speedY = wetlinextremeprofessionalhairgel.Y;

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
