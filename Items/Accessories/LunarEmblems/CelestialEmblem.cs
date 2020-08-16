using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories.LunarEmblems
{
    public class CelestialEmblem : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("22% increased damage" +
                             "\n8% increased critical strike chance" +
                             "\nIncreases your max number of minions by 1");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.value = Item.sellPrice(0, 5);
            item.rare = ItemRarityID.Purple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.22f;
            player.meleeCrit += 8;
            player.rangedCrit += 8;
            player.magicCrit += 8;
            player.slotsMinions++;
        }

        public override void AddRecipes()
        {
            // TODO: CELESTIAL FRAGMENT
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SolarEmblem>());
            recipe.AddIngredient(ModContent.ItemType<VortexEmblem>());
            recipe.AddIngredient(ModContent.ItemType<NebulaEmblem>());
            recipe.AddIngredient(ModContent.ItemType<StardustEmblem>());
            recipe.AddIngredient(ItemID.DirtBlock, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
