using DarknessUnbound.Dusts;
using DarknessUnbound.Items.Materials;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace DarknessUnbound.Tiles
{
    public class TropidiumOre_Tile : ModTile
    {
        public override void SetDefaults()
        {
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Tropidium");
            AddMapEntry(new Color(56, 255, 201), name);

            TileID.Sets.Ore[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileValue[Type] = 450;
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;

            drop = ModContent.ItemType<TropidiumOre>();
            minPick = 65;
            soundType = SoundID.Tink;
            dustType = ModContent.DustType<TropidiumSolid>();
            mineResist = 0.9f;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Color red = new Color(40, 3, 20);
            Color blue = new Color(4, 23, 51);
            Vector3 light = new AnimatedColor(red, blue, 10).LightingColor();

            r = light.X;
            g = light.Y;
            b = light.Z;
        }
    }
}
