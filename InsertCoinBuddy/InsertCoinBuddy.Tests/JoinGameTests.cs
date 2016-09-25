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
		IInsertCoinComponent credits;

		[SetUp]
		public void Setup()
		{
			credits = new CreditManager("", "", 2);
		}

		[Test]
		public void noCredits_join_result()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			var result = credits.JoinGame(PlayerIndex.Two, false);
			Assert.IsFalse(result);
		}

		[Test]
		public void noCredits_join_notPlaying()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			var result = credits.JoinGame(PlayerIndex.Two, false);
			Assert.IsFalse(credits.IsPlaying(PlayerIndex.Two));
		}

		[Test]
		public void freeplay_join()
		{
			credits.CoinsPerCredit = 0;
			credits.PlayerButtonPressed(PlayerIndex.One);

			var result = credits.JoinGame(PlayerIndex.Two, false);
			Assert.IsTrue(result);
		}

		[Test]
		public void freeplay_join_playing()
		{
			credits.CoinsPerCredit = 0;
			credits.PlayerButtonPressed(PlayerIndex.One);

			var result = credits.JoinGame(PlayerIndex.Two, false);
			Assert.IsTrue(credits.IsPlaying(PlayerIndex.Two));
		}

		[Test]
		public void credits_join_result()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			credits.AddCoin();
			credits.AddCoin();
			var result = credits.JoinGame(PlayerIndex.Two, false);
			Assert.IsTrue(result);
		}

		[Test]
		public void credits_join_Playing()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			credits.AddCoin();
			credits.AddCoin();
			var result = credits.JoinGame(PlayerIndex.Two, false);
			Assert.IsTrue(credits.IsPlaying(PlayerIndex.Two));
		}

		[Test]
		public void credits_join_correctNumCredits_1()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			var result = credits.JoinGame(PlayerIndex.Two, false);

			Assert.AreEqual(1, credits.NumCredits);
		}

		[Test]
		public void credits_join_correctNumCredits_2()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			var result = credits.JoinGame(PlayerIndex.Two, false);

			Assert.AreEqual(2, credits.NumCredits);
		}

		[Test]
		public void credits_join_p1()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			credits.AddCoin();
			credits.AddCoin();
			var result = credits.JoinGame(PlayerIndex.Two, false);
			Assert.IsTrue(credits.IsPlaying(PlayerIndex.One));
		}
	}
}
