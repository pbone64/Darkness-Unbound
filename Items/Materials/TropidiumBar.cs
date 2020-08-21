using DarknessUnbound.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Materials
{
    public class TropidiumBar : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tropidium Bar");
            Tooltip.SetDefault("'It shines in a multicolored fashion'");
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
            item.placeStyle = 1;
            item.useTurn = true;
            item.createTile = ModContent.TileType<TropidiumBar_Tile>();
            item.value = Item.buyPrice(0, 0, 30, 50);
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<TropidiumOre>(), 3);
            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
