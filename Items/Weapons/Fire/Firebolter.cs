using DarknessUnbound.Items.Materials.Souls;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Fire
{
    public class Firebolter : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Burning Hot!'" +
                             "\nDoes not consume ammo" +
                             "\nRapidly shoots hellish arrows");
        }

        public override void SafeSetDefaults()
        {
			item.damage = 56;
            item.crit = 6;
			item.ranged = true;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = ProjectileID.HellfireArrow;
			item.shootSpeed = 16f;
		}

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SoulOfFire>(), 20);
            recipe.AddIngredient(ItemID.AdamantiteRepeater);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SoulOfFire>(), 20);
            recipe.AddIngredient(ItemID.TitaniumRepeater);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
