using DarknessUnbound.Dusts;
using DarknessUnbound.Items.Tropidium;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class HeatDaggerLaunched : ModProjectile
    {
        public override void SetDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
            projectile.width = 14;
            projectile.height = 30;
            projectile.thrown = true;
            projectile.arrow = true;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.penetrate = 2;
            projectile.aiStyle = 2;

        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<TropidiumGlow>(), null, 0, Color.White, 0.5f);
            dust.noGravity = true;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            if (Main.rand.Next(2) == 0)
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ModContent.ItemType<HeatDagger>());
        }
    }
}
