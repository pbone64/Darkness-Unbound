using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Dusts
{
    public class TropidiumSolid : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.color = default;
            dust.scale *= Main.rand.NextFloat(0.5f, 2.25f);
        }

        public override bool Update(Dust dust)
        {
            dust.rotation += dust.velocity.X * 0.3f;
            dust.velocity *= 1.03f;
            dust.scale *= 0.975f;

            if (dust.scale < 0.05f) dust.active = false;

            Color red = new Color(40, 3, 20);
            Color blue = new Color(4, 23, 51);

            Lighting.AddLight(dust.position, new AnimatedColor(red, blue, 10).LightingColor());

            return true;
        }
    }
}
