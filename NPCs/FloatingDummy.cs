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

        public override void AI()
        {
            Player player = Main.player[Main.myPlayer];
            if (npc.life < npc.lifeMax)
                npc.life += npc.lifeMax;

            float length = Vector2.Distance(Main.MouseWorld, npc.Center);

            if (length < 33 && player.altFunctionUse == 2)
                npc.active = false;

            if (!npc.active)
            {
                Main.PlaySound(SoundID.NPCHit16, npc.Center);
                for (int i = 0; i < 41; i++)
                {
                    Vector2 spin = new Vector2(0, 2).RotatedBy(((MathHelper.Pi * 2f) / 40) * i);
                    Dust dust = Dust.NewDustPerfect(npc.Center, 204, spin, 0, Color.White, 0.75f);
                }
            }
        }
        public override bool CheckActive() => false;
    }
}
