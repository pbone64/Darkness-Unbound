using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound
{
    public class DUPlayer : ModPlayer
    {
        public bool eldritchCore;

        public override void ResetEffects()
        {
            eldritchCore = false;
        }

        #region OnHitBy
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (eldritchCore) OnHitBy_EldritchCore();
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (eldritchCore) OnHitBy_EldritchCore();
        }

        private void OnHitBy_EldritchCore()
        {
            for (int i = 0; i < 4; i++)
            {
                Projectile proj = Projectile.NewProjectileDirect(player.Center - new Vector2(Main.rand.Next(-215, 216), 800 + Main.rand.Next(-100, 101)), Vector2.UnitY * 8, ProjectileID.LunarFlare, 250, 6f, player.whoAmI);
                proj.velocity = proj.velocity.RotatedBy(proj.DirectionTo(player.Center).ToRotation() - MathHelper.PiOver2);
            }
        }
        #endregion
    }
}
