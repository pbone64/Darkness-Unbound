using DarknessUnbound.Projectiles.Bosses.pbone;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.NPCs.Bosses.pbone
{
    public partial class pbone : ModNPC
    {
        private void AiPhaseOne()
        {
            switch (State)
            {
                // Default, target then choose an attack
                case 0:
                    if (!target.active)
                        npc.TargetClosest();
                    else
                    {
                        // Go to target and choose attack
                        if (PhaseOne_MoveToTarget())
                        {
                            Timer = 0;
                            State = 1;
                            //Chat("Found you");
                        }
                    }
                    break;

                // Dust hell attack
                case 1:
                    if (Timer == 0) Chat("I will make you suffer");
                    Timer++;
                    if (Timer > 16 && Timer % 2 == 0)
                    {
                        Projectile.NewProjectile(npc.Center, new Vector2(6, 0).RotatedBy(MathHelper.ToRadians(8 * Timer / 4)), ModContent.ProjectileType<ElementalBlast>(), npc.damage, 100f);
                        Projectile.NewProjectile(npc.Center, new Vector2(-6, 0).RotatedBy(MathHelper.ToRadians(8 * Timer / 4)), ModContent.ProjectileType<ElementalBlast>(), npc.damage, 100f);
                    }
                    if (Timer >= 120)
                    {
                        Timer = 0;
                        State = 0;
                    }
                    break;
            }
        }

        private bool PhaseOne_MoveToTarget()
        {
            Vector2 targetPosition = target.Center - new Vector2(0, 360);
            npc.Center = Vector2.Lerp(npc.Center, targetPosition, Timer);
            Timer += 0.045f;

            return Timer >= 1f;
        }
    }
}
