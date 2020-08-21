using DarknessUnbound.Items;
using DarknessUnbound.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.MeltBiome.Blocks
{
    public class SoftIce : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soft Ice");
        }
        public override void SafeSetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 999;
            item.consumable = true;
            item.createTile = ModContent.TileType<SoftIce_Tile>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IceBlock);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PinkIceBlock);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PurpleIceBlock);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RedIceBlock);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(this, 2);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(ItemID.IceBlock);
            recipe.AddRecipe();
        }
    }
}
