using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string KundenID { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public void OnPost()
    {
        //have to catch the Id that is inputed in the form
        KundenID = Request.Form["KundenID"];
        Response.Redirect($"/Kunden/Details/{KundenID}");
    }

}
