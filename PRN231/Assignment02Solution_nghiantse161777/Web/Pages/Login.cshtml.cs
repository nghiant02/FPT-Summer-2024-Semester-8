using BusinessObject.Dto.RequestDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using System.Text.Json;
using Web.API_Interface;

namespace Web.Pages;

public class LoginModel : PageModel
{
    private readonly IBookStoreApi _bookStoreApi;

    public LoginModel(IBookStoreApi bookStoreApi)
    {
        _bookStoreApi = bookStoreApi;
    }

    [BindProperty]
    public LoginDto? Login { get; set; }

    [BindProperty]
    public List<string?>? ErrorMessages { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Login == null)
        {
            ErrorMessages = new List<string?> { "Please fill the form" };
            return Page();
        }
        try
        {
            var response = await _bookStoreApi.Login(Login);
            if (response == null)
            {
                ErrorMessages = new List<string?> { "Invalid login request" };
                return Page();
            }
            else
            {
                if (string.IsNullOrEmpty(response.EmailAddress))
                {
                    ErrorMessages = new List<string?> { "Invalid login request" };
                    return Page();
                }
                HttpContext.Session.SetString("token", response.EmailAddress);
                return RedirectToPage("/Index");
            }
        }
        catch(ApiException ex)
        {
            if(ex.Content == null)
            {
                ErrorMessages = new List<string?> { "An unknown error occurred." };
                return Page();
            }
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(ex.Content);

            if (errorResponse != null && errorResponse.Errors != null)
            {
                ErrorMessages = errorResponse.Errors.Values.SelectMany(v => v).ToList();
            }
        }
        return Page();
    }
}
