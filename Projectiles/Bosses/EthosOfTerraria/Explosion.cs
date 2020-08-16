using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Projectiles.Bosses.EthosOfTerraria
{
    public class Explosion : ModProjectile
    {
		public override string Texture => "Terraria/Projectile_" + ProjectileID.ShadowBeamFriendly;

        public override void SetDefaults()
        {
			projectile.Size = Vector2.One * 64;
        }

        public override void AI()
        {
			projectile.ai[1] += 0.01f;
			projectile.scale = projectile.ai[1];
			projectile.ai[0]++;
			if (projectile.ai[0] >= (float)(3 * 4))
			{
				projectile.Kill();
				return;
			}

			projectile.alpha -= 63;
			if (projectile.alpha < 0)
				projectile.alpha = 0;

			

			if (projectile.ai[0] != 1f)
				return;

			projectile.position = projectile.Center;
			projectile.width = (projectile.height = (int)(52f * projectile.scale));
			projectile.Center = projectile.position;
			if (true)
			{
				Main.PlaySound(SoundID.Item14, projectile.position);
				for (int num873 = 0; num873 < 4; num873++)
				{
					int num874 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num874].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
				}

				for (int num875 = 0; num875 < 10; num875++)
				{
					int num876 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 200, default(Color), 2.7f);
					Main.dust[num876].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
					Main.dust[num876].noGravity = true;
					Dust dust = Main.dust[num876];
					dust.velocity *= 3f;
					num876 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 110778, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num876].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
					dust = Main.dust[num876];
					dust.velocity *= 2f;
					Main.dust[num876].noGravity = true;
					Main.dust[num876].fadeIn = 2.5f;
				}

				for (int num877 = 0; num877 < 5; num877++)
				{
					int num878 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 0, default(Color), 2.7f);
					Main.dust[num878].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation()) * projectile.width / 2f;
					Main.dust[num878].noGravity = true;
					Dust dust = Main.dust[num878];
					dust.velocity *= 3f;
				}

				for (int num879 = 0; num879 < 10; num879++)
				{
					int num880 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 0, default(Color), 1.5f);
					Main.dust[num880].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation()) * projectile.width / 2f;
					Main.dust[num880].noGravity = true;
					Dust dust = Main.dust[num880];
					dust.velocity *= 3f;
				}
			}


			Main.PlaySound(SoundID.Item14, projectile.position);
			for (int num881 = 0; num881 < 20; num881++)
			{
				int num882 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num882].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
				Dust dust = Main.dust[num882];
				dust.velocity *= 2f;
				Main.dust[num882].noGravity = true;
				Main.dust[num882].fadeIn = 2.5f;
			}

			for (int num883 = 0; num883 < 15; num883++)
			{
				int num884 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 107, 0f, 0f, 0, default(Color), 2.7f);
				Main.dust[num884].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation()) * projectile.width / 2f;
				Main.dust[num884].noGravity = true;
				Dust dust = Main.dust[num884];
				dust.velocity *= 3f;
			}

			float num885 = (float)Main.rand.NextDouble() * ((float)Math.PI * 2f);
			float num886 = (float)Main.rand.NextDouble() * ((float)Math.PI * 2f);
			float num887 = (float)Main.rand.NextDouble() * ((float)Math.PI * 2f);
			float num888 = 7f + (float)Main.rand.NextDouble() * 7f;
			float num889 = 7f + (float)Main.rand.NextDouble() * 7f;
			float num890 = 7f + (float)Main.rand.NextDouble() * 7f;
			float num891 = num888;
			if (num889 > num891)
				num891 = num889;

			if (num890 > num891)
				num891 = num890;

			for (int num892 = 0; num892 < 200; num892++)
			{
				int num893 = 135;
				float num894 = num891;
				if (num892 > 50)
					num894 = num889;

				if (num892 > 100)
					num894 = num888;

				if (num892 > 150)
					num894 = num890;

				int num895 = Dust.NewDust(projectile.position, 6, 6, 107, 0f, 0f, 100);
				Vector2 vector86 = Main.dust[num895].velocity;
				Main.dust[num895].position = projectile.Center;
				vector86.Normalize();
				vector86 *= num894;
				if (num892 > 150)
				{
					vector86.Y *= 0.5f;
					vector86 = vector86.RotatedBy(num887);
				}
				else if (num892 > 100)
				{
					vector86.X *= 0.5f;
					vector86 = vector86.RotatedBy(num885);
				}
				else if (num892 > 50)
				{
					vector86.Y *= 0.5f;
					vector86 = vector86.RotatedBy(num886);
				}

				Dust dust = Main.dust[num895];
				dust.velocity *= 0.2f;
				dust = Main.dust[num895];
				dust.velocity += vector86;
				if (num892 <= 200)
				{
					Main.dust[num895].scale = 2f;
					Main.dust[num895].noGravity = true;
					Main.dust[num895].fadeIn = Main.rand.NextFloat() * 2f;
					if (Main.rand.Next(4) == 0)
						Main.dust[num895].fadeIn = 2.5f;

					Main.dust[num895].noLight = true;
					if (num892 < 100)
					{
						dust = Main.dust[num895];
						dust.position += Main.dust[num895].velocity * 20f;
						dust = Main.dust[num895];
						dust.velocity *= -1f;
					}
				}
			}
		}
    }
}
