using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DarknessUnbound.Skies
{
	public class PillarSky : CustomSky
	{
		private struct Star
		{
			public Vector2 Position;
			public float Depth;
			public int TextureIndex;
			public float SinOffset;
			public float AlphaFrequency;
			public float AlphaAmplitude;
		}

		private UnifiedRandom _random = new UnifiedRandom();
		private Texture2D _planetTexture;
		private Texture2D _bgTexture;
		//private Texture2D[] _starTextures;
		private bool _isActive;
		private Star[] _stars;
		private float _fadeOpacity;

		public override void OnLoad()
		{
			_planetTexture = ModContent.GetTexture("DarknessUnbound/Skies/PillarPlanet");
			_bgTexture = ModContent.GetTexture("DarknessUnbound/Skies/PillarGradient");
		}

		public override void Update(GameTime gameTime)
		{
			if (_isActive)
				_fadeOpacity = Math.Min(1f, 0.01f + _fadeOpacity);
			else
				_fadeOpacity = Math.Max(0f, _fadeOpacity - 0.01f);
		}

		public override Color OnTileColor(Color inColor) => new Color(Vector4.Lerp(inColor.ToVector4(), Vector4.One, _fadeOpacity * 0.5f));

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 3.4028235E+38f && minDepth < 3.4028235E+38f)
			{
				spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * _fadeOpacity);
				spriteBatch.Draw(_bgTexture, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - 2400.0) * 0.10000000149011612)), Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * _fadeOpacity));
				Vector2 value = new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
				Vector2 value2 = 0.01f * (new Vector2((float)Main.maxTilesX * 8f, (float)Main.worldSurface / 2f) - Main.screenPosition);
				spriteBatch.Draw(_planetTexture, value + new Vector2(-200f, -200f) + value2, null, Color.White * 0.9f * _fadeOpacity, 0f, new Vector2(_planetTexture.Width >> 1, _planetTexture.Height >> 1), 0.95f, SpriteEffects.None, 1f);
			}

			/*int num = -1;
			int num2 = 0;
			for (int i = 0; i < _stars.Length; i++)
			{
				float depth = _stars[i].Depth;
				if (num == -1 && depth < maxDepth)
					num = i;

				if (depth <= minDepth)
					break;

				num2 = i;
			}

			if (num == -1)
				return;

			float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
			Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
			Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);*/
			/*for (int j = num; j < num2; j++)
			{
				Vector2 value4 = new Vector2(1f / _stars[j].Depth, 1.1f / _stars[j].Depth);
				Vector2 position = (_stars[j].Position - value3) * value4 + value3 - Main.screenPosition;
				if (rectangle.Contains((int)position.X, (int)position.Y))
				{
					float value5 = (float)Math.Sin(_stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly + _stars[j].SinOffset) * _stars[j].AlphaAmplitude + _stars[j].AlphaAmplitude;
					float num3 = (float)Math.Sin(_stars[j].AlphaFrequency * Main.GlobalTimeWrappedHourly * 5f + _stars[j].SinOffset) * 0.1f - 0.1f;
					value5 = MathHelper.Clamp(value5, 0f, 1f);
					Texture2D value6 = _starTextures[_stars[j].TextureIndex].Value;
					spriteBatch.Draw(value6, position, null, Color.White * scale * value5 * 0.8f * (1f - num3) * _fadeOpacity, 0f, new Vector2(value6.Width >> 1, value6.Height >> 1), (value4.X * 0.5f + 0.5f) * (value5 * 0.3f + 0.7f), SpriteEffects.None, 0f);
				}
			}*/
		}

		public override float GetCloudAlpha() => 1 - _fadeOpacity;

		public override void Activate(Vector2 position, params object[] args)
		{
			_fadeOpacity = 0.002f;
			_isActive = true;
			int num = 200;
			int num2 = 10;
			//_stars = new Star[num * num2];
			int num3 = 0;
			/*for (int i = 0; i < num; i++)
			{
				float num4 = (float)i / (float)num;
				for (int j = 0; j < num2; j++)
				{
					float num5 = (float)j / (float)num2;
					_stars[num3].Position.X = num4 * (float)Main.maxTilesX * 16f;
					_stars[num3].Position.Y = num5 * ((float)Main.worldSurface * 16f + 2000f) - 1000f;
					_stars[num3].Depth = _random.NextFloat() * 8f + 1.5f;
					_stars[num3].TextureIndex = _random.Next(_starTextures.Length);
					_stars[num3].SinOffset = _random.NextFloat() * 6.28f;
					_stars[num3].AlphaAmplitude = _random.NextFloat() * 5f;
					_stars[num3].AlphaFrequency = _random.NextFloat() + 1f;
					num3++;
				}
			}

			Array.Sort(_stars, SortMethod);*/
		}

		private int SortMethod(Star meteor1, Star meteor2) => meteor2.Depth.CompareTo(meteor1.Depth);

		public override void Deactivate(params object[] args)
		{
			_isActive = false;
		}

		public override void Reset()
		{
			_isActive = false;
		}

		public override bool IsActive()
		{
			if (!_isActive)
				return _fadeOpacity > 0.001f;

			return true;
		}
	}
}
