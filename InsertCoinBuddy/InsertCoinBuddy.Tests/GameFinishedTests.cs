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
		IInsertCoinComponent credits;

		[SetUp]
		public void Setup()
		{
			credits = new CreditManager("", "", 2);
		}

		[Test]
		public void done_goto_menus()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			credits.GameFinished();

			Assert.AreEqual(GameState.Menu, credits.CurrentGameState);
		}

		[Test]
		public void done_notplaying()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			credits.GameFinished();

			Assert.IsFalse(credits.IsPlaying(PlayerIndex.One));
		}

		[Test]
		public void done_notReady()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.PlayerButtonPressed(PlayerIndex.One);

			credits.GameFinished();

			Assert.IsFalse(credits.IsReady(PlayerIndex.One));
		}
	}
}
