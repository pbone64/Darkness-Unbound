using DarknessUnbound.Projectiles.Bosses.EthosOfTerraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
        public const bool FUNNY_BOSS = false;

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
        private bool superAttacks = false;
        private float ringRotation = 0;
        private int oldDialogueQuotient = 0;
        private int dialogueQuotient = 0;
        AttackProfile attack;
        private int nextCopypasta = 0;
        private int saidCopypasta = 0;
        private int copypastaCounter = 0;
        private int saidHpPercentages = 0;

        public float DialogueTimer { get => npc.ai[0]; set => npc.ai[0] = value; }
        public float DialogueState { get => npc.ai[1]; set => npc.ai[1] = value; }
        public float AttackTimer { get => npc.ai[2]; set => npc.ai[2] = value; }
        public float AttackCounter { get => npc.ai[3]; set => npc.ai[3] = value; }

        public float InAttackTimer1 { get => npc.localAI[0]; set => npc.localAI[0] = value; }
        public float InAttackTimer2 { get => npc.localAI[1]; set => npc.localAI[1] = value; }
        public float AttackFinished { get => npc.localAI[2]; set => npc.localAI[2] = value; }
        public float PassiveDialogueTimer { get => npc.localAI[3]; set => npc.localAI[3] = value; }

        private float PassiveDialogueState;
        private int oldPassiveDialogueQuotient = 0;
        private int passiveDialogueQuotient = 0;

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
                            npc.dontTakeDamage = false;
                            npc.immortal = false;
                            break;
                    }
                }
            }
            else
                PassiveDialogue();
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

            //Main.NewText("RANDOM BULLETS");

            if (InAttackTimer1 >= 90)
            {
                AttackFinished = 1f;
            }
        }

        #region DIALOGUE
        private void PassiveDialogue()
        {
            PassiveDialogueTimer++;
            oldPassiveDialogueQuotient = passiveDialogueQuotient;
            passiveDialogueQuotient = (int)(PassiveDialogueTimer / 95);

            if (oldPassiveDialogueQuotient != passiveDialogueQuotient)
            {
                switch (passiveDialogueQuotient)
                {
                    case 1:
                        switch (PassiveDialogueState)
                        {
                            case 0:
                                chat("It was a beautiful day outside, until you came along...");
                                break;
                            case 1:
                                chat("Everything points towards massive anomalies in the spacetime continuum. Worlds starting, worlds ending...");
                                break;
                            case 2:
                                chat("To be honest, it just makes it harder for me to give it my all...");
                                break;
                            case 3:
                                chat("When you first appeared here, I imagined us fighting by eachother's side...");
                                break;
                            case 4:
                                chat("After seeing you butcher them whole, I can't help but get this feeling...");
                                break;
                            case 5:
                                chat("A message to the master of this world, if I should die before this blongus does...");
                                break;
                            case 6:
                                chat("I can try my best...");
                                break;
                            case 7:
                                chat("Auegh..! Now you've done it!");
                                superAttacks = true;
                                break;
                            case 8:
                                chat("CONVULSE ON THE FLOOR UNTIL THE SIRENS GO OFF", true);
                                goto end;
                            case 9:
                                chat("You think you're invincable...", sad: true);
                                break;
                            case 10:
                                chat("You were known as a threat the moment you killed the eye...");
                                break;
                            case 11:
                                chat("There's no way...!");
                                break;
                            case 12:
                                
                                break;
                        }
                        break;
                    // END OF CASE

                    case 2:
                        switch (PassiveDialogueState)
                        {
                            case 0:
                                chat("You've already been beaten down by those you've killed...");
                                break;
                            case 1:
                                chat("Every time you die, timelines jump...");
                                break;
                            case 2:
                                chat("Knowing that no matter what, you'll always win...");
                                break;
                            case 3:
                                chat("Fending off the corruption, protecting the hallowed...");
                                break;
                            case 4:
                                chat("I can't afford to let you live any longer", sad: true);
                                goto end;
                            case 5:
                                chat("They will dimolish whatever we make. They dare stand in your way", sad: true);
                                goto end;
                            case 6:
                                chat("But if I die before I continue...");
                                break;
                            case 7:
                                chat("YOU'VE SCALED IN POWER TOO MUCH! NO MORE HOLDING BACK", true);
                                goto end;
                            case 9:
                                chat("But I can see through your hollow lies.", sad: true);
                                goto end;
                            case 10:
                                chat("Your wrath, but a sliver compared to mine.....");
                                break;
                            case 11:
                                chat("Should you win... my final hour willl tick", sad: true);
                                goto end;
                        }
                        break;
                    // END OF CASE

                    case 3:
                        switch (PassiveDialogueState)
                        {
                            case 0:
                                chat("I'M JUST FINISHING THE JOB", true);
                                goto end;
                            case 1:
                                chat("It's almost like...");
                                break;
                            case 2:
                                chat("No matter who should win, no matter who's in the right...");
                                break;
                            case 3:
                                chat("But no.", sad: true);
                                break;
                            case 6:
                                chat("Well, let's just say...");
                                break;
                            case 10:
                                chat("I have been sent to dimolish you upon the Master's demand...", true);
                                goto end;
                        }
                        break;
                    // END OF CASE

                    case 4:
                        switch (PassiveDialogueState)
                        {
                            case 1:
                                chat("YOU'RE A DIRTY CHEATER", true);
                                goto end;
                            case 2:
                                chat("Heh, hell if I know if that's just an excuse to be lazy", sad: true);
                                goto end;
                            case 3:
                                chat("You'll keep coming back, attempting to end my life.", sad: true);
                                goto end;
                            case 6:
                                chat("Victory will stir in their favour");
                                goto end;
                        }
                        break;
                    // END OF CASE

                    end:
                        PassiveDialogueState++;
                        PassiveDialogueTimer = 0;
                        break;
                }
            }

            if (FUNNY_BOSS) checkCopypasta();
            hpPercentageDialogue();
        }

        private void checkCopypasta()
        {
            nextCopypasta = 0;

            if (npc.life < npc.lifeMax / 3f * 2)
            {
                nextCopypasta++;
            }

            if (npc.life < npc.lifeMax / 2f)
            {
                nextCopypasta++;
            }

            if (npc.life < npc.lifeMax / 3f)
            {
                nextCopypasta++;
            }

            if (saidCopypasta != nextCopypasta)
            {
                List<string> cp = new List<string>();
                switch (nextCopypasta)
                {
                    case 1:
                        cp = CopypastaManager.Potato_One().ToList();
                        break;
                    case 2:
                        cp = CopypastaManager.Potato_Two().ToList();
                        break;
                    case 3:
                        cp = CopypastaManager.MUTATED_FROM_FARGO_SOULS().ToList();
                        break;
                }

                copypasta(cp.ToArray(), nextCopypasta);
            }
        }

        private void hpPercentageDialogue()
        {
            string text = "";

            if (npc.life < npc.lifeMax / 10f * 9f && saidHpPercentages < 1)
            {
                text = "Have you taken the right path?";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 8.75f && saidHpPercentages < 2)
            {
                text = "Are your morals mistaken?";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 8.5f && saidHpPercentages < 3)
            {
                text = "You, who could save us all...";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 8.35f && saidHpPercentages < 4)
            {
                text = "All I have sacrificed...";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 8.45f && saidHpPercentages < 5)
            {
                text = "Petty emoitions control your soul...";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 8f && saidHpPercentages < 6)
            {
                text = "Who shall survive your wrath?";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 7.75f && saidHpPercentages < 7)
            {
                text = "Auegh...! You've grown too strong!";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 7.25f && saidHpPercentages < 8)
            {
                text = "I can't hold out for must longer...";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 7f && saidHpPercentages < 9)
            {
                text = "Soon, It'll all be consumed...";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 6.75f && saidHpPercentages < 10)
            {
                text = "Give up! It'll make this less painful for everyone!";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 6.5f && saidHpPercentages < 11)
            {
                text = "Alright, now I'M mad!!!!";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 6f && saidHpPercentages < 12)
            {
                text = "Can we just stop this?!";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 5.75f && saidHpPercentages < 13)
            {
                text = "Niether of us will die";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 5.5f && saidHpPercentages < 14)
            {
                text = "Time will keepy... *huff* rewinding *huff*";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 5.25f && saidHpPercentages < 15)
            {
                text = "Eurgh..! We'll never die!";
                saidHpPercentages++;
            }

            if (npc.life <= npc.lifeMax / 10f * 5f && saidHpPercentages < 16)
            {
                text = "I guess I could just... become invincible...";
                npc.dontTakeDamage = true;
                npc.immortal = true;
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 5f && saidHpPercentages < 17 && Main.GameUpdateCount % 110 == 0)
            {
                text = "Then you'll never kill me";
                saidHpPercentages++;
            }

            if (npc.life < npc.lifeMax / 10f * 5f && saidHpPercentages < 18 && Main.GameUpdateCount % 110 == 0)
            {
                text = "Will you spare me..? Or finish the job?";
                saidHpPercentages++;
            }

            if (!string.IsNullOrEmpty(text))
            {
                chat(text);
                CombatText.NewText(npc.getRect(), Color.Lime, text, true);
            }
        }
        #endregion

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
            writer.Write(npc.localAI[3]);
            writer.Write(PassiveDialogueState);
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
            npc.localAI[3] = (float)reader.ReadDouble();
            PassiveDialogueState = (float)reader.ReadDouble();
        }

        private void chat(string text, bool dramatic = false, bool sad = false)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
                Main.NewText($"<Ethos of Terraria> {text}" + (dramatic ? "!!" : ""), (sad ? Color.DarkGray : Color.LimeGreen * (dramatic ? 1.85f : 0.95f)));
            else
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral($"<Ethos of Terraria> {text}"), (sad ? Color.DarkGray : Color.LimeGreen * (dramatic ? 1.85f : 0.95f)));
        }

        private void copypasta(string[] text, int expectedSaid)
        {
            if (expectedSaid == saidCopypasta) return;

            try
            {
                if (Main.GameUpdateCount % 10 == 0)
                {
                    CombatText.NewText(new Rectangle((int)npc.position.X + Main.rand.Next(-160, 161), (int)npc.position.Y + Main.rand.Next(-160, 161), 28, 24), Color.LimeGreen, text[copypastaCounter], true);
                    copypastaCounter++;
                }
            }
            catch (IndexOutOfRangeException e)
            {
                copypastaCounter = 0;
                saidCopypasta++;
            }
        }
    }
}
