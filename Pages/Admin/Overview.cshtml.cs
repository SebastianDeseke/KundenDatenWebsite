using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages;

public class OverviewModel : PageModel
{
    private readonly ILogger<OverviewModel> _logger;

    public string KundenID { get; set; }

    public OverviewModel(ILogger<OverviewModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}