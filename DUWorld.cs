using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace DarknessUnbound
{
    public class DUWorld : ModWorld
    {
        public static bool restlessShadows;

        public override void Initialize()
        {
            restlessShadows = false;
            //DarknessUnbound.SET_UNDERTABLE_DIALOGUE("Hello, world! I'm blue and you're a large blongus! This line is here to test the line breaking", ModContent.GetTexture("DarknessUnbound/NPCs/Bosses/EthosOfTerraria/TerrariaEthos"));
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if (genIndex != -1)
            {
                tasks.Add(new PassLegacy("Post Terrain", delegate (GenerationProgress progress) {
                    // We can inline the world generation code like this, but if exceptions happen within this code 
                    // the error messages are difficult to read, so making methods is better. This is called an anonymous method.
                    progress.Message = "Melting ice...";
                    MeltIce();
                }));
            }
        }

        private void MeltIce()
        {
            bool foundAnchor = false;
            bool foundEnd = false;

            Point anchor = new Point(0, 0);
            Point end = new Point(0, 0);

            for (int i = Main.maxTilesX; i > 0; i--)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.type == TileID.SnowBlock || tile.type == TileID.IceBlock || tile.type == TileID.CorruptIce || tile.type == TileID.FleshIce)
                    {
                        foundAnchor = true;
                        anchor = new Point(i, j);
                    }
                    else continue;
                }
            }

            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.type == TileID.SnowBlock || tile.type == TileID.IceBlock || tile.type == TileID.CorruptIce || tile.type == TileID.FleshIce)
                    {
                        foundEnd = true;
                        end = new Point(i, j);
                    }
                    else continue;
                }
            }

            if (!foundAnchor) mod.Logger.Error("Anchor for melted ice biome not found. Generation could not be completed");
            if (!foundEnd) mod.Logger.Error("End for melted ice biome not found. Generation could not be completed");
            
            if (foundAnchor && foundEnd)
            {
                mod.Logger.Debug("Anchor and end for melted ice biome found. Starting generation");
                mod.Logger.Debug("Let the record show that the anchor is X: " + anchor.X.ToString() + ", Y: " + anchor.Y.ToString() + " And that the end is X: " + end.X.ToString() + ", Y: " + end.Y.ToString());

                byte modX = (byte)WorldGen.genRand.Next(20, 48);
                byte modY = (byte)WorldGen.genRand.Next(4, 8);
                anchor.X += modX;
                anchor.Y += modY;
                end.X += -modX;
                end.Y += modY;

                int range = end.X - anchor.X;
                mod.Logger.Debug("Range: " + range.ToString());

                // LIQUIDS
                mod.Logger.Debug("Handeling liquids and clearing for the melted ice biome");

                try
                {
                    for (int i = anchor.X; i < end.X; i++)
                    {
                        for (int j = anchor.Y; j < Main.maxTilesY; j++)
                        {
                            Tile tile = Main.tile[i, j];

                            if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick || tile.type == TileID.RainbowBrick)
                                continue;

                            tile.liquidType();
                            tile.liquid = 0;

                            if (tile.type == TileID.Ash) break;

                            WorldGen.TileRunner(i, j, 16, 12, -1);
                            tile.wall = WallID.None;
                        }
                    }
                }
                catch (Exception e)
                {
                    MeltedIce_HandleError(e);
                    return;
                }
                // LIQUIDS DONE

                // WALLING IN
                mod.Logger.Debug("Walling in the melted ice biome");

                int goneDownTo = anchor.Y;
                int goneDownToEnd = end.Y;

                try
                {
                    // Anchor
                    while (true)
                    {
                        int Y = goneDownTo;
                        Y++;
                        goneDownTo = Y;
                        Tile tile = Framing.GetTileSafely(anchor.X, Y);

                        if (tile.type == TileID.Ash) break;
                        if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick)
                            continue;
                        WorldGen.TileRunner(anchor.X + WorldGen.genRand.Next(-6, 7), Y, 6d, 12, TileID.RainbowBrick, true);
                    }

                    // End
                    while (true)
                    {
                        int Y = goneDownToEnd;
                        Y++;
                        goneDownToEnd = Y;
                        Tile tile = Framing.GetTileSafely(end.X, Y);

                        if (tile.type == TileID.Ash) break;
                        if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick)
                            continue;

                        WorldGen.TileRunner(end.X + WorldGen.genRand.Next(-6, 7), Y, 6d, 12, TileID.RainbowBrick, true);
                    }

                    for (int i = anchor.X; i < end.X; i += 5)
                    {
                        Tile tile = Framing.GetTileSafely(i, anchor.Y);

                        if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick)
                            continue;

                        WorldGen.TileRunner(i, anchor.Y, 5d, 16, TileID.RainbowBrick, true);
                    }

                    for (int i = anchor.X; i < end.X; i++)
                    {
                        Tile tile = Framing.GetTileSafely(i, goneDownTo);

                        if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick)
                            continue;

                        WorldGen.TileRunner(i, goneDownTo, 6d, 12, TileID.RainbowBrick, true);
                    }
                }
                catch (Exception e)
                {
                    MeltedIce_HandleError(e);
                    return;
                }
                // WALLED IN

                // TERRAIN
                mod.Logger.Debug("Making the melted ice terrain");

                int counter = 0;

                try
                {
                    while (true)
                    {
                        Point pos = new Point(anchor.X + WorldGen.genRand.Next(range), anchor.Y + Main.rand.Next(goneDownTo) - 200);
                        Tile tile = Framing.GetTileSafely(pos.X, pos.Y);
                        if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick)
                            continue;
                        if (tile.wall == WallID.BlueDungeonUnsafe || tile.wall == WallID.BlueDungeonSlabUnsafe || tile.wall == WallID.BlueDungeonTileUnsafe ||
                            tile.wall == WallID.GreenDungeonUnsafe || tile.wall == WallID.GreenDungeonSlabUnsafe || tile.wall == WallID.GreenDungeonTileUnsafe ||
                            tile.wall == WallID.PinkDungeonUnsafe || tile.wall == WallID.PinkDungeonSlabUnsafe || tile.wall == WallID.PinkDungeonTileUnsafe)
                            continue;

                        if (WorldGen.genRand.NextBool(140))
                        {
                            WorldGen.TileRunner(pos.X, pos.Y, WorldGen.genRand.Next(28, 36), WorldGen.genRand.Next(28, 39), TileID.IceBrick, true);

                            counter++;
                        }

                        if (counter >= 255) break;
                    }
                }
                catch (Exception e)
                {
                    MeltedIce_HandleError(e);
                    return;
                }
                // TERRAIN DONE

                // FREEING SPACE
                /*mod.Logger.Debug("Freeing space in the melted ice biome");

                int counter = 0;

                try
                {
                    while (true)
                    {
                        Point pos = new Point(anchor.X + WorldGen.genRand.Next(range), anchor.Y + Main.rand.Next(goneDownTo) - 200);
                        Tile tile = Framing.GetTileSafely(pos.X, pos.Y);
                        if (tile.type == TileID.BlueDungeonBrick || tile.type == TileID.GreenDungeonBrick || tile.type == TileID.PinkDungeonBrick)
                            continue;
                        if (tile.wall == WallID.BlueDungeonUnsafe || tile.wall == WallID.BlueDungeonSlabUnsafe || tile.wall == WallID.BlueDungeonTileUnsafe ||
                            tile.wall == WallID.GreenDungeonUnsafe || tile.wall == WallID.GreenDungeonSlabUnsafe || tile.wall == WallID.GreenDungeonTileUnsafe ||
                            tile.wall == WallID.PinkDungeonUnsafe || tile.wall == WallID.PinkDungeonSlabUnsafe || tile.wall == WallID.PinkDungeonTileUnsafe)
                            continue;

                        if (WorldGen.genRand.NextBool(140))
                        {
                            WorldGen.TileRunner(pos.X, pos.Y, WorldGen.genRand.Next(14, 36), WorldGen.genRand.Next(28, 39), -1);

                            counter++;
                        }

                        if (counter >= 255) break;
                    }
                }
                catch (Exception e)
                {
                    MeltedIce_HandleError(e);
                    return;
                }*/
                // SPACE FREED

                // MERGI
                mod.Logger.Debug("Hiding something in the melted ice biome");

                try
                {
                    Point pos = new Point(anchor.X + (range / 2), (goneDownTo - 100));
                    WorldGen.TileRunner(pos.X, pos.Y, 28, 46, TileID.LunarBlockVortex, true);
                }
                catch (Exception e)
                {
                    MeltedIce_HandleError(e);
                    return;
                }

                // KILLING UNDERWORLD
                mod.Logger.Debug("Cooling the underworld");

                try
                {
                    for (int j = goneDownTo + 20; j < Main.maxTilesY; j++)
                    {
                        for (int i = anchor.X; i < end.X; i++)
                        {
                            Tile tile = Main.tile[i, j];

                            tile.liquidType();
                            tile.liquid = 0;

                            WorldGen.TileRunner(i, j, 16, 12, -1);
                            tile.wall = WallID.None;
                        }
                    }
                }
                catch (Exception e)
                {
                    MeltedIce_HandleError(e);
                    return;
                }
            }
        }

        private void MeltedIce_HandleError(Exception e)
        {
            mod.Logger.Debug("Error occured while walling in melted ice biome: " + e.ToString());
            mod.Logger.Debug("Melted ice biome generation has been aborted");
        }

        public override TagCompound Save() => new TagCompound() {
            { "RestlessShadows", restlessShadows }
        };

        public override void Load(TagCompound tag)
        {
            restlessShadows = tag.GetBool("RestlessShadows");
        }
    }
}
