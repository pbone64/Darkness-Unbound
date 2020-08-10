using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DarknessUnbound.Helpers
{
    public class AnimatedColor
    {
        private static float colorCounter = 0f;
        private static bool colorLoop = false;
        private Color color1;
        private Color color2;

        public AnimatedColor(Color c1, Color c2)
        {
            color1 = c1;
            color2 = c2;
        }

        public static void Update()
        {
            colorCounter += !colorLoop ? 0.01f : -0.01f;
            colorCounter = MathHelper.Clamp(colorCounter, 0, 1);
            if (colorCounter >= 1) colorLoop = true;
            if (colorCounter <= 0) colorLoop = false;
        }
        public Color GetColor() => Color.Lerp(color1, color2, colorCounter);
        public Vector3 LightingColor() 
        {
            Color src = GetColor();
            return new Vector3(src.R / 255f, src.G / 255f, src.B / 255f);
        }
    }
}
