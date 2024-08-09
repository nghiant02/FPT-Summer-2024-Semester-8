using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStoreClient.Pages.OrderDetails
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public OrderDetailDTO OrderDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId, int productId)
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            OrderDetail = await client.GetFromJsonAsync<OrderDetailDTO>($"http://localhost:5145/api/OrderDetail/{orderId}/{productId}");

            if (OrderDetail == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            var response = await client.DeleteAsync($"http://localhost:5145/api/OrderDetail/{OrderDetail.OrderId}/{OrderDetail.ProductId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index", new { orderId = OrderDetail.OrderId });
            }

            return Page();
        }
    }
}
