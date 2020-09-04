using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.NPCs
{
    public class TerrenceTheFatDragon : ModNPC
    {
        public override void SetDefaults()
        {
            npc.Size = new Vector2(64, 128);
            npc.lifeMax = int.MaxValue;
            npc.knockBackResist = 0f;
        }
    }
}