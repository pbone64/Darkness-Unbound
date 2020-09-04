using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DarknessUnbound.NPCs
{
    public class DUGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.Golem:
                    if (!NPC.downedHalloweenKing || !NPC.downedHalloweenTree || !NPC.downedChristmasIceQueen || !NPC.downedChristmasSantank || !NPC.downedChristmasTree)
                    {
                        string chat = "The ancient cultists remain sealed by the festive moons...";
                        if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(chat, Color.Yellow);
                        else NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(chat), Color.Yellow);

                        DUWorld.saidThatCultistsAreSealedByMoons = true;
                    }
                    else
                    {
                        string chat = "The ancient cultists are free!";
                        if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(chat, Color.Cyan);
                        else
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(chat), Color.Cyan);
                            DUWorld.saidCultistMessage = true;
                            NetMessage.SendData(MessageID.WorldData);
                        }

                        DUWorld.saidCultistMessage = true;
                    }
                    break;
            }
        }
    }
}
