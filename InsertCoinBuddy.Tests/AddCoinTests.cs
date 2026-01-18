using NUnit.Framework;

namespace InsertCoinBuddy.Tests
{
	[TestFixture]
	public class AddCoinTests
	{
		IInsertCoinService credits;

		[SetUp]
		public void Setup()
		{
			credits = new InsertCoinService(2, 2);
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

			Assert.AreEqual(0, credits.Players[0].TotalCoins);
			Assert.AreEqual(0, credits.Players[0].NumCredits);
			Assert.AreEqual(0, credits.Players[0].NumCoins);
			Assert.AreEqual(0, credits.Players[0].NumCoinsNeededForNextCredit);
			Assert.IsTrue(credits.Players[0].CreditAvailable);
		}

		[Test]
		public void default_TotalCoins()
		{
			Assert.AreEqual(0, credits.Players[0].TotalCoins);
		}

		[Test]
		public void default_NumCredits()
		{
			Assert.AreEqual(0, credits.Players[0].NumCredits);
		}

		[Test]
		public void default_NumCoins()
		{
			Assert.AreEqual(0, credits.Players[0].NumCoins);
		}

		[Test]
		public void default_NumCoinsNeededForNextCredit()
		{
			Assert.AreEqual(2, credits.Players[0].NumCoinsNeededForNextCredit);
		}

		[Test]
		public void addOneCoin()
		{
			credits.Players[0].AddCoin();

			Assert.AreEqual(1, credits.Players[0].TotalCoins);
			Assert.AreEqual(0, credits.Players[0].NumCredits);
			Assert.AreEqual(1, credits.Players[0].NumCoins);
			Assert.AreEqual(1, credits.Players[0].NumCoinsNeededForNextCredit);
			Assert.IsFalse(credits.Players[0].CreditAvailable);
		}

		[Test]
		public void addTwoCoins()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			Assert.AreEqual(2, credits.Players[0].TotalCoins);
			Assert.AreEqual(1, credits.Players[0].NumCredits);
			Assert.AreEqual(0, credits.Players[0].NumCoins);
			Assert.AreEqual(2, credits.Players[0].NumCoinsNeededForNextCredit);
			Assert.IsTrue(credits.Players[0].CreditAvailable);
		}

		[Test]
		public void addThreeCoins()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			Assert.AreEqual(3, credits.Players[0].TotalCoins);
			Assert.AreEqual(1, credits.Players[0].NumCredits);
			Assert.AreEqual(1, credits.Players[0].NumCoins);
			Assert.AreEqual(1, credits.Players[0].NumCoinsNeededForNextCredit);
			Assert.IsTrue(credits.Players[0].CreditAvailable);
		}

		[Test]
		public void addFourCoins_TotalCoins()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			Assert.AreEqual(4, credits.Players[0].TotalCoins);
		}

		[Test]
		public void addFourCoins_NumCredits()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			Assert.AreEqual(2, credits.Players[0].NumCredits);
		}

		[Test]
		public void addFourCoins_NumCoins()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			Assert.AreEqual(0, credits.Players[1].NumCoins);
		}

		[Test]
		public void addFourCoins_NumCoinsNeededForNextCredit()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			Assert.AreEqual(2, credits.Players[1].NumCoinsNeededForNextCredit);
		}

		[Test]
		public void addFourCoins_CreditAvailable()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			Assert.IsTrue(credits.Players[0].CreditAvailable);
		}

		[Test]
		public void oddNumberCoins()
		{
			credits.CoinsPerCredit = 3;

			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			Assert.AreEqual(4, credits.Players[0].TotalCoins);
			Assert.AreEqual(1, credits.Players[0].NumCredits);
			Assert.AreEqual(1, credits.Players[0].NumCoins);
			Assert.AreEqual(2, credits.Players[0].NumCoinsNeededForNextCredit);
			Assert.IsTrue(credits.Players[0].CreditAvailable);
		}
	}
}
