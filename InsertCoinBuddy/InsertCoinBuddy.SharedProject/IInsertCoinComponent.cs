
using Microsoft.Xna.Framework;
using System;

namespace InsertCoinBuddy
{
	public interface IInsertCoinComponent
	{
		CreditManager Credits { get; }

		event EventHandler Updated;

		/// <summary>
		/// Gets or sets the coins per credit.
		/// </summary>
		/// <value>The coins per credit.</value>
		int CoinsPerCredit { get; set; }

		/// <summary>
		/// Get the current state of the game.
		/// </summary>
		GameState CurrentGameState { get; }

		/// <summary>
		/// Total number of coins in the system that haven't been spent
		/// </summary>
		int TotalCoins { get; }

		/// <summary>
		/// Total number of credits available to player
		/// </summary>
		int NumCredits { get; }

		/// <summary>
		/// Gets or sets the number coins not used by a credit.
		/// So if it is 2 coins per credit and there are 5 coins loaded, this will return 1.
		/// </summary>
		/// <value>The number coins.</value>
		int NumCoins { get; }

		/// <summary>
		/// Whether or not the game is in free play
		/// </summary>
		bool FreePlay { get; }

		/// <summary>
		/// Whether or not there are any credits available, or is free play we dont care about credits
		/// </summary>
		bool CreditAvailable { get; }

		/// <summary>
		/// Get the number of coins the player needs to enter before they will complete a credit
		/// </summary>
		/// <returns>The coins needed for next credit.</returns>
		int NumCoinsNeededForNextCredit { get; }

		/// <summary>
		/// A coin was added to the game!
		/// </summary>
		void AddCoin();

		/// <summary>
		/// One of the players has hit their "start" button
		/// </summary>
		/// <param name="player">The player that hit the button</param>
		void PlayerButtonPressed(PlayerIndex player);

		/// <summary>
		/// Someone tried to join a game.
		/// Check if they can join and update the coin state.
		/// </summary>
		/// <param name="player">The player trying to join the game</param>
		/// <param name="playSound">Soud to play if the player is able to join</param>
		/// <returns><c>true</c> if able to join a game, <c>false</c> otherwise.</returns>
		bool JoinGame(PlayerIndex player, bool playSound);

		/// <summary>
		/// Check whether or not a player is ready to start
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		bool IsReady(PlayerIndex player);

		/// <summary>
		/// Check whether or not a player is currently playing
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		bool IsPlaying(PlayerIndex player);

		/// <summary>
		/// Call this method when the game is finished playing, and returning to menus.
		/// </summary>
		void GameFinished();
	}
}
