using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DarknessUnbound
{
    public class DUWorld : ModWorld
    {
        public static bool restlessShadows;

        public override void Initialize()
        {
            restlessShadows = false;
            //DarknessUnbound.SET_UNDERTABLE_DIALOGUE("Hello, world! I'm blue and you're a large blongus! This line is here to test the line breaking", ModContent.GetTexture("DarknessUnbound/NPCs/Bosses/EthosOfTerraria/TerrariaEthos"));
        }

        public override TagCompound Save() => new TagCompound() {
            { "RestlessShadows", restlessShadows }
        };

        public override void Load(TagCompound tag)
        {
            restlessShadows = tag.GetBool("RestlessShadows");
        }
    }
}
