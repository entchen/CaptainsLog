using System;
using System.Collections.Generic;
using SCANsat;
using SCANsat.SCAN_Data;

namespace SCANsatNotebook
{
	[KSPScenario(ScenarioCreationOptions.AddToAllGames | ScenarioCreationOptions.AddToExistingGames, GameScenes.FLIGHT, GameScenes.SPACECENTER, GameScenes.TRACKSTATION)]
	public class NotebookController : ScenarioModule
	{
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

			List<CelestialBody> bodies = FlightGlobals.Bodies;
			if (bodies == null)
			{
				NotebookUtil.NotebookLog ("No Celestial Bodies found...");
			}
			else
			{
				foreach (CelestialBody body in bodies)
				{
					NotebookUtil.NotebookLog ("  body - name: {0}", body.bodyName);
					NotebookUtil.NotebookLog ("  body - {0}", body);
					SaveBody (body);
				}
			}
		}

		internal void SaveBody(CelestialBody body)
		{
			if (body == null)
			{
				NotebookUtil.NotebookLog ("  -- no body for: {0}: '{1}'", new Object[] { name, body });
			}
			else
			{
				body.bodyName = name;
				NotebookUtil.NotebookLog ("  -- body for: {0}: '{1}'", new Object[] { name, body });

				SCANdata data = SCANUtil.getData (body);
				NotebookUtil.NotebookLog ("  -- data - {0}", data);

				if (data == null)
				{
					NotebookUtil.NotebookLog ("    -- no data for: {0}", body.bodyName);
				}
				else
				{
					NotebookUtil.NotebookLog ("    -- data for: {0}: '{1}'", new Object[] { body.bodyName, data });
					SCANanomaly[] anomalies = data.Anomalies;
					if (anomalies == null)
					{
						NotebookUtil.NotebookLog ("      -- no anomalies for: {0}", body.bodyName);
					}
					else
					{
						NotebookUtil.NotebookLog ("      -- {1} anomalies for: {0}", new Object[] { body.bodyName, anomalies.Length });
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
					}
				}
			}
		}
	}
}

