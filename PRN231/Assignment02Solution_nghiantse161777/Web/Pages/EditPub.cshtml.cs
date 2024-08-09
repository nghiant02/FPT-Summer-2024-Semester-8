using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.API_Interface;

namespace Web.Pages;

public class EditPubModel : PageModel
{
    private readonly IBookStoreApi? _bookStoreApi;

    public EditPubModel(IBookStoreApi? bookStoreApi)
    {
        _bookStoreApi = bookStoreApi;
    }
    [BindProperty]
    public PublisherResponseDto? Publisher { get; set; }
    public List<string> VietnameseCities { get; } = new List<string>
    {
        "Ha Noi",
        "Ho Chi Minh",
        "Da Nang",
        "Hai Phong",
        "Can Tho",
    };
    [BindProperty]
    public string? PublisherId { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        bool isLogin = HttpContext.Session.GetString("token") != null;
        if (!isLogin)
        {
            return RedirectToPage("/Login");
        }
        PublisherId = Request.Query["id"];
        var response = await _bookStoreApi.GetPublisherById(PublisherId);
        Publisher = response;
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var updatePublisher = new PublisherRequestDto
        {
            City = Publisher?.City,
            PublisherName = Publisher?.PublisherName,
            Country = Publisher?.Country,
            State = Publisher?.State,
        };
        try
        {
            PublisherId = Request.Query["id"];
            var response = await _bookStoreApi.UpdatePublisher(PublisherId, updatePublisher);
            if (response)
            {
                return RedirectToPage("/Publishers");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
        }

        return RedirectToPage("/Publishers");
    }
}
