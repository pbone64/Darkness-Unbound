using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories
{
    public class AccessoryDropNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.IceBat:
                    if (Main.rand.NextBool(100)) Item.NewItem(npc.getRect(), ModContent.ItemType<IcyStone>());
                    break;
            }
        }
    }
}
