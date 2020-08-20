using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace DarknessUnbound.Items
{
    public abstract class DarknessItem : ModItem
    {
        /// <summary>
        /// Return false if you want to stop the auto sizing based on texture
        /// </summary>
        public virtual bool AutoSize() => true;

        /// <summary>
        /// Identical to SetDefaults except that the item's size is already set
        /// </summary>
        public virtual void SafeSetDefaults() { }

        public sealed override void SetDefaults()
        {
            if (AutoSize()) autoSize();
            SafeSetDefaults();
        }

        private void autoSize()
        {
            try {
                item.Size = Main.itemTexture[item.type].Size();
            }
            catch (NullReferenceException e) {
                item.Size = Vector2.Zero;
                return;
            }
        }
    }
}
