using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.API_Interface;

namespace Web.Pages;

public class PublisersModel : PageModel
{
    private readonly IBookStoreApi? _bookStoreApi;

    public PublisersModel(IBookStoreApi? bookStoreApi)
    {
        _bookStoreApi = bookStoreApi;
    }
    [BindProperty] public List<PublisherResponseDto>? Publishers { get; set; }

    [BindProperty] public PublisherRequestDto? Publisher { get; set; }
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
        var response = await _bookStoreApi.GetPublishers();
        if (response != null)
        {
            Publishers = response.ToList();
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var response = await _bookStoreApi.CreatePublisher(Publisher);
            if (response != null)
            {
                return RedirectToPage();
            }
        }
        return Page();
    }
}
