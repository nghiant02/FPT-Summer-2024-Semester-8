using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessObject.DTO;

namespace eStoreClient.Pages.Reports
{
    public class SalesReportModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public SalesReportModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IList<SalesReportDTO> SalesReport { get; set; }

        public async Task OnGetAsync(string startDate, string endDate)
        {
            if (string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
            {
                SalesReport = new List<SalesReportDTO>();
                return;
            }

            var client = _clientFactory.CreateClient("eStoreClient");
            var response = await client.GetAsync($"http://localhost:5145/api/Order/Report?startDate={startDate}&endDate={endDate}");

            if (response.IsSuccessStatusCode)
            {
                SalesReport = await response.Content.ReadFromJsonAsync<IList<SalesReportDTO>>();
            }
            else
            {
                SalesReport = new List<SalesReportDTO>();
            }
        }
    }
}
