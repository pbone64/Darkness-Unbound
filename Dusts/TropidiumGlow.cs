using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Dusts
{
    public class TropidiumGlow : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.color = default;
            dust.scale *= Main.rand.NextFloat(1.2f, 2f);
        }

        public override bool Update(Dust dust)
        {
            dust.rotation += dust.velocity.X * 0.2f;
            dust.velocity *= 0.7f;
            dust.scale *= 0.975f;
            Color red = new Color(99, 10, 50);
            Color blue = new Color(7, 43, 96);
            Lighting.AddLight(dust.position, new AnimatedColor(red, blue, 1f).LightingColor());
            if (dust.scale < 0.05f)
            {
                dust.active = false;
            }
            return true;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return new Color(255, 255, 255, dust.alpha);
        }
    }
}
