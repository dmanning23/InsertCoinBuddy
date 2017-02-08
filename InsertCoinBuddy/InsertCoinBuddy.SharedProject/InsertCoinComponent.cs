using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InsertCoinBuddy
{
	public class InsertCoinComponent : DrawableGameComponent, IInsertCoinComponent, IGameComponent
	{
		#region Fields

		/// <summary>
		/// The _prev keys.  Used to check for key down
		/// </summary>
		private KeyboardState _prevKeys;

		private GamePadState[] _prevGamePadStates;

		public event EventHandler Updated;

		#endregion //Fields

		#region Properties

		public CreditManager Credits { get; private set; }
		
		/// <summary>
		/// The keyboard key we will listen to for coin drops
		/// defaults to q? sure why not.
		/// </summary>
		/// <value>The coin key.</value>
		public Keys CoinKey { get; set; }

		/// <summary>
		/// the game pad button to listen for coin drops
		/// defaults to ltrigger.
		/// </summary>
		public Buttons CoinButton { get; set; }

		public int CoinsPerCredit
		{
			get
			{
				return Credits.CoinsPerCredit;
			}

			set
			{
				Credits.CoinsPerCredit = value;
			}
		}

		public GameState CurrentGameState
		{
			get
			{
				return Credits.CurrentGameState;
			}
		}

		public int TotalCoins
		{
			get
			{
				return Credits.TotalCoins;
			}
		}

		public int NumCredits
		{
			get
			{
				return Credits.NumCredits;
			}
		}

		public int NumCoins
		{
			get
			{
				return Credits.NumCoins;
			}
		}

		public bool CreditAvailable
		{
			get
			{
				return Credits.CreditAvailable;
			}
		}

		public bool FreePlay
		{
			get
			{
				return Credits.FreePlay;
			}
		}

		public int NumCoinsNeededForNextCredit
		{
			get
			{
				return Credits.NumCoinsNeededForNextCredit;
			}
		}

		#endregion //Properties

		#region Init

		/// <summary>
		/// Initializes a new instance of the <see cref="InsertCoinBuddy.CreditsWatcher"/> class.
		/// </summary>
		public InsertCoinComponent(Game game,
			string coinSound,
			string playerJoinSound,
			int coinsPerCredit) : base(game)
		{
			Credits = new CreditManager(coinSound, playerJoinSound, coinsPerCredit);
			Credits.Updated += OnUpdated;
			CoinKey = Keys.L;
			CoinButton = Buttons.LeftTrigger;
			_prevKeys = new KeyboardState();

			_prevGamePadStates = new GamePadState[4];

			Game.Components.Add(this);
			Game.Services.AddService<IInsertCoinComponent>(this);
		}

		protected override void LoadContent()
		{
			Credits.LoadContent(Game.Content);

			base.LoadContent();
		}

		#endregion //Init

		#region Methods

		private void OnUpdated(object obj, EventArgs e)
		{
			if (null != Updated)
			{
				Updated(obj, e);
			}
		}

		/// <summary>
		/// Called each frame, checks the keyboard input for a coin drop
		/// </summary>
		public override void Update(GameTime gameTime)
		{
			//update the keyboard state
			KeyboardState curKeys = Keyboard.GetState();

			//Check for a coin drop on keyboard
			if (curKeys.IsKeyDown(CoinKey) && _prevKeys.IsKeyUp(CoinKey))
			{
				Credits.AddCoin();
			}

			//update the prev state
			_prevKeys = curKeys;

			for (int i = 0; i < 4; i++)
			{
				//get the current state
				GamePadState curPad = GamePad.GetState((PlayerIndex)i);

				//check for coin drop on gamepad
				if (curPad.IsButtonDown(CoinButton) && _prevGamePadStates[i].IsButtonUp(CoinButton))
				{
					Credits.AddCoin();
				}

				//update the prev state
				_prevGamePadStates[i] = curPad;
			}

			base.Update(gameTime);
		}

		public void AddCoin()
		{
			Credits.AddCoin();
		}

		public void PlayerButtonPressed(PlayerIndex player)
		{
			Credits.PlayerButtonPressed(player);
		}

		public bool JoinGame(PlayerIndex player, bool playSound)
		{
			return Credits.JoinGame(player, playSound);
		}

		public bool IsReady(PlayerIndex player)
		{
			return Credits.IsReady(player);
		}

		public bool IsPlaying(PlayerIndex player)
		{
			return Credits.IsPlaying(player);
		}

		public void GameFinished()
		{
			Credits.GameFinished();
		}

		#endregion //Methods
	}
}
