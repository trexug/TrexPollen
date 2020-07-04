using System;
using System.Collections.Generic;

namespace TrexPollen.Integrations.Dmi
{
	public class PollenReport
	{
		public DateTime Time { get; }
		public IReadOnlyDictionary<string, PollenReportRegion> Regions { get; }

		public PollenReport(DateTime time, IReadOnlyDictionary<string, PollenReportRegion> regions)
		{
			Time = time;
			Regions = regions;
		}
	}
}
