using DarknessUnbound.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Ranged
{
    public class BFG : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BFG2011");
            Tooltip.SetDefault("'Looks like christmas came early'" +
                             "\nRight clck to shoot a devastating blast of doomed plasma");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 66;
            item.crit = 2;
            item.ranged = true;
            item.useTime = 5;
            item.useAnimation = 25;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = 10000;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item115;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.scale = 1.15f;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useTime = 18;
                item.useAnimation = 20;
            }
            else
            {
                item.useTime = 5;
                item.useAnimation = 25;
            }

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 50;

            position += (Vector2)HoldoutOffset();
            position -= new Vector2(0, 6);
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            if (player.altFunctionUse == 2)
            {
                // SUPER BLAST
            }
            else
                type = /*Main.rand.NextBool(2) ? 2 : 1*/ModContent.ProjectileType<BFGPlasmaRed>();

            Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.PiOver4 / 2f), type, damage, knockBack, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset() => new Vector2(-4, 10);
    }
}
