using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System.Reflection;
using Terraria.ModLoader;

namespace DarknessUnbound.Dusts
{
    public class BasicDustNoVelocity : ModDust
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "DarknessUnbound/Dusts/BasicDust";
            return true;
        }

        public override bool Update(Dust dust)
        {
            dust.velocity.Y /= 0.98f;
            dust.velocity.X /= 0.98f;
            dust.scale -= 0.02f;
            if (dust.scale <= 0)
                dust.active = false;
            float scal = dust.scale * 0.5f;
            Lighting.AddLight(dust.position, scal * 0.3f, scal * 0.3f, scal * 0.3f);
            return false;
        }

        public override Color? GetAlpha(Dust dust, Color lightColor) => new Color(255, 255, 255, dust.alpha);
    }

    public class BasicDustGlow : ModDust
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "DarknessUnbound/Dusts/BasicDust";
            return true;
        }

        public override bool Update(Dust dust)
        {
            float scal = dust.scale * 0.5f;
            Lighting.AddLight(dust.position, scal * 0.3f, scal * 0.3f, scal * 0.3f);
            return true;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor) => new Color(255, 255, 255, dust.alpha);
    }
}
