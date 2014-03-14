using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace InsertCoinBuddy
{
	public interface ICreditsManager
	{
		/// <summary>
		/// Gets or sets the coins per credit.
		/// </summary>
		/// <value>The coins per credit.</value>
		int CoinsPerCredit { get; set; }
	}
}

