using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eStoreClient.Pages.OrderDetails
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public OrderDetailCreateUpdateDTO OrderDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId, int productId)
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            OrderDetail = await client.GetFromJsonAsync<OrderDetailCreateUpdateDTO>($"http://localhost:5145/api/OrderDetail/{orderId}/{productId}");

            if (OrderDetail == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("eStoreClient");
            var json = JsonConvert.SerializeObject(OrderDetail);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"http://localhost:5145/api/OrderDetail/{OrderDetail.OrderId}/{OrderDetail.ProductId}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index", new { orderId = OrderDetail.OrderId });
            }

            return Page();
        }
    }
}
