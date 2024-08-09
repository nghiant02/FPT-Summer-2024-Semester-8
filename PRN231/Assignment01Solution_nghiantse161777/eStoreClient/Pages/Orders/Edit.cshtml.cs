using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eStoreClient.Pages.Orders
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public OrderCreateUpdateDTO Order { get; set; }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            OrderId = id;
            var client = _clientFactory.CreateClient("eStoreClient");
            var order = await client.GetFromJsonAsync<OrderDTO>($"http://localhost:5145/api/Order/{id}");

            if (order == null)
            {
                return NotFound();
            }

            Order = new OrderCreateUpdateDTO
            {
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                Freight = order.Freight
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("eStoreClient");
            var json = JsonConvert.SerializeObject(Order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"http://localhost:5145/api/Order/{OrderId}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
