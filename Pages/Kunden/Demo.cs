using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KundenDatenWebsite.Pages.Kunden;

public class DemoModel : PageModel
{
    private readonly ILogger<DemoModel> _logger;

    public string KundenID { get; set; }

    public DemoModel (ILogger<DemoModel> logger)
    {
        _logger = logger;
    }

    public DateTime determinStartTime ()
    {
        DateTime startTime = DateTime.Now;
        return startTime;
    }

    public void OnGet()
    {

    }

    public void OnPost()
    {
        //Take the information that is inputed in the form
        //and save it in the database
        DbConnection db = new DbConnection();
        DemoKunden demo = new DemoKunden();
        db.CreateDemoCustomer( Request.Form["Vorname"], Request.Form["Nachname"],Request.Form["Title"], Request.Form["Adresse"], Request.Form["Email"],  Request.Form["Telefonnummer"], Request.Form["Geburtsdatum"], determinStartTime(), demo.GenerateDemoID());

    }
}
