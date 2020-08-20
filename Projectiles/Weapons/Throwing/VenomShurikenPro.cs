using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DarknessUnbound.Projectiles.Weapons.Throwing
{
    public class VenomShurikenPro : ModProjectile
    {
        public override string Texture => "DarknessUnbound/Items/Weapons/Throwing/VenomShuriken";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Shuriken);
            projectile.friendly = true;
            projectile.hostile = false;
            aiType = ProjectileID.Shuriken;
            projectile.extraUpdates++;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 12;
        }

        public override bool PreAI()
        {
            projectile.ai[0] -= 0.55f;
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.AddBuff(BuffID.Venom, 600);
        public override void OnHitPvp(Player target, int damage, bool crit) => target.AddBuff(BuffID.Venom, 600);

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int d = Dust.NewDust(projectile.position + Vector2.UnitY * 16f, projectile.width, projectile.height - 16, 171, 0f, 0f, 100);
                Main.dust[d].scale = Main.rand.Next(1, 10) * 0.1f;
                Main.dust[d].noGravity = true;
                Main.dust[d].fadeIn = 1.5f;
                Dust dust = Main.dust[d];
                dust.velocity *= 0.75f;
            }
            Main.PlaySound(SoundID.Dig, projectile.Center);
        }
    }
}
