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
	public class JoinGameTests
	{
		IInsertCoinService credits;

		[SetUp]
		public void Setup()
		{
			credits = new InsertCoinService(2, 2);
		}

		//TODO: fix tests

		//[Test]
		//public void noCredits_join_result()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);
		//	Assert.IsFalse(result);
		//}

		//[Test]
		//public void noCredits_join_notPlaying()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);
		//	Assert.IsFalse(credits.Players[0].Current);
		//}

		//[Test]
		//public void freeplay_join()
		//{
		//	credits.CoinsPerCredit = 0;
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);
		//	Assert.IsTrue(result);
		//}

		//[Test]
		//public void freeplay_join_playing()
		//{
		//	credits.CoinsPerCredit = 0;
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);
		//	Assert.IsTrue(credits.Players[0].Current);
		//}

		//[Test]
		//public void credits_join_result()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);
		//	Assert.IsTrue(result);
		//}

		//[Test]
		//public void credits_join_Playing()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);
		//	Assert.IsTrue(credits.Players[0].Current);
		//}

		//[Test]
		//public void credits_join_correctNumCredits_1()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);

		//	Assert.AreEqual(1, credits.Players[0].NumCredits);
		//}

		//[Test]
		//public void credits_join_correctNumCredits_2()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);

		//	Assert.AreEqual(2, credits.Players[0].NumCredits);
		//}

		//[Test]
		//public void credits_join_p1()
		//{
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	credits.Players[0].PlayerButtonPressed(PlayerIndex.One);

		//	credits.Players[0].AddCoin();
		//	credits.Players[0].AddCoin();
		//	var result = credits.Players[0].JoinGame(PlayerIndex.Two, false);
		//	Assert.IsTrue(credits.Players[0].Current);
		//}
	}
}
