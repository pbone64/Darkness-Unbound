using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.Items.Weapons.dev
{
    public class Naga : ModItem
    {
        private int comboCount = 0;
        private int comboTimer = 0;
        private const int maxCombo = 15;

        private bool isDashing = false;
        private int dashCoolDown = 0;
        private Color green = new AnimatedColor(Color.Yellow, Color.GreenYellow, 2f).GetColor();
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to dash into enemies" +
                "\n25% increased damage at full combo");
        }
        public override void SetDefaults()
        {
            item.width = 68;
            item.height = 74;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.damage = 250;
            item.autoReuse = true;
            item.useTime = 10;
            item.useAnimation = item.useTime;
            item.tileBoost = 3;
            item.rare = ItemRarityID.Purple;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.useTurn = true;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override bool AltFunctionUse(Player player) => true;
        public override void UseStyle(Player player)
        {
            bool dir = player.direction > 0;

            #region swing_rewrite
            float progress = 1f / player.itemAnimation;
            float rot = dir ? MathHelper.TwoPi : -MathHelper.TwoPi;
            player.itemRotation += rot;
            player.itemLocation = new Vector2(player.MountedCenter.X, player.MountedCenter.Y - 2);
            #endregion 

            #region abilities
            //if (player.controlUseItem && player.altFunctionUse == 0)
            foreach (NPC target in from NPC n in Main.npc where n.active && !n.friendly && !n.dontTakeDamage && !n.immortal && n.lifeMax > 5 select n) //npc tracking
            {
                float range = Vector2.Distance(player.Center, target.Center);
                if (range <= 690 && player.altFunctionUse == 2 && player.controlUseItem == false && target.CanBeChasedBy(player) && dashCoolDown <= 0)
                {
                    isDashing = true;
                }
                if (isDashing == true && player.altFunctionUse == 2 && player.controlUseItem == false)
                {
                    player.itemLocation = player.MountedCenter;
                    player.itemRotation += rot * 2;
                    player.velocity.X = (player.DirectionTo(target.Center) * 30).X;
                    player.velocity.Y = (player.DirectionTo(target.Center) * 30).Y;
                    player.immune = true;
                    player.immuneTime = 10;
                    player.itemAnimation = 1;
                    player.itemTime = 1;
                    item.UseSound = null;
                    dashCoolDown++;
                    for (float d = 0; d < 41; d++)//dust
                    {
                        Vector2 circle = new Vector2(player.MountedCenter.X, player.MountedCenter.Y - 35).RotatedBy((MathHelper.TwoPi / 40) * d, player.MountedCenter);
                        Dust dust = Dust.NewDustPerfect(circle, 107, Vector2.Zero, default, green, 0.75f);
                        dust.color = green;
                        dust.noLight = true;
                        dust.noGravity = true;
                    }
                    Vector2 position = new Vector2(player.MountedCenter.X - 50, player.MountedCenter.Y - 50);
                }
                else if (isDashing == false)
                {
                    item.UseSound = SoundID.Item1;
                    item.useTime = 10;
                }
            }
            #endregion
        }

        public override void UpdateInventory(Player player)
        {
            bool dir = player.direction > 0;

            #region combo

            switch (comboCount)
            {
                case maxCombo:
                    player.allDamageMult = 1.25f; //25% more dmg
                    break;
                default:
                    break;
            }
            #endregion
            #region cooldowns/timers/limits/resets
            comboTimer--;
            if (comboTimer < 0)
            {
                comboCount = 0;
                comboTimer = 0;
            }
            if (comboCount > maxCombo)
            {
                comboCount = maxCombo;
            }

            dashCoolDown--;
            if (dashCoolDown < 0)
                dashCoolDown = 0;
            #endregion
        }
        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            Vector2 position = new Vector2(player.MountedCenter.X - 50, player.MountedCenter.Y - 50);
            if (isDashing == true)
                hitbox = new Rectangle((int)position.X, (int)position.Y, 100, 100);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (isDashing == true)
            {
                crit = true;
                player.immuneTime = 50;
                player.velocity = -player.DirectionTo(target.Center) * 14;
                Main.PlaySound(SoundID.Item71, player.MountedCenter);
                isDashing = false;
                for (int h = 0; h < 61; h++)
                {
                    float max = Math.Max(target.width, target.height) / 12;
                    Vector2 moon = new Vector2(max, max).RotatedByRandom((MathHelper.TwoPi / 60) * h);
                    Dust dust = Dust.NewDustPerfect(target.Center, 107, moon, default, green, 0.75f);
                    dust.color = green;
                    dust.noLight = true;
                }
            }
            
            dashCoolDown = 60; //1second

            comboTimer = 180;
            comboCount++;
        }
    }
}