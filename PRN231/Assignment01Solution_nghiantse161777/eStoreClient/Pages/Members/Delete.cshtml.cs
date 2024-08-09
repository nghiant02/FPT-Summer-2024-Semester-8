using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BusinessObject.DTO;

namespace eStoreClient.Pages.Members
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public MemberDTO Member { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            var response = await client.GetAsync($"http://localhost:5145/api/Member/{id}");

            if (response.IsSuccessStatusCode)
            {
                Member = await response.Content.ReadFromJsonAsync<MemberDTO>();
                return Page();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var client = _clientFactory.CreateClient("eStoreClient");
            var response = await client.DeleteAsync($"http://localhost:5145/api/Member/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
