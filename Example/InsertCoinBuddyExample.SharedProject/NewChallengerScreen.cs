using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace InsertCoinBuddyExample
{
	/// <summary>
	/// This screen is displayed when a second player joins a game, displays some text, and starts a new 2p game.
	/// </summary>
	public class NewChallengerScreen : WidgetScreen
	{
		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public NewChallengerScreen() : base("newChallenger")
		{
			CoverOtherScreens = true;
			this.Transition.OnTime = 1f;
			Transition.OffTime = 0.25f;
		}

		#endregion //Initialization

		#region Methods

		public override async Task LoadContent()
		{
			await base.LoadContent();

			var text = new Label("Here Comes A New Challenger!", Content, FontSize.Medium)
			{
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopTop),
				Position = Resolution.ScreenArea.Center,
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			};
			AddItem(text);
		}

		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			if (IsActive)
			{
				if (Time.CurrentTime >= 4f)
				{
					LoadingScreen.Load(ScreenManager, new GameplayScreen());
				}
			}
		}

		public override void Draw(GameTime gameTime)
		{
			//draw the text
			ScreenManager.SpriteBatchBegin();

			FadeBackground();

			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion
	}
}