using FontBuddyLib;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Diagnostics;

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
	public class InsertCoinScreen : Screen
	{
		#region Fields

		/// <summary>
		/// name of the font resource to use to write "Insert Coin"
		/// </summary>
		private string _insertCoinFontName;

		/// <summary>
		/// name of the font resource to use to write "Credits: x/X"
		/// </summary>
		private string _numCreditsFontName;

		/// <summary>
		/// The thing that actually manages the number of credits for this screen.
		/// </summary>
		private IInsertCoinComponent _creditsManager;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// location of the "Insert Coin" text
		/// </summary>
		public Vector2 InsertCoinTextLocation { get; set; }

		/// <summary>
		/// thing for writing "Insert Coin" text
		/// </summary>
		public PulsateBuddy InsertCoinFont { get; set; }

		/// <summary>
		/// thing for writing "NumCredits" text
		/// </summary>
		public FontBuddy NumCreditsFont { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public InsertCoinScreen(string insertCoinFont, string numCreditsFont, IInsertCoinComponent insertCoin)
		{
			if (null == insertCoin)
			{
				throw new ArgumentNullException("insertCoin");
			}

			_insertCoinFontName = insertCoinFont;
			_numCreditsFontName = numCreditsFont;
			_creditsManager = insertCoin;
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		public override void LoadContent()
		{
			//load teh fonts
			InsertCoinFont = new PulsateBuddy();
			InsertCoinFont.PulsateSize = 0.25f;
			InsertCoinFont.Font = ScreenManager.Game.Content.Load<SpriteFont>(_insertCoinFontName);

			NumCreditsFont = new FontBuddy();
			NumCreditsFont.Font = ScreenManager.Game.Content.Load<SpriteFont>(_numCreditsFontName);

			//initialize some default locations for text

			//Insert coin stuff is displayed right above that
			InsertCoinTextLocation = new Vector2(Resolution.TitleSafeArea.Center.X,
				Resolution.TitleSafeArea.Bottom - (2.0f * InsertCoinFont.Font.LineSpacing));
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			ScreenManager.SpriteBatchBegin();

			switch (_creditsManager.CurrentGameState)
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
			//Draw instructions for the players
			if ((2 <= _creditsManager.NumCredits) || _creditsManager.FreePlay)
			{
				DrawPressStart(6.0f, 1.2f, "Press 1P or 2P start!", gameTime);
			}
			else if (1 <= _creditsManager.NumCredits)
			{
				DrawPressStart(6.0f, 1.2f, "Press 1P Start!", gameTime);
			}
			else
			{
				//draw in slowly pulsating white letters
				DrawPressStart(3.0f, 1.0f, InsertCoinText(), gameTime);
			}

			//Draw the number of credits text!
			if (ShouldDisplayNumCredits())
			{
				//Number of credits is displayed at the bottom of the screen
				NumCreditsFont.Write(NumCreditsText(),
					new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Bottom - NumCreditsFont.Font.LineSpacing),
					Justify.Center,
					0.6f, //write normal
					Color.White,
					ScreenManager.SpriteBatch,
					Time);
			}
		}

		private void DrawReadyText(GameTime gameTime)
		{
			//Draw instructions for the players
			if (_creditsManager.IsReady(PlayerIndex.One))
			{
				DrawPressStart(6.0f, 1.2f, "1P Ready!", gameTime, PlayerIndex.One);
			}
			else
			{
				DrawPressStart(3.0f, 1.0f, "Waiting for 1P...", gameTime, PlayerIndex.One);
			}

			if (_creditsManager.IsReady(PlayerIndex.Two))
			{
				DrawPressStart(6.0f, 1.2f, "2P Ready!", gameTime, PlayerIndex.Two);
			}
			else
			{
				DrawPressStart(3.0f, 1.0f, "Waiting for 2P...", gameTime, PlayerIndex.Two);
			}

			//Draw the number of credits text!
			if (ShouldDisplayNumCredits())
			{
				//Number of credits is displayed at the bottom of the screen
				NumCreditsFont.Write(NumCreditsText(),
					new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Bottom - NumCreditsFont.Font.LineSpacing),
					Justify.Center,
					0.6f, //write normal
					Color.White,
					ScreenManager.SpriteBatch,
					Time);
			}
		}

		private void DrawPlayingText(GameTime gameTime)
		{
			//Game is in play, draw the text in the corners
			DrawGameInPlayText(gameTime);

			//Draw the number of credits text!
			if (ShouldDisplayNumCredits())
			{
				//number of credits is displayed at top of the screen
				NumCreditsFont.Write(NumCreditsText(),
					new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Top),
					Justify.Center,
					0.6f, //write normal
					Color.White,
					ScreenManager.SpriteBatch,
					Time);
			}
		}

		private void DrawGameInPlayText(GameTime gameTime)
		{
			//is p1 playing?
			if (!_creditsManager.IsPlaying(PlayerIndex.One))
			{
				//draw the text in the upper left
				DrawPlayerJoinText(true, gameTime);
			}

			//is p2 playing?
			if (!_creditsManager.IsPlaying(PlayerIndex.Two))
			{
				//draw the text in the upper right
				DrawPlayerJoinText(false, gameTime);
			}
		}

		private void DrawPlayerJoinText(bool p1, GameTime gameTime)
		{
			//prepare the string for display
			string text = "";
			if ((1 <= _creditsManager.NumCredits) || _creditsManager.FreePlay)
			{
				text = string.Format("Press {0} start!", (p1 ? "1P" : "2P"));
			}
			else
			{
				text = InsertCoinText();
			}

			//prepare the justification for display
			Justify justify = (p1 ? Justify.Left : Justify.Right);

			//prepare the location
			Vector2 location = (p1 ? 
				new Vector2(Resolution.TitleSafeArea.Left, Resolution.TitleSafeArea.Top) :
				new Vector2(Resolution.TitleSafeArea.Right, Resolution.TitleSafeArea.Top));

			//write the text
			DrawPressStart(2.0f, 0.5f, text, gameTime, location, justify);
		}

		/// <summary>
		/// Check if the screen should write the number of credits available
		/// </summary>
		/// <returns><c>true</c>, if display number credits was shoulded, <c>false</c> otherwise.</returns>
		private bool ShouldDisplayNumCredits()
		{
			if (GameState.Playing != _creditsManager.CurrentGameState)
			{
				//always display the number of credits if the game is not in play
				return true;
			}
			else
			{
				//If the game is in play, only display credits if there are any coins in the system
				return (_creditsManager.FreePlay || (_creditsManager.TotalCoins >= 1));
			}
		}

		/// <summary>
		/// Get the text to write for inserting coins
		/// </summary>
		/// <returns>The coin text.</returns>
		private string InsertCoinText()
		{
			if (_creditsManager.NumCoinsNeededForNextCredit == 1)
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
		private string NumCreditsText()
		{
			//if it is free play mode, just say so
			if (_creditsManager.FreePlay)
			{
				return "Free Play";
			}

			if (0 == _creditsManager.NumCredits)
			{
				//Don't display 0 for number of credits, it will be confusing
				return string.Format("Credits: {0}/{1}",
					_creditsManager.NumCoins,
					_creditsManager.CoinsPerCredit);
			}
			else
			{
				//There are credits in the system!
				if (0 == _creditsManager.NumCoins)
				{
					//Don't display 0/x for num coins, it looks terrible
					return string.Format("Credits: {0}", _creditsManager.NumCredits);
				}
				else
				{
					return string.Format("Credits: {0} {1}/{2}",
						_creditsManager.NumCredits,
						_creditsManager.NumCoins,
						_creditsManager.CoinsPerCredit);
				}
			}
		}

		/// <summary>
		/// Draw the "press start" text
		/// </summary>
		/// <param name="strText">the text to write</param>
		private void DrawPressStart(float pulsateSpeed, float size, string strText, GameTime gameTime, PlayerIndex? player = null)
		{
			//get the location to draw the text
			var location = InsertCoinTextLocation;
			if (player.HasValue)
			{
				switch (player.Value)
				{
					case PlayerIndex.One:
						{
							location = new Vector2(InsertCoinTextLocation.X,
								InsertCoinTextLocation.Y - ((InsertCoinFont.Font.LineSpacing * size) * 2f));
						}
						break;
					case PlayerIndex.Two:
						{
							location = new Vector2(InsertCoinTextLocation.X,
								InsertCoinTextLocation.Y - (InsertCoinFont.Font.LineSpacing * size));
						}
						break;
				}
			}

			//Draw in big pulsating letters
			DrawPressStart(
				pulsateSpeed,
				size,
				strText,
				gameTime,
				location,
				Justify.Center);
		}

		private void DrawPressStart(float pulsateSpeed, float size, string strText, GameTime gameTime, Vector2 location, Justify eJustify)
		{
			//Draw in big pulsating letters
			InsertCoinFont.PulsateSpeed = pulsateSpeed; //pulsate faster
			InsertCoinFont.Write(strText,
				location,
				eJustify,
				size, //write bigger
				Color.White,
				ScreenManager.SpriteBatch,
				Time);
		}

		#endregion
	}
}
