using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Consumables
{
    public class RestlessShadows : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'You're sins grow worser...'" +
                             "\nFoobar");
        }

        public override void SafeSetDefaults()
        {
            item.useTime = item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.DD2_EtherianPortalDryadTouch;
            item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine name = tooltips.FirstOrDefault((TooltipLine tt) => tt.Name == "ItemName");
            if (name != null)
                name.overrideColor = Color.Black;
        }

        public override bool UseItem(Player player)
        {
            DUWorld.restlessShadows = !DUWorld.restlessShadows;

            string chat = "";
            if (DUWorld.restlessShadows) chat = "Restless Shadows have been released. Have fun, kid";
            else chat = "Restless Shadows have been sealed. Coward";
            if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(chat, Color.DarkRed);
            else
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(chat), Color.DarkRed);
                NetMessage.SendData(MessageID.WorldData);
            }

            bool flag = true;
            foreach (NPC npc in from NPC n in Main.npc where n.boss select n)
            {
                flag = true;
            }
            return flag;
        }
    }
}
