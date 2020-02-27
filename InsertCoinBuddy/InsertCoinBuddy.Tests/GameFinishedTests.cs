using InsertCoinBuddy;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertCoinBuddy.Tests
{
	[TestFixture]
	public class GameFinishedTests
	{
		IInsertCoinService credits;

		[SetUp]
		public void Setup()
		{
			credits = new InsertCoinService(2, 2);
		}

		//TODO: fix tests

		//[Test]
		//public void done_goto_menus()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	credits.GameFinished();

		//	Assert.AreEqual(GameState.Menu, credits.CurrentGameState);
		//}

		//[Test]
		//public void done_notplaying()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	credits.GameFinished();

		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.One));
		//}

		//[Test]
		//public void done_notReady()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	credits.GameFinished();

		//	Assert.IsFalse(credits.IsReady(PlayerIndex.One));
		//}
	}
}
