using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.GemSwords
{
    public class DiamondSword : DarknessItem
    {
        public override void SafeSetDefaults()
        {
            item.damage = 20;
            item.melee = true;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6f;
            item.value = Item.sellPrice(0, 0, 60);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.scale = 1.2f;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) => Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height, 91).noGravity = true;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumBar, 10);
            recipe.AddIngredient(ItemID.Diamond, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
