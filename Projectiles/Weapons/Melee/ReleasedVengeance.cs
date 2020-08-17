using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Melee
{
    public class ReleasedVengeance : ModProjectile
    {
        public static AnimatedColor animatedColor { get => new AnimatedColor(new Color[] { new Color(75, 70, 219), new Color(255, 56, 73), new Color(86, 240, 255) }, 7.5f); }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(52, 56);
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.extraUpdates = 4;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.scale = 1.45f;
            projectile.alpha = 100;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() - MathHelper.Pi * 0.75f + MathHelper.Pi;
            Lighting.AddLight(projectile.Center / 16f, animatedColor.LightingColor());

            /*Dust dust = Dust.NewDustPerfect(projectile.Center, 88, null, 0, animatedColor.GetColor(), 1.45f);
            dust.noGravity = true;
            dust.noLight = true;
            dust.velocity = Vector2.Zero;*/
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, null, animatedColor.GetColor(), projectile.rotation, Main.projectileTexture[projectile.type].Size() * 0.5f, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsOverWiresUI.Add(index);
        }
    }
}
