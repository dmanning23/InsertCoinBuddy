using NUnit.Framework;

namespace InsertCoinBuddy.Tests
{
	[TestFixture]
	public class TextTests
	{
		IInsertCoinService credits;
		InsertCoinScreen _screen;

		[SetUp]
		public void Setup()
		{
			credits = new InsertCoinService(2, 2);
			_screen = new InsertCoinScreen("", "", "", "")
			{
				Service = credits
			};
		}

		[Test]
		public void initialText()
		{
			Assert.AreEqual("Credits: 0/2", _screen.NumCreditsText(credits.Players[0]));
		}

		[Test]
		public void addCoinText()
		{
			credits.Players[0].AddCoin();
			Assert.AreEqual("Credits: 1/2", _screen.NumCreditsText(credits.Players[0]));
		}

		[Test]
		public void add2CoinText()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			Assert.AreEqual("Credits: 1", _screen.NumCreditsText(credits.Players[0]));
		}

		[Test]
		public void add3CoinText()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			Assert.AreEqual("Credits: 1 1/2", _screen.NumCreditsText(credits.Players[0]));
		}

		[Test]
		public void changeCoinsPerCredit()
		{
			credits.CoinsPerCredit = 0;
			Assert.AreEqual("Free Play", _screen.NumCreditsText(credits.Players[0]));
		}

		[Test]
		public void add3CoinText_change()
		{
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();
			credits.Players[0].AddCoin();

			credits.CoinsPerCredit = 3;

			Assert.AreEqual("Credits: 1", _screen.NumCreditsText(credits.Players[0]));
		}

	}
}
