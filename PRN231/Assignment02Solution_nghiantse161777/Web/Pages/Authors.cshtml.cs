using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.API_Interface;

namespace Web.Pages;

public class AuthorsModel : PageModel
{
    private readonly IBookStoreApi? _bookStoreApi;

    public AuthorsModel(IBookStoreApi? bookStoreApi)
    {
        _bookStoreApi = bookStoreApi;
    }
    [BindProperty]
    public AuthorRequestDto? Author { get; set; }
    [BindProperty]
    public List<AuthorResponseDto>? Authors { get; set; }
    public List<string> VietnameseCities { get; } = new List<string>
        {
            "Ha Noi",
            "Ho Chi Minh",
            "Da Nang",
            "Hai Phong",
            "Can Tho",
        };
    public async Task<IActionResult> OnGetAsync()
    {
        bool isLogin = HttpContext.Session.GetString("token") != null;
        if (!isLogin)
        {
            return RedirectToPage("/Login");
        }
        var response = await _bookStoreApi?.GetAuthors();
        if (response != null)
        {
            Authors = response.ToList();
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var response = await _bookStoreApi?.CreateAuthor(Author);
            if (response != null)
            {
                return RedirectToPage();
            }
        }
        return Page();
    }
}
