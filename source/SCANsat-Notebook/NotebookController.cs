using System;
using SCANsat;

namespace SCANsatNotebook
{
	[KSPScenario(ScenarioCreationOptions.AddToAllGames | ScenarioCreationOptions.AddToExistingGames, GameScenes.FLIGHT, GameScenes.SPACECENTER, GameScenes.TRACKSTATION)]
	public class NotebookController : ScenarioModule
	{
		internal string[] body_names = new string[] { "Kerbin", "Mun", "Minmus" };

		public NotebookController ()
		{
		}

		public override void OnLoad(ConfigNode node)
		{
			NotebookUtil.NotebookLog ("OnLoad called");
		}

		public override void OnSave(ConfigNode node)
		{
			NotebookUtil.NotebookLog ("OnSave called");

			foreach (string body_name in body_names)
			{
				SaveBody (body_name);
			}
		}

		internal void SaveBody(string name)
		{
			NotebookUtil.NotebookLog ("Checking {0}", name);

			CelestialBody body = new CelestialBody ();
			body.bodyName = name;

			NotebookUtil.NotebookLog ("Created Celestial Body {0}", name);
			NotebookUtil.NotebookLog ("  -- {0}", body.bodyName);
			// SCANdata data = SCANUtil.getData (body);
			//
			// SCANdata.SCANanomaly[] anomalies = data.Anomalies;
			// if (anomalies == null)
			// {
				// NotebookUtil.NotebookLog ("No known anomalies for: {0}", body.bodyName);
			// }
			// else
			// {
				// NotebookUtil.NotebookLog ("Body {0}: {1} known anomalies", new object[2] { body.bodyName, anomalies.Length});
				// for (int i = 0; i < anomalies.Length; i++)
				// {
					// SCANdata.SCANanomaly anomaly = anomalies [i];
					// internal bool known;
					// internal bool detail;
					// internal string name;
					// internal double longitude;
					// internal double latitude;
					// internal PQSMod mod;
					// NotebookUtil.NotebookLog ("    name:   {0}", anomaly.Name);
					// NotebookUtil.NotebookLog ("    known:  {0}", anomaly.Known);
					// NotebookUtil.NotebookLog ("    detail: {0}", anomaly.Detail);
					// NotebookUtil.NotebookLog ("    lon:    {0}", anomaly.Longitude);
					// NotebookUtil.NotebookLog ("    lat:    {0}", anomaly.Latitude);
				// }
			// }
		}
	}
}

