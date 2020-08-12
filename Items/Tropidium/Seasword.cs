using DarknessUnbound.Projectiles.Tropidium;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
	public class Seasword : DarknessItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Seasword");
			Tooltip.SetDefault("insert effect tooltip");
		}

		public override void SafeSetDefaults() 
		{
			item.damage = 35;
			item.melee = true;
			//item.width = 40;
			//item.height = 40;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 3;
			item.value = 35000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
			item.scale = 1.5f;
		}
        public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(insert bar);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}