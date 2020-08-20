using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound
{
    public class DUPlayer : ModPlayer
    {
        public bool eldritchCore;
        public bool icyStone;
        public bool frostfireNecklace;

        public int eldritchCore_CountDown;

        public override void Initialize()
        {
            //SkyManager.Instance.Activate("DarknessUnbound:PillarSky");
        }

        public override void ResetEffects()
        {
            eldritchCore = false;
            icyStone = false;
            frostfireNecklace = false;

            eldritchCore_CountDown--;
            if (eldritchCore_CountDown < 0) eldritchCore_CountDown = 0;
        }

        #region OnHitBy
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (eldritchCore && eldritchCore_CountDown == 0) OnHitBy_EldritchCore();
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            if (eldritchCore && eldritchCore_CountDown == 0) OnHitBy_EldritchCore();
        }

        private void OnHitBy_EldritchCore()
        {
            for (int i = 0; i < 4; i++)
            {
                Projectile proj = Projectile.NewProjectileDirect(player.Center - new Vector2(Main.rand.Next(-215, 216), 800 + Main.rand.Next(-100, 101)), Vector2.UnitY * 8, ProjectileID.LunarFlare, 250, 6f, player.whoAmI);
                proj.velocity = proj.velocity.RotatedBy(proj.DirectionTo(player.Center).ToRotation() - MathHelper.PiOver2);
            }

            eldritchCore_CountDown = 24;
        }
        #endregion

        #region OnHit
        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (icyStone && item.magic) OnHit_IcyStone(target);
            if (frostfireNecklace && (item.summon || item.sentry)) OnHit_IcyStone(target);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (icyStone && proj.magic) OnHit_IcyStone(target);
            if (frostfireNecklace && (proj.minion || proj.sentry)) OnHit_IcyStone(target);
        }

        private void OnHit_IcyStone(NPC target) => target.AddBuff(BuffID.Frostburn, Main.rand.Next(2, 7) * 60);
        #endregion
    }
}
