using Terraria;
using System.Diagnostics;
using Terraria.ID;
using Terraria.ModLoader;
using DarknessUnbound.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DarknessUnbound.Helpers;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class SeltzerExplosion : ModProjectile
    {
        Player player = Main.player[Main.myPlayer];

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
            projectile.alpha = 300;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        { 
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Texture2D tex = mod.GetTexture("Projectiles/Tropidium/SeltzerExplosion");
            projectile.rotation += MathHelper.ToRadians(15);
            projectile.alpha -= 6;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            Color blue = new Color(0, 229, 255, projectile.alpha);
            Color red = new Color(255, 75, 43, projectile.alpha);
            Color anim = new AnimatedColor(blue, red).GetColor();
            return anim;
        }
    }
}
