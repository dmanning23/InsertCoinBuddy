using FontBuddyLib;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
	public abstract class BaseInsertCoinScreen : Screen
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
		protected PulsateBuddy InsertCoinFont { get; set; }

		/// <summary>
		/// thing for writing "NumCredits" text
		/// </summary>
		protected IFontBuddy NumCreditsFont { get; set; }

		protected float LineSpacing { get; set; }

		public string CoinSoundName { get; set; }

		public string PlayerJoinSoundName { get; set; }

		protected SoundEffect CoinSound { get; set; }

		protected SoundEffect PlayerJoinSound { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public BaseInsertCoinScreen(string insertCoinFont, string numCreditsFont, string coinSound, string playerJoinSound)
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

			if (!string.IsNullOrEmpty(CoinSoundName))
			{
				CoinSound = Content.Load<SoundEffect>(CoinSoundName);
			}

			if (!string.IsNullOrEmpty(PlayerJoinSoundName))
			{
				PlayerJoinSound = Content.Load<SoundEffect>(PlayerJoinSoundName);
			}

			LoadFonts();

			//Insert coin stuff is displayed right above that
			LineSpacing = InsertCoinFont.MeasureString("Insert Coin").Y;
		}

		protected abstract void LoadFonts();

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
						DrawMenuText();
					}
					break;

				case GameState.Ready:
					{
						DrawReadyText();
					}
					break;

				case GameState.Playing:
					{
						DrawPlayingText();
					}
					break;
			}

			ScreenManager.SpriteBatchEnd();
		}

		protected abstract void DrawMenuText();

		protected abstract void DrawReadyText();

		protected abstract void DrawPlayingText();

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

		#endregion //Methods
	}
}
