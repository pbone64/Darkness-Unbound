using DarknessUnbound.Items.Materials.Souls;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.Earth
{
    public class EarthenEdge : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Earthen Edge");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 75;
            item.crit = 8;
            item.melee = true;
            item.useTime = item.useAnimation = 26;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.LightRed;
            item.scale = 1.45f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) => Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 1, 0, 0, 0, Color.White);

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SoulOfEarth>(), 20);
            recipe.AddIngredient(ItemID.AdamantiteSword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<SoulOfEarth>(), 20);
            recipe.AddIngredient(ItemID.TitaniumSword);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
