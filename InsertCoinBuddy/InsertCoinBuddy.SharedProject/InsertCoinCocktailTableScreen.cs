using FontBuddyLib;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ResolutionBuddy;
using System;
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
	public class InsertCoinCocktailTableScreen : Screen
	{
		#region Properties

		/// <summary>
		/// The thing that actually manages the number of credits for this screen.
		/// </summary>
		public IInsertCoinService Service;

		/// <summary>
		/// name of the font resource to use to write "Insert Coin"
		/// </summary>
		public string InsertCoinFontName { get; set; }

		/// <summary>
		/// name of the font resource to use to write "Credits: x/X"
		/// </summary>
		public string NumCreditsFontName { get; set; }

		/// <summary>
		/// thing for writing "Insert Coin" text
		/// </summary>
		private PulsateBuddy InsertCoinFont { get; set; }

		/// <summary>
		/// thing for writing "NumCredits" text
		/// </summary>
		private IFontBuddy NumCreditsFont { get; set; }

		private float LineSpacing { get; set; }

		public string CoinSoundName { get; set; }

		public string PlayerJoinSoundName { get; set; }

		private SoundEffect CoinSound { get; set; }

		private SoundEffect PlayerJoinSound { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public InsertCoinCocktailTableScreen(string insertCoinFont, string numCreditsFont, string coinSound, string playerJoinSound)
		{
			CoveredByOtherScreens = false;
			CoverOtherScreens = false;

			InsertCoinFontName = insertCoinFont;
			NumCreditsFontName = numCreditsFont;
			CoinSoundName = coinSound;
			PlayerJoinSoundName = playerJoinSound;
		}

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		public override async Task LoadContent()
		{
			await base.LoadContent();

			Service = ScreenManager.Game.Services.GetService<IInsertCoinService>();
			if (null == Service)
			{
				throw new ArgumentNullException("_service");
			}
			Service.OnCoinAdded += OnCoinDropped;
			Service.OnPlayerJoined += OnPlayerJoined;

			//load teh fonts
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

			//initialize some default locations for text

			//Insert coin stuff is displayed right above that
			LineSpacing = InsertCoinFont.MeasureString("Insert Coin").Y;

			if (!string.IsNullOrEmpty(CoinSoundName))
			{
				CoinSound = Content.Load<SoundEffect>(CoinSoundName);
			}

			if (!string.IsNullOrEmpty(PlayerJoinSoundName))
			{
				PlayerJoinSound = Content.Load<SoundEffect>(PlayerJoinSoundName);
			}
		}

		private void OnCoinDropped(object sender, CoinEventArgs e)
		{
			CoinSound?.Play();
		}

		private void OnPlayerJoined(object sender, CoinEventArgs e)
		{
			PlayerJoinSound?.Play();
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			ScreenManager.SpriteBatchBegin();

			switch (Service.CurrentGameState)
			{
				case GameState.Menu:
					{
						DrawMenuText(gameTime);
					}
					break;

				case GameState.Ready:
					{
						DrawReadyText(gameTime);
					}
					break;

				case GameState.Playing:
					{
						DrawPlayingText(gameTime);
					}
					break;
			}

			ScreenManager.SpriteBatchEnd();
		}

		private void DrawMenuText(GameTime gameTime)
		{
			//draw the left player
			if (Service.Players.Count > 0)
			{
				var player = Service.Players[0];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.TitleSafeArea.Left + (2f * LineSpacing), Resolution.ScreenArea.Center.Y);
				WritePlayerMenuText(player, location, gameTime, true);
			}

			//draw the right player
			if (Service.Players.Count > 1)
			{
			}
		}

		private void WritePlayerMenuText(PlayerCredits player, Vector2 location, GameTime gameTime, bool p1)
		{
			if (p1)
			{
				InsertCoinFont.Rotation = MathHelper.ToRadians(90f);
				NumCreditsFont.Rotation = MathHelper.ToRadians(90f);
			}

			//Draw instructions for the players
			if (player.CreditAvailable)
			{
				WriteInsertCoinText(player, location, 6.0f, 1.2f, "Press Start!", gameTime, p1);
			}
			else
			{
				//draw in slowly pulsating white letters
				WriteInsertCoinText(player, location, 3.0f, 1.0f, InsertCoinText(player), gameTime, p1);
			}

			//Draw the number of credits text!
			if (ShouldDisplayNumCredits(player))
			{
				//Number of credits is displayed at the bottom of the screen
				location = LineFormatter
				NumCreditsFont.Write(NumCreditsText(player),
					new Vector2(location.X, location.Y + LineSpacing),
					Justify.Left,
					0.6f, //write normal
					Color.White,
					ScreenManager.SpriteBatch,
					Time);
			}
		}

		private void DrawReadyText(GameTime gameTime)
		{
			//draw the left player
			if (Service.Players.Count > 0)
			{
				var player = Service.Players[0];

				//Get the location to draw the player's text
				var location = new Vector2(Resolution.TitleSafeArea.Left + (2f * LineSpacing));
				WritePlayerMenuText(player, location, gameTime, true);
			}

			//draw the right player
			if (Service.Players.Count > 1)
			{
			}
		}

		private void WritePlayerReadyText(PlayerCredits player, Vector2 location, GameTime gameTime, bool p1)
		{
			//Draw instructions for the players
			if (!player.Ready)
			{
				WritePlayerMenuText(player, location, gameTime);
			}
			else
			{
				WriteInsertCoinText(player, location, 6.0f, 1.2f, "Ready!", gameTime);

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

		private void DrawPlayingText(GameTime gameTime)
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
						WriteInsertCoinText(player, location, 2.0f, 0.5f, "Press Start!", gameTime);
						location = new Vector2(location.X, location.Y + LineSpacing);
					}
					else
					{
						WriteInsertCoinText(player, location, 2.0f, 0.5f, InsertCoinText(player), gameTime);
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

		/// <summary>
		/// Check if the screen should write the number of credits available
		/// </summary>
		/// <returns><c>true</c>, if display number credits was shoulded, <c>false</c> otherwise.</returns>
		public bool ShouldDisplayNumCredits(PlayerCredits player)
		{
			if (GameState.Playing != Service.CurrentGameState)
			{
				//always display the number of credits if the game is not in play
				return true;
			}
			else
			{
				//If the game is in play, only display credits if there are any coins in the system
				return (Service.FreePlay || (player.TotalCoins >= 1));
			}
		}

		/// <summary>
		/// Get the text to write for inserting coins
		/// </summary>
		/// <returns>The coin text.</returns>
		public string InsertCoinText(PlayerCredits player)
		{
			if (player.NumCoinsNeededForNextCredit == 1)
			{
				return "Insert Coin";
			}
			else
			{
				//need more than one coin, reutrn "coins" plural
				return "Insert Coins";
			}
		}

		/// <summary>
		/// Get the text to write to display the number of credits
		/// </summary>
		/// <returns>The credits text.</returns>
		public string NumCreditsText(PlayerCredits player)
		{
			//if it is free play mode, just say so
			if (Service.FreePlay)
			{
				return "Free Play";
			}

			if (0 == player.NumCredits)
			{
				//Don't display 0 for number of credits, it will be confusing
				return $"Credits: {player.NumCoins}/{Service.CoinsPerCredit}";
			}
			else
			{
				//There are credits in the system!
				if (0 == player.NumCoins)
				{
					//Don't display 0/x for num coins, it looks terrible
					return $"Credits: {player.NumCredits}";
				}
				else
				{
					return $"Credits: {player.NumCredits} {player.NumCoins}/{Service.CoinsPerCredit}";
				}
			}
		}

		private void WriteInsertCoinText(PlayerCredits player, Vector2 location, float pulsateSpeed, float size, string strText, GameTime gameTime)
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
