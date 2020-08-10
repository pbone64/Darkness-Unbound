using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DarknessUnbound.Dusts;
using Microsoft.Xna.Framework;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class SeltzerExplosion : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 108;
            projectile.height = 108;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.damage *= (int)1.5f;
            projectile.velocity *= 0.66f;
            projectile.tileCollide = true;
            projectile.timeLeft = 4000;
        }

        public override void AI()
        {
            Dust dust = Dust.NewDustDirect(projectile.position, 108, 108, ModContent.DustType<TropidiumGlow>(), -projectile.velocity.X, -projectile.velocity.Y, 0, Color.White, 1.5f);
            dust.noGravity = true;
            projectile.rotation += MathHelper.ToRadians(9);
        }
    }
}
