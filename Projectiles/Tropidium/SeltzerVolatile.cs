using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DarknessUnbound.Dusts;
using Microsoft.Xna.Framework;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class SeltzerVolatile : ModProjectile
    {
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
            projectile.tileCollide = true;
            projectile.velocity *= 0.5f;
            projectile.netUpdate = true;
            projectile.timeLeft = 300;
            projectile.magic = true;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override void AI()
        {
            Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<TropidiumGlow>(), null, 0, default, 2f);
            dust.noGravity = true;
            dust.color = Color.Red;

            projectile.rotation += projectile.velocity.X / 2;
            projectile.velocity *= 0.995f;
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[1] == 0)
                for (int greg = 0; greg < 1; greg++)
                {
                    Projectile.NewProjectile(projectile.position, new Vector2(0, 0), ModContent.ProjectileType<SeltzerExplosion>(), projectile.damage * (int)1.5f, 0, projectile.owner, 0, 1);
                }

            for (int i = 0; i < 50; i++)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, ModContent.DustType<TropidiumSteam>(), 0, 0, 0, Color.White, 1.3f);
                dust.noGravity = true;
                dust.velocity *= 1.1f;
            }

            Main.PlaySound(SoundID.Item14, projectile.position);
        }
    }
}
