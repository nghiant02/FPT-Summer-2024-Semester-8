using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessObject.DTO;

namespace eStoreClient.Pages.Members
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public MemberCreateUpdateDTO Member { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            var response = await client.GetAsync($"http://localhost:5145/api/Member/{id}");

            if (response.IsSuccessStatusCode)
            {
                Member = await response.Content.ReadFromJsonAsync<MemberCreateUpdateDTO>();
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("eStoreClient");
            var response = await client.PutAsJsonAsync($"http://localhost:5145/api/Member/{id}", Member);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
