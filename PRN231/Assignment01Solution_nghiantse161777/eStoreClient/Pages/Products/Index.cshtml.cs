using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessObject.DTO;

namespace eStoreClient.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IList<ProductDTO> Products { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            var response = await client.GetAsync("http://localhost:5145/api/Product");

            if (response.IsSuccessStatusCode)
            {
                Products = await response.Content.ReadFromJsonAsync<IList<ProductDTO>>();
            }
        }
    }
}
