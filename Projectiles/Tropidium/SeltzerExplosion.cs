using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DarknessUnbound.Helpers;
using System;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class SeltzerExplosion : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 108;
            projectile.height = 108;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.netUpdate = true;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.alpha = 250;
            projectile.magic = true;
            projectile.tileCollide = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D exploder = Main.projectileTexture[projectile.type];
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            spriteBatch.Draw(exploder, projectile.Center - Main.screenPosition, new Rectangle(0, 0, exploder.Width, exploder.Height), projectile.GetAlpha(default), projectile.rotation, new Vector2(exploder.Width, exploder.Height) / 2f, 1f, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Texture2D tex = mod.GetTexture("Projectiles/Tropidium/SeltzerExplosion");
            projectile.rotation += MathHelper.ToRadians(15);
            projectile.alpha -= (int)projectile.ai[0];
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color blue = new Color(0, 229, 255, projectile.alpha);
            Color red = new Color(255, 75, 43, projectile.alpha);

            return new AnimatedColor(blue, red).GetColor();
        }
    }
}
