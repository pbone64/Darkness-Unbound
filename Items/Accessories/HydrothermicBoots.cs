using Terraria;
using Terraria.ID;

namespace DarknessUnbound.Items.Accessories
{
    // TBD - Eqiupped sprite recipe
    public class HydrothermicBoots : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows flight, incredibly fast running, and extra mobility on ice" +
                             "\nProvides the ability to walk on water and lava" +
                             "\nGrants immunity to fire blocks and 14 seconds of immunity to lava" +
                             "\n10% increased movement speed");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.buyPrice(0, 25);
            item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // FROSTSPARK
            player.accRunSpeed = 7.5f;
            player.rocketBoots = 1;
            player.moveSpeed += 0.1f;
            player.iceSkate = true;

            // LAVA WADERS
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 840;
        }
    }
}
