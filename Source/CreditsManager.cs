using System;
using Microsoft.Xna.Framework.Input;

namespace InsertCoinBuddy
{
	public class CreditsWatcher
	{
		#region Fields

		/// <summary>
		/// The number of coins that have not been used.
		/// </summary>
		private int _coins = 0;

		/// <summary>
		/// The _prev keys.  Used to check for key down
		/// </summary>
		private KeyboardState _prevKeys;

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
		public bool FreePlay { get; set; }

		/// <summary>
		/// Gets or sets the number coins not used by a credit.
		/// So if it is 2 coins per credit and there are 5 coins loaded, this will return 1.
		/// </summary>
		/// <value>The number coins.</value>
		public int NumCoins
		{ 
			get
			{
				return _coins % CoinsPerCredit;
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
				return _coins / CoinsPerCredit;
			}
		}

		/// <summary>
		/// The keyboard key we will listen to for coin drops
		/// defaults to q? sure why not.
		/// </summary>
		/// <value>The coin key.</value>
		public Keys CoinKey { get; set; }

		#endregion //Properties

		/// <summary>
		/// Initializes a new instance of the <see cref="InsertCoinBuddy.CreditsWatcher"/> class.
		/// </summary>
		public CreditsWatcher()
		{
			CoinKey = Keys.Q;
			_prevKeys = new KeyboardState();
		}

		/// <summary>
		/// Called each frame, checks the keyboard input for a coin drop
		/// </summary>
		public void Update()
		{
			//update the keyboard state
			KeyboardState curKeys = Keyboard.GetState();

			//Check for a coin drop
			if (_prevKeys.IsKeyUp(CoinKey) && curKeys.IsKeyDown(CoinKey))
			{
				//Detected a coin drop!
				AddCoin();
			}

			//update the prev state
			_prevKeys = curKeys;
		}

		/// <summary>
		/// Somebody dropped a coin
		/// </summary>
		public void AddCoin()
		{
			_coins++;
		}

		/// <summary>
		/// Someone tried to start a game.
		/// Check if they can start a game and update the coin state.
		/// </summary>
		/// <returns><c>true</c> if able to start a game, <c>false</c> otherwise.</returns>
		public bool StartGame()
		{
			//Are we able to start a game?
			if ((1 > NumCredits) || !FreePlay)
			{
				//No credits and not in free play mode! 
				return false;
			}

			//remove a credit from the number of coins
			if (_coins >= CoinsPerCredit)
			{
				_coins -= CoinsPerCredit;
			}

			//Able to start a game!
			return true;
		}

		/// <summary>
		/// All the coins were returned.
		/// </summary>
		public void CoinReturn()
		{
			_coins = 0;
		}
	}
}

