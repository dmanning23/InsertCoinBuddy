using InputHelper;
using MenuBuddy;
using Microsoft.Xna.Framework;

namespace InsertCoinBuddy
{
	/// <summary>
	/// The options screen is brought up over the top of the main menu
	/// screen, and gives the user a chance to configure the game in various hopefully useful ways.
	/// If you'd like to add more options to this screen, inherit from it and use SettingsComponent<YourScreen>()
	/// </summary>
	public class SettingsScreen : MenuScreen
	{
		#region Fields

		/// <summary>
		/// menu entry to change the difficulty
		/// </summary>
		private MenuEntryInt _difficulty;

		/// <summary>
		/// menu entry to change the num credits to play
		/// </summary>
		private MenuEntryInt _numCredits;

		/// <summary>
		/// menu entry to change the attract mode sound on/off
		/// </summary>
		private MenuEntryBool _attractModeSound;

		/// <summary>
		/// menu entry to change the attract mode music on/off
		/// </summary>
		private MenuEntryBool _attractModeMusic;

		private MenuEntry _doneMenuEntry;

		public ISettingsComponent Settings { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		public SettingsScreen()
			: base("Operator Settings")
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = false;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			//Create our menu entries.
			_difficulty = new MenuEntryInt("Difficulty", Settings.Settings.Difficulty, Content)
			{
				Step = 1,
				Min = 1,
				Max = 10,
			};

			_numCredits = new MenuEntryInt("Credits Per Play", Settings.Settings.CoinsPerCredit, Content)
			{
				Step = 1,
				Min = 0,
				Max = 20,
			};

			_attractModeSound = new MenuEntryBool("Attract Mode Sound", Settings.Settings.AttractModeSound, Content)
			{
			};
			
			_attractModeMusic = new MenuEntryBool("Attract Mode Music", Settings.Settings.AttractModeMusic, Content)
			{
			};

			_doneMenuEntry = new MenuEntry("Done", Content)
			{
			};

			_doneMenuEntry.OnClick += Done;

			//Add entries to the menu.
			AddMenuEntry(_difficulty);
			AddMenuEntry(_numCredits);
			AddMenuEntry(_attractModeSound);
			AddMenuEntry(_attractModeMusic);
			AddMenuEntry(_doneMenuEntry);
		}

		#endregion

		#region Handle Input

		/// <summary>
		/// Handler for when the user has cancelled the menu.
		/// </summary>
		protected virtual void Done(object obj, ClickEventArgs e)
		{
			//update settings object from menu entries
			Settings.Settings.Difficulty = _difficulty.Value;
			Settings.Settings.CoinsPerCredit = _numCredits.Value;
			Settings.Settings.AttractModeSound = _attractModeSound.Value;
			Settings.Settings.AttractModeMusic = _attractModeMusic.Value;

			//save settings
			Settings.SaveSettings();

			ExitScreen();
		}

		public override void Draw(GameTime gameTime)
		{
			//Draw on a black background
			ScreenManager.SpriteBatchBegin();
			FadeBackground();
			ScreenManager.SpriteBatchEnd();
			
			base.Draw(gameTime);
		}

		#endregion
	}
}