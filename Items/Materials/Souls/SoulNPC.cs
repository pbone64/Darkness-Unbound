using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Materials.Souls
{
    public class SoulNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.hardMode) //yeah?
            {
                //earth
                if (Main.player[npc.FindClosestPlayer()].ZoneJungle && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfEarth>());
                if (Main.player[npc.FindClosestPlayer()].ZoneUndergroundDesert && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfEarth>());
                if (Main.player[npc.FindClosestPlayer()].ZoneGlowshroom && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfEarth>());
                //yeah add the rest, cba rn

                //water
                if (Main.player[npc.FindClosestPlayer()].ZoneSnow && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfWater>());
                if (Main.player[npc.FindClosestPlayer()].ZoneBeach && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfWater>());
                //fire
                if (Main.player[npc.FindClosestPlayer()].ZoneUnderworldHeight && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfFire>());
                //air
                if (Main.player[npc.FindClosestPlayer()].ZoneSkyHeight && Main.rand.NextBool(5))
                    Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfFire>());
            }
        }
    }
}
