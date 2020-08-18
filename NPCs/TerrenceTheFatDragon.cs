using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace DarknessUnbound.NPCs
{
    public class TerrenceTheFatDragon : ModNPC
    {
        public override void SetDefaults()
        {
            npc.Size = new Vector2(64, 128);
            npc.lifeMax = int.MaxValue;
        }
    }
}