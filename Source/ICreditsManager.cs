
namespace InsertCoinBuddy
{
	public interface ICreditsManager
	{
		/// <summary>
		/// Gets or sets the coins per credit.
		/// </summary>
		/// <value>The coins per credit.</value>
		int CoinsPerCredit { get; set; }

		/// <summary>
		/// Someone tried to join a game.
		/// Check if they can join and update the coin state.
		/// </summary>
		/// <returns><c>true</c> if able to join a game, <c>false</c> otherwise.</returns>
		bool JoinGame();

		/// <summary>
		/// whether or not the p1 side is playing
		/// </summary>
		bool P1Playing { get; set; }

		/// <summary>
		/// whether or not the p2 side is playing
		/// </summary>
		bool P2Playing { get; set; }
	}
}
