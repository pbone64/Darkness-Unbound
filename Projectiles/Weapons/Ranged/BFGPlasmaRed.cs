using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Ranged
{
    public class BFGPlasmaRed : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Plasma");
            Main.projFrames[projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(32);
            projectile.ranged = true;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
                projectile.frame++;
            if (projectile.frame == 7)
                projectile.frame = 0;

            // TODO: MAKE THIS NICE
            if (projectile.alpha > 80) projectile.alpha -= 25;

            projectile.rotation += 0.05f;

            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 60, Scale: 1.65f, Alpha: 145).noGravity = true;

            Lighting.AddLight(projectile.Center, new Vector3(1f, 0, 0));
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0]++;
            if (projectile.ai[0] < 4)
            {
                Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Item91, projectile.position);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }

                return false;
            }

            return true;
        }
    }
}
