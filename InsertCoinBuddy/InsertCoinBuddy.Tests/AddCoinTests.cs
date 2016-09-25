using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace InsertCoinBuddy.Tests
{
	[TestFixture]
	public class AddCoinTests
	{
		IInsertCoinComponent credits;

		[SetUp]
		public void Setup()
		{
			credits = new CreditManager("", "", 2);
		}

		[Test]
		public void default_CoinsPerCredit()
		{
			Assert.AreEqual(2, credits.CoinsPerCredit);
		}

		[Test]
		public void changeCoinsPerCredit()
		{
			credits.CoinsPerCredit = 3;
			Assert.AreEqual(3, credits.CoinsPerCredit);
		}

		[Test]
		public void notFreeplay()
		{
			Assert.IsFalse(credits.FreePlay);
		}

		[Test]
		public void freeplay()
		{
			credits.CoinsPerCredit = 0;
			Assert.IsTrue(credits.FreePlay);
		}

		[Test]
		public void allFreeplay()
		{
			credits.CoinsPerCredit = 0;

			Assert.AreEqual(0, credits.TotalCoins);
			Assert.AreEqual(0, credits.NumCredits);
			Assert.AreEqual(0, credits.NumCoins);
			Assert.AreEqual(0, credits.NumCoinsNeededForNextCredit);
			Assert.IsTrue(credits.CreditAvailable);
		}

		[Test]
		public void default_TotalCoins()
		{
			Assert.AreEqual(0, credits.TotalCoins);
		}

		[Test]
		public void default_NumCredits()
		{
			Assert.AreEqual(0, credits.NumCredits);
		}

		[Test]
		public void default_NumCoins()
		{
			Assert.AreEqual(0, credits.NumCoins);
		}

		[Test]
		public void default_NumCoinsNeededForNextCredit()
		{
			Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		}

		[Test]
		public void addOneCoin()
		{
			credits.AddCoin();

			Assert.AreEqual(1, credits.TotalCoins);
			Assert.AreEqual(0, credits.NumCredits);
			Assert.AreEqual(1, credits.NumCoins);
			Assert.AreEqual(1, credits.NumCoinsNeededForNextCredit);
			Assert.IsFalse(credits.CreditAvailable);
		}

		[Test]
		public void addTwoCoins()
		{
			credits.AddCoin();
			credits.AddCoin();

			Assert.AreEqual(2, credits.TotalCoins);
			Assert.AreEqual(1, credits.NumCredits);
			Assert.AreEqual(0, credits.NumCoins);
			Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
			Assert.IsTrue(credits.CreditAvailable);
		}

		[Test]
		public void addThreeCoins()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();

			Assert.AreEqual(3, credits.TotalCoins);
			Assert.AreEqual(1, credits.NumCredits);
			Assert.AreEqual(1, credits.NumCoins);
			Assert.AreEqual(1, credits.NumCoinsNeededForNextCredit);
			Assert.IsTrue(credits.CreditAvailable);
		}

		[Test]
		public void addFourCoins_TotalCoins()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();

			Assert.AreEqual(4, credits.TotalCoins);
		}

		[Test]
		public void addFourCoins_NumCredits()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();

			Assert.AreEqual(2, credits.NumCredits);
		}

		[Test]
		public void addFourCoins_NumCoins()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();

			Assert.AreEqual(0, credits.NumCoins);
		}

		[Test]
		public void addFourCoins_NumCoinsNeededForNextCredit()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();

			Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
		}

		[Test]
		public void addFourCoins_CreditAvailable()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();

			Assert.IsTrue(credits.CreditAvailable);
		}

		[Test]
		public void oddNumberCoins()
		{
			credits.CoinsPerCredit = 3;

			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();

			Assert.AreEqual(4, credits.TotalCoins);
			Assert.AreEqual(1, credits.NumCredits);
			Assert.AreEqual(1, credits.NumCoins);
			Assert.AreEqual(2, credits.NumCoinsNeededForNextCredit);
			Assert.IsTrue(credits.CreditAvailable);
		}
	}
}
