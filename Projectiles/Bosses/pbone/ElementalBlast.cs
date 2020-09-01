using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Bosses.pbone
{
    public class ElementalBlast : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_" + ProjectileID.ShadowBeamFriendly;

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(8f);
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 6;
            projectile.timeLeft = 60;
        }

        public override void AI()
        {
            Dust.NewDustPerfect(projectile.Center, 92, Vector2.Zero).noGravity = true;
        }
    }
}
