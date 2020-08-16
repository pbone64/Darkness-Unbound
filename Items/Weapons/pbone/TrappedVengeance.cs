using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace DarknessUnbound.Items.Weapons.pbone
{
    public class TrappedVengeance : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Many great warlords have fallen to this blade.'");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 666666;
            item.melee = true;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 16;
            item.value = Item.sellPrice(10);
            item.rare = ItemRarityID.Green; 
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
    }
}
