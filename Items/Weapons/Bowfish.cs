using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons
{
    public class Bowfish : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("66% chance not to consume ammo" + 
            "\n'Hanging by a thread'");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 65;
            item.crit = 4;
            item.useTime = item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.autoReuse = true;
            item.knockBack = 1f;
            item.ranged = true;
            item.rare = ItemRarityID.Yellow;
            item.UseSound = SoundID.Item40;
            item.value = Item.buyPrice(0, 7, 25, 0);
            item.shoot = AmmoID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.shootSpeed = 18f;
        }
        public override bool ConsumeAmmo(Player player) => Main.rand.NextFloat() >= .66f;

        public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Megashark);
            recipe.AddIngredient(ItemID.WoodenBow, 2);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddIngredient(ItemID.ShroomiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 randSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(2));
            speedX = randSpeed.X;
            speedY = randSpeed.Y;

            if (type == ProjectileID.Bullet) type = ProjectileID.BulletHighVelocity;

            return true;
        }
    }
}
