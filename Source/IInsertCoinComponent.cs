
using Microsoft.Xna.Framework;

namespace InsertCoinBuddy
{
	public interface IInsertCoinComponent : IGameComponent
	{
		/// <summary>
		/// Gets or sets the coins per credit.
		/// </summary>
		/// <value>The coins per credit.</value>
		int CoinsPerCredit { get; set; }

		/// <summary>
		/// Tell the credits manager whether or not a game is in session
		/// </summary>
		bool GameInPlay { get; set; }

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
		/// Get the number of coins the player needs to enter before they will complete a credit
		/// </summary>
		/// <returns>The coins needed for next credit.</returns>
		int NumCoinsNeededForNextCredit { get; }

		/// <summary>
		/// Start a game with a particular player
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		bool StartGame(PlayerIndex player);

		/// <summary>
		/// Someone tried to join a game.
		/// Check if they can join and update the coin state.
		/// </summary>
		/// <param name="player">The player trying to join the game</param>
		/// <param name="playSound">Soud to play if the player is able to join</param>
		/// <returns><c>true</c> if able to join a game, <c>false</c> otherwise.</returns>
		bool JoinGame(PlayerIndex player, bool playSound);

		/// <summary>
		/// Check whether or not a player is currently playing
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		bool IsPlaying(PlayerIndex player);
	}
}
