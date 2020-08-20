using DarknessUnbound.Projectiles.Weapons.Throwing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing
{
    public class VenomShuriken : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts target with Venom" +
                             "\nRight click to throw three shurikens" +
                             "\n50% chance to not consume while using the left click" +
                             "\n33% chance to not consume while using the right click");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 66;
            item.crit = 8;
            item.thrown = true;
            item.useTime = item.useAnimation = 10;
            item.knockBack = 0f;
            item.maxStack = 999;
            item.consumable = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<VenomShurikenPro>();
            item.shootSpeed = 13f;
            item.rare = ItemRarityID.LightRed;
        }

        public override bool ConsumeItem(Player player) => player.altFunctionUse == 2 ? Main.rand.NextBool(3) : Main.rand.NextBool(2);
        
        public override bool AltFunctionUse(Player player) => true;
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
                item.useTime = item.useAnimation = 15;
            else
                item.useTime = item.useAnimation = 10;
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = -1; i < 2; i++)
                {
                    Projectile.NewProjectile(player.Center, new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(20) / 3f * i), type, damage, knockBack, player.whoAmI);
                }

                return false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Shuriken, 75);
            recipe.AddIngredient(ItemID.VialofVenom);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
