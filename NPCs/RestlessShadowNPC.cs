using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace DarknessUnbound.NPCs
{
    public class RestlessShadowNPC : GlobalNPC
    {
        public override void AI(NPC npc)
        {
            if (DUWorld.restlessShadows)
            {
                if (npc.noTileCollide)
                    npc.position += npc.velocity;
                else
                    if (npc.velocity.Y < 0) npc.position.Y += npc.velocity.Y;

                switch (npc.type)
                {
                    case NPCID.EyeofCthulhu:
                        if ((npc.ai[0] == 0 && npc.ai[3] == 40) || (npc.ai[0] == 3 && npc.ai[2] % 60 == 0))
                            Projectile.NewProjectileDirect(npc.Center, npc.DirectionTo(Main.player[npc.target].Center) * 8f, npc.ai[0] == 0 ? ProjectileID.DeathLaser : ProjectileID.PhantasmalBolt, npc.damage, 4f);

                        if (npc.ai[0] == 1 || npc.ai[0] == 2)
                        {
                            npc.immortal = true;
                            npc.dontTakeDamage = true;
                        }
                        else
                        {
                            npc.immortal = false;
                            npc.dontTakeDamage = false;
                        }

                        if (npc.life < npc.lifeMax * 0.85f && npc.ai[0] == 0)
                            npc.ai[0]++;

                        npc.ai[3]++;
                        break;
                }
            }
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.boss)
                ChatManager.DrawColorCodedString(spriteBatch, Main.fontDeathText,
                    $"AI: {npc.ai[0]}, {npc.ai[1]}, {npc.ai[2]}, {npc.ai[3]}\n" +
                    $"LocalAI: {npc.localAI[0]}, {npc.localAI[1]}, {npc.localAI[2]}, {npc.localAI[3]}",
                    Vector2.UnitY * 48, Color.Green, 0f, default, Vector2.One);
        }
    }
}
