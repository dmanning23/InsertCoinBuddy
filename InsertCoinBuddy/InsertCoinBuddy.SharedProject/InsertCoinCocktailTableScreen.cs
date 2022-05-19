using FontBuddyLib;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;

namespace InsertCoinBuddy
{
	/// <summary>
	/// This screen displays credits status on top of all the other screens
	/// Displays "Insert Coin" if the game is not being played and there are no credits available
	/// Displays "Press Start!" if the game is not being played and there are 1+ credits available
	/// 
	/// At the bottom of the screen, displays the number of credits
	/// Credits: [NumCredits] [NumCoins]/[CoinsPerCredit]
	/// </summary>
	public class InsertCoinCocktailTableScreen : BaseInsertCoinScreen
	{
		#region Methods

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public InsertCoinCocktailTableScreen(string insertCoinFont, string numCreditsFont, string coinSound, string playerJoinSound) :
			base(insertCoinFont, numCreditsFont, coinSound, playerJoinSound)
		{
		}

		protected override void LoadFonts()
		{
			InsertCoinFont = new PulsateBuddy()
			{
				ShadowOffset = Vector2.Zero,
				ShadowSize = 1f,
			};
			InsertCoinFont.PulsateSize = 0.25f;
			InsertCoinFont.LoadContent(Content, InsertCoinFontName, StyleSheet.UseFontPlus, StyleSheet.SmallFontSize);

			if (StyleSheet.UseFontPlus)
			{
				NumCreditsFont = new FontBuddyPlusStroked();
			}
			else
			{
				NumCreditsFont = new OutlineTextBuddy();
			}
			NumCreditsFont.LoadContent(Content, NumCreditsFontName, StyleSheet.UseFontPlus, StyleSheet.SmallFontSize);
		}

		protected override void DrawMenuText()
		{
			//draw the left player
			if (Service.Players.Count > 0)
			{
				var player = Service.Players[0];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.ScreenArea.Center.X, Resolution.TitleSafeArea.Bottom - LineSpacing * 2f);
				WritePlayerMenuText(player, location, true);
			}

			//draw the right player
			if (Service.Players.Count > 1)
			{
				var player = Service.Players[1];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.ScreenArea.Center.X, Resolution.TitleSafeArea.Top + LineSpacing * 2f);
				WritePlayerMenuText(player, location, false);
			}
		}

		protected override void DrawReadyText()
		{
			//draw the left player
			if (Service.Players.Count > 0)
			{
				var player = Service.Players[0];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.ScreenArea.Center.X, Resolution.TitleSafeArea.Bottom - LineSpacing * 2f);
				WritePlayerReadyText(player, location, true);
			}

			//draw the right player
			if (Service.Players.Count > 1)
			{
				var player = Service.Players[1];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.ScreenArea.Center.X, Resolution.TitleSafeArea.Top + LineSpacing * 2f);
				WritePlayerReadyText(player, location, false);
			}
		}

		protected override void DrawPlayingText()
		{
			//draw the left player
			if (Service.Players.Count > 0)
			{
				var player = Service.Players[0];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.ScreenArea.Center.X, Resolution.TitleSafeArea.Bottom - LineSpacing * 2f);
				WritePlayerPlayingText(player, location, true);
			}

			//draw the right player
			if (Service.Players.Count > 1)
			{
				var player = Service.Players[1];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.ScreenArea.Center.X, Resolution.TitleSafeArea.Top + LineSpacing * 2f);
				WritePlayerPlayingText(player, location, false);
			}
		}

		private void WritePlayerMenuText(PlayerCredits player, Vector2 location, bool p1)
		{
			SetFont(p1);

			//Draw instructions for the players
			if (player.CreditAvailable)
			{
				WriteInsertCoinText(location, 6.0f, 1.2f, "Press Start!", p1);
			}
			else
			{
				//draw in slowly pulsating white letters
				WriteInsertCoinText(location, 3.0f, 1.0f, InsertCoinText(player), p1);
			}

			WriteNumCreditsText(player, location, p1);
		}

		private void WritePlayerReadyText(PlayerCredits player, Vector2 location, bool p1)
		{
			SetFont(p1);

			//Draw instructions for the players
			if (!player.Ready)
			{
				WritePlayerMenuText(player, location, p1);
			}
			else
			{
				WriteInsertCoinText(location, 6.0f, 1.2f, "Ready!", p1);
				WriteNumCreditsText(player, location, p1);
			}
		}

		private void WritePlayerPlayingText(PlayerCredits player, Vector2 location, bool p1)
		{
			SetFont(p1);

			//Only draw text for player's that aren't currently playing.
			if (!player.Current)
			{
				if (player.CreditAvailable)
				{
					WriteInsertCoinText(location, 2.0f, 0.5f, "Press Start!", false);
				}
				else
				{
					WriteInsertCoinText(location, 2.0f, 0.5f, InsertCoinText(player), false);
				}
			}

			WriteNumCreditsText(player, location, p1);
		}

		private void WriteNumCreditsText(PlayerCredits player, Vector2 location, bool p1)
		{
			//Draw the number of credits text!
			if (ShouldDisplayNumCredits(player))
			{
				var numCreditsText = NumCreditsText(player);
				var scale = 0.8f;
				if (p1)
				{
					location = new Vector2(location.X, location.Y + LineSpacing);
					
				}
				else
				{
					location = new Vector2(location.X, location.Y - LineSpacing);
					//location = LineFormatter.GetRotate270JustifiedPosition(numCreditsText, location, InsertCoinFont, Justify.Center, scale);
				}

				NumCreditsFont.Write(NumCreditsText(player),
						location,
						Justify.Center,
						scale, //write normal
						Color.White,
						ScreenManager.SpriteBatch,
						Time);
			}
		}

		private void WriteInsertCoinText(Vector2 location, float pulsateSpeed, float size, string text, bool p1)
		{
			//Draw in big pulsating letters
			InsertCoinFont.PulsateSpeed = pulsateSpeed; //pulsate faster

			InsertCoinFont.Write(text,
					location,
					Justify.Center,
					size, //write bigger
					Color.White,
					ScreenManager.SpriteBatch,
					Time);
		}

		private void SetFont(bool p1)
		{
			if (p1)
			{
				InsertCoinFont.SpriteEffects = SpriteEffects.None;
				NumCreditsFont.SpriteEffects = SpriteEffects.None;
			}
			else
			{
				InsertCoinFont.SpriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
				NumCreditsFont.SpriteEffects = SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally;
			}
		}

		#endregion //Methods
	}
}
