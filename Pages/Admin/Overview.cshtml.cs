using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages;

public class OverviewModel : PageModel
{
    private readonly ILogger<OverviewModel> _logger;
    public string KundenID { get; set; }
    public List<Dictionary<string, string>> Customers { get; set; }
    private readonly DbConnection _db;

    public OverviewModel(ILogger<OverviewModel> logger, DbConnection db)
    {
        _logger = logger;
        _db = db;
    }

    public void OnGet()
    {
        Customers = _db.GetAllCustomer();
    }
}