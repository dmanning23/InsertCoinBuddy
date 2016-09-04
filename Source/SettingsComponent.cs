using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SQLiteConnectionBuddy;
using System;
using System.Linq;

namespace InsertCoinBuddy
{
	/// <summary>
	/// This is a game component that sits and listens for a specific keypress/controller button.
	/// When that input is received, the Settings screen is popped up.
	/// </summary>
	public class SettingsComponent<T> : DrawableGameComponent, ISettingsComponent where T : SettingsScreen, new()
	{
		#region Fields

		const string _db = "Settings.db";

		/// <summary>
		/// Lock object
		/// </summary>
		static object locker = new object();

		#endregion //Fields

		#region Properties

		/// <summary>
		/// the settings object
		/// </summary>
		public Settings Settings { get; private set; }

		/// <summary>
		/// the key to press to bring up the menu screen
		/// </summary>
		protected Keys MenuKey { get; set; }

		/// <summary>
		/// the button to press to bring up the menu screen
		/// </summary>
		protected Buttons MenuButton { get; set; }

		private IInsertCoinComponent InsertCoinComponent { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="game"></param>
		/// <param name="menuKey">the key to press to bring up the settings screen</param>
		/// <param name="menuButton">the controller button to bring up the settings screen</param>
		public SettingsComponent(Game game, 
			Keys menuKey = Keys.K,
			Buttons menuButton = Buttons.Back)
			: base(game)
		{
			MenuKey = menuKey;
			MenuButton = menuButton;

			Game.Services.AddService<ISettingsComponent>(this);
		}

		/// <summary>
		/// load all the content required for this dude
		/// </summary>
		public override void Initialize()
		{
			InsertCoinComponent = Game.Services.GetService<IInsertCoinComponent>();
			if (null == InsertCoinComponent)
			{
				throw new Exception("Tried to initialize SettingsComponent, but InsertCoinComponent was missing");
			}

			base.Initialize();
		}

		protected override void LoadContent()
		{
			LoadSettings();
			base.LoadContent();
		}

		/// <summary>
		/// Called every frame to update the FPS
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			//check for magic button press to load settingsscreen
			if (ScreenRequest())
			{
				//check if the menu is already being displayed
				var screen = CreateSettingsScreen();
				var screenManager = Game.Services.GetService<IScreenManager>();
				if (null == screenManager.FindScreen(screen.ScreenName))
				{
					//add a settings screen and display it
					screenManager.AddScreen(screen, null);
				}
			}
		}

		/// <summary>
		/// Check if the player tried to pop up the operator screen
		/// </summary>
		/// <returns></returns>
		private bool ScreenRequest()
		{
			return (Keyboard.GetState().IsKeyDown(MenuKey) ||
				GamePad.GetState(PlayerIndex.One).IsButtonDown(MenuButton));
		}

		/// <summary>
		/// factory method to create the correct settings screen
		/// </summary>
		/// <returns></returns>
		private SettingsScreen CreateSettingsScreen()
		{
			var screen = new T()
			{
				Settings = this
			};
			return screen;
		}

		/// <summary>
		/// Load the settings object from the file
		/// </summary>
		private void LoadSettings()
		{
			//load settingsfile
			using (var db = SQLiteConnectionHelper.GetConnection(_db))
			{
				//this will create the table if it doesn exist, upgrade if it has changed, or nothing if it is the same
				db.CreateTable<Settings>();
			}

			//load from the db
			lock (locker)
			{
				using (var connection = SQLiteConnectionHelper.GetConnection(_db))
				{
					Settings = connection.Table<Settings>().FirstOrDefault(x => x.Id == 0);
					if (null == Settings)
					{
						Settings = new Settings();
					}
				}
			}

			//set the num coins per credit
			InsertCoinComponent.CoinsPerCredit = Settings.CoinsPerCredit;
		}

		public void SaveSettings()
		{
			//set the num of credits

			//save to the db
			lock (locker)
			{
				using (var connection = SQLiteConnectionHelper.GetConnection(_db))
				{
					connection.InsertOrReplace(Settings);
				}
			}
		}

		#endregion //Methods
	}
}