using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.GemSwords
{
    public class AmethystSword : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 11;
            item.melee = true;
            item.useTime = 29;
            item.useAnimation = 29;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5f;
            item.value = Item.sellPrice(0, 0, 20);
            item.rare = ItemRarityID.White;
            item.UseSound = SoundID.Item1;
            item.scale = 1.1f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) => Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height, 86).noGravity = true;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CopperBar, 10);
            recipe.AddIngredient(ItemID.Amethyst, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
