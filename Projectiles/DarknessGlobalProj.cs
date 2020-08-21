using DarknessUnbound.MeltBiome.Blocks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles
{
	public class DarknessGlobalProj : GlobalProjectile //thx seraph for pure conversion
	{
        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.PureSpray)
                Convert((int)(projectile.position.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16, 2);
        }
        public void Convert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                    {
                        int type = (int)Main.tile[k, l].type;
                        int wall = (int)Main.tile[k, l].wall;

                        /*if (wall == < insertwalltypetoconverthere >)
						{
							Main.tile[k, l].wall = (ushort)ModContent.WallType<Wall>();

						}*/

                        if (type == ModContent.TileType<SoftIce_Tile>())
                        {
                            Main.tile[k, l].type = TileID.IceBlock;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }


                    }
                }
            }
        }
    }
}
