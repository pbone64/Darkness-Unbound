using DarknessUnbound.Helpers;
using DarknessUnbound.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons
{
    public class TerraBladeMK2 : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Blade MKII");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 160;
            item.crit = 30;
            item.melee = true;
            item.Hitbox.Inflate(14, 14);
            item.useTime = item.useAnimation = 13;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item60;
            item.shootSpeed = 16f;
            item.shoot = ModContent.ProjectileType<TerraMK2Blade>();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust = Main.dust[Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 107, 0, 0, 0, Color.White, 1f)];
            dust.noGravity = true;
            dust.scale *= 0.985f;
        }

        public override void HoldItem(Player player)
        {
            //colors
            Color green = new Color(13, 112, 41);
            Color blue = new Color(0, 97, 180);
            Lighting.AddLight(player.Center, new AnimatedColor(green, blue, 1f).LightingColor());
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int projTotal = Main.rand.Next(1, 3);

            for (int i = 0; i < projTotal; i++)
            {
                Vector2 randSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                randSpeed = randSpeed * (1f - Main.rand.NextFloat() * 0.1f);
                Projectile.NewProjectile(position, randSpeed, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TerraBlade);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 3);
            recipe.AddIngredient(ItemID.SpectreBar, 3);
            recipe.AddIngredient(ItemID.ShroomiteBar, 3);
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
