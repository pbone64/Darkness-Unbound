using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing
{
    public class PlatinumJavelin : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 20;
            item.thrown = true;
            item.useTime = item.useAnimation = 15;
            item.knockBack = 4f;
            item.maxStack = 999;
            item.consumable = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<PlatinumJavelinPro>();
            item.shootSpeed = 12f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 66);
            recipe.AddRecipe();
        }
    }
}
