using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
    public class TheWhirlpool : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Whirlpool");
            Tooltip.SetDefault("hella");
        }
		public override void SafeSetDefaults()
		{
			item.damage = 32;
			item.melee = true;
			item.useTime = 1;
			item.useAnimation = 1;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3;
			item.value = 35000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.useTurn = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;
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
