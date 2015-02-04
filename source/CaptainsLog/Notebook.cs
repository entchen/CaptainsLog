﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using KSP;

namespace SCANsatNotebook
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	public class Notebook : MonoBehaviour
	{
		public Notebook ()
		{
			NotebookUtil.NotebookLog ("Notebook created");
		}
	}
}

