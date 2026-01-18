using FontBuddyLib;
using HadoukInput;
using InsertCoinBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace InsertCoinBuddyExample
{
	/// <summary>
	/// This screen displays on top of all the other screens
	/// </summary>
	public class GameplayScreen : Screen, IGameScreen
	{
		#region Properties

		const float TextVelocity = 3.0f;

		/// <summary>
		/// current location of the text
		/// </summary>
		Vector2 TextLocation = Vector2.Zero;

		/// <summary>
		/// current direction the text is travelling in
		/// </summary>
		Vector2 TextDirection;

		/// <summary>
		/// thing for writing text
		/// </summary>
		FontBuddy Text;

		IInsertCoinService _service;

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public GameplayScreen()
		{
			TextLocation = new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Center.Y);
			TextDirection = new Vector2(TextVelocity, TextVelocity);

			CoveredByOtherScreens = false;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			_service = ScreenManager.Game.Services.GetService<IInsertCoinService>();
			_service.OnGameStart += OnStartGame;

			Text = new FontBuddy();
			Text.LoadContent(Content, @"Fonts\ArialBlack72");
		}

		public override void UnloadContent()
		{
			base.UnloadContent();

			_service.OnGameStart -= OnStartGame;
		}

		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			if (HasFocus)
			{
				//move the text
				TextLocation += TextDirection;

				//bounce the text off the walls
				if ((TextLocation.X - 256) <= 0)
				{
					TextDirection.X = TextVelocity;
				}
				else if ((TextLocation.X + 256) >= Resolution.ScreenArea.Right)
				{
					TextDirection.X = -TextVelocity;
				}

				if (TextLocation.Y <= 0)
				{
					TextDirection.Y = TextVelocity;
				}
				else if ((TextLocation.Y + 128) >= Resolution.ScreenArea.Bottom)
				{
					TextDirection.Y = -TextVelocity;
				}
			}
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			//draw the text
			ScreenManager.SpriteBatchBegin();
			Text.Write("Gameplay Screen!!!", TextLocation, Justify.Center, 1.0f, Color.Red, ScreenManager.SpriteBatch, Time);

			Vector2 quitLocation = new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Top);

			Text.Write("Press 'space' to end game", quitLocation, Justify.Center, 0.75f, Color.Green, ScreenManager.SpriteBatch, Time);

			ScreenManager.SpriteBatchEnd();
		}

		public void HandleInput(IInputState input)
		{
			var inputState = input as InputState;
			if (IsActive)
			{
				//does the uesr want to exit?
				if (inputState.IsNewKeyPress(Keys.Space))
				{
					//Load the main menu back up
					ScreenManager.PopToScreen<BackgroundScreen>();
					LoadingScreen.Load(ScreenManager, ScreenManager.MainMenuStack());

					//the game isn't playing anymore
					_service.CurrentGameState = GameState.Menu;
				}
			}
		}

		public async void OnStartGame(object obj, GameStartEventArgs e)
		{
			await ScreenManager.AddScreen(new NewChallengerScreen(), null);
		}

		#endregion
	}
}