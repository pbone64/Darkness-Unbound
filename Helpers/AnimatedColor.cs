using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DarknessUnbound.Helpers
{
    public struct AnimatedColor
    {
        public Color color1;
        public Color color2;
        public float speedModifier;

        /// <param name="c1">The first color</param>
        /// <param name="c2">The second color</param>
        /// <param name="speedMod">A way to modifiy how fast it goes. Lower is slower, higher is faster</param>
        public AnimatedColor(Color c1, Color c2, float speedMod = 25f)
        {
            color1 = c1;
            color2 = c2;
            speedModifier = speedMod;
        }

        public Color GetColor() => Color.Lerp(color1, color2, (float)(Math.Sin(Main.GameUpdateCount / speedModifier) + 1f) / 2f);
        

        public Vector3 LightingColor()
        {
            Color src = GetColor();
            return new Vector3(src.R / 255f, src.G / 255f, src.B / 255f);
        }
    }
}
