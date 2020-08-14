using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Materials.Souls
{
    public class SoulNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.player[npc.FindClosestPlayer()].ZoneUnderworldHeight)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfFire>());
            }
        }
    }
}
