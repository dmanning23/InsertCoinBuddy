using SQLite.Net.Attributes;

namespace InsertCoinBuddy
{
	/// <summary>
	/// This is a class to hold the operator settings for a game
	/// </summary>
	[Table("Settings")]
	public class Settings
	{
		#region Properties

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int CoinsPerCredit { get; set; }

		public int Difficulty { get; set; }

		public bool AttractModeSound { get; set; }

		public bool AttractModeMusic { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// hello standard constructor!
		/// </summary>
		public Settings()
		{
			CoinsPerCredit = 2;
			Difficulty = 1;
			AttractModeSound = true;
			AttractModeMusic = false;
		}

		#endregion //Methods
	}
}