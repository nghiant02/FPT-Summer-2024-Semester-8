using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.API_Interface;

namespace Web.Pages
{
    public class DeleteAuthorModel : PageModel
    {
        private readonly IBookStoreApi? _bookStoreApi;

        public DeleteAuthorModel(IBookStoreApi? bookStoreApi)
        {
            _bookStoreApi = bookStoreApi;
        }

        public string? AuthorId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            bool isLogin = HttpContext.Session.GetString("token") != null;
            if (!isLogin)
            {
                return RedirectToPage("/Login");
            }
            try
            {
                AuthorId = Request.Query["id"];
                if (string.IsNullOrEmpty(AuthorId) || _bookStoreApi == null)
                {
                    return RedirectToPage("/Authors");
                }
                var result = await _bookStoreApi.DeleteAuthor(AuthorId);

                if (result == 1)
                {
                    return RedirectToPage("/Authors");
                }
                else
                {
                    return RedirectToPage("/Authors");
                }
            }
            catch (Exception)
            {
                return RedirectToPage("/Authors"); 
            }
        }
    }
}
