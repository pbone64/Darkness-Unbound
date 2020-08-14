using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Materials.Souls
{
    public class SoulNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.player[npc.FindClosestPlayer()].ZoneUnderworldHeight && Main.rand.NextBool(5))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfFire>());
            }
        }
    }
}
