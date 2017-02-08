using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertCoinBuddy.Tests
{
	[TestFixture]
	public class TextTests
	{
		IInsertCoinComponent credits;
		InsertCoinScreen _screen;

		[SetUp]
		public void Setup()
		{
			credits = new CreditManager("", "", 2);
			_screen = new InsertCoinScreen("", "", credits);
		}

		[Test]
		public void initialText()
		{
			Assert.AreEqual("Credits: 0/2", _screen.CreditsText);
		}

		[Test]
		public void addCoinText()
		{
			credits.AddCoin();
			Assert.AreEqual("Credits: 1/2", _screen.CreditsText);
		}

		[Test]
		public void add2CoinText()
		{
			credits.AddCoin();
			credits.AddCoin();
			Assert.AreEqual("Credits: 1", _screen.CreditsText);
		}

		[Test]
		public void add3CoinText()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();
			Assert.AreEqual("Credits: 1 1/2", _screen.CreditsText);
		}

		[Test]
		public void changeCoinsPerCredit()
		{
			credits.CoinsPerCredit = 0;
			Assert.AreEqual("Free Play", _screen.CreditsText);
		}

		[Test]
		public void add3CoinText_change()
		{
			credits.AddCoin();
			credits.AddCoin();
			credits.AddCoin();

			credits.CoinsPerCredit = 3;

			Assert.AreEqual("Credits: 1", _screen.CreditsText);
		}

	}
}
