using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DarknessUnbound.Items.Weapons.Throwing.Unconsumable
{
    public class DawnsEnd : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 36;
            item.crit = 8;
            item.thrown = true;
            item.useTime = item.useAnimation = 12;
            item.knockBack = 1f;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<DawnsEndPro>();
            item.shootSpeed = 14f;
            item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<DarkDagger>());
            recipe.AddIngredient(ModContent.ItemType<SharpBranch>());
            recipe.AddIngredient(ModContent.ItemType<MoltenGrenade>());
            recipe.AddIngredient(ModContent.ItemType<Bashosen>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
