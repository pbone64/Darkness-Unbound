using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Throwing
{
    public class MeteorShurikenPro : ModProjectile
    {
        public override string Texture => "DarknessUnbound/Items/Weapons/Throwing/Unconsumable/MeteorShuriken";

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Shuriken);
            projectile.penetrate = 3;
            aiType = ProjectileID.Shuriken;
        }

        public override bool PreAI()
        {
            projectile.ai[0] -= 0.4f;
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.AddBuff(BuffID.OnFire, 240);

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
                projectile.Kill();
            else
            {
                Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Item10, projectile.position);
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, Main.rand.NextBool(2) ? DustID.t_Meteor : DustID.Fire, 0, 0, 0, default, 0.8f);
            }
            Main.PlaySound(SoundID.Dig, projectile.Center);
        }
    }
}
