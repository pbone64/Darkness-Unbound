using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Throwing
{
    public class LeadWeightPro : ModProjectile
    {
        public override string Texture => "DarknessUnbound/Items/Weapons/Throwing/LeadWeight";

        public override void SetDefaults()
        {
            projectile.CloneDefaults(24);
            aiType = 24;
        }

        public override bool PreAI()
        {
            if (projectile.velocity.Y < 0) projectile.velocity.Y -= (projectile.velocity.Y / 10f);
            projectile.velocity.X -= (projectile.velocity.X / 90f);
            return true;
        }
    }
}
