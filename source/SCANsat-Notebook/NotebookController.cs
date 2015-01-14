using System;

namespace SCANsatNotebook
{
	[KSPScenario(ScenarioCreationOptions.AddToAllGames | ScenarioCreationOptions.AddToExistingGames, GameScenes.FLIGHT, GameScenes.SPACECENTER, GameScenes.TRACKSTATION)]
	public class NotebookController
	{
		public NotebookController ()
		{
		}

		public override void OnSave(ConfigNode node)
		{
		}
	}
}

