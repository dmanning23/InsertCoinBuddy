using System;

namespace InsertCoinBuddy
{
	/// <summary>
	/// Event arguments that get fired off when a game is started
	/// </summary>
	public class GameStartEventArgs : EventArgs
    {
		#region Fields

		private bool[] _currentPlayers = new bool[] { false, false, false, false };

		#endregion //Fields

		#region Properties

		public bool[] CurrentPlayers
		{
			get
			{
				return _currentPlayers;
			}
		}

		#endregion //Properties

		#region Method

		public GameStartEventArgs(bool[] currentPlayers)
		{
			_currentPlayers = currentPlayers;
		}

		#endregion //Method
	}
}
