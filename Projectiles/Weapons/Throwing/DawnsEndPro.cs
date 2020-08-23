using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Throwing
{
    public class DawnsEndPro : ModProjectile
    {
        public override string Texture => "DarknessUnbound/Items/Weapons/Throwing/Unconsumable/DawnsEnd";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenBoomerang);
            projectile.extraUpdates = 3;
            projectile.tileCollide = false;
        }

        float oldAi0;

        public override bool PreAI()
        {
            projectile.ai[1] -= 0.25f;
            if (oldAi0 != projectile.ai[0])
            {
                for (int i = 0; i < 12; i++)
                    Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 37, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 175, default, 2.25f).noGravity = true;
            }
            if (Main.GameUpdateCount % 2 == 0) Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 27).noGravity = true;

            oldAi0 = projectile.ai[0];
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - i) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
    }
}
