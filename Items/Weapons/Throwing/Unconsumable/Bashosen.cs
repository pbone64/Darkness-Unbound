using DarknessUnbound.Projectiles.Weapons.Throwing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing.Unconsumable
{
    public class Bashosen : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 18;
            item.thrown = true;
            item.useTime = item.useAnimation = 10;
            item.knockBack = 33f;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<BashosenPro>();
            item.shootSpeed = 10f;
            item.rare = ItemRarityID.Green;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = -2; i < 3; i++)
                Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedBy((MathHelper.Pi / 5f / 2.5f * i)), type, damage, knockBack, player.whoAmI);
            return false;
        }
    }
}
