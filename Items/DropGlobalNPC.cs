using DarknessUnbound.Items.Accessories.LunarEmblems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items
{
    public class DropGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            switch (npc.type)
            {
                case NPCID.MoonLordCore:
                    Item.NewItem(npc.getRect(), ModContent.ItemType<EldritchCore>());
                    break;
            }
        }
    }
}
