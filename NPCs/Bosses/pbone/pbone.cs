using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.NPCs.Bosses.pbone
{
    public partial class pbone : ModNPC
    {
        public override void SetDefaults()
        {
            npc.boss = true;
            npc.Size = new Vector2(32, 50);
            npc.lifeMax = 1000;
            npc.damage = 10;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.knockBackResist = 0f;
        }

        public Player target { get => Main.player[npc.target]; }
        public float State { get => npc.ai[0]; set => npc.ai[0] = value; }
        public float Timer { get => npc.ai[1]; set => npc.ai[1] = value; }

        public override void AI()
        {
            if (State >= 0 && State <= 3)
                AiPhaseOne();
        }

        public void Chat(string text)
        {
            Main.NewText($"<pbone> {text}.");
        }
    }
}
