using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Social;
using Terraria.UI;

namespace DarknessUnbound.Items
{
    public class TestCrystal : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Debug Crystal");
            Tooltip.SetDefault("This might do something");
        }
        public override void SafeSetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = item.useAnimation = 15;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;
        }

        public override bool AltFunctionUse(Player player) => true;
        public override bool UseItem(Player player)
        {
            Filters.Scene.Activate("Melt");
            Filters.Scene["Melt"].GetShader().UseProgress(60);

            if (player.altFunctionUse == 2)
            {
                Filters.Scene["Melt"].Deactivate();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                Main.NewText("D");
                DUWorld.GenOre(TileID.RainbowBrick, 10, 10, 1000);
            }

            return true;
        }
    }
}
