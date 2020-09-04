using DarknessUnbound.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound
{
    public partial class DarknessUnbound : Mod
    {
        private void Load_MethodSwap()
        {
            //IL.Terraria.Main.DoDraw += DoDraw_ILEdit;
            On.Terraria.GameContent.Events.CultistRitual.TrySpawning += CultistRitual_TrySpawning;
        }

        private void CultistRitual_TrySpawning(On.Terraria.GameContent.Events.CultistRitual.orig_TrySpawning orig, int x, int y)
        {
            if (!NPC.downedHalloweenKing || !NPC.downedHalloweenTree || !NPC.downedChristmasIceQueen || !NPC.downedChristmasSantank || !NPC.downedChristmasTree)
                return;

            orig(x, y);
        }

        public void DoDraw_ILEdit(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (!c.TryGotoNext((Instruction i) => i.MatchLdfld<List<Int32>>("DrawCacheNPCsMoonMoon")))
            {
                c.Index--;

                c.EmitDelegate<EmptyDelegate>(() =>
                {
#pragma warning disable CS0618 // Type or member is obsolete
                    Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.instance.Rasterizer, null, Main.Transform);
#pragma warning restore CS0618 // Type or member is obsolete
                    Main.spriteBatch.Draw(Main.blackTileTexture, new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f), null, Color.White, 0f, Vector2.One * 8f, 1f, SpriteEffects.None, 0f);
                    Main.spriteBatch.End();
                });
            }
        }

        private delegate void EmptyDelegate();
    }
}
