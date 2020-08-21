using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories.LunarEmblems
{
    public class EldritchEmblem : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'It beats with an otherwordly power...'" +
                             "\n20% increased damage" +
                             "\n10% increased critical strike chance" +
                             "\nIncreases your max number of minions by 1" +
                             "\nReduces damage taken by 10%" +
                             "\nBeing hit rains down lunar flares" +
                             "\nYou have the agility of a dimension traversing elder god");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.defense = 12;
            item.value = Item.sellPrice(0, 15);
            item.rare = ItemRarityID.Purple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.20f;
            player.meleeCrit += 10;
            player.rangedCrit += 10;
            player.magicCrit += 10;
            player.slotsMinions++;

            player.GetModPlayer<DUPlayer>().eldritchCore = true;
            player.endurance += 0.10f;
            player.moveSpeed += 1.5f;
            player.maxFallSpeed += 0.5f;
            player.maxRunSpeed += 3f;
            player.meleeSpeed += 0.25f;
            player.jumpSpeedBoost += 0.5f;
        }

        public override void AddRecipes()
        {
            // TODO: CELESTIAL FRAGMENT
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<CelestialEmblem>());
            recipe.AddIngredient(ModContent.ItemType<EldritchCore>());
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
