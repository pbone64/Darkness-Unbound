using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DarknessUnbound.Helpers
{
    public struct AnimatedColor
    {
        public Color[] Colors;
        public Color color1;
        public Color color2;
        public float speedModifier;
        private bool multiColor;

        /// <param name="c1">The first color</param>
        /// <param name="c2">The second color</param>
        /// <param name="speedMod">A way to modifiy how fast it goes. Lower is slower, higher is faster</param>
        public AnimatedColor(Color c1, Color c2, float speedMod = 25f)
        {
            Colors = default;
            color1 = c1;
            color2 = c2;
            speedModifier = speedMod;
            multiColor = false;
        }

        /// <param name="colors">The colors in this AniamtedColor</param>
        /// <param name="speedMod">A way to modifiy how fast it goes. Higher is slower, lower is faster</param>
        public AnimatedColor(Color[] colors, float speedMod = 25f)
        {
            Colors = colors;
            color1 = default;
            color2 = default;
            speedModifier = speedMod;
            multiColor = true;
        }

        public Color GetColor()
        {
            if (!multiColor)
                return Color.Lerp(color1, color2, (float)(Math.Sin(Main.GameUpdateCount / speedModifier) + 1f) / 2f);
            else
            {
                float amount = Main.GameUpdateCount % 60 / speedModifier;
                int type = (int)(Main.GameUpdateCount / 60 % Colors.Length);
                return Color.Lerp(Colors[type], Colors[(type + 1) % Colors.Length], amount);
            }
        }

        public Vector3 LightingColor() => GetColor().ToVector3() / 255f;
    }
}
