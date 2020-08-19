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

        [Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);

            return true;
        }
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.rotation = player.itemRotation;

            //Dust dust = Dust.NewDustPerfect(player.Center, ModContent.DustType<TropidiumGlow>(), null, 0, Color.White, 1f);
            //dust.noGravity = true;
            //dust.velocity += projectile.rotation.ToRotationVector2() * 5;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color blue = new Color(61, 255, 203);
            Color red = new Color(255, 81, 50);

            return new AnimatedColor(blue, red).GetColor();
        }
    }
}
