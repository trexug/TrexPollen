using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrexPollen.Integrations.Dmi.ProviderModel
{
	public class PollenForecastProducts
	{
		[JsonConverter(typeof(UTCDateTimeConverter))]
		public DateTime Timestamp { get; set; }
		[JsonProperty("text_type")]
		public string TextType { get; set; }
		[JsonProperty("text_format")]
		public string TextFormat { get; set; }
		public string Text { get; set; }
		public string VarnishUrl { get; set; }
	}
}
