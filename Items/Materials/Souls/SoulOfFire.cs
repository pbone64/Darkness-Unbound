using Terraria;
using Terraria.ID;

namespace DarknessUnbound.Items.Materials.Souls
{
    public class SoulOfFire : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Fire");
            Tooltip.SetDefault("'The essence of burning creatures'");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 2, 0);
            item.rare = ItemRarityID.Orange;
        }
    }
}
