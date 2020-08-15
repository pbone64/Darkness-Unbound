using DarknessUnbound.Projectiles.Bosses.EthosOfTerraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DarknessUnbound.NPCs.Bosses.EthosOfTerraria
{
    public class TerrariaEthos : ModNPC
    {
        public static Texture2D CultistRingTexture;
        public static Texture2D BlurTexture;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ethos of Terraria");
        }

        public override void SetDefaults()
        {
            npc.Size = new Vector2(76, 142);
            npc.lifeMax = 150000;
            npc.knockBackResist = 0f;
            npc.defense = 75;
            npc.damage = 0;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.timeLeft = 3600;
            drawOffsetY = -8;

            music = MusicID.LunarBoss;
        }

        static TerrariaEthos()
        {
            CultistRingTexture = ModContent.GetTexture("Terraria/Projectile_" + ProjectileID.CultistRitual);
            BlurTexture = ModContent.GetTexture("DarknessUnbound/NPCs/Bosses/EthosOfTerraria/TerrariaEthosBlur");
            AppDomain.CurrentDomain.ProcessExit += Deconstruct;
        }

        static void Deconstruct(object sender, EventArgs args)
        {
            CultistRingTexture.Dispose();
            BlurTexture.Dispose();
        }

        public const int PatternLength = 1;

        public AttackProfile Attack_Ring = default;
        public AttackProfile Attack_RandomBullets = default;

        private bool drawRing = true;
        private bool dialogue = true;
        private bool initialized = false;
        private float ringRotation = 0;
        private int oldDialogueQuotient = 0;
        private int dialogueQuotient = 0;
        AttackProfile attack;

        public float DialogueTimer { get => npc.ai[0]; set => npc.ai[0] = value; }
        public float DialogueState { get => npc.ai[1]; set => npc.ai[1] = value; }
        public float AttackTimer { get => npc.ai[2]; set => npc.ai[2] = value; }
        public float AttackCounter { get => npc.ai[3]; set => npc.ai[3] = value; }

        public float InAttackTimer1 { get => npc.localAI[0]; set => npc.localAI[0] = value; }
        public float InAttackTimer2 { get => npc.localAI[1]; set => npc.localAI[1] = value; }
        public float AttackFinished { get => npc.localAI[2]; set => npc.localAI[2] = value; }

        public override void AI()
        {
            drawOffsetY = -8 + ((float)Math.Sin(Main.GameUpdateCount / 25f) * 6f);

            if (npc.timeLeft < 10) npc.timeLeft = 10;

            if (!initialized)
            {
                Attack_Ring = new AttackProfile(30, () => AttackMethod_THERING());
                Attack_RandomBullets = new AttackProfile(45, () => AttackMethod_RandomBullets());

                initialized = true;
            }

            if (drawRing) ringRotation += (float)Math.PI / 36f;

            if (dialogue)
            {
                npc.dontTakeDamage = true;
                npc.immortal = true;
                for (int i = 0; i < npc.buffType.Length; i++)
                {
                    npc.buffType[i] = 0;
                }

                DialogueTimer++;
                oldDialogueQuotient = dialogueQuotient;
                dialogueQuotient = (int)(DialogueTimer / 120f);

                if (oldDialogueQuotient != dialogueQuotient)
                {
                    switch (dialogueQuotient)
                    {
                        case 1:
                            switch (DialogueState)
                            {
                                case 0:
                                    chat(Main.LocalPlayer.name);
                                    break;
                            }
                            break;
                        // END OF CASE

                        case 2:
                            switch (DialogueState)
                            {
                                case 0:
                                    chat("I've been watching you throughout your journey");
                                    break;
                            }
                            break;
                        // END OF CASE

                        case 3:
                            switch (DialogueState)
                            {
                                case 0:
                                    chat("The bosses you've slaughtered... the lives you've ended...");
                                    break;
                            }
                            break;
                        // END OF CASE

                        case 4:
                            switch (DialogueState)
                            {
                                case 0:
                                    chat("IT ENDS NOW", true);
                                    Main.PlaySound(SoundID.Roar, npc.Center, 0);
                                    goto end;
                            }
                            break;
                        // END OF CASE

                        end:
                            DialogueState++;
                            DialogueTimer = 0;
                            dialogue = false;
                            break;
                    }
                }
            }
            else
            {
                npc.dontTakeDamage = false;
                npc.immortal = false;
            }
            // END OF DIALOGUE

            if (!dialogue)
            {
                AttackTimer++;

                switch (AttackCounter)
                {
                    case 0: attack = Attack_Ring; break;
                    case 1: attack = Attack_RandomBullets; break;

                }

                if (AttackTimer >= attack.CD)
                {
                    attack.Attack.Invoke();
                    if (AttackFinished == 1f)
                    {
                        AttackFinished = 0f;
                        AttackCounter++;
                        AttackTimer = 0f;
                        InAttackTimer1 = 0f;
                        InAttackTimer2 = 0f;

                        if (AttackCounter > PatternLength) AttackCounter = 0;
                    }
                }
            }
        }

        private void AttackMethod_THERING()
        {
            InAttackTimer1++;

            if (InAttackTimer1 % 2 == 0)
            {
                Projectile proj = Projectile.NewProjectileDirect(new Vector2(npc.Center.X, npc.Center.Y + 240f).RotatedBy(Math.PI / 12f * (InAttackTimer1 / 2), npc.Center), Vector2.Zero, ModContent.ProjectileType<EthosLeaf>(), 45, 5f, ai0: 1f);
                proj.timeLeft = 240;
                proj.ai[0] = 1f;
                proj.ai[1] = (Vector2.One.RotatedBy(Math.PI / 12f * (InAttackTimer1 / 2)) * 4f).ToRotation();
            }

            if (InAttackTimer1 >= 48)
            {
                AttackFinished = 1f;
            }
        }

        private void AttackMethod_RandomBullets()
        {
            InAttackTimer1++;

            Main.NewText("RANDOM BULLETS");

            if (InAttackTimer1 >= 90)
            {
                AttackFinished = 1f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            spriteBatch.Draw(BlurTexture, npc.Center - Main.screenPosition + new Vector2(0f, drawOffsetY + 4f), null, Color.White, 0f, BlurTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, default, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.Transform);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D ringTexture = CultistRingTexture;
            Vector2 origin = ringTexture.Size() * 0.5f;

            if (drawRing) spriteBatch.Draw(ringTexture, npc.Center - Main.screenPosition, null, Color.LimeGreen * 1.75f, ringRotation, origin, 1f, SpriteEffects.None, 0f);
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(drawRing);
            writer.Write(dialogue);
            writer.Write(initialized);
            writer.Write(ringRotation);
            writer.Write(npc.localAI[0]);
            writer.Write(npc.localAI[1]);
            writer.Write(npc.localAI[2]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            drawRing = reader.ReadBoolean();
            dialogue = reader.ReadBoolean();
            initialized = reader.ReadBoolean();
            ringRotation = (float)reader.ReadDouble();
            npc.localAI[0] = (float)reader.ReadDouble();
            npc.localAI[1] = (float)reader.ReadDouble();
            npc.localAI[2] = (float)reader.ReadDouble();
        }

        private void chat(string text, bool dramatic = false)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText($"<Ethos of Terraria> {text}" + (dramatic ? "!!" : ""), Color.LimeGreen * (dramatic ? 1.85f : 0.95f));
            else
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral($"<Ethos of Terraria> {text}"), Color.LimeGreen * (dramatic ? 1.85f : 0.95f));
        }
    }
}
