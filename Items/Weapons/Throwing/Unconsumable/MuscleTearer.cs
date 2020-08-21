using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing.Unconsumable
{
    public class MuscleTearer : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 26;
            item.thrown = true;
            item.useTime = item.useAnimation = 16;
            item.knockBack = 4.5f;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<DarkDaggerPro>();
            item.shootSpeed = 14f;
            item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
