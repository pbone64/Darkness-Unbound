using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Tiles
{
    public class DUGlobalTile : GlobalTile
    {
        public override bool CanExplode(int i, int j, int type)
        {
            if (type == TileID.Obsidian) return false;
            return base.CanExplode(i, j, type);
        }
    }
}
