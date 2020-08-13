using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
    public class TropicalLongbow : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tropical Longbow");
        }
        public override void SafeSetDefaults()
        {
            item.useAnimation = item.useTime = 4;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.damage = 18;
            item.ranged = true;
            item.rare = ItemRarityID.Blue;
            item.channel = true;
            item.UseSound = item.UseSound = SoundID.Item5;
            item.crit = 20;
            item.useAmmo = AmmoID.Arrow;
            item.shootSpeed = 25f;
            item.shoot = AmmoID.Arrow;
            item.noMelee = true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
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
