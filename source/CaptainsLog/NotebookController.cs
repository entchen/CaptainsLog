﻿using System;
using System.Collections.Generic;
using SCANsat;
using SCANsat.SCAN_Data;
using KSP.IO;
using File = KSP.IO.File;

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
					SaveBody (body);
				}
			}
		}

		internal void SaveBody(CelestialBody body)
		{
			Boolean persist = false;
			ConfigNode node = null;

			if (body == null)
			{
				NotebookUtil.NotebookLog ("  -- no body for: {0}: '{1}'", new Object[] { name, body });
			}
			else
			{
				SCANdata data = SCANUtil.getData (body);
				NotebookUtil.NotebookLog ("  -- data - {0}", data);

				if (data == null)
				{
					NotebookUtil.NotebookLog ("    -- no data for: {0}", body.name);
				}
				else
				{
					node = new ConfigNode (body.name);

					ConfigNode innerNode = new ConfigNode ();
					innerNode.AddValue ("body", body.name);
					node.AddNode (innerNode);

					SCANanomaly[] anomalies = data.Anomalies;
					if (anomalies == null)
					{
						NotebookUtil.NotebookLog ("      -- no anomalies for: {0}", body.name);
					}
					else
					{
						NotebookUtil.NotebookLog ("      -- {1} anomalies for: {0}", new Object[] { body.name, anomalies.Length });
						// NotebookUtil.NotebookLog ("Body {0}: {1} known anomalies", new object[2] { body.bodyName, anomalies.Length});
						for (int i = 0; i < anomalies.Length; i++)
						{
							SCANanomaly anomaly = anomalies [i];

							Boolean show = anomaly.Known;
							// TODO this is a flag dependent on debug level...
							show = true;

							if (show)
							{
								ConfigNode anomalyNode = new ConfigNode ();
								anomalyNode.AddValue ("name", anomaly.Name);
								anomalyNode.AddValue ("known", anomaly.Known.ToString());
								anomalyNode.AddValue ("detail", anomaly.Detail.ToString());
								anomalyNode.AddValue ("lon", anomaly.Longitude.ToString());
								anomalyNode.AddValue ("lat", anomaly.Latitude.ToString());

								innerNode.AddNode (anomalyNode);

								// We have something to show, let's save it...
								persist = true;
							}
						}
					}
				}
			}

			// TODO make this save-force switchable
			persist = true;

			if (persist)
			{
				if (node != null)
				{
					// IOUtils.
					string saveFolder = GetRootPath() + "/Saves/" + HighLogic.SaveFolder + "/";
					node.Save (saveFolder + "/" + "Discoveries-" + node.name + ".cfg");
				}
			}
		}

		internal static String GetRootPath()
		{
			// TODO is-mock flag somewhere here...
			String path = KSPUtil.ApplicationRootPath;

			path = path.Replace("\\", "/");
			if (path.EndsWith ("/"))
			{
				path = path.Substring (0, path.Length - 1);
			}

			return path;
		}
	}
}

