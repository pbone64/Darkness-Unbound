using DarknessUnbound.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items
{
    public class FloatingDummyItem : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Floating Target Dummy");
            Tooltip.SetDefault("Creates a puppet in midair" +
                "\nRight click to remove a puppet");
        }
        //NPC.NewNPC((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y + 20, (ushort)ModContent.NPCType<FloatingDummy>(), 0);
        public override void SafeSetDefaults()
        {
            item.useTime = item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noUseGraphic = true;
            item.value = Item.buyPrice(0, 0, 90, 0);
            item.autoReuse = true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 0)
                NPC.NewNPC((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y + 2, (ushort)ModContent.NPCType<FloatingDummy>(), 0);
            return true;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TargetDummy);
            recipe.AddIngredient(ItemID.WhiteString, 2);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
