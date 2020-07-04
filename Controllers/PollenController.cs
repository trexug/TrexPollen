using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TrexPollen.Integrations.Dmi;

namespace TrexPollen.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class PollenController : ControllerBase
	{
		private readonly ILogger<PollenController> _logger;
		private DmiProvider DmiProvider { get; }

		public PollenController(ILogger<PollenController> logger)
		{
			DmiProvider = new DmiProvider("https://www.dmi.dk", "/dmidk_byvejrWS/rest/texts/forecast/pollen/Danmark");
			_logger = logger;
		}

		[HttpGet]
		public async Task<PollenReport> GetAsync() => await DmiProvider.GetPollenReportAsync();
	}
}
