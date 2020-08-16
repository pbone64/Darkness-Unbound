using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories.LunarEmblems
{
    public class VortexEmblem : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("25% increased ranged damage" +
                             "\n10% increased ranged critical strike chance");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.value = Item.sellPrice(0, 4);
            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.25f;
            player.rangedCrit += 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddIngredient(ItemID.FragmentVortex, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
