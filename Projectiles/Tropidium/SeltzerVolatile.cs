using Terraria;
using System.Diagnostics;
using Terraria.ID;
using Terraria.ModLoader;
using DarknessUnbound.Dusts;
using Microsoft.Xna.Framework;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class SeltzerVolatile : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.damage *= (int)1.5f;
            projectile.velocity *= 0.66f;
            projectile.tileCollide = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<TropidiumGlow>(), null, 0, Color.LightCoral, 2f);
            dust.noGravity = true;
            projectile.rotation += projectile.velocity.X / 2;
        }
    }
}
