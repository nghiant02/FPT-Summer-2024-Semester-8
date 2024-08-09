using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessObject.DTO;

namespace eStoreClient.Pages.OrderDetails
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IList<OrderDetailDTO> OrderDetails { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            var response = await client.GetAsync("http://localhost:5145/api/OrderDetail");

            if (response.IsSuccessStatusCode)
            {
                OrderDetails = await response.Content.ReadFromJsonAsync<IList<OrderDetailDTO>>();
            }
        }
    }
}
