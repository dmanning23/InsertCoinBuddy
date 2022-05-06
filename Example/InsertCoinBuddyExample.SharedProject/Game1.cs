using HadoukInput;
using InputHelper;
using InsertCoinBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InsertCoinBuddyExample
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : ControllerGame
	{
		/// <summary>
		/// The credit manager.
		/// </summary>
		private InsertCoinComponent _insertCoin;

		/// <summary>
		/// Component used to save system settings
		/// </summary>
		private ISettingsComponent _settings;

		public Game1()
		{
			//FullScreen = true;
			Mappings.UseKeyboard[0] = true;
			Mappings.UseKeyboard[1] = true;
			Mappings.KeyMaps[0].UseIpacMappings(0);
			Mappings.KeyMaps[1].UseIpacMappings(1);

			var debug = new DebugInputComponent(this, ResolutionBuddy.Resolution.TransformationMatrix);

			//Setup the credits manager.
			var insertCoin = new InsertCoinComponent(this, 3, 2); //$.75 per game
			_insertCoin = insertCoin;

			//Add the settings manager
			_settings = new SettingsComponent<SettingsScreen>(this, Keys.K, Buttons.Back);
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			// Activate the first screens.
			ScreenManager.SetTopScreen(new InsertCoinScreen(@"Fonts\ArialBlack24", @"Fonts\ArialBlack24", "coindrop",  "gamestart"), null);
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void InitStyles()
		{
			base.InitStyles();
			//DefaultStyles.Instance().MainStyle.HasOutline = true;
			//DefaultStyles.Instance().MenuEntryStyle.HasOutline = true;
			//DefaultStyles.Instance().MenuTitleStyle.HasOutline = true;
			//DefaultStyles.Instance().MessageBoxStyle.HasOutline = true;
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// TODO: Add your update logic here
			base.Update(gameTime);
		}

		/// <summary>
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public override IScreen[] GetMainMenuScreenStack()
		{
			return new IScreen[] { new BackgroundScreen(), new MainMenuScreen() };
		}
	}
}
