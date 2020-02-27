using System;
using System.Collections.Generic;

namespace InsertCoinBuddy
{
	public interface IInsertCoinService
	{
		/// <summary>
		/// Event that gets called when a coin is dropped
		/// </summary>
		event EventHandler<CoinEventArgs> OnCoinAdded;

		/// <summary>
		/// Event that gets called when a player joins the game
		/// </summary>
		event EventHandler<CoinEventArgs> OnPlayerJoined;

		/// <summary>
		/// Event that gets called when a game is started.
		/// Can be triggered by:
		/// all players ready
		/// a player that is ready hitting "start"
		/// players are ready, and time is up
		/// </summary>
		event EventHandler<GameStartEventArgs> OnGameStart;

		List<PlayerCredits> Players { get; }

		/// <summary>
		/// Gets or sets the coins per credit.
		/// </summary>
		/// <value>The coins per credit.</value>
		int CoinsPerCredit { get; set; }

		/// <summary>
		/// Whether or not the game is in free play
		/// </summary>
		bool FreePlay { get; }

		/// <summary>
		/// Get the current state of the game.
		/// </summary>
		GameState CurrentGameState { get; set; }

		void Update();
	}
}
