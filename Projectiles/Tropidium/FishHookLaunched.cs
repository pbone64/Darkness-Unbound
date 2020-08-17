using DarknessUnbound.Dusts;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Tropidium
{
    public class FishHookLaunched : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 42;
            projectile.height = 42;
            projectile.thrown = true;
            projectile.arrow = true;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.aiStyle = 1;
        }
        //someone make this a javelin style projectile with a chain
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

            Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<TropidiumGlow>(), null, 0, Color.White, 1f);
            dust.noGravity = true;
        }

        public override bool PreKill(int timeLeft)
        {

            Main.PlaySound(SoundID.Item10, projectile.position);

            return true;
        }
    }
}
