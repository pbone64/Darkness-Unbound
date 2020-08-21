using DarknessUnbound.Dusts;
using DarknessUnbound.Items.Materials;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ObjectData;

namespace DarknessUnbound.Tiles
{
    public class TropidiumBar_Tile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileShine[Type] = 1100;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.MetalBar"));
            drop = ModContent.ItemType<TropidiumBar>();
            dustType = ModContent.DustType<TropidiumSolid>();
        }
        public override bool Drop(int i, int j)
        {
            Tile t = Main.tile[i, j];
            int style = t.frameX / 18;
            if (style == 0)
            {
                Item.NewItem(i * 16, j * 16, 16, 16, ModContent.ItemType<TropidiumBar>());
            }
            return base.Drop(i, j);
        }
    }
}
