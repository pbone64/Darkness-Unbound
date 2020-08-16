using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Tropidium
{
    public class FilletingKnife : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Filleting Knife");
            Tooltip.SetDefault("insert effect tooltip");
        }
        public override void SafeSetDefaults()
        {
            item.useTime = item.useAnimation = 8;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.damage = 52;
            item.melee = true;
            item.rare = ItemRarityID.Blue;
            item.autoReuse = true;
            item.useTurn = true;
            item.UseSound = item.UseSound = SoundID.Item7;
            item.crit = 30;
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
