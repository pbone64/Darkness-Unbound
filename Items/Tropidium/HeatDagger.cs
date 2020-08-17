using DarknessUnbound.Projectiles.Tropidium;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
    public class HeatDagger : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heat Dagger");
        }
        public override void SafeSetDefaults()
        {

            item.useAnimation = item.useTime = 12;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 18;
            item.thrown = true;
            item.rare = ItemRarityID.Blue;
            item.channel = true;
            item.UseSound = item.UseSound = SoundID.Item1;
            item.crit = 20;
            item.shootSpeed = 13f;
            item.shoot = ModContent.ProjectileType<HeatDaggerLaunched>();
            item.autoReuse = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.consumable = true;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ThrowingKnife, 50);
            //recipe.AddIngredient(insert bar);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}
