using DarknessUnbound.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Materials
{
    public class TropidiumOre : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tropidium Ore");
            Tooltip.SetDefault("'Iridescent'");
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
            item.createTile = ModContent.TileType<TropidiumOre_Tile>();
            item.value = Item.buyPrice(0, 0, 10, 0);       
        }
    }
}
