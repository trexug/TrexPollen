using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrexPollen.Integrations.Dmi
{
	public class PollenReportRegion
	{
		public string Name { get; set; }
		public IReadOnlyDictionary<string, int> Readings { get; }

		public PollenReportRegion(string name, IReadOnlyDictionary<string, int> readings)
		{
			Name = name;
			Readings = readings;
		}
	}
}
