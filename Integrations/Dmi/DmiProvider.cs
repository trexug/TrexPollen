using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TrexPollen.Integrations.Dmi.ProviderModel;

namespace TrexPollen.Integrations.Dmi
{
	public class DmiProvider
	{
		public RestClient RestClient { get; }
		public string BaseUrl { get; }
		public string RequestUrl { get; }
		public DmiProvider(string baseUrl, string requestUrl)
		{
			BaseUrl = baseUrl;
			RequestUrl = requestUrl;
			RestClient = new RestClient(baseUrl);
		}

		private static PollenReport ToReport(PollenForecast pollenForecast)
		{
			using (StringReader reader = new StringReader(pollenForecast.Products.Text))
			{
				HtmlDocument document = new HtmlDocument();
				document.Load(reader);
				var pollenInfo = document.DocumentNode
					.SingleChild("pollen_info");
				var regions = pollenInfo
					.Children("region").Select(r => ParseRegion(r))
					.ToDictionary(r => r.Name);
				var time = ParseFileInfo(pollenInfo.SingleChild("file_info").SingleChild("date_UTC"));
				return new PollenReport(time, new ReadOnlyDictionary<string, PollenReportRegion>(regions));
			}
		}

		private static DateTime ParseFileInfo(HtmlNode utc)
		{
			int day = utc.SingleChild("day").InnerTextToInt();
			int month = utc.SingleChild("month").InnerTextToInt();
			int year = utc.SingleChild("year").InnerTextToInt();
			TimeSpan timeOfDay = TimeSpan.Parse(utc.SingleChild("time").InnerText);
			return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc) + timeOfDay;
		}

		private static PollenReportRegion ParseRegion(HtmlNode regionNode)
		{
			string name = regionNode.SingleChild("name").InnerText;
			Dictionary<string, int> readings = new Dictionary<string, int>(ParseReadings(regionNode.SingleChild("readings")));
			return new PollenReportRegion(name, new ReadOnlyDictionary<string, int>(readings));
		}

		private static IEnumerable<KeyValuePair<string, int>> ParseReadings(HtmlNode readingsNode)
		{
			return readingsNode.Children("reading")
				.Select(r => ParseReading(r));
		}

		private static KeyValuePair<string, int> ParseReading(HtmlNode readingNode)
		{
			return new KeyValuePair<string, int>(readingNode.SingleChild("name").InnerText, int.TryParse(readingNode.SingleChild("value").InnerText, out int value) ? value : 0);
		}

		public async Task<PollenReport> GetPollenReportAsync()
		{
			IRestRequest request = new RestRequest(RequestUrl, Method.GET);
			var forecast = await RestClient.GetAsync<List<PollenForecast>>(request);
			return ToReport(forecast.First());
		}
	}
}
