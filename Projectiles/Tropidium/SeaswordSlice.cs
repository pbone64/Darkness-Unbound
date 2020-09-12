using DarknessUnbound.Dusts;
using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class SeaswordSlice : ModProjectile
    {
        private float rot = 0f;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 80;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.netUpdate = true;
            projectile.aiStyle = 0;
            projectile.timeLeft = 16;
            projectile.scale = 1.25f;
            projectile.tileCollide = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), projectile.GetAlpha(default), projectile.rotation, new Vector2(tex.Width, tex.Height) / 2f, 1f, SpriteEffects.None, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

            return false;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.rotation = player.itemRotation;// + player.direction > 0 ? -MathHelper.TwoPi : MathHelper.TwoPi;
            projectile.Center = new Vector2(player.itemLocation.X, player.itemLocation.Y);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color blue = new Color(61, 255, 203);
            Color red = new Color(255, 81, 50);

            return new AnimatedColor(red, blue).GetColor();
        }
    }
}
