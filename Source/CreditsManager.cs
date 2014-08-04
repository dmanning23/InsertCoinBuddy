using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System;

namespace InsertCoinBuddy
{
	public class CreditsManager : DrawableGameComponent, ICreditsManager
	{
		#region Fields

		/// <summary>
		/// The _prev keys.  Used to check for key down
		/// </summary>
		private KeyboardState _prevKeys;

		private GamePadState[] _prevGamePadStates;

		/// <summary>
		/// Event that gets called when a coin is dropped
		/// Used to exit menu screens
		/// </summary>
		public event EventHandler<EventArgs> OnCoinAdded;

		private string CoinSoundName { get; set; }

		private string PlayerJoinSoundName { get; set; }

		private SoundEffect CoinSound { get; set; }

		private SoundEffect PlayerJoinSound { get; set; }

		#endregion //Fields

		#region Properties

		/// <summary>
		/// Gets or sets the coins per credit.
		/// </summary>
		/// <value>The coins per credit.</value>
		public int CoinsPerCredit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="InsertCoinBuddy.CreditsWatcher"/> is on free play.
		/// </summary>
		/// <value><c>true</c> if free play; otherwise, <c>false</c>.</value>
		public bool FreePlay 
		{
			get
			{
				return (0 == CoinsPerCredit);
			}
		}

		/// <summary>
		/// The number of coins that are currently in the system, available for use.
		/// </summary>
		public int TotalCoins { get; private set; }

		/// <summary>
		/// Gets or sets the number coins not used by a credit.
		/// So if it is 2 coins per credit and there are 5 coins loaded, this will return 1.
		/// </summary>
		/// <value>The number coins.</value>
		public int NumCoins
		{ 
			get
			{
				//guard against divide by zero
				if (CoinsPerCredit > 0)
				{
					return TotalCoins % CoinsPerCredit;
				}
				else
				{
					return TotalCoins;
				}
			}
		}

		/// <summary>
		/// The number of complete credits that have not been used.
		/// So if it is 2 coins per credit and there are 5 coins loaded, this will return 2.
		/// </summary>
		/// <value>The number of complete credits.</value>
		public int NumCredits
		{ 
			get
			{
				//guard against divide by zero
				if (CoinsPerCredit > 0)
				{
					return TotalCoins / CoinsPerCredit;
				}
				else
				{
					return TotalCoins;
				}
			}
		}

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

		/// <summary>
		/// Gets or sets a value indicating whether a game is in play.
		/// </summary>
		/// <value><c>true</c> if game in play; otherwise, <c>false</c>.</value>
		public bool GameInPlay { get; set; }

		/// <summary>
		/// whether or not the p1 side is playing
		/// </summary>
		public bool P1Playing { get; set; }

		/// <summary>
		/// whether or not the p2 side is playing
		/// </summary>
		public bool P2Playing { get; set; }

		#endregion //Properties

		#region Init

		/// <summary>
		/// Initializes a new instance of the <see cref="InsertCoinBuddy.CreditsWatcher"/> class.
		/// </summary>
		public CreditsManager(Game game, 
			string coinSound, 
			string playerJoinSound, 
			int coinsPerCredit) : base(game)
		{
			TotalCoins = 0;
			CoinKey = Keys.L;
			CoinButton = Buttons.LeftTrigger;
			GameInPlay = false;
			_prevKeys = new KeyboardState();
			CoinSoundName = coinSound;
			PlayerJoinSoundName = playerJoinSound;
			CoinsPerCredit = coinsPerCredit;

			_prevGamePadStates = new GamePadState[2];

			Game.Services.AddService(typeof(ICreditsManager), this);
		}

		protected override void LoadContent()
		{
			if (!string.IsNullOrEmpty(CoinSoundName))
			{
				CoinSound = Game.Content.Load<SoundEffect>(CoinSoundName);
			}

			if (!string.IsNullOrEmpty(PlayerJoinSoundName))
			{
				PlayerJoinSound = Game.Content.Load<SoundEffect>(PlayerJoinSoundName);
			}

			base.LoadContent();
		}

		#endregion //Init

		#region Methods

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
				CoinDrop();
			}

			//update the prev state
			_prevKeys = curKeys;

			for (int i = 0; i < 2; i++)
			{
				//get the current state
				GamePadState curPad = GamePad.GetState((PlayerIndex)i);

				//check for coin drop on gamepad
				if (curPad.IsButtonDown(CoinButton) && _prevGamePadStates[i].IsButtonUp(CoinButton))
				{
					CoinDrop();
				}

				//update the prev state
				_prevGamePadStates[i] = curPad;
			}

			base.Update(gameTime);
		}

		private void CoinDrop()
		{
			//Detected a coin drop!
			AddCoin();
			if (OnCoinAdded != null)
			{
				OnCoinAdded(this, new EventArgs());
			}
		}

		/// <summary>
		/// Somebody dropped a coin
		/// </summary>
		public void AddCoin()
		{
			TotalCoins++;

			if (null != CoinSound)
			{
				CoinSound.Play();
			}
		}

		/// <summary>
		/// Someone tried to start a game.
		/// Check if they can start a game and update the coin state.
		/// </summary>
		/// <returns><c>true</c> if able to start a game, <c>false</c> otherwise.</returns>
		public bool StartGame()
		{
			//Are we able to start a game?
			if (GameInPlay || (!FreePlay && (1 > NumCredits)))
			{
				//Game is already in play, or no credits and not in free play mode! 
				return false;
			}

			//remove a credit from the number of coins
			SubtractCredit();

			//play the sound for player join
			if (null != PlayerJoinSound)
			{
				PlayerJoinSound.Play();
			}

			//Able to start a game!
			return true;
		}

		/// <summary>
		/// Someone tried to join a game.
		/// Check if they can join and update the coin state.
		/// </summary>
		/// <returns><c>true</c> if able to join a game, <c>false</c> otherwise.</returns>
		public bool JoinGame(bool playSound)
		{
			//Are we able to join a game?
			if (!GameInPlay || (!FreePlay && (1 > NumCredits)))
			{
				//Game is not in play, or no credits and not in free play mode! 
				return false;
			}

			//remove a credit from the number of coins
			SubtractCredit();

			//play the sound for player join
			if (playSound && (null != PlayerJoinSound))
			{
				PlayerJoinSound.Play();
			}

			//Able to join a game!
			return true;
		}

		/// <summary>
		/// Removes one credit from the number of available coins
		/// </summary>
		private void SubtractCredit()
		{
			if (TotalCoins >= CoinsPerCredit)
			{
				TotalCoins -= CoinsPerCredit;
			}
		}

		/// <summary>
		/// All the coins were returned.
		/// </summary>
		public void CoinReturn()
		{
			TotalCoins = 0;
		}

		/// <summary>
		/// Get the number of coins the player needs to enter before they will complete a credit
		/// </summary>
		/// <returns>The coins needed for next credit.</returns>
		public int NumCoinsNeededForNextCredit()
		{
			return CoinsPerCredit - NumCoins;
		}

		#endregion //Methods
	}
}
