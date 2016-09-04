using Microsoft.Xna.Framework;

namespace InsertCoinBuddy
{
	public interface ISettingsComponent : IGameComponent
	{
		Settings Settings { get; }

		void SaveSettings();
	}
}