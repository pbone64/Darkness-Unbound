using DarknessUnbound.Items.Weapons.Throwing.Unconsumable;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace DarknessUnbound
{
    public class DUWorld : ModWorld
    {
        public static bool restlessShadows;
        public static bool saidThatCultistsAreSealedByMoons;
        public static bool saidCultistMessage;

        public override void Initialize()
        {
            restlessShadows = false;
            saidThatCultistsAreSealedByMoons = false;
            saidCultistMessage = false;
        }

        public override void PreUpdate()
        {
            if (restlessShadows && Main.expertMode)
            {
                Main.expertDamage = 3f;
                Main.expertKnockBack = 0.5f;
                Main.expertDebuffTime = 3.5f;
                Main.expertLife = 3f;
                Main.expertNPCDamage = 3f;
            }
            else if (Main.expertMode)
            {
                Main.expertDamage = 2f;
                Main.expertKnockBack = 0.9f;
                Main.expertDebuffTime = 2f;
                Main.expertLife = 2f;
                Main.expertNPCDamage = 1.5f;
            }
            else
            {
                Main.expertDamage = 1f;
                Main.expertKnockBack = 1f;
                Main.expertDebuffTime = 1f;
                Main.expertLife = 1f;
                Main.expertNPCDamage = 1f;
            }

            if (saidThatCultistsAreSealedByMoons && !saidCultistMessage && NPC.downedHalloweenKing && NPC.downedHalloweenTree && NPC.downedChristmasIceQueen && NPC.downedChristmasSantank && NPC.downedChristmasTree)
            {
                string chat = "The ancient cultists are free!";
                if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(chat, Color.Cyan);
                else
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(chat), Color.Cyan);
                    saidCultistMessage = true;
                    NetMessage.SendData(MessageID.WorldData);
                }

                saidCultistMessage = true;
            }
        }

        public override void PostWorldGen()
        {
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
                {
                    if (Main.rand.NextBool(2))
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == ItemID.None)
                            {
                                chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<Bashosen>());
                                break;
                            }
                        }
                    }
                }
            }
        }

        #region WORLDGEN
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {

            /*tasks.Add(new PassLegacy("Post Terrain", delegate (GenerationProgress progress)
            {
                // We can inline the world generation code like this, but if exceptions happen within this code 
                // the error messages are difficult to read, so making methods is better. This is called an anonymous method.
                progress.Message = "Melting ice...";
                MeltIce(progress);
            }));*/
        }

        private void MeltIce(GenerationProgress progress)
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
            progress.Value += 0.05f;

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
            progress.Value += 0.05f;

            if (!foundAnchor) mod.Logger.Error("Anchor for melted ice biome not found. Generation could not be completed");
            if (!foundEnd) mod.Logger.Error("End for melted ice biome not found. Generation could not be completed");
            
            if (foundAnchor && foundEnd)
            {
                mod.Logger.Debug("Anchor and end for melted ice biome found. Starting generation");
                mod.Logger.Debug("Let the record show that the anchor is X: " + anchor.X.ToString() + ", Y: " + anchor.Y.ToString() + " And that the end is X: " + end.X.ToString() + ", Y: " + end.Y.ToString());
                progress.Value += 0.1f; // 0.2

                byte modX = (byte)WorldGen.genRand.Next(20, 48);
                byte modY = (byte)WorldGen.genRand.Next(4, 8);
                anchor.X += modX;
                anchor.Y += modY;
                end.X += -modX;
                end.Y += modY;

                int range = end.X - anchor.X;
                mod.Logger.Debug("Range: " + range.ToString());

                // LIQUIDS
                mod.Logger.Debug("Handeling liquids and convering the melted ice biome");
                progress.Value += 0.1f; // 0.3;
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

                            if ((tile.type == TileID.WoodBlock && tile.type == TileID.WoodenBeam) || Main.tileContainer[tile.type])
                            {
                                tile.type = 0;
                                tile.active(false);

                                continue;
                            }

                            tile.type = TileID.IceBlock;
                            tile.wall = WallID.None;
                        }
                    }
                    // LIQUIDS

                    progress.Value += 0.05f;    // 0.35
                    for (int i = anchor.X; i < end.X; i += WorldGen.genRand.Next(5, 9))
                    {
                        for (int j = anchor.Y; j < Main.maxTilesY; j += WorldGen.genRand.Next(5, 9))
                        {
                            Tile tile = Main.tile[i, j];

                            if (Main.rand.NextBool(3))
                            {
                                ushort type = 0;
                                switch (Main.rand.Next(11))
                                {
                                    case 0:
                                    case 1:
                                        type = 162;
                                        break;
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                    case 6:
                                        type = TileID.SnowBlock;
                                        break;
                                    case 7:
                                    case 8:
                                    case 9:
                                        type = TileID.IceBlock;
                                        break;
                                    case 10:
                                        type = TileID.Slush;
                                        break;
                                }

                                WorldGen.TileRunner(i, j, WorldGen.genRand.Next(10, 14) - (type == TileID.Slush ? 5 : 0), 14, type);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    MeltedIce_HandleError(e);
                    return;
                }

                #region removed
                // LIQUIDS DONE

                // WALLING IN
                /*mod.Logger.Debug("Walling in the melted ice biome");
                progress.Value += 0.1f; // 0.45

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
                }*/
                // WALLED IN
                #endregion

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
                    Point pos = new Point(anchor.X + (range / 2), (Main.maxTilesY - 300));
                    WorldGen.TileRunner(pos.X, pos.Y, 28, 46, TileID.LunarBlockVortex, true);
                }
                catch (Exception e)
                {
                    MeltedIce_HandleError(e);
                    return;
                }
                progress.Value += 0.05f; // 0.5f;

                // KILLING UNDERWORLD
                mod.Logger.Debug("Cooling the underworld");

                try
                {
                    for (int j = Main.maxTilesY - 200 + 20; j < Main.maxTilesY; j++)
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
        #endregion

        public static void GenOre(ushort type, int steps, double strength, int times)
        {
            for (int i = 0; i < times; i++)
                WorldGen.OreRunner(WorldGen.genRand.Next(Main.maxTilesX), WorldGen.genRand.Next(100, Main.maxTilesY / 4), strength, steps, type);
        }

        public override TagCompound Save() => new TagCompound() {
            { "RestlessShadows", restlessShadows },
            { "CultistMessage", saidThatCultistsAreSealedByMoons },
            { "SaidCultistMessage", saidCultistMessage }
        };

        public override void Load(TagCompound tag)
        {
            restlessShadows = tag.GetBool("RestlessShadows");
            saidThatCultistsAreSealedByMoons = tag.GetBool("CultistMessage");
            saidCultistMessage = tag.GetBool("SaidCultistMessage");
        }
    }
}
