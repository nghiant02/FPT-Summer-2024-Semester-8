using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;

namespace eStoreClient.Pages.Products
{
    public class SearchModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public SearchModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<ProductDTO> Products { get; set; }

        [BindProperty]
        public string ProductName { get; set; }

        [BindProperty]
        public decimal? UnitPrice { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            var token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.GetAsync($"http://localhost:5145/api/Product?productName={ProductName}&unitPrice={UnitPrice}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<List<ProductDTO>>(json);
            }
        }
    }
}
