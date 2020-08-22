using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing.Unconsumable
{
    public class MoltenGrenade : DarknessItem
    {
        public override void SetStaticDefaults() => DisplayName.SetDefault("Magnade");

        public override void SafeSetDefaults()
        {
            item.damage = 35;
            item.crit = 6;
            item.thrown = true;
            item.useTime = item.useAnimation = 12;
            item.knockBack = 10f;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<MoltenGrenadePro>();
            item.shootSpeed = 13f;
            item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
