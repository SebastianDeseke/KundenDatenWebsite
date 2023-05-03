using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class NeuModel : PageModel
{
    private readonly ILogger<NeuModel> _logger;
    
    public string KundenID {get; set;}


    public NeuModel(ILogger<NeuModel> logger)
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
