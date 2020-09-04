using DarknessUnbound.Helpers;
using DarknessUnbound.Skies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace DarknessUnbound
{
    public partial class DarknessUnbound : Mod
    {
        public static bool showEthosOptions;
        public Rectangle spareRect = default;
        public Rectangle killRect = default;
        public static int spared = -1;

        public static bool whiteScreen;
        public static float whiteFade;
        public static bool whiteLoop;

        public static Texture2D UNDERTABLE_BOX;
        public static Texture2D UNDERTABLE_HEADTEX;
        public static string UNDERTABLE_DIALOGUE = "";
        public static int UNDERTABLE_DIALOGUE_COUNTER = 0;
        public string UNDERTABLE_REALTEXT = "";
        public static bool UNDERTABLE_SAD = false;
        public static bool UNDERTABLE_ANGER = false;
        private static float[] UNDERTABLE_OLDMUS = new float[Main.musicFade.Length];

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            if (showEthosOptions)
            {
                Vector2 lengthSpare = ChatManager.GetStringSize(Main.fontDeathText, "Spare?", new Vector2(1.75f));
                Vector2 lengthKill = ChatManager.GetStringSize(Main.fontDeathText, "Kill.", new Vector2(1.75f));
                Vector2 screenCenter = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f);
                Vector2 posSpare = screenCenter - lengthSpare - new Vector2(lengthSpare.X / 2f, 96);
                Vector2 posKill = screenCenter + new Vector2(lengthKill.X, -lengthKill.Y) - new Vector2(0, 96);

                ChatManager.DrawColorCodedString(spriteBatch, Main.fontDeathText, "Spare?", posSpare, spareRect.Contains(Main.MouseScreen.ToPoint()) ? Color.YellowGreen : Color.Lime, 0f, default, new Vector2(1.75f));
                ChatManager.DrawColorCodedString(spriteBatch, Main.fontDeathText, "Kill.", posKill, killRect.Contains(Main.MouseScreen.ToPoint()) ? Color.YellowGreen : Color.Lime, 0f, default, new Vector2(1.75f));

                spareRect = new Rectangle((int)posSpare.X, (int)posSpare.Y, (int)lengthSpare.X, (int)lengthSpare.Y);
                killRect = new Rectangle((int)posKill.X, (int)posKill.Y, (int)lengthKill.X, (int)lengthKill.Y);

                if (spareRect.Contains(Main.MouseScreen.ToPoint()) && PlayerInput.Triggers.Current.MouseLeft)
                {
                    showEthosOptions = false;
                    spared = 0;
                }

                if (killRect.Contains(Main.MouseScreen.ToPoint()) && PlayerInput.Triggers.Current.MouseLeft)
                {
                    showEthosOptions = false;
                    spared = 1;
                }
            }

            if (whiteScreen)
            {
                whiteFade += whiteLoop ? -0.05f : 0.05f;

                if (whiteFade >= 1f) whiteLoop = true;

                if (whiteLoop && whiteFade <= 0f) whiteScreen = false;

                spriteBatch.Draw(Main.blackTileTexture, new Rectangle(-2, -2, Main.screenHeight * 2, Main.screenWidth * 2), Color.White * whiteFade);
            }

            if (!string.IsNullOrEmpty(UNDERTABLE_DIALOGUE))
            {
                if (UNDERTABLE_SAD)
                {
                    for (int i = 0; i < Main.musicFade.Length; i++)
                    {
                        Main.musicFade[i] = 0f;
                    }
                }

                float width = UNDERTABLE_BOX.Width;
                float halfWidth = width / 2f;
                float height = UNDERTABLE_BOX.Height;
                float halfHeight = height / 2f;
                float headWidth = UNDERTABLE_HEADTEX.Width;
                char[] textSplit = UNDERTABLE_DIALOGUE.ToCharArray();
                float delay = (UNDERTABLE_ANGER ? 3f : 6f);
                string oldText = UNDERTABLE_REALTEXT;
                UNDERTABLE_REALTEXT = "";
                UNDERTABLE_DIALOGUE_COUNTER++;
                for (int i = 0; i < textSplit.Length; i++)
                {
                    if (UNDERTABLE_DIALOGUE_COUNTER / delay >= i)
                    {
                        UNDERTABLE_REALTEXT += textSplit[i];
                        if (UNDERTABLE_SAD) UNDERTABLE_REALTEXT += " ";
                    }
                    else break;
                }
                if (UNDERTABLE_REALTEXT != oldText && !UNDERTABLE_SAD) Main.PlaySound(GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Beep"));
                Vector2 anchor = new Vector2(Main.screenWidth / 2f - halfWidth, Main.screenHeight - height - 20);

                spriteBatch.Draw(UNDERTABLE_BOX, anchor, Color.White);
                spriteBatch.Draw(UNDERTABLE_HEADTEX, anchor + new Vector2(UNDERTABLE_HEADTEX.Width / 2f, halfHeight / 2f), Color.White);

                //Vector2 length = ChatManager.GetStringSize(Main.fontCombatText[1], "* " + UNDERTABLE_REALTEXT, new Vector2(1.75f));
                string actuallyDrawThis = "";
                string[] realWords = UNDERTABLE_REALTEXT.Split();

                string line = "";
                foreach (string s in realWords)
                {
                    if (Main.fontCombatText[1].MeasureString(line).X * 1.75f < width - UNDERTABLE_HEADTEX.Width * 3.05f)
                    {
                        actuallyDrawThis += (" " + s);
                        line += (" " + s);
                    }
                    else
                    {
                        actuallyDrawThis += ("\n" + s);
                        line = (s);
                    }
                }
                ChatManager.DrawColorCodedString(spriteBatch, Main.fontCombatText[1], "* " + actuallyDrawThis, anchor + new Vector2(headWidth * 1.5f + 8, 30), (UNDERTABLE_SAD ? Color.DarkGray : UNDERTABLE_ANGER ? Color.Red : Color.White), 0f, default, new Vector2(1.5f));

                if (UNDERTABLE_DIALOGUE_COUNTER / delay > textSplit.Length + 8)
                {
                    if (UNDERTABLE_SAD) Main.musicFade = UNDERTABLE_OLDMUS;
                    SET_UNDERTABLE_DIALOGUE("", default);
                }
            }

            /*spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, 100, 100), new AnimatedColor(new Color[] { Color.Red, Color.Blue }).GetColor());
            spriteBatch.Draw(Main.blackTileTexture, new Rectangle(100, 100, 100, 100), new AnimatedColor(Color.Red, Color.Blue).GetColor());
            spriteBatch.Draw(Main.blackTileTexture, new Rectangle(100, 0, 100, 100), new AnimatedColor(new Color[] { Color.Blue, Color.Aqua, Color.SeaGreen, Color.Green }).GetColor());*/
        }

        public static void SET_UNDERTABLE_DIALOGUE(string text, Texture2D headTex, bool sad = false, bool anger = false)
        {
            UNDERTABLE_DIALOGUE = text;
            UNDERTABLE_HEADTEX = headTex;
            UNDERTABLE_DIALOGUE_COUNTER = 0;
            UNDERTABLE_ANGER = anger;
            UNDERTABLE_SAD = sad;
            if (sad) UNDERTABLE_OLDMUS = Main.musicFade;
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                SET_UNDERTABLE_DIALOGUE("", default);
                UNDERTABLE_BOX = ModContent.GetTexture("DarknessUnbound/TextBox");

                SkyManager.Instance["DarknessUnbound:EthosSky"] = new EthosP2Sky();
                SkyManager.Instance["DarknessUnbound:PillarSky"] = new PillarSky();

                Filters.Scene["Melt"] = new Filter(new ScreenShaderData("FilterBloodMoon").UseColor(Color.RoyalBlue).UseIntensity(1.21f), EffectPriority.High);
                //Filters.Scene["DUPillar"] = new Filter(new ScreenShaderData("Solar").UseColor(Color.Indigo).UseIntensity(10f).UseProgress(1f), EffectPriority.VeryHigh);
                
            }

            Load_MethodSwap();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.Leather);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddIngredient(ItemID.SwiftnessPotion);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(ItemID.HermesBoots);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddIngredient(ItemID.FlipperPotion);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.Flipper);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddIngredient(ItemID.IceBlock, 100);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.IceSkates);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddIngredient(ItemID.Daybloom);
            recipe.AddIngredient(ItemID.Blinkroot);
            recipe.AddIngredient(ItemID.Waterleaf);
            recipe.AddIngredient(ItemID.Shiverthorn);
            recipe.AddIngredient(ItemID.Moonglow);
            recipe.AddIngredient(ItemID.Deathweed);
            recipe.AddIngredient(ItemID.Fireblossom);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.FlowerBoots);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Leather, 3);
            recipe.AddIngredient(ItemID.Silk, 8);
            recipe.AddIngredient(ItemID.WaterWalkingPotion);
            recipe.AddTile(TileID.Loom);
            recipe.SetResult(ItemID.WaterWalkingBoots);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.BandofRegeneration);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.Aglet);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Vine, 6);
            recipe.AddIngredient(ItemID.Ruby);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.AnkletoftheWind);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.BoneSword);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Bone, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.BonePickaxe);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("IronBar", 10);
            recipe.AddIngredient(ItemID.GillsPotion);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(ItemID.DivingHelmet);
            recipe.AddRecipe();
        }
    }
}