using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eStoreClient.Pages.OrderDetails
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public OrderDetailCreateUpdateDTO OrderDetail { get; set; }

        public IActionResult OnGet(int orderId)
        {
            OrderDetail = new OrderDetailCreateUpdateDTO { OrderId = orderId };
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
            var response = await client.PostAsync("http://localhost:5145/api/OrderDetail", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index", new { orderId = OrderDetail.OrderId });
            }

            return Page();
        }
    }
}
