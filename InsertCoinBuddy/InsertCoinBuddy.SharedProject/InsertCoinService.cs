using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsertCoinBuddy
{
	public class InsertCoinService : IInsertCoinService
	{
		#region Properties

		public event EventHandler<CoinEventArgs> OnCoinAdded;

		public event EventHandler<CoinEventArgs> OnPlayerJoined;

		public event EventHandler<GameStartEventArgs> OnGameStart;

		public List<PlayerCredits> Players { get; private set; }

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

		private GameState _gameState;
		/// <summary>
		/// Gets or sets a value indicating whether a game is in play.
		/// </summary>
		/// <value><c>true</c> if game in play; otherwise, <c>false</c>.</value>
		public GameState CurrentGameState
		{
			get
			{
				return _gameState;
			}
			set
			{
				_gameState = value;

				switch (CurrentGameState)
				{
					case GameState.Menu:
						{
							//if the game state is menus, no one is ready or playing
							foreach (var player in Players)
							{
								player.Ready = false;
								player.Current = false;
							}
						}
						break;
					case GameState.Playing:
						{
							//if the game is in session, move the "ready" players to "playing"
							foreach (var player in Players)
							{
								player.Current = player.Ready || player.Current;
								player.Ready = false;
							}
						}
						break;
				}
			}
		}

		/// <summary>
		/// The _prev keys.  Used to check for key down
		/// </summary>
		private KeyboardState _prevKeys;

		private GamePadState[] _prevGamePadStates;

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="InsertCoinBuddy.InsertCoinService"/> class.
		/// </summary>
		public InsertCoinService(int coinsPerCredit, int numPlayers)
		{
			_gameState = GameState.Menu;

			CoinsPerCredit = coinsPerCredit;

			Players = new List<PlayerCredits>();
			numPlayers = Math.Min(numPlayers, GamePad.MaximumGamePadCount);
			for (var i = 0; i < numPlayers; i++)
			{
				Players.Add(new PlayerCredits(this, i));
			}

			_prevKeys = new KeyboardState();
			_prevGamePadStates = new GamePadState[numPlayers];
		}

		public void Update()
		{
			//update the keyboard state
			KeyboardState curKeys = Keyboard.GetState();

			for (int i = 0; i < Players.Count; i++)
			{
				//get the current gamepad state
				GamePadState curPad = GamePad.GetState(i);

				//check for coin drops
				if (Players[i].CheckCoin(_prevKeys, curKeys, _prevGamePadStates[i], curPad))
				{
					CoinAdded(i);
				}

				//check for start button
				if (Players[i].CheckStart(_prevKeys, curKeys, _prevGamePadStates[i], curPad))
				{
					PlayerButtonPressed(i);
				}

				//update the prev gamepad state
				_prevGamePadStates[i] = curPad;
			}

			//update the prev state
			_prevKeys = curKeys;
		}

		/// <summary>
		/// Somebody dropped a coin
		/// </summary>
		private void CoinAdded(int playerIndex)
		{
			if (OnCoinAdded != null)
			{
				OnCoinAdded(this, new CoinEventArgs(playerIndex));
			}
		}

		/// <summary>
		/// Someone tried to start a game.
		/// Check if they can start a game and update the coin state.
		/// </summary>
		/// <returns><c>true</c> if able to start a game, <c>false</c> otherwise.</returns>
		private void PlayerButtonPressed(int playerIndex)
		{
			if (!Players[playerIndex].CreditAvailable)
			{
				//no credits and not in free play mode! 
				return;
			}

			switch (CurrentGameState)
			{
				case GameState.Menu:
					{
						//play the sound for player join
						if (null != OnPlayerJoined)
						{
							OnPlayerJoined(this, new CoinEventArgs(playerIndex));
						}

						//Set the player to "ready
						Players[playerIndex].Ready = true;

						//Are we waiting for any more players?
						if (!Players.Any(x => x.CreditAvailable && x.PlayerIndex != playerIndex))
						{
							//No one else is putting quarters in
							StartGame();
						}
						else
						{
							//There are more credits, move to "ready" game state and wait for other players to join
							CurrentGameState = GameState.Ready;
						}
					}
					break;
				case GameState.Ready:
					{
						//is this guy already in the "ready" state?
						if (Players[playerIndex].Ready)
						{
							//If so, let him start the game.
							StartGame();
						}
						else
						{
							//Set the player to "ready
							Players[playerIndex].Ready = true;

							//Are we waiting for any more players?
							if (!Players.Any(x => x.CreditAvailable && x.PlayerIndex != playerIndex && !x.Ready))
							{
								//There are no more credits, so just let this guy start the game
								StartGame();
							}
						}
					}
					break;
				case GameState.Playing:
					{
						if (Players[playerIndex].JoinGame())
						{
							if (null != OnPlayerJoined)
							{
								OnPlayerJoined(this, new CoinEventArgs(playerIndex));
							}
						}
					}
					break;
			}
		}

		private void StartGame()
		{
			CurrentGameState = GameState.Playing;

			for (var i = 0; i < Players.Count; i++)
			{
				//subtract a credit from the player
				if (Players[i].SubtractCredit())
				{
					//set the player to "playing"
					Players[i].Current = true;
				}
			}

			if (null != OnGameStart)
			{
				OnGameStart(this, new GameStartEventArgs(Players.Select(x => x.Ready).ToList()));
			}
		}

		#endregion //Methods
	}
}
