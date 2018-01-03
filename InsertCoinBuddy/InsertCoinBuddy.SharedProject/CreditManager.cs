using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

namespace InsertCoinBuddy
{
	public class CreditManager : IInsertCoinComponent
	{
		#region Fields

		private GameState _gameState;

		/// <summary>
		/// If the game hasn't started yet, these are the players that are ready to play.
		/// </summary>
		private bool[] _readyPlayers = new bool[] { false, false, false, false };

		/// <summary>
		/// If the game is in play, these players are currently playing.
		/// </summary>
		private bool[] _currentPlayers = new bool[] { false, false, false, false };

		/// <summary>
		/// Event that gets called when a coin is dropped
		/// Used to exit menu screens
		/// </summary>
		public event EventHandler<EventArgs> OnCoinAdded;

		/// <summary>
		/// Event that gets called when a game is started.
		/// Can be triggered by:
		/// all players ready
		/// a player that is ready hitting "start"
		/// players are ready, and time is up
		/// </summary>
		public event EventHandler<GameStartEventArgs> OnGameStart;

		public event EventHandler Updated;

		#endregion //Fields

		#region Properties

		private string CoinSoundName { get; set; }

		private string PlayerJoinSoundName { get; set; }

		private SoundEffect CoinSound { get; set; }

		private SoundEffect PlayerJoinSound { get; set; }

		/// <summary>
		/// Gets or sets the coins per credit.
		/// </summary>
		/// <value>The coins per credit.</value>
		private int _coinsPerCredit;
		public int CoinsPerCredit
	{
			get
			{
				return _coinsPerCredit;
			}
			set
			{
				_coinsPerCredit = value;
				OnUpdated(this, new EventArgs());
			}
		}

		public bool CreditAvailable
		{
			get
			{
				return FreePlay || 1 <= NumCredits;
			}
		}

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
		private int _totalCoins;
		public int TotalCoins
		{
			get
			{
				return _totalCoins;
			}
			private set
			{
				_totalCoins = value;
				OnUpdated(this, new EventArgs());
			}
		}

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
		/// Gets or sets a value indicating whether a game is in play.
		/// </summary>
		/// <value><c>true</c> if game in play; otherwise, <c>false</c>.</value>
		public GameState CurrentGameState
		{
			get
			{
				return _gameState;
			}
			private set
			{
				_gameState = value;

				switch (CurrentGameState)
				{
					case GameState.Menu:
						{
							//if the game state is menus, no one is ready or playing
							for (int i = 0; i < 4; i++)
							{
								_readyPlayers[i] = false;
								_currentPlayers[i] = false;
							}
						}
						break;
					case GameState.Playing:
						{
							//if the game is in session, move the "ready" players to "playing"
							for (int i = 0; i < 4; i++)
							{
								_currentPlayers[i] = _readyPlayers[i] || _currentPlayers[i];
								_readyPlayers[i] = false;
							}
						}
						break;
				}
			}
		}

		/// <summary>
		/// Get the number of coins the player needs to enter before they will complete a credit
		/// </summary>
		/// <returns>The coins needed for next credit.</returns>
		public int NumCoinsNeededForNextCredit
		{
			get
			{
				return CoinsPerCredit - NumCoins;
			}
		}

		public CreditManager Credits
		{
			get
			{
				return this;
			}
		}

		#endregion //Properties

		#region Init

		/// <summary>
		/// Initializes a new instance of the <see cref="InsertCoinBuddy.CreditManager"/> class.
		/// </summary>
		public CreditManager(string coinSound,
			string playerJoinSound,
			int coinsPerCredit)
		{
			TotalCoins = 0;
			CurrentGameState = GameState.Menu;
			CoinSoundName = coinSound;
			PlayerJoinSoundName = playerJoinSound;
			CoinsPerCredit = coinsPerCredit;
		}

		public void LoadContent(ContentManager content)
		{
			if (!string.IsNullOrEmpty(CoinSoundName))
			{
				CoinSound = content.Load<SoundEffect>(CoinSoundName);
			}

			if (!string.IsNullOrEmpty(PlayerJoinSoundName))
			{
				PlayerJoinSound = content.Load<SoundEffect>(PlayerJoinSoundName);
			}
		}

		#endregion //Init

		#region Methods

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

			if (OnCoinAdded != null)
			{
				OnCoinAdded(this, new EventArgs());
			}
		}

		public void OnUpdated(object obj, EventArgs e)
		{
			if (null != Updated)
			{
				Updated(obj, e);
			}
		}

		/// <summary>
		/// Someone tried to start a game.
		/// Check if they can start a game and update the coin state.
		/// </summary>
		/// <returns><c>true</c> if able to start a game, <c>false</c> otherwise.</returns>
		public void PlayerButtonPressed(PlayerIndex player)
		{
			switch (CurrentGameState)
			{
				case GameState.Menu:
					{
						if (!FreePlay && (1 > NumCredits))
						{
							//no credits and not in free play mode! 
							return;
						}

						//remove a credit from the number of coins
						SubtractCredit();

						//play the sound for player join
						if (null != PlayerJoinSound)
						{
							PlayerJoinSound.Play();
						}

						//Set the player to "ready
						_readyPlayers[(int)player] = true;

						//are there any more credits?
						if (0 >= NumCredits)
						{
							//There are no more credits, so just let this guy start the game
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
						if (_readyPlayers[(int)player])
						{
							//If so, let him start the game.
							StartGame();
						}
						else
						{
							if (!FreePlay && (1 > NumCredits))
							{
								//no credits and not in free play mode! 
								return;
							}

							//remove a credit from the number of coins
							SubtractCredit();

							//play the sound for player join
							if (null != PlayerJoinSound)
							{
								PlayerJoinSound.Play();
							}

							//Set the player to "ready
							_readyPlayers[(int)player] = true;

							//are there any more credits?
							if (0 >= NumCredits)
							{
								//There are no more credits, so just let this guy start the game
								StartGame();
							}
						}
					}
					break;
				case GameState.Playing:
					{
						JoinGame(player, true);
					}
					break;
			}
		}

		/// <summary>
		/// Someone tried to join a game.
		/// Check if they can join and update the coin state.
		/// </summary>
		/// <returns><c>true</c> if able to join a game, <c>false</c> otherwise.</returns>
		public bool JoinGame(PlayerIndex player, bool playSound)
		{
			//Are we able to join a game?
			if ((GameState.Playing != CurrentGameState) ||
				(!FreePlay && (1 > NumCredits) ||
				IsPlaying(player)))
			{
				//Game is not in play, or no credits and not in free play mode, or the player is already playing!
				return false;
			}

			//remove a credit from the number of coins
			SubtractCredit();

			//play the sound for player join
			if (playSound && (null != PlayerJoinSound))
			{
				PlayerJoinSound.Play();
			}

			//Set the player as playing
			_readyPlayers[(int)player] = true;
			StartGame();

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

		private void StartGame()
		{
			CurrentGameState = GameState.Playing;

			if (null != OnGameStart)
			{
				OnGameStart(this, new GameStartEventArgs(_currentPlayers));
			}
		}

		public bool IsReady(PlayerIndex player)
		{
			return _readyPlayers[(int)player];
		}

		public bool IsPlaying(PlayerIndex player)
		{
			return _currentPlayers[(int)player];
		}

		public void GameFinished()
		{
			CurrentGameState = GameState.Menu;
		}

		#endregion //Methods
	}
}
