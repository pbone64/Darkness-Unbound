using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Bosses.pbone
{
    public class PhantasmalSphere : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.PhantasmalSphere;

        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.PhantasmalSphere);
            projectile.aiStyle = -1;
        }

        public override void AI()
        {
            if (projectile.alpha > 200)
                projectile.alpha = 200;

            projectile.alpha -= 5;
            if (projectile.alpha < 0)
                projectile.alpha = 0;

            float num676 = (float)projectile.alpha / 255f;
            projectile.scale = 1f - num676;
            if (projectile.ai[0] >= 0f)
                projectile.ai[0]++;

            if (projectile.ai[0] == -1f)
            {
                projectile.frame = 1;
                projectile.extraUpdates = 1;
            }
            /*else if (projectile.ai[0] < 30f)
            {
                projectile.position = Main.npc[(int)projectile.ai[1]].Center - new Vector2(projectile.width, projectile.height) / 2f - projectile.velocity;
            }*/
            else
            {
                projectile.velocity *= 0.96f;
                if (++projectile.frameCounter >= 6)
                {
                    projectile.frameCounter = 0;
                    if (++projectile.frame >= 2)
                        projectile.frame = 0;
                }
            }

            if (projectile.alpha >= 40)
                return;

            /*for (int num677 = 0; num677 < 2; num677++)
            {
                float num678 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                if (num678 < -0.5f)
                    num678 = -0.5f;

                if (num678 > 0.5f)
                    num678 = 0.5f;

                Vector2 value24 = new Vector2((float)(-projectile.width) * 0.65f * projectile.scale, 0f).RotatedBy(num678 * ((float)Math.PI * 2f)).RotatedBy(projectile.velocity.ToRotation());
                int num679 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 229, (0f - projectile.velocity.X) / 3f, (0f - projectile.velocity.Y) / 3f, 150, Color.Transparent, 0.7f);
                Main.dust[num679].velocity = Vector2.Zero;
                Main.dust[num679].position = projectile.Center + value24;
                Main.dust[num679].noGravity = true;
            }*/
        }

        public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 255, 255) * (1f - (float)projectile.alpha / 255f);

    }
}
