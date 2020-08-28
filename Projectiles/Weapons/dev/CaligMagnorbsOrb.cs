using DarknessUnbound.Items.Weapons.dev;
using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.dev
{
    public class CaligMagnorbsOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.Size = new Vector2(8f);
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.ignoreWater = true;
        }

        public const float Orbit = 0;
        public const float Thrown = 1;
        public const float Returning = 2;

        public float Index { get => projectile.ai[0]; }
        public float State { get => projectile.ai[1]; set => projectile.ai[1] = value; }
        public float Timer { get => projectile.localAI[0]; set => projectile.localAI[0] = value; }
        public bool ReturnTransition { get => projectile.localAI[1] == 1; set => projectile.localAI[1] = (value ? 1 : 0); }

        public Player Owner { get => Main.player[projectile.owner]; }

        public override void AI()
        {
            // Don't Despawn
            if (projectile.timeLeft < 10) projectile.timeLeft = 10;

            // Kill if there isn't the right amount or if the player swapped items
            //if (Owner.ownedProjectileCounts[projectile.type] != 6)
            //    projectile.Kill();
            if (Owner.HeldItem.type != ModContent.ItemType<CaligMagnorbs>())
                projectile.Kill();

            // STATES
            if (State == Orbit)
            {
                // Orbiting around player hand
                projectile.Center = Owner.Center;
                projectile.velocity = Vector2.Zero;
            }
            else if (State == Thrown)
            {
                // Throwing out
                Timer++;
                projectile.rotation = projectile.velocity.ToRotation();

                if (Timer <= 10) projectile.velocity *= 0.95f;

                if (Timer >= 35)
                {
                    State = Returning;
                    ReturnTransition = true;
                }
            }
            else if (State == Returning)
            {
                // TODO: fix this
                // Returning after being thrown out
                if (ReturnTransition)
                {
                    projectile.velocity = projectile.DirectionTo(Owner.Center);
                    Timer = 1;
                    ReturnTransition = false;
                }

                if (Timer > 0) Timer += 0.05f;
                if (Timer <= 3) projectile.velocity *= Timer;
                if (Timer >= 3) Timer = 0;
                if (projectile.velocity.Length() < new Vector2(16f).Length())
                {
                    projectile.velocity = projectile.velocity * 0.85f;
                }

                if (projectile.getRect().Intersects(Owner.getRect())) {
                    State = Orbit;
                }
            }
        }

        public void Throw()
        {
            Vector2 dir = projectile.DirectionTo(Main.MouseWorld) * 24f;
            projectile.velocity = dir;
            State = Thrown;
        }

        public static CaligMagnorbsOrb FirstAvaiableOrb(int owner) => Main.projectile.FirstOrDefault((Projectile p) => p.active && p.whoAmI != 1000 && p.owner == owner && p.type == ModContent.ProjectileType<CaligMagnorbsOrb>() && p.ai[1] == Orbit).modProjectile as CaligMagnorbsOrb;

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Timer);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Timer = reader.Read();
        }
    }
}
