using DarknessUnbound.Helpers;
using DarknessUnbound.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DarknessUnbound.NPCs
{
    public class FloatingDummy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Puppet");
            NPCID.Sets.ExcludedFromDeathTally[npc.type] = true;
            NPCID.Sets.TeleportationImmune[npc.type] = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => false;

        public override void SetDefaults()
        {
            Player player = Main.player[Main.myPlayer];
            npc.width = 42;
            npc.height = 38;
            npc.aiStyle = 0;
            npc.lifeMax = 100000;
            npc.HitSound = SoundID.NPCHit15;
            npc.spriteDirection = -player.direction;
            npc.defense = 1;
            npc.damage = 0;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.chaseable = true;
            npc.lifeRegen = 90000;
            npc.timeLeft = int.MaxValue;
        }
        private float Spin { get => npc.ai[0]; set => npc.ai[0] = value; }
        private float Spin2 { get => npc.ai[1]; set => npc.ai[1] = value; }
        public override void AI()
        {
            Player player = Main.LocalPlayer;

            Spin += MathHelper.ToRadians(0.15f);
            if (Spin > MathHelper.ToRadians(360))
                Spin = 0;
            Spin2 += MathHelper.ToRadians(0.325f);
            if (Spin2 > MathHelper.ToRadians(360))
                Spin2 = 0;

            if (npc.timeLeft < 10)
                npc.timeLeft = int.MaxValue;

            float length = Vector2.Distance(Main.MouseWorld, npc.Center);
            if (length < 25 && player.altFunctionUse == 2 && player.HeldItem.type == ModContent.ItemType<FloatingDummyItem>())
                npc.active = false;

            if (!npc.active)
            {
                Main.PlaySound(SoundID.NPCHit16, npc.Center);
                for (int i = 0; i < 41; i++)
                {
                    Vector2 spin = new Vector2(0, 3).RotatedBy((MathHelper.TwoPi / 40) * i);
                    Dust dust = Dust.NewDustPerfect(npc.Center, 204, spin, 0, Color.Yellow, 1f);
                    dust.color = default;
                }
            }
        }

        public override bool CheckDead()
        {
            npc.life = npc.lifeMax;
            return false;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Player player = Main.player[Main.myPlayer];
            bool hover = npc.getRect().Contains((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y);
            Texture2D outline = ModContent.GetTexture("DarknessUnbound/Effects/PuppetGlow");
            Vector2 pos1 = npc.position - Main.screenPosition;
            Vector2 posFinal = new Vector2(pos1.X, pos1.Y + 4);
            Rectangle rec = new Rectangle(0, 0, npc.width, npc.height);
            SpriteEffects flipper = npc.direction > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            if (npc.type == ModContent.NPCType<FloatingDummy>() && hover == true)
            {
                spriteBatch.Draw(outline, posFinal, new Rectangle?(rec), Color.Yellow, npc.rotation, Vector2.Zero, 1f, flipper, 1f);
            }

            /*Rectangle rec2 = new Rectangle(0, 0, 1920, 1920);
            Texture2D baseTex = ModContent.GetTexture("DarknessUnbound/Effects/Galaxy");
            Texture2D altTex = ModContent.GetTexture("DarknessUnbound/Effects/Galaxy2");
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            spriteBatch.Draw(baseTex, npc.Center - Main.screenPosition, new Rectangle?(rec2), Color.BlueViolet * 0.1f, -Spin, new Vector2(960, 960), 1f, SpriteEffects.FlipHorizontally, 1f);
            spriteBatch.Draw(baseTex, npc.Center - Main.screenPosition, new Rectangle?(rec2), Color.CornflowerBlue, Spin2, new Vector2(960, 960), 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(altTex, npc.Center - Main.screenPosition, new Rectangle?(rec2), Color.LightCyan * 0.2f, Spin2, new Vector2(960, 960), 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(altTex, npc.Center - Main.screenPosition, new Rectangle?(rec2), Color.LightSteelBlue, Spin, new Vector2(960, 960), 1f, SpriteEffects.None, 1f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            */
        }
    }
}
