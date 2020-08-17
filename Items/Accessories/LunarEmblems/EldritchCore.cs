using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories.LunarEmblems
{
    public class EldritchCore : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'It beats with an otherwordly power...'" +
                             "\nBeing hit rains down lunar flares" +
                             "\nYou have the agility of a dimension traversing elder god");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.defense = 12;
            item.value = Item.sellPrice(0, 10);
            item.rare = ItemRarityID.Purple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DUPlayer>().eldritchCore = true;
            player.moveSpeed += 1.5f;
            player.maxFallSpeed += 0.5f;
            player.maxRunSpeed += 3f;
            if (player.accRunSpeed > 0) player.accRunSpeed += 1.5f;
            player.meleeSpeed += 0.25f;
            player.jumpSpeedBoost += 0.5f;
        }
    }
}
