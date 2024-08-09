using BusinessObject.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FE.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginDto? login { get; set;}
        private readonly IApi _api;
        public LoginModel(IApi api)
        {
            _api = api;
        }
       public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _api.LoginToken(login);

            HttpContext.Session.SetString("JWTToken", response);
            return RedirectToPage("/Index");
        }
    }
}