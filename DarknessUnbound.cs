using DarknessUnbound.Skies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace DarknessUnbound
{
    public class DarknessUnbound : Mod
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
                float width = UNDERTABLE_BOX.Width;
                float halfWidth = UNDERTABLE_BOX.Width / 2f;

                spriteBatch.Draw(UNDERTABLE_BOX, new Vector2(Main.screenWidth / 2f - halfWidth, Main.screenHeight - UNDERTABLE_BOX.Height - 20), Color.White);

            }
        }

        public static void SET_UNDERTABLE_DIALOGUE(string text, Texture2D headTex)
        {
            UNDERTABLE_DIALOGUE = text;
            UNDERTABLE_DIALOGUE_COUNTER = 0;
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                SET_UNDERTABLE_DIALOGUE("MacGuffin", default);
                SkyManager.Instance["DarknessUnbound:EthosSky"] = new EthosP2Sky();
                UNDERTABLE_BOX = ModContent.GetTexture("DarknessUnbound/TextBox");
            }
        }
    }
}