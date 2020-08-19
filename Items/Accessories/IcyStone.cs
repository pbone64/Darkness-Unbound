using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Accessories
{
    public class IcyStone : DarknessItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Magic attacks inflict frostburn damage");
        }

        public override void SafeSetDefaults()
        {
            item.accessory = true;
            item.value = Item.sellPrice(0, 2);
            item.rare = ItemRarityID.Orange;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<DUPlayer>().icyStone = true;
        }
    }

    #region Effects
    public class IcyStoneGlobalNPC : GlobalProjectile
    {
        public override bool PreAI(Projectile projectile)
        {
            if (projectile.magic && Main.rand.NextBool(12) && Main.player[projectile.owner].GetModPlayer<DUPlayer>().icyStone)
            {
                Dust dust15 = Dust.NewDustDirect(new Vector2(projectile.position.X - 2f, projectile.position.Y - 2f), projectile.width + 4, projectile.height + 4, 135, projectile.velocity.X * 0.4f, projectile.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                dust15.noGravity = true;
                dust15.velocity *= 1.8f;
                dust15.velocity.Y -= 0.5f;
                if (Main.rand.Next(4) == 0)
                {
                    dust15.noGravity = false;
                    dust15.scale *= 0.5f;
                }
            }
            return true;
        }
    }

    public class IcyStoneGlobalItem : GlobalItem
    {
        public override void MeleeEffects(Item item, Player player, Rectangle hitbox)
        {
            if (item.magic && Main.rand.NextBool(8) && player.GetModPlayer<DUPlayer>().icyStone)
            {
                Dust dust15 = Dust.NewDustDirect(new Vector2(hitbox.X - 2f, hitbox.Y - 2f), hitbox.Width + 4, hitbox.Height + 4, 135, 1f, 1f, 100, default(Color), 3.5f);
                dust15.noGravity = true;
                dust15.velocity *= 1.8f;
                dust15.velocity.Y -= 0.5f;
                if (Main.rand.Next(4) == 0)
                {
                    dust15.noGravity = false;
                    dust15.scale *= 0.5f;
                }
            }
        }
    }
    #endregion
}
