using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using DarknessUnbound.NPCs.Bosses.EthosOfTerraria;

namespace DarknessUnbound.Projectiles.Bosses.EthosOfTerraria
{
    public class EthosLeaf : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 0;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
        }

        public override Color? GetAlpha(Color lightColor) => Color.White;
        bool one_reachedEnd = false;

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation();

            projectile.frameCounter++;
            if (projectile.frameCounter % 8 == 0) projectile.frame++;
            if (projectile.frame == 5) projectile.frame = 0;

            projectile.alpha -= 5;

            if (projectile.ai[0] == 1f)
            {
                if (projectile.extraUpdates == 1) projectile.extraUpdates++;
                projectile.rotation = projectile.ai[1] + MathHelper.PiOver2;
                projectile.localAI[0]--;
                float sin = (float)Math.Sin(projectile.localAI[0] / 40f);
                if (!one_reachedEnd) projectile.velocity = new Vector2(sin * 10f, 0).RotatedBy(projectile.rotation);
                if (projectile.localAI[0] == -40) one_reachedEnd = true;
                if (one_reachedEnd) projectile.velocity = new Vector2(6f).RotatedBy(projectile.rotation);
            }

            projectile.localAI[1]--;
            if (!projectile.InArena() && projectile.localAI[1] <= 0) projectile.Kill();
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(projectile.localAI[0]);
            writer.Write(projectile.localAI[1]);
            writer.Write(one_reachedEnd);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            projectile.localAI[0] = (float)reader.ReadDouble();
            projectile.localAI[1] = (float)reader.ReadDouble();
            one_reachedEnd = reader.ReadBoolean();
        }
    }
}
