using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DarknessUnbound.Dusts;
using Microsoft.Xna.Framework;
using System.Linq;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class SeltzerQuick : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.netUpdate = true;
            projectile.arrow = true;
            projectile.magic = true;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90);

            Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<TropidiumGlow>(), null, 0, Color.White, 1f);
            dust.noGravity = true;

            foreach (NPC npc in from NPC n in Main.npc where n.active && !n.friendly && !n.dontTakeDamage && !n.immortal && n.lifeMax > 5 select n) //the pbone line
            {
                float distanceToNPC = (npc.Center - projectile.Center).Length();

                // necessary unless you want every projectile in the world to come forth
                if (distanceToNPC <= 300 && distanceToNPC >= 0) 
                    projectile.velocity = projectile.DirectionTo(npc.Center) * 30;
            }
        }
    }
}
