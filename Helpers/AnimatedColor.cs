using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace DarknessUnbound.Helpers
{
    public class AnimatedColor
    {
        private static float colorCounter = 0f;
        private static bool colorLoop = false;
        private Color col1;
        private Color col2;

        private static float spd;

        public AnimatedColor(Color color1, Color color2, float speed)
        {
            col1 = color1;
            col2 = color2;
            spd = speed * 0.01f;
        }

        public static void Update()
        {
            colorCounter += !colorLoop ? spd : -spd;
            colorCounter = MathHelper.Clamp(colorCounter, 0, 1);
            if (colorCounter >= 1) colorLoop = true;
            if (colorCounter <= 0) colorLoop = false;
        }
        public Color GetColor() => Color.Lerp(col1, col2, colorCounter);
        public Vector3 LightingColor() 
        {
            Color src = GetColor();
            return new Vector3(src.R / 255f, src.G / 255f, src.B / 255f);
        }
    }
}
