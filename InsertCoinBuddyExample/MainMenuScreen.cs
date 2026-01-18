using InputHelper;
using InsertCoinBuddy;
using MenuBuddy;
using System.Threading.Tasks;

namespace InsertCoinBuddyExample
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	public class MainMenuScreen : MenuScreen, IMainMenu
	{
		/// <summary>
		/// The credit manager.
		/// </summary>
		private IInsertCoinService _service;

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public MainMenuScreen() : base("Main Menu")
		{
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			_service = ScreenManager.Game.Services.GetService<IInsertCoinService>();
			_service.OnGameStart += OnStartGame;
		}

		public override void UnloadContent()
		{
			base.UnloadContent();

			_service.OnGameStart -= OnStartGame;
		}

        public async void OnStartGame(object obj, GameStartEventArgs e)
        {
            ScreenManager.PopToScreen<BackgroundScreen>();
            await LoadingScreen.Load(ScreenManager, new IScreen[] { new GameplayScreen() });
		}

		public override void Cancelled(object obj, ClickEventArgs e)
		{
		}
	}
}