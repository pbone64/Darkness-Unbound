using DarknessUnbound.Projectiles.Weapons.dev;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.dev
{
    public class CaligMagnorbs : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empiricist's magnorbs");
            Tooltip.SetDefault("'runs off pure wwhite science no wwhite magic to be found'" +
                             "\n");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 1000;
            item.crit = 50;
            item.melee = true;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 16;
            item.value = Item.sellPrice(10);
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = 10;
        }

        public override void HoldItem(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<CaligMagnorbsOrb>()] != 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    Projectile.NewProjectile(player.Center, Vector2.Zero, ModContent.ProjectileType<CaligMagnorbsOrb>(), item.damage, 10f, player.whoAmI, i);
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            CaligMagnorbsOrb orb = CaligMagnorbsOrb.FirstAvaiableOrb(player.whoAmI);
            if (orb != null) orb.Throw();
            return false;
        }
    }
}
