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
	public class MenuHitStartTests
	{
		IInsertCoinService credits;

		[SetUp]
		public void Setup()
		{
			credits = new InsertCoinService(2, 2);
		}

		[Test]
		public void default_gameState()
		{
			Assert.AreEqual(GameState.Menu, credits.CurrentGameState);
		}

		//TODO: fix tests

		//[Test]
		//public void noCredits_tryToStart()
		//{
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	Assert.AreEqual(GameState.Menu, credits.CurrentGameState);
		//}

		//[Test]
		//public void onePlayerStart_numCredits()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.AreEqual(0, credits.TotalCoins);
		//	Assert.AreEqual(0, credits.NumCredits);
		//	Assert.AreEqual(0, credits.NumCoins);
		//	Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		//}

		//[Test]
		//public void onePlayerStart_gameState()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.AreEqual(GameState.Playing, credits.CurrentGameState);
		//}

		//[Test]
		//public void onePlayerStart_readyness()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.IsFalse(credits.IsReady(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Four));
		//}

		//[Test]
		//public void onePlayerStart_isPlaying()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.IsTrue(credits.IsPlaying(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Four));
		//}

		//[Test]
		//public void extracred_onePlayerStart_numCredits()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.AreEqual(2, credits.TotalCoins);
		//	Assert.AreEqual(1, credits.NumCredits);
		//	Assert.AreEqual(0, credits.NumCoins);
		//	Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		//}

		//[Test]
		//public void extracred_onePlayerStart_gameState()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.AreEqual(GameState.Ready, credits.CurrentGameState);
		//}

		//[Test]
		//public void extracred_onePlayerStart_readyness()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.IsTrue(credits.IsReady(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Four));
		//}

		//[Test]
		//public void extracred_onePlayerStart_isPlaying()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Four));
		//}

		//[Test]
		//public void dontWait_onePlayerStart_numCredits()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.AreEqual(2, credits.TotalCoins);
		//	Assert.AreEqual(1, credits.NumCredits);
		//	Assert.AreEqual(0, credits.NumCoins);
		//	Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		//}

		//[Test]
		//public void dontWait_onePlayerStartTwice_numCredits()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.AreEqual(2, credits.TotalCoins);
		//	Assert.AreEqual(1, credits.NumCredits);
		//	Assert.AreEqual(0, credits.NumCoins);
		//	Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		//}

		//[Test]
		//public void dontWait_onePlayerStart_gameState()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.AreEqual(GameState.Playing, credits.CurrentGameState);
		//}

		//[Test]
		//public void dontWait_onePlayerStart_readyness()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.IsFalse(credits.IsReady(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Four));
		//}

		//[Test]
		//public void dontWait_onePlayerStart_isPlaying()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.One);

		//	Assert.IsTrue(credits.IsPlaying(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Four));
		//}

		//[Test]
		//public void twoPlayerStart_numCredits()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.AreEqual(0, credits.TotalCoins);
		//	Assert.AreEqual(0, credits.NumCredits);
		//	Assert.AreEqual(0, credits.NumCoins);
		//	Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		//}

		//[Test]
		//public void twoPlayerStart_gameState()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.AreEqual(GameState.Playing, credits.CurrentGameState);
		//}

		//[Test]
		//public void twoPlayerStart_readyness()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.IsFalse(credits.IsReady(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Four));
		//}

		//[Test]
		//public void twoPlayerStart_isPlaying()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.IsTrue(credits.IsPlaying(PlayerIndex.One));
		//	Assert.IsTrue(credits.IsPlaying(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Four));
		//}

		//[Test]
		//public void extracred_twoPlayerStart_numCredits()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.AreEqual(2, credits.TotalCoins);
		//	Assert.AreEqual(1, credits.NumCredits);
		//	Assert.AreEqual(0, credits.NumCoins);
		//	Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		//}

		//[Test]
		//public void extracred_twoPlayerStart_gameState()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.AreEqual(GameState.Ready, credits.CurrentGameState);
		//}

		//[Test]
		//public void extracred_twoPlayerStart_readyness()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.IsTrue(credits.IsReady(PlayerIndex.One));
		//	Assert.IsTrue(credits.IsReady(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Four));
		//}

		//[Test]
		//public void extracred_twoPlayerStart_isPlaying()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Four));
		//}

		//[Test]
		//public void dontWait_twoPlayerStart_numCredits()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.AreEqual(2, credits.TotalCoins);
		//	Assert.AreEqual(1, credits.NumCredits);
		//	Assert.AreEqual(0, credits.NumCoins);
		//	Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		//}

		//[Test]
		//public void dontWait_twoPlayerStartTwice_numCredits()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.AreEqual(2, credits.TotalCoins);
		//	Assert.AreEqual(1, credits.NumCredits);
		//	Assert.AreEqual(0, credits.NumCoins);
		//	Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		//}

		//[Test]
		//public void dontWait_twoPlayerStart_gameState()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.AreEqual(GameState.Playing, credits.CurrentGameState);
		//}

		//[Test]
		//public void dontWait_twoPlayerStart_readyness()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.IsFalse(credits.IsReady(PlayerIndex.One));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsReady(PlayerIndex.Four));
		//}

		//[Test]
		//public void dontWait_twoPlayerStart_isPlaying()
		//{
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.AddCoin();
		//	credits.PlayerButtonPressed(PlayerIndex.One);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);
		//	credits.PlayerButtonPressed(PlayerIndex.Two);

		//	Assert.IsTrue(credits.IsPlaying(PlayerIndex.One));
		//	Assert.IsTrue(credits.IsPlaying(PlayerIndex.Two));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Three));
		//	Assert.IsFalse(credits.IsPlaying(PlayerIndex.Four));
		//}
	}
}
