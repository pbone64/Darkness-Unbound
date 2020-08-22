using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Throwing
{
    public class BashosenPro : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.Size = Vector2.One * 4f;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 60;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            int def = target.defense;
            int bonus = (int)MathHelper.Min(16, def / 2f);
            damage += bonus;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
                Dust.NewDustDirect(projectile.Center, projectile.width, projectile.height, 29).noGravity = true;
        }
    }
}
