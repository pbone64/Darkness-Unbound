using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing
{
    public class SilverShuriken : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 18;
            item.thrown = true;
            item.useTime = item.useAnimation = 13;
            item.knockBack = 0.5f;
            item.maxStack = 999;
            item.consumable = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<SilverShurikenPro>();
            item.shootSpeed = 12f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 45);
            recipe.AddRecipe();
        }
    }
}
