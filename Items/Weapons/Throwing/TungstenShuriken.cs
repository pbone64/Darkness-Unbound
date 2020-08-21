using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing
{
    public class TungstenShuriken : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 18;
            item.thrown = true;
            item.useTime = item.useAnimation = 15;
            item.knockBack = 0.5f;
            item.maxStack = 999;
            item.consumable = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<TungstenShurikenPro>();
            item.shootSpeed = 12f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 45);
            recipe.AddRecipe();
        }
    }
}
