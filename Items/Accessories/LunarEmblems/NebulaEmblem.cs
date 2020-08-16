using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories.LunarEmblems
{
    public class NebulaEmblem : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("25% increased magic damage" +
                             "\n10% increased magic critical strike chance");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.value = Item.sellPrice(0, 4);
            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += 0.25f;
            player.magicDamage += 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddIngredient(ItemID.FragmentNebula, 18);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
