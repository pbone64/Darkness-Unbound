using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Throwing
{
    public class MuscleTearerPro : ModProjectile
    {
        public override string Texture => "DarknessUnbound/Items/Weapons/Throwing/Unconsumable/MuscleTearer";

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.ThrowingKnife);
            projectile.Size += Vector2.One * 4f;
            aiType = ProjectileID.ThrowingKnife;
        }

        public override bool PreAI()
        {
            projectile.ai[0] -= 0.5f;
            return true;
        }

        public override void PostAI()
        {
            if (projectile.localAI[0] == 0)
            {
                projectile.rotation -= MathHelper.Pi;
                projectile.localAI[0]++;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, null, lightColor, projectile.rotation - MathHelper.PiOver4, Main.projectileTexture[projectile.type].Size() * 0.5f, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, 115, 0, 0, 0, default, 0.85f);
            }
            Main.PlaySound(SoundID.Dig, projectile.Center);
        }
    }
}
