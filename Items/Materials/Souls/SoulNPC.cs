using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Materials.Souls
{
    public class SoulNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.hardMode)
            {
                Player player = Main.player[npc.FindClosestPlayer()];

                //earth
                if ((player.ZoneJungle || player.ZoneUndergroundDesert || player.ZoneGlowshroom) && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfEarth>());

                //water
                if ((player.ZoneSnow || player.ZoneBeach) && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfWater>());

                //fire
                if (player.ZoneUnderworldHeight && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfFire>());

                //air
                if (player.ZoneSkyHeight && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfFire>());
            }
        }
    }
}
