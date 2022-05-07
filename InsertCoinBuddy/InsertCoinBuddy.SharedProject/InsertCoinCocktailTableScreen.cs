using FontBuddyLib;
using MenuBuddy;
using Microsoft.Xna.Framework;
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
				NumCreditsFont = new FontBuddyPlus();
			}
			else
			{
				NumCreditsFont = new FontBuddy();
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
				var location = new Vector2(Resolution.TitleSafeArea.Left + (2f * LineSpacing), Resolution.ScreenArea.Center.Y);
				WritePlayerMenuText(player, location, true);
			}

			//draw the right player
			if (Service.Players.Count > 1)
			{
				var player = Service.Players[1];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.TitleSafeArea.Right - (2f * LineSpacing), Resolution.ScreenArea.Center.Y);
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
				var location = new Vector2(Resolution.TitleSafeArea.Left + (2f * LineSpacing), Resolution.ScreenArea.Center.Y);
				WritePlayerReadyText(player, location, true);
			}

			//draw the right player
			if (Service.Players.Count > 1)
			{
				var player = Service.Players[1];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.TitleSafeArea.Right - (2f * LineSpacing), Resolution.ScreenArea.Center.Y);
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
				var location = new Vector2(Resolution.TitleSafeArea.Left + (2f * LineSpacing), Resolution.ScreenArea.Center.Y);
				WritePlayerPlayingText(player, location, true);
			}

			//draw the right player
			if (Service.Players.Count > 1)
			{
				var player = Service.Players[1];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.TitleSafeArea.Right - (2f * LineSpacing), Resolution.ScreenArea.Center.Y);
				WritePlayerPlayingText(player, location, false);
			}
		}

		private void WritePlayerMenuText(PlayerCredits player, Vector2 location, bool p1)
		{
			if (p1)
			{
				InsertCoinFont.Rotation = MathHelper.ToRadians(90f);
				NumCreditsFont.Rotation = MathHelper.ToRadians(90f);
			}
			else 
			{
				InsertCoinFont.Rotation = MathHelper.ToRadians(270f);
				NumCreditsFont.Rotation = MathHelper.ToRadians(270f);
			}

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
			if (p1)
			{
				InsertCoinFont.Rotation = MathHelper.ToRadians(90f);
				NumCreditsFont.Rotation = MathHelper.ToRadians(90f);
			}
			else
			{
				InsertCoinFont.Rotation = MathHelper.ToRadians(270f);
				NumCreditsFont.Rotation = MathHelper.ToRadians(270f);
			}

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
			if (p1)
			{
				InsertCoinFont.Rotation = MathHelper.ToRadians(90f);
				NumCreditsFont.Rotation = MathHelper.ToRadians(90f);
			}
			else
			{
				InsertCoinFont.Rotation = MathHelper.ToRadians(270f);
				NumCreditsFont.Rotation = MathHelper.ToRadians(270f);
			}

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

		private Vector2 WriteNumCreditsText(PlayerCredits player, Vector2 location, bool p1)
		{
			//Draw the number of credits text!
			if (ShouldDisplayNumCredits(player))
			{
				var numCreditsText = NumCreditsText(player);
				var scale = 0.6f;
				if (p1)
				{
					location = new Vector2(location.X - LineSpacing, location.Y);
					location = LineFormatter.GetRotate90JustifiedPosition(numCreditsText, location, InsertCoinFont, Justify.Center, scale);
				}
				else
				{
					location = new Vector2(location.X + LineSpacing, location.Y);
					location = LineFormatter.GetRotate270JustifiedPosition(numCreditsText, location, InsertCoinFont, Justify.Center, scale);
				}

				//Number of credits is displayed at the bottom of the screen
				NumCreditsFont.Write(NumCreditsText(player),
					location,
					Justify.Left,
					0.6f, //write normal
					Color.White,
					ScreenManager.SpriteBatch,
					Time);
			}

			return location;
		}

		private void WriteInsertCoinText(Vector2 location, float pulsateSpeed, float size, string text, bool p1)
		{
			//Update with the correct rotation
			if (p1)
			{
				location = LineFormatter.GetRotate90JustifiedPosition(text, location, InsertCoinFont, Justify.Center, size);
			}
			else
			{
				location = LineFormatter.GetRotate270JustifiedPosition(text, location, InsertCoinFont, Justify.Center, size);
			}

			//get the location to draw this player's text
			//Draw in big pulsating letters
			InsertCoinFont.PulsateSpeed = pulsateSpeed; //pulsate faster
			InsertCoinFont.Write(text,
				location,
				Justify.Left,
				size, //write bigger
				Color.White,
				ScreenManager.SpriteBatch,
				Time);
		}

		#endregion //Methods
	}
}
