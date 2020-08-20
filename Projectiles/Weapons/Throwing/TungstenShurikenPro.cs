using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Throwing
{
    public class TungstenShurikenPro : ModProjectile
    {
        public override string Texture => "DarknessUnbound/Items/Weapons/Throwing/TungstenShuriken";

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Shuriken);
            aiType = ProjectileID.Shuriken;
        }

        public override bool PreAI()
        {
            projectile.ai[0] -= 0.35f;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDust(projectile.Center, projectile.width, projectile.height, DustID.Tungsten, 0, 0, 0, default, 0.75f);
            }
            Main.PlaySound(SoundID.Dig, projectile.Center);
        }
    }
}
