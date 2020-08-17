using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
    public class PlatedFlintlock : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plated Flintlock");
        }
        public override void SafeSetDefaults()
        {
            item.CloneDefaults(ItemID.FlintlockPistol);
            item.useAnimation = item.useTime = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.damage = 25;
            item.ranged = true;
            item.rare = ItemRarityID.Blue;
            item.useAmmo = AmmoID.Bullet;
            item.shoot = AmmoID.Bullet;
            item.noMelee = true;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FlintlockPistol);
            //recipe.AddIngredient(insert bar);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
