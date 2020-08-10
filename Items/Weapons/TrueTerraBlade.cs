using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DarknessUnbound.Items.Weapons
{
    public class TrueTerraBlade : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Forged from the finest blades in the cosmos'" +
                             "\nShoots ludicrously powerful True Terra Beams. These beams light hit enemies ablaze with the wrath of Terraria" +
                             "\nRains down a stream of Terra Comets upon the cursor posiion. These comets sunder hit enemies with the powerful of the elements" +
                             "\nNearby enemies are assulted with incredibly agile Terra Sparks. These sparkes hit multiple times and deal obscene damage." +
                             "\nHitting enemies with the blade supercharges you, greatly increasing your power. These diminish over time");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 250;
            item.crit = 25;
            item.useTime = item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.knockBack = 10f;
            item.melee = true;
            item.rare = ItemRarityID.Purple;
            item.UseSound = SoundID.Item1;
            item.value = Item.buyPrice(1);
            item.shoot = ProjectileID.TerraBeam;
            item.shootSpeed = 16f;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.AddBuff(BuffID.Ironskin, 600);
            target.immune[player.whoAmI] = 0;

            int[] projArray = getRandPro();

            for (int i = 0; i < projArray.Length; i++)
            {
                Projectile.NewProjectile(target.Center, Vector2.One.RotatedBy(MathHelper.ToRadians(i * (360 / projArray.Length))).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat() * 6)) * 15, projArray[i], (int)(item.damage / 1.5f), 0f, player.whoAmI);
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 shootPosition = position;
            float shootSpeedX = speedX;
            float shootSpeedY = speedY;
            Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }
            for (int i = 0; i < 3; i++)
            {
                shootPosition = player.Center + new Vector2((-(float)Main.rand.Next(401) * player.direction), -600f);
                shootPosition.Y -= (100 * i);
                Vector2 heading = target - shootPosition;
                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }
                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }
                heading.Normalize();
                heading *= new Vector2(shootSpeedX, shootSpeedY).Length();
                shootSpeedX = heading.X;
                shootSpeedY = heading.Y + 40 * 0.02f;
                Projectile.NewProjectile(shootPosition.X, shootPosition.Y, shootSpeedX, shootSpeedY, ProjectileID.StarWrath, (int)(damage * 1.5f), knockBack, player.whoAmI, 0f, ceilingLimit);
            }
            return true;
        }

        public static int[] getRandPro()
        {
            int[] ret = new int[8];

            for (int i = 0; i < 8; i++)
            {
                switch (Main.rand.Next(16))
                {
                    case 0:
                        ret[i] = ProjectileID.EnchantedBeam;
                        break;
                    case 1:
                        ret[i] = ProjectileID.SwordBeam;
                        break;
                    case 2:
                        ret[i] = ProjectileID.LightBeam;
                        break;
                    case 3:
                        ret[i] = ProjectileID.NightBeam;
                        break;
                    case 4:
                        ret[i] = ProjectileID.InfluxWaver;
                        break;
                    case 5:
                        ret[i] = ProjectileID.SporeCloud;
                        break;
                    case 6:
                        ret[i] = ProjectileID.ChlorophyteOrb;
                        break;
                    case 7:
                        ret[i] = ProjectileID.Meowmere;
                        break;
                    case 8:
                        ret[i] = ProjectileID.IceBolt;
                        break;
                    case 9:
                        ret[i] = ProjectileID.DeathSickle;
                        break;
                    case 10:
                        ret[i] = ProjectileID.IceSickle;
                        break;
                    case 11:
                        ret[i] = ProjectileID.Starfury;
                        break;
                    case 12:
                        ret[i] = ProjectileID.FlamingJack;
                        break;
                    case 13:
                        ret[i] = ProjectileID.OrnamentFriendly;
                        break;
                    case 14:
                        ret[i] = ProjectileID.DD2SquireSonicBoom;
                        break;
                    case 15:
                        ret[i] = ProjectileID.StarWrath;
                        break;
                }
            }

            return ret;
        }
    }
}
