using DarknessUnbound.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class SteamBubble : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.tileCollide = false;
            projectile.timeLeft = 120;
            projectile.alpha = 255;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.magic = true;
        }

        public override void AI()
        {
            projectile.velocity *= 0.94f;
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, ModContent.DustType<TropidiumSteam>(), 0, 0, 0, Color.White, 1.1f);
            dust.noGravity = true;
        }
    }
}
