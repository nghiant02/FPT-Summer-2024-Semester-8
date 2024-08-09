using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.API_Interface;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly IBookStoreApi _testApi;

    public IndexModel(IBookStoreApi testApi)
    {
        _testApi = testApi;
    }
    public async Task<IActionResult> OnGetAsync()
    {
        bool isLogin = HttpContext.Session.GetString("token") != null;
        if (!isLogin)
        {
          return RedirectToPage("/Login");
        }
        return Page();
    }
}