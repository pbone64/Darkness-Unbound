using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories.LunarEmblems
{
    public class StardustEmblem : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("25% increased summon damage" +
                             "\nIncreases your max number of minions by 1");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.value = Item.sellPrice(0, 4);
            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += 0.25f;
            player.slotsMinions++;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddIngredient(ItemID.FragmentStardust, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
