using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories.LunarEmblems
{
    public class SolarEmblem : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("25% increased melee damage" +
                             "\n10% increased melee critical strike chance");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.value = Item.sellPrice(0, 4);
            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.25f;
            player.meleeCrit += 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddIngredient(ItemID.FragmentSolar, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
