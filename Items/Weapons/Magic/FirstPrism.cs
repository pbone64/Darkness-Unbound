using DarknessUnbound.Projectiles.Weapons.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Magic
{
    public class FirstPrism : DarknessItem
    {
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Fire a lifeform burning rainbow'");
		}

		public override void SafeSetDefaults()
		{
			item.CloneDefaults(ItemID.LastPrism);
			item.rare = ItemRarityID.LightRed;
			item.mana = 8;
			item.damage = 8;
			item.shoot = ModContent.ProjectileType<FirstPrismHoldout>();
			item.shootSpeed = 30f;
			item.value = Item.sellPrice(0, 2);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Glass, 100);
			recipe.AddIngredient(ItemID.Diamond);
			recipe.AddIngredient(ItemID.Ruby);
			recipe.AddIngredient(ItemID.Amber);
			recipe.AddIngredient(ItemID.Topaz);
			recipe.AddIngredient(ItemID.Emerald);
			recipe.AddIngredient(ItemID.Sapphire);
			recipe.AddIngredient(ItemID.Amethyst);
			recipe.AddIngredient(ItemID.HellstoneBar, 5);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		// Because this weapon fires a holdout projectile, it needs to block usage if its projectile already exists.
		public override bool CanUseItem(Player player) => player.ownedProjectileCounts[ModContent.ProjectileType<FirstPrismHoldout>()] <= 0;
	}
}
