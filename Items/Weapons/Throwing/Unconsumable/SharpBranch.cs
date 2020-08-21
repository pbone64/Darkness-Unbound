using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing.Unconsumable
{
    public class SharpBranch : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 28;
            item.thrown = true;
            item.useTime = item.useAnimation = 15;
            item.knockBack = 5.5f;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<SharpBranchPro>();
            item.shootSpeed = 14f;
            item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddIngredient(ItemID.Stinger, 13);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
