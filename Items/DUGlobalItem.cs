using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items
{
    public class DUGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.ObsidianHelm || item.type == ItemID.ObsidianShirt || item.type == ItemID.ObsidianPants)
            {
                item.defense++;
                item.rare = ItemRarityID.Orange;
            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            switch (item.type)
            {
                case ItemID.ObsidianHelm:
                    player.thrownCrit += 3;
                    break;
                case ItemID.ObsidianShirt:
                    player.thrownDamage += 0.03f;
                    break;
                case ItemID.ObsidianPants:
                    player.thrownDamage += 0.03f;
                    break;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int defIndex = tooltips.FindIndex((TooltipLine tt) => tt.Name == "Defense");

            switch (item.type)
            {
                case ItemID.ObsidianHelm:
                    if (defIndex != -1)
                        tooltips.Insert(defIndex + 1, new TooltipLine(mod, "ArmorRebalance", "3% increased throwing critical strike chance"));
                    break;
                case ItemID.ObsidianShirt:
                    if (defIndex != -1)
                        tooltips.Insert(defIndex + 1, new TooltipLine(mod, "ArmorRebalance", "3% increased throwing damage"));
                    break;
                case ItemID.ObsidianPants:
                    if (defIndex != -1)
                        tooltips.Insert(defIndex + 1, new TooltipLine(mod, "ArmorRebalance", "3% increaased throwing damage"));
                    break;
            }
        }

        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ItemID.ObsidianHelm && body.type == ItemID.ObsidianShirt && legs.type == ItemID.ObsidianPants) return "Obsidian Set";
            return base.IsArmorSet(head, body, legs);
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            if (set == "Obsidian Set")
            {
                player.setBonus = "10% increased throwing damage\n5% increased throwing critical strike chance";
                player.thrownDamage += 0.1f;
                player.thrownCrit += 5;
            }
        }
    }
}
