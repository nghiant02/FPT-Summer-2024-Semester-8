using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.API_Interface;

namespace Web.Pages;

public class DeletePubModel : PageModel
{
    private readonly IBookStoreApi? _bookStoreApi;

    public DeletePubModel(IBookStoreApi? bookStoreApi)
    {
        _bookStoreApi = bookStoreApi;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        bool isLogin = HttpContext.Session.GetString("token") != null;
        if (!isLogin)
        {
            return RedirectToPage("/Login");
        }
        try
        {
            var result = await _bookStoreApi.DeletePublisher(Request.Query["id"]);

            if (result)
            {
                return RedirectToPage("/Publishers");
            }
            else
            {
                return RedirectToPage("/Publishers");
            }
        }
        catch (Exception)
        {
            return RedirectToPage("/Publishers");
        }
    }
}
