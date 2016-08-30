namespace InsertCoinBuddy
{
	public interface ISettingsComponent
	{
		Settings Settings { get; }

		void SaveSettings();
	}
}