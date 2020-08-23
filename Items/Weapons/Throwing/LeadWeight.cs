using DarknessUnbound.Projectiles.Weapons.Throwing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Throwing
{
    public class LeadWeight : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 16;
            item.thrown = true;
            item.useTime = item.useAnimation = 16;
            item.knockBack = 0.5f;
            item.maxStack = 999;
            item.consumable = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<LeadWeightPro>();
            item.shootSpeed = 6f;
            item.value = Item.sellPrice(0, 0, 0, 7);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeadBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 55);
            recipe.AddRecipe();
        }
    }
}
