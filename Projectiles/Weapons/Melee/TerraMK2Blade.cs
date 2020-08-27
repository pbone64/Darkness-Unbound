using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Weapons.Melee
{
    public class TerraMK2Blade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.netUpdate = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.width = projectile.height = 50;
            projectile.timeLeft = 500;
            projectile.aiStyle = 0;
            projectile.alpha = 245;
        }

        private Color green = new Color(13, 112, 41);
        private Color blue = new Color(0, 97, 180);

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45);

            foreach (NPC npc in from NPC n in Main.npc where n.active && !n.friendly && !n.dontTakeDamage && !n.immortal && n.lifeMax > 5 select n) //the pbone line
            {
                if (npc.CanBeChasedBy(this))
                {
                    float rangeMax = (npc.position - Main.MouseWorld).Length();
                    float distanceToNPC = (npc.Center - projectile.Center).Length();
                    if (//rangeMax <= 700 && rangeMax >= 3 && //for cursor limit
                        distanceToNPC <= 700) // necessary unless you want every projectile in the world to come forth
                    {
                        projectile.velocity += projectile.DirectionTo(npc.Center) * 2;
                    }
                }
            }

            for (int i = 0; i < 2; i++)
            {
                Dust dust = Dust.NewDustPerfect(projectile.Center, 107, null, 0, Color.Transparent, 1f);
                dust.noGravity = true;
                dust.noLight = true;
            }

            //colors
            Lighting.AddLight(projectile.Center / 16f, new AnimatedColor(green, blue).LightingColor());
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color projCol = new AnimatedColor(green, blue).GetColor();
            Texture2D tex = Main.projectileTexture[projectile.type];
            Rectangle rectangle = new Rectangle(0, 0, tex.Width, tex.Height);

            for (int d = 0; d > ProjectileID.Sets.TrailCacheLength[projectile.type]; d++)
            {
                Vector2 oldloc = projectile.oldPos[d];
                float oldang = projectile.oldRot[d];
                spriteBatch.Draw(tex, projectile.position - Main.screenPosition, new Rectangle?(rectangle), projCol, oldang, oldloc, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}
