using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.GemSwords
{
    public class RubySword : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 19;
            item.melee = true;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6f;
            item.value = Item.sellPrice(0, 0, 60);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.scale = 1.2f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) => Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height, 90).noGravity = true;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 10);
            recipe.AddIngredient(ItemID.Ruby, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
