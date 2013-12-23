using FontBuddyLib;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
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
	public class InsertCoinScreen : GameScreen
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
		private CreditsManager _creditsManager;

		/// <summary>
		/// how to justify all the text for this screen
		/// </summary>
		public Justify TextJustification { get; set; }

		#endregion //Fields

		#region Properties

		/// <summary>
		/// location of the "Insert Coin" text
		/// </summary>
		public Vector2 InsertCoinTextLocation { get; set; }

		/// <summary>
		/// location of the "Credits: x/X" text
		/// </summary>
		/// <value>The number credits text location.</value>
		public Vector2 NumCreditsTextLocation { get; set; }

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
		public InsertCoinScreen(string strInsertCoinFont, string strNumCreditsFont, CreditsManager manager)
		{
			InsertCoinTextLocation = Vector2.Zero;
			NumCreditsTextLocation = Vector2.Zero;
			InsertCoinFont = new PulsateBuddy();
			InsertCoinFont.PulsateSize = 0.25f;
			NumCreditsFont = new FontBuddy();
			_insertCoinFontName = strInsertCoinFont;
			_numCreditsFontName = strNumCreditsFont;
			_creditsManager = manager;
			Debug.Assert(null != _creditsManager);
			TextJustification = Justify.Center;
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		public override void LoadContent()
		{
			//load teh fonts
			InsertCoinFont.Font = ScreenManager.Game.Content.Load<SpriteFont>(_insertCoinFontName);
			NumCreditsFont.Font = ScreenManager.Game.Content.Load<SpriteFont>(_numCreditsFontName);

			//initialize some default locations for text

			//Number of credits is displayed at the bottom of the screen
			NumCreditsTextLocation = new Vector2(Resolution.TitleSafeArea.Center.X,
				Resolution.TitleSafeArea.Bottom - NumCreditsFont.Font.LineSpacing);

			//Insert coin stuff is displayed right above that
			InsertCoinTextLocation = new Vector2(Resolution.TitleSafeArea.Center.X,
				NumCreditsTextLocation.Y - InsertCoinFont.Font.LineSpacing);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			ScreenManager.SpriteBatchBegin();

			//Draw the "Insert Coin" text!

			//dont display "insert coin" or "press start" if a game is currently in play
			if (!_creditsManager.GameInPlay)
			{
				//Are there any credits in the system?
				if ((2 <= _creditsManager.NumCredits) || _creditsManager.FreePlay)
				{
					DrawPressStart("Press 1P or 2P start!", gameTime);
				}
				else if (1 <= _creditsManager.NumCredits)
				{
					DrawPressStart("Press 1P Start!", gameTime);
				}
				else
				{
					//draw in slowly pulsating white letters
					InsertCoinFont.PulsateSpeed = 3.0f; //pulsate sloooowly
					InsertCoinFont.Write(InsertCoinText(),
						InsertCoinTextLocation,
						TextJustification, 
						1.0f, //write normal
						Color.White,
						ScreenManager.SpriteBatch,
						gameTime.TotalGameTime.TotalSeconds);
				}
			}

			//Draw the number of credits text!
			if (ShouldDisplayNumCredits())
			{
				NumCreditsFont.Write(NumCreditsText(),
					NumCreditsTextLocation,
					TextJustification, 
					0.6f, //write normal
					Color.White,
					ScreenManager.SpriteBatch,
					gameTime.TotalGameTime.TotalSeconds);
			}

			ScreenManager.SpriteBatchEnd();
		}

		/// <summary>
		/// Check if the screen should write the number of credits available
		/// </summary>
		/// <returns><c>true</c>, if display number credits was shoulded, <c>false</c> otherwise.</returns>
		public bool ShouldDisplayNumCredits()
		{
			if (!_creditsManager.GameInPlay)
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
		public string InsertCoinText()
		{
			if (_creditsManager.NumCoinsNeededForNextCredit() == 1)
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
		public string NumCreditsText()
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
		public void DrawPressStart(string strText, GameTime gameTime)
		{
			//Draw in big pulsating letters
			InsertCoinFont.PulsateSpeed = 6.0f; //pulsate faster
			InsertCoinFont.Write(strText,
				InsertCoinTextLocation,
				TextJustification,
				1.2f, //write bigger
				Color.White,
				ScreenManager.SpriteBatch,
				gameTime.TotalGameTime.TotalSeconds);
		}

		#endregion
	}
}

