using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing.Unconsumable
{
    public class MeteorShuriken : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 21;
            item.thrown = true;
            item.useTime = item.useAnimation = 17;
            item.knockBack = 1f;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<MeteorShurikenPro>();
            item.shootSpeed = 12.5f;
            item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
