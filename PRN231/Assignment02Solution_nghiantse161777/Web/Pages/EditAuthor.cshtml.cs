using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.API_Interface;

namespace Web.Pages;

public class EditAuthorModel : PageModel
{
    private readonly IBookStoreApi? _bookStoreApi;
    public List<string> VietnameseCities { get; } = new List<string>
    {
        "Ha Noi",
        "Ho Chi Minh",
        "Da Nang",
        "Hai Phong",
        "Can Tho",
    };
    [BindProperty] public AuthorResponseDto? Author { get; set; }
    public EditAuthorModel(IBookStoreApi? bookStoreApi)
    {
        _bookStoreApi = bookStoreApi;
    }

    [BindProperty] public string? AuthorId { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        bool isLogin = HttpContext.Session.GetString("token") != null;
        if (!isLogin)
        {
            return RedirectToPage("/Login");
        }
        AuthorId = Request.Query["id"];
        var response = await _bookStoreApi.GetAuthorById(AuthorId);
        Author = response;
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var updateAuthor = new AuthorUpdateRequestDto
        {
            Address = Author?.Address,
            City = Author?.City,
            EmailAddress = Author?.EmailAddress,
            FirstName = Author?.FirstName,
            LastName = Author?.LastName,
            Phone = Author?.Phone,
            MiddelName = Author?.MiddelName,
            State = Author?.State,
            Zip = Author?.Zip
        };
        try
        {
            AuthorId = Request.Query["id"];
            var response = await _bookStoreApi.UpdateAuthor(AuthorId, updateAuthor);
            return RedirectToPage("/Authors");

        } catch (Exception e)
        {
            return RedirectToPage("/Authors");
        }
        return Page();
    }
}
