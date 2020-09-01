using DarknessUnbound.Helpers;
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
        }
        private float spin = 0;
        public override void AI()
        {
            spin += MathHelper.ToRadians(1);
            if (spin > MathHelper.ToRadians(360))
                spin = 0;
            Player player = Main.player[Main.myPlayer];
            if (npc.life < npc.lifeMax)
                npc.life += npc.lifeMax;

            float length = Vector2.Distance(Main.MouseWorld, npc.Center);

            if (length < 25 && player.altFunctionUse == 2)
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
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Rectangle rec = new Rectangle(0, 0, 1920, 1920);
            Texture2D massiveDong = ModContent.GetTexture("DarknessUnbound/Effects/Galaxy");
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            spriteBatch.Draw(massiveDong, npc.Center - Main.screenPosition, new Rectangle?(rec), new AnimatedColor(Color.DarkBlue, Color.DarkMagenta).GetColor(), spin, new Vector2(960, 960), 1f, SpriteEffects.None, 1f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            spriteBatch.Draw(massiveDong, npc.Center - Main.screenPosition, new Rectangle?(rec), new AnimatedColor(Color.LightCyan, Color.Purple).GetColor(), spin, new Vector2(960, 960), 1f, SpriteEffects.None, 1f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            return true;
        }
    }
}
