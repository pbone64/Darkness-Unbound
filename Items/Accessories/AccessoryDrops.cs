using DarknessUnbound.Items.Accessories.Thrower;
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
                case NPCID.WallofFlesh:
                    if (!Main.expertMode)
                    {
                        if (Main.rand.NextBool(2)) Item.NewItem(npc.getRect(), ModContent.ItemType<NinjaEmblem>());
                    }
                    break;
            }
        }
    }

    public class BossBagDropsItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            switch (context)
            {
                case "bossBag":
                    if (arg == ItemID.WallOfFleshBossBag)
                    {
                        if (Main.rand.NextBool(2)) player.QuickSpawnItem(ModContent.ItemType<NinjaEmblem>());
                    }
                    break;
            }
        }
    }
}
