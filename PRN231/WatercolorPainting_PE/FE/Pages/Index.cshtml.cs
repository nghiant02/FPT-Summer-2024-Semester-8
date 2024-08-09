using BusinessObject.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IApi _api;
        [BindProperty] public IEnumerable<WatercolorsPaintingResponseDto> Paintings { get; set; }
        public IndexModel(IApi api)
        {
            _api = api;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Login");
            }


            var paintings = await _api.GetWatercolorPaintings();

            Paintings = paintings;

            return Page();
        }
    }
}
