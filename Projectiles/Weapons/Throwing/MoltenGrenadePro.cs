using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Throwing
{
    public class MoltenGrenadePro : ModProjectile
    {
        public override string Texture => "DarknessUnbound/Items/Weapons/Throwing/Unconsumable/MoltenGrenade";

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Grenade);
            projectile.thrown = true;
            aiType = ProjectileID.Grenade;
        }

        public override bool PreAI()
        {
            projectile.rotation *= 0.95f;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 35; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 127, Main.rand.Next(-8, 9), Main.rand.Next(-8, 9));
                d.scale = 1.1f;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustDirect(projectile.position + Vector2.One * 50, projectile.width - 100, projectile.height - 100, DustID.Smoke);
                d.scale = 2f;
                d.noGravity = true;
                d.velocity *= 1.5f;
            }
            Main.PlaySound(SoundID.Item, projectile.Center, 14);
        }
    }
}
