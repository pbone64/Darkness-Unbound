using DarknessUnbound.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.pbone
{
    public class TrappedVengeance : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Many great warlords have fallen to this blade...'");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 666666;
            item.crit = 66;
            item.melee = true;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 16;
            item.value = Item.sellPrice(10);
            item.rare = ItemRarityID.Green; 
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<ReleasedVengeance>();
            item.shootSpeed = 8f;
            item.scale = 2f;
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.PiOver4 / 4f), type, damage, knockBack);
            return true;
        }
    }
}
