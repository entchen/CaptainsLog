using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SCANsatNotebook
{
	public class NotebookUtil
	{
		public NotebookUtil ()
		{
		}

		public static void NotebookLog(string log, params object[] stringObjects)
		{
			log = string.Format(log, stringObjects);
			string finalLog = string.Format("[SCANsat-Notebook] {0}", log);
			Debug.Log(finalLog);
		}
	}
}

