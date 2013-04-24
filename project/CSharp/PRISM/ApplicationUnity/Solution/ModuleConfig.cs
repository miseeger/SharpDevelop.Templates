﻿using System;
using Microsoft.Practices.Prism.Modularity;

namespace ${SolutionName}
{

	public class ModuleConfig
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }
		public ModuleInfo Module { get; set; }
	}

}
