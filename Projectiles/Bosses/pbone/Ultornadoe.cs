using DarknessUnbound.NPCs.Bosses.pbone;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Bosses.pbone
{
    public class Ultornadoe : ModProjectile
    {
		//public override string Texture => "Terraria/Projectile_" + ProjectileID.Sharknado;

        public override void SetStaticDefaults()
        {
			Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Sharknado);
			projectile.aiStyle = -1;
			projectile.timeLeft = int.MaxValue;
			projectile.scale = 1f;
		}

		public override Color? GetAlpha(Color lightColor) => Main.DiscoColor * 0.9f;

		public int target;

		public override void AI()
		{
			if (!NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.pbone.pbone>())) projectile.Kill();

			int num519 = 45;
			int num520 = 85;
			float num521 = 2f;
			int num522 = 150;
			int num523 = 42;

			if (projectile.velocity.X != 0f)
				projectile.direction = (projectile.spriteDirection = -Math.Sign(projectile.velocity.X));

			/*projectile.frameCounter++;
			if (projectile.frameCounter > 2)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}*/

			projectile.frame++;

			if (projectile.frame >= 6)
				projectile.frame = 0;

			if (projectile.localAI[0] == 0f && Main.myPlayer == projectile.owner)
			{
				projectile.localAI[0] = 1f;
				projectile.position.X += projectile.width / 2;
				projectile.position.Y += projectile.height / 2;
				projectile.scale = ((float)(num519 + num520) - projectile.ai[1]) * num521 / (float)(num520 + num519);
				projectile.width = (int)((float)num522 * projectile.scale);
				projectile.height = (int)((float)num523 * projectile.scale);
				projectile.position.X -= projectile.width / 2;
				projectile.position.Y -= projectile.height / 2;
				projectile.netUpdate = true;
			}

			if (projectile.ai[1] != -1f)
			{
				projectile.scale = ((float)(num519 + num520) - projectile.ai[1]) * num521 / (float)(num520 + num519);
				projectile.width = (int)((float)num522 * projectile.scale);
				projectile.height = (int)((float)num523 * projectile.scale);
			}



			projectile.alpha += 30;
			if (projectile.alpha > 200)
				projectile.alpha = 200;

			if (projectile.ai[0] > 0f)
				projectile.ai[0]--;

			if (projectile.ai[0] == 1f && projectile.ai[1] > 0f && projectile.owner == Main.myPlayer)
			{
				projectile.netUpdate = true;
				Vector2 center2 = projectile.Center;
				center2.Y -= (float)num523 * projectile.scale / 2f;
				float num524 = ((float)(num519 + num520) - projectile.ai[1] + 1f) * num521 / (float)(num520 + num519);
				center2.Y -= (float)num523 * num524 / 2f;
				center2.Y += 2f;
				Projectile.NewProjectile(center2.X, center2.Y, projectile.velocity.X, projectile.velocity.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 10f, projectile.ai[1] - 1f);
			}

			if (projectile.ai[0] <= 0f)
			{
				float num528 = (float)Math.PI / 30f;
				float num529 = (float)projectile.width / 5f;
				if (projectile.type == 386)
					num529 *= 2f;

				float num530 = (float)(Math.Cos(num528 * (0f - projectile.ai[0])) - 0.5) * num529;
				projectile.position.X -= num530 * (float)(-projectile.direction);
				projectile.ai[0]--;
				num530 = (float)(Math.Cos(num528 * (0f - projectile.ai[0])) - 0.5) * num529;
				projectile.position.X += num530 * (float)(-projectile.direction);
			}
		}
    }
}
