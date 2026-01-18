using System;
using System.Collections.Generic;

namespace InsertCoinBuddy
{
	/// <summary>
	/// Event arguments that get fired off when a game is started
	/// </summary>
	public class GameStartEventArgs : EventArgs
	{
		public List<bool> CurrentPlayers { get; set; }

		public GameStartEventArgs(List<bool> currentPlayers)
		{
			CurrentPlayers = currentPlayers;
		}
	}
}
