using Terraria;
using Terraria.ID;

namespace DarknessUnbound.Items.Materials.Bars.Advisor
{
    public class Realitum : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A concentrated bar of of reality...'" +
                             "\nProlonged contact is not advised");
        }

        public override void SafeSetDefaults()
        {
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = ItemRarityID.Purple;
        }
    }
}
