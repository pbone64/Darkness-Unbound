using DarknessUnbound.Projectiles.Bosses.pbone;
using Microsoft.Xna.Framework;
using System.IO;
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
            npc.timeLeft = int.MaxValue;
        }

        public Player target { get => Main.player[npc.target]; }
        public float State { get => npc.ai[0]; set => npc.ai[0] = value; }
        public float Timer { get => npc.ai[1]; set => npc.ai[1] = value; }
        public float Counter { get => npc.ai[2]; set => npc.ai[2] = value; }

        public bool initialized = false;

        public override void AI()
        {
            if (!initialized)
            {
                (Projectile.NewProjectileDirect(npc.Center + new Vector2(640, 160), Vector2.Zero, ModContent.ProjectileType<Ultornadoe>(), 666666, 100f, Main.myPlayer, 20, 20).modProjectile as Ultornadoe).target = npc.target;
                (Projectile.NewProjectileDirect(npc.Center + new Vector2(-640, 160), Vector2.Zero, ModContent.ProjectileType<Ultornadoe>(), 666666, 100f, Main.myPlayer, 20, 20).modProjectile as Ultornadoe).target = npc.target;
                initialized = true;
            }

            if (npc.timeLeft < 666)
                npc.timeLeft = 666;

            if (State >= 0 && State <= 3)
                AiPhaseOne();
        }

        public void Chat(string text)
        {
            Main.NewText($"<pbone> {text}.");
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(initialized);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            initialized = reader.ReadBoolean();
        }
    }
}
