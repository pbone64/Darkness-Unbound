using DarknessUnbound.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace DarknessUnbound.Skies
{
    public class EthosP2Sky : CustomSky
    {
		public static Texture2D gradient;
		private bool _isActive;

		static EthosP2Sky() => gradient = ModContent.GetTexture("DarknessUnbound/Skies/EthosGradient");

		public override void OnLoad() { }

		public override void Update(GameTime gameTime) { }

		private float GetIntensity() => 1f - Utils.SmoothStep(3000f, 6000f, 200f);

		public override Color OnTileColor(Color inColor) => new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), inColor.ToVector4(), 1f - GetIntensity()));

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 0f && minDepth < 0f)
				spriteBatch.Draw(gradient, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new AnimatedColor(Color.Green, Color.Aqua).GetColor() * GetIntensity());
		}

		public override float GetCloudAlpha() => 0f;

		public override void Activate(Vector2 position, params object[] args) => _isActive = true;

		public override void Deactivate(params object[] args) => _isActive = false;

		public override void Reset() => _isActive = false;

		public override bool IsActive() =>_isActive;
	}
}
