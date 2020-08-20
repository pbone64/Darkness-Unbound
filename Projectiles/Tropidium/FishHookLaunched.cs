using DarknessUnbound.Dusts;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class FishHookLaunched : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.thrown = true;
            projectile.arrow = true;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.aiStyle = 1;
        }
        //someone make this a javelin style sticking projectile
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.ai[0] == 0f)
            {
                projectile.extraUpdates = 0;
            }
            else
            {
                projectile.extraUpdates = 1;
            }

            if (player.dead)
            {
                projectile.Kill();
                return;
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

            Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<TropidiumGlow>(), null, 0, Color.White, 0.5f);
            dust.noGravity = true;

        }
        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) //chain code
        {
            Texture2D chain = ModContent.GetTexture("DarknessUnbound/ChainTextures/TropidiumChain");
            Player player = Main.player[projectile.owner];
            Vector2 cent = player.MountedCenter;
            var drawPos = projectile.Center;
            var remainingVectorToPlayer = cent - drawPos;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            while (true)
            {
                float length = remainingVectorToPlayer.Length();
                if (length < 25f || float.IsNaN(length))
                    break;

                drawPos += remainingVectorToPlayer * 12 / length;
                remainingVectorToPlayer = cent - drawPos;
                Color color = Lighting.GetColor((int)drawPos.X / 16, (int)(drawPos.Y / 16f));
                spriteBatch.Draw(chain, drawPos - Main.screenPosition, null, color, rotation, chain.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }*/
        public override bool PreKill(int timeLeft)
        {

            Main.PlaySound(SoundID.Item10, projectile.position);

            return true;
        }
    }
}
