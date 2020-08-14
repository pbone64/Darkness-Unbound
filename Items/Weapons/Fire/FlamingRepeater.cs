using Terraria;
using Terraria.ID;

namespace DarknessUnbound.Items.Weapons.Fire
{
    public class FlamingRepeater : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Burning Hot!'" +
                             "\nDoes not consume ammo" +
                             "\nRapidly shoots burning arrows");
        }

        public override void SafeSetDefaults()
        {
			item.damage = 42;
			item.ranged = true;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = ProjectileID.HellfireArrow;
			item.shootSpeed = 16f;
		}
    }
}
