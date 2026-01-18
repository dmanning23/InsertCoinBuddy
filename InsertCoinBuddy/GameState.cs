
namespace InsertCoinBuddy
{
	/// <summary>
	/// This is the current state of the game.
	/// </summary>
	public enum GameState
	{
		/// <summary>
		/// The game hasn't started yet, we are waiting for players.
		/// </summary>
		Menu,

		/// <summary>
		/// The game hasn't started yet, but some players have already joined.
		/// We are waiting for the rest of the players.
		/// </summary>
		Ready,

		/// <summary>
		/// The game is being played.
		/// </summary>
		Playing
	}
}
