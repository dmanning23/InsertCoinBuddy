using System;

namespace InsertCoinBuddy
{
	public class CoinEventArgs : EventArgs
	{
		public int PlayerIndex { get; set; }

		public CoinEventArgs(int playerIndex)
		{
			PlayerIndex = playerIndex;
		}
	}
}
