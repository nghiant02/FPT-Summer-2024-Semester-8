using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStoreClient.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IList<OrderDTO> Orders { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            Orders = await client.GetFromJsonAsync<IList<OrderDTO>>("http://localhost:5145/api/Order");
        }
    }
}
