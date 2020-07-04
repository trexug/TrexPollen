using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrexPollen.Integrations.Dmi.ProviderModel
{
	public class PollenForecast
	{
		public string Name { get; set; }
		public string Language { get; set; }
		public PollenForecastProducts Products { get; set; }
	}
}
