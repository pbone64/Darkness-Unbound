using DarknessUnbound.NPCs;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound
{
    public class PillarAdder : ModWorld
    {
        public static bool lastLunarApocalypse;

        public override void PreUpdate()
        {
            if (!lastLunarApocalypse && NPC.LunarApocalypseIsUp)
            {
                MovePillars();
            }

            lastLunarApocalypse = NPC.LunarApocalypseIsUp;
        }

        private void MovePillars()
        {
            Main.NewText("Moving...");
            float counter = 1;
            float quotient = Main.maxTilesX * 16f / 6f;

            foreach (NPC npc in from NPC n in Main.npc where n.IsAPillar() select n)
            {
                int num = Main.maxTilesX / 5;
                int num2 = (int)Main.worldSurface;
                for (int j = 0; j < 4; j++)
                {
                    int num3 = num * (1 + j);
                    bool flag = false;
                    for (int k = 0; k < 30; k++)
                    {
                        int num4 = Main.rand.Next(-100, 101);
                        for (int num5 = num2; num5 > 100; num5--)
                        {
                            if (!Collision.SolidTiles(num3 + num4 - 10, num3 + num4 + 10, num5 - 20, num5 + 15) && !WorldGen.PlayerLOS(num3 + num4 - 10, num5) && !WorldGen.PlayerLOS(num3 + num4 + 10, num5) && !WorldGen.PlayerLOS(num3 + num4 - 10, num5 - 20) && !WorldGen.PlayerLOS(num3 + num4 + 10, num5 - 20))
                            {
                                npc.position = new Vector2(quotient * counter, num5 * 16);
                                if (Main.netMode == 2 && npc.whoAmI < 200)
                                    NetMessage.SendData(23, -1, -1, null, npc.whoAmI);

                                flag = true;
                                break;
                            }
                        }
                    }
                }

                //npc.position.X = quotient * counter;
                counter++;
            }

            NPC.NewNPC((int)(quotient * counter), (int)Main.worldSurface * 16, ModContent.NPCType<TerrenceTheFatDragon>());
            Main.NewText("Moved.");
        }
    }

    public static class PillarUtils
    {
        public static bool IsAPillar(this NPC npc) => npc.type == NPCID.LunarTowerSolar || npc.type == NPCID.LunarTowerNebula || npc.type == NPCID.LunarTowerVortex || npc.type == NPCID.LunarTowerStardust;
    }
}
