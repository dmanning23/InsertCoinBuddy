using FontBuddyLib;
using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

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
	public class InsertCoinScreen : BaseInsertCoinScreen
	{
		#region Propertie

		/// <summary>
		/// location of the "Insert Coin" text
		/// </summary>
		private float InsertCoinTextLocation { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public InsertCoinScreen(string insertCoinFont, string numCreditsFont, string coinSound, string playerJoinSound) : 
			base(insertCoinFont, numCreditsFont, coinSound, playerJoinSound)
		{
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			//Insert coin stuff is displayed right above that
			InsertCoinTextLocation = Resolution.TitleSafeArea.Bottom - (2.0f * LineSpacing);
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
			for (int i = 0; i < Service.Players.Count; i++)
			{
				var player = Service.Players[i];

				//Get the location to draw the player's text
				var location = new Vector2((i + 1) * (Resolution.ScreenArea.Width / (Service.Players.Count + 1)), InsertCoinTextLocation);
				WritePlayerMenuText(player, location);
			}
		}

		protected override void DrawReadyText()
		{
			for (int i = 0; i < Service.Players.Count; i++)
			{
				var player = Service.Players[i];

				//Get the location to draw the player's text
				var location = new Vector2((i + 1) * (Resolution.ScreenArea.Width / (Service.Players.Count + 1)), InsertCoinTextLocation);

				//Draw instructions for the players
				if (!player.Ready)
				{
					WritePlayerMenuText(player, location);
				}
				else
				{
					WriteInsertCoinText(location, 6.0f, 1.2f, "Ready!");

					//Draw the number of credits text!
					if (ShouldDisplayNumCredits(player))
					{
						//Number of credits is displayed at the bottom of the screen
						NumCreditsFont.Write(NumCreditsText(player),
							new Vector2(location.X, location.Y + LineSpacing),
							Justify.Center,
							0.6f, //write normal
							Color.White,
							ScreenManager.SpriteBatch,
							Time);
					}
				}
			}
		}

		protected override void DrawPlayingText()
		{
			for (int i = 0; i < Service.Players.Count; i++)
			{
				var player = Service.Players[i];

				//Get the location to draw the player's text
				var location = new Vector2((i + 1) * (Resolution.ScreenArea.Width / (Service.Players.Count + 1)), Resolution.TitleSafeArea.Top);

				//Only draw text for player's that aren't currently playing.
				if (!player.Current)
				{
					if (player.CreditAvailable)
					{
						WriteInsertCoinText(location, 2.0f, 0.5f, "Press Start!");
						location = new Vector2(location.X, location.Y + LineSpacing);
					}
					else
					{
						WriteInsertCoinText(location, 2.0f, 0.5f, InsertCoinText(player));
						location = new Vector2(location.X, location.Y + LineSpacing);
					}
				}

				//Write the player's number of credits if they have coins in the system
				if (ShouldDisplayNumCredits(player))
				{
					//number of credits is displayed at top of the screen
					NumCreditsFont.Write(NumCreditsText(player),
						location,
						Justify.Center,
						0.6f, //write normal
						Color.White,
						ScreenManager.SpriteBatch,
						Time);
				}
			}
		}

		private void WritePlayerMenuText(PlayerCredits player, Vector2 location)
		{
			//Draw instructions for the players
			if (player.CreditAvailable)
			{
				WriteInsertCoinText(location, 6.0f, 1.2f, "Press Start!");
			}
			else
			{
				//draw in slowly pulsating white letters
				WriteInsertCoinText(location, 3.0f, 1.0f, InsertCoinText(player));
			}

			//Draw the number of credits text!
			if (ShouldDisplayNumCredits(player))
			{
				//Number of credits is displayed at the bottom of the screen
				NumCreditsFont.Write(NumCreditsText(player),
					new Vector2(location.X, location.Y + LineSpacing),
					Justify.Center,
					0.6f, //write normal
					Color.White,
					ScreenManager.SpriteBatch,
					Time);
			}
		}

		private void WriteInsertCoinText(Vector2 location, float pulsateSpeed, float size, string strText)
		{
			//get the location to draw this player's text
			//Draw in big pulsating letters
			InsertCoinFont.PulsateSpeed = pulsateSpeed; //pulsate faster
			InsertCoinFont.Write(strText,
				location,
				Justify.Center,
				size, //write bigger
				Color.White,
				ScreenManager.SpriteBatch,
				Time);
		}

		#endregion //Methods
	}
}
