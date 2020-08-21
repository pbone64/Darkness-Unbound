using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Dusts
{
    public class GelumSolution : ModDust
    {
        public override void SetDefaults()
        {
            updateType = 110;
        }
        public override bool Update(Dust dust)
        {
            float scal = dust.scale * 0.1f;
            if (scal > 1f)
            {
                scal = 1f;
            }
            Lighting.AddLight(dust.position, Color.CornflowerBlue.ToVector3() * scal / 2);

            return true;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor) => new Color(200, 200, 200, 0);
    }
}
