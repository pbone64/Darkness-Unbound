using DarknessUnbound.Projectiles.Bosses.pbone;
using Microsoft.Xna.Framework;
using Steamworks;
using System;
using Terraria;
using Terraria.ID;
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
                            State = Main.rand.Next(3) + 1;
                            //Chat("Found you");
                        }
                    }
                    break;

                // Cultist
                case 1:
                    if (Timer == 0)
                    {
                        for (int i = 1; i < 7; i++)
                            Projectile.NewProjectileDirect(target.Center + (new Vector2(0, 600).RotatedBy(MathHelper.ToRadians(60 * i))), Vector2.Zero, ProjectileID.CultistBossLightningOrb, npc.damage, 100f).timeLeft = 100;

                        Chat("Cultist");
                    }
                    Timer++;

                    /*if (Timer % 60 == 0)
                        Projectile.NewProjectile(npc.Top, npc.DirectionTo(target.Center) * 16, ProjectileID.CultistBossFireBall, npc.damage, 100f);*/

                    if (Timer >= 300)
                    {
                        Timer = 0;
                        State = 0;
                    }
                    break;

                // Duke
                case 2:
                    if (Timer == 0) Chat("Duke fishron");
                    Timer++;

                    if (Timer % 100 == 0)
                    {
                        for (int i = -5; i < 5; i++)
                            Projectile.NewProjectile(npc.Center - new Vector2(1680 / 10 * i, 600), Vector2.UnitY * 10, ProjectileID.SharknadoBolt, npc.damage, 100f, Main.myPlayer);
                    }

                    if (Timer >= 200)
                    {
                        Timer = 0;
                        State = 0;
                    }
                    break;

                // Foobar
                case 3:
                    if (Timer == 0) Chat("Heehoo");
                    Timer++;
                    if (Timer == 100) Main.PlaySound(SoundID.Zombie, npc.Center, 104);

                    if (Timer >= 100)
                    {
                        if (Timer % 45 == 0)
                        {
                            Projectile.NewProjectile(target.oldPosition + target.velocity * 10f, Vector2.Zero, ModContent.ProjectileType<PhantasmalSphere>(), npc.damage, 100);
                        }

                        if (Timer % 4 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.One.RotatedByRandom(Math.PI * 2f), ProjectileID.PhantasmalEye, npc.damage, 100f);
                        }

                        if (Timer % 10 == 0)
                        {
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(target.Center) * 12f, ProjectileID.PhantasmalBolt, npc.damage, 100f);
                        }
                    }

                    if (Timer >= 300)
                    {
                        Timer = 0;
                        State = 0;
                    }
                    break;
            }

            /*MiscValue++;
            if (MiscValue % 3 == 0)
            {
                Projectile.NewProjectile(npc.Center, new Vector2(6, 0).RotatedBy(MathHelper.ToRadians(8 * MiscValue / 4)), ModContent.ProjectileType<ElementalBlast>(), npc.damage, 100f);
                Projectile.NewProjectile(npc.Center, new Vector2(-6, 0).RotatedBy(MathHelper.ToRadians(8 * MiscValue / 4)), ModContent.ProjectileType<ElementalBlast>(), npc.damage, 100f);
            }*/
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
