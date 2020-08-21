using DarknessUnbound.Dusts;
using DarknessUnbound.Items.Materials;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace DarknessUnbound.MeltBiome.Blocks
{
    public class SoftIce_Tile : ModTile
    {
        public override void SetDefaults()
        {
            AddMapEntry(new Color(114, 230, 234));

            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = true;

            Main.tileSpelunker[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            drop = ModContent.ItemType<SoftIce>();
            dustType = 217;
        }
    }
}
