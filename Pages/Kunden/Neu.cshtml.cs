using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class NeuModel : PageModel
{
    private readonly ILogger<NeuModel> _logger;


    public NeuModel(ILogger<NeuModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {


    }

    public void OnPost()
    {
        Response.Redirect("/Kunden/Details?KundeID=1");
    }
}
