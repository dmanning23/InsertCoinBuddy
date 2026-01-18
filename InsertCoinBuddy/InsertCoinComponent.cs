using Microsoft.Xna.Framework;

namespace InsertCoinBuddy
{
	public class InsertCoinComponent : DrawableGameComponent, IGameComponent
	{
		#region Properties

		IInsertCoinService InsertCoinService { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="InsertCoinBuddy.CreditsWatcher"/> class.
		/// </summary>
		public InsertCoinComponent(Game game, int coinsPerCredit, int numPlayers) : base(game)
		{
			InsertCoinService = new InsertCoinService(coinsPerCredit, numPlayers);

			Game.Components.Add(this);
			Game.Services.AddService<IInsertCoinService>(InsertCoinService);
		}

		/// <summary>
		/// Called each frame, checks the keyboard input for a coin drop
		/// </summary>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			InsertCoinService.Update();
		}

		#endregion //Methods
	}
}
