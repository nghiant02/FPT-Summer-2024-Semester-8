using BusinessObject.Dto.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using Web.API_Interface;

namespace Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IBookStoreApi _bookStoreApi;

        public RegisterModel(IBookStoreApi bookStoreApi)
        {
            _bookStoreApi = bookStoreApi;
        }

        [BindProperty]
        public RegisterDto? Register { get; set; }
        [BindProperty]
        public List<string>? ErrorMessages { get; set; }
        [BindProperty]
        public string? SuccessMessage { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessages = new List<string>();
                foreach (var modelStateValue in ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        if (!string.IsNullOrEmpty(error.ErrorMessage))
                        {
                            ErrorMessages.Add(error.ErrorMessage);
                        }
                    }
                }
                return Page();
            }

            if (Register == null) return Page();

            try
            {
                var reponse = await _bookStoreApi.Register(Register);
                if (reponse.IsSuccess)
                {
                    SuccessMessage = "User registered successfully";
                }
                else
                {
                    ErrorMessages = new List<string> { reponse.Message };
                }
            }
            catch (Exception e)
            {
                ErrorMessages = new List<string> { e.Message };
                return Page();
            }

            return Page();
        }
    }
}
