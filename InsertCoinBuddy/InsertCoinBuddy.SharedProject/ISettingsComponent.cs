using Microsoft.Xna.Framework;
using System;

namespace InsertCoinBuddy
{
	public interface ISettingsComponent : IGameComponent
	{
		Settings Settings { get; }

		void SaveSettings();
	}
}