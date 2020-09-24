using DarknessUnbound.NPCs.Bosses.Sagnus.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Consumables.BossSummons
{
    public class VermiMajor : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons Sagnus, Heavenly Scourge");
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 100;
        }

        public override void SafeSetDefaults()
        {
            item.maxStack = 20;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 30;
            item.useAnimation = 30;
            item.value = 0;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
        }

        public override bool CanUseItem(Player player) => !Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<SagnusHead>());

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Sagnus>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }
    }
}
