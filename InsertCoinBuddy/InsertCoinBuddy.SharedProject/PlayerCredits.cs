using HadoukInput;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsertCoinBuddy
{
	public class PlayerCredits
	{
		#region Properties

		/// <summary>
		/// If the game hasn't started yet, these are the players that are ready to play.
		/// </summary>
		public bool Ready { get; set; }

		/// <summary>
		/// If the game is in play, these players are currently playing.
		/// </summary>
		public bool Current { get; set; }

		/// <summary>
		/// The number of coins that are currently in the system, available for use.
		/// </summary>
		public int TotalCoins { get; set; }

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
				if (InsertCoinService.CoinsPerCredit > 0)
				{
					return TotalCoins % InsertCoinService.CoinsPerCredit;
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
				if (InsertCoinService.CoinsPerCredit > 0)
				{
					return TotalCoins / InsertCoinService.CoinsPerCredit;
				}
				else
				{
					return TotalCoins;
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
				return InsertCoinService.CoinsPerCredit - NumCoins;
			}
		}

		public bool CreditAvailable
		{
			get
			{
				return InsertCoinService.FreePlay || 1 <= NumCredits;
			}
		}

		private IInsertCoinService InsertCoinService { get; set; }

		public int PlayerIndex { get; set; }

		#endregion //Properties

		#region Methods

		public PlayerCredits(IInsertCoinService service, int playerIndex)
		{
			Ready = false;
			Current = false;
			TotalCoins = 0;
			InsertCoinService = service;
			PlayerIndex = playerIndex;
		}

		public bool CheckCoin(KeyboardState prevKeys, KeyboardState curKeys, GamePadState prevGamePad, GamePadState curGamePad)
		{
			//check the keyboard for coins
			if (Mappings.UseKeyboard[PlayerIndex])
			{
				if (curKeys.IsKeyDown(Mappings.KeyMaps[PlayerIndex].Select) && prevKeys.IsKeyUp(Mappings.KeyMaps[PlayerIndex].Select))
				{
					AddCoin();
					return true;
				}
			}

			//check the gamepdad for coins
			if (curGamePad.IsButtonDown(Buttons.Back) && prevGamePad.IsButtonUp(Buttons.Back))
			{
				AddCoin();
				return true;
			}

			//no coin
			return false;
		}

		/// <summary>
		/// Somebody dropped a coin
		/// </summary>
		public void AddCoin()
		{
			TotalCoins++;
		}

		public bool CheckStart(KeyboardState prevKeys, KeyboardState curKeys, GamePadState prevGamePad, GamePadState curGamePad)
		{
			//check the keyboard for coins
			if (Mappings.UseKeyboard[PlayerIndex])
			{
				if (curKeys.IsKeyDown(Mappings.KeyMaps[PlayerIndex].Start) && prevKeys.IsKeyUp(Mappings.KeyMaps[PlayerIndex].Start))
				{
					return CreditAvailable;
				}
			}

			//check the gamepdad for coins
			if (curGamePad.IsButtonDown(Buttons.Start) && prevGamePad.IsButtonUp(Buttons.Start))
			{
				return CreditAvailable;
			}

			//no coin
			return false;
		}


		/// <summary>
		/// Someone tried to join a game.
		/// Check if they can join and update the coin state.
		/// </summary>
		/// <returns><c>true</c> if able to join a game, <c>false</c> otherwise.</returns>
		public bool JoinGame()
		{
			//Are we able to join a game?
			if ((GameState.Playing != InsertCoinService.CurrentGameState) ||
				(!CreditAvailable ||
				Current))
			{
				//Game is not in play, or no credits and not in free play mode, or the player is already playing!
				return false;
			}

			//remove a credit from the number of coins
			if (SubtractCredit())
			{
				//Set the player as playing
				Current = true;

				//Able to join a game!
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Removes one credit from the number of available coins
		/// </summary>
		public bool SubtractCredit()
		{
			if (!CreditAvailable)
			{
				return false;
			}

			TotalCoins -= InsertCoinService.CoinsPerCredit;

			return true;
		}

		/// <summary>
		/// All the coins were returned.
		/// </summary>
		public void CoinReturn()
		{
			TotalCoins = 0;
		}

		#endregion //Methods
	}
}
