using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Dusts
{
    public class GelumFlame : ModDust
    {
        public override bool Update(Dust dust)
        {
            float scal = dust.scale * 0.1f;
            if (scal > 1f)
            {
                scal = 1f;
            }
            dust.rotation += dust.velocity.X * 0.3f;
            Lighting.AddLight(dust.position, Color.CornflowerBlue.ToVector3() * scal / 2);

            return true;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor) => new Color(200, 200, 200, 0);
    }
}
