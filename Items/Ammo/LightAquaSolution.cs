using DarknessUnbound.Projectiles.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Ammo
{
	public class LightAquaSolution : DarknessItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Light Aqua Solution");
			Tooltip.SetDefault("Used by the Clentaminator"
				+ "\nSpreads the (insert melted ice biome name)");
		}

		public override void SafeSetDefaults()
		{
			item.shoot = ModContent.ProjectileType<LightAquaSolutionProj>() - ProjectileID.PureSpray;
			item.ammo = AmmoID.Solution;
			item.value = Item.buyPrice(0, 0, 25, 0);
			item.rare = ItemRarityID.Orange;
			item.maxStack = 999;
			item.consumable = true;
		}

        public override bool ConsumeAmmo(Player player)
        {
			return Main.rand.Next(3) == 0;
        }
    }
}